import os
from datetime import datetime
from faker import Faker
from supabase import create_client
import random

# Configuración de Faker
fake = Faker('es_ES')

# Configuración de Supabase
SUPABASE_URL = "https://nwxtzgufuxfffjlaaxps.supabase.co"
SUPABASE_KEY = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6Im53eHR6Z3VmdXhmZmZqbGFheHBzIiwicm9sZSI6ImFub24iLCJpYXQiOjE3NDkzNjAyMTAsImV4cCI6MjA2NDkzNjIxMH0.iYH2IPY-GnCMYEvcokksnxEpkltvWWDztyZVouY2pvw"

# Lista de géneros comunes de manga
GENEROS = [
    "Acción", "Aventura", "Comedia", "Drama", "Fantasía", "Horror",
    "Misterio", "Romance", "Ciencia Ficción", "Slice of Life", "Deportes",
    "Psicológico", "Sobrenatural", "Mecha", "Histórico", "Musical",
    "Magia", "Escolar", "Shoujo", "Shounen", "Seinen", "Josei"
]

def generar_manga():
    titulo = fake.catch_phrase()
    autor = fake.name()
    genero = random.choice(GENEROS)
    sinopsis = fake.text(max_nb_chars=200)
    anio_publicacion = random.randint(1980, 2024)
    imagen_url = f"https://picsum.photos/seed/{fake.uuid4()}/300/400"
    return {
        "titulo": titulo,
        "autor": autor,
        "genero": genero,
        "sinopsis": sinopsis,
        "anio_publicacion": anio_publicacion,
        "imagen_url": imagen_url
    }

def main():
    print("=== Generador de Mangas para Supabase (estructura personalizada) ===")
    try:
        supabase = create_client(SUPABASE_URL, SUPABASE_KEY)
        print("Conexión a Supabase establecida correctamente")
    except Exception as e:
        print(f"Error al conectar con Supabase: {e}")
        return

    total_mangas = 1750
    mangas_generados = set()
    print(f"\nGenerando {total_mangas} mangas únicos...")

    for i in range(total_mangas):
        while True:
            manga = generar_manga()
            manga_key = f"{manga['titulo']}_{manga['autor']}_{manga['anio_publicacion']}"
            if manga_key not in mangas_generados:
                mangas_generados.add(manga_key)
                try:
                    supabase.table('mangas').insert(manga).execute()
                    print(f"Manga {i+1}/{total_mangas} guardado.")
                except Exception as e:
                    print(f"Error al guardar manga {i+1}: {e}")
                break
        if (i + 1) % 100 == 0:
            print(f"\nProgreso: {i+1}/{total_mangas} mangas generados")
    print("\n¡Proceso completado!")
    print(f"Total de mangas generados: {len(mangas_generados)}")

if __name__ == "__main__":
    main() 