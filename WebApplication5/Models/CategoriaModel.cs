using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace WebApplication5.Models
{
    public class CategoriaModel : IDisposable
    {
        private SqlConnection connection;

        public CategoriaModel()
        {
            string strConn = WebConfigurationManager.ConnectionStrings["dbMinhasContas"].ConnectionString;
            connection = new SqlConnection(strConn);
            connection.Open();

        }
        public void Dispose()
        {
            connection.Close();
        }

        public void Create(Categoria categoria)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"INSERT INTO Categoria(Nome) VALUES (@Nome)";
            cmd.Parameters.AddWithValue("@Nome", categoria.Nome);

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SqlException error)
            {

            }
        }

        public void Update(Categoria categoria)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"UPDATE Categoria SET Nome = @Nome WHERE CategoriaId = @CategoriaId";
            cmd.Parameters.AddWithValue("@Nome", categoria.Nome);

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SqlException error)
            {


            }
        }

        public void Delete(Categoria categoria)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"DELETE FROM Categoria WHERE CategoriaId = @CategoriaId";
            cmd.Parameters.AddWithValue("@EmissorId", categoria.CategoriaId);

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SqlException error)
            {


            }
        }

        public List<Categoria> Read()
        {
            List<Categoria> lista = new List<Categoria>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"SELECT * FROM Categoria";
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Categoria categoria = new Categoria();
                categoria.CategoriaId = (int)reader[0];
                categoria.Nome = (string)reader[1];
                lista.Add(categoria);
            }

            return lista;
        }

        public SqlDataReader GetCategoria()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"SELECT Nome FROM Categoria";
            SqlDataReader reader = cmd.ExecuteReader();

            return reader;
        }
        public Categoria ReadId(int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"SELECT * FROM Categoria where EmissorId = @CategoriaId";

            SqlParameter param;
            param = cmd.Parameters.AddWithValue("@CategoriaId", id);
            SqlDataReader reader = cmd.ExecuteReader();
            Categoria categoria = null;
            try
            {
                while (reader.Read())
                {
                    categoria = new Categoria();
                    categoria.CategoriaId = (int)reader[0];
                    categoria.Nome = (string)reader[1];
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return categoria;

        }

    }
}