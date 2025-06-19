// JaveragesLibrary/Services/Features/Mangas/MangaService.cs
using Mibot.Domain.Entities;
using Mibot.Data.Repositories;
using System.Collections.Generic;       // Para usar List<T>
using System.Linq;                      // Para usar LINQ (FirstOrDefault, etc.)
using Supabase;
<<<<<<< HEAD
using Postgrest;
=======
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
>>>>>>> 9dfa43d2119b54cfae1c72483f6f08ccfa5ddf8a

namespace Mibot.Services.Features.Mangas;

public class MangaService
{
<<<<<<< HEAD
    private readonly Supabase.Client _supabase;
    private const string SUPABASE_URL = "https://nwxtzgufuxfffjlaaxps.supabase.co";
    private const string SUPABASE_KEY = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6Im53eHR6Z3VmdXhmZmZqbGFheHBzIiwicm9sZSI6ImFub24iLCJpYXQiOjE3NDkzNjAyMTAsImV4cCI6MjA2NDkzNjIxMH0.iYH2IPY-GnCMYEvcokksnxEpkltvWWDztyZVouY2pvw";
=======
    private readonly IMangaRepository _mangaRepository;
>>>>>>> 9dfa43d2119b54cfae1c72483f6f08ccfa5ddf8a

    public MangaService(IMangaRepository mangaRepository)
    {
<<<<<<< HEAD
        _supabase = new Supabase.Client(SUPABASE_URL, SUPABASE_KEY);
=======
        _mangaRepository = mangaRepository;
>>>>>>> 9dfa43d2119b54cfae1c72483f6f08ccfa5ddf8a
    }

    // Operaciones CRUD (Create, Read, Update, Delete)

    // READ All
    public async Task<IEnumerable<Manga>> GetAll()
    {
<<<<<<< HEAD
        var response = await _supabase
            .From<Manga>()
            .Get();
        return response.Models;
=======
        return await _mangaRepository.GetAllAsync();
>>>>>>> 9dfa43d2119b54cfae1c72483f6f08ccfa5ddf8a
    }

    // READ by Id
    public async Task<Manga?> GetById(int id) // Devolvemos Manga? para indicar que podría no encontrarse
    {
<<<<<<< HEAD
        var response = await _supabase
            .From<Manga>()
            .Where(x => x.Id == id)
            .Get();
        return response.Models.FirstOrDefault();
=======
        return await _mangaRepository.GetByIdAsync(id);
>>>>>>> 9dfa43d2119b54cfae1c72483f6f08ccfa5ddf8a
    }

    // CREATE
    public async Task<Manga> Add(Manga manga)
    {
<<<<<<< HEAD
        var response = await _supabase
            .From<Manga>()
            .Insert(manga);
        return response.Models.First();
=======
        return await _mangaRepository.AddAsync(manga);
>>>>>>> 9dfa43d2119b54cfae1c72483f6f08ccfa5ddf8a
    }

    // UPDATE
    public async Task<bool> Update(Manga manga)
    {
<<<<<<< HEAD
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
=======
        return await _mangaRepository.UpdateAsync(manga);
>>>>>>> 9dfa43d2119b54cfae1c72483f6f08ccfa5ddf8a
    }

    // DELETE
    public async Task<bool> Delete(int id)
    {
<<<<<<< HEAD
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
=======
        return await _mangaRepository.DeleteAsync(id);
>>>>>>> 9dfa43d2119b54cfae1c72483f6f08ccfa5ddf8a
    }
}