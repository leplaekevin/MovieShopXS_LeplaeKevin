using System;
using System.Collections.Generic;

namespace MovieShopXSLib.ViewModels
{
    public partial class MovieVM
    {
        public MovieVM()
        {
        
        }

        public int MovieId { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public byte Stars { get; set; }

        public  string Director { get; set; }
        public  string Genre { get; set; }
    }
}
