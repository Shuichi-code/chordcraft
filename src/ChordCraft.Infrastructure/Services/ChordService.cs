using ChordCraft.Core.Entities;
using ChordCraft.Core.Interfaces;
using ChordCraft.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ChordCraft.Infrastructure.Services;

public class ChordService : IChordService
{
    private readonly AppDbContext _db;
    public ChordService(AppDbContext db) => _db = db;

    public async Task<List<ChordEntry>> GetChordsAsync(int? difficulty = null, string? category = null)
    {
        var query = _db.ChordEntries.AsQueryable();
        if (difficulty.HasValue) query = query.Where(c => c.Difficulty == difficulty.Value);
        if (!string.IsNullOrEmpty(category)) query = query.Where(c => c.Category == category);
        return await query.OrderBy(c => c.Difficulty).ThenBy(c => c.Id).ToListAsync();
    }
}
