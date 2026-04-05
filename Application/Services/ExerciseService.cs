using fityou.Infrastructure.Persistence;
using fityou.Contracts.Responses;
using fityou.Contracts.Requests;
using fityou.Domains.Entities;
using Microsoft.EntityFrameworkCore;

namespace fityou.Application.Services;

public class ExerciseService
{
    private readonly AppDbContext _dbContext;

    public ExerciseService (AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ExerciseDto> CreateExerciseAsync(CreateExerciseRequest request)
    {
        //1.Map DTO to entity
        var exercise = new Exercise
        {
            Name = request.Name,
            BodayArea = request.BodayArea
        };
        //2.Add to DbContext
        _dbContext.Exercises.Add(exercise);
        await _dbContext.SaveChangesAsync();

        //3.Map entity to response DTO
        return MapToDto(exercise);
    }

    public async Task<List<ExerciseDto>> GetAllExerciseAsync()
    {
        var exercises = await _dbContext.Exercises
            .AsNoTracking()
            .ToListAsync();

        //Already fetched from db, no need to use .ToListAsync(); again
        return exercises.Select(MapToDto).ToList();
    }

    //AsNoTracking() tells EF “I will NOT modify these entities → don’t track them” (performance, used for read queries)
    public async Task<ExerciseDto> GetExerciseByIdAsync(int id)
    {
        var exercise = await _dbContext.Exercises
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id);
        
        if (exercise == null)
            return null;

        //Return a DTO, never expose an entity
        return MapToDto(exercise);
    }

    public async Task<ExerciseDto?> UpdateExerciseAsync(int id, CreateExerciseRequest request)
    {
        var exercise = await _dbContext.Exercises.FindAsync(id);
        if (exercise == null)
            return null;
        exercise.Name = request.Name;
        await _dbContext.SaveChangesAsync();

        return MapToDto(exercise);
    }


    public async Task<bool> DeleteExerciseAsync(int id)
    {
        var exercise = await _dbContext.Exercises.FindAsync(id);
        if (exercise == null)
            return false;

        _dbContext.Exercises.Remove(exercise);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    private static ExerciseDto MapToDto(Exercise exercise)
    {
        return new ExerciseDto
        {
            Id = exercise.Id,
            Name = exercise.Name,
            BodayArea = exercise.BodayArea
        };
    }
}