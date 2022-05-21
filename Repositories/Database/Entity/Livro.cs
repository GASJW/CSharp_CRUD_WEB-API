using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Database.Entity
{
    public class Livro
    {
        public static string ConnectionString { get; set; }
        const string chave = "livros";

        public static List<Models.Livro> get()
        {
            List<Models.Livro> livros = (List<Models.Livro>) Repositories.Database.Cache.get(chave);

            if (livros != null)
                return livros;

            using (Context context = new Context(ConnectionString))
            {
                livros = context.Livros.ToList();                
            }

            Cache.add(chave, livros, 120);
            return livros;
        }

        public static Models.Livro get(int id)
        {
            using (Context context = new Context(ConnectionString))
            {
                //context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
                return context.Livros.FirstOrDefault(livro => livro.Id == id);
            }
        }
        public static void add(Models.Livro livro)
        {
            using (Context context = new Context(ConnectionString))
            {
                //context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
                context.Livros.Add(livro);
                context.SaveChanges();
            }
            Cache.remove(chave);
        }

        public static void update(Models.Livro livro)
        {
            using (Context context = new Context(ConnectionString))
            {
                //context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
                context.Entry(livro).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }

            Cache.remove(chave);
        }

        public static void delete(int id)
        {
            Models.Livro livro = new Models.Livro();
            livro.Id = id;

            using (Context context = new Context(ConnectionString))
            {
                //context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
                context.Entry(livro).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
            }

            Cache.remove(chave);
        }
    }
}