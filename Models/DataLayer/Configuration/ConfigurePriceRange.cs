using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OpenTable.Models.DataLayer.Configuration
{
    internal class ConfigurePriceRange : IEntityTypeConfiguration<PriceRange>
    {
        public void Configure(EntityTypeBuilder<PriceRange> entity)
        {
            // seed initial data
            entity.HasData(
                new PriceRange { PriceRangeId = 1, PriceRanges = "$" },
                new PriceRange { PriceRangeId = 2, PriceRanges = "$$" },
                new PriceRange { PriceRangeId = 3, PriceRanges = "$$$" },
                new PriceRange { PriceRangeId = 4, PriceRanges = "$$$$" }
            );
        }

    }
}
