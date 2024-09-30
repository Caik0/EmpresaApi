using Microsoft.EntityFrameworkCore;
using EmpresaApi.Data;
using EmpresaApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
namespace EmpresaApi;

public static class EnderecoEndpoints
{
    public static void MapEnderecoEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Endereco").WithTags(nameof(Endereco));

        group.MapGet("/", async (EmpresaApiContext db) =>
        {
            return await db.Endereco.ToListAsync();
        })
        .WithName("GetAllEnderecos")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Endereco>, NotFound>> (int id, EmpresaApiContext db) =>
        {
            return await db.Endereco.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is Endereco model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetEnderecoById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, Endereco endereco, EmpresaApiContext db) =>
        {
            var affected = await db.Endereco
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.Id, endereco.Id)
                    .SetProperty(m => m.Rua, endereco.Rua)
                    .SetProperty(m => m.Cidade, endereco.Cidade)
                    .SetProperty(m => m.Estado, endereco.Estado)
                    .SetProperty(m => m.Cep, endereco.Cep)
                    .SetProperty(m => m.EmpresaId, endereco.EmpresaId)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateEndereco")
        .WithOpenApi();

        group.MapPost("/", async (Endereco endereco, EmpresaApiContext db) =>
        {
            db.Endereco.Add(endereco);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Endereco/{endereco.Id}",endereco);
        })
        .WithName("CreateEndereco")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, EmpresaApiContext db) =>
        {
            var affected = await db.Endereco
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteEndereco")
        .WithOpenApi();
    }
}
