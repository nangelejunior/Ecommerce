using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace Ecommerce
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        // variável de conexão
        private SqlConnection con;
        // instância da classe banco
        private Banco bd = new Banco();
        // variável de comandos SQL
        private SqlCommand cmd;
        // variável que obtém o valores do banco
        private SqlDataReader dr;

        protected void Page_Load(object sender, EventArgs e)
        {
            // se a sessão não estiver vazia
            if (Session["Codigo_Usuario"].ToString() != "")
            {
                // cria e inicializa a variável
                string sql = "";

                // comando SQL de seleção
                sql = "SELECT SUM(qtdade) as Itens FROM Carrinho " +
                    "WHERE cliente = " + Session["Codigo_Usuario"].ToString();

                // tenta executar o comando SQL
                try
                {
                    // pego a conexão com o banco
                    con = bd.getConexao();
                    // instância a classe SqlCommand
                    cmd = new SqlCommand();
                    // passa a conexão para o objeto
                    cmd.Connection = con;
                    // passa o comando SQL ao objeto cmd
                    cmd.CommandText = sql;
                    // executa o comando SQL
                    dr = cmd.ExecuteReader();

                    // se retornar um valor
                    if (dr.HasRows == true)
                    {
                        // se conseguiu ler o resultado
                        if (dr.Read() == true)
                        {
                            // se for nulo
                            if (dr["Itens"].ToString() == "")
                            {
                                // exive 0
                                lnkCarrinho.Text = "" + 0 + " item";
                            }
                            else if (String.Format("{0:f0}", dr["Itens"]) == "1")
                            {
                                // exibe o campo carrinho
                                lnkCarrinho.Text = String.Format("{0:f0}", dr["Itens"]) + " item";
                            }
                            else
                            {
                                // exibe o campo carrinho
                                lnkCarrinho.Text = String.Format("{0:f0}", dr["Itens"]) + " itens";
                            } // fim do if..else
                        } // fim do if
                    } // fim do if
                }
                catch (SqlException ex)
                {
                    // exibe o erro
                    lnkCarrinho.Text = "Erro!";
                }
                finally
                {
                    cmd.Dispose(); // elimina o objeto da memória
                    dr.Dispose(); // elimina o objeto da memória
                } // fim do try..cath..finally
            }
            else
            {
                // exibe 0
                lnkCarrinho.Text = "" + 0 + " item";
            } // fim do if..else
        }

        protected void imgCarrinho_Click(object sender, ImageClickEventArgs e)
        {
            // redireciona para a página do carrinho
            Response.Redirect("Carrinho.aspx");
        }

        protected void lnkCarrinho_Click(object sender, EventArgs e)
        {
            // redireciona para a página do carrinho
            Response.Redirect("Carrinho.aspx");
        }
    }
}
