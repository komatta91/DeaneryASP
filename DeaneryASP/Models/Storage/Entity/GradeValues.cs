namespace DeaneryASP.Models.Storage.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class GradeValues
    {
        [Key]
        public int GradeValueID { get; set; }

        public int GradeID { get; set; }

        public int RegistrationID { get; set; }

        [Required]
        [StringLength(10)]
        public string Value { get; set; }

        [Required]
        [StringLength(10)]
        public string Date { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamp { get; set; }

        public virtual Grades Grades { get; set; }

        public virtual Registrations Registrations { get; set; }
    }
}
