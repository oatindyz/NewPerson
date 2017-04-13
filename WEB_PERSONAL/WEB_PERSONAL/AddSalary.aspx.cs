using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using WEB_PERSONAL.Class;
using System.Data.OracleClient;

namespace WEB_PERSONAL
{
    public partial class AddSalary : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PersonnelSystem ps = PersonnelSystem.GetPersonnelSystem(this);
            Person loginPerson = ps.LoginPerson;
            if (loginPerson.PERSON_ROLE_ID != "2")
            {
                Server.Transfer("NoPermission.aspx");
            }

            if (Request.QueryString["id"] == null)
            {
                Response.Redirect("ListPerson-ADMIN.aspx");
            }

            if (!IsPostBack)
            {
                BindSalary();
            }
        }

        //
        protected void BindSalary()
        {
            OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING);
            OracleDataAdapter sda = new OracleDataAdapter("SELECT SALARY_ID, SALARY, POSITION_SALARY, RESULT1, PERCENT_SALARY1, RESULT2, PERCENT_SALARY2, DO_DATE FROM PS_SALARY WHERE CITIZEN_ID = '" + MyCrypto.GetDecryptedQueryString(Request.QueryString["id"].ToString()) + "' ORDER BY DO_DATE ASC", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            myRepeaterSalary.DataSource = dt;
            myRepeaterSalary.DataBind();
        }
        protected void ClearSalary()
        {
            tbSalary.Text = "";
            tbPositionSalary.Text = "";
            tbResult1.Text = "";
            tbPercentSalary1.Text = "";
            tbResult2.Text = "";
            tbPercentSalary2.Text = "";
        }
        protected void lbuMenuSalary_Click(object sender, EventArgs e)
        {
            BindSalary();
        }
        protected void btnInsertSalary_Click(object sender, EventArgs e)
        {
            OracleConnection.ClearAllPools();
            using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
            {
                con.Open();
                using (OracleCommand com = new OracleCommand("INSERT INTO PS_SALARY (CITIZEN_ID,SALARY,POSITION_SALARY,RESULT1,PERCENT_SALARY1,RESULT2,PERCENT_SALARY2,DO_DATE) VALUES (:CITIZEN_ID,:SALARY,:POSITION_SALARY,:RESULT1,:PERCENT_SALARY1,:RESULT2,:PERCENT_SALARY2,:DO_DATE)", con))
                {
                    com.Parameters.Add(new OracleParameter("CITIZEN_ID", MyCrypto.GetDecryptedQueryString(Request.QueryString["id"].ToString())));
                    com.Parameters.Add(new OracleParameter("SALARY", tbSalary.Text));
                    com.Parameters.Add(new OracleParameter("POSITION_SALARY", tbPositionSalary.Text));
                    com.Parameters.Add(new OracleParameter("RESULT1", tbResult1.Text));
                    com.Parameters.Add(new OracleParameter("PERCENT_SALARY1", tbPercentSalary1.Text));
                    com.Parameters.Add(new OracleParameter("RESULT2", tbResult2.Text));
                    com.Parameters.Add(new OracleParameter("PERCENT_SALARY2", tbPercentSalary2.Text));
                    com.Parameters.Add(new OracleParameter("DO_DATE", Util.ToDateTimeOracle(tbInsertDateSalary.Text)));
                    com.ExecuteNonQuery();
                }
            }

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('เพิ่มข้อมูลเรียบร้อย')", true);
            BindSalary();
            ClearSalary();
        }
        protected void btnUpdateSalary_Click(object sender, EventArgs e)
        {
            string ValueSalary = tbSalary.Text;
            string ValuePositionSalary = tbPositionSalary.Text;
            string ValueResult1 = tbResult1.Text;
            string ValuePercentSalary1 = tbPercentSalary1.Text;
            string ValueResult2 = tbResult2.Text;
            string ValuePercentSalary2 = tbPercentSalary2.Text;
            DateTime ValueDate = Util.ToDateTimeOracle(tbInsertDateSalary.Text);

            if (Session["DefaultIdSalary"] == null)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('กรุณาเลือกรายการที่จะแก้ไขก่อน')", true);
                return;
            }

            if (ValueSalary != "")
            {
                OracleConnection.ClearAllPools();
                using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
                {
                    con.Open();
                    using (OracleCommand com = new OracleCommand("UPDATE PS_SALARY SET CITIZEN_ID = :CITIZEN_ID, SALARY = :SALARY, POSITION_SALARY = :POSITION_SALARY, RESULT1 = :RESULT1 ,PERCENT_SALARY1 = :PERCENT_SALARY1 ,RESULT2 = :RESULT2 ,PERCENT_SALARY2 = :PERCENT_SALARY2, DO_DATE = :DO_DATE WHERE SALARY_ID = :SALARY_ID", con))
                    {
                        com.Parameters.Add(new OracleParameter("CITIZEN_ID", MyCrypto.GetDecryptedQueryString(Request.QueryString["id"].ToString())));
                        com.Parameters.Add(new OracleParameter("SALARY", ValueSalary));
                        com.Parameters.Add(new OracleParameter("POSITION_SALARY", ValuePositionSalary));
                        com.Parameters.Add(new OracleParameter("RESULT1", ValueResult1));
                        com.Parameters.Add(new OracleParameter("PERCENT_SALARY1", ValuePercentSalary1));
                        com.Parameters.Add(new OracleParameter("RESULT2", ValueResult2));
                        com.Parameters.Add(new OracleParameter("PERCENT_SALARY2", ValuePercentSalary2));
                        com.Parameters.Add(new OracleParameter("DO_DATE", ValueDate));
                        com.Parameters.Add(new OracleParameter("SALARY_ID", Session["DefaultIdSalary"].ToString()));
                        com.ExecuteNonQuery();
                    }
                }

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('อัพเดทข้อมูลเรียบร้อย')", true);
                BindSalary();
                ClearSalary();
                Session.Remove("DefaultIdSalary");
            }
        }
        protected void lbuClearSalary_Click(object sender, EventArgs e)
        {
            BindSalary();
            ClearSalary();
            Session.Remove("DefaultIdSalary");
        }
        protected void OnEditSalary(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            string ValueSalaryID = (item.FindControl("HFSalary_ID") as HiddenField).Value;
            string ValueSalary = (item.FindControl("lbSalary") as Label).Text;
            string ValuePositionSalary = (item.FindControl("lbPositionSalary") as Label).Text;
            string ValueResult1 = (item.FindControl("lbResult1") as Label).Text;
            string ValuePercentSalary1 = (item.FindControl("lbPercentSalary1") as Label).Text;
            string ValueResult2 = (item.FindControl("lbResult2") as Label).Text;
            string ValuePercentSalary2 = (item.FindControl("lbPercentSalary2") as Label).Text;
            string ValueGetDate = (item.FindControl("lbDoDate") as Label).Text;

            tbSalary.Text = ValueSalary;
            tbPositionSalary.Text = ValuePositionSalary;
            tbResult1.Text = ValueResult1;
            tbPercentSalary1.Text = ValuePercentSalary1;
            tbResult2.Text = ValueResult2;
            tbPercentSalary2.Text = ValuePercentSalary2;
            tbInsertDateSalary.Text = ValueGetDate;

            Session["DefaultIdSalary"] = ValueSalaryID;
        }
        protected void OnDeleteSalary(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            string ValueSalaryID = (item.FindControl("HFSalary_ID") as HiddenField).Value;

            if (ValueSalaryID != "")
            {
                DatabaseManager.ExecuteNonQuery("DELETE PS_SALARY WHERE SALARY_ID = '" + ValueSalaryID + "'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('ลบข้อมูลเรียบร้อย')", true);
                BindSalary();
            }
        }
    }
}