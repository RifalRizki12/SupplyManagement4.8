using SupplyManagementAPI.Utilities.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupplyManagementAPI.Models
{
    [Table("tb_m_vendor")]
    public class Vendor : BaseEntity
    {
        [Key, ForeignKey("Company")]
        public Guid Guid { get; set; }

        [Column("bidang_usaha")]
        public string BidangUsaha { get; set; }

        [Column("jenis_perusahaan")]
        public string JenisPerusahaan { get; set; }

        [Column("status_vendor")]
        public StatusVendor StatusVendor { get; set; }

        public virtual Company Company { get; set; }
    }
}
