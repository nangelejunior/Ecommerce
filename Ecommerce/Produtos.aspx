<%@ Page Language="C#" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true" CodeBehind="Produtos.aspx.cs" Inherits="Ecommerce.Produtos" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="Panel1" runat="server">
        <table style="border: 2px solid #536D93; width: 100%">
            <tr>
                <td colspan="2">
                    <table style="width:100%;">
                        <tr>
                            <td style="width: 80px">
                                <asp:Label ID="lblCategoria" runat="server" Text="Categoria &gt;"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblCategoriaValor" runat="server"></asp:Label>
                            </td>
                            <td align="right" style="width: 100px">
                                <asp:LinkButton ID="lnkSair" runat="server" onclick="lnkSair_Click" 
                                    TabIndex="10">Sair</asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <h2>
                        Relação de Produtos</h2>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <table style="border: 2px solid #000000; width: 600px; background-color: #E6E6E6;">
                        <tr>
                            <td style="width: 50px">
                                <asp:Image ID="imgLupa" runat="server" ImageUrl="~/Images/zoom.png" />
                            </td>
                            <td style="width: 100px">
                                <asp:Label ID="lblPesquisar" runat="server" Text="Pesquisar:"></asp:Label>
                            </td>
                            <td style="width: 350px">
                                <asp:TextBox ID="txtPesquisar" runat="server" Width="300px" 
                                    BorderColor="#666666" BorderStyle="Solid" BorderWidth="2px" TabIndex="1"></asp:TextBox>
                            </td>
                            <td style="width: 100px">
                                <asp:Button ID="btnPesquisar" runat="server" onclick="btnPesquisar_Click" 
                                    Text="Pesquisar" BorderColor="#333333" BorderStyle="Solid" 
                                    BorderWidth="2px" Font-Bold="True" Font-Names="Verdana" 
                                    ForeColor="#536D93" TabIndex="2" />
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
                <td colspan="2" align="right">
                    <table style="width: 400px;">
                        <tr>
                            <td style="width: 150px">
                                <asp:Label ID="lblOrdem" runat="server" Text="Ordenar por:"></asp:Label>
                            </td>
                            <td style="width: 250px">
                                <asp:DropDownList ID="ddlOrdem" runat="server" AutoPostBack="True" 
                                    onselectedindexchanged="ddlOrdem_SelectedIndexChanged" Width="200px" 
                                    TabIndex="3">
                                    <asp:ListItem Selected="True" Value="p.data_cadastramento">Mais recente</asp:ListItem>
                                    <asp:ListItem Value="venda">Mais vendidos</asp:ListItem>
                                    <asp:ListItem Value="p.codigo">Código</asp:ListItem>
                                    <asp:ListItem Value="descricao">Descrição</asp:ListItem>
                                    <asp:ListItem Value="p.val_venda">Preço</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="width: 150px; vertical-align: top;">
                    <table style="border: 2px solid #48556A; width: 150px; ">
                        <tr>
                            <td style="background-color: #536D93; font-size: medium; color: #FFFFFF; text-align: center; font-weight: 700;">
                                Categorias</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:LinkButton ID="lnkAcessorios" runat="server" onclick="lnkAcessorios_Click" 
                                    TabIndex="5">Acessórios</asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:LinkButton ID="lnkCelulares" runat="server" onclick="lnkCelulares_Click" 
                                    TabIndex="6">Celulares</asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:LinkButton ID="lnkDesktops" runat="server" onclick="lnkDesktops_Click" 
                                    TabIndex="7">Desktops</asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:LinkButton ID="lnkNotebooks" runat="server" onclick="lnkNotebooks_Click" 
                                    TabIndex="8">Notebooks</asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:LinkButton ID="lnkTodos" runat="server" onclick="lnkTodos_Click" 
                                    TabIndex="9">Todos</asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <asp:GridView ID="gdvProdutos" runat="server" AutoGenerateColumns="False" 
                        CellPadding="4" ForeColor="#333333" GridLines="None" 
                        onrowcommand="gdvProdutos_RowCommand" Width="750px" TabIndex="4">
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <Columns>
                            <asp:BoundField DataField="codigo" HeaderText="Código" />
                            <asp:ImageField DataImageUrlField="foto" DataImageUrlFormatString="Images/{0}" 
                                HeaderText="Foto">
                                <ControlStyle Width="50px" />
                            </asp:ImageField>
                            <asp:ButtonField CommandName="detalhar" DataTextField="descricao" 
                                HeaderText="Descrição" />
                            <asp:BoundField DataField="val_venda" DataFormatString="{0:c}" 
                                HeaderText="Preço" />
                            <asp:ButtonField ButtonType="Button" CommandName="incluir" Text="Incluir" >
                                <ControlStyle BorderColor="#666666" BorderStyle="Solid" BorderWidth="2px" 
                                    Font-Bold="True" Font-Names="Verdana" ForeColor="#536D93" />
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
                <td colspan="2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <asp:Label ID="lblMensagem" runat="server" ForeColor="Red" Font-Bold="True" 
                        Font-Names="Verdana" Font-Size="Small"></asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>
    </asp:Content>
