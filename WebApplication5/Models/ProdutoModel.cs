using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace WebApplication5.Models
{
    public class ProdutoModel : IDisposable
    {

        private SqlConnection connection;

        public ProdutoModel()
        {
            string strConn = WebConfigurationManager.ConnectionStrings["DbLojaVirtual"].ConnectionString;

            connection = new SqlConnection(strConn);
            connection.Open();

        }

        public void Dispose()
        {
            connection.Close();
        }

        public void Create(clsProdutos produto)
        {

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"INSERT INTO tbProdutos(NomeProduto,ValorProduto,QtdeProduto) VALUES (@NomeProduto,@ValorProduto,@QtdeProduto)";

            cmd.Parameters.AddWithValue("@NomeProduto", produto.NomeProduto);
            cmd.Parameters.AddWithValue("@ValorProduto", produto.ValorProduto);
            cmd.Parameters.AddWithValue("@QtdeProduto", produto.QtdeProduto);

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SqlException error)
            {


            }


        }

        public void CreateProc(clsProdutos produto)
        {

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = ("sp_InsertProdutos");
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@NomeProduto", produto.NomeProduto);
            cmd.Parameters.AddWithValue("@ValorProduto", produto.ValorProduto);
            cmd.Parameters.AddWithValue("@QtdeProduto", produto.QtdeProduto);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SqlException error)
            {


            }


        }



        public void Update(clsProdutos produto)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"UPDATE tbProdutos SET NomeProduto=@NomeProduto,ValorProduto=@ValorProduto,QtdeProduto=@QtdeProduto WHERE IdProduto=@IdProduto";

            cmd.Parameters.AddWithValue("@NomeProduto", produto.NomeProduto);
            cmd.Parameters.AddWithValue("@ValorProduto", produto.ValorProduto);
            cmd.Parameters.AddWithValue("@QtdeProduto", produto.QtdeProduto);
            cmd.Parameters.AddWithValue("@IdProduto", produto.IdProduto);

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SqlException error)
            {


            }
        }

        public void Delete(clsProdutos produto)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"DELETE FROM tbProdutos WHERE IdProduto=@IdProduto";

            cmd.Parameters.AddWithValue("@IdProduto", produto.IdProduto);

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SqlException error)
            {


            }
        }


        public List<clsProdutos> Read()
        {
            List<clsProdutos> lista = new List<clsProdutos>();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"SELECT * FROM tbProdutos";

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {

                clsProdutos produto = new clsProdutos();
                produto.IdProduto = (int)reader[0];
                produto.NomeProduto = (string)reader[1];
                produto.ValorProduto = (decimal)reader[2];
                produto.QtdeProduto = (int)reader[3];


                lista.Add(produto);
            }

            return lista;


        }

        public SqlDataReader GetProdutos()
        {


            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"SELECT NomeProduto,ValorProduto FROM tbProdutos";

            SqlDataReader reader = cmd.ExecuteReader();

            return reader;




        }
        public clsProdutos ReadId(int id)
        {

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"SELECT * FROM tbProdutos where IdProduto=@IdProduto";

            SqlParameter param;
            param = cmd.Parameters.AddWithValue("@IdProduto", id);
            SqlDataReader reader = cmd.ExecuteReader();
            clsProdutos produto = null;
            try
            {

                while (reader.Read())
                {
                    produto = new clsProdutos();
                    produto.IdProduto = (int)reader[0];
                    produto.NomeProduto = (string)reader[1];
                    produto.ValorProduto = (decimal)reader[2];
                    produto.QtdeProduto = (int)reader[3];
                }

            }
            catch (Exception e)
            {
                throw e;
            }
            return produto;

        }







    }
}