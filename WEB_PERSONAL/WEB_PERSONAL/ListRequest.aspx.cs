using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OracleClient;
using System.Data;
using WEB_PERSONAL.Class;

namespace WEB_PERSONAL
{
    public partial class ListRequest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PersonnelSystem ps = PersonnelSystem.GetPersonnelSystem(this);
            Person loginPerson = ps.LoginPerson;
            if (loginPerson.PERSON_ROLE_ID != "2")
            {
                Server.Transfer("NoPermission.aspx");
            }

            if (!IsPostBack)
            {
                BindData();
            }
        }

        protected void BindData()
        {
            OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING);
            OracleDataAdapter sda = new OracleDataAdapter("SELECT (SELECT PS_FIRSTNAME || ' ' || PS_LASTNAME FROM PS_PERSON WHERE PS_PERSON.PS_CITIZEN_ID = TB_REQUEST.CITIZEN_ID) NAME, (SELECT (SELECT STAFFTYPE_NAME FROM TB_STAFFTYPE WHERE PS_PERSON.PS_STAFFTYPE_ID = TB_STAFFTYPE.STAFFTYPE_ID) FROM PS_PERSON WHERE PS_PERSON.PS_CITIZEN_ID = TB_REQUEST.CITIZEN_ID) STAFF_NAME, R_ID FROM TB_REQUEST WHERE R_STATUS_ID = 0 ORDER BY R_ID ASC", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            myRepeater.DataSource = dt;
            myRepeater.DataBind();
        }

        protected void myRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Edit" && e.CommandArgument.ToString() != "")
            {
                LinkButton lbu = (LinkButton)e.Item.FindControl("lbuEdit");
                string value = lbu.CommandArgument;
                Response.Redirect("Request.aspx?id=" + value);
            }
        }

    }
}