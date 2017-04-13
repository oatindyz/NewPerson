using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OracleClient;
using System.Data;
using System.Text;
using WEB_PERSONAL.Class;

namespace WEB_PERSONAL
{
    public partial class ListPerson_ADMIN : System.Web.UI.Page
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
            OracleDataAdapter sda = new OracleDataAdapter("SELECT PS_ID,PS_CITIZEN_ID, PS_FIRSTNAME || ' ' || PS_LASTNAME NAME, (SELECT STAFFTYPE_NAME FROM TB_STAFFTYPE WHERE STAFFTYPE_ID = PS_STAFFTYPE_ID) STAFFTYPE_NAME, (SELECT CAMPUS_NAME FROM TB_CAMPUS WHERE TB_CAMPUS.CAMPUS_ID = PS_PERSON.PS_CAMPUS_ID) CAMPUS_NAME FROM PS_PERSON", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            myRepeater.DataSource = dt;
            myRepeater.DataBind();
        }

        protected void myRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Edituser" && e.CommandArgument.ToString() != "")
            {
                LinkButton lbu = (LinkButton)e.Item.FindControl("lbuEdituser");
                string value = lbu.CommandArgument;
                Response.Redirect("Edituser.aspx?id=" + value);
            }
            if (e.CommandName == "ManagePosition" && e.CommandArgument.ToString() != "")
            {
                LinkButton lbu = (LinkButton)e.Item.FindControl("lbuManagePosition");
                string value = lbu.CommandArgument;
                Response.Redirect("AddPosition.aspx?id=" + value);
            }
            if (e.CommandName == "ManageSalary" && e.CommandArgument.ToString() != "")
            {
                LinkButton lbu = (LinkButton)e.Item.FindControl("lbuManageSalary");
                string value = lbu.CommandArgument;
                Response.Redirect("AddSalary.aspx?id=" + value);
            }
        }

    }
}