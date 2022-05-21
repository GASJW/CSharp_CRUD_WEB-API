using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace api_bib.Repositories.Database.SQLServer
{
    public class Livro
    {
        public static List<Models.Livro> get()
        {
            List<Models.Livro> livros = new List<Models.Livro>();

            using (SqlConnection connection = new SqlConnection(Configuration.Database.SQLServer.Parameters.getConnectionString()))
            {
                connection.Open();

                string sql = "select id, autor, titulo, numeroPaginas  from Livro;";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    SqlDataReader dr = command.ExecuteReader();                  

                    while (dr.Read())
                    {
                        Models.Livro livro = new Models.Livro();
                        livro.Id = (int)dr["id"];
                        livro.Autor = (string)dr["autor"];
                        livro.Titulo = (string)dr["titulo"];
                        livro.NumeroDePaginas = (int)dr["numeroPaginas"];

                        livros.Add(livro);
                    }
                }
            }
            return livros;
        }

        public static Models.Livro get(int id)
        {
            return null;
        }

        public static void add(Models.Livro livro)
        {
            
        }

        public static void update(int id, Models.Livro livro)
        {
            
        }

        public static void delete(int id)
        {
            
        }
    }
}