namespace DeaneryASP.Models.Storage.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Registrations
    {
        public Registrations()
        {
            GradeValues = new HashSet<GradeValues>();
        }

        [Key]
        public int RegistrationID { get; set; }

        public int StudentID { get; set; }

        public int RealisationID { get; set; }

        [StringLength(5)]
        public string Value { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamp { get; set; }

        public virtual ICollection<GradeValues> GradeValues { get; set; }

        public virtual Realisations Realisations { get; set; }

        public virtual Students Students { get; set; }
    }
}
