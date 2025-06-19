// Mibot/Domain/Entities/Manga.cs
namespace Mibot.Domain.Entities;
using Postgrest.Attributes;
using Postgrest.Models;

[Table("mangas")]
public class Manga : BaseModel
{
    public int Id { get; set; } // Identificador único
    public string Title { get; set; } = string.Empty; // Título del manga (C# 11 'required')
    public string Author { get; set; } = string.Empty; // Autor del manga (C# 11 'required')
    public string Genre { get; set; } = string.Empty; // Género (opcional, por eso el '?')
    public DateTime PublicationDate { get; set; } // Fecha de publicación
    public int Volumes { get; set; } // Número de volúmenes
    public bool IsOngoing { get; set; } // ¿Sigue en publicación?

    // Constructor (opcional, pero útil para inicializar)
    public Manga()
    {
        Title = string.Empty; // Inicialización para 'required' si no se usa constructor con params
        Author = string.Empty;
        PublicationDate = DateTime.MinValue; // O un valor por defecto más sensato
    }
}