using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OpenTable.Models.DataLayer.Configuration
{
    internal class ConfigureRestaurant : IEntityTypeConfiguration<Restaurant>
    {
        public void Configure(EntityTypeBuilder<Restaurant> entity)
        {
            // seed initial data
            entity.HasData(
                new Restaurant
                {
                    RestaurantId = 1,
                    RestaurantName = "Dragon Palace",
                    OpenHours = "11:00-22:00",
                    ImagePath = "DragonPalace",
                    CuisinesId = 1,
                    PriceRangeId = 2,
                    MetropolisId = 1,
                    Description = "Authentic Sichuan and Cantonese cuisine",
                    Address = "88 Mott Street, Chinatown, New York, NY 10013"
                },
                new Restaurant
                {
                    RestaurantId = 2,
                    RestaurantName = "Spice Route",
                    OpenHours = "12:00-23:00",
                    ImagePath = "SpiceRoute",
                    CuisinesId = 1,
                    PriceRangeId = 3,
                    MetropolisId = 2,
                    Description = "Modern Indian fusion with traditional flavors",
                    Address = "123 Sunset Boulevard, Los Angeles, CA 90026"
                },
                new Restaurant
                {
                    RestaurantId = 3,
                    RestaurantName = "Bangkok Nights",
                    OpenHours = "17:00-22:00",
                    ImagePath = "BangkokNights",
                    CuisinesId = 4,
                    PriceRangeId = 2,
                    MetropolisId = 3,
                    Description = "Award-winning Thai street food experience",
                    Address = "456 W Randolph Street, Chicago, IL 60606"
                },
                new Restaurant
                {
                    RestaurantId = 4,
                    RestaurantName = "Olive Grove",
                    OpenHours = "10:00-21:00",
                    ImagePath = "OliveGrove",
                    CuisinesId = 2,
                    PriceRangeId = 3,
                    MetropolisId = 4,
                    Description = "Sun-drenched flavors of the Mediterranean coast",
                    Address = "789 Kirby Drive, Houston, TX 77098"
                },
                new Restaurant
                {
                    RestaurantId = 5,
                    RestaurantName = "Seoul Kitchen",
                    OpenHours = "16:00-23:30",
                    ImagePath = "SeoulKitchen",
                    CuisinesId = 3,
                    PriceRangeId = 2,
                    MetropolisId = 5,
                    Description = "Modern Korean BBQ with premium ingredients",
                    Address = "101 Grant Avenue, San Francisco, CA 94108"
                }
            );
        }

    }
}
