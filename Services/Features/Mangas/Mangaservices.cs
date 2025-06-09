// JaveragesLibrary/Services/Features/Mangas/MangaService.cs
using Mibot.Domain.Entities;
using System.Collections.Generic;       // Para usar List<T>
using System.Linq;                      // Para usar LINQ (FirstOrDefault, etc.)
using Supabase;

namespace Mibot.Services.Features.Mangas;

public class MangaService
{
    private readonly Client _supabase;
    private const string SUPABASE_URL = "https://nwxtzgufuxfffjlaaxps.supabase.co";
    private const string SUPABASE_KEY = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6Im53eHR6Z3VmdXhmZmZqbGFheHBzIiwicm9sZSI6ImFub24iLCJpYXQiOjE3NDkzNjAyMTAsImV4cCI6MjA2NDkzNjIxMH0.iYH2IPY-GnCMYEvcokksnxEpkltvWWDztyZVouY2pvw";

    public MangaService()
    {
        _supabase = new Client(SUPABASE_URL, SUPABASE_KEY);
    }

    // Operaciones CRUD (Create, Read, Update, Delete)

    // READ All
    public async Task<IEnumerable<Manga>> GetAll()
    {
        var response = await _supabase
            .From<Manga>("mangas")
            .Select("*")
            .Execute();
        return response.Models;
    }

    // READ by Id
    public async Task<Manga?> GetById(int id) // Devolvemos Manga? para indicar que podría no encontrarse
    {
        var response = await _supabase
            .From<Manga>("mangas")
            .Select("*")
            .Where(m => m.Id == id)
            .Single();
        return response.Model;
    }

    // CREATE
    public async Task<Manga> Add(Manga manga)
    {
        var response = await _supabase
            .From<Manga>("mangas")
            .Insert(manga)
            .Execute();
        return response.Model;
    }

    // UPDATE
    public async Task<bool> Update(Manga mangaToUpdate)
    {
        try
        {
            var response = await _supabase
                .From<Manga>("mangas")
                .Update(mangaToUpdate)
                .Where(m => m.Id == mangaToUpdate.Id)
                .Execute();
            return response.Model != null;
        }
        catch
        {
            return false;
        }
    }

    // DELETE
    public async Task<bool> Delete(int id)
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