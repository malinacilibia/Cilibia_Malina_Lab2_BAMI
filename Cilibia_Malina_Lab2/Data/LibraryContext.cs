using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Cilibia_Malina_Lab2Context.Models;

namespace Cilibia_Malina_Lab2Context.Data
{
    public class LibraryContext : DbContext
    {
        public LibraryContext (DbContextOptions<LibraryContext> options)
            : base(options)
        {
        }

        public DbSet<Cilibia_Malina_Lab2Context.Models.Book> Book { get; set; } = default!;
        public DbSet<Cilibia_Malina_Lab2Context.Models.Customer> Customer { get; set; } = default!;
        public DbSet<Cilibia_Malina_Lab2Context.Models.Genre> Genre { get; set; } = default!;
        public DbSet<Cilibia_Malina_Lab2Context.Models.Author> Author { get; set; }
        public DbSet<Cilibia_Malina_Lab2Context.Models.Order> Order { get; set; } = default!;
    }
}

