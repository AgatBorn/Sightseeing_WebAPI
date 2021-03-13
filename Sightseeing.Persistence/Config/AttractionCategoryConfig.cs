using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sightseeing.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sightseeing.Persistence.Config
{
    public class AttractionCategoryConfig : IEntityTypeConfiguration<AttractionCategory>
    {
        public void Configure(EntityTypeBuilder<AttractionCategory> builder)
        {
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
