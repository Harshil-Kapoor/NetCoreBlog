using ExploreCalifornia.ViewComponents;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace ExploreCalifornia.Models
{
    public class BlogDataContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }

        public IQueryable<MonthlySpecial> MonthlySpecials
        {
            get
            {
                return new[]
                {
                    new MonthlySpecial
                    {
                        Key = "calm",
                        Name = "California Calm",
                        Type = "Day Spa Package",
                        Price = 250,
                    },
                    new MonthlySpecial
                    {
                        Key = "desert",
                        Name = "From desert to sea",
                        Type = "2 Day Salton Sea",
                        Price = 350,
                    },
                    new MonthlySpecial
                    {
                        Key = "backpack",
                        Name = "Backpack Cali",
                        Type = "Big Sur Retreat",
                        Price = 620,
                    },
                    new MonthlySpecial
                    {
                        Key = "taste",
                        Name = "Taste of California",
                        Type = "Tapas & Groves",
                        Price = 150,
                    }
                }.AsQueryable();
            }
        }

        public BlogDataContext([NotNullAttribute] DbContextOptions options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
