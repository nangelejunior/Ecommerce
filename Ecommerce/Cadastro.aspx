<%@ Page Language="C#" MasterPageFile="~/MasterPages/Inicial.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="Ecommerce.Cadastro" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="Panel1" runat="server">
        <table style="border: 2px solid #516B92; width: 100%;">
            <tr>
                <td align="right" colspan="4">
                    <asp:LinkButton ID="lnkSair" runat="server" onclick="lnkSair_Click" 
                        TabIndex="17">Sair</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <h2>
                        Cadastro de Cliente<asp:ScriptManager ID="smg" runat="server">
                        </asp:ScriptManager>
                    </h2>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td align="center">
                    &nbsp;</td>
                <td style="margin-left: 40px">
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="4">
                    <p>
                        Preencha o formulário abaixo para criar uma conta!</p>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td align="center">
                    &nbsp;</td>
                <td style="margin-left: 40px">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: left">
                    <asp:Label ID="lblNome" runat="server" Text="* Nome Completo:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtNome" runat="server" Width="300px" MaxLength="50" 
                        TabIndex="1" BorderColor="#666666" BorderStyle="Solid" BorderWidth="2px" 
                        ToolTip="Exemplo: José da Silva."></asp:TextBox>
                </td>
                <td style="text-align: left">
                    <asp:Label ID="lblTelefone" runat="server" Text="* Telefone:"></asp:Label>
                </td>
                <td style="margin-left: 40px">
                    <asp:TextBox ID="txtTelefone" runat="server" Width="300px" MaxLength="11" 
                        TabIndex="9" BorderColor="#666666" BorderStyle="Solid" BorderWidth="2px" 
                        ToolTip="Somente números. Exemplo: 01633335555."></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    <asp:Label ID="lblSexo" runat="server" Text="* Sexo:"></asp:Label>
                </td>
                <td>
                    <asp:RadioButtonList ID="rblSexo" runat="server" 
                        RepeatDirection="Horizontal" TabIndex="2">
                        <asp:ListItem Value="M">Masculino</asp:ListItem>
                        <asp:ListItem Value="F">Feminino</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td style="text-align: left">
                    <asp:Label ID="lblCelular" runat="server" Text="Celular:"></asp:Label>
                </td>
                <td style="margin-left: 40px">
                    <asp:TextBox ID="txtCelular" runat="server" Width="300px" MaxLength="11" 
                        TabIndex="10" BorderColor="#666666" BorderStyle="Solid" BorderWidth="2px" 
                        ToolTip="Somente números. Exemplo: 01699991111."></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    <asp:Label ID="lblDtNascimento" runat="server" Text="* Data de Nascimento:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtDtNascimento" runat="server" Width="300px" MaxLength="10" 
                        TabIndex="3" AutoPostBack="True" 
                        ontextchanged="txtDtNascimento_TextChanged" BorderColor="#666666" 
                        BorderStyle="Solid" BorderWidth="2px" ToolTip="Exemplo: 01/01/1988."></asp:TextBox>
                </td>
                <td style="text-align: left">
                    <asp:Label ID="lblEmail" runat="server" Text="* E-mail:"></asp:Label>
                </td>
                <td style="margin-left: 40px">
                    <asp:TextBox ID="txtEmail" runat="server" Width="300px" MaxLength="128" 
                        TabIndex="11" AutoPostBack="True" ontextchanged="txtEmail_TextChanged" 
                        BorderColor="#666666" BorderStyle="Solid" BorderWidth="2px" 
                        ToolTip="Exemplo: josedasilva@bol.com.br"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    <asp:Label ID="lblCPF" runat="server" Text="* CPF:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCPF" runat="server" Width="300px" MaxLength="11" 
                        TabIndex="4" AutoPostBack="True" ontextchanged="txtCPF_TextChanged" 
                        BorderColor="#666666" BorderStyle="Solid" BorderWidth="2px" 
                        ToolTip="Somente números. Exemplo: 12345678910."></asp:TextBox>
                </td>
                <td style="text-align: left">
                    &nbsp;</td>
                <td style="margin-left: 40px">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: left">
                    <asp:Label ID="lblEndereco" runat="server" Text="* Endereço:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtEndereco" runat="server" Width="300px" MaxLength="50" 
                        TabIndex="5" BorderColor="#666666" BorderStyle="Solid" BorderWidth="2px"></asp:TextBox>
                </td>
                <td align="right">
                    &nbsp;</td>
                <td align="left">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: left">
                    <asp:Label ID="lblCidade" runat="server" Text="* Cidade:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCidade" runat="server" Width="300px" MaxLength="30" 
                        TabIndex="6" BorderColor="#666666" BorderStyle="Solid" BorderWidth="2px"></asp:TextBox>
                </td>
                <td style="text-align: left">
                    <asp:Label ID="lblUsuario" runat="server" Text="* Usuário:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtUsuario" runat="server" Width="300px" MaxLength="15" 
                        TabIndex="12" BorderColor="#666666" BorderStyle="Solid" BorderWidth="2px" 
                        ToolTip="Exemplo: josedasilva"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    <asp:Label ID="lblEstado" runat="server" Text="* Estado:"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlEstado" runat="server" Width="300px" TabIndex="7">
                        <asp:ListItem Value="Nenhum" Selected="True">--Selecione o Estado--</asp:ListItem>
                        <asp:ListItem Value="GO">Goiás</asp:ListItem>
                        <asp:ListItem Value="MA">Maranhão</asp:ListItem>
                        <asp:ListItem Value="MT">Mato Grosso</asp:ListItem>
                        <asp:ListItem Value="MS">Mato Grosso do Sul</asp:ListItem>
                        <asp:ListItem Value="MG">Minas Gerais</asp:ListItem>
                        <asp:ListItem Value="PA">Pará</asp:ListItem>
                        <asp:ListItem Value="PB">Paraíba</asp:ListItem>
                        <asp:ListItem Value="PR">Paraná</asp:ListItem>
                        <asp:ListItem Value="PE">Pernambuco</asp:ListItem>
                        <asp:ListItem Value="PI">Piauí</asp:ListItem>
                        <asp:ListItem Value="RJ">Rio de Janeiro</asp:ListItem>
                        <asp:ListItem Value="RN">Rio Grande do Norte</asp:ListItem>
                        <asp:ListItem Value="RS">Rio Grande do Sul</asp:ListItem>
                        <asp:ListItem Value="RO">Rondônia</asp:ListItem>
                        <asp:ListItem Value="RR">Roraima</asp:ListItem>
                        <asp:ListItem Value="SC">Santa Catarina</asp:ListItem>
                        <asp:ListItem Value="SP">São Paulo</asp:ListItem>
                        <asp:ListItem Value="SE">Sergipe</asp:ListItem>
                        <asp:ListItem Value="TO">Tocantins</asp:ListItem>
                        <asp:ListItem Value="AC">Acre</asp:ListItem>
                        <asp:ListItem Value="AL">Alagoas</asp:ListItem>
                        <asp:ListItem Value="AP">Amapá</asp:ListItem>
                        <asp:ListItem Value="AM">Amazonas</asp:ListItem>
                        <asp:ListItem Value="BA">Bahia</asp:ListItem>
                        <asp:ListItem Value="CE">Ceará</asp:ListItem>
                        <asp:ListItem Value="DF">Distrito Federeal</asp:ListItem>
                        <asp:ListItem Value="ES">Espírito Santo</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="text-align: left">
                    <asp:Label ID="lblSenha" runat="server" Text="* Senha:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtSenha" runat="server" Width="300px" MaxLength="8" 
                        TabIndex="13" TextMode="Password" BorderColor="#666666" 
                        BorderStyle="Solid" BorderWidth="2px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    <asp:Label ID="lblCEP" runat="server" Text="* CEP:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCEP" runat="server" Width="300px" MaxLength="8" 
                        TabIndex="8" BorderColor="#666666" BorderStyle="Solid" BorderWidth="2px" 
                        ToolTip="Somente números. Exemplo: 15900000."></asp:TextBox>
                </td>
                <td style="text-align: left">
                    <asp:Label ID="lblConfirma" runat="server" Text="* Confirma Senha:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtConfirma" runat="server" Width="300px" MaxLength="8" 
                        TabIndex="14" TextMode="Password" BorderColor="#666666" 
                        BorderStyle="Solid" BorderWidth="2px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="center" colspan="4">
                    <table style="width: 50%;">
                        <tr>
                            <td>
                                <asp:Button ID="btnEnviar" runat="server" BorderColor="#333333" 
                                    BorderStyle="Solid" BorderWidth="2px" Font-Bold="True" Font-Names="Verdana" 
                                    ForeColor="#516B92" onclick="btnEnviar_Click" TabIndex="15" Text="Enviar" 
                                    Width="75px" />
                            </td>
                            <td>
                                <asp:Button ID="btnLimpar" runat="server" BorderColor="#333333" 
                                    BorderStyle="Solid" BorderWidth="2px" Font-Bold="True" Font-Names="Verdana" 
                                    ForeColor="Red" onclick="btn_Limpar_Click" Text="Limpar" Width="75px" 
                                    TabIndex="16" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="right" colspan="4">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="right" colspan="4" style="text-align: center">
                    <asp:Label ID="lblMensagem" runat="server" ForeColor="Red" Font-Bold="True" 
                        Font-Italic="False" Font-Names="Verdana" Font-Size="Small"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right" colspan="4" style="text-align: center">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="right" colspan="4" style="text-align: center">
                    Obs.: Campos com (*) são obrigatórios!</td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
