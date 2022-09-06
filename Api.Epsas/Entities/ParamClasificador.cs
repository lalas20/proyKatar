using Api.Epsas.Entities.common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Epsas.Entities
{

    [Table("param_clasificador", Schema = "public")]
    public class ParamClasificador : AuditableBaseEntity
    {
        [Key]
        public int IdparamClasificador { get; set; }

        public int IdparamClasificadortipo { get; set; }
        public string Descripcion { get; set; }
        public string? Valor1 { get; set; }

    }
}
