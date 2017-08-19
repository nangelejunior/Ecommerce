<%@ Page Language="C#" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true" CodeBehind="Pedidos.aspx.cs" Inherits="Ecommerce.Pedidos" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="Panel1" runat="server">
        <table style="border: 2px solid #536D93; width:100%;">
            <tr>
                <td colspan="3" align="right">
                    <asp:LinkButton ID="lnkSair" runat="server" onclick="lnkSair_Click" 
                        TabIndex="3">Sair</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <h2>
                        Relação de Pedidos</h2>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:GridView ID="gdvPedidos" runat="server" AutoGenerateColumns="False" 
                        CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%" 
                        onrowcommand="gdvPedidos_RowCommand" TabIndex="1">
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <Columns>
                            <asp:BoundField DataField="nro_pedido" HeaderText="Nº Pedido" />
                            <asp:BoundField DataField="data_pedido" HeaderText="Data do Pedido" 
                                DataFormatString="{0:d}" />
                            <asp:BoundField DataField="total_pedido" HeaderText="Valor" 
                                DataFormatString="{0:c}" />
                            <asp:BoundField DataField="forma_pagto" HeaderText="Forma de Pagto." />
                            <asp:BoundField DataField="data_entrega" HeaderText="Data de Entrega" 
                                DataFormatString="{0:d}" />
                            <asp:ButtonField ButtonType="Button" CommandName="detalhar" Text="Detalhes">
                                <ControlStyle BorderColor="#333333" BorderStyle="Solid" BorderWidth="2px" 
                                    Font-Bold="True" Font-Names="Verdana" Font-Size="Small" ForeColor="#536D93" />
                            </asp:ButtonField>
                        </Columns>
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#999999" />
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="3" align="center">
                    <asp:Label ID="lblDetalhes" runat="server" Visible="False"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="3" align="center">
                    <asp:GridView ID="gdvItens" runat="server" AutoGenerateColumns="False" 
                        Visible="False" Width="300px">
                        <Columns>
                            <asp:BoundField DataField="qtdade" HeaderText="Qtdade" 
                                DataFormatString="{0:f0}" />
                            <asp:BoundField DataField="descricao" HeaderText="Descricão" />
                            <asp:BoundField DataField="total_produto" HeaderText="Total" 
                                DataFormatString="{0:c}" />
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td colspan="3" align="center">
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
                <td colspan="3">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="center" colspan="3">
                    <asp:Button ID="btnVoltar" runat="server" BorderColor="#333333" 
                        BorderStyle="Solid" BorderWidth="2px" Font-Bold="True" Font-Names="Verdana" 
                        Font-Size="Small" ForeColor="#536D93" onclick="btnVoltar_Click" 
                        Text="Voltar" TabIndex="2" Visible="False" />
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="center" colspan="3">
                    <asp:Label ID="lblMensagem" runat="server" ForeColor="Red" Font-Bold="True" 
                        Font-Names="Verdana" Font-Size="Small"></asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
