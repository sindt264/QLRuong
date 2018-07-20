namespace QL_Ruong.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            Ruongs = new HashSet<Ruong>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Mã User")]
        public int User_ID { get; set; }

        [Display(Name = "Mã quyền")]
        public short? CTUSER_ID { get; set; }

        [StringLength(100)]
        [Display(Name = "Tên")]
        public string User_Name { get; set; }

        [StringLength(255)]
        [Display(Name = "Mô tả")]
        public string User_Detail { get; set; }

        [StringLength(20)]
        [Display(Name = "Tài khoản")]
        public string User_TK { get; set; }

        [StringLength(500)]
        [Display(Name = "Mật khẩu")]
        public string User_MK { get; set; }

        public virtual ChiTietUs ChiTietUs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ruong> Ruongs { get; set; }
    }
}
