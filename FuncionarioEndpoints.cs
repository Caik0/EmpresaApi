using Microsoft.EntityFrameworkCore;
using EmpresaApi.Data;
using EmpresaApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
namespace EmpresaApi;

public static class FuncionarioEndpoints
{
    public static void MapFuncionarioEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Funcionario").WithTags(nameof(Funcionario));

        group.MapGet("/", async (EmpresaApiContext db) =>
        {
            return await db.Funcionario.ToListAsync();
        })
        .WithName("GetAllFuncionarios")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Funcionario>, NotFound>> (int id, EmpresaApiContext db) =>
        {
            return await db.Funcionario.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is Funcionario model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetFuncionarioById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, Funcionario funcionario, EmpresaApiContext db) =>
        {
            var affected = await db.Funcionario
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.Id, funcionario.Id)
                    .SetProperty(m => m.Nome, funcionario.Nome)
                    .SetProperty(m => m.Cargo, funcionario.Cargo)
                    .SetProperty(m => m.Salario, funcionario.Salario)
                    .SetProperty(m => m.EmpresaId, funcionario.EmpresaId)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateFuncionario")
        .WithOpenApi();

        group.MapPost("/", async (Funcionario funcionario, EmpresaApiContext db) =>
        {
            db.Funcionario.Add(funcionario);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Funcionario/{funcionario.Id}",funcionario);
        })
        .WithName("CreateFuncionario")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, EmpresaApiContext db) =>
        {
            var affected = await db.Funcionario
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteFuncionario")
        .WithOpenApi();
    }
}
