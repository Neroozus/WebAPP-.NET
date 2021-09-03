using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestAuth.Models
{
    public class ComputerPartsModel
    {

        [StringLength(20, ErrorMessage = "ManufacturerLength", MinimumLength = 2)]
        [RegularExpression(@"^[a-zA-Z0-9-а-яА-Я ]+$", ErrorMessage = "ManufacturerRegularExpression")]
        [Required(ErrorMessage = "ManufacturerRequired")]
        [Display(Name = "Manufacturer")]
        public string Manufacturer { get; set; }

        [StringLength(20, ErrorMessage = "ComputerPartLength", MinimumLength = 2)]
        [RegularExpression(@"^[a-zA-Z0-9-а-яА-Я ]+$", ErrorMessage = "ComputerPartRegularExpression")]
        [Required(ErrorMessage = "ComputerPartRequired")]
        [Display(Name = "ComputerPart")]
        public string ComputerPart { get; set; }

        [RegularExpression(@"^[0-9]+$", ErrorMessage = "QuantityRegularExpression")]
        [Required(ErrorMessage = "QuantityRequired")]
        [Display(Name = "Quantity")]
        public int? Quantity { get; set; }

        public int Id { get; set; }
    }
}
