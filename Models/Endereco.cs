using System.Text.Json.Serialization;

namespace EmpresaApi.Models
{
    public class Endereco
    {
        public int Id { get; set; }
        public string? Rua { get; set; }
        public string? Cidade { get; set; }
        public string? Estado { get; set; }
        public string? Cep { get; set; }
        public int EmpresaId { get; set; }
        [JsonIgnore]
        public Empresa Empresa { get; set; } = null!;
    }
}
