using Microsoft.EntityFrameworkCore;
using EmpresaApi.Data;
using EmpresaApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
namespace EmpresaApi;

public static class EmpresaEndpoints
{
    public static void MapEmpresaEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Empresa").WithTags(nameof(Empresa));

        group.MapGet("/", async (EmpresaApiContext db) =>
        {
            return await db.Empresa
                .Include(e => e.Enderecos)
                .Include(e => e.Funcionarios)
                .ToListAsync();
        })
        .WithName("GetAllEmpresas")
        .WithOpenApi();



        group.MapGet("/{id}", async Task<Results<Ok<Empresa>, NotFound>> (int id, EmpresaApiContext db) =>
        {
            var empresa = await db.Empresa
                .Include(e => e.Enderecos)
                .Include(e => e.Funcionarios)
                .AsNoTracking()
                .FirstOrDefaultAsync(model => model.ID == id);

            return empresa is Empresa model
                ? TypedResults.Ok(model)
                : TypedResults.NotFound();
        })
        .WithName("GetEmpresaById")
        .WithOpenApi();


        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, Empresa empresa, EmpresaApiContext db) =>
        {
            var affected = await db.Empresa
                .Where(model => model.ID == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.ID, empresa.ID)
                    .SetProperty(m => m.Nome, empresa.Nome)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateEmpresa")
        .WithOpenApi();

        group.MapPost("/", async (Empresa empresa, EmpresaApiContext db) =>
        {
            db.Empresa.Add(empresa);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Empresa/{empresa.ID}",empresa);
        })
        .WithName("CreateEmpresa")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, EmpresaApiContext db) =>
        {
            var affected = await db.Empresa
                .Where(model => model.ID == id)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteEmpresa")
        .WithOpenApi();
    }
}
