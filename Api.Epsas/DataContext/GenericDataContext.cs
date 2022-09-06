using Api.Epsas.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Epsas.DataContext
{

    public class GenericDataContext : DbContext
    {
        public GenericDataContext(DbContextOptions options) : base(options) { }

        //TODO: 001 - Crear Entidades en la Carpete Entities
        //TODO: 002 - Registrar DBSET en GenericDataContext

        public DbSet<SegUsuarios> Usuarios { get; set; }
        public DbSet<SegRoles> Roles { get; set; }
        public DbSet<ParamClasificador> Clasificador { get; set; }

    }
}