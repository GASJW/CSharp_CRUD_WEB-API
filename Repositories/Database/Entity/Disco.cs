using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Database.Entity
{
    public class Disco
    {
        public static string ConnectionString { get; set; }
        const string chave = "discos";

        public static List<Models.Disco> get()
        {
            List<Models.Disco> discos = (List<Models.Disco>)Repositories.Database.Cache.get(chave);

            if (discos != null)
                return discos;

            using (Context context = new Context(ConnectionString))
            {
                discos = context.Discos.ToList();
            }

            Cache.add(chave, discos, 120);
            return discos;
        }

        public static Models.Disco get(int id)
        {
            using (Context context = new Context(ConnectionString))
            {
                //context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
                return context.Discos.FirstOrDefault(disco => disco.Id == id);
            }
        }
        public static void add(Models.Disco disco)
        {
            using (Context context = new Context(ConnectionString))
            {
                //context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
                context.Discos.Add(disco);
                context.SaveChanges();
            }
            Cache.remove(chave);
        }

        public static void update(Models.Disco disco)
        {
            using (Context context = new Context(ConnectionString))
            {
                //context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
                context.Entry(disco).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }

            Cache.remove(chave);
        }

        public static void delete(int id)
        {
            Models.Disco disco = new Models.Disco();
            disco.Id = id;

            using (Context context = new Context(ConnectionString))
            {
                //context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
                context.Entry(disco).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
            }

            Cache.remove(chave);
        }
    }
}