using Microsoft.EntityFrameworkCore;
using ToDosFE.Business.Entities;

namespace ToDosFE.Business.Persistence;

public class ToDosDbContext : DbContext
{
    public DbSet<ToDo> ToDos { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("ToDosDb");
    }
}