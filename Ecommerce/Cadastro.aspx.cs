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
    public partial class Cadastro : System.Web.UI.Page
    {
        // variável de conexão conexão com o bando de dados
        private SqlConnection con;
        // instância da classe Banco
        private Banco bd = new Banco();
        // instância da classe Utilitario
        private Utilitario utl = new Utilitario();
        // variável de comandos SQL
        private SqlCommand cmd;
        // variável que retem o resultado dos comandos SQL
        private SqlDataReader dr;
        // instância da data de nascimento
        private DateTime nasc = new DateTime();

        protected void Page_Load(object sender, EventArgs e)
        {
            // foca o campo nome
            smg.FindControl("txtNome").Focus();

            // se a página estiver sendo acessada pela primeira vez
            if (!Page.IsPostBack)
            {
                // se a sessão não estiver vazia
                if (Session["Codigo_Usuario"].ToString() != "")
                {
                    // se não, sair
                    lnkSair.Text = "Sair";

                    // variável local que recebe a sintaxe SQL
                    string sql = "";

                    // comando SQL de seleção
                    sql = "SELECT * FROM Cliente WHERE Codigo = " +
                        Session["Codigo_Usuario"].ToString();

                    // tenta recuperar os dados do cliente
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
                                // preenche o campo nome
                                txtNome.Text = dr["nome_completo"].ToString();
                                // preenche o campo CPF
                                txtCPF.Text = dr["cpf"].ToString();
                                // seleciona o sexo
                                rblSexo.SelectedValue = dr["sexo"].ToString();
                                // preenche o campo data de nascimento
                                txtDtNascimento.Text = DateTime.Parse(
                                    dr["data_nascimento"].ToString()).ToString("dd/MM/yyy");
                                // preenche o campo endereço
                                txtEndereco.Text = dr["endereco"].ToString();
                                // preenche o campo cidade
                                txtCidade.Text = dr["cidade"].ToString();
                                // seleciona o estado
                                ddlEstado.SelectedValue = dr["estado"].ToString();
                                // preenche o campo CEP
                                txtCEP.Text = dr["cep"].ToString();
                                // preenche o campo telefone
                                txtTelefone.Text = dr["telefone"].ToString();
                                // preenche o campo celular
                                txtCelular.Text = dr["celular"].ToString();
                                // preenche o campo e-mail
                                txtEmail.Text = dr["email"].ToString();
                                // preenche o campo usuário
                                txtUsuario.Text = dr["usuario"].ToString();
                                // preenche o campo senha
                                txtSenha.Attributes.Add("value", dr["senha"].ToString());
                                // preenche o campo confirma a senha
                                txtConfirma.Attributes.Add("value", dr["senha"].ToString());

                                // desabilita o campo usuário
                                txtUsuario.Enabled = false;
                                // desabilita o botão limpar
                                btnLimpar.Enabled = false;
                            } // fim do if
                        }
                        else
                        {
                            // se não, exibe o erro
                            lblMensagem.Text = "Erro ao obter os dados do cliente!";
                        } // fim do if..else
                    }
                    catch (SqlException ex)
                    {
                        // exibe o erro
                        lblMensagem.Text = "Erro ao obter os dados do cliente! <br>" +
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
                else
                {
                    // receve entrar
                    lnkSair.Text = "Entrar";
                }
            } // fim do if
        }

        protected void txtDtNascimento_TextChanged(object sender, EventArgs e)
        {
            // se o campo data de nascimento não contiver uma data válida
            if (!DateTime.TryParse(txtDtNascimento.Text, out nasc))
            {
                // informa sobre o erro
                lblMensagem.Text = "A data de nascimento informada é inválida!";
                // limpa o campo data de nascimento
                txtDtNascimento.Text = "";
                // foca o campo data de nascimento
                smg.FindControl("txtDtNascimento").Focus();
            }
            else
            {
                // limpa o label de mensagem
                lblMensagem.Text = "";
                // foca o campo data de nascimento
                smg.FindControl("txtCPF").Focus();
            } // fim do if..else
        }

        protected void txtCPF_TextChanged(object sender, EventArgs e)
        {
            // se o número de cpf for inválido
            if (utl.validarCPF(txtCPF.Text) == false)
            {
                // informa o usuário
                lblMensagem.Text = "O número de CPF informado é inválido!";
                // limpa o campo CPF
                txtCPF.Text = "";
                // foca o campo CPF
                smg.FindControl("txtCPF").Focus();
            }
            else
            {
                // limpa a label de mensagem
                lblMensagem.Text = "";
                // foca o campo Endereço
                smg.FindControl("txtEndereco").Focus();
            } // fim do if..else
        }

        protected void txtEmail_TextChanged(object sender, EventArgs e)
        {
            // se o e-mail for inválido
            if (utl.validarEmail(txtEmail.Text) == false)
            {
                // informa o usuário
                lblMensagem.Text = "O e-mail informado é inválido!";
                // limpa o campo e-mail
                txtEmail.Text = "";
                // foca o campo e-mail
                smg.FindControl("txtEmail").Focus();
            }
            else
            {
                // limpa a label de mensagem
                lblMensagem.Text = "";
                // foca o campo usuário
                smg.FindControl("txtUsuario").Focus();
            } // fim do if..else
        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            // variável local que recebe a sintaxe SQL
            string sql = "";

            // variável que recebe false se o nome de usuário existe
            // ou false se não existe
            bool usuarioExistente = false;

            // variável que recebe false se o cliente não é cadastrado
            // ou true se já é cadastrado
            bool clienteExistente = false;

            // se o campo nome estiver vazio
            if (txtNome.Text == String.Empty)
            {
                // informa o usuário
                lblMensagem.Text = "Por favor, informe seu nome completo!";
                // foca o campo nome
                smg.FindControl("txtNome").Focus();
                // encerra o processamento
                return;
            } // fim do if

            // se o campo CPF estiver vazio
            if (txtCPF.Text == String.Empty)
            {
                // informa o usuário
                lblMensagem.Text = "Por favor, informe seu CPF!";
                // foca o campo CPF
                smg.FindControl("txtCPF").Focus();
                // encerra o processamento
                return;
            } 
            else if (utl.validarCPF(txtCPF.Text) == false)
            {
                // se não, se o número de cpf for inválido
                // informa o usuário
                lblMensagem.Text = "O número de CPF informado é inválido!";
                // foca o campo CPF
                smg.FindControl("txtCPF").Focus();
                // encerra o processamento
                return;
            } // fim do if..else

            // se os campos masculino e feminino não estiverem marcados
            if ((!rblSexo.Items[0].Selected) && (!rblSexo.Items[1].Selected))
            {
                // informa o usuário
                lblMensagem.Text = "Por favor, informe seu sexo!";
                // foca o campo sexo
                smg.FindControl("rblSexo").Focus();
                // encerra o processamento
                return;
            } // fim do if

            // se o campo data de nascimento estiver vazio
            if (txtDtNascimento.Text == String.Empty)
            {
                // informa o usuário
                lblMensagem.Text = "Por favor, informe sua data de nascimento!";
                // foca o campo data de nascimenrto
                smg.FindControl("txtDtNascimento").Focus();
                // encerra o processamento
                return;
            }
            else if (!DateTime.TryParse(txtDtNascimento.Text, out nasc))
            {
                // se não, se não contiver uma data válida
                // informa o usuário
                lblMensagem.Text = "Data de nascimento inválida!";
                // foca o campo data de nascimenrto
                smg.FindControl("txtDtNascimento").Focus();
                // encerra o processamento
                return;
            } // fim do if..else

            // se o campo endereço estiver vazio
            if (txtEndereco.Text == String.Empty)
            {
                // informa o usuário
                lblMensagem.Text = "Por favor, informe seu endereço!";
                // foca o campo endereço
                smg.FindControl("txtEndereco").Focus();
                // encerra o processamento
                return;
            }

            // se o campo cidade estiver vazio
            if (txtCidade.Text == String.Empty)
            {
                // informa o usuário
                lblMensagem.Text = "Por favor, informe seu Cidade!";
                // foca o campo cidade
                smg.FindControl("txtCidade").Focus();
                // encerra o processamento
                return;
            } // fim do if

            // se o usuário não selecionou neenhum estado
            if (ddlEstado.SelectedValue == "Nenhum")
            {
                // informa o usuário
                lblMensagem.Text = "Por favor, informe seu Estado!";
                // foca o campo estado
                smg.FindControl("ddlEstado").Focus();
                // encerra o processamento
                return;
            } // fim do if

            // se o campo CEP estiver vazio
            if (txtCEP.Text == String.Empty)
            {
                // informa o usuário
                lblMensagem.Text = "Por favor, informe seu CEP!";
                // foca o campo CEP
                smg.FindControl("txtCEP").Focus();
                // encerra o processamento
                return;
            } // fim do if

            // se o campo telefone estiver vazio
            if (txtTelefone.Text == String.Empty)
            {
                // informa o usuário
                lblMensagem.Text = "Por favor, informe seu Telefone!";
                // foca o campo telefone
                smg.FindControl("txtTelefone").Focus();
                // encerra o processamento
                return;
            } // fim do if

            // se o campo email estiver vazio
            if (txtEmail.Text == String.Empty)
            {
                // informa o usuário
                lblMensagem.Text = "Por favor, informe seu E-mail!";
                // foca o campo email
                smg.FindControl("txtEmail").Focus();
                // encerra o processamento
                return;
            } // fim do if
            else if (utl.validarEmail(txtEmail.Text) == false)
            {
                // se não, se o e-mail for inválido
                // informa o usuário
                lblMensagem.Text = "O e-mail informado é inválido!";
                // foca o campo e-mail
                smg.FindControl("txtEmail").Focus();
                // encerra o processamento
                return;
            }

            // se o campo usuário estiver vazio
            if (txtUsuario.Text == String.Empty)
            {
                // informa o usuário
                lblMensagem.Text = "Por favor, informe seu Usuário!";
                // foca o campo usuário
                smg.FindControl("txtUsuario").Focus();
                // encerra o processamento
                return;
            } // fim do if

            // se o campo senha estiver vazio
            if (txtSenha.Text == String.Empty)
            {
                // informa o usuário
                lblMensagem.Text = "Por favor, informe sua Senha!";
                // foca o campo senha
                smg.FindControl("txtSenha").Focus();
                // encerra o processamento
                return;
            } // fim do if

            // se o campo confirmação de senha estiver vazio
            if (txtConfirma.Text == String.Empty)
            {
                // informa o usuário
                lblMensagem.Text = "Por favor, informe a senha de confirmação!";
                // foca o campo confirma a senha
                smg.FindControl("txtConfirma").Focus();
                // encerra o processamento
                return;
            } // fim do if

            // se a sessão estiver vazia
            // significa que o usuário está se cadastrando
            if (Session["Codigo_Usuario"].ToString() == "")
            {
                // comando SQL de seleção
                sql = "SELECT usuario FROM Cliente WHERE usuario = '"
                    + txtUsuario.Text + "'";

                // tenta recuperar o nome de usuário
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
                        // significa que o usuário já existe
                        // usuarioExiste recebe true
                        usuarioExistente = true;
                    } // fim do if
                }
                catch (SqlException ex)
                {
                    // informa sobre o erro
                    lblMensagem.Text = "Erro ao pesquisar usuário! <br>" +
                        ex.Message;
                }
                finally
                {
                    // elimina o objeto da memoria
                    dr.Dispose();
                    // elimina o objeto da memória
                    cmd.Dispose();
                } // fim do try..cath..finally

                // comando SQL de seleção
                sql = "SELECT cpf FROM Cliente WHERE cpf = '"
                    + txtCPF.Text + "'";

                // tenta recuperar o cpf do cliente
                try
                {
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
                        // significa que o cliente já existe
                        // clienteExistente recebe true
                        clienteExistente = true;
                    } // fim do if
                }
                catch (SqlException ex)
                {
                    // informa sobre o erro
                    lblMensagem.Text = "Erro ao pesquisar CPF! <br>" +
                        ex.Message;
                }
                finally
                {
                    // elimina o objeto da memoria
                    dr.Dispose();
                    // elimina o objeto da memória
                    cmd.Dispose();
                } // fim do try..cath..finally

                // se o usuário já existe no cadastro de clientes
                if (usuarioExistente == true)
                {
                    // informa que o usuário existe
                    lblMensagem.Text = "Este nome de usuário já existe!";
                    // limpa o campo usuário
                    txtUsuario.Text = "";
                    // foca o campo usuário
                    smg.FindControl("txtUsuario").Focus();
                }
                else if (clienteExistente == true)
                {
                    // se não, se cpf já dastrado
                    lblMensagem.Text = "CPF já cadastrado em nossa base de dados!" +
                        "<br>" + "Possivelmente você já esteja cadastrado em nosso site.";
                }
                else if (txtSenha.Text.ToLower() != txtConfirma.Text.ToLower())
                {
                    // se a senha e a confirmação de senha forem diferentes
                    // informa o usuário
                    lblMensagem.Text = "A senha nao confere, por favor digite novamente!";
                    // limpa o campo senha
                    txtSenha.Text = "";
                    // limpa o campo confirmação de senha
                    txtConfirma.Text = "";
                    // foca o campo senha
                    smg.FindControl("txtSenha").Focus();
                }
                else
                {
                    // se não, o cliente esta pronto para ser cadastrado
                    // comando SQL de inserção
                    sql = "INSERT INTO Cliente VALUES(" +
                        "'" + DateTime.Now.ToString("MM/dd/yyyy") + "', " +
                        "'" + txtNome.Text + "', " +
                        "'" + txtCPF.Text + "', " +
                        "'" + rblSexo.SelectedValue + "', " +
                        "'" + DateTime.Parse(txtDtNascimento.Text).ToString("MM/dd/yyyy") + "', " +
                        "'" + txtEndereco.Text + "', " +
                        "'" + txtCidade.Text + "', " +
                        "'" + ddlEstado.SelectedValue + "', " +
                        "'" + txtCEP.Text + "', " +
                        "'" + txtTelefone.Text + "', " +
                        "'" + txtCelular.Text + "', " +
                        "'" + txtEmail.Text + "', " +
                        "'" + txtUsuario.Text.ToLower() + "', " +
                        "'" + txtSenha.Text.ToLower() + "')";

                    // se conseguiu cadastrar o cliente
                    if (utl.executeSQL(sql) == true)
                    {
                        // comando SQL de seleção
                        sql = "SELECT codigo FROM Cliente WHERE Usuario = '" +
                            txtUsuario.Text + "' AND Senha = '" + txtSenha.Text + "'";

                        // tenta recuperar o código do cliente
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
                            lblMensagem.Text = "Erro ao recuperar o código do cliente! <br>" +
                                ex.Message;
                        }
                        finally
                        {
                            // elimina o objeto da memória
                            cmd.Dispose();
                            // elimina o objeto da mamória
                            dr.Dispose();
                        } // fim do try..cath..finally  
                    }
                    else
                    {
                        // informa sobre o erro
                        lblMensagem.Text = "Erro ao cadastrar o cliente!";
                    } // fim do if..else
                } // fim do if..else
            }
            else
            {
                // se não, se a sessão possuir o código do usuário
                // significa que o usuário pode alterar seu cadastro
                // se a senha e a confirmação de senha forem diferentes
                if (txtSenha.Text.ToLower() != txtConfirma.Text.ToLower())
                {
                    // informa o usuário
                    lblMensagem.Text = "A senha nao confere, por favor digite novamente!";
                    // limpa o campo senha
                    txtSenha.Text = "";
                    // limpa o campo confirmação de senha
                    txtConfirma.Text = "";
                    // foca o campo senha
                    smg.FindControl("txtSenha").Focus();
                }
                else
                {
                    // comando SQL de alteração
                    sql = "UPDATE Cliente SET " +
                        "nome_completo = '" + txtNome.Text + "', " +
                        "cpf = '" + txtCPF.Text + "', " +
                        "sexo = '" + rblSexo.SelectedValue + "', " +
                        "data_nascimento = '" + DateTime.Parse(txtDtNascimento.Text).ToString("MM/dd/yyyy") + "', " +
                        "endereco = '" + txtEndereco.Text + "', " +
                        "cidade = '" + txtCidade.Text + "', " +
                        "estado = '" + ddlEstado.SelectedValue + "', " +
                        "cep = '" + txtCEP.Text + "', " +
                        "telefone = '" + txtTelefone.Text + "', " +
                        "celular = '" + txtCelular.Text + "', " +
                        "email = '" + txtEmail.Text + "', " +
                        "senha = '" + txtSenha.Text.ToLower() + "' " +
                        "WHERE codigo = " + Session["Codigo_Usuario"].ToString();

                    // se conseguiu alterar o cadastro
                    if (utl.executeSQL(sql) == true)
                    {
                        // informa o usuário
                        lblMensagem.Text = "Alterações realizadas com sucesso!";
                    }
                    else
                    {
                        // informa sobre o erro
                        lblMensagem.Text = "Erro ao alterar o cadastro de cliente!";
                    } // fim do if..else
                } // fim do if..else
            } // fim do if..else

        }

        protected void btn_Limpar_Click(object sender, EventArgs e)
        {
            // limpa o campo nome
            txtNome.Text = "";
            // limpa o campo CPF
            txtCPF.Text = "";
            // limpa o campo sexo
            rblSexo.ClearSelection();
            // limpa o campo data de nascimento
            txtDtNascimento.Text = "";
            // limpa o campo endereço
            txtEndereco.Text = "";
            // limpa o campo cidade
            txtCidade.Text = "";
            // limpa o campo estado
            ddlEstado.SelectedIndex = 0;
            // limpa o campo CEP
            txtCEP.Text = "";
            // limpa o campo telefone
            txtTelefone.Text = "";
            // limpa o campo celular
            txtCelular.Text = "";
            // limpa o campo e-mail
            txtEmail.Text = "";
            // limpa o campo usuário
            txtUsuario.Text = "";
            // limpa o campo senha
            txtSenha.Text = "";
            // limpa o campo confirma senha
            txtConfirma.Text = "";
            // limpa o label de mensagem 
            lblMensagem.Text = "";
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
