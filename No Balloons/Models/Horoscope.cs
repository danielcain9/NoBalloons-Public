namespace No_Balloons.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Horoscope")]
    public partial class Horoscope
    {
        [Key]
        [StringLength(50)]
        public string Sign { get; set; }

        [Required]
        public string Message { get; set; }
    }
}
