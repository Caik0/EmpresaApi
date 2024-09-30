using System.ComponentModel.DataAnnotations;
namespace EmpresaApi.Models
{
    public class Empresa
    {
        [Key]
    public int ID { get; set; }
    public string? Nome { get; set; }
    public ICollection<Endereco> Enderecos { get; } = new List<Endereco>();
    public ICollection<Funcionario> Funcionarios { get;} = new List<Funcionario>();
    }
}
