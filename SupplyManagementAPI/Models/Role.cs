using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupplyManagementAPI.Models
{
    [Table("tb_m_roles")]
    public class Role : BaseEntity
    {
        [Key, Column("guid")]
        public Guid Guid { get; set; }

        [Column("name")]
        public string Name { get; set; }

        // Kardinalitas
        public ICollection<Account> Accounts { get; set; }
    }
}
