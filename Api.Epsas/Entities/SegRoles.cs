using Api.Epsas.Entities.common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Epsas.Entities
{

    [Table("seg_roles", Schema = "public")]
    public class SegRoles : AuditableBaseEntity
    {
        [Key]
        public int IdsegRoles { get; set; }
        public string? Rol { get; set; }
        public string? Descripcion { get; set; }


    }
}
