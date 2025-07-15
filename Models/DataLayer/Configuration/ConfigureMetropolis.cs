using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OpenTable.Models.DataLayer.Configuration
{
    internal class ConfigureMetropolis: IEntityTypeConfiguration<Metropolis>
    {
        public void Configure(EntityTypeBuilder<Metropolis> entity)
        {
            // seed initial data
            entity.HasData(
                new Metropolis { MetropolisId = 1, Name = "Toronto" },
                new Metropolis { MetropolisId = 2, Name = "Vancouver" },
                new Metropolis { MetropolisId = 3, Name = "Montreal" },
                new Metropolis { MetropolisId = 4, Name = "Boston" },
                new Metropolis { MetropolisId = 5, Name = "Washington, D.C." },
                new Metropolis { MetropolisId = 6, Name = "Atlanta" },
                new Metropolis { MetropolisId = 7, Name = "Dallas" }
            );
        }
    }
}
