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
    public partial class Produtos : System.Web.UI.Page
    {
        // variável de conexão com o banco de dados
        private SqlConnection con;
        // instância da classe banco
        private Banco bd = new Banco();
        // instância da classe Utilitario
        private Utilitario utl = new Utilitario();
        // variável de comandos SQL
        private SqlCommand cmd;
        // // variável que retem o resultado dos comandos SQL
        private SqlDataReader dr;
        // variável que intermedia o DataSet e o banco de dados 
        private SqlDataAdapter dap;
        // variável retem os dados recuperados do banco  
        private DataSet ds;

        // categorias do site
        private enum Categoria 
        { 
            Geral, Acessorios, Celulares, Desktops, Notebooks
        } // fim Categoria

        // variável que aramazena a categoria do site
        Categoria ctg;

        // método que preenche a gridView
        protected void preencherGridView(string sql)
        {
            // tenta executar o comando SQL
            try
            {
                // instância da classe SqlConnection
                con = new SqlConnection();
                // o objeto con recebe a conexão existente
                con = bd.getConexao();
                // instância da classe SqlDataAdapter
                dap = new SqlDataAdapter(sql, con);
                // instância da classe DataSet
                ds = new DataSet();

                // atribui o nome a tabela
                dap.Fill(ds, "Produtos");

                // preenche a GridView com o resultado obtido
                gdvProdutos.DataSource = ds.Tables["Produtos"].DefaultView;
                // redesenha o componente GridView
                gdvProdutos.DataBind();
            }
            catch (SqlException ex)
            {
                // exibe o erro
                lblMensagem.Text = "Erro ao obter os produtos!" +
                    "<br>" + ex.Message;
            }
            finally
            {
                dap.Dispose(); // elimina o objeto da memória
                ds.Dispose(); // elimina o objeto da memória
            } // fim do try..cath..finally
        } // fim do método preencherGridView

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.ExpiresAbsolute = DateTime.Now;

            // se a sessão não retornar o código do usuário
            if (Session["Codigo_Usuario"].ToString() == "")
            {
                // receve entrar
                lnkSair.Text = "Entrar";
            }
            else
            {
                // se não, sair
                lnkSair.Text = "Sair";
            } // fim do if..else

            // se for a primeira vez que a página é acessada
            if (!Page.IsPostBack)
            {
                // define a categoria e adiciona na session
                Session.Add("Categoria", ctg = Categoria.Geral);
                // mostra a categoria
                lblCategoriaValor.Text = Session["Categoria"].ToString();

                // cria e inicializa a variável
                string sql = "";

                // comando SQL de seleção
                sql = "SELECT p.codigo, p.foto, descricao = p.descricao + ' - ' + f.nome, " +
                    "p.val_venda FROM Produto p " +
                    "INNER JOIN Fornecedor f " +
                    "ON f.codigo = p.fornecedor " +
                    "ORDER BY " + ddlOrdem.SelectedValue;

                // preenche a grid de produtos
                preencherGridView(sql);
            } // fim do if
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            // variável local que recebe a sintaxe SQL
            string sql = "";

            // se estiver na categoria Geral
            if (Session["Categoria"].Equals(Categoria.Geral))
            {
                // se estiver ordenado por mais vendido
                if (ddlOrdem.SelectedValue == "venda")
                {
                    // comando SQL de seleção
                    sql = "SELECT p.codigo, p.foto, descricao = p.descricao + ' - ' + f.nome, " + 
                        "p.val_venda, SUM(i.qtdade) as qtdade FROM Produto p " +
                        "INNER JOIN Fornecedor f " +
                        "ON f.codigo = p.fornecedor " +
                        "INNER JOIN Item_Pedido i " +
                        "ON i.produto = p.codigo " +
                        "WHERE p.descricao LIKE '%" + txtPesquisar.Text + "%' " +
                        "GROUP BY p.codigo, p.foto, p.descricao, f.nome, p.val_venda " +
                        "ORDER BY qtdade DESC";
                }
                else
                {
                    // comando SQL de seleção
                    sql = "SELECT p.codigo, p.foto, descricao = p.descricao + ' - ' + f.nome, " +
                        "p.val_venda FROM Produto p " +
                        "INNER JOIN Fornecedor f " +
                        "ON f.codigo = p.fornecedor " +
                        "WHERE p.descricao LIKE '%" + txtPesquisar.Text + "%' " +
                        "ORDER BY " + ddlOrdem.SelectedValue;
                } // fim do if..else
            }
            else
            {
                // se estiver ordenado por mais vendido
                if (ddlOrdem.SelectedValue == "venda")
                {
                    // comando SQL de seleção
                    sql = "SELECT p.codigo, p.foto, descricao = p.descricao + ' - ' + f.nome, " +
                        "p.val_venda, SUM(i.qtdade) as qtdade FROM Produto p " +
                        "INNER JOIN Fornecedor f " +
                        "ON f.codigo = p.fornecedor " +
                        "INNER JOIN Item_Pedido i " +
                        "ON i.produto = p.codigo " +
                        "WHERE p.descricao LIKE '%" + txtPesquisar.Text + "%' " +
                        "AND p.categoria = (SELECT codigo FROM Categoria WHERE descricao LIKE '%" +
                            Session["Categoria"].ToString() + "%') " +
                        "GROUP BY p.codigo, p.foto, p.descricao, f.nome, p.val_venda " +
                        "ORDER BY qtdade DESC";
                }
                else
                {
                    // comando SQL de seleção
                    sql = "SELECT p.codigo, p.foto, descricao = p.descricao + ' - ' + f.nome, " +
                        "p.val_venda FROM Produto p " +
                        "INNER JOIN Fornecedor f " +
                        "ON f.codigo = p.fornecedor " +
                        "WHERE p.descricao LIKE '%" + txtPesquisar.Text + "%' " +
                        "AND p.categoria = (SELECT codigo FROM Categoria WHERE descricao LIKE '%" +
                             Session["Categoria"].ToString() + "%') " +
                        "ORDER BY " + ddlOrdem.SelectedValue;
                }
            } // fim do if..else

            // preenche a gridView
            preencherGridView(sql);
        }

        protected void lnkAcessorios_Click(object sender, EventArgs e)
        {
            // define a categoria e adiciona na session
            Session.Add("Categoria", ctg = Categoria.Acessorios);
            // mostra a categoria
            lblCategoriaValor.Text = Session["Categoria"].ToString();

            // variável local que recebe a sintaxe SQL
            string sql = "";

            if (ddlOrdem.SelectedValue == "venda")
            {
                // comando SQL de seleção
                sql = "SELECT p.codigo, p.foto, descricao = p.descricao + ' - ' + f.nome, " +
                    "p.val_venda, SUM(i.qtdade) as qtdade FROM Produto p " +
                    "INNER JOIN Fornecedor f " +
                    "ON f.codigo = p.fornecedor " +
                    "INNER JOIN Item_Pedido i " +
                    "ON i.produto = p.codigo " +
                    "WHERE p.categoria = (SELECT codigo FROM Categoria WHERE descricao LIKE '%" +
                         Session["Categoria"].ToString() + "%') " +
                    "GROUP BY p.codigo, p.foto, p.descricao, f.nome, p.val_venda " +
                    "ORDER BY qtdade DESC";
            }
            else
            {
                // comando SQL de seleção
                sql = "SELECT p.codigo, p.foto, descricao = p.descricao + ' - ' + f.nome, " + 
                    "p.val_venda FROM Produto p " +
                    "INNER JOIN Fornecedor f " +
                    "ON f.codigo = p.fornecedor " +
                    "WHERE p.categoria = (SELECT codigo FROM Categoria WHERE descricao LIKE '%" +
                         Session["Categoria"].ToString() + "%') " +
                    "ORDER BY " + ddlOrdem.SelectedValue;
            } // fim do if..else

            // preenche a gridView
            preencherGridView(sql);
        }

        protected void lnkCelulares_Click(object sender, EventArgs e)
        {
            // define a categoria e adiciona na session
            Session.Add("Categoria", ctg = Categoria.Celulares);
            // mostra a categoria
            lblCategoriaValor.Text = Session["Categoria"].ToString();

            // cria e inicializa a variável
            string sql = "";

            if (ddlOrdem.SelectedValue == "venda")
            {
                // comando SQL de seleção
                sql = "SELECT p.codigo, p.foto, descricao = p.descricao + ' - ' + f.nome, " +
                    "p.val_venda, SUM(i.qtdade) as qtdade FROM Produto p " +
                    "INNER JOIN Fornecedor f " +
                    "ON f.codigo = p.fornecedor " +
                    "INNER JOIN Item_Pedido i " +
                    "ON i.produto = p.codigo " +
                    "WHERE p.categoria = (SELECT codigo FROM Categoria WHERE descricao LIKE '%" +
                         Session["Categoria"].ToString() + "%') " +
                    "GROUP BY p.codigo, p.foto, p.descricao, f.nome, p.val_venda " +
                    "ORDER BY qtdade DESC";
            }
            else
            {
                // comando SQL de seleção
                sql = "SELECT p.codigo, p.foto, descricao = p.descricao + ' - ' + f.nome, " +
                    "p.val_venda FROM Produto p " +
                    "INNER JOIN Fornecedor f " +
                    "ON f.codigo = p.fornecedor " +
                    "WHERE p.categoria = (SELECT codigo FROM Categoria WHERE descricao LIKE '%" +
                         Session["Categoria"].ToString() + "%') " +
                    "ORDER BY " + ddlOrdem.SelectedValue;
            } // fim do if..else

            // preenche a gridView
            preencherGridView(sql);
        }

        protected void lnkDesktops_Click(object sender, EventArgs e)
        {
            // define a categoria e adiciona na session
            Session.Add("Categoria", ctg = Categoria.Desktops);
            // mostra a categoria
            lblCategoriaValor.Text = Session["Categoria"].ToString();

            // cria e inicializa a variável
            string sql = "";

            if (ddlOrdem.SelectedValue == "venda")
            {
                // comando SQL de seleção
                sql = "SELECT p.codigo, p.foto, descricao = p.descricao + ' - ' + f.nome, " +
                    "p.val_venda, SUM(i.qtdade) as qtdade FROM Produto p " +
                    "INNER JOIN Fornecedor f " +
                    "ON f.codigo = p.fornecedor " +
                    "INNER JOIN Item_Pedido i " +
                    "ON i.produto = p.codigo " +
                    "WHERE p.categoria = (SELECT codigo FROM Categoria WHERE descricao LIKE '%" +
                         Session["Categoria"].ToString() + "%') " +
                    "GROUP BY p.codigo, p.foto, p.descricao, f.nome, p.val_venda " +
                    "ORDER BY qtdade DESC";
            }
            else
            {
                // comando SQL de seleção
                sql = "SELECT p.codigo, p.foto, descricao = p.descricao + ' - ' + f.nome, " +
                    "p.val_venda FROM Produto p " +
                    "INNER JOIN Fornecedor f " +
                    "ON f.codigo = p.fornecedor " +
                    "WHERE p.categoria = (SELECT codigo FROM Categoria WHERE descricao LIKE '%" +
                         Session["Categoria"].ToString() + "%') " +
                    "ORDER BY " + ddlOrdem.SelectedValue;
            } // fim do if..else

            // preenche a gridView
            preencherGridView(sql);
        }

        protected void lnkNotebooks_Click(object sender, EventArgs e)
        {
            // define a categoria e adiciona na session
            Session.Add("Categoria", ctg = Categoria.Notebooks);
            // mostra a categoria
            lblCategoriaValor.Text = Session["Categoria"].ToString();

            // cria e inicializa a variável
            string sql = "";

            if (ddlOrdem.SelectedValue == "venda")
            {
                // comando SQL de seleção
                sql = "SELECT p.codigo, p.foto, descricao = p.descricao + ' - ' + f.nome, " +
                    "p.val_venda, SUM(i.qtdade) as qtdade FROM Produto p " +
                    "INNER JOIN Fornecedor f " +
                    "ON f.codigo = p.fornecedor " +
                    "INNER JOIN Item_Pedido i " +
                    "ON i.produto = p.codigo " +
                    "WHERE p.categoria = (SELECT codigo FROM Categoria WHERE descricao LIKE '%" +
                         Session["Categoria"].ToString() + "%') " +
                    "GROUP BY p.codigo, p.foto, p.descricao, f.nome, p.val_venda " +
                    "ORDER BY qtdade DESC";
            }
            else
            {
                // comando SQL de seleção
                sql = "SELECT p.codigo, p.foto, descricao = p.descricao + ' - ' + f.nome, " +
                    "p.val_venda FROM Produto p " +
                    "INNER JOIN Fornecedor f " +
                    "ON f.codigo = p.fornecedor " +
                    "WHERE p.categoria = (SELECT codigo FROM Categoria WHERE descricao LIKE '%" +
                         Session["Categoria"].ToString() + "%') " +
                    "ORDER BY " + ddlOrdem.SelectedValue;
            } // fim do if..else

            // preenche a gridView
            preencherGridView(sql);
        }

        protected void gdvProdutos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // se a sessão não retornar o código do usuário
            if (Session["Codigo_Usuario"].ToString() == "")
            {
                // redireciona para a página de login
                Response.Redirect("Login.aspx");
            } // fim do if

            // variável que recebe false quando não é permitido inserir
            // no carrinho e true quando é permitido
            bool inserir = false;

            // pega o número da linha
            int linha = Convert.ToInt32(e.CommandArgument);

            // pega o código do produto
            string codigo = gdvProdutos.Rows[linha].Cells[0].Text;

            // se o usuário clicou em incluir
            if (e.CommandName == "incluir")
            {
                // variável local que recebe a sintaxe SQL
                string sql = "";

                // se código não estiver vazio
                if (codigo != "")
                {
                    // comando SQL de seleção
                    sql = "SELECT produto FROM Carrinho WHERE produto = " 
                        + codigo;

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
                            ", " + codigo +
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
            }
            else if (e.CommandName == "detalhar")
            {
                // se o comando for detalhar
                // aramazena o código do produto na sessão
                Session.Add("Cod_Produto", codigo);


                // redireciona para a página de detalhes do produto
                Response.Redirect("/ProdutoDetalhes.aspx");
            } // fim do if..else
        }

        protected void ddlOrdem_SelectedIndexChanged(object sender, EventArgs e)
        {
            // cria e inicializa a variável
            string sql = "";

            // se estiver na categoria Geraç
            if (Session["Categoria"].Equals(Categoria.Geral))
            {
                // se estiver ordenado por mais vendido
                if (ddlOrdem.SelectedValue == "venda")
                {
                    // comando SQL de seleção
                    sql = "SELECT p.codigo, p.foto, descricao = p.descricao + ' - ' + f.nome, " +
                        "p.val_venda, SUM(i.qtdade) as qtdade FROM Produto p " +
                        "INNER JOIN Fornecedor f " +
                        "ON f.codigo = p.fornecedor " +
                        "INNER JOIN Item_Pedido i " +
                        "ON i.produto = p.codigo " +
                        "GROUP BY p.codigo, p.foto, p.descricao, f.nome, p.val_venda " +
                        "ORDER BY qtdade DESC";
                }
                else
                {
                    // comando SQL de seleção
                    sql = "SELECT p.codigo, p.foto, descricao = p.descricao + ' - ' + f.nome, " +
                        "p.val_venda FROM Produto p " +
                        "INNER JOIN Fornecedor f " +
                        "ON f.codigo = p.fornecedor " +
                        "ORDER BY " + ddlOrdem.SelectedValue;
                } // fim do if..else
            }
            else
            {
                // se estiver ordenado por mais vendido
                if (ddlOrdem.SelectedValue == "venda")
                {
                    // comando SQL de seleção
                    sql = "SELECT p.codigo, p.foto, descricao = p.descricao + ' - ' + f.nome, " +
                        "p.val_venda, SUM(i.qtdade) as qtdade FROM Produto p " +
                        "INNER JOIN Fornecedor f " +
                        "ON f.codigo = p.fornecedor " +
                        "INNER JOIN Item_Pedido i " +
                        "ON i.produto = p.codigo " +
                        "WHERE p.categoria = (SELECT codigo FROM Categoria " +
                            "WHERE descricao LIKE '%" +
                             Session["Categoria"].ToString() + "%') " +
                        "GROUP BY p.codigo, p.foto, p.descricao, f.nome, p.val_venda " +
                        "ORDER BY qtdade DESC";
                }
                else
                {
                    // comando SQL de seleção
                    sql = "SELECT p.codigo, p.foto, descricao = p.descricao + ' - ' + f.nome, " +
                        "p.val_venda FROM Produto p " +
                        "INNER JOIN Fornecedor f " +
                        "ON f.codigo = p.fornecedor " +
                        "WHERE p.categoria = (SELECT codigo FROM Categoria " +
                            "WHERE descricao LIKE '%" +
                             Session["Categoria"].ToString() + "%') " +
                        "ORDER BY " + ddlOrdem.SelectedValue;
                } // fim do if..else
            } // fim do if..else

            // preenche a grid de produtos
            preencherGridView(sql);
        }

        protected void lnkLogOut_Click(object sender, EventArgs e)
        {
            // redireciona para a página de login
            Response.Redirect("Login.aspx");
        }

        protected void lnkTodos_Click(object sender, EventArgs e)
        {
            // define a categoria e adiciona na session
            Session.Add("Categoria", ctg = Categoria.Geral);
            // mostra a categoria
            lblCategoriaValor.Text = Session["Categoria"].ToString();

            // cria e inicializa a variável
            string sql = "";

            // se estiver ordenado por mais vendido
            if (ddlOrdem.SelectedValue == "venda")
            {
                // comando SQL de seleção
                sql = "SELECT p.codigo, p.foto, descricao = p.descricao + ' - ' + f.nome, " +
                    "p.val_venda, SUM(i.qtdade) as qtdade FROM Produto p " +
                    "INNER JOIN Fornecedor f " +
                    "ON f.codigo = p.fornecedor " +
                    "INNER JOIN Item_Pedido i " +
                    "ON i.produto = p.codigo " +
                    "GROUP BY p.codigo, p.foto, p.descricao, f.nome, p.val_venda " +
                    "ORDER BY qtdade DESC";
            }
            else
            {
                // comando SQL de seleção
                sql = "SELECT p.codigo, p.foto, descricao = p.descricao + ' - ' + f.nome, " +
                    "p.val_venda FROM Produto p " +
                    "INNER JOIN Fornecedor f " +
                    "ON f.codigo = p.fornecedor " +
                    "ORDER BY " + ddlOrdem.SelectedValue;
            } // fim do if..else

            // preenche a grid de produtos
            preencherGridView(sql);
        }

        protected void lnkSair_Click(object sender, EventArgs e)
        {
            // se a sessão não retornar o código do usuário
            if (Session["Codigo_Usuario"].ToString() == "")
            {
                // redireciona para a página de default
                Response.Redirect("Login.aspx");
            }
            else
            {
                // redireciona para a página de default
                Response.Redirect("Default.aspx");
            } // fim do if..else
        }
    }
}
