using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OpenTable.Models.DataLayer.Configuration
{
    internal class ConfigureCuisines : IEntityTypeConfiguration<Cuisines>
    {
        public void Configure(EntityTypeBuilder<Cuisines> entity)
        {
            // seed initial data
            entity.HasData(
                new Cuisines { CuisinesId = 1, Cuisine = "Vietnamese" },
                new Cuisines { CuisinesId = 2, Cuisine = "Greek" },
                new Cuisines { CuisinesId = 3, Cuisine = "Spanish" },
                new Cuisines { CuisinesId = 4, Cuisine = "Lebanese" },
                new Cuisines { CuisinesId = 5, Cuisine = "Brazilian" }
            );
        }

    }
}
