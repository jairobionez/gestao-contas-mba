using System.ComponentModel.DataAnnotations;

namespace GestaoContas.Api.Models
{
    public abstract class BaseModel
    {
        [Key]
        public Guid Id { get; set; }
    }
}
