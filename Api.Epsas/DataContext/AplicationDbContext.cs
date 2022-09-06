using Api.Epsas.DataContext.Atributos;
using Api.Epsas.Entities.common;
using Api.Epsas.Interfaces.common;
using EntityFramework.Exceptions.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Api.Epsas.DataContext
{
    public class AplicationDbContext : GenericDataContext
    {
        private readonly IDateTimeService _dateTime;
        private readonly ICurrentUserService _user;

        public AplicationDbContext(DbContextOptions<AplicationDbContext> options, IDateTimeService dateTime, ICurrentUserService user) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _dateTime = dateTime;
            _user = user;
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.Entity.UsuarioModificacion = _user.Login;
                        entry.Entity.FechaModificacion = _dateTime.NowUtc;
                        entry.Property(x=> x.FechaCreacion).IsModified = false;
                        entry.Property(x=> x.UsuarioCreacion).IsModified = false;
                        break;
                    case EntityState.Added:
                        entry.Entity.UsuarioCreacion = _user.Login;
                        entry.Entity.FechaCreacion = _dateTime.NowUtc;                        
                        entry.Property(x => x.FechaModificacion).IsModified = false;
                        entry.Property(x => x.UsuarioModificacion).IsModified = false;                        
                        break;
                }

                var propAttr = entry.Entity.GetType().GetProperties().ToList().Where(prop => prop.IsDefined(typeof(IsUpperCase), false)).ToList();
                for (int i = 0; i < propAttr.Count; i++)
                {
                    var value = entry.Entity.GetType().GetProperty(propAttr[i].Name).GetValue(entry.Entity) != null ? entry.Entity.GetType().GetProperty(propAttr[i].Name).GetValue(entry.Entity).ToString().ToUpper() : null;
                    if (value != null)
                    {
                        entry.Entity.GetType().GetProperty(propAttr[i].Name).SetValue(entry.Entity, value);
                    }
                }

            }
            return base.SaveChangesAsync(cancellationToken);
        }




    }
}
