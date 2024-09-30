using System.Text.Json.Serialization;

namespace EmpresaApi.Models
{
    public class Funcionario
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Cargo { get; set;}
        public double Salario { get; set;}
        public int EmpresaId { get; set; }
        [JsonIgnore]
        public Empresa Empresa { get; set; } = null!;

    }
}
