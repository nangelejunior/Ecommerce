<%@ Page Language="C#" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true" CodeBehind="Carrinho.aspx.cs" Inherits="Ecommerce.Carrinho" Title="Untitled Page" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="Panel1" runat="server">
        <table style="border: thin solid #536D93; width:100%;">
            <tr>
                <td colspan="2" align="right">
                    <asp:LinkButton ID="lnkSair" runat="server" onclick="lnkSair_Click" 
                        TabIndex="9">Sair</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <h2>
                        Itens Presentes no Carrinho de Compras<asp:ScriptManager ID="smg" 
                            runat="server">
                        </asp:ScriptManager>
                    </h2>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <table style="width: 400px;">
                        <tr>
                            <td>
                                <asp:Label ID="lblQtdade" runat="server" ForeColor="Red" 
                                    Text="Informe aqui a quantidade:" Visible="False" Font-Bold="True" 
                                    Font-Names="Verdana" Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtQtdade" runat="server" Visible="False" Width="100px" 
                                    BorderColor="#666666" BorderStyle="Solid" BorderWidth="2px" TabIndex="1"></asp:TextBox>
                            </td>
                            <td style="width: 80px">
                                <asp:Button ID="btnOK" runat="server" onclick="btnOK_Click" Text="OK" 
                                    Visible="False" BorderColor="#333333" BorderStyle="Solid" 
                                    BorderWidth="2px" Font-Bold="True" Font-Names="Verdana" 
                                    ForeColor="#536D93" TabIndex="2" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:GridView ID="gdvCarrinho" runat="server" AutoGenerateColumns="False" 
                        CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%" 
                        onrowcommand="gdvCarrinho_RowCommand" TabIndex="3">
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <Columns>
                            <asp:BoundField DataField="item" HeaderText="Item" />
                            <asp:ImageField DataImageUrlField="foto" DataImageUrlFormatString="Images/{0}" 
                                HeaderText="Foto">
                                <ControlStyle Width="50px" />
                            </asp:ImageField>
                            <asp:BoundField DataField="descricao" HeaderText="Descrição" />
                            <asp:BoundField DataField="qtdade" HeaderText="Quantidade" 
                                DataFormatString="{0:f0}" />
                            <asp:ButtonField ButtonType="Button" CommandName="alterar" Text="Alterar" >
                                <ControlStyle BorderColor="#333333" BorderStyle="Solid" BorderWidth="2px" 
                                    Font-Bold="True" Font-Names="Verdana" ForeColor="#536D93" />
                            </asp:ButtonField>
                            <asp:ButtonField ButtonType="Button" CommandName="excluir" Text="Excluir" >
                                <ControlStyle BorderColor="#333333" BorderStyle="Solid" BorderWidth="2px" 
                                    Font-Bold="True" Font-Names="Verdana" ForeColor="Red" />
                            </asp:ButtonField>
                            <asp:BoundField DataField="total_produto" HeaderText="Total" 
                                DataFormatString="{0:c}" />
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
                <td align="right" colspan="2">
                    <table style="width: 300px;">
                        <tr>
                            <td style="width: 120px">
                                <asp:Label ID="lblTotalPedido" runat="server" Text="Total do Pedido:"></asp:Label>
                            </td>
                            <td style="width: 180px">
                                <asp:Label ID="lblTotalPedidoValor" runat="server" style="text-align: left"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="2">
                    <h2>
                        Forma de Pagamento</h2>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <asp:RadioButtonList ID="rblFormaPagto" runat="server" 
                        RepeatDirection="Horizontal" AutoPostBack="True" 
                        onselectedindexchanged="rblFormaPagto_SelectedIndexChanged" TabIndex="4">
                        <asp:ListItem>Boleto</asp:ListItem>
                        <asp:ListItem>Credito</asp:ListItem>
                        <asp:ListItem>Debito</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <table style="width: 200px;">
                        <tr>
                            <td align="center" rowspan="2">
                                <asp:RadioButtonList ID="rblCartao" runat="server" AutoPostBack="True" 
                                    onselectedindexchanged="rblCartao_SelectedIndexChanged" Visible="False" 
                                    TabIndex="5">
                                    <asp:ListItem Value="Visa  ">Visa</asp:ListItem>
                                    <asp:ListItem>Master Card</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td align="center">
                                <asp:Image ID="imgVisa" runat="server" ImageUrl="~/Images/visa.png" 
                                    Visible="False" />
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Image ID="imgMCard" runat="server" ImageUrl="~/Images/mastercard.png" 
                                    Visible="False" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="left" colspan="2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblParcela" runat="server" Text="Número de Parcelas:" 
                        Visible="False"></asp:Label>
                </td>
                <td align="left">
                    <asp:DropDownList ID="ddlParcela" runat="server" Visible="False" TabIndex="6">
                        <asp:ListItem>1x</asp:ListItem>
                        <asp:ListItem>2x</asp:ListItem>
                        <asp:ListItem>3x</asp:ListItem>
                        <asp:ListItem>4x</asp:ListItem>
                        <asp:ListItem>5x</asp:ListItem>
                        <asp:ListItem>6x</asp:ListItem>
                        <asp:ListItem>7x</asp:ListItem>
                        <asp:ListItem>8x</asp:ListItem>
                        <asp:ListItem>9x</asp:ListItem>
                        <asp:ListItem>10x</asp:ListItem>
                        <asp:ListItem>11x</asp:ListItem>
                        <asp:ListItem>12x</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <asp:Label ID="lblMensagem" runat="server" ForeColor="Red" Font-Bold="True" 
                        Font-Names="Verdana" Font-Size="Small"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="left">
                    <asp:Button ID="btnComprando" runat="server" onclick="btnComprando_Click" 
                        Text="&lt; Continuar comprando" BorderColor="#333333" BorderStyle="Solid" 
                        BorderWidth="2px" Font-Bold="True" Font-Names="Verdana" 
                        ForeColor="#536D93" TabIndex="8" />
                </td>
                <td align="right">
                    <asp:Button ID="btnFinalizar" runat="server" onclick="btnFinalizar_Click" 
                        Text="Finalizar compra &gt;" BorderColor="#333333" BorderStyle="Solid" 
                        BorderWidth="2px" Font-Bold="True" Font-Names="Verdana" 
                        ForeColor="#536D93" TabIndex="7" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
