using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Repositories.Database.SQLServer
{
    public class Funcionario
    {
        public static string ConnectionString { get; set; }

        public static List<Models.Funcionario> get()
        {
            List<Models.Funcionario> funcionarios = new List<Models.Funcionario>();

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    string sql = "select id, nome, cargo,sobrenome,cargo,idade  from Funcionario;";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        SqlDataReader dr = command.ExecuteReader();

                        while (dr.Read())
                        {
                            Models.Funcionario funcionario = new Models.Funcionario();
                            funcionario.Id = (int)dr["id"];
                            funcionario.Nome = dr["nome"].ToString();
                            funcionario.Sobrenome = dr["sobrenome"].ToString();
                            funcionario.Cargo = dr["cargo"].ToString();
                            funcionario.Idade = (int)(dr["idade"]);

                            funcionarios.Add(funcionario);
                        }

                        dr.Close();
                    }

                }
            }
            catch
            {
                throw;
            }

            return funcionarios;
        }

        public static Models.Funcionario get(int id)
        {
            Models.Funcionario funcionario = null;

            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = ConnectionString;
                connection.Open();

                string sql = "select id, nome, cargo,sobrenome,cargo,idade  from Funcionario where id = @id;";

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = sql;
                    command.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.Int)).Value = id;

                    SqlDataReader dr = command.ExecuteReader();

                    if (dr.Read())
                    {
                        funcionario = new Models.Funcionario();
                        funcionario.Id = (int)dr["id"];
                        funcionario.Nome = dr["nome"].ToString();
                        funcionario.Sobrenome = dr["sobrenome"].ToString();
                        funcionario.Cargo = dr["cargo"].ToString();
                        funcionario.Idade = (int)(dr["Idade"]);
                    }

                    dr.Close();
                }
            }
            return funcionario;
        }

        public static void add(Models.Funcionario funcionario)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string sql = "INSERT INTO Funcionario (nome,sobrenome,cargo,idade)  VALUES(@nome, @sobrenome, @cargo,@idade); SELECT CAST(scope_identity() AS int);";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {

                    command.Parameters.Add(new SqlParameter("@nome", System.Data.SqlDbType.VarChar)).Value = funcionario.Nome;

                    command.Parameters.Add(new SqlParameter("@sobrenome", System.Data.SqlDbType.VarChar)).Value = funcionario.Sobrenome;

                    command.Parameters.Add(new SqlParameter("@cargo", System.Data.SqlDbType.VarChar)).Value = funcionario.Cargo;

                    command.Parameters.Add(new SqlParameter("@idade", System.Data.SqlDbType.Int)).Value = funcionario.Idade;

                    try
                    {
                        funcionario.Id = (int)command.ExecuteScalar();
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
        }

        public static void update(int id, Models.Funcionario funcionario)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string sql = "Update Funcionario set nome=@nome, sobrenome=@sobrenome, cargo=@cargo, idade =@idade where id = @id";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {

                    command.Parameters.Add(new SqlParameter("@nome", System.Data.SqlDbType.VarChar)).Value = funcionario.Nome;

                    command.Parameters.Add(new SqlParameter("@sobrenome", System.Data.SqlDbType.VarChar)).Value = funcionario.Sobrenome;

                    command.Parameters.Add(new SqlParameter("@cargo", System.Data.SqlDbType.VarChar)).Value = funcionario.Cargo;

                    command.Parameters.Add(new SqlParameter("@idade", System.Data.SqlDbType.Int)).Value = funcionario.Idade;

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

                string sql = "delete from Funcionario where id = @id";

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
