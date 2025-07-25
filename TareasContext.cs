using Microsoft.EntityFrameworkCore;
//using proyectoef.Migrations;
using webapi.Models;

namespace webapi;

public class TareasContext : DbContext
{
    public DbSet<Categoria> categorias { get; set; }
    public DbSet<Tarea> tareas { get; set; }
    public DbSet<User> users { get; set; }
    public DbSet<History> Histories { get; set; }
    public TareasContext(DbContextOptions<TareasContext> options) : base(options) { }


    protected override void OnModelCreating(ModelBuilder modelBuilder)

    {
        List<Categoria> categoriasInit = new List<Categoria>();
        categoriasInit.Add(new Categoria() { CategoriaId = Guid.Parse("b3afe5cd-ad93-461f-89d5-c4360d6f1c7d"), Nombre = "Cocina", Peso = 1, Descripcion = "Debes hacer todos los quehaceres de la cocina" });
        categoriasInit.Add(new Categoria() { CategoriaId = Guid.Parse("b3afe5cd-ad93-461f-89d5-c4360d6f1c74"), Nombre = "Sala", Peso = 16, Descripcion = "Debes hacer todos los quehaceres de la cocina" });
        modelBuilder.Entity<Categoria>(categoria =>
        {
            categoria.ToTable("Categoria");
            categoria.HasKey(p => p.CategoriaId);
            categoria.Property(p => p.Nombre).IsRequired().HasMaxLength(150);
            categoria.Property(p => p.Peso);
            categoria.Property(p => p.Descripcion).IsRequired().HasMaxLength(500);
            categoria.HasData(categoriasInit);
        });

        List<Tarea> tareasInit = new List<Tarea>();
        tareasInit.Add(new Tarea() { TareaId = Guid.Parse("b3afe5cd-ad93-461f-89d5-c4360d6f1c66"), CategoriaId = Guid.Parse("b3afe5cd-ad93-461f-89d5-c4360d6f1c74"), Titulo = "Lavar las sillas", Descripcion = "Se debe lavar las sillas con Jabón", PrioridadTarea = Prioridad.Media, Frecuencia = 1 });
        tareasInit.Add(new Tarea() { TareaId = Guid.Parse("b3afe5cd-ad93-461f-89d5-c4360d6f1c68"), CategoriaId = Guid.Parse("b3afe5cd-ad93-461f-89d5-c4360d6f1c7d"), Titulo = "Lavar los platos", Descripcion = "Se debe lavar los platos con Jabón", PrioridadTarea = Prioridad.Media, Frecuencia = 2 });

        modelBuilder.Entity<Tarea>(tarea =>
        {
            tarea.ToTable("Tarea");
            tarea.HasKey(p => p.TareaId);
            tarea.HasOne(p => p.Categoria).WithMany(p => p.Tareas).HasForeignKey(p => p.CategoriaId);
            tarea.Property(p => p.Titulo).IsRequired().HasMaxLength(150);
            tarea.Property(p => p.Descripcion).IsRequired().HasMaxLength(500);
            tarea.Property(p => p.PrioridadTarea);
            tarea.Property(p => p.Frecuencia);
            tarea.Property(p => p.FechaCreacion).HasDefaultValueSql("CURRENT_TIMESTAMP"); ;
            tarea.Ignore(p => p.Resumen);
            tarea.HasData(tareasInit);
        });
        modelBuilder.Entity<User>(user =>
        {
            user.ToTable("User");
            user.HasKey(p => p.UserId);
            user.Property(p => p.Name).IsRequired().HasMaxLength(150);
            user.Property(p => p.LastName).IsRequired().HasMaxLength(150);
            user.Property(p => p.Password).IsRequired().HasMaxLength(150);
            user.Property(p => p.Email).IsRequired().HasMaxLength(150);
            user.Property(p => p.Phone).IsRequired().HasMaxLength(15);
            user.Property(p => p.ImageData).HasColumnType("varbinary(max)").IsRequired(false);
        });

        modelBuilder.Entity<History>(Histories =>
        {
            Histories.ToTable("History");
            Histories.HasKey(h => h.Id);
            Histories.Property(h => h.Title).IsRequired().HasMaxLength(1000);
            Histories.Property(h => h.Description).HasMaxLength(3000);
            Histories.Property(h => h.Date).HasDefaultValueSql("CURRENT_TIMESTAMP");
            Histories.Property(h => h.SprintName).HasMaxLength(150);
            Histories.Property(h => h.EstimationId);
            Histories.Property(h => h.AccessCode);

            Histories.HasOne<User>() // Relación con User si aplica
                .WithMany()
                .HasForeignKey(h => h.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

    }
}