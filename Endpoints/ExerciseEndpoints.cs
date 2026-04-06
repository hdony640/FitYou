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
        })
        //Assign a unique name to the endpoint.
        .WithName("GetAllExercise")
        //For swagger UI
        .WithTags("Exercises");

        app.MapGet("/exercises/{id:int}", async (int id, [FromServices] ExerciseService service) =>
        {
            return Results.Ok(await service.GetExerciseByIdAsync(id));
        })
        .WithName("GetExerciseById")
        .WithTags("Exercises");

        app.MapPost("/exercises", async ([FromBody] CreateExerciseRequest request, [FromServices] ExerciseService service) =>
        {
            var result = await service.CreateExerciseAsync(request);
            return Results.Created($"/exercises/{result.Id}", result);
        })
        .WithName("CreateExercise")
        .WithTags("Exercises");

        app.MapPut("/exercices", async (int id, [FromBody] CreateExerciseRequest request, [FromServices] ExerciseService service) =>
        {
            var updated = await service.UpdateExerciseAsync(id, request);
            return updated is not null ? Results.Ok(updated) : Results.NotFound();
        })
        .WithName("UpdateExercise")
        .WithTags("Exercises");

        app.MapDelete("/exercices", async (int id, [FromServices] ExerciseService service) =>
        {
            var deleted = await service.DeleteExerciseAsync(id);
            return deleted is true ? Results.Ok(deleted) : Results.NotFound();
        })
        .WithName("DeleteExercise")
        .WithTags("Exercises");
    }
}