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
    public partial class Carrinho : System.Web.UI.Page
    {
        // variável de conexão com o banco de dados
        private SqlConnection con;
        // instância da classe banco
        private Banco bd = new Banco();
        // instância da classe utilitario
        private Utilitario utl = new Utilitario();
        // variável de comandos SQL
        private SqlCommand cmd;
        // variável que retem o resultado dos comandos SQL
        private SqlDataReader dr;
        // variável que intermedia o DataSet e o banco de dados 
        private SqlDataAdapter dap;
        // variável retem os dados recuperados do banco  
        private DataSet ds;
     
        // método que preenche o gridView
        protected void preencherGridView()
        {
            // variável local que recebe a sintaxe SQL
            string sql = "";

            // comando SQL de seleção
            sql = "SELECT c.item, p.foto, p.descricao, c.qtdade, " +
                "(c.qtdade * p.val_venda) AS Total_Produto " +
                "FROM Carrinho c " +
                "INNER JOIN Produto p " +
                "ON p.codigo = c.produto " +
                "WHERE c.cliente = " + Session["Codigo_Usuario"].ToString() +
                "ORDER BY c.item";

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
                dap.Fill(ds, "Carrinho");

                // preenche a GridView com o resultado obtido
                gdvCarrinho.DataSource = ds.Tables["Carrinho"].DefaultView;
                // redesenha o componente GridView
                gdvCarrinho.DataBind();

                // preenche o total do pedido
                lblTotalPedidoValor.Text = 
                    String.Format("{0:c}", ds.Tables["Carrinho"].Compute("Sum(Total_Produto)", ""));
            }
            catch (SqlException ex)
            {
                // informa sobre o erro
                lblMensagem.Text = "Erro ao obter as informações do carrinho de " +
                    "compras! <br>" + ex.Message;
            }
            finally
            {
                // elimina o objeto da memória
                dap.Dispose();
                // elimina o objeto da memória
                ds.Dispose();
            } // fim do try..cath..finally
        } // fim do método preencherGridView

        // metodo que retorna o total do pedido
        protected float getTotalPedido()
        {
            // varável que armazena o total_pedido
            float total_pedido = 0.0f;

            // variável local que recebe a sintaxe SQL
            string sql = "";

            // comando SQL de seleção
            sql = "SELECT c.item, p.foto, p.descricao, c.qtdade, " +
                "(c.qtdade * p.val_venda) AS Total_Produto " +
                "FROM Carrinho c " +
                "INNER JOIN Produto p " +
                "ON p.codigo = c.produto " +
                "WHERE c.cliente = " + Session["Codigo_Usuario"].ToString() +
                "ORDER BY c.item";

            // tenta executar o comando SQL
            try
            {
                // o objeto con recebe a conexão existente
                con = bd.getConexao();
                // instância da classe SqlDataAdapter
                dap = new SqlDataAdapter(sql, con);
                // instância da classe DataSet
                ds = new DataSet();

                // atribui o nome a tabela
                dap.Fill(ds, "Carrinho");

                // se o DataSet não estiver vazio
                if ((ds != null) && (ds.Tables["Carrinho"].Rows.Count > 0))
                {
                    // calcula o total
                    total_pedido = float.Parse(
                        ds.Tables["Carrinho"].Compute("SUM(Total_Produto)", "").ToString());
                }
                else
                {
                    // total do pedido recebe 0
                    total_pedido = 0.00f;
                } // if else
            }
            catch (SqlException ex)
            {
                // informa sobre o erro
                lblMensagem.Text = "Erro ao obter as informações do carrinho de " +
                    "compras! <br>" + ex.Message;
            }
            finally
            {
                // elimina o objeto da memória
                dap.Dispose();
                // elimina o objeto da memória
                ds.Dispose();
            } // fim do try..cath..finally

            return total_pedido;
        } // fim do método getTotalPedido

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.ExpiresAbsolute = DateTime.Now;

            // se a sessão não retornar o código do usuário
            if (Session["Codigo_Usuario"].ToString() == "")
            {
                // redireciona para a página de login
                Response.Redirect("Login.aspx");
            } // fim do if

            // se for a primeira vez que a página é acessada
            if (!Page.IsPostBack)
            {                
                // preenche o gridView
                preencherGridView();
            } // fim do if
        }

        protected void gdvCarrinho_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // pega o número da linha
            int linha = Convert.ToInt32(e.CommandArgument);

            // pega o item que o usuário deseja alterar ou excluir
            string item = gdvCarrinho.Rows[linha].Cells[0].Text;

            // se o usuário clicou em alterar
            if (e.CommandName == "alterar")
            {
                // adiciono o item a sessão
                Session.Add("Item", item);

                lblQtdade.Visible = true; // mostra a label qtdade 
                txtQtdade.Visible = true; // mostra o textBox qtdade
                txtQtdade.Text = ""; // limpa o campo qtdade
                btnOK.Visible = true; // mostra o botão OK

                // foca o campo qtdade
                smg.FindControl("txtQtdade").Focus();
            } // fim do if
            else if (e.CommandName == "excluir")
            {
                // se o usuário clicou em excluir
                // variável local que recebe a sintaxe SQL
                string sql = "";

                // se o item não estiver vazio
                if (item != "")
                {
                    // comando SQL de deleção
                    sql = "DELETE FROM Carrinho " +
                        "WHERE item = " + item;

                    // se não conseguir excluir o item do carrinho
                    if (utl.executeSQL(sql) == false)
                    {
                        // informa sobre o erro
                        lblMensagem.Text = "Erro ao excluir o item do carrinho!";
                    } // fim do if
                    else
                    {
                        // redireciona para a própria página
                        Response.Redirect("Carrinho.aspx");
                    }
                } // fim do if
            } // fim do if..else
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            int qtdade = 0;

            // variável local que recebe a sintaxe SQL
            string sql = "";

            // se o textBox estiver vazio ou não contiver um número ou a qtdade for igual a 0
            if ((txtQtdade.Text == String.Empty) || ((!int.TryParse(txtQtdade.Text, out qtdade))) || (int.Parse(txtQtdade.Text) == 0))
            {
                // informa sobre o erro
                lblMensagem.Text = "A quantidade informada é invalida!";
            }
            else
            {
                // se não
                // se a sessão item não estiver vazia
                if (Session["Item"].ToString() != "")
                {
                    // comando SQL de alteração
                    sql = "UPDATE Carrinho SET qtdade = " + txtQtdade.Text +
                        " WHERE item = " + Session["Item"].ToString();

                    // se não alterar a quantidade do carrinho
                    if (utl.executeSQL(sql) == false)
                    {
                        // informa sobre o erro
                        lblMensagem.Text = "Erro ao alterar a quantidade!";
                    }
                    else
                    {
                        // se não
                        lblQtdade.Visible = false; // esconde a label qtdade 
                        txtQtdade.Visible = false; // esconde o textBox qtdade
                        txtQtdade.Text = ""; // limpa o campo qtdade
                        btnOK.Visible = false; // esconde o botão OK

                        // redireciona para a própria página
                        Response.Redirect("Carrinho.aspx");

                        // limpa a sessão item
                        Session.Add("Item", "");
                    } // fim do i..else
                } // fim do if
            } // fim do if..else
        }

        protected void btnFinalizar_Click(object sender, EventArgs e)
        {
            // variável local que recebe a sintaxe SQL
            string sql = "";

            // ArrayList que aramazena os itens do carrinho
            ArrayList produtosCarrinho = new ArrayList();

            // se não foi selecionada nenhuma forma de pagamento
            if ((!rblFormaPagto.Items[0].Selected) && (!rblFormaPagto.Items[1].Selected) && (!rblFormaPagto.Items[2].Selected))
            {
                // informa sobre o erro
                lblMensagem.Text = "Escolha a forma de pagamento!";
                // foca o campo forma de pagamento
                smg.FindControl("rblFormaPagto").Focus();
                // encerra o processamento
                return;
            }
            else if ((rblFormaPagto.Items[1].Selected) || (rblFormaPagto.Items[2].Selected))
            {
                // se crédito ou débito estiverem selecionados
                // verifica se o usuário selecionou o cartão
                if ((!rblCartao.Items[0].Selected) && (!rblCartao.Items[1].Selected))
                {
                    // informa sobre o erro
                    lblMensagem.Text = "Escolha o seu cartão!";
                    // foca o campo forma de pagamento
                    smg.FindControl("rblCartao").Focus();
                    // encerra o processamento
                    return;
                } // fim do if
            } // fim do if..else

            // comando SQL de seleção
            sql = "INSERT INTO Pedido " +
                "VALUES('" + DateTime.Now.ToString("MM/dd/yyyy") +
                "', '" + DateTime.Today.AddDays(7).ToString("MM/dd/yyyy") +
                "', " + Session["Codigo_Usuario"].ToString();

            // se o usuário selecionou crédido
            if (rblFormaPagto.Items[1].Selected)
            {
                // concatena forma de pagamento
                // cartão escolhido e
                // número de parcelas
                sql = sql + ", '" + rblFormaPagto.SelectedValue +
                  " - " + rblCartao.SelectedValue +
                  " - " + ddlParcela.SelectedValue;
            }
            else if (rblFormaPagto.Items[2].Selected)
            {
                // se não, se o usuário selecionou debito
                // concatena forma de pagamento e
                // cartão escolhido
                sql = sql + ", '" + rblFormaPagto.SelectedValue +
                    " - " + rblCartao.SelectedValue;
            }
            else
            {
                // concatena forma de pagamento
                sql = sql + ", '" + rblFormaPagto.SelectedValue;
            } // fim do if..else

            // restante do SQL de inserção
            sql = sql + "', " + this.getTotalPedido() + ")";

            // se o total do pedido for diferente de 0
            if (this.getTotalPedido() == 0)
            {
                // informa sobre o erro
                lblMensagem.Text = "Você não possui itens no carrinho!";
            }
            else if (utl.executeSQL(sql) == true)
            {
                //se conseguir gerar o pedido
                // comando SQL de seleção
                sql = "SELECT MAX(nro_pedido) as Nro_Pedido FROM Pedido WHERE cliente = "
                        + Session["Codigo_Usuario"].ToString();

                // tenta obter o número do pedido
                try
                {
                    // o objeto con recebe a conexão existente
                    con = bd.getConexao();
                    // instância a classe SqlCommand
                    cmd = new SqlCommand();
                    // passa a conexão para o objeto
                    cmd.Connection = con;
                    // passa o comando SQL ao objeto cmd
                    cmd.CommandText = sql;
                    // inicializa o objeto dr

                    // obtem o resultado da consulta
                    dr = cmd.ExecuteReader();

                    // se obteve algum resultado na consulta
                    if (dr.HasRows == true)
                    {
                        // se conseguiu ler o resultado da consulta
                        if (dr.Read() == true)
                        {
                            // adiciona o número do pedido a sessão
                            Session.Add("Nro_Pedido", dr["Nro_Pedido"].ToString());
                        } // fim do if
                    } // fim do if
                }
                catch (SqlException ex)
                {
                    // informa sobre o erro
                    lblMensagem.Text = "Erro ao obter o número do pedido! <br>" +
                        ex.Message;
                }
                finally
                {
                    // elimina o objeto da memória
                    cmd.Dispose();
                    // elimina o objeto da memória
                    dr.Dispose();
                } // fim do try..cath..finally

                // se a sessão que armazena o número do pedido não estiver vazia
                if (Session["Nro_Pedido"].ToString() != "")
                {
                    // comando SQL de seleção
                    sql = "SELECT c.produto, c.qtdade, p.val_venda FROM Carrinho c " +
                            "INNER JOIN Produto p " +
                            "ON p.codigo = c.produto " +
                            "WHERE cliente = " + Session["Codigo_Usuario"].ToString();

                    // tenta obter os produtos do carrinho
                    try
                    {
                        // o objeto con recebe a conexão existente
                        con = bd.getConexao();
                        // instância da classe SqlCommand
                        cmd = new SqlCommand();
                        // passa a conexão para o objeto
                        cmd.Connection = con;
                        // passa o comando SQL ao objeto cmd
                        cmd.CommandText = sql;

                        // obtém o resultado da consulta
                        dr = cmd.ExecuteReader();

                        // se obteve algum resultado na consulta
                        if (dr.HasRows == true)
                        {
                            // percorre o resultado da consulta
                            while (dr.Read() == true)
                            {
                                // armazena o resultado em um ArrayList
                                produtosCarrinho.Add(Session["Nro_Pedido"].ToString() +
                                    ", " + dr["produto"].ToString() +
                                    ", " + double.Parse(dr["qtdade"].ToString()) +
                                    ", " + float.Parse(dr["val_venda"].ToString()));
                            } // fim do while
                        } // fim do if
                    }
                    catch (SqlException ex)
                    {
                        // informa sobre o erro
                        lblMensagem.Text = "Erro ao obter os itens do carrinho! <br>" +
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
                else
                {
                    // informa sobre o erro
                    lblMensagem.Text = "Número de pedido vazio!";
                    // encerra o processamento
                    return;
                } // fim do if

                // se o ArryList produtosCarrinho não estiver zerado
                if (produtosCarrinho.Count != 0)
                {
                    // recupera os produtos do ArrayList
                    foreach (string str in produtosCarrinho)
                    {
                        // comando SQL de inserção
                        sql = "INSERT INTO Item_Pedido " +
                            "VALUES(" + str + ")";

                        // se executeSQL retornar false
                        if (utl.executeSQL(sql) == false)
                        {
                            // informa sobre o erro
                            lblMensagem.Text = "Erro ao inserir os itens do pedido!";
                            // encerra o processamento
                            return;
                        } // fim do if
                    } // fim do foreach

                    // comando SQL de deleção
                    sql = "DELETE FROM Carrinho " +
                        "WHERE cliente = " + Session["Codigo_Usuario"].ToString();

                    // se conseguiu excluir os itens do carrinho
                    if (utl.executeSQL(sql) == true)
                    {
                        // redireciona para a página de pedidos gerados
                        Response.Redirect("PedidoGerado.aspx");
                    }
                    else
                    {
                        // informa sobre o erro
                        lblMensagem.Text = "Erro ao limpar o carrinho de compras!";
                        // encerra o processamento
                        return;
                    } // fim do if..else
                }
                else
                {
                    // informa sobre o erro
                    lblMensagem.Text = "Não encontrou itens no ArrayList!";
                    // encerra o processamento
                    return;
                } // fim do if else
            }
            else
            {
                // informa sobre o erro
                lblMensagem.Text = "Erro ao gerar o pedido!";
                // encerra o processamento
                return;
            } // fim do if..else
        }

        protected void btnComprando_Click(object sender, EventArgs e)
        {
            // redireciona para a página de produtos
            Response.Redirect("Produtos.aspx");
        }

        protected void lnkLogOut_Click(object sender, EventArgs e)
        {
            // redireciona para a página de login
            Response.Redirect("Login.aspx");
        }

        protected void rblFormaPagto_SelectedIndexChanged(object sender, EventArgs e)
        {
            // se boleto estiver marcado
            if (rblFormaPagto.Items[0].Selected)
            {
                // esconde a forma de pagamento
                rblCartao.Visible = false;
                // esconde os cartões
                imgVisa.Visible = false;
                imgMCard.Visible = false;
                // esconde as parcelas
                lblParcela.Visible = false;
                ddlParcela.Visible = false;
            }
            else if (rblFormaPagto.Items[2].Selected)
            {
                // se não, se debito estiver marcado
                // mostra a forma de pagamento
                rblCartao.Visible = true;
                // mostra os cartões
                imgVisa.Visible = true;
                imgMCard.Visible = true;
                // esconde as parcelas
                lblParcela.Visible = false;
                ddlParcela.Visible = false;
            }
            else
            {
                // se não
                // mostra a forma de pagamento
                rblCartao.Visible = true;
                // mostra os cartões
                imgVisa.Visible = true;
                imgMCard.Visible = true;
            } // fim do if..else
        }

        protected void rblCartao_SelectedIndexChanged(object sender, EventArgs e)
        {
            // se a forma de pagamento for credito
            if (rblFormaPagto.Items[1].Selected)
            {
                // mostra o parcelamento
                lblParcela.Visible = true;
                ddlParcela.Visible = true;
            } // fim do if
        }

        protected void lnkSair_Click(object sender, EventArgs e)
        {
            // redireciona para a página de default
            Response.Redirect("Default.aspx");
        }
    }
}
