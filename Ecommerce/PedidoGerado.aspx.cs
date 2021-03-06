﻿using System;
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
    public partial class PedidoGerado : System.Web.UI.Page
    {
        // variável de conexão conexão com o bando de dados
        private SqlConnection con;
        // instância da classe Banco
        private Banco bd = new Banco();
        // variável de comandos SQL
        private SqlCommand cmd;
        // variável que retem o resultado dos comandos SQL
        private SqlDataReader dr;
        // variável que intermedia o DataSet e o banco de dados 
        private SqlDataAdapter dap;
        // variável retem os dados recuperados do banco  
        private DataSet ds;

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.ExpiresAbsolute = DateTime.Now;

            // se a sessão não retornar o código do usuário
            if (Session["Codigo_Usuario"].ToString() == "")
            {
                // redireciona para a página de login
                Response.Redirect("Login.aspx");
            } // fim do if

            // atribui o número do pedido a lblPedido
            lblPedido.Text = Session["Nro_Pedido"].ToString();
        }

        protected void lnkPedido_Click(object sender, EventArgs e)
        {
            // variável local que recebe a sintaxe SQL
            string sql = "";

            // comando SQL de seleção
            sql = "SELECT pd.nro_pedido, pd.data_pedido, c.nome_completo, c.cpf, " +
                "c.endereco, c.cidade, c.estado, c.cep, c.telefone, c.email, " +
                "pd.forma_pagto, pd.data_entrega FROM Pedido pd " +
                "INNER JOIN Cliente c " +
                    "ON c.codigo = pd.cliente " +
                "WHERE nro_pedido = " + Session["Nro_Pedido"].ToString();

            // tenta recuperar os dados do pedido
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
                        // exibe os detaques
                        lblDetalhes.Text =
                            "Pedido: " + dr["nro_pedido"].ToString() + "<br>" +
                            "Data: " + dr["data_pedido"].ToString() + "<br><br>" +
                            "<b>DADOS DE COBRANÇA E ENTREGA</b>" + "<br>" +
                            "Nome: " + dr["nome_completo"].ToString() + "<br>" +
                            "CPF: " + dr["cpf"].ToString() + "<br>" +
                            "Endereço: " + dr["endereco"].ToString() + "<br>" +
                            "Cidade: " + dr["cidade"].ToString() + "<br>" +
                            "Estado: " + dr["estado"].ToString() + "<br>" +
                            "CEP: " + dr["cep"].ToString() + "<br>" +
                            "Telefone: " + dr["telefone"].ToString() + "<br>" +
                            "E-mail: " + dr["email"].ToString() + "<br><br>" +
                            "<b>FORMA DE PAGAMENTO</b>" + "<br>" +
                            dr["forma_pagto"].ToString() + "<br><br>" +
                            "<b>PRAZO DE ENTREGA</b>" + "<br>" +
                            dr["data_entrega"].ToString() + "<br><br>" +
                            "<b>ITENS DO PEDIDO</b>";
                    } // fim do if
                } // fim do if               
            }
            catch (SqlException ex)
            {
                // informa sobre o erro
                lblDetalhes.Text = "Erro ao obter detalhes do pedido! <br>" +
                    ex.Message;
            }
            finally
            {
                // elimina o objeto da memória
                dr.Dispose();
                // elimina o objeto da memória
                cmd.Dispose();
            } // fim do try..cath..finaly

            // comando SQL de seleção
            sql = "SELECT i.qtdade, p.descricao, total_produto = (i.qtdade * p.val_venda) " +
                "FROM Item_Pedido i " +
                "INNER JOIN Produto p " +
                "ON p.codigo = i.produto " +
                "WHERE pedido = " + Session["Nro_Pedido"].ToString();

            // tenta obter o itens do pedido
            try
            {
                // o objeto con recebe a conexão existente
                con = bd.getConexao();
                // instância da classe SqlDataAdapter
                dap = new SqlDataAdapter(sql, con);
                // instância da classe DataSet
                ds = new DataSet();

                // atribui o nome a tabela
                dap.Fill(ds, "Itens");

                // preenche a GridView com o resultado obtido
                gdvItens.DataSource = ds.Tables["Itens"].DefaultView;
                // redesenha o componente GridView
                gdvItens.DataBind();

                // se o DataSet não estiver vazio
                if ((ds != null) && (ds.Tables["Itens"].Rows.Count > 0))
                {
                    // habilia o label TotalPedido
                    lblTotalPedido.Visible = true;
                    // habilita o label que ira receber o TotalPedido
                    lblTotalPedidoValor.Visible = true;

                    // preenche o total do pedido
                    lblTotalPedidoValor.Text =
                        String.Format("{0:c}", 
                        ds.Tables["Itens"].Compute("Sum(total_produto)", ""));

                    // esconde o link
                    lnkPedido.Visible = false;
                    // esconde a label
                    lblClick.Visible = false;

                    // limpa a sessão do número do pedido
                    Session.Add("Nro_Pedido", "");
                } // fim do if
            }
            catch (SqlException ex)
            {
                // informa sobre o erro
                lblDetalhes.Text = "Erro ao obter itens do pedido <br>" 
                    + ex.Message;
            }
            finally
            {
                // elimina o objeto da memória
                dap.Dispose();
                // elimina o objeto da memória
                ds.Dispose();
            } // fim do try..cath..finally
        }

        protected void lnkSair_Click(object sender, EventArgs e)
        {
            // redireciona para a página de default
            Response.Redirect("Default.aspx");
        }

        protected void btnContComprando_Click(object sender, EventArgs e)
        {
            // redireciona para a página de produtos
            Response.Redirect("Produtos.aspx");
        }
    }
}
