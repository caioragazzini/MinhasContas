using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace WebApplication5.Models
{
    public class FaturaModel : IDisposable
    {
        private SqlConnection connection;

        public FaturaModel()
        {
            string strConn = WebConfigurationManager.ConnectionStrings["dbMinhasContas"].ConnectionString;
            connection = new SqlConnection(strConn);
            connection.Open();
        }
        public void Dispose()
        {
            connection.Close();
        }
        public void Create(Fatura fatura)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"INSERT INTO Fatura(EmissorId, ValorConta, DataFatura, DataVencimento, Status) VALUES (@EmissorId, @ValorConta, @DataFatura, @DataVencimento, @Status)";

            cmd.Parameters.AddWithValue("@EmissorId", fatura.EmissorId);
          
            cmd.Parameters.AddWithValue("@ValorConta", fatura.ValorConta);
            cmd.Parameters.AddWithValue("@DataFatura", fatura.DataFatura);
            cmd.Parameters.AddWithValue("@DataVencimento", fatura.DataVencimento);
            cmd.Parameters.AddWithValue("@Status", fatura.Status);

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SqlException error)
            {


            }


        }    
        public void Update(Fatura fatura)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"UPDATE Fatura SET EmissorId=@EmissorId,  ValorConta=@ValorConta, DataFatura=@DataFatura, DataVencimento=@DataVencimento, Status=@Status  WHERE FaturaId = @FaturaId";

            cmd.Parameters.AddWithValue("@EmissorId", fatura.EmissorId);
            
            cmd.Parameters.AddWithValue("@ValorConta", fatura.ValorConta);
            cmd.Parameters.AddWithValue("@DataFatura", fatura.DataFatura);
            cmd.Parameters.AddWithValue("@DataVencimento", fatura.DataVencimento);
            cmd.Parameters.AddWithValue("@Status", fatura.Status);

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SqlException error)
            {

            }
        }
        public void Delete(Fatura fatura)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"DELETE FROM Fatura WHERE FaturaId = @FaturaId";
            cmd.Parameters.AddWithValue("@FaturaId",fatura.FaturaId);            
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SqlException error)
            {

            }
        }
        public List<Fatura> Read()
        {
            List<Fatura> lista = new List<Fatura>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"SELECT f.FaturaId, e.Nome, f.ValorConta, f.DataFatura, f.DataVencimento, f.Status FROM Fatura as f inner join Emissor as e on f.EmissorId= e.EmissorId inner join Categoria as c on c.CategoriaId = e.CategoriaId";

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Fatura fatura = new Fatura();
                Emissor emissor = new Emissor(); 
                fatura.FaturaId = (int)reader[0];
                emissor.Nome = reader.GetString(reader.GetOrdinal("Nome"));
                fatura.ValorConta = (decimal)reader[2];
                fatura.DataFatura = (DateTime)reader[3];
                fatura.DataVencimento = (DateTime)reader[4];
                fatura.Status = (Boolean)reader[5];
                fatura.emissors = emissor;
                lista.Add(fatura);
            }
            return lista;
        }
        public SqlDataReader GetFatura()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"SELECT f.FaturaId,e.Nome, c.Nome, f.ValorConta, f.DataFatura, f.DataVencimento, f.Status FROM Fatura as f inner join Emissor as e on f.EmissorId= e.EmissorId inner join Categoria as c on c.CategoriaId=e.CategoriaId";
            SqlDataReader reader = cmd.ExecuteReader();

            return reader;

        }
        public Fatura ReadId(int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"SELECT * FROM Fatura where FaturaId = @FaturaId";

            SqlParameter param;
            param = cmd.Parameters.AddWithValue("@FaturaId", id);
            SqlDataReader reader = cmd.ExecuteReader();
            Fatura fatura = null;
            try
            {                
                while (reader.Read())
                {
                    fatura = new Fatura();
                    fatura.FaturaId = (int)reader[0];
                    fatura.EmissorId = (int)reader[1];
                   
                    fatura.ValorConta = (decimal)reader[2];
                    fatura.DataFatura = (DateTime)reader[3];
                    fatura.DataVencimento = (DateTime)reader[4];
                    fatura.Status = (Boolean)reader[5];                                        
                }

            }
            catch (Exception e)
            {
                throw e;
            }
            return fatura;

        }




    }
}