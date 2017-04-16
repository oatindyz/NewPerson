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

            if (Request.QueryString["ID"] == "GovAvailable") { BindGovAvailable(); }
            else if (Request.QueryString["ID"] == "GovHighSalaryCon") { BindGovHighSalaryCon(); }
            else if (Request.QueryString["ID"] == "GovInsigYearCon") { BindGovInsigYearCon(); }
            else if (Request.QueryString["ID"] == "Amphur") { BindGovPosYearCon(); }
            /*else if (Request.QueryString["ID"] == "Tambon") { BindTambon(); }
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
            Panel2.Visible = false;
            Panel3.Visible = false;
            Panel4.Visible = false;
            /*Panel5.Visible = false;
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
            OracleDataAdapter sda = new OracleDataAdapter("SELECT IA_ID, P_ID, (SELECT P_NAME FROM TB_POSITION WHERE TB_POSITION.P_ID = TB_INSIG_GOV_AVAILABLE.P_ID) P_NAME, POS_SALARY, INSIG_MIN, (SELECT INSIG_GRADE_NAME_L FROM TB_INSIG_GRADE WHERE INSIG_GRADE_ID = INSIG_MIN) INSIG_MIN_NAME, INSIG_MAX, (SELECT INSIG_GRADE_NAME_L FROM TB_INSIG_GRADE WHERE INSIG_GRADE_ID = INSIG_MAX) INSIG_MAX_NAME FROM TB_INSIG_GOV_AVAILABLE ORDER BY ABS(IA_ID) ASC", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            myRepeaterGovAvailable.DataSource = dt;
            myRepeaterGovAvailable.DataBind();
            DatabaseManager.BindDropDown(ddlInsertGovAvailablePositionID, "SELECT * FROM TB_POSITION ORDER BY ABS(P_ID) ASC", "P_NAME", "P_ID", "--กรุณาเลือก--");
            DatabaseManager.BindDropDown(ddlInsertGovAvailableInsigMin, "SELECT * FROM TB_INSIG_GRADE ORDER BY ABS(INSIG_GRADE_ID) ASC", "INSIG_GRADE_NAME_L", "INSIG_GRADE_ID", "--กรุณาเลือก--");
            DatabaseManager.BindDropDown(ddlInsertGovAvailableInsigMax, "SELECT * FROM TB_INSIG_GRADE ORDER BY ABS(INSIG_GRADE_ID) ASC", "INSIG_GRADE_NAME_L", "INSIG_GRADE_ID", "--กรุณาเลือก--");
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
            DatabaseManager.ExecuteNonQuery("INSERT INTO TB_INSIG_GOV_AVAILABLE (IA_ID,P_ID,POS_SALARY,INSIG_MIN,INSIG_MAX) VALUES (TB_INSIG_GOV_AVAILABLE_SEQ.NEXTVAL," + ddlInsertGovAvailablePositionID.SelectedValue + ",'" + tbInsertGovAvailablePositionSalary.Text + "'," + ddlInsertGovAvailableInsigMin.SelectedValue + "," + ddlInsertGovAvailableInsigMax.SelectedValue + ")");
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
                DatabaseManager.ExecuteNonQuery("UPDATE TB_INSIG_GOV_AVAILABLE SET P_ID = " + ddlInsertGovAvailablePositionID.SelectedValue + ", POS_SALARY = '" + tbInsertGovAvailablePositionSalary.Text + "', INSIG_MIN = " + ddlInsertGovAvailableInsigMin.SelectedValue + ", INSIG_MAX = " + ddlInsertGovAvailableInsigMax.SelectedValue + " WHERE IA_ID = '" + Session["DefaultIdGovAvailable"].ToString() + "'");
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
            string ValueGovInsigAvailablePositionID = (item.FindControl("HFGovInsigPositionID") as HiddenField).Value;
            string ValuePositionSalary = (item.FindControl("lbPositionSalary") as Label).Text;
            string ValueGovInsigAvailableMin = (item.FindControl("HFGovInsigAvailableMin") as HiddenField).Value;
            string ValueGovInsigAvailableMax = (item.FindControl("HFGovInsigAvailableMax") as HiddenField).Value;

            ddlInsertGovAvailablePositionID.SelectedValue = ValueGovInsigAvailablePositionID;
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
        protected void BindGovHighSalaryCon()
        {
            HideAll();
            Panel2.Visible = true;
            OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING);
            OracleDataAdapter sda = new OracleDataAdapter("SELECT IGHSC_ID, P_ID, (SELECT P_NAME FROM TB_POSITION WHERE TB_POSITION.P_ID = TB_INSIG_GOV_HIGH_SALARY_CON.P_ID)P_NAME, INSIG_TARGET, (SELECT INSIG_GRADE_NAME_L FROM TB_INSIG_GRADE WHERE TB_INSIG_GRADE.INSIG_GRADE_ID = TB_INSIG_GOV_HIGH_SALARY_CON.INSIG_TARGET)INSIG_TARGET_NAME, P_ID_USE, (SELECT P_NAME FROM TB_POSITION WHERE TB_POSITION.P_ID = TB_INSIG_GOV_HIGH_SALARY_CON.P_ID_USE)P_ID_USE_NAME FROM TB_INSIG_GOV_HIGH_SALARY_CON ORDER BY ABS(IGHSC_ID) ASC", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            myRepeaterGovHighSalaryCon.DataSource = dt;
            myRepeaterGovHighSalaryCon.DataBind();
            DatabaseManager.BindDropDown(ddlInsertGovHighSalaryConP_ID, "SELECT * FROM TB_POSITION ORDER BY ABS(P_ID) ASC", "P_NAME", "P_ID", "--กรุณาเลือก--");
            DatabaseManager.BindDropDown(ddlInsertGovHighSalaryConINSIG_TARGET, "SELECT * FROM TB_INSIG_GRADE ORDER BY ABS(INSIG_GRADE_ID) ASC", "INSIG_GRADE_NAME_L", "INSIG_GRADE_ID", "--กรุณาเลือก--");
            DatabaseManager.BindDropDown(ddlInsertGovHighSalaryConP_ID_USE, "SELECT * FROM TB_POSITION ORDER BY ABS(P_ID) ASC", "P_NAME", "P_ID", "--กรุณาเลือก--");
        }
        protected void ClearGovHighSalaryCon()
        {
            ddlInsertGovHighSalaryConP_ID.SelectedIndex = 0;
            ddlInsertGovHighSalaryConINSIG_TARGET.SelectedIndex = 0;
            ddlInsertGovHighSalaryConP_ID_USE.SelectedIndex = 0;
        }
        protected void lbuMenuGovHighSalaryCon_Click(object sender, EventArgs e)
        {
            BindGovHighSalaryCon();
        }
        protected void btnInsertGovHighSalaryCon_Click(object sender, EventArgs e)
        {
            DatabaseManager.ExecuteNonQuery("INSERT INTO TB_INSIG_GOV_HIGH_SALARY_CON (IGHSC_ID,P_ID,INSIG_TARGET,P_ID_USE) VALUES (TB_INSIG_GOV_HIGH_SALARY_SEQ.NEXTVAL," + ddlInsertGovHighSalaryConP_ID.SelectedValue + "," + ddlInsertGovHighSalaryConINSIG_TARGET.SelectedValue + "," + ddlInsertGovHighSalaryConP_ID_USE.SelectedValue + ")");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('เพิ่มข้อมูลเรียบร้อย')", true);
            BindGovHighSalaryCon();
            ClearGovHighSalaryCon();
        }
        protected void btnUpdateGovHighSalaryCon_Click(object sender, EventArgs e)
        {
            if (Session["DefaultIdGovHighSalaryCon"] == null)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('กรุณาเลือกรายการที่จะแก้ไขก่อน')", true);
                return;
            }
            else
            {
                DatabaseManager.ExecuteNonQuery("UPDATE TB_INSIG_GOV_HIGH_SALARY_CON SET P_ID = " + ddlInsertGovHighSalaryConP_ID.SelectedValue + ", INSIG_TARGET = " + ddlInsertGovHighSalaryConINSIG_TARGET.SelectedValue + ", P_ID_USE = " + ddlInsertGovHighSalaryConP_ID_USE.SelectedValue + " WHERE IGHSC_ID = '" + Session["DefaultIdGovHighSalaryCon"].ToString() + "'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('อัพเดทข้อมูลเรียบร้อย')", true);
                BindGovHighSalaryCon();
                ClearGovHighSalaryCon();
                Session.Remove("DefaultIdGovHighSalaryCon");
            }
        }
        protected void lbuClearGovHighSalaryCon_Click(object sender, EventArgs e)
        {
            BindGovHighSalaryCon();
            ClearGovHighSalaryCon();
            Session.Remove("DefaultIdGovHighSalaryCon");
        }
        protected void OnEditGovHighSalaryCon(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            string ValueGovHighSalaryConID = (item.FindControl("HFGovHighSalaryConID") as HiddenField).Value;
            string ValueGovHighSalaryConP_ID = (item.FindControl("HFGovHighSalaryConP_ID") as HiddenField).Value;
            string ValueGovHighSalaryConINSIG_TARGET = (item.FindControl("HFGovHighSalaryConINSIG_TARGET") as HiddenField).Value;
            string ValueGovHighSalaryConP_ID_USE = (item.FindControl("HFGovHighSalaryConP_ID_USE") as HiddenField).Value;

            ddlInsertGovHighSalaryConP_ID.SelectedValue = ValueGovHighSalaryConP_ID;
            ddlInsertGovHighSalaryConINSIG_TARGET.SelectedValue = ValueGovHighSalaryConINSIG_TARGET;
            ddlInsertGovHighSalaryConP_ID_USE.SelectedValue = ValueGovHighSalaryConP_ID_USE;

            Session["DefaultIdGovHighSalaryCon"] = ValueGovHighSalaryConID;
        }
        protected void OnDeleteGovHighSalaryCon(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            int ValueID = int.Parse((item.FindControl("HFGovHighSalaryConID") as HiddenField).Value);

            if (ValueID != 0)
            {
                DatabaseManager.ExecuteNonQuery("DELETE TB_INSIG_GOV_HIGH_SALARY_CON WHERE IGHSC_ID = '" + ValueID + "'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('ลบข้อมูลเรียบร้อย')", true);
                BindGovHighSalaryCon();
            }
        }

        //
        protected void BindGovInsigYearCon()
        {
            HideAll();
            Panel3.Visible = true;
            OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING);
            OracleDataAdapter sda = new OracleDataAdapter("SELECT IGIYC_ID, P_ID, (SELECT P_NAME FROM TB_POSITION WHERE TB_POSITION.P_ID = TB_INSIG_GOV_INSIG_YEAR_CON.P_ID)P_NAME, SALARY, INSIG_TARGET, (SELECT INSIG_GRADE_NAME_L FROM TB_INSIG_GRADE WHERE TB_INSIG_GRADE.INSIG_GRADE_ID = TB_INSIG_GOV_INSIG_YEAR_CON.INSIG_TARGET)INSIG_TARGET_NAME, INSIG_USE, (SELECT INSIG_GRADE_NAME_L FROM TB_INSIG_GRADE WHERE TB_INSIG_GRADE.INSIG_GRADE_ID = TB_INSIG_GOV_INSIG_YEAR_CON.INSIG_USE)INSIG_USE_NAME, INSIG_YEAR FROM TB_INSIG_GOV_INSIG_YEAR_CON ORDER BY ABS(IGIYC_ID) ASC", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            myRepeaterGovInsigYearCon.DataSource = dt;
            myRepeaterGovInsigYearCon.DataBind();
            DatabaseManager.BindDropDown(ddlInsertGovInsigYearConP_ID, "SELECT * FROM TB_POSITION ORDER BY ABS(P_ID) ASC", "P_NAME", "P_ID", "--กรุณาเลือก--");
            DatabaseManager.BindDropDown(ddlInsertGovInsigYearConINSIG_TARGET, "SELECT * FROM TB_INSIG_GRADE ORDER BY ABS(INSIG_GRADE_ID) ASC", "INSIG_GRADE_NAME_L", "INSIG_GRADE_ID", "--กรุณาเลือก--");
            DatabaseManager.BindDropDown(ddlInsertGovInsigYearConINSIG_USE, "SELECT * FROM TB_INSIG_GRADE ORDER BY ABS(INSIG_GRADE_ID) ASC", "INSIG_GRADE_NAME_L", "INSIG_GRADE_ID", "--กรุณาเลือก--");
        }
        protected void ClearGovInsigYearCon()
        {
            ddlInsertGovInsigYearConP_ID.SelectedIndex = 0;
            tbInsertGovInsigYearConSALARY.Text = "";
            ddlInsertGovInsigYearConINSIG_TARGET.SelectedIndex = 0;
            ddlInsertGovInsigYearConINSIG_USE.SelectedIndex = 0;
            tbInsertGovInsigYearConINSIG_YEAR.Text = "";
        }
        protected void lbuMenuGovInsigYearCon_Click(object sender, EventArgs e)
        {
            BindGovInsigYearCon();
        }
        protected void btnInsertGovInsigYearCon_Click(object sender, EventArgs e)
        {
            DatabaseManager.ExecuteNonQuery("INSERT INTO TB_INSIG_GOV_INSIG_YEAR_CON (IGIYC_ID,P_ID,SALARY,INSIG_TARGET,INSIG_USE,INSIG_YEAR) VALUES (TB_INSIG_GOV_INSIG_YEAR_SEQ.NEXTVAL," + ddlInsertGovInsigYearConP_ID.SelectedValue + ",'" + tbInsertGovInsigYearConSALARY.Text + "'," + ddlInsertGovInsigYearConINSIG_TARGET.SelectedValue + "," + ddlInsertGovInsigYearConINSIG_USE.SelectedValue + ",'" + tbInsertGovInsigYearConINSIG_YEAR.Text + "')");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('เพิ่มข้อมูลเรียบร้อย')", true);
            BindGovInsigYearCon();
            ClearGovInsigYearCon();
        }
        protected void btnUpdateGovInsigYearCon_Click(object sender, EventArgs e)
        {
            if (Session["DefaultIdGovInsigYearCon"] == null)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('กรุณาเลือกรายการที่จะแก้ไขก่อน')", true);
                return;
            }
            else
            {
                DatabaseManager.ExecuteNonQuery("UPDATE TB_INSIG_GOV_INSIG_YEAR_CON SET P_ID = " + ddlInsertGovInsigYearConP_ID.SelectedValue + ", SALARY = '" + tbInsertGovInsigYearConSALARY.Text + "', INSIG_TARGET = " + ddlInsertGovInsigYearConINSIG_TARGET.SelectedValue + ", INSIG_USE = " + ddlInsertGovInsigYearConINSIG_USE.SelectedValue + ", INSIG_YEAR = '" + tbInsertGovInsigYearConINSIG_YEAR.Text + "' WHERE IGIYC_ID = '" + Session["DefaultIdGovInsigYearCon"].ToString() + "'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('อัพเดทข้อมูลเรียบร้อย')", true);
                BindGovInsigYearCon();
                ClearGovInsigYearCon();
                Session.Remove("DefaultIdGovInsigYearCon");
            }
        }
        protected void lbuClearGovInsigYearCon_Click(object sender, EventArgs e)
        {
            BindGovInsigYearCon();
            ClearGovInsigYearCon();
            Session.Remove("DefaultIdGovInsigYearCon");
        }
        protected void OnEditGovInsigYearCon(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            string ValueHFGovInsigYearConID = (item.FindControl("HFGovInsigYearConID") as HiddenField).Value;
            string ValueHFGovInsigYearConP_ID = (item.FindControl("HFGovInsigYearConP_ID") as HiddenField).Value;
            string ValuelbPositionSalary = (item.FindControl("lbPositionSalary") as Label).Text;
            string ValueHFGovInsigYearConINSIG_TARGET = (item.FindControl("HFGovInsigYearConINSIG_TARGET") as HiddenField).Value;
            string ValueHFGovInsigYearConINSIG_USE = (item.FindControl("HFGovInsigYearConINSIG_USE") as HiddenField).Value;
            string ValuelbInsigYear = (item.FindControl("lbInsigYear") as Label).Text;

            ddlInsertGovInsigYearConP_ID.SelectedValue = ValueHFGovInsigYearConP_ID;
            tbInsertGovInsigYearConSALARY.Text = ValuelbPositionSalary;
            ddlInsertGovInsigYearConINSIG_TARGET.SelectedValue = ValueHFGovInsigYearConINSIG_TARGET;
            ddlInsertGovInsigYearConINSIG_USE.SelectedValue = ValueHFGovInsigYearConINSIG_USE;
            tbInsertGovInsigYearConINSIG_YEAR.Text = ValuelbInsigYear;

            Session["DefaultIdGovInsigYearCon"] = ValueHFGovInsigYearConID;
        }
        protected void OnDeleteGovInsigYearCon(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            int ValueID = int.Parse((item.FindControl("HFGovInsigYearConID") as HiddenField).Value);

            if (ValueID != 0)
            {
                DatabaseManager.ExecuteNonQuery("DELETE TB_INSIG_GOV_INSIG_YEAR_CON WHERE IGIYC_ID = '" + ValueID + "'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('ลบข้อมูลเรียบร้อย')", true);
                BindGovInsigYearCon();
            }
        }

        //
        protected void BindGovPosYearCon()
        {
            HideAll();
            Panel4.Visible = true;
            OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING);
            OracleDataAdapter sda = new OracleDataAdapter("SELECT IGPYC_ID, P_ID, (SELECT P_NAME FROM TB_POSITION WHERE TB_POSITION.P_ID = TB_INSIG_GOV_POS_YEAR_CON.P_ID)P_NAME, INSIG_TARGET, (SELECT INSIG_GRADE_NAME_L FROM TB_INSIG_GRADE WHERE TB_INSIG_GRADE.INSIG_GRADE_ID = TB_INSIG_GOV_POS_YEAR_CON.INSIG_TARGET)INSIG_TARGET_NAME, P_ID_USE, (SELECT P_NAME FROM TB_POSITION WHERE TB_POSITION.P_ID = TB_INSIG_GOV_POS_YEAR_CON.P_ID_USE)P_ID_USE_NAME, POS_YEAR  FROM TB_INSIG_GOV_POS_YEAR_CON ORDER BY ABS(IGPYC_ID) ASC", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            myRepeaterGovPosYearCon.DataSource = dt;
            myRepeaterGovPosYearCon.DataBind();
            DatabaseManager.BindDropDown(ddlInsertGovPosYearConP_ID, "SELECT * FROM TB_POSITION ORDER BY ABS(P_ID) ASC", "P_NAME", "P_ID", "--กรุณาเลือก--");
            DatabaseManager.BindDropDown(ddlInsertGovPosYearConINSIG_TARGET, "SELECT * FROM TB_INSIG_GRADE ORDER BY ABS(INSIG_GRADE_ID) ASC", "INSIG_GRADE_NAME_L", "INSIG_GRADE_ID", "--กรุณาเลือก--");
            DatabaseManager.BindDropDown(ddlInsertGovPosYearConP_ID_USE, "SELECT * FROM TB_POSITION ORDER BY ABS(P_ID) ASC", "P_NAME", "P_ID", "--กรุณาเลือก--");
        }
        protected void ClearGovPosYearCon()
        {
            ddlInsertGovPosYearConP_ID.SelectedIndex = 0;
            ddlInsertGovPosYearConINSIG_TARGET.SelectedIndex = 0;
            ddlInsertGovPosYearConP_ID_USE.SelectedIndex = 0;
            tbInsertGovPosYearConPOS_YEAR.Text = "";
        }
        protected void lbuMenuGovPosYearCon_Click(object sender, EventArgs e)
        {
            BindGovPosYearCon();
        }
        protected void btnInsertGovPosYearCon_Click(object sender, EventArgs e)
        {
            DatabaseManager.ExecuteNonQuery("INSERT INTO TB_INSIG_GOV_POS_YEAR_CON (IGPYC_ID,P_ID,INSIG_TARGET,P_ID_USE,POS_YEAR) VALUES (TB_INSIG_GOV_POS_YEAR_CON_SEQ.NEXTVAL," + ddlInsertGovPosYearConP_ID.SelectedValue + "," + ddlInsertGovPosYearConINSIG_TARGET.SelectedValue + "," + ddlInsertGovPosYearConP_ID_USE.SelectedValue + ",'" + tbInsertGovPosYearConPOS_YEAR.Text + "')");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('เพิ่มข้อมูลเรียบร้อย')", true);
            BindGovPosYearCon();
            ClearGovPosYearCon();
        }
        protected void btnUpdateGovPosYearCon_Click(object sender, EventArgs e)
        {
            if (Session["DefaultIdGovPosYearCon"] == null)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('กรุณาเลือกรายการที่จะแก้ไขก่อน')", true);
                return;
            }
            else
            {
                DatabaseManager.ExecuteNonQuery("UPDATE TB_INSIG_GOV_POS_YEAR_CON SET P_ID = " + ddlInsertGovPosYearConP_ID.SelectedValue + ", INSIG_TARGET = " + ddlInsertGovPosYearConINSIG_TARGET.SelectedValue + ", P_ID_USE = " + ddlInsertGovPosYearConP_ID_USE.SelectedValue + ", POS_YEAR = '" + tbInsertGovPosYearConPOS_YEAR.Text + "' WHERE IGPYC_ID = '" + Session["DefaultIdGovPosYearCon"].ToString() + "'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('อัพเดทข้อมูลเรียบร้อย')", true);
                BindGovPosYearCon();
                ClearGovPosYearCon();
                Session.Remove("DefaultIdGovPosYearCon");
            }
        }
        protected void lbuClearGovPosYearCon_Click(object sender, EventArgs e)
        {
            BindGovPosYearCon();
            ClearGovPosYearCon();
            Session.Remove("DefaultIdGovPosYearCon");
        }
        protected void OnEditGovPosYearCon(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            string ValueHFGovPosYearConID = (item.FindControl("HFGovPosYearConID") as HiddenField).Value;
            string ValueHFGovPosYearConP_ID = (item.FindControl("HFGovPosYearConP_ID") as HiddenField).Value;  
            string ValueHFGovPosYearConINSIG_TARGET = (item.FindControl("HFGovPosYearConINSIG_TARGET") as HiddenField).Value;
            string ValueHFGovPosYearConP_ID_USE = (item.FindControl("HFGovPosYearConP_ID_USE") as HiddenField).Value;
            string ValuelbGovPosYearConPOS_YEAR = (item.FindControl("lbGovPosYearConPOS_YEAR") as Label).Text;

            ddlInsertGovPosYearConP_ID.SelectedValue = ValueHFGovPosYearConP_ID;
            ddlInsertGovPosYearConINSIG_TARGET.SelectedValue = ValueHFGovPosYearConINSIG_TARGET;
            ddlInsertGovPosYearConP_ID_USE.SelectedValue = ValueHFGovPosYearConP_ID_USE;
            tbInsertGovPosYearConPOS_YEAR.Text = ValuelbGovPosYearConPOS_YEAR;

            Session["DefaultIdGovPosYearCon"] = ValueHFGovPosYearConID;
        }
        protected void OnDeleteGovPosYearCon(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            int ValueID = int.Parse((item.FindControl("HFGovPosYearConID") as HiddenField).Value);

            if (ValueID != 0)
            {
                DatabaseManager.ExecuteNonQuery("DELETE TB_INSIG_GOV_POS_YEAR_CON WHERE IGPYC_ID = '" + ValueID + "'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('ลบข้อมูลเรียบร้อย')", true);
                BindGovPosYearCon();
            }
        }






    }
}