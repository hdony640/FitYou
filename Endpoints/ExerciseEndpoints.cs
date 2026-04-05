using fityou.Application.Services;
using fityou.Contracts.Requests;
using Microsoft.AspNetCore.Mvc;


namespace fityou.Endpoints;

public static class ExerciseEndpoints
{
    public static void MapExerciseEndpoints(this WebApplication app)
    {
        app.MapGet("/exercises", async ([FromServices] ExerciseService service) =>
        {
            return Results.Ok(await service.GetAllExerciseAsync());
        });

        app.MapPost("/exercises", async ([FromBody] CreateExerciseRequest request, [FromServices] ExerciseService service) =>
        {
            var result = await service.CreateExerciseAsync(request);
            return Results.Created($"/exercises/{result.Id}", result);
        });
    }
}