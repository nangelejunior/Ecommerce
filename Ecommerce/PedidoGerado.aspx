<%@ Page Language="C#" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true" CodeBehind="PedidoGerado.aspx.cs" Inherits="Ecommerce.PedidoGerado" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="Panel1" runat="server">
        <table style="border: 2px solid #536D93; width:100%;">
            <tr>
                <td colspan="4" align="right">
                    <asp:LinkButton ID="lnkSair" runat="server" onclick="lnkSair_Click" 
                        TabIndex="3">Sair</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <h2>
                        Pedido N°
                        <asp:Label ID="lblPedido" runat="server"></asp:Label>
                        &nbsp;gerado com sucesso!</h2>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td colspan="2">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="4" style="text-align: center">
                    <asp:LinkButton ID="lnkPedido" runat="server" onclick="lnkPedido_Click" 
                        TabIndex="1">Clique 
                    aqui</asp:LinkButton>
                    &nbsp;<asp:Label ID="lblClick" runat="server" Text="para ver os detalhes do pedido."></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="4" style="text-align: center">
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="4" align="center">
                    <asp:Label ID="lblDetalhes" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="4" style="text-align: center">
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="4" align="center">
                    <asp:GridView ID="gdvItens" runat="server" AutoGenerateColumns="False" 
                        Width="300px">
                        <Columns>
                            <asp:BoundField DataField="qtdade" HeaderText="Qtdade" 
                                DataFormatString="{0:f0}" />
                            <asp:BoundField DataField="descricao" HeaderText="Descrição" />
                            <asp:BoundField DataField="total_produto" DataFormatString="{0:c}" 
                                HeaderText="Total" />
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td align="right" colspan="2">
                    &nbsp;</td>
                <td align="left" colspan="2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="4" align="center">
                    <table style="width: 300px;">
                        <tr>
                            <td style="width: 150px">
                                <asp:Label ID="lblTotalPedido" runat="server" Text="Total do Pedido:" 
                                    Visible="False"></asp:Label>
                            </td>
                            <td style="width: 150px">
                                <asp:Label ID="lblTotalPedidoValor" runat="server" Visible="False"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="center" colspan="4">
                    <asp:Button ID="btnContComprando" runat="server" BorderColor="#333333" 
                        BorderStyle="Solid" BorderWidth="2px" Font-Bold="True" Font-Names="Verdana" 
                        Font-Size="Small" ForeColor="#536D93" onclick="btnContComprando_Click" 
                        Text="&lt; Continuar comprando" TabIndex="2" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
