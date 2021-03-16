namespace No_Balloons.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Article")]
    public partial class Article
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public string Content { get; set; }

        [Column(TypeName = "date")]
        public DateTime ArticleDate { get; set; }

        [Required]
        [StringLength(50)]
        public string Image { get; set; }

        [Required]
        public string Summary { get; set; }

        [Required]
        [StringLength(1)]
        public string Main { get; set; }

        [Key]
        [StringLength(50)]
        public string Link { get; set; }
    }
}
