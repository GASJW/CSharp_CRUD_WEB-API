using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api_bib.Repositories.Memory
{
    public class Livro
    {
        static List<Models.Livro> livros = new List<Models.Livro>();
        static int contador = 0;

        public static List<Models.Livro> get()
        {
            return livros;
        }

        public static Models.Livro get(int id)
        {
            return livros.FirstOrDefault(livro => livro.Id == id);
        }

        public static void add(Models.Livro livro)
        {
            livro.Id = ++contador;
            livros.Add(livro);
        }

        public static void update(int id, Models.Livro livro)
        {
            Models.Livro livroASerAlterado = get(id);

            if (livroASerAlterado != null)
            {
                livroASerAlterado.Copiar(livro);
            }
        }

        public static void delete(int id)
        {
            livros.Remove(get(id));
            //livros.RemoveAll(livro => livro.Id == id);
        }
    }
}