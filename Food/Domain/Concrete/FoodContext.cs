/*
 * Класс для связи Entity Framework с баззой данных
 * содержит коллекцию User
 */
using System.Data.Entity;
using Domain.Entities;

namespace Domain.Concrete
{
  public class FoodContext : DbContext
  {
    public DbSet<User> Users { get; set; }

  }
}