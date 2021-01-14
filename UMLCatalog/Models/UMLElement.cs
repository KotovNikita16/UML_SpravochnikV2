using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace UMLCatalog.Models
{
    public class UMLElement
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Необходимо заполнить!")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Необходимо заполнить!")]
        public string Description { get; set; }
        public byte[] Picture { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "Необходимо прикрепить диаграмму!")]
        [DataType(DataType.Upload)]
        public IFormFile File { get; set; }
        public string ImgType { get; set; }
        public string Code { get; set; }
        public string Tags { get; set; }
        [Required(ErrorMessage = "Необходимо выбрать тип содержимого!")]
        public string DiagramOrElement { get; set; }
    }
}
