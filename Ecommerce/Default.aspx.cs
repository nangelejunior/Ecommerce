using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace Ecommerce
{
    public partial class Default : System.Web.UI.Page
    {
        // instância da classe Banco
        private Banco bd = new Banco();

        protected void Page_Load(object sender, EventArgs e)
        {
            // cria uma sessão
            Session.Add("Codigo_Usuario", "");

            // se ao abrir o banco retornar true
            if (bd.AbrirBanco() == true)
            {
                // redireciona para a página de produtos
                Response.Redirect("Produtos.aspx");
            } // fim do if
        }
    }
}
