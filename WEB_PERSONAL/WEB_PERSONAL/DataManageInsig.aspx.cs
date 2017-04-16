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
    public partial class DataManageInsig : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PersonnelSystem ps = PersonnelSystem.GetPersonnelSystem(this);
            Person loginPerson = ps.LoginPerson;
            if (loginPerson.PERSON_ROLE_ID != "4")
            {
                Server.Transfer("NoPermission.aspx");
            }

            if (!IsPostBack)
            {
                BindGovAvailable();
            }

            /*if (Request.QueryString["ID"] == "Title") { BindTitle(); }
            else if (Request.QueryString["ID"] == "Gender") { BindGender(); }
            else if (Request.QueryString["ID"] == "Province") { BindProvince(); }
            else if (Request.QueryString["ID"] == "Amphur") { BindAmphur(); }
            else if (Request.QueryString["ID"] == "Tambon") { BindTambon(); }
            else if (Request.QueryString["ID"] == "Nation") { BindNation(); }
            else if (Request.QueryString["ID"] == "Campus") { BindCampus(); }
            else if (Request.QueryString["ID"] == "Faculty") { BindFaculty(); }
            else if (Request.QueryString["ID"] == "Division") { BindDivision(); }
            else if (Request.QueryString["ID"] == "WorkDivision") { BindWorkDivision(); }
            else if (Request.QueryString["ID"] == "Stafftype") { BindStafftype(); }
            else if (Request.QueryString["ID"] == "TimeContact") { BindTimeContact(); }
            else if (Request.QueryString["ID"] == "Budget") { BindBudget(); }
            else if (Request.QueryString["ID"] == "SubStafftype") { BindSubStafftype(); }
            else if (Request.QueryString["ID"] == "AdminPosition") { BindAdminPosition(); }
            else if (Request.QueryString["ID"] == "Position") { BindPosition(); }
            else if (Request.QueryString["ID"] == "WorkPos") { BindWorkPos(); }
            else if (Request.QueryString["ID"] == "TeachISCED") { BindTeachISCED(); }
            else if (Request.QueryString["ID"] == "GradLev") { BindGradLev(); }
            else if (Request.QueryString["ID"] == "GradProg") { BindGradProg(); }
            else if (Request.QueryString["ID"] == "Deform") { BindDeform(); }
            else if (Request.QueryString["ID"] == "Religion") { BindReligion(); }
            else if (Request.QueryString["ID"] == "MovementType") { BindMovementType(); }*/
        }

        private void HideAll()
        {
            Panel1.Visible = false;
            /*Panel2.Visible = false;
            Panel3.Visible = false;
            Panel4.Visible = false;
            Panel5.Visible = false;
            Panel6.Visible = false;
            Panel7.Visible = false;
            Panel8.Visible = false;
            Panel9.Visible = false;
            Panel10.Visible = false;
            Panel11.Visible = false;
            Panel12.Visible = false;
            Panel13.Visible = false;
            Panel14.Visible = false;
            Panel15.Visible = false;
            Panel16.Visible = false;
            Panel17.Visible = false;
            Panel18.Visible = false;
            Panel19.Visible = false;
            Panel20.Visible = false;
            Panel21.Visible = false;
            Panel22.Visible = false;
            Panel23.Visible = false;*/
        }

        //
        protected void BindGovAvailable()
        {
            HideAll();
            Panel1.Visible = true;
            OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING);
            OracleDataAdapter sda = new OracleDataAdapter("SELECT IA_ID, P_ID, POS_SALARY, INSIG_MIN, (SELECT INSIG_GRADE_NAME_L FROM TB_INSIG_GRADE WHERE INSIG_GRADE_ID = INSIG_MIN) INSIG_MIN_NAME, INSIG_MAX, (SELECT INSIG_GRADE_NAME_L FROM TB_INSIG_GRADE WHERE INSIG_GRADE_ID = INSIG_MAX) INSIG_MAX_NAME FROM TB_INSIG_GOV_AVAILABLE ORDER BY ABS(IA_ID) ASC", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            myRepeaterGovAvailable.DataSource = dt;
            myRepeaterGovAvailable.DataBind();
        }
        protected void ClearGovAvailable()
        {
            ddlInsertGovAvailablePositionID.SelectedIndex = 0;
            tbInsertGovAvailablePositionSalary.Text = "";
            ddlInsertGovAvailableInsigMin.SelectedIndex = 0;
            ddlInsertGovAvailableInsigMax.SelectedIndex = 0;
        }
        protected void lbuMenuGovAvailable_Click(object sender, EventArgs e)
        {
            BindGovAvailable();
        }
        protected void btnInsertGovAvailable_Click(object sender, EventArgs e)
        {
            DatabaseManager.ExecuteNonQuery("INSERT INTO TB_INSIG_GOV_AVAILABLE (IA_ID,P_ID,POS_SALARY,INSIG_MIN,INSIG_MAX) VALUES (" + ddlInsertGovAvailablePositionID.SelectedValue + ",'" + tbInsertGovAvailablePositionSalary.Text + "'," + ddlInsertGovAvailableInsigMin.SelectedValue + "," + ddlInsertGovAvailableInsigMax.SelectedValue + ")");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('เพิ่มข้อมูลเรียบร้อย')", true);
            BindGovAvailable();
            ClearGovAvailable();
        }
        protected void btnUpdateGovAvailable_Click(object sender, EventArgs e)
        {
            if (Session["DefaultIdGovAvailable"] == null)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('กรุณาเลือกรายการที่จะแก้ไขก่อน')", true);
                return;
            }
            else
            {
                DatabaseManager.ExecuteNonQuery("UPDATE TB_INSIG_GOV_AVAILABLE SET P_ID = '" + ddlInsertGovAvailablePositionID.SelectedValue + "', POS_SALARY = '" + tbInsertGovAvailablePositionSalary.Text + "', INSIG_MIN = '" + ddlInsertGovAvailableInsigMin.SelectedValue + "', INSIG_MAX = '" + ddlInsertGovAvailableInsigMax.SelectedValue + "' WHERE IA_ID = '" + Session["DefaultIdGovAvailable"].ToString() + "'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('อัพเดทข้อมูลเรียบร้อย')", true);
                BindGovAvailable();
                ClearGovAvailable();
                Session.Remove("DefaultIdGovAvailable");
            }
        }
        protected void lbuClearGovAvailable_Click(object sender, EventArgs e)
        {
            BindGovAvailable();
            ClearGovAvailable();
            Session.Remove("DefaultIdGovAvailable");
        }
        protected void OnEditGovAvailable(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            string ValueGovInsigAvailableID = (item.FindControl("HFGovAvailable") as HiddenField).Value;
            string ValuePositionID = (item.FindControl("lbPositionID") as Label).Text;
            string ValuePositionSalary = (item.FindControl("lbPositionSalary") as Label).Text;
            string ValueGovInsigAvailableMin = (item.FindControl("HFGovInsigAvailableMin") as HiddenField).Value;
            string ValueGovInsigAvailableMax = (item.FindControl("HFGovInsigAvailableMax") as HiddenField).Value;

            ddlInsertGovAvailablePositionID.SelectedValue = ValuePositionID;
            tbInsertGovAvailablePositionSalary.Text = ValuePositionSalary;
            ddlInsertGovAvailableInsigMin.SelectedValue = ValueGovInsigAvailableMin;
            ddlInsertGovAvailableInsigMax.SelectedValue = ValueGovInsigAvailableMax;

            Session["DefaultIdGovAvailable"] = ValueGovInsigAvailableID;
        }
        protected void OnDeleteGovAvailable(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            int ValueID = int.Parse((item.FindControl("HFGovAvailable") as HiddenField).Value);

            if (ValueID != 0)
            {
                DatabaseManager.ExecuteNonQuery("DELETE TB_INSIG_GOV_AVAILABLE WHERE IA_ID = '" + ValueID + "'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('ลบข้อมูลเรียบร้อย')", true);
                BindGovAvailable();
            }
        }

        //

    }
}