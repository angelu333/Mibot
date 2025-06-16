const { createClient } = require('@supabase/supabase-js');

const supabaseUrl = 'https://nwxtzgufuxfffjlaaxps.supabase.co';
const supabaseKey = 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6Im53eHR6Z3VmdXhmZmZqbGFheHBzIiwicm9sZSI6ImFub24iLCJpYXQiOjE3NDkzNjAyMTAsImV4cCI6MjA2NDkzNjIxMH0.iYH2IPY-GnCMYEvcokksnxEpkltvWWDztyZVouY2pvw';

const supabase = createClient(supabaseUrl, supabaseKey);

async function editarManga(id, nuevosDatos) {
    try {
        // Primero obtenemos el manga actual para ver sus datos
        const { data: mangaActual, error: errorLectura } = await supabase
            .from('mangas')
            .select('*')
            .eq('id', id)
            .single();

        if (errorLectura) {
            console.error('❌ Error al obtener el manga:', errorLectura.message);
            return;
        }

        console.log('\n📚 Datos actuales del manga:');
        console.log('------------------------');
        console.log(`ID: ${mangaActual.id}`);
        console.log(`Título: ${mangaActual.titulo}`);
        console.log(`Autor: ${mangaActual.autor}`);
        console.log(`Género: ${mangaActual.genero}`);
        console.log(`Año: ${mangaActual.anio_publicacion}`);
        console.log(`Sinopsis: ${mangaActual.sinopsis}`);
        console.log('------------------------');

        // Actualizamos el manga con los nuevos datos
        const { data: mangaActualizado, error: errorActualizacion } = await supabase
            .from('mangas')
            .update(nuevosDatos)
            .eq('id', id)
            .select();

        if (errorActualizacion) {
            console.error('❌ Error al actualizar el manga:', errorActualizacion.message);
            return;
        }

        console.log('\n✅ Manga actualizado exitosamente:');
        console.log('------------------------');
        console.log(`ID: ${mangaActualizado[0].id}`);
        console.log(`Título: ${mangaActualizado[0].titulo}`);
        console.log(`Autor: ${mangaActualizado[0].autor}`);
        console.log(`Género: ${mangaActualizado[0].genero}`);
        console.log(`Año: ${mangaActualizado[0].anio_publicacion}`);
        console.log(`Sinopsis: ${mangaActualizado[0].sinopsis}`);
        console.log('------------------------');
    } catch (error) {
        console.error('❌ Error inesperado:', error.message);
    }
}

// Datos actualizados para el manga con ID 10502
const nuevosDatos = {
    titulo: "Dragon Ball Super",
    autor: "Akira Toriyama",
    genero: "Acción",
    sinopsis: "La continuación de Dragon Ball Z, siguiendo las aventuras de Goku y sus amigos después de la batalla contra Majin Buu.",
    anio_publicacion: 2015,
    imagen_url: "https://ejemplo.com/dragon-ball-super.jpg"
};

// Editamos el manga con ID 10502
editarManga(10502, nuevosDatos); 