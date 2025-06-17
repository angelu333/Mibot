// JaveragesLibrary/Services/Features/Mangas/MangaService.cs
using Mibot.Domain.Entities;
using Mibot.Data.Repositories;
using System.Collections.Generic;       // Para usar List<T>
using System.Linq;                      // Para usar LINQ (FirstOrDefault, etc.)
using Supabase;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Mibot.Services.Features.Mangas;

public class MangaService
{
    private readonly IMangaRepository _mangaRepository;

    public MangaService(IMangaRepository mangaRepository)
    {
        _mangaRepository = mangaRepository;
    }

    // Operaciones CRUD (Create, Read, Update, Delete)

    // READ All
    public async Task<IEnumerable<Manga>> GetAll()
    {
        return await _mangaRepository.GetAllAsync();
    }

    // READ by Id
    public async Task<Manga?> GetById(int id) // Devolvemos Manga? para indicar que podría no encontrarse
    {
        return await _mangaRepository.GetByIdAsync(id);
    }

    // CREATE
    public async Task<Manga> Add(Manga manga)
    {
        return await _mangaRepository.AddAsync(manga);
    }

    // UPDATE
    public async Task<bool> Update(Manga manga)
    {
        return await _mangaRepository.UpdateAsync(manga);
    }

    // DELETE
    public async Task<bool> Delete(int id)
    {
        return await _mangaRepository.DeleteAsync(id);
    }
}