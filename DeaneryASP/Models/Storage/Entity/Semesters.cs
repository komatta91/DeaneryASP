namespace DeaneryASP.Models.Storage.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Semesters
    {
        public Semesters()
        {
            Realisations = new HashSet<Realisations>();
        }

        [Key]
        public int SemesterID { get; set; }

        [Required]
        [StringLength(10)]
        public string Name { get; set; }

        [Required]
        [StringLength(1)]
        public string Active { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamp { get; set; }

        public virtual ICollection<Realisations> Realisations { get; set; }
    }
}
