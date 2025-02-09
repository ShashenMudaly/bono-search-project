using NpgsqlTypes;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query;

namespace BonoSearch.Models;

public class Movie
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Plot { get; set; }
} 