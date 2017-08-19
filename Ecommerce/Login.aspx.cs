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

namespace Ecommerce.Account
{
    public partial class Login : System.Web.UI.Page
    {
        // variável de conexão com o banco de dados
        private SqlConnection con;
        // instância da classe Banco
        private Banco bd = new Banco();
        // variável de comandos SQL
        private SqlCommand cmd;
        // variável que retem o resultado dos comandos SQL
        private SqlDataReader dr;

        protected void Page_Load(object sender, EventArgs e)
        {
            // cria uma sessão
            Session.Add("Codigo_Usuario", "");
            // foca o campo usuario
            smg.FindControl("txtUsuario").Focus();
        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            // variável local que recebe a sintaxe SQL
            string sql = "";

            // se o campo nome do usuário estiver vazio
            if (txtUsuario.Text == String.Empty)
            {
                // habilita a mensagem de erro
                lblMsgUsuario.Visible = true;
                // foca o campo usuario
                smg.FindControl("txtUsuario").Focus();
                // encerra o processamento
                return;
            } // fim do if

            // se o campo senha estiver vazio
            if (txtSenha.Text == String.Empty)
            {
                // habilita a mensagem de erro
                lblMsgSenha.Visible = true;
                // foca o campo senha
                smg.FindControl("txtSenha").Focus();
                // encerra o processamento
                return;
            } // fim do if

            // se ao abrir o banco retornar true
            if (bd.AbrirBanco() == true)
            {
                // comando SQL de seleção
                sql = "SELECT Codigo FROM Cliente WHERE Usuario = '" + 
                    txtUsuario.Text + "' AND Senha = '" + txtSenha.Text + "'";

                // tenta localizar o cliente
                try
                {
                    // instância da classe SqlConnection
                    con = new SqlConnection();
                    // o objeto con recebe a conexão existente
                    con = bd.getConexao();
                    // instância da classe SqlCommand
                    cmd = new SqlCommand();
                    // passa a conexão ao objeto cmd
                    cmd.Connection = con;
                    // passa o comando SQL ao objeto cmd
                    cmd.CommandText = sql;

                    // obtém o resultado da cunsutla
                    dr = cmd.ExecuteReader();

                    // se obteve algum resultado na consulta
                    if (dr.HasRows == true)
                    {
                        // se conseguiu ler o resultado da consulta
                        if (dr.Read() == true)
                        {
                            // adiciona o codigo obtido à sessão
                            Session.Add("Codigo_Usuario", dr["codigo"].ToString());
                            // redireciona para a página de produtos
                            Response.Redirect("Produtos.aspx");
                        } // fim do if
                    }
                    else
                    {
                        // se não, exibe o erro
                        lblMensagem.Text = "Usuário ou senha inválido!";
                        // não adiciona nada a sessão
                        Session.Add("Codigo_Usuario", "");
                    } // fim do if..else
                }
                catch (SqlException ex)
                {
                    // informa sobre o erro
                    lblMensagem.Text = "Erro ao obter as informaçõs de Log in! <br>" +
                        ex.Message;
                }
                finally
                {
                    // elimina o objeto da memória
                    dr.Dispose();
                    // elimina o objeto da memória
                    cmd.Dispose();
                } // fim do try..cath..finally
            } // fim do if
        }

        protected void txtUsuario_TextChanged(object sender, EventArgs e)
        {
            // se a mensagem de erro estiver visivel e o campo estiver preenchido
            if ((lblMsgUsuario.Visible == true) && (lblMsgUsuario.Text != String.Empty))
            {
                // esconde a mensagem de erro
                lblMsgUsuario.Visible = false;
            } // fim do if
        }

        protected void txtSenha_TextChanged(object sender, EventArgs e)
        {
            // se a mensagem de erro estiver visivel e o campo estiver preenchido
            if ((lblMsgSenha.Visible == true) && (lblMsgSenha.Text != String.Empty))
            {
                // esconde a mensagem de erro
                lblMsgSenha.Visible = false;
            } // fim do if
        }

        protected void lnkClique_Click(object sender, EventArgs e)
        {
            // redireciona para a pagina de cadastro
            Response.Redirect("Cadastro.aspx");
        }
    }
}
