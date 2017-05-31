using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieShopXS.Models
{
    public partial class Movie
    {
        public Movie()
        {
        
        }
       

        public string Title { get; set; }
        [Required]
        [Range(1900,2017)]
        public int Year { get; set; }
        [Required]
        public string Description { get; set; }

        [Required]
        [Range(0, 5)]
        [Display(Name ="rating")]
        public byte Stars { get; set; }


        public int GenreId { get; set; }
        public int DirectorId { get; set; }

        public virtual Person Director { get; set; }
        public virtual Genre Genre { get; set; }
    }
}
