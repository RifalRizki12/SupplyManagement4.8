using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupplyManagementAPI.Models
{
    [Table("tb_m_company")]
    public class Company : BaseEntity
    {
        [Key, Column("guid")]
        public Guid Guid { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("address")]
        public string Address { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("phone_number")]
        public string PhoneNumber { get; set; }

        [Column("foto")]
        public string Foto { get; set; }

        public virtual Account Account { get; set; }
        public virtual Vendor Vendor { get; set; }
    }
}
