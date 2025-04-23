using MicroService_NaceTuIdea.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroService_NaceTuIdea.Context
{
    public class AppDbcontext : DbContext
    {
        public AppDbcontext(DbContextOptions<AppDbcontext> options) : base(options) { }

        public DbSet<Users> Users { get; set; }
        public DbSet<RespuestaR> RespuestaRs { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<Reseña> Reseñas { get; set; }
        public DbSet<ReglaLocal> ReglaLocals { get; set; }
        public DbSet<Notificacion> Notificacions { get; set; }
        public DbSet<Local> locals { get; set; }
        public DbSet<DisponibilidadLocal> DisponibilidadLocals { get; set; }
        public DbSet<ComodidadLocal> ComodidadLocals { get; set; }
        public DbSet<CategoriaLocal> CategoriaLocals { get; set; }
        public DbSet<AddService> AddServices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relación Users - Reserva (Uno a Muchos)
            modelBuilder.Entity<Reserva>()
                .HasOne(r => r.Usuario)
                .WithMany(u => u.Reservas)
                .HasForeignKey(r => r.usuarioid);

            // Relación Local - Reserva (Uno a Muchos)
            modelBuilder.Entity<Reserva>()
                .HasOne(r => r.Local)
                .WithMany(l => l.Reservas)
                .HasForeignKey(r => r.localid)
                .OnDelete(DeleteBehavior.NoAction);

            // Relación Users - Reseña (Uno a Muchos)
            modelBuilder.Entity<Reseña>()
                .HasOne(r => r.Usuario)
                .WithMany(u => u.Reseñas)
                .HasForeignKey(r => r.userid);

            // Relación Local - Reseña (Uno a Muchos)
            modelBuilder.Entity<Reseña>()
                .HasOne(r => r.Local)
                .WithMany(l => l.Reseñas)
                .HasForeignKey(r => r.localid)
                .OnDelete(DeleteBehavior.NoAction);

            // Relación Reseña - RespuestaR (Uno a Uno)
            modelBuilder.Entity<RespuestaR>()
                .HasOne(rr => rr.Reseña)
                .WithOne(r => r.Respuesta)
                .HasForeignKey<RespuestaR>(rr => rr.reseñaid);

            // Relación Users (Propietario) - RespuestaR (Uno a Muchos)
            modelBuilder.Entity<RespuestaR>()
                .HasOne(rr => rr.Propietario)
                .WithMany(u => u.Respuestas)
                .HasForeignKey(rr => rr.propietarioid)
                .OnDelete(DeleteBehavior.NoAction); // Considerar el OnDelete

            // Relación Users (Propietario) - Local (Uno a Muchos)
            modelBuilder.Entity<Local>()
                .HasOne(l => l.Propietario)
                .WithMany(u => u.Locales)
                .HasForeignKey(l => l.propietarioid)
                .OnDelete(DeleteBehavior.Cascade); // Considerar el OnDelete

            // Relación Users - Notificacion (Uno a Muchos)
            modelBuilder.Entity<Notificacion>()
                .HasOne(n => n.Usuario)
                .WithMany(u => u.Notificaciones)
                .HasForeignKey(n => n.usuarioid);

            // Relación Local - DisponibilidadLocal (Uno a Muchos)
            modelBuilder.Entity<DisponibilidadLocal>()
                .HasOne(d => d.Local)
                .WithMany(l => l.Disponibilidades)
                .HasForeignKey(d => d.LocalId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relación Local - ReglaLocal (Uno a Muchos)
            modelBuilder.Entity<ReglaLocal>()
                .HasOne(r => r.Local)
                .WithMany(l => l.Reglas)
                .HasForeignKey(r => r.LocalId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relación Local - AddService (Uno a Muchos)
            modelBuilder.Entity<AddService>()
                .HasOne(s => s.Local)
                .WithMany(l => l.ServiciosAdicionales)
                .HasForeignKey(s => s.LocalId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relación Muchos a Muchos entre Local y ComodidadLocal
            modelBuilder.Entity<Local>()
                .HasMany(l => l.Comodidades)
                .WithMany(c => c.Locales)
                .UsingEntity(j => j.ToTable("LocalComodidades"));

            // Relación Muchos a Muchos entre Local y CategoriaLocal
            modelBuilder.Entity<Local>()
                .HasMany(l => l.Categorias)
                .WithMany(c => c.Locales)
                .UsingEntity(j => j.ToTable("LocalCategorias"));

            
            modelBuilder.Entity<Reserva>(entity =>
                entity.Property(r => r.precio)
                    .HasColumnType("decimal(18, 2)"));

            modelBuilder.Entity<Local>(entity =>
                entity.Property(r => r.precioxhora)
                    .HasColumnType("decimal(18, 2)"));

            modelBuilder.Entity<AddService>(entity =>
                entity.Property(r => r.Precio)
                    .HasColumnType("decimal(18, 2)"));
        }

        public async Task<bool> SaveAsync()
        {
            return await SaveChangesAsync() > 0;
        }
    }
}
