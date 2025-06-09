from supabase import create_client
import json

# Configuración de Supabase
SUPABASE_URL = "https://nwxtzgufuxfffjlaaxps.supabase.co"
SUPABASE_KEY = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6Im53eHR6Z3VmdXhmZmZqbGFheHBzIiwicm9sZSI6ImFub24iLCJpYXQiOjE3NDkzNjAyMTAsImV4cCI6MjA2NDkzNjIxMH0.iYH2IPY-GnCMYEvcokksnxEpkltvWWDztyZVouY2pvw"

def consultar_mangas():
    try:
        print("Intentando conectar a Supabase...")
        # Crear cliente de Supabase
        supabase = create_client(SUPABASE_URL, SUPABASE_KEY)
        print("Conexión exitosa a Supabase")
        
        print("\nConsultando la tabla 'mangas'...")
        # Consultar todos los mangas
        response = supabase.table('mangas').select("*").execute()
        
        # Mostrar resultados
        mangas = response.data
        print(f"\nTotal de mangas encontrados: {len(mangas)}")
        
        if len(mangas) == 0:
            print("\n⚠️ ADVERTENCIA: No se encontraron mangas en la base de datos.")
            print("\nVerificando la estructura de la tabla...")
            # Intentar obtener la estructura de la tabla
            try:
                table_info = supabase.table('mangas').select("count").execute()
                print("La tabla 'mangas' existe pero está vacía.")
            except Exception as e:
                print(f"Error al verificar la tabla: {e}")
        else:
            print("\nPrimeros 5 mangas:")
            for i, manga in enumerate(mangas[:5], 1):
                print(f"\nManga {i}:")
                print(f"Título: {manga['titulo']}")
                print(f"Autor: {manga['autor']}")
                print(f"Género: {manga['genero']}")
                print(f"Año: {manga['anio_publicacion']}")
                print("-" * 50)
            
    except Exception as e:
        print(f"\n❌ Error al consultar la base de datos: {e}")
        print("\nPor favor, verifica:")
        print("1. Que la URL y la KEY de Supabase sean correctas")
        print("2. Que la tabla 'mangas' exista en tu proyecto")
        print("3. Que tengas los permisos necesarios para acceder a la tabla")

if __name__ == "__main__":
    consultar_mangas() 