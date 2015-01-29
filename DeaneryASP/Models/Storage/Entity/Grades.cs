namespace DeaneryASP.Models.Storage.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Grades
    {
        public Grades()
        {
            GradeValues = new HashSet<GradeValues>();
        }

        [Key]
        public int GradeID { get; set; }

        public int RealisationID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(64)]
        public string MaxValue { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamp { get; set; }

        public virtual Realisations Realisations { get; set; }

        public virtual ICollection<GradeValues> GradeValues { get; set; }
    }
}
