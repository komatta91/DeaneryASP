namespace DeaneryASP.Models.Storage.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Realisations
    {
        public Realisations()
        {
            Grades = new HashSet<Grades>();
            Registrations = new HashSet<Registrations>();
        }

        [Key]
        public int RealisationID { get; set; }

        [Required]
        [StringLength(1)]
        public string Ver { get; set; }

        public int SubjectID { get; set; }

        public int SemesterID { get; set; }

        public int? UserID { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] TimeStamp { get; set; }

        public virtual ICollection<Grades> Grades { get; set; }

        public virtual Semesters Semesters { get; set; }

        public virtual Subjects Subjects { get; set; }

        public virtual Users Users { get; set; }

        public virtual ICollection<Registrations> Registrations { get; set; }
    }
}
