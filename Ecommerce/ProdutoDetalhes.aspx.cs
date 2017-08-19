using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

namespace Ecommerce
{
    public partial class ProdutoDetalhes : System.Web.UI.Page
    {
        // variável de conexão com o banco de dados
        private SqlConnection con;
        // instância da classe banco
        private Banco bd = new Banco();
        // instância da classe Utilitario
        private Utilitario utl = new Utilitario();
        // variável de comandos SQL
        private SqlCommand cmd;
        // variável que retem o resultado dos comandos SQL
        private SqlDataReader dr;

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.ExpiresAbsolute = DateTime.Now;

            // variável local que recebe a sintaxe SQL
            string sql = "";

            // se a sessão não retornar o código do usuário
            if (Session["Codigo_Usuario"].ToString() == "")
            {
                // redireciona para a página de login
                Response.Redirect("Login.aspx");
            } // fim do if

            // comando SQL de seleção
            sql = "SELECT p.foto, descricao = p.descricao + ' - ' + f.nome + ' Código: ' + CONVERT(char(10), p.codigo), " +
                "p.val_venda, p.caracteristicas FROM Produto p " +
                "INNER JOIN Fornecedor f " +
                "ON f.codigo = p.fornecedor " +
                "WHERE p.codigo = " + Session["Cod_Produto"].ToString();

            // tenta recuperar o produto
            try
            {
                // instância da classe SqlConnection
                con = new SqlConnection();
                // pego a conexão com o banco
                con = bd.getConexao();
                // instância a classe SqlCommand
                cmd = new SqlCommand();
                // passa a conexão para o objeto
                cmd.Connection = con;
                // passa o comando SQL ao objeto cmd
                cmd.CommandText = sql;

                // obtem o resultado da consulta
                dr = cmd.ExecuteReader();

                // se obteve algum resultado na consulta
                if (dr.HasRows == true)
                {
                    // se conseguiu
                    if (dr.Read() == true)
                    {
                        // mostra a descricão
                        lblDescricao.Text = dr["descricao"].ToString();
                        // mostra a imagem
                        imgProduto.ImageUrl = "~/Images/" + dr["foto"].ToString();
                        // mostra o preço
                        lblPreco.Text = String.Format("{0:c}", dr["val_venda"]);
                        // mostra os detalhes
                        lblDetalhes.Text = dr["caracteristicas"].ToString();
                    } // fim do if
                } // fim fo if
            }
            catch (SqlException ex)
            {
                // informa sobre o erro
                lblMensagem.Text = "Erro ao obter os detalhes do produto! <br>" +
                    ex.Message;
            }
            finally
            {
                // elimina o objeto da memória
                cmd.Dispose();
                // elimina o objeto da memória
                dr.Dispose();
            } // fim do try..cath..finally
        }

        protected void lnkSair_Click(object sender, EventArgs e)
        {
            // redireciona para a página de default
            Response.Redirect("Default.aspx");
        }

        protected void imgAdicionar_Click(object sender, ImageClickEventArgs e)
        {
            // variável que recebe false quando não é permitido inserir
            // no carrinho e true quando é permitido
            bool inserir = false;

            // variável local que recebe a sintaxe SQL
            string sql = "";

            // se a sessão código do produto não estiver vazio
            if (Session["Cod_Produto"].ToString() != "")
            {
                // comando SQL de seleção
                sql = "SELECT produto FROM Carrinho WHERE produto = "
                    + Session["Cod_Produto"].ToString();

                // tenta recuperar se o produto do carrinho
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

                    // obtem o resultado da consulta
                    dr = cmd.ExecuteReader();

                    // se obteve algum resultado na consulta
                    if (dr.HasRows == true)
                    {
                        // significa que o produto já está no carrinho
                        // inserir recebe false
                        inserir = false;
                        // informa o usuário
                        lblMensagem.Text = "Já existe esse produto no Carrinho!";
                    }
                    else
                    {
                        // se não, significa que o produto ainda não esta no carrinho
                        // inserir recebe true
                        inserir = true;
                    } // fim do if..else
                }
                catch (SqlException ex)
                {
                    // informa sobre o erro
                    lblMensagem.Text = "Erro ao obter os produtos do carrinho! <br>" +
                        ex.Message;
                }
                finally
                {
                    // elimina o objeto da memória
                    cmd.Dispose();
                    // elimina o objeto da memória
                    dr.Dispose();
                } // fim do try..cath.finally

                // se inserir for true
                if (inserir == true)
                {
                    // comando SQL de inserção
                    sql = "INSERT INTO Carrinho(cliente, produto, qtdade) " +
                        "VALUES(" + Session["Codigo_Usuario"].ToString() +
                        ", " + Session["Cod_Produto"].ToString() +
                        ", 1.0)";

                    // se não conseguiu inserir o produto no carrinho
                    if (utl.executeSQL(sql) == false)
                    {
                        // informa sobre o erro
                        lblMensagem.Text = "Erro ao inserir no carrinho!";
                    }
                    else
                    {
                        // se não,
                        // redireciona para a página do carrinho
                        Response.Redirect("Carrinho.aspx");
                    } // fim do if..else
                } // fim do if
            } // fim do if

            // limpa a sessão que armazena o código do produto
            Session.Add("Cod_Produto", "");
        }
    }
}
