namespace DeaneryASP.Models.Storage.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Users
    {
        public Users()
        {
            Realisations = new HashSet<Realisations>();
        }

        [Key]
        public int UserID { get; set; }

        [Required]
        [StringLength(16)]
        public string UID { get; set; }

        [Required]
        [StringLength(16)]
        public string PWD { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamp { get; set; }

        public virtual ICollection<Realisations> Realisations { get; set; }

        public virtual Students Students { get; set; }
    }
}
