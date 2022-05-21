using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace Repositories.Database.SQLServer
{
    public class Livro
    {
        public static string ConnectionString { get;  set; }

        public static List<Models.Livro> get()
        {
            List<Models.Livro> livros = new List<Models.Livro>();

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
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
                            livro.Autor = dr["autor"].ToString();
                            livro.Titulo = (string)dr["titulo"];
                            livro.NumeroDePaginas = (int)dr["numeroPaginas"];

                            livros.Add(livro);
                        }

                        dr.Close();
                    }

                }
            }
            catch
            {
                throw;
            }

            return livros;
        }

        public static Models.Livro get(int id)
        {
            Models.Livro livro = null;

            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = ConnectionString;
                connection.Open();

                string sql = "select id, autor, titulo, numeroPaginas from Livro where id = @id;";

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = sql;
                    command.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.Int)).Value = id;

                    SqlDataReader dr = command.ExecuteReader();

                    if (dr.Read())
                    {
                        livro = new Models.Livro();
                        livro.Id = (int)dr["id"];
                        livro.Autor = dr["autor"].ToString();
                        livro.Titulo = (string)dr["titulo"];
                        livro.NumeroDePaginas = (int)dr["numeroPaginas"];
                    }

                    dr.Close();
                }
            }
            return livro;
        }

        public static void add(Models.Livro livro)
        {            
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string sql = "INSERT INTO Livro (titulo, autor, numeroPaginas) VALUES(@titulo, @autor, @numeroPaginas); SELECT CAST(scope_identity() AS int);";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {

                    command.Parameters.Add(new SqlParameter("@titulo", System.Data.SqlDbType.VarChar)).Value = livro.Titulo;

                    if (livro.Autor != null)
                        command.Parameters.Add(new SqlParameter("@autor", System.Data.SqlDbType.VarChar)).Value = livro.Autor;
                    else
                        command.Parameters.Add(new SqlParameter("@autor", System.Data.SqlDbType.VarChar)).Value = DBNull.Value;

                    command.Parameters.Add(new SqlParameter("@numeroPaginas", System.Data.SqlDbType.VarChar)).Value = livro.NumeroDePaginas;

                    try
                    {
                        livro.Id = (int) command.ExecuteScalar();
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
        }

        public static void update(int id, Models.Livro livro)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string sql = "Update Livro set titulo=@titulo, autor=@autor, numeroPaginas=@numeroPaginas where id = @id";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {

                    command.Parameters.Add(new SqlParameter("@titulo", System.Data.SqlDbType.VarChar)).Value = livro.Titulo;

                    if (livro.Autor != null)
                        command.Parameters.Add(new SqlParameter("@autor", System.Data.SqlDbType.VarChar)).Value = livro.Autor;
                    else
                        command.Parameters.Add(new SqlParameter("@autor", System.Data.SqlDbType.VarChar)).Value = DBNull.Value;

                    command.Parameters.Add(new SqlParameter("@numeroPaginas", System.Data.SqlDbType.VarChar)).Value = livro.NumeroDePaginas;

                    command.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.Int)).Value = id;

                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
        }

        public static void delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string sql = "delete from Livro where id = @id";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.Int)).Value = id;

                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
        }
    }
}