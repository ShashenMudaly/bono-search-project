using Microsoft.EntityFrameworkCore;
using BonoSearch.Models;

namespace BonoSearch.Data;

public class MovieDbContext : DbContext
{
    public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options) { }
    
    public DbSet<Movie> Movies { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Movie>();
            //.HasIndex(m => m.PlotVector)
            //.HasMethod("ivfflat")
            //.HasOperators("vector_l2_ops");

    }
} 