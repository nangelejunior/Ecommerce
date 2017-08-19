using System;
using System.Data;
using System.Data.SqlClient;

namespace Ecommerce
{
    public class Banco
    {
        // string de conexão com o banco de dados
        private string conexao = "Data Source = .\\SQLEXPRESS; " +
            "Initial Catalog = ecommerce; Integrated Security = true;";
        // conexão propriamente dita
        private SqlConnection con;

        // método que abre a conexão com o banco
        public bool AbrirBanco()
        {
            // cria e inicializa a variável
            bool status = false;
            // instância a classe SqlConnection
            con = new SqlConnection();
            // passa a string de conexão para o objeto de conexão
            con.ConnectionString = conexao;

            // tenta abrir o banco
            try
            {
                con.Open(); // abre o banco
                status = true; // se abriu com sucesso status recebe true
            }
            catch (SqlException ex)
            {
                // caso ocorra um falha ao abrir o banco status recebe false
                status = false;
            } // fim do try..catch

            return status;
        } // fim do método AbrirBanco

        // método que fecha a conexão com o banco
        public bool FecharBanco(SqlConnection c)
        {
            // cria e inicializa a variável
            bool status = false;

            // se o estado do objeto c é Open
            if (c.State == ConnectionState.Open)
            {
                // tenta fechara a conexão
                try
                {
                    c.Close(); // fecha o banco
                    status = true; // se fechou com sucesso status recebe true    
                }
                catch (SqlException ex)
                {
                    // caso ocorra uma falha ao fechar o banco status recebe false
                    status = false;
                }
                finally
                {
                    c.Dispose(); // limpa o objeto da memória
                } // fim do try..catch..finally
            }
            else
            {
                // se não, status recebe true
                status = true;
            } // fim do if..else

            return status; 
        } // fim do métodod fechar banco

        // método que retorna a conexão existente
        public SqlConnection getConexao()
        {
            // se com é igual a null
            if (con == null)
            {
                // invoca o método AbrirBanco
                this.AbrirBanco();
            }
            else if (con.State == ConnectionState.Closed)
            {
                // se não, se a conexão com o banco estiver fechada
                // abre a conexão
                this.AbrirBanco();
            } // fim do if..else

            return con;
        } // fim do método getConexao
    }
}
