using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEB_PERSONAL {
    public partial class NoPermission : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            /*if (Request.QueryString["id"] == "1")
            {
                NoPermissionz.Visible = true;
            }
            else if (Request.QueryString["id"] == "2")
            {
                NoPermissionz.Visible = true;
            }else
            {
                NoPermissionz.Visible = true;
            }*/
            if (!IsPostBack)
            {
                
            }
        }
    }
}