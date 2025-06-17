using Mibot.Domain.Entities;
using Supabase;
using Microsoft.Extensions.Configuration;

namespace Mibot.Data.Repositories;

public interface IMangaRepository
{
    Task<IEnumerable<Manga>> GetAllAsync();
    Task<Manga?> GetByIdAsync(int id);
    Task<Manga> AddAsync(Manga manga);
    Task<bool> UpdateAsync(Manga manga);
    Task<bool> DeleteAsync(int id);
}

public class MangaRepository : IMangaRepository
{
    private readonly Client _supabase;

    public MangaRepository(IConfiguration configuration)
    {
        var supabaseUrl = configuration["Supabase:Url"];
        var supabaseKey = configuration["Supabase:Key"];
        
        if (string.IsNullOrEmpty(supabaseUrl) || string.IsNullOrEmpty(supabaseKey))
        {
            throw new ArgumentException("La configuración de Supabase no está completa en appsettings.json");
        }

        _supabase = new Client(supabaseUrl, supabaseKey);
    }

    public async Task<IEnumerable<Manga>> GetAllAsync()
    {
        var response = await _supabase
            .From<Manga>("mangas")
            .Select("*")
            .Execute();
        return response.Models;
    }

    public async Task<Manga?> GetByIdAsync(int id)
    {
        var response = await _supabase
            .From<Manga>("mangas")
            .Select("*")
            .Where(m => m.Id == id)
            .Single();
        return response.Model;
    }

    public async Task<Manga> AddAsync(Manga manga)
    {
        var response = await _supabase
            .From<Manga>("mangas")
            .Insert(manga)
            .Execute();
        return response.Model;
    }

    public async Task<bool> UpdateAsync(Manga manga)
    {
        try
        {
            var response = await _supabase
                .From<Manga>("mangas")
                .Update(manga)
                .Where(m => m.Id == manga.Id)
                .Execute();
            return response.Model != null;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            var response = await _supabase
                .From<Manga>("mangas")
                .Delete()
                .Where(m => m.Id == id)
                .Execute();
            return response.Model != null;
        }
        catch
        {
            return false;
        }
    }
} 