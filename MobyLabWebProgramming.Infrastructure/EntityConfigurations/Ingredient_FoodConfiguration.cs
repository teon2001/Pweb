using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Infrastructure.EntityConfigurations
{
    public class Ingredient_FoodConfiguration : IEntityTypeConfiguration<Ingredient_Food>
    {
        public void Configure(EntityTypeBuilder<Ingredient_Food> builder)
        {
            builder.ToTable("Ingredient_Food");

            // Setarea cheii primare compuse
            builder.HasKey(fii => new { fii.FoodId, fii.IngredientId });

            // Configurarea relației many-to-many între Food și Ingredient prin entitatea intermediară
            builder.HasOne(fii => fii.Food)
                .WithMany(fi => fi.IngredientsFoods)
                .HasForeignKey(fii => fii.FoodId);

            builder.HasOne(fii => fii.Ingredient)
                .WithMany(i => i.IngredientsFoods)
                .HasForeignKey(fii => fii.IngredientId);
        }
    }

}
