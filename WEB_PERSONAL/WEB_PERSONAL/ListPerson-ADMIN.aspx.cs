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
            if (!IsPostBack)
            {
                BindData();
            }
        }

        protected void BindData()
        {
            OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING);
            OracleDataAdapter sda = new OracleDataAdapter("SELECT PS_CITIZEN_ID, PS_FN_TH || ' ' || PS_LN_TH NAME, (SELECT STAFFTYPE_NAME FROM TB_STAFFTYPE WHERE STAFFTYPE_ID = PS_STAFFTYPE_ID) STAFFTYPE_NAME,(SELECT CAMPUS_NAME FROM TB_CAMPUS WHERE CAMPUS_ID = PS_CAMPUS_ID) CAMPUS_NAME, (SELECT FACULTY_NAME FROM TB_FACULTY WHERE FACULTY_ID = PS_FACULTY_ID) FACULTY_NAME, (SELECT DIVISION_NAME FROM TB_DIVISION WHERE DIVISION_ID = PS_DIVISION_ID) DIVISION_NAME, (SELECT WORK_NAME FROM TB_WORK_DIVISION WHERE WORK_ID = PS_WORK_DIVISION_ID) WORK_NAME FROM PS_PERSON", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            myRepeater.DataSource = dt;
            myRepeater.DataBind();
        }

        protected void myRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            /*if (e.CommandName == "Edit" && e.CommandArgument.ToString() != "")
            {
                LinkButton lbu = (LinkButton)e.Item.FindControl("lbuEdit");
                string value = lbu.CommandArgument;
                Response.Redirect("#.aspx?id=" + value);
            }*/
        }

    }
}