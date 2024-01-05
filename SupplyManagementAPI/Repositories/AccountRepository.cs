
using SupplyManagementAPI.Models;
using SupplyManagementAPI.Contracts;
using SupplyManagementAPI.Data;
using System.Linq;

namespace SupplyManagementAPI.Repositories
{
    public class AccountRepository : GeneralRepository<Account>, IAccountRepository
    {
        private readonly SupplyManagementDbContext _context;

        // Konstruktor AccountRepository yang menerima ManagementDbContext
        public AccountRepository(SupplyManagementDbContext context) : base(context)
        {
            _context = context;
        }

        // Metode untuk mendapatkan akun berdasarkan alamat email
        public Account GetByCompanyEmail(string companyEmail)
        {
            // Melakukan join antara tabel Akun (Account) dan Employee berdasarkan email Employee
            var account = (from acc in _context.Accounts
                           join company in _context.Companies on acc.Guid equals company.Guid
                           where company.Email == companyEmail
                           select acc).FirstOrDefault();

            return account;
        }
    }

}
