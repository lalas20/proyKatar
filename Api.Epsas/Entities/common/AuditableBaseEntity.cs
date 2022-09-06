namespace Api.Epsas.Entities.common
{
    public abstract class AuditableBaseEntity
    {
        public string? UsuarioCreacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string? UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }

    }
}
