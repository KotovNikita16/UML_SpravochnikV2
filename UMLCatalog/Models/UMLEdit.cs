using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace UMLCatalog.Models
{
    public class UMLEdit
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Необходимо заполнить!")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Необходимо заполнить!")]
        public string Description { get; set; }
        public IFormFile File { get; set; }
        public string Code { get; set; }
    }
}
