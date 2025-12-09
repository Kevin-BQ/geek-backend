using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Configuration
{
    public class RoleUserApplicationConfiguration : IEntityTypeConfiguration<RoleUserAplication>
    {
        public void Configure(EntityTypeBuilder<RoleUserAplication> builder)
        {
            // Configurar clave primaria compuesta
            builder.HasKey(r => new { r.UserId, r.RoleId });

            // Relación con Usuario
            builder.HasOne(r => r.UserAplication)
                   .WithMany(u => u.UserRole)
                   .HasForeignKey(r => r.UserId)
                   .OnDelete(DeleteBehavior.NoAction); // Evitar cascada

            // Relación con Rol
            builder.HasOne(r => r.RoleAplication)
                   .WithMany(r => r.RoleUser)
                   .HasForeignKey(r => r.RoleId)
                   .OnDelete(DeleteBehavior.NoAction); // Evitar cascada
        }
    }
}
