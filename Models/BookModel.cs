using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace FirstApp.Models
{
    public class BookModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please Insert Book Name")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Please Insert Author Name")]
        public string Author { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        [Required(ErrorMessage = "Please select your book language")]
        public string Language { get; set; }
        [Required(ErrorMessage = "Please Insert Total page of Book")]
        public int? TotalPages { get; set; }
        [Display(Name = "Insert the image of Cover Photo")]
        [Required(ErrorMessage = "Please Insert the image of Cover Photo")]
        public IFormFile CoverPhoto { get; set; }
        public string CoverPhotoUrl { get; set; }
    }
}
