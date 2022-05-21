using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Repositories.Database.Entity
{
   public class Context: DbContext
   {
        public DbSet<Models.Livro> Livros { get; set; }
        public DbSet<Models.Funcionario> Funcionarios { get; set; }
        public DbSet<Models.Disco> Discos { get; set; }

        public Context(string connectionString)
             :base(connectionString)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
