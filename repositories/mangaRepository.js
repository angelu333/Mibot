import { createClient } from '@supabase/supabase-js';
import supabaseConfig from '../config/supabase.js';

const supabase = createClient(supabaseConfig.url, supabaseConfig.key);

class MangaRepository {
    async listarMangas(limit = 10) {
        try {
            const { data: mangas, error } = await supabase
                .from('mangas')
                .select('*')
                .limit(limit);

            if (error) throw error;
            return mangas;
        } catch (error) {
            throw new Error(`Error al obtener mangas: ${error.message}`);
        }
    }
}

export default new MangaRepository(); 