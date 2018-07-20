namespace QL_Ruong.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChamSocChiTiet")]
    public partial class ChamSocChiTiet
    {
        [StringLength(100)]
        [Display(Name = "Tên thuốc/phân")]
        public string CSCT_TenThuoc { get; set; }

        [Display(Name = "Số lượng")]
        public int? CSCT_SoLuong { get; set; }

        [Display(Name = "Ngày")]
        public DateTime? CSCT_Ngay { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Mã phân biệt")]
        public int CSCT_ID { get; set; }

        [Display(Name = "Mã chăm sóc")]
        public int? CS_ID { get; set; }

        [Display(Name = "Tổng tiền")]
        public int? CSCT_TongTien { get; set; }

        public virtual ChamSoc ChamSoc { get; set; }
    }
}
