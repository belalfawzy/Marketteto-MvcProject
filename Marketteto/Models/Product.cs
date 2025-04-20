using Marketteto.Data.Base;
using Marketteto.Data.Enum;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Marketteto.Models
{
    public class Product : IBaseEntity
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public ProductColor Color { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [ValidateNever]
        public Category category { get; set; }
    }
}
