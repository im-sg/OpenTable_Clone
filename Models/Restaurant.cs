using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace OpenTable.Models
{
    public class Restaurant
    {
        public int RestaurantId { get; set; }

        [Required(ErrorMessage = "Please enter a RestaurantName.")]
        public string RestaurantName { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Please enter a OpenHours.")]
        public string OpenHours { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a ImagePath.")]
        public string ImagePath { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a Description.")]
        public string? Description { get; set; }
        
        [Required(ErrorMessage = "Please enter a Address.")]
        [StringLength(200, ErrorMessage = "Address cannot exceed 200 characters.")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "Please enter a Metropolis.")]
        public int MetropolisId { get; set; }
        [ValidateNever]
        public Metropolis Metropolis { get; set; } = null!;

        [Required(ErrorMessage = "Please enter a Cuisines.")]
        public int CuisinesId { get; set; }
        [ValidateNever]
        public Cuisines Cuisines { get; set; } = null!; 
        
        [Required(ErrorMessage = "Please enter a PriceRange.")]
        public int PriceRangeId { get; set; }
        [ValidateNever]
        public PriceRange PriceRange { get; set; } = null!; 
    }
}
