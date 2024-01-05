using SupplyManagementAPI.Utilities.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupplyManagementAPI.Models
{
    [Table("tb_m_accounts")]
    public class Account : BaseEntity
    {
        [Key, ForeignKey("Company")]
        public Guid Guid { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [Column("otp")]
        public int Otp { get; set; }

        [Column("status")]
        public StatusLevel Status { get; set; }

        [Column("is_used")]
        public bool IsUsed { get; set; }

        [Column("expired_time")]
        public DateTime ExpiredTime { get; set; }

        [Column("role_guid")]
        public Guid RoleGuid { get; set; }

        // Kardinalitas
        public virtual Company Company { get; set; }
        public virtual Role Role { get; set; }
    }
}
