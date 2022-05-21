using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Database.Entity
{
    public class Funcionario
    {
        public static string ConnectionString { get; set; }
        const string chave = "funcionarios";

        public static List<Models.Funcionario> get()
        {
            List<Models.Funcionario> funcionarios = (List<Models.Funcionario>)Repositories.Database.Cache.get(chave);

            if (funcionarios != null)
                return funcionarios;

            using (Context context = new Context(ConnectionString))
            {
                funcionarios = context.Funcionarios.ToList();
            }

            Cache.add(chave, funcionarios, 120);
            return funcionarios;
        }

        public static Models.Funcionario get(int id)
        {
            using (Context context = new Context(ConnectionString))
            {
                //context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
                return context.Funcionarios.FirstOrDefault(funcionario => funcionario.Id == id);
            }
        }
        public static void add(Models.Funcionario funcionario)
        {
            using (Context context = new Context(ConnectionString))
            {
                //context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
                context.Funcionarios.Add(funcionario);
                context.SaveChanges();
            }
            Cache.remove(chave);
        }

        public static void update(Models.Funcionario funcionario)
        {
            using (Context context = new Context(ConnectionString))
            {
                //context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
                context.Entry(funcionario).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }

            Cache.remove(chave);
        }

        public static void delete(int id)
        {
            Models.Funcionario funcionario = new Models.Funcionario();
            funcionario.Id = id;

            using (Context context = new Context(ConnectionString))
            {
                //context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
                context.Entry(funcionario).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
            }

            Cache.remove(chave);
        }
    }
}