namespace QL_Ruong.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Ruong")]
    public partial class Ruong
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ruong()
        {
            ChamSocs = new HashSet<ChamSoc>();
            ThuHoaches = new HashSet<ThuHoach>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Mã ruộng")]
        public int Ruong_ID { get; set; }

        [Display(Name = "Mã User")]
        public int? User_ID { get; set; }

        [StringLength(100)]
        [Display(Name = "Tên ruộng")]
        public string Ruong_Name { get; set; }

        [Display(Name = "Diện tích")]
        public double? Ruong_DienTich { get; set; }

        [StringLength(255)]
        [Display(Name = "Mô tẩ")]
        public string Ruong_MoTa { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChamSoc> ChamSocs { get; set; }

        public virtual User User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ThuHoach> ThuHoaches { get; set; }
    }
}
