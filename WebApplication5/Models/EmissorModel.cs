using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace WebApplication5.Models
{
    public class EmissorModel : IDisposable
    {

        private SqlConnection connection;
        public EmissorModel()
        {
            string strConn = WebConfigurationManager.ConnectionStrings["dbMinhasContas"].ConnectionString;
            connection = new SqlConnection(strConn);
            connection.Open();
        }

        public void Dispose()
        {
            connection.Close();
        }

        public void Create(Emissor emissor)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"INSERT INTO Emissor(Nome) VALUES (@Nome)";
            cmd.Parameters.AddWithValue("@Nome", emissor.Nome);
            
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SqlException error)
            {

            }
        }    

        public void Update(Emissor emissor)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"UPDATE Emissor SET Nome = @Nome WHERE EmissorId = @EmissorId";
            cmd.Parameters.AddWithValue("@Nome", emissor.Nome);

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SqlException error)
            {


            }
        }

        public void Delete(Emissor emissor)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"DELETE FROM Emissor WHERE EmissorId = @EmissorId";
            cmd.Parameters.AddWithValue("@EmissorId",emissor.EmissorId);

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SqlException error)
            {


            }
        }


        public List<Emissor> Read()
        {
            List<Emissor> lista = new List<Emissor>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"SELECT * FROM Emissor";
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Emissor emissor = new Emissor();
                emissor.EmissorId = (int)reader[0];
                emissor.Nome = (string)reader[1];
                lista.Add(emissor);
            }

            return lista;
        }

        public SqlDataReader GetEmissor()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"SELECT Nome FROM Emissor";
            SqlDataReader reader = cmd.ExecuteReader();

            return reader;
        }
        public Emissor ReadId(int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"SELECT * FROM Emissor where EmissorId = @EmissorId";

            SqlParameter param;
            param = cmd.Parameters.AddWithValue("@EmissorId", id);
            SqlDataReader reader = cmd.ExecuteReader();
            Emissor emissor = null;
            try
            {
                while (reader.Read())
                {
                    emissor = new Emissor();
                    emissor.EmissorId = (int)reader[0];
                    emissor.Nome = (string)reader[1];                    
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return emissor;

        }







    }
}