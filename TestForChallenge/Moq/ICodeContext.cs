using CodeChallenge.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;

public interface ICodeContext : IDisposable
{
    DbSet<Product> Products { get; set; }
    DbSet<Order> Orders { get; set; }

    int SaveChanges();
    EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
}