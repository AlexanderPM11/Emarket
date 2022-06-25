using Emarket.Core.Aplication.Helpers;
using Emarket.Core.Aplication.Viewmodels.Users;
using Emarket.Core.Domain.Common;
using Emarket.Core.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Emarket.Infrastructure.Persistence.Contexts
{
   public class EmarketContext:DbContext
    {
        
        
        public EmarketContext(DbContextOptions<EmarketContext> options) : base(options) 
        {
        }
        public DbSet<Adds> Adds { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Categories> Categories { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancelToken = new CancellationToken())
        {
            foreach (var entri in ChangeTracker.Entries<AuditableBase>())
            {
                switch (entri.State)
                {
                    case EntityState.Added:
                        entri.Entity.Created = DateTime.Now;
                        entri.Entity.CreateBy = "default";
                        break;
                    case EntityState.Modified:
                        entri.Entity.LastModified = DateTime.Now;
                        entri.Entity.LastModifiedBy = "default";
                        break;
                }
            }

            return base.SaveChangesAsync(cancelToken);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region tables
            modelBuilder.Entity<Adds>().ToTable("Adds");
            modelBuilder.Entity<Categories>().ToTable("Categories");
            modelBuilder.Entity<User>().ToTable("Users");
            #endregion

            #region"primary keys"
            modelBuilder.Entity<Adds>().HasKey(h => h.Id);
            modelBuilder.Entity<Categories>().HasKey(h => h.Id);
            modelBuilder.Entity<User>().HasKey(h => h.Id);
            #endregion

            #region"Relationships"
            modelBuilder.Entity<Categories>()
                .HasMany<Adds>(s => s.Adds)
                .WithOne(g => g.Categories)
                .HasForeignKey(s => s.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);            
            modelBuilder.Entity<User>()
               .HasMany<Adds>(s => s.Adds)
               .WithOne(g => g.Users)
               .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>()
               .HasMany<Categories>(s => s.Categories)
               .WithOne(g => g.Users)
               .HasForeignKey(s => s.usId)
               .OnDelete(DeleteBehavior.Cascade);
            
            #endregion

            #region"Property configurations"

            #region Adds
            modelBuilder.Entity<Adds>()
                .Property(p => p.NameArticul)
                .IsRequired();
                        
            modelBuilder.Entity<Adds>()
                .Property(p => p.Price)
                .IsRequired();
            modelBuilder.Entity<Adds>()
                .Property(p => p.CategoryId)
                .IsRequired();
            modelBuilder.Entity<Adds>()
                .Property(p => p.UserId)
                .IsRequired();

            #endregion

            #region categories
            modelBuilder.Entity<Categories>()
                .Property(p => p.Name)
                .HasMaxLength(100)
                .IsRequired();
            modelBuilder.Entity<Categories>()
               .Property(p => p.Descripcion)
               .HasMaxLength(100)
               .IsRequired();
            #endregion
            #endregion

            #region User
            modelBuilder.Entity<User>()
               .Property(u => u.Username)
               .IsRequired();
            modelBuilder.Entity<User>()
               .Property(u => u.Password)
               .IsRequired();
            modelBuilder.Entity<User>()
              .Property(u => u.Email)
              .IsRequired();
            modelBuilder.Entity<User>()
              .Property(u => u.Phone)
              .IsRequired();
            modelBuilder.Entity<User>()
              .Property(u => u.Name)
              .IsRequired();

            #endregion
        }












    }
}
