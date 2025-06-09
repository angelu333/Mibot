const { createClient } = require('@supabase/supabase-js');
const { faker } = require('@faker-js/faker');

const supabaseUrl = 'https://nwxtzgufuxfffjlaaxps.supabase.co';
const supabaseKey = 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6Im53eHR6Z3VmdXhmZmZqbGFheHBzIiwicm9sZSI6ImFub24iLCJpYXQiOjE3NDkzNjAyMTAsImV4cCI6MjA2NDkzNjIxMH0.iYH2IPY-GnCMYEvcokksnxEpkltvWWDztyZVouY2pvw';

const supabase = createClient(supabaseUrl, supabaseKey);

async function insertarMangas(cantidad) {
  const mangas = [];

  for (let i = 0; i < cantidad; i++) {
    mangas.push({
      titulo: faker.lorem.words(3),
      autor: faker.person.fullName(),
      genero: faker.music.genre(),
      sinopsis: faker.lorem.paragraph(),
      anio_publicacion: faker.date.between({ from: '1980-01-01', to: '2024-12-31' }).getFullYear(),
      imagen_url: faker.image.urlLoremFlickr({ category: 'anime' })
    });
  }

  // Primero, verificar la conexión
  const { data: testData, error: testError } = await supabase.from('mangas').select('count').limit(1);
  
  if (testError) {
    console.error('❌ Error al conectar con la tabla mangas:', testError);
    return;
  }

  console.log('✅ Conexión exitosa con la tabla mangas');

  for (let i = 0; i < mangas.length; i += 100) {
    const chunk = mangas.slice(i, i + 100);
    const { data, error } = await supabase.from('mangas').insert(chunk);

    if (error) {
      console.error(`❌ Error al insertar desde índice ${i}:`, error.message);
    } else {
      console.log(`✅ Insertados ${chunk.length} mangas (bloque desde ${i})`);
    }
  }
}

insertarMangas(1750);
