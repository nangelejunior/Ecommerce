<%@ Page Language="C#" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true" CodeBehind="ProdutoDetalhes.aspx.cs" Inherits="Ecommerce.ProdutoDetalhes" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="Panel1" runat="server">
        <table style="border: 2px solid #536D93; width:100%;">
            <tr>
                <td align="right" colspan="2">
                    <asp:LinkButton ID="lnkSair" runat="server" onclick="lnkSair_Click" 
                        TabIndex="2">Sair</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblDescricao" runat="server" Font-Bold="True" 
                        Font-Names="Verdana" Font-Size="Medium"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 200px; height: 200px">
                    <asp:Image ID="imgProduto" runat="server" Height="193px" Width="193px" />
                </td>
                <td style="width: 600px; text-align: left">
                    <asp:Label ID="lblPreco" runat="server" Font-Bold="True" Font-Names="Verdana" 
                        Font-Size="Medium" ForeColor="Red"></asp:Label>
                    <br />
                    <br />
                    Parcelamento em até 12x sem juros.</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td style="text-align: center">
                    <asp:ImageButton ID="imgAdicionar" runat="server" 
                        ImageUrl="~/Images/cart_add.png" onclick="imgAdicionar_Click" 
                        TabIndex="1" />
                    <br />
                    Adicionar no Carrinho</td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="2">
                    DETALHES DO PRODUTO</td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblDetalhes" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <asp:Label ID="lblMensagem" runat="server" Font-Bold="True" 
                        Font-Names="Verdana" Font-Size="Small" ForeColor="Red"></asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
