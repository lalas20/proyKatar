namespace Api.Epsas.Entities;

using Api.Epsas.DataContext.Atributos;
using Api.Epsas.Entities.common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

[Table("seg_usuarios", Schema = "public")]
public class SegUsuarios : AuditableBaseEntity
{
    [Key]
    public int IdsegUsuarios { get; set; }
    [IsUpperCase] public string Login { get; set; }
    public string Cedula { get; set; }
    [IsUpperCase] public string Nombre { get; set; }
    public int IdsegRoles { get; set; }
    public int IdcEstadousaurio { get; set; }
    [JsonIgnore]
    public string Password { get; set; }

    [ForeignKey("IdsegRoles")]
    public SegRoles Roles { get; set; }

}
