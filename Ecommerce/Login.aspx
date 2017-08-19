<%@ Page Language="C#" MasterPageFile="~/MasterPages/Inicial.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Ecommerce.Account.Login" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
        width: 70px;
        text-align: left;
    }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="Panel1" runat="server">
        <h2>
            Efetuar log in<asp:ScriptManager ID="smg" runat="server">
            </asp:ScriptManager>
        </h2>
        <p>
            Informe o nome de usuário e a senha!</p>
        <p>
            <table style="border: 2px solid #516B92; width: 30%;" align="center">
                <tr>
                    <td dir="ltr" colspan="3">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td dir="ltr" align="right" style="width: 20px;">
                        &nbsp;</td>
                    <td align="right" dir="ltr" style="text-align: left">
                        <asp:Label ID="lblUsuario" runat="server" Text="Usuário:"></asp:Label>
                    </td>
                    <td dir="ltr" align="left">
                        <asp:TextBox ID="txtUsuario" runat="server" Width="150px" MaxLength="15" 
                            ontextchanged="txtUsuario_TextChanged" TabIndex="1" BorderColor="#666666" 
                            BorderStyle="Solid" BorderWidth="2px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" align="center">
                        <asp:Label ID="lblMsgUsuario" runat="server" ForeColor="Red" 
                            Text="Informe o nome de usuário!" Visible="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right" style="width: 20px;">
                        &nbsp;</td>
                    <td align="right" style="text-align: left">
                        <asp:Label ID="lblSenha" runat="server" Text="Senha:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtSenha" runat="server" Width="150px" MaxLength="8" 
                            TextMode="Password" ontextchanged="txtSenha_TextChanged" TabIndex="2" 
                            BorderColor="#666666" BorderStyle="Solid" BorderWidth="2px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" align="center">
                        <asp:Label ID="lblMsgSenha" runat="server" ForeColor="Red" 
                            Text="Informe a senha!" Visible="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td align="center" colspan="3">
                        <asp:Button ID="btnEnviar" runat="server" BorderColor="#333333" 
                            BorderStyle="Solid" BorderWidth="2px" Font-Bold="True" Font-Names="Verdana" 
                            ForeColor="#536D93" onclick="btnEnviar_Click" TabIndex="3" Text="Enviar" />
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="3">
                        <asp:Label ID="lblMensagem" runat="server" Font-Bold="True" 
                            Font-Names="Verdana" Font-Size="Small" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="3">
                        &nbsp;</td>
                </tr>
            </table>
        </p>
        <p>
            &nbsp;Obs.: Campos com (*) são obrigatórios.</p>
        <p>
            Ainda não é cadastro?
            <asp:LinkButton ID="lnkClique" runat="server" onclick="lnkClique_Click">Cliquie aqui.</asp:LinkButton>
        </p>
    </asp:Panel>
</asp:Content>
