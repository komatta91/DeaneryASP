namespace DeaneryASP.Models.Storage.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Students
    {
        public Students()
        {
            Registrations = new HashSet<Registrations>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StudentID { get; set; }

        public int GroupID { get; set; }

        [Required]
        [StringLength(10)]
        public string IndexNo { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamp { get; set; }

        public virtual Groups Groups { get; set; }

        public virtual ICollection<Registrations> Registrations { get; set; }

        public virtual Users Users { get; set; }
    }
}
