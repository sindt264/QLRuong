namespace QL_Ruong.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChamSoc")]
    public partial class ChamSoc
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ChamSoc()
        {
            ChamSocChiTiets = new HashSet<ChamSocChiTiet>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name ="Mã chăm sóc")]
        public int CS_ID { get; set; }

        [Display(Name = "Mã ruộng")]
        public int? Ruong_ID { get; set; }

        [StringLength(20)]
        [Display(Name = "Tên")]
        public string CS_Name { get; set; }

        [StringLength(255)]
        [Display(Name = "Mô tả")]
        public string CS_MoTa { get; set; }

        [StringLength(20)]
        [Display(Name = "Mùa")]
        public string CS_Mua { get; set; }

        public virtual Ruong Ruong { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChamSocChiTiet> ChamSocChiTiets { get; set; }
    }
}
