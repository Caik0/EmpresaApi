using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EmpresaApi.Models;

namespace EmpresaApi.Data
{
    public class EmpresaApiContext : DbContext
    {
        public EmpresaApiContext (DbContextOptions<EmpresaApiContext> options)
            : base(options)
        {
        }

        public DbSet<EmpresaApi.Models.Empresa> Empresa { get; set; } = default!;
        public DbSet<EmpresaApi.Models.Endereco> Endereco { get; set; } = default!;
        public DbSet<EmpresaApi.Models.Funcionario> Funcionario { get; set; } = default!;
    }
}
