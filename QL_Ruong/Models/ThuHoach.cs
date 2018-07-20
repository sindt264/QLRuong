namespace QL_Ruong.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ThuHoach")]
    public partial class ThuHoach
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "ID")]
        public int TH_ID { get; set; }

        [Display(Name = "Mã ruộng")]
        public int? Ruong_ID { get; set; }

        [StringLength(100)]
        [Display(Name = "Tên mùa")]
        public string TH_Name { get; set; }

        [Display(Name = "Số dạ")]
        public int? TH_ChiSo { get; set; }

        [Display(Name = "Tiền/dạ")]
        public int? TH_TienTrenDa { get; set; }

        [Display(Name = "Tổng tiền")]
        public int? TH_TongTien { get; set; }

        [StringLength(255)]
        [Display(Name = "Ghi chú")]
        public string TH_MoTa { get; set; }

        public virtual Ruong Ruong { get; set; }
    }
}
