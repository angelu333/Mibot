import mangaRepository from './repositories/mangaRepository.js';

async function listarMangas() {
    try {
        const mangas = await mangaRepository.listarMangas();

        console.log('\n📚 Mangas disponibles:');
        console.log('------------------------');
        mangas.forEach(manga => {
            console.log(`\nID: ${manga.id}`);
            console.log(`Título: ${manga.titulo}`);
            console.log(`Autor: ${manga.autor}`);
            console.log(`Género: ${manga.genero}`);
            console.log(`Año: ${manga.anio_publicacion}`);
            console.log('------------------------');
        });
    } catch (error) {
        console.error('❌ Error inesperado:', error.message);
    }
}

listarMangas(); 