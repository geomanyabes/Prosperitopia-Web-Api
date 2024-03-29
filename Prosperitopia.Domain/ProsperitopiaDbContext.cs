﻿using Microsoft.EntityFrameworkCore;
using Prosperitopia.Domain.Model.Entity;

namespace Prosperitopia.Domain
{
    public class ProsperitopiaDbContext : DbContext
    {
        public DbSet<Item> Items { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public ProsperitopiaDbContext()
        {

        }
        public ProsperitopiaDbContext(DbContextOptions<ProsperitopiaDbContext> options) : base(options) 
        { 

        }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.



        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<Item>(entity =>
            {
                entity.ToTable("Item");
            });
        }
    }
}
