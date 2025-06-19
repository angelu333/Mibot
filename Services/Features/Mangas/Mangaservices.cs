// JaveragesLibrary/Services/Features/Mangas/MangaService.cs
using Mibot.Domain.Entities;
using System.Collections.Generic;       // Para usar List<T>
using System.Linq;                      // Para usar LINQ (FirstOrDefault, etc.)
using Supabase;
using Postgrest;

namespace Mibot.Services.Features.Mangas;

public class MangaService
{
    private readonly Supabase.Client _supabase;
    private const string SUPABASE_URL = "https://nwxtzgufuxfffjlaaxps.supabase.co";
    private const string SUPABASE_KEY = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6Im53eHR6Z3VmdXhmZmZqbGFheHBzIiwicm9sZSI6ImFub24iLCJpYXQiOjE3NDkzNjAyMTAsImV4cCI6MjA2NDkzNjIxMH0.iYH2IPY-GnCMYEvcokksnxEpkltvWWDztyZVouY2pvw";

    public MangaService()
    {
        _supabase = new Supabase.Client(SUPABASE_URL, SUPABASE_KEY);
    }

    // Operaciones CRUD (Create, Read, Update, Delete)

    // READ All
    public async Task<IEnumerable<Manga>> GetAll()
    {
        var response = await _supabase
            .From<Manga>()
            .Get();
        return response.Models;
    }

    // READ by Id
    public async Task<Manga?> GetById(int id) // Devolvemos Manga? para indicar que podría no encontrarse
    {
        var response = await _supabase
            .From<Manga>()
            .Where(x => x.Id == id)
            .Get();
        return response.Models.FirstOrDefault();
    }

    // CREATE
    public async Task<Manga> Add(Manga manga)
    {
        var response = await _supabase
            .From<Manga>()
            .Insert(manga);
        return response.Models.First();
    }

    // UPDATE
    public async Task<bool> Update(Manga mangaToUpdate)
    {
        try
        {
            var response = await _supabase
                .From<Manga>()
                .Where(x => x.Id == mangaToUpdate.Id)
                .Update(mangaToUpdate);
            return response.Models.Any();
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
            await _supabase
                .From<Manga>()
                .Where(x => x.Id == id)
                .Delete();
            return true;
        }
        catch
        {
            return false;
        }
    }
}