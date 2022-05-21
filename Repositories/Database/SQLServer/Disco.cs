using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace Repositories.Database.SQLServer
{
    public class Disco
    {
        public static string ConnectionString { get; set; }

        public static List<Models.Disco> get()
        {
            List<Models.Disco> discos = new List<Models.Disco>();

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    string sql = "select id, nomeDisco, nomeAutor, numeroDeMusicas, dataLancamento from Disco;";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        SqlDataReader dr = command.ExecuteReader();

                        while (dr.Read())
                        {
                            Models.Disco disco = new Models.Disco();
                            disco.Id = (int)dr["id"];
                            disco.NomeDisco = dr["nomeDisco"].ToString();
                            disco.NomeDisco = dr["nomeAutor"].ToString();
                            disco.NumeroDeMusicas = (int)dr["numeroDeMusicas"];
                            disco.DataLancamento = (DateTime)dr["dataLancamento"];

                            discos.Add(disco);
                        }

                        dr.Close();
                    }

                }
            }
            catch
            {
                throw;
            }

            return discos;
        }

        public static Models.Disco get(int id)
        {
            Models.Disco disco = null;

            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = ConnectionString;
                connection.Open();

                string sql = "select id, nomeDisco, nomeAutor, numeroDeMusicas,dataLancamento from Disco where id = @id;";

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = sql;
                    command.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.Int)).Value = id;

                    SqlDataReader dr = command.ExecuteReader();

                    if (dr.Read())
                    {
                        disco = new Models.Disco();
                        disco.Id = (int)dr["id"];
                        disco.NomeDisco = dr["nomeDisco"].ToString();
                        disco.NomeDisco = dr["nomeAutor"].ToString();
                        disco.NumeroDeMusicas = (int)dr["numeroDeMusicas"];
                        disco.DataLancamento = (DateTime)dr["dataLancamento"];
                    }

                    dr.Close();
                }
            }
            return disco;
        }

        public static void add(Models.Disco disco)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string sql = "INSERT INTO Disco (nomeDisco, nomeAutor,numeroDeMusicas,dataLancamento)  VALUES(@nomeDisco, @nomeAutor, @numeroDeMusicas, @dataLancamento); SELECT CAST(scope_identity() AS int);";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {

                    command.Parameters.Add(new SqlParameter("@nomeDisco", System.Data.SqlDbType.VarChar)).Value = disco.NomeDisco;

                    if (disco.NomeAutor != null)
                        command.Parameters.Add(new SqlParameter("@nomeAutor", System.Data.SqlDbType.VarChar)).Value = disco.NomeAutor;
                    else
                        command.Parameters.Add(new SqlParameter("@nomeAutor", System.Data.SqlDbType.VarChar)).Value = DBNull.Value;

                    command.Parameters.Add(new SqlParameter("@numeroDeMusicas", System.Data.SqlDbType.Int)).Value = disco.NumeroDeMusicas;

                    command.Parameters.Add(new SqlParameter("@dataLancamento", System.Data.SqlDbType.DateTime)).Value = disco.DataLancamento;

                    try
                    {
                        disco.Id = (int)command.ExecuteScalar();
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
        }

        public static void update(int id, Models.Disco disco)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string sql = "Update Disco set nomeDisco=@nomeDisco, nomeAutor=@nomeAutor, numeroDeMusicas=@numeroDeMusicas, dataLancamento=@dataLancamento where id = @id";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {

                    command.Parameters.Add(new SqlParameter("@nomeDisco", System.Data.SqlDbType.VarChar)).Value = disco.NomeDisco;

                    if (disco.NomeAutor != null)
                        command.Parameters.Add(new SqlParameter("@nomeAutor", System.Data.SqlDbType.VarChar)).Value = disco.NomeAutor;
                    else
                        command.Parameters.Add(new SqlParameter("@nomeAutor", System.Data.SqlDbType.VarChar)).Value = DBNull.Value;

                    command.Parameters.Add(new SqlParameter("@numeroDeMusicas", System.Data.SqlDbType.Int)).Value = disco.NumeroDeMusicas;

                    command.Parameters.Add(new SqlParameter("@dataLancamento", System.Data.SqlDbType.DateTime)).Value = disco.DataLancamento;

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

                string sql = "delete from Disco where id = @id";

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
