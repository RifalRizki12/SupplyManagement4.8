using System;
using System.IO;
using System.Transactions;
using System.Web.Http;
using SupplyManagementAPI.Contracts;
using SupplyManagementAPI.DTOs.Accounts;
using SupplyManagementAPI.DTOs.Tokens;
using SupplyManagementAPI.Models;
using SupplyManagementAPI.Utilities.Handler;

namespace SupplyManagementAPI.Controllers
{

    public class AccountController : ApiController
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IVendorRepository _vendorRepository;
        private readonly ITokenHandlers _tokenHandler;

        public AccountController(ICompanyRepository companyRepository, IAccountRepository accountRepository, IRoleRepository roleRepository, IVendorRepository vendorRepository, ITokenHandlers tokenHandler)
        {
            _companyRepository = companyRepository;
            _accountRepository = accountRepository;
            _roleRepository = roleRepository;
            _vendorRepository = vendorRepository;
            _tokenHandler = tokenHandler;
        }

        [HttpPost]
        [Route("registerCompany")]
        public IHttpActionResult RegisterClient(RegisterCompanyDto registrationDto)
        {
            if (ModelState.IsValid)
            {
                using (var transactionScope = new TransactionScope())
                {
                    try
                    {
                        Account account = registrationDto;
                        Company company = registrationDto;
                        Vendor vendor = registrationDto;

                        // Handle pengunggahan foto
                        byte[] photoBytes = null;

                        if (registrationDto.FotoCompany != null && registrationDto.FotoCompany.ContentLength > 0)
                        {
                            using (var memoryStream = new MemoryStream())
                            {
                                registrationDto.FotoCompany.InputStream.CopyTo(memoryStream);
                                photoBytes = memoryStream.ToArray();
                            }

                            // Simpan nama berkas unik ke atribut Foto pada objek company
                            company.Foto = $"{DateTime.Now:yyyyMMddHHmmssfff}_{Guid.NewGuid()}_{Path.GetFileName(registrationDto.FotoCompany.FileName)}";
                        }

                        // Handle konfirmasi password
                        if (registrationDto.ConfirmPassword != registrationDto.Password)
                        {
                            return BadRequest("Password and Confirm Password do not match.");
                        }

                        // Simpan Company dalam repository
                        _companyRepository.Create(company);

                        // Simpan foto ke sistem file jika ada
                        if (photoBytes != null)
                        {
                            string uploadPath = "Utilities/File/FotoCompany/"; // Ganti dengan direktori yang sesuai
                            string filePath = Path.Combine(uploadPath, company.Foto);

                            using (var fileStream = new FileStream(filePath, FileMode.Create))
                            {
                                fileStream.Write(photoBytes, 0, photoBytes.Length);
                            }
                        }

                        // Hubungkan Vendor dengan Company
                        vendor.Guid = company.Guid;
                        _vendorRepository.Create(vendor);

                        // Hubungkan Account dengan Company pemilik
                        account.Guid = company.Guid;
                        account.RoleGuid = _roleRepository.GetDefaultGuid() ?? throw new Exception("Default role not found");
                        account.Password = HashHandler.HashPassword(registrationDto.Password);

                        // Simpan Account dalam repository
                        _accountRepository.Create(account);

                        // Commit transaksi jika semua operasi berhasil
                        transactionScope.Complete();

                        return Ok("Registration successful, Waiting for Admin Approval");
                    }
                    catch (Exception ex)
                    {
                        // Rollback transaksi jika terjadi kesalahan
                        return BadRequest("Registration failed. " + ex.Message);
                    }
                }
            }

            return BadRequest("Invalid request data.");
        }

        [HttpGet]
        [Route("{guid}")]
        public IHttpActionResult GetByGuid(Guid guid)
        {
            try
            {
                // Memanggil metode GetByGuid dari _accountRepository dengan parameter GUID.
                var result = _accountRepository.GetByGuid(guid);

                if (result == null)
                {
                    return NotFound();
                }

                // Mengembalikan data yang ditemukan dalam respons OK.
                return Ok((AccountDto)result);
            }
            catch (ExceptionHandler ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPut]
        public IHttpActionResult Update(AccountDto accountDto)
        {
            try
            {
                //get data by guid dan menggunakan format DTO 
                var entity = _accountRepository.GetByGuid(accountDto.Guid);
                if (entity == null) //cek apakah data berdasarkan guid tersedia 
                {
                    return NotFound();
                }
                //convert data DTO dari inputan user menjadi objek Account
                Account toUpdate = accountDto;
                //menyimpan createdate yg lama
                toUpdate.CreatedDate = entity.CreatedDate;
                toUpdate.Password = entity.Password;
                toUpdate.RoleGuid = entity.RoleGuid;

                //update Account dalam repository
                _accountRepository.Update(toUpdate);

                // return HTTP OK dengan kode status 200 dan return "data updated" untuk sukses update.
                return Ok("Data Updated");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
