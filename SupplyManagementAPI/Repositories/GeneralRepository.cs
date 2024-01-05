using SupplyManagementAPI.Contracts;
using SupplyManagementAPI.Data;
using SupplyManagementAPI.Utilities.Handler;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace SupplyManagementAPI.Repositories
{
    public class GeneralRepository<TEntity> : IGeneralRepository<TEntity> where TEntity : class
    {
        protected readonly SupplyManagementDbContext _context;  // Mendefinisikan instance dari DbContext untuk mengakses database.

        public GeneralRepository(SupplyManagementDbContext context)
        {
            _context = context;  // Menginisialisasi DbContext melalui konstruktor.
        }

        // Mendapatkan semua entitas TEntity dari database dan mengembalikannya dalam bentuk IEnumerable.
        public IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }

        // Mencari entitas TEntity berdasarkan GUID yang diberikan.
        public TEntity GetByGuid(Guid guid)
        {
            var entity = _context.Set<TEntity>().Find(guid);
            _context.Entry(entity).State = EntityState.Detached; // Detach entity to avoid unnecessary tracking.
            return entity ?? default(TEntity);
        }

        // Menambahkan entitas baru TEntity ke dalam database.
        public TEntity Create(TEntity entity)
        {
            try
            {
                _context.Set<TEntity>().Add(entity);  // Menambahkan entitas ke DbSet yang sesuai.
                _context.SaveChanges();  // Menyimpan perubahan ke dalam database.
                return entity;  // Mengembalikan entitas yang berhasil ditambahkan.
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.Contains("IX_tb_m_employees_email"))
                {
                    throw new ExceptionHandler("Email already exists");
                }
                if (ex.InnerException != null && ex.InnerException.Message.Contains("IX_tb_m_employees_phone_number"))
                {
                    throw new ExceptionHandler("Phone number already exists");
                }
                throw new ExceptionHandler(ex.InnerException?.Message ?? ex.Message);
            }
        }


        // Memperbarui entitas TEntity yang ada dalam database.
        public bool Update(TEntity entity)
        {
            try
            {
                // Detach entity to avoid tracking issues
                var entry = _context.Entry(entity);
                if (entry.State == EntityState.Detached)
                {
                    _context.Set<TEntity>().Attach(entity);
                    entry.State = EntityState.Modified;
                }

                _context.SaveChanges();  // Menyimpan perubahan ke dalam database.
                return true;  // Mengembalikan true jika pembaruan berhasil.
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.Contains("IX_tb_m_employees_email"))
                {
                    throw new ExceptionHandler("Email sudah ada !!!");
                }
                if (ex.InnerException != null && ex.InnerException.Message.Contains("IX_tb_m_employees_phone_number"))
                {
                    throw new ExceptionHandler("Phone number already exists");
                }
                throw new ExceptionHandler(ex.InnerException?.Message ?? ex.Message);
            }
        }

        // Menghapus entitas TEntity dari database.
        public bool Delete(TEntity entity)
        {
            try
            {
                _context.Set<TEntity>().Remove(entity);  // Menghapus entitas dari DbSet yang sesuai.
                _context.SaveChanges();  // Menyimpan perubahan ke dalam database.
                return true;  // Mengembalikan true jika penghapusan berhasil.
            }
            catch
            {
                return false;  // Mengembalikan false jika terjadi kesalahan selama penghapusan.
            }
        }
    }
}
