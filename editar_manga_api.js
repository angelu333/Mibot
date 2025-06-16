import fetch from 'node-fetch';

async function editarManga(id, nuevosDatos) {
    try {
        // URL de tu API local
        const url = `http://localhost:5000/api/Manga/${id}`;

        // Realizar la petición PUT
        const response = await fetch(url, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                id: id,
                ...nuevosDatos
            })
        });

        if (response.status === 204) {
            console.log('✅ Manga actualizado exitosamente');
            
            // Obtener el manga actualizado para mostrar los cambios
            const getResponse = await fetch(`http://localhost:5000/api/Manga/${id}`);
            const mangaActualizado = await getResponse.json();
            
            console.log('\nDatos actualizados del manga:');
            console.log('------------------------');
            console.log(`ID: ${mangaActualizado.id}`);
            console.log(`Título: ${mangaActualizado.titulo}`);
            console.log(`Autor: ${mangaActualizado.autor}`);
            console.log(`Género: ${mangaActualizado.genero}`);
            console.log(`Año: ${mangaActualizado.anio_publicacion}`);
            console.log(`Sinopsis: ${mangaActualizado.sinopsis}`);
            console.log('------------------------');
        } else if (response.status === 404) {
            console.error('❌ Manga no encontrado');
        } else if (response.status === 400) {
            console.error('❌ Error en la solicitud');
        } else {
            console.error('❌ Error inesperado:', response.status);
        }
    } catch (error) {
        console.error('❌ Error al conectar con la API:', error.message);
    }
}

// Ejemplo de uso: editar el manga con ID 10502
const nuevosDatos = {
    titulo: "Dragon Ball Super",
    autor: "Akira Toriyama",
    genero: "Acción",
    sinopsis: "La continuación de Dragon Ball Z, siguiendo las aventuras de Goku y sus amigos después de la batalla contra Majin Buu.",
    anio_publicacion: 2015,
    imagen_url: "https://ejemplo.com/dragon-ball-super.jpg"
};

// Reemplaza el número 10502 con el ID del manga que quieras editar
editarManga(10502, nuevosDatos); 