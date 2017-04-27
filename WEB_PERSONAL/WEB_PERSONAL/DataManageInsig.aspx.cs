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
            else if (Request.QueryString["ID"] == "GovPosYearCon") { BindGovPosYearCon(); }
            else if (Request.QueryString["ID"] == "GovSalaryCon") { BindGovSalaryCon(); }
            else if (Request.QueryString["ID"] == "GovSalaryYearCon") { BindGovSalaryYearCon(); }
            else if (Request.QueryString["ID"] == "GovAddOne") { BindGovAddOne(); }
            else if (Request.QueryString["ID"] == "EmpAvailable") { BindEmpAvailable(); }
            else if (Request.QueryString["ID"] == "EmpInsigYearCon") { BindEmpInsigYearCon(); }
            else if (Request.QueryString["ID"] == "GovEmpAvailable") { BindGovEmpAvailable(); }
            else if (Request.QueryString["ID"] == "GovEmpInsigYearCon") { BindGovEmpInsigYearCon(); }
            else if (Request.QueryString["ID"] == "GovEmpInworkYear") { BindGovEmpInworkYear(); }
            else if (Request.QueryString["ID"] == "EUAvailable") { BindEUAvailable(); }
            else if (Request.QueryString["ID"] == "EUInsigYearCon") { BindEUInsigYearCon(); }
        }

        private void HideAll()
        {
            Panel1.Visible = false;
            Panel2.Visible = false;
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
            DatabaseManager.BindDropDown(ddlInsertGovAvailablePositionID, "SELECT * FROM TB_POSITION WHERE P_STAFFTYPE_ID = 1 ORDER BY ABS(P_ID) ASC", "P_NAME", "P_ID", "--กรุณาเลือก--");
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
            DatabaseManager.BindDropDown(ddlInsertGovHighSalaryConP_ID, "SELECT * FROM TB_POSITION WHERE P_STAFFTYPE_ID = 1 ORDER BY ABS(P_ID) ASC", "P_NAME", "P_ID", "--กรุณาเลือก--");
            DatabaseManager.BindDropDown(ddlInsertGovHighSalaryConINSIG_TARGET, "SELECT * FROM TB_INSIG_GRADE ORDER BY ABS(INSIG_GRADE_ID) ASC", "INSIG_GRADE_NAME_L", "INSIG_GRADE_ID", "--กรุณาเลือก--");
            DatabaseManager.BindDropDown(ddlInsertGovHighSalaryConP_ID_USE, "SELECT * FROM TB_POSITION WHERE P_STAFFTYPE_ID = 1 ORDER BY ABS(P_ID) ASC", "P_NAME", "P_ID", "--กรุณาเลือก--");
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
            DatabaseManager.BindDropDown(ddlInsertGovInsigYearConP_ID, "SELECT * FROM TB_POSITION WHERE P_STAFFTYPE_ID = 1 ORDER BY ABS(P_ID) ASC", "P_NAME", "P_ID", "--กรุณาเลือก--");
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
            DatabaseManager.BindDropDown(ddlInsertGovPosYearConP_ID, "SELECT * FROM TB_POSITION WHERE P_STAFFTYPE_ID = 1 ORDER BY ABS(P_ID) ASC", "P_NAME", "P_ID", "--กรุณาเลือก--");
            DatabaseManager.BindDropDown(ddlInsertGovPosYearConINSIG_TARGET, "SELECT * FROM TB_INSIG_GRADE ORDER BY ABS(INSIG_GRADE_ID) ASC", "INSIG_GRADE_NAME_L", "INSIG_GRADE_ID", "--กรุณาเลือก--");
            DatabaseManager.BindDropDown(ddlInsertGovPosYearConP_ID_USE, "SELECT * FROM TB_POSITION WHERE P_STAFFTYPE_ID = 1 ORDER BY ABS(P_ID) ASC", "P_NAME", "P_ID", "--กรุณาเลือก--");
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

        //
        protected void BindGovSalaryCon()
        {
            HideAll();
            Panel5.Visible = true;
            OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING);
            OracleDataAdapter sda = new OracleDataAdapter("SELECT IGSC_ID, P_ID, (SELECT P_NAME FROM TB_POSITION WHERE TB_POSITION.P_ID = TB_INSIG_GOV_SALARY_CON.P_ID) P_NAME, INSIG_TARGET, (SELECT INSIG_GRADE_NAME_L FROM TB_INSIG_GRADE WHERE TB_INSIG_GRADE.INSIG_GRADE_ID = TB_INSIG_GOV_SALARY_CON.INSIG_TARGET) INSIG_TARGET_NAME, P_ID_USE, (SELECT P_NAME FROM TB_POSITION WHERE TB_POSITION.P_ID = TB_INSIG_GOV_SALARY_CON.P_ID_USE) P_ID_USE_NAME, IGSC_CON FROM TB_INSIG_GOV_SALARY_CON ORDER BY ABS(IGSC_ID) ASC", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            myRepeaterGovSalaryCon.DataSource = dt;
            myRepeaterGovSalaryCon.DataBind();
            DatabaseManager.BindDropDown(ddlInsertGovSalaryConP_ID, "SELECT * FROM TB_POSITION WHERE P_STAFFTYPE_ID = 1 ORDER BY ABS(P_ID) ASC", "P_NAME", "P_ID", "--กรุณาเลือก--");
            DatabaseManager.BindDropDown(ddlInsertGovSalaryConINSIG_TARGET, "SELECT * FROM TB_INSIG_GRADE ORDER BY ABS(INSIG_GRADE_ID) ASC", "INSIG_GRADE_NAME_L", "INSIG_GRADE_ID", "--กรุณาเลือก--");
            DatabaseManager.BindDropDown(ddlInsertGovSalaryConP_ID_USE, "SELECT * FROM TB_POSITION WHERE P_STAFFTYPE_ID = 1 ORDER BY ABS(P_ID) ASC", "P_NAME", "P_ID", "--กรุณาเลือก--");
        }
        protected void ClearGovSalaryCon()
        {
            ddlInsertGovSalaryConP_ID.SelectedIndex = 0;
            ddlInsertGovSalaryConINSIG_TARGET.SelectedIndex = 0;
            ddlInsertGovSalaryConP_ID_USE.SelectedIndex = 0;
            tbInsertGovSalaryConIGSC_CON.Text = "";
        }
        protected void lbuMenuGovSalaryCon_Click(object sender, EventArgs e)
        {
            BindGovSalaryCon();
        }
        protected void btnInsertGovSalaryCon_Click(object sender, EventArgs e)
        {
            DatabaseManager.ExecuteNonQuery("INSERT INTO TB_INSIG_GOV_SALARY_CON (IGSC_ID,P_ID,INSIG_TARGET,P_ID_USE,IGSC_CON) VALUES (TB_INSIG_GOV_SALARY_CON_SEQ.NEXTVAL," + ddlInsertGovSalaryConP_ID.SelectedValue + "," + ddlInsertGovSalaryConINSIG_TARGET.SelectedValue + "," + ddlInsertGovSalaryConP_ID_USE.SelectedValue + ",'" + tbInsertGovSalaryConIGSC_CON.Text + "')");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('เพิ่มข้อมูลเรียบร้อย')", true);
            BindGovSalaryCon();
            ClearGovSalaryCon();
        }
        protected void btnUpdateGovSalaryCon_Click(object sender, EventArgs e)
        {
            if (Session["DefaultIdGovSalaryCon"] == null)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('กรุณาเลือกรายการที่จะแก้ไขก่อน')", true);
                return;
            }
            else
            {
                DatabaseManager.ExecuteNonQuery("UPDATE TB_INSIG_GOV_SALARY_CON SET P_ID = " + ddlInsertGovSalaryConP_ID.SelectedValue + ", INSIG_TARGET = " + ddlInsertGovSalaryConINSIG_TARGET.SelectedValue + ", P_ID_USE = " + ddlInsertGovSalaryConP_ID_USE.SelectedValue + ", IGSC_CON = " + tbInsertGovSalaryConIGSC_CON.Text + " WHERE IGSC_ID = '" + Session["DefaultIdGovSalaryCon"].ToString() + "'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('อัพเดทข้อมูลเรียบร้อย')", true);
                BindGovSalaryCon();
                ClearGovSalaryCon();
                Session.Remove("DefaultIdGovSalaryCon");
            }
        }
        protected void lbuClearGovSalaryCon_Click(object sender, EventArgs e)
        {
            BindGovSalaryCon();
            ClearGovSalaryCon();
            Session.Remove("DefaultIdGovSalaryCon");
        }
        protected void OnEditGovSalaryCon(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            string ValueHFGovSalaryConID = (item.FindControl("HFGovSalaryConID") as HiddenField).Value;
            string ValueHFGovSalaryConP_ID = (item.FindControl("HFGovSalaryConP_ID") as HiddenField).Value;
            string ValueHFGovSalaryConINSIG_TARGET = (item.FindControl("HFGovSalaryConINSIG_TARGET") as HiddenField).Value;
            string ValueHFGovSalaryConP_ID_USE = (item.FindControl("HFGovSalaryConP_ID_USE") as HiddenField).Value;
            string ValuelbGovSalaryConIGSC_CON = (item.FindControl("lbGovSalaryConIGSC_CON") as Label).Text;

            ddlInsertGovSalaryConP_ID.SelectedValue = ValueHFGovSalaryConP_ID;
            ddlInsertGovSalaryConINSIG_TARGET.SelectedValue = ValueHFGovSalaryConINSIG_TARGET;
            ddlInsertGovSalaryConP_ID_USE.SelectedValue = ValueHFGovSalaryConP_ID_USE;
            tbInsertGovSalaryConIGSC_CON.Text = ValuelbGovSalaryConIGSC_CON;

            Session["DefaultIdGovSalaryCon"] = ValueHFGovSalaryConID;
        }
        protected void OnDeleteGovSalaryCon(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            int ValueID = int.Parse((item.FindControl("HFGovSalaryConID") as HiddenField).Value);

            if (ValueID != 0)
            {
                DatabaseManager.ExecuteNonQuery("DELETE TB_INSIG_GOV_SALARY_CON WHERE IGSC_ID = '" + ValueID + "'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('ลบข้อมูลเรียบร้อย')", true);
                BindGovSalaryCon();
            }
        }

        //
        protected void BindGovSalaryYearCon()
        {
            HideAll();
            Panel6.Visible = true;
            OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING);
            OracleDataAdapter sda = new OracleDataAdapter("SELECT IGSYC_ID, P_ID, (SELECT P_NAME FROM TB_POSITION WHERE TB_POSITION.P_ID = TB_INSIG_GOV_SALARY_YEAR_CON.P_ID) P_NAME, INSIG_TARGET, (SELECT INSIG_GRADE_NAME_L FROM TB_INSIG_GRADE WHERE TB_INSIG_GRADE.INSIG_GRADE_ID = TB_INSIG_GOV_SALARY_YEAR_CON.INSIG_TARGET) INSIG_TARGET_NAME, P_ID_USE, (SELECT P_NAME FROM TB_POSITION WHERE TB_POSITION.P_ID = TB_INSIG_GOV_SALARY_YEAR_CON.P_ID_USE) P_ID_USE_NAME, GET_YEAR FROM TB_INSIG_GOV_SALARY_YEAR_CON ORDER BY ABS(IGSYC_ID) ASC", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            myRepeaterGovSalaryYearCon.DataSource = dt;
            myRepeaterGovSalaryYearCon.DataBind();
            DatabaseManager.BindDropDown(ddlInsertGovSalaryYearConP_ID, "SELECT * FROM TB_POSITION WHERE P_STAFFTYPE_ID = 1 ORDER BY ABS(P_ID) ASC", "P_NAME", "P_ID", "--กรุณาเลือก--");
            DatabaseManager.BindDropDown(ddlInsertGovSalaryYearConINSIG_TARGET, "SELECT * FROM TB_INSIG_GRADE ORDER BY ABS(INSIG_GRADE_ID) ASC", "INSIG_GRADE_NAME_L", "INSIG_GRADE_ID", "--กรุณาเลือก--");
            DatabaseManager.BindDropDown(ddlInsertGovSalaryYearConP_ID_USE, "SELECT * FROM TB_POSITION ORDER BY ABS(P_ID) ASC", "P_NAME", "P_ID", "--กรุณาเลือก--");
        }
        protected void ClearGovSalaryYearCon()
        {
            ddlInsertGovSalaryYearConP_ID.SelectedIndex = 0;
            ddlInsertGovSalaryYearConINSIG_TARGET.SelectedIndex = 0;
            ddlInsertGovSalaryYearConP_ID_USE.SelectedIndex = 0;
            lbInsertGovSalaryYearConGET_YEAR.Text = "";
        }
        protected void lbuMenuGovSalaryYearCon_Click(object sender, EventArgs e)
        {
            BindGovSalaryYearCon();
        }
        protected void btnInsertGovSalaryYearCon_Click(object sender, EventArgs e)
        {
            DatabaseManager.ExecuteNonQuery("INSERT INTO TB_INSIG_GOV_SALARY_YEAR_CON (IGSYC_ID,P_ID,INSIG_TARGET,P_ID_USE,GET_YEAR) VALUES (TB_INSIG_GOV_SALARY_YEAR_SEQ.NEXTVAL," + ddlInsertGovSalaryYearConP_ID.SelectedValue + "," + ddlInsertGovSalaryYearConINSIG_TARGET.SelectedValue + "," + ddlInsertGovSalaryYearConP_ID_USE.SelectedValue + ",'" + lbInsertGovSalaryYearConGET_YEAR.Text + "')");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('เพิ่มข้อมูลเรียบร้อย')", true);
            BindGovSalaryYearCon();
            ClearGovSalaryYearCon();
        }
        protected void btnUpdateGovSalaryYearCon_Click(object sender, EventArgs e)
        {
            if (Session["DefaultIdGovSalaryYearCon"] == null)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('กรุณาเลือกรายการที่จะแก้ไขก่อน')", true);
                return;
            }
            else
            {
                DatabaseManager.ExecuteNonQuery("UPDATE TB_INSIG_GOV_SALARY_YEAR_CON SET P_ID = " + ddlInsertGovSalaryYearConP_ID.SelectedValue + ", INSIG_TARGET = " + ddlInsertGovSalaryYearConINSIG_TARGET.SelectedValue + ", P_ID_USE = " + ddlInsertGovSalaryYearConP_ID_USE.SelectedValue + ", GET_YEAR = " + lbInsertGovSalaryYearConGET_YEAR.Text + " WHERE IGSYC_ID = '" + Session["DefaultIdGovSalaryYearCon"].ToString() + "'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('อัพเดทข้อมูลเรียบร้อย')", true);
                BindGovSalaryYearCon();
                ClearGovSalaryYearCon();
                Session.Remove("DefaultIdGovSalaryYearCon");
            }
        }
        protected void lbuClearGovSalaryYearCon_Click(object sender, EventArgs e)
        {
            BindGovSalaryYearCon();
            ClearGovSalaryYearCon();
            Session.Remove("DefaultIdGovSalaryYearCon");
        }
        protected void OnEditGovSalaryYearCon(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            string ValueHFGovSalaryYearConID = (item.FindControl("HFGovSalaryYearConID") as HiddenField).Value;
            string ValueHFGovSalaryYearConP_ID = (item.FindControl("HFGovSalaryYearConP_ID") as HiddenField).Value;
            string ValueHFGovSalaryYearConINSIG_TARGET = (item.FindControl("HFGovSalaryYearConINSIG_TARGET") as HiddenField).Value;
            string ValueHFGovSalaryYearConP_ID_USE = (item.FindControl("HFGovSalaryYearConP_ID_USE") as HiddenField).Value;
            string ValuelbGovSalaryYearConGET_YEAR = (item.FindControl("lbGovSalaryYearConGET_YEAR") as Label).Text;

            ddlInsertGovSalaryYearConP_ID.SelectedValue = ValueHFGovSalaryYearConP_ID;
            ddlInsertGovSalaryYearConINSIG_TARGET.SelectedValue = ValueHFGovSalaryYearConINSIG_TARGET;
            ddlInsertGovSalaryYearConP_ID_USE.SelectedValue = ValueHFGovSalaryYearConP_ID_USE;
            lbInsertGovSalaryYearConGET_YEAR.Text = ValuelbGovSalaryYearConGET_YEAR;

            Session["DefaultIdGovSalaryYearCon"] = ValueHFGovSalaryYearConID;
        }
        protected void OnDeleteGovSalaryYearCon(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            int ValueID = int.Parse((item.FindControl("HFGovSalaryYearConID") as HiddenField).Value);

            if (ValueID != 0)
            {
                DatabaseManager.ExecuteNonQuery("DELETE TB_INSIG_GOV_SALARY_YEAR_CON WHERE IGSYC_ID = '" + ValueID + "'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('ลบข้อมูลเรียบร้อย')", true);
                BindGovSalaryYearCon();
            }
        }

        //
        //
        protected void BindGovAddOne()
        {
            HideAll();
            Panel7.Visible = true;
            OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING);
            OracleDataAdapter sda = new OracleDataAdapter("SELECT IGAO_ID, P_ID, (SELECT P_NAME FROM TB_POSITION WHERE TB_POSITION.P_ID = TB_INSIG_GOV_ADD_ONE.P_ID) P_NAME, POS_SALARY, INSIG_TARGET, (SELECT INSIG_GRADE_NAME_L FROM TB_INSIG_GRADE WHERE INSIG_GRADE_ID = INSIG_TARGET) INSIG_TARGET_NAME, INSIG_MAX, (SELECT INSIG_GRADE_NAME_L FROM TB_INSIG_GRADE WHERE INSIG_GRADE_ID = INSIG_MAX) INSIG_MAX_NAME FROM TB_INSIG_GOV_ADD_ONE ORDER BY ABS(IGAO_ID) ASC", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            myRepeaterGovAddOne.DataSource = dt;
            myRepeaterGovAddOne.DataBind();
            DatabaseManager.BindDropDown(ddlInsertGovAddOneP_ID, "SELECT * FROM TB_POSITION WHERE P_STAFFTYPE_ID = 1 ORDER BY ABS(P_ID) ASC", "P_NAME", "P_ID", "--กรุณาเลือก--");
            DatabaseManager.BindDropDown(ddlInsertGovAddOneINSIG_TARGET, "SELECT * FROM TB_INSIG_GRADE ORDER BY ABS(INSIG_GRADE_ID) ASC", "INSIG_GRADE_NAME_L", "INSIG_GRADE_ID", "--กรุณาเลือก--");
            DatabaseManager.BindDropDown(ddlInsertGovAddOneINSIG_MAX, "SELECT * FROM TB_INSIG_GRADE ORDER BY ABS(INSIG_GRADE_ID) ASC", "INSIG_GRADE_NAME_L", "INSIG_GRADE_ID", "--กรุณาเลือก--");
        }
        protected void ClearGovAddOne()
        {
            ddlInsertGovAddOneP_ID.SelectedIndex = 0;
            lbInsertGovAddOnePOS_SALARY.Text = "";
            ddlInsertGovAddOneINSIG_TARGET.SelectedIndex = 0;
            ddlInsertGovAddOneINSIG_MAX.SelectedIndex = 0;
        }
        protected void lbuMenuGovAddOne_Click(object sender, EventArgs e)
        {
            BindGovAddOne();
        }
        protected void btnInsertGovAddOne_Click(object sender, EventArgs e)
        {
            DatabaseManager.ExecuteNonQuery("INSERT INTO TB_INSIG_GOV_ADD_ONE (IGAO_ID,P_ID,POS_SALARY,INSIG_TARGET,INSIG_MAX) VALUES (TB_INSIG_GOV_ADD_ONE_SEQ.NEXTVAL," + ddlInsertGovAddOneP_ID.SelectedValue + ",'" + lbInsertGovAddOnePOS_SALARY.Text + "'," + ddlInsertGovAddOneINSIG_TARGET.SelectedValue + "," + ddlInsertGovAddOneINSIG_MAX.SelectedValue + ")");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('เพิ่มข้อมูลเรียบร้อย')", true);
            BindGovAddOne();
            ClearGovAddOne();
        }
        protected void btnUpdateGovAddOne_Click(object sender, EventArgs e)
        {
            if (Session["DefaultIdGovAddOne"] == null)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('กรุณาเลือกรายการที่จะแก้ไขก่อน')", true);
                return;
            }
            else
            {
                DatabaseManager.ExecuteNonQuery("UPDATE TB_INSIG_GOV_ADD_ONE SET P_ID = " + ddlInsertGovAddOneP_ID.SelectedValue + ", POS_SALARY = '" + lbInsertGovAddOnePOS_SALARY.Text + "', INSIG_TARGET = " + ddlInsertGovAddOneINSIG_TARGET.SelectedValue + ", INSIG_MAX = " + ddlInsertGovAddOneINSIG_MAX.SelectedValue + " WHERE IGAO_ID = '" + Session["DefaultIdGovAddOne"].ToString() + "'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('อัพเดทข้อมูลเรียบร้อย')", true);
                BindGovAddOne();
                ClearGovAddOne();
                Session.Remove("DefaultIdGovAddOne");
            }
        }
        protected void lbuClearGovAddOne_Click(object sender, EventArgs e)
        {
            BindGovAddOne();
            ClearGovAddOne();
            Session.Remove("DefaultIdGovAddOne");
        }
        protected void OnEditGovAddOne(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            string ValueHFGovAddOneID = (item.FindControl("HFGovAddOneID") as HiddenField).Value;
            string ValueHFGovAddOneP_ID = (item.FindControl("HFGovAddOneP_ID") as HiddenField).Value;
            string ValuelbGovAddOnePOS_SALARY = (item.FindControl("lbGovAddOnePOS_SALARY") as Label).Text;
            string ValueHFGovAddOneINSIG_TARGET = (item.FindControl("HFGovAddOneINSIG_TARGET") as HiddenField).Value;
            string ValueHFGovAddOneINSIG_MAX = (item.FindControl("HFGovAddOneINSIG_MAX") as HiddenField).Value;

            ddlInsertGovAddOneP_ID.SelectedValue = ValueHFGovAddOneP_ID;
            lbInsertGovAddOnePOS_SALARY.Text = ValuelbGovAddOnePOS_SALARY;
            ddlInsertGovAddOneINSIG_TARGET.SelectedValue = ValueHFGovAddOneINSIG_TARGET;
            ddlInsertGovAddOneINSIG_MAX.SelectedValue = ValueHFGovAddOneINSIG_MAX;

            Session["DefaultIdGovAddOne"] = ValueHFGovAddOneID;
        }
        protected void OnDeleteGovAddOne(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            int ValueID = int.Parse((item.FindControl("HFGovAddOneID") as HiddenField).Value);

            if (ValueID != 0)
            {
                DatabaseManager.ExecuteNonQuery("DELETE TB_INSIG_GOV_ADD_ONE WHERE IGAO_ID = '" + ValueID + "'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('ลบข้อมูลเรียบร้อย')", true);
                BindGovAddOne();
            }
        }

        //
        protected void BindEmpAvailable()
        {
            HideAll();
            Panel8.Visible = true;
            OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING);
            OracleDataAdapter sda = new OracleDataAdapter("SELECT IEA_ID, SALARY_MIN, SALARY_MAX, INSIG_MIN, (SELECT INSIG_GRADE_NAME_L FROM TB_INSIG_GRADE WHERE INSIG_GRADE_ID = INSIG_MIN) INSIG_MIN_NAME, INSIG_MAX, (SELECT INSIG_GRADE_NAME_L FROM TB_INSIG_GRADE WHERE INSIG_GRADE_ID = INSIG_MAX) INSIG_MAX_NAME FROM TB_INSIG_EMP_AVAILABLE ORDER BY ABS(IEA_ID) ASC", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            myRepeaterEmpAvailable.DataSource = dt;
            myRepeaterEmpAvailable.DataBind();
            DatabaseManager.BindDropDown(ddlEmpAvailableINSIG_MIN, "SELECT * FROM TB_INSIG_GRADE ORDER BY ABS(INSIG_GRADE_ID) ASC", "INSIG_GRADE_NAME_L", "INSIG_GRADE_ID", "--กรุณาเลือก--");
            DatabaseManager.BindDropDown(ddlEmpAvailableINSIG_MAX, "SELECT * FROM TB_INSIG_GRADE ORDER BY ABS(INSIG_GRADE_ID) ASC", "INSIG_GRADE_NAME_L", "INSIG_GRADE_ID", "--กรุณาเลือก--");
        }
        protected void ClearEmpAvailable()
        {
            tbEmpAvailableSALARY_MIN.Text = "";
            tbEmpAvailableSALARY_MAX.Text = "";
            ddlEmpAvailableINSIG_MIN.SelectedIndex = 0;
            ddlEmpAvailableINSIG_MAX.SelectedIndex = 0;
        }
        protected void lbuMenuEmpAvailable_Click(object sender, EventArgs e)
        {
            BindEmpAvailable();
        }
        protected void btnInsertEmpAvailable_Click(object sender, EventArgs e)
        {
            DatabaseManager.ExecuteNonQuery("INSERT INTO TB_INSIG_EMP_AVAILABLE (IEA_ID,SALARY_MIN,SALARY_MAX,INSIG_MIN,INSIG_MAX) VALUES (TB_INSIG_EMP_AVAILABLE_SEQ.NEXTVAL,'" + tbEmpAvailableSALARY_MIN.Text + "','" + tbEmpAvailableSALARY_MAX.Text + "'," + ddlEmpAvailableINSIG_MIN.SelectedValue + "," + ddlEmpAvailableINSIG_MAX.SelectedValue + ")");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('เพิ่มข้อมูลเรียบร้อย')", true);
            BindEmpAvailable();
            ClearEmpAvailable();
        }
        protected void btnUpdateEmpAvailable_Click(object sender, EventArgs e)
        {
            if (Session["DefaultIdEmpAvailable"] == null)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('กรุณาเลือกรายการที่จะแก้ไขก่อน')", true);
                return;
            }
            else
            {
                DatabaseManager.ExecuteNonQuery("UPDATE TB_INSIG_EMP_AVAILABLE SET SALARY_MIN = " + tbEmpAvailableSALARY_MIN.Text + ", SALARY_MAX = '" + tbEmpAvailableSALARY_MAX.Text + "', INSIG_MIN = " + ddlEmpAvailableINSIG_MIN.SelectedValue + ", INSIG_MAX = " + ddlEmpAvailableINSIG_MAX.SelectedValue + " WHERE IEA_ID = '" + Session["DefaultIdEmpAvailable"].ToString() + "'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('อัพเดทข้อมูลเรียบร้อย')", true);
                BindEmpAvailable();
                ClearEmpAvailable();
                Session.Remove("DefaultIdEmpAvailable");
            }
        }
        protected void lbuClearEmpAvailable_Click(object sender, EventArgs e)
        {
            BindEmpAvailable();
            ClearEmpAvailable();
            Session.Remove("DefaultIdEmpAvailable");
        }
        protected void OnEditEmpAvailable(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            string ValueHFEmpAvailableID = (item.FindControl("HFEmpAvailableID") as HiddenField).Value;
            string ValuelbEmpAvailableSALARY_MIN = (item.FindControl("lbEmpAvailableSALARY_MIN") as Label).Text;
            string ValuelbEmpAvailableSALARY_MAX = (item.FindControl("lbEmpAvailableSALARY_MAX") as Label).Text;
            string ValueHFEmpAvailableINSIG_MIN = (item.FindControl("HFEmpAvailableINSIG_MIN") as HiddenField).Value;
            string ValueHFEmpAvailableINSIG_MAX = (item.FindControl("HFEmpAvailableINSIG_MAX") as HiddenField).Value;

            tbEmpAvailableSALARY_MIN.Text = ValuelbEmpAvailableSALARY_MIN;
            tbEmpAvailableSALARY_MAX.Text = ValuelbEmpAvailableSALARY_MAX;
            ddlEmpAvailableINSIG_MIN.SelectedValue = ValueHFEmpAvailableINSIG_MIN;
            ddlEmpAvailableINSIG_MAX.SelectedValue = ValueHFEmpAvailableINSIG_MAX;

            Session["DefaultIdEmpAvailable"] = ValueHFEmpAvailableID;
        }
        protected void OnDeleteEmpAvailable(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            int ValueID = int.Parse((item.FindControl("HFEmpAvailableID") as HiddenField).Value);

            if (ValueID != 0)
            {
                DatabaseManager.ExecuteNonQuery("DELETE TB_INSIG_EMP_AVAILABLE WHERE IEA_ID = '" + ValueID + "'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('ลบข้อมูลเรียบร้อย')", true);
                BindEmpAvailable();
            }
        }

        //
        protected void BindEmpInsigYearCon()
        {
            HideAll();
            Panel9.Visible = true;
            OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING);
            OracleDataAdapter sda = new OracleDataAdapter("SELECT IEIYC_ID, SALARY_MIN, SALARY_MAX, INSIG_TARGET, (SELECT INSIG_GRADE_NAME_L FROM TB_INSIG_GRADE WHERE TB_INSIG_GRADE.INSIG_GRADE_ID = TB_INSIG_EMP_INSIG_YEAR_CON.INSIG_TARGET)INSIG_TARGET_NAME, INSIG_USE, (SELECT INSIG_GRADE_NAME_L FROM TB_INSIG_GRADE WHERE TB_INSIG_GRADE.INSIG_GRADE_ID = TB_INSIG_EMP_INSIG_YEAR_CON.INSIG_USE)INSIG_USE_NAME, INSIG_YEAR FROM TB_INSIG_EMP_INSIG_YEAR_CON ORDER BY ABS(IEIYC_ID) ASC", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            myRepeaterEmpInsigYearCon.DataSource = dt;
            myRepeaterEmpInsigYearCon.DataBind();
            DatabaseManager.BindDropDown(ddlEmpInsigYearConINSIG_TARGET, "SELECT * FROM TB_INSIG_GRADE ORDER BY ABS(INSIG_GRADE_ID) ASC", "INSIG_GRADE_NAME_L", "INSIG_GRADE_ID", "--กรุณาเลือก--");
            DatabaseManager.BindDropDown(ddlEmpInsigYearConINSIG_USE, "SELECT * FROM TB_INSIG_GRADE ORDER BY ABS(INSIG_GRADE_ID) ASC", "INSIG_GRADE_NAME_L", "INSIG_GRADE_ID", "--กรุณาเลือก--");
        }
        protected void ClearEmpInsigYearCon()
        {
            tbEmpInsigYearConSALARY_MIN.Text = "";
            tbEmpInsigYearConSALARY_MAX.Text = "";
            ddlEmpInsigYearConINSIG_TARGET.SelectedIndex = 0;
            ddlEmpInsigYearConINSIG_USE.SelectedIndex = 0;
            tbEmpInsigYearConINSIG_YEAR.Text = "";
        }
        protected void lbuMenuEmpInsigYearCon_Click(object sender, EventArgs e)
        {
            BindEmpInsigYearCon();
        }
        protected void btnInsertEmpInsigYearCon_Click(object sender, EventArgs e)
        {
            DatabaseManager.ExecuteNonQuery("INSERT INTO TB_INSIG_EMP_INSIG_YEAR_CON (IEIYC_ID,SALARY_MIN,SALARY_MAX,INSIG_TARGET,INSIG_USE,INSIG_YEAR) VALUES (TB_INSIG_EMP_INSIG_YEAR_SEQ.NEXTVAL,'" + tbEmpInsigYearConSALARY_MIN.Text + "','" + tbEmpInsigYearConSALARY_MAX.Text + "'," + ddlEmpInsigYearConINSIG_TARGET.SelectedValue + "," + ddlEmpInsigYearConINSIG_USE.SelectedValue + ",'" + tbEmpInsigYearConINSIG_YEAR.Text + "')");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('เพิ่มข้อมูลเรียบร้อย')", true);
            BindEmpInsigYearCon();
            ClearEmpInsigYearCon();
        }
        protected void btnUpdateEmpInsigYearCon_Click(object sender, EventArgs e)
        {
            if (Session["DefaultIdEmpInsigYearCon"] == null)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('กรุณาเลือกรายการที่จะแก้ไขก่อน')", true);
                return;
            }
            else
            {
                DatabaseManager.ExecuteNonQuery("UPDATE TB_INSIG_EMP_INSIG_YEAR_CON SET SALARY_MIN = '" + tbEmpInsigYearConSALARY_MIN.Text + "', SALARY_MAX = '" + tbEmpInsigYearConSALARY_MAX.Text + "', INSIG_TARGET = " + ddlEmpInsigYearConINSIG_TARGET.SelectedValue + ", INSIG_USE = " + ddlEmpInsigYearConINSIG_USE.SelectedValue + ", INSIG_YEAR = '" + tbEmpInsigYearConINSIG_YEAR.Text + "' WHERE IEIYC_ID = '" + Session["DefaultIdEmpInsigYearCon"].ToString() + "'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('อัพเดทข้อมูลเรียบร้อย')", true);
                BindEmpInsigYearCon();
                ClearEmpInsigYearCon();
                Session.Remove("DefaultIdEmpInsigYearCon");
            }
        }
        protected void lbuClearEmpInsigYearCon_Click(object sender, EventArgs e)
        {
            BindEmpInsigYearCon();
            ClearEmpInsigYearCon();
            Session.Remove("DefaultIdEmpInsigYearCon");
        }
        protected void OnEditEmpInsigYearCon(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            string ValueHFEmpInsigYearConID = (item.FindControl("HFEmpInsigYearConID") as HiddenField).Value;
            string ValuelblbEmpInsigYearSALARY_MIN = (item.FindControl("lbEmpInsigYearSALARY_MIN") as Label).Text;
            string ValuelblbEmpInsigYearSALARY_MAX = (item.FindControl("lbEmpInsigYearSALARY_MAX") as Label).Text;
            string ValueHFEmpInsigYearConINSIG_TARGET = (item.FindControl("HFEmpInsigYearConINSIG_TARGET") as HiddenField).Value;
            string ValueHFEmpInsigYearConINSIG_USE = (item.FindControl("HFEmpInsigYearConINSIG_USE") as HiddenField).Value;
            string ValuelblbInsigYear = (item.FindControl("lbInsigYear") as Label).Text;

            tbEmpInsigYearConSALARY_MIN.Text = ValuelblbEmpInsigYearSALARY_MIN;
            tbEmpInsigYearConSALARY_MAX.Text = ValuelblbEmpInsigYearSALARY_MAX;
            ddlEmpInsigYearConINSIG_TARGET.SelectedValue = ValueHFEmpInsigYearConINSIG_TARGET;
            ddlEmpInsigYearConINSIG_USE.SelectedValue = ValueHFEmpInsigYearConINSIG_USE;
            tbEmpInsigYearConINSIG_YEAR.Text = ValuelblbInsigYear;

            Session["DefaultIdEmpInsigYearCon"] = ValueHFEmpInsigYearConID;
        }
        protected void OnDeleteEmpInsigYearCon(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            int ValueID = int.Parse((item.FindControl("HFEmpInsigYearConID") as HiddenField).Value);

            if (ValueID != 0)
            {
                DatabaseManager.ExecuteNonQuery("DELETE TB_INSIG_EMP_INSIG_YEAR_CON WHERE IEIYC_ID = '" + ValueID + "'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('ลบข้อมูลเรียบร้อย')", true);
                BindEmpInsigYearCon();
            }
        }

        //
        protected void BindGovEmpAvailable()
        {
            HideAll();
            Panel10.Visible = true;
            OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING);
            OracleDataAdapter sda = new OracleDataAdapter("SELECT IGA_ID, P_ID, (SELECT P_NAME FROM TB_POSITION WHERE TB_POSITION.P_ID = TB_INSIG_GOVEMP_AVAILABLE.P_ID) P_NAME, INSIG_MIN, (SELECT INSIG_GRADE_NAME_L FROM TB_INSIG_GRADE WHERE INSIG_GRADE_ID = INSIG_MIN) INSIG_MIN_NAME, INSIG_MAX, (SELECT INSIG_GRADE_NAME_L FROM TB_INSIG_GRADE WHERE INSIG_GRADE_ID = INSIG_MAX) INSIG_MAX_NAME FROM TB_INSIG_GOVEMP_AVAILABLE ORDER BY ABS(IGA_ID) ASC", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            myRepeaterGovEmpAvailable.DataSource = dt;
            myRepeaterGovEmpAvailable.DataBind();
            DatabaseManager.BindDropDown(ddlGovEmpAvailableP_ID, "SELECT * FROM TB_POSITION WHERE P_STAFFTYPE_ID = 5 ORDER BY ABS(P_ID) ASC", "P_NAME", "P_ID", "--กรุณาเลือก--");
            DatabaseManager.BindDropDown(ddlGovEmpAvailableINSIG_MIN, "SELECT * FROM TB_INSIG_GRADE ORDER BY ABS(INSIG_GRADE_ID) ASC", "INSIG_GRADE_NAME_L", "INSIG_GRADE_ID", "--กรุณาเลือก--");
            DatabaseManager.BindDropDown(ddlGovEmpAvailableINSIG_MAX, "SELECT * FROM TB_INSIG_GRADE ORDER BY ABS(INSIG_GRADE_ID) ASC", "INSIG_GRADE_NAME_L", "INSIG_GRADE_ID", "--กรุณาเลือก--");
        }
        protected void ClearGovEmpAvailable()
        {
            ddlGovEmpAvailableP_ID.SelectedIndex = 0;
            ddlGovEmpAvailableINSIG_MIN.SelectedIndex = 0;
            ddlGovEmpAvailableINSIG_MAX.SelectedIndex = 0;
        }
        protected void lbuMenuGovEmpAvailable_Click(object sender, EventArgs e)
        {
            BindGovEmpAvailable();
        }
        protected void btnInsertGovEmpAvailable_Click(object sender, EventArgs e)
        {
            DatabaseManager.ExecuteNonQuery("INSERT INTO TB_INSIG_GOVEMP_AVAILABLE (IGA_ID,P_ID,INSIG_MIN,INSIG_MAX) VALUES (TB_INSIG_GOVEMP_AVAILABLE_SEQ.NEXTVAL," + ddlGovEmpAvailableP_ID.SelectedValue + "," + ddlGovEmpAvailableINSIG_MIN.SelectedValue + "," + ddlGovEmpAvailableINSIG_MAX.SelectedValue + ")");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('เพิ่มข้อมูลเรียบร้อย')", true);
            BindGovEmpAvailable();
            ClearGovEmpAvailable();
        }
        protected void btnUpdateGovEmpAvailable_Click(object sender, EventArgs e)
        {
            if (Session["DefaultIdGovEmpAvailable"] == null)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('กรุณาเลือกรายการที่จะแก้ไขก่อน')", true);
                return;
            }
            else
            {
                DatabaseManager.ExecuteNonQuery("UPDATE TB_INSIG_GOVEMP_AVAILABLE SET P_ID = " + ddlGovEmpAvailableP_ID.SelectedValue + ", INSIG_MIN = " + ddlGovEmpAvailableINSIG_MIN.SelectedValue + ", INSIG_MAX = " + ddlGovEmpAvailableINSIG_MAX.SelectedValue + " WHERE IGA_ID = '" + Session["DefaultIdGovEmpAvailable"].ToString() + "'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('อัพเดทข้อมูลเรียบร้อย')", true);
                BindGovEmpAvailable();
                ClearGovEmpAvailable();
                Session.Remove("DefaultIdGovEmpAvailable");
            }
        }
        protected void lbuClearGovEmpAvailable_Click(object sender, EventArgs e)
        {
            BindGovEmpAvailable();
            ClearGovEmpAvailable();
            Session.Remove("DefaultIdGovEmpAvailable");
        }
        protected void OnEditGovEmpAvailable(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            string ValueHFGovEmpAvailableID = (item.FindControl("HFGovEmpAvailableID") as HiddenField).Value;
            string ValueHFGovEmpAvailableP_ID = (item.FindControl("HFGovEmpAvailableP_ID") as HiddenField).Value;
            string ValueHFGovEmpAvailableINSIG_MIN = (item.FindControl("HFGovEmpAvailableINSIG_MIN") as HiddenField).Value;
            string ValueHFGovEmpAvailableINSIG_MAX = (item.FindControl("HFGovEmpAvailableINSIG_MAX") as HiddenField).Value;

            ddlGovEmpAvailableP_ID.SelectedValue = ValueHFGovEmpAvailableP_ID;
            ddlGovEmpAvailableINSIG_MIN.SelectedValue = ValueHFGovEmpAvailableINSIG_MIN;
            ddlGovEmpAvailableINSIG_MAX.SelectedValue = ValueHFGovEmpAvailableINSIG_MAX;

            Session["DefaultIdGovEmpAvailable"] = ValueHFGovEmpAvailableID;
        }
        protected void OnDeleteGovEmpAvailable(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            int ValueID = int.Parse((item.FindControl("HFGovEmpAvailableID") as HiddenField).Value);

            if (ValueID != 0)
            {
                DatabaseManager.ExecuteNonQuery("DELETE TB_INSIG_GOVEMP_AVAILABLE WHERE IGA_ID = '" + ValueID + "'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('ลบข้อมูลเรียบร้อย')", true);
                BindGovEmpAvailable();
            }
        }

        //
        protected void BindGovEmpInsigYearCon()
        {
            HideAll();
            Panel11.Visible = true;
            OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING);
            OracleDataAdapter sda = new OracleDataAdapter("SELECT IGYC_ID, P_ID, (SELECT P_NAME FROM TB_POSITION WHERE TB_POSITION.P_ID = TB_INSIG_GOVEMP_INSIG_YEAR_CON.P_ID)P_NAME, INSIG_TARGET, (SELECT INSIG_GRADE_NAME_L FROM TB_INSIG_GRADE WHERE TB_INSIG_GRADE.INSIG_GRADE_ID = TB_INSIG_GOVEMP_INSIG_YEAR_CON.INSIG_TARGET)INSIG_TARGET_NAME, INSIG_USE, (SELECT INSIG_GRADE_NAME_L FROM TB_INSIG_GRADE WHERE TB_INSIG_GRADE.INSIG_GRADE_ID = TB_INSIG_GOVEMP_INSIG_YEAR_CON.INSIG_USE)INSIG_USE_NAME, INSIG_YEAR FROM TB_INSIG_GOVEMP_INSIG_YEAR_CON ORDER BY ABS(IGYC_ID) ASC", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            myRepeaterGovEmpInsigYearCon.DataSource = dt;
            myRepeaterGovEmpInsigYearCon.DataBind();
            DatabaseManager.BindDropDown(ddlGovEmpInsigYearConP_ID, "SELECT * FROM TB_POSITION WHERE P_STAFFTYPE_ID = 5 ORDER BY ABS(P_ID) ASC", "P_NAME", "P_ID", "--กรุณาเลือก--");
            DatabaseManager.BindDropDown(ddlGovEmpInsigYearConINSIG_TARGET, "SELECT * FROM TB_INSIG_GRADE ORDER BY ABS(INSIG_GRADE_ID) ASC", "INSIG_GRADE_NAME_L", "INSIG_GRADE_ID", "--กรุณาเลือก--");
            DatabaseManager.BindDropDown(ddlGovEmpInsigYearConINSIG_USE, "SELECT * FROM TB_INSIG_GRADE ORDER BY ABS(INSIG_GRADE_ID) ASC", "INSIG_GRADE_NAME_L", "INSIG_GRADE_ID", "--กรุณาเลือก--");
        }
        protected void ClearGovEmpInsigYearCon()
        {
            ddlGovEmpInsigYearConP_ID.SelectedIndex = 0;
            ddlGovEmpInsigYearConINSIG_TARGET.SelectedIndex = 0;
            ddlGovEmpInsigYearConINSIG_USE.SelectedIndex = 0;
            tbGovEmpInsigYearConINSG_YEAR.Text = "";
        }
        protected void lbuMenuGovEmpInsigYearCon_Click(object sender, EventArgs e)
        {
            BindGovEmpInsigYearCon();
        }
        protected void btnInsertGovEmpInsigYearCon_Click(object sender, EventArgs e)
        {
            DatabaseManager.ExecuteNonQuery("INSERT INTO TB_INSIG_GOVEMP_INSIG_YEAR_CON (IGYC_ID,P_ID,INSIG_TARGET,INSIG_USE,INSIG_YEAR) VALUES (TB_INSIG_GOVEMP_INSIG_YEAR_SEQ.NEXTVAL," + ddlGovEmpInsigYearConP_ID.SelectedValue + "," + ddlGovEmpInsigYearConINSIG_TARGET.SelectedValue + "," + ddlGovEmpInsigYearConINSIG_USE.SelectedValue + ",'" + tbGovEmpInsigYearConINSG_YEAR.Text + "')");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('เพิ่มข้อมูลเรียบร้อย')", true);
            BindGovEmpInsigYearCon();
            ClearGovEmpInsigYearCon();
        }
        protected void btnUpdateGovEmpInsigYearCon_Click(object sender, EventArgs e)
        {
            if (Session["DefaultIdGovEmpInsigYearCon"] == null)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('กรุณาเลือกรายการที่จะแก้ไขก่อน')", true);
                return;
            }
            else
            {
                DatabaseManager.ExecuteNonQuery("UPDATE TB_INSIG_GOVEMP_INSIG_YEAR_CON SET P_ID = " + ddlGovEmpInsigYearConP_ID.SelectedValue + ", INSIG_TARGET = " + ddlGovEmpInsigYearConINSIG_TARGET.SelectedValue + ", INSIG_USE = " + ddlGovEmpInsigYearConINSIG_USE.SelectedValue + ", INSIG_YEAR = '" + tbGovEmpInsigYearConINSG_YEAR.Text + "' WHERE IGYC_ID = '" + Session["DefaultIdGovEmpInsigYearCon"].ToString() + "'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('อัพเดทข้อมูลเรียบร้อย')", true);
                BindGovEmpInsigYearCon();
                ClearGovEmpInsigYearCon();
                Session.Remove("DefaultIdGovEmpInsigYearCon");
            }
        }
        protected void lbuClearGovEmpInsigYearCon_Click(object sender, EventArgs e)
        {
            BindGovEmpInsigYearCon();
            ClearGovEmpInsigYearCon();
            Session.Remove("DefaultIdGovEmpInsigYearCon");
        }
        protected void OnEditGovEmpInsigYearCon(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            string ValueHFGovEmpInsigYearConID = (item.FindControl("HFGovEmpInsigYearConID") as HiddenField).Value;
            string ValueHFGovEmpInsigYearConP_ID = (item.FindControl("HFGovEmpInsigYearConP_ID") as HiddenField).Value;
            string ValueHFGovEmpInsigYearConINSIG_TARGET = (item.FindControl("HFGovEmpInsigYearConINSIG_TARGET") as HiddenField).Value;
            string ValueHFGovEmpInsigYearConINSIG_USE = (item.FindControl("HFGovEmpInsigYearConINSIG_USE") as HiddenField).Value;
            string ValuelbInsigYear = (item.FindControl("lbInsigYear") as Label).Text;

            ddlGovEmpInsigYearConP_ID.SelectedValue = ValueHFGovEmpInsigYearConP_ID;
            ddlGovEmpInsigYearConINSIG_TARGET.SelectedValue = ValueHFGovEmpInsigYearConINSIG_TARGET;
            ddlGovEmpInsigYearConINSIG_USE.SelectedValue = ValueHFGovEmpInsigYearConINSIG_USE;
            tbGovEmpInsigYearConINSG_YEAR.Text = ValuelbInsigYear;

            Session["DefaultIdGovEmpInsigYearCon"] = ValueHFGovEmpInsigYearConID;
        }
        protected void OnDeleteGovEmpInsigYearCon(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            int ValueID = int.Parse((item.FindControl("HFGovEmpInsigYearConID") as HiddenField).Value);

            if (ValueID != 0)
            {
                DatabaseManager.ExecuteNonQuery("DELETE TB_INSIG_GOVEMP_INSIG_YEAR_CON WHERE IGYC_ID = '" + ValueID + "'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('ลบข้อมูลเรียบร้อย')", true);
                BindGovEmpInsigYearCon();
            }
        }

        //
        protected void BindGovEmpInworkYear()
        {
            HideAll();
            Panel12.Visible = true;
            OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING);
            OracleDataAdapter sda = new OracleDataAdapter("SELECT IGIY_ID, P_ID_MIN, (SELECT P_NAME FROM TB_POSITION WHERE TB_POSITION.P_ID = TB_INSIG_GOVEMP_INWORK_YEAR.P_ID_MIN)P_ID_MIN_NAME, P_ID_MAX, (SELECT P_NAME FROM TB_POSITION WHERE TB_POSITION.P_ID = TB_INSIG_GOVEMP_INWORK_YEAR.P_ID_MAX)P_ID_MAX_NAME, P_ID_YEAR FROM TB_INSIG_GOVEMP_INWORK_YEAR ORDER BY ABS(IGIY_ID) ASC", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            myRepeaterGovEmpInworkYear.DataSource = dt;
            myRepeaterGovEmpInworkYear.DataBind();
            DatabaseManager.BindDropDown(ddlGovEmpInworkYearP_ID_MIN, "SELECT * FROM TB_POSITION WHERE P_STAFFTYPE_ID = 5 ORDER BY ABS(P_ID) ASC", "P_NAME", "P_ID", "--กรุณาเลือก--");
            DatabaseManager.BindDropDown(ddlGovEmpInworkYearP_ID_MAX, "SELECT * FROM TB_POSITION WHERE P_STAFFTYPE_ID = 5 ORDER BY ABS(P_ID) ASC", "P_NAME", "P_ID", "--กรุณาเลือก--");
        }
        protected void ClearGovEmpInworkYear()
        {
            ddlGovEmpInworkYearP_ID_MIN.SelectedIndex = 0;
            ddlGovEmpInworkYearP_ID_MAX.SelectedIndex = 0;
            tbGovEmpInworkYearP_ID_YEAR.Text = "";
        }
        protected void lbuMenuGovEmpInworkYear_Click(object sender, EventArgs e)
        {
            BindGovEmpInworkYear();
        }
        protected void btnInsertGovEmpInworkYear_Click(object sender, EventArgs e)
        {
            DatabaseManager.ExecuteNonQuery("INSERT INTO TB_INSIG_GOVEMP_INWORK_YEAR (IGIY_ID,P_ID_MIN,P_ID_MAX,P_ID_YEAR) VALUES (TB_INSIG_GOVEMP_INWORK_YE_SEQ.NEXTVAL," + ddlGovEmpInworkYearP_ID_MIN.SelectedValue + "," + ddlGovEmpInworkYearP_ID_MAX.SelectedValue + ",'" + tbGovEmpInworkYearP_ID_YEAR.Text + "')");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('เพิ่มข้อมูลเรียบร้อย')", true);
            BindGovEmpInworkYear();
            ClearGovEmpInworkYear();
        }
        protected void btnUpdateGovEmpInworkYear_Click(object sender, EventArgs e)
        {
            if (Session["DefaultIdGovEmpInworkYear"] == null)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('กรุณาเลือกรายการที่จะแก้ไขก่อน')", true);
                return;
            }
            else
            {
                DatabaseManager.ExecuteNonQuery("UPDATE TB_INSIG_GOVEMP_INWORK_YEAR SET P_ID_MIN = " + ddlGovEmpInworkYearP_ID_MIN.SelectedValue + ", P_ID_MAX = " + ddlGovEmpInworkYearP_ID_MAX.SelectedValue + ", P_ID_YEAR = '" + tbGovEmpInworkYearP_ID_YEAR.Text + "' WHERE IGIY_ID = '" + Session["DefaultIdGovEmpInworkYear"].ToString() + "'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('อัพเดทข้อมูลเรียบร้อย')", true);
                BindGovEmpInworkYear();
                ClearGovEmpInworkYear();
                Session.Remove("DefaultIdGovEmpInworkYear");
            }
        }
        protected void lbuClearGovEmpInworkYear_Click(object sender, EventArgs e)
        {
            BindGovEmpInworkYear();
            ClearGovEmpInworkYear();
            Session.Remove("DefaultIdGovEmpInworkYear");
        }
        protected void OnEditGovEmpInworkYear(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            string ValueHFGovEmpInworkYearID = (item.FindControl("HFGovEmpInworkYearID") as HiddenField).Value;
            string ValueHFGovEmpInworkYearP_ID_MIN = (item.FindControl("HFGovEmpInworkYearP_ID_MIN") as HiddenField).Value;
            string ValueHFGovEmpInworkYearP_ID_MAX = (item.FindControl("HFGovEmpInworkYearP_ID_MAX") as HiddenField).Value;
            string ValuelbInworkYear = (item.FindControl("lbInworkYear") as Label).Text;

            ddlGovEmpInworkYearP_ID_MIN.SelectedValue = ValueHFGovEmpInworkYearP_ID_MIN;
            ddlGovEmpInworkYearP_ID_MAX.SelectedValue = ValueHFGovEmpInworkYearP_ID_MAX;
            tbGovEmpInworkYearP_ID_YEAR.Text = ValuelbInworkYear;

            Session["DefaultIdGovEmpInworkYear"] = ValueHFGovEmpInworkYearID;
        }
        protected void OnDeleteGovEmpInworkYear(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            int ValueID = int.Parse((item.FindControl("HFGovEmpInworkYearID") as HiddenField).Value);

            if (ValueID != 0)
            {
                DatabaseManager.ExecuteNonQuery("DELETE TB_INSIG_GOVEMP_INWORK_YEAR WHERE IGIY_ID = '" + ValueID + "'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('ลบข้อมูลเรียบร้อย')", true);
                BindGovEmpInworkYear();
            }
        }

        //
        protected void BindEUAvailable()
        {
            HideAll();
            Panel13.Visible = true;
            OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING);
            OracleDataAdapter sda = new OracleDataAdapter("SELECT IEUA_ID, POSITION_WORK_ID, (SELECT POSITION_WORK_NAME FROM TB_POSITION_WORK WHERE TB_POSITION_WORK.POSITION_WORK_ID = TB_INSIG_EU_AVAILABLE.POSITION_WORK_ID) POSITION_WORK_ID_NAME, ADMIN_POS_ID, (SELECT ADMIN_POSITION_NAME FROM TB_ADMIN_POSITION WHERE TB_ADMIN_POSITION.ADMIN_POSITION_ID = TB_INSIG_EU_AVAILABLE.ADMIN_POS_ID) ADMIN_POS_ID_NAME, INSIG_MIN, (SELECT INSIG_GRADE_NAME_L FROM TB_INSIG_GRADE WHERE INSIG_GRADE_ID = INSIG_MIN) INSIG_MIN_NAME, INSIG_MAX, (SELECT INSIG_GRADE_NAME_L FROM TB_INSIG_GRADE WHERE INSIG_GRADE_ID = INSIG_MAX) INSIG_MAX_NAME FROM TB_INSIG_EU_AVAILABLE ORDER BY ABS(IEUA_ID) ASC", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            myRepeaterEUAvailable.DataSource = dt;
            myRepeaterEUAvailable.DataBind();
            DatabaseManager.BindDropDown(ddlEUAvailablePOSITION_WORK_ID, "SELECT * FROM TB_POSITION_WORK ORDER BY ABS(POSITION_WORK_ID) ASC", "POSITION_WORK_NAME", "POSITION_WORK_ID", "--กรุณาเลือก--");
            DatabaseManager.BindDropDown(ddlEUAvailableADMIN_POS_ID, "SELECT * FROM TB_ADMIN_POSITION ORDER BY ABS(ADMIN_POSITION_ID) ASC", "ADMIN_POSITION_NAME", "ADMIN_POSITION_ID", "--กรุณาเลือก--");
            DatabaseManager.BindDropDown(ddlEUAvailableINSIG_MIN, "SELECT * FROM TB_INSIG_GRADE ORDER BY ABS(INSIG_GRADE_ID) ASC", "INSIG_GRADE_NAME_L", "INSIG_GRADE_ID", "--กรุณาเลือก--");
            DatabaseManager.BindDropDown(ddlEUAvailableINSIG_MAX, "SELECT * FROM TB_INSIG_GRADE ORDER BY ABS(INSIG_GRADE_ID) ASC", "INSIG_GRADE_NAME_L", "INSIG_GRADE_ID", "--กรุณาเลือก--");
        }
        protected void ClearEUAvailable()
        {
            ddlEUAvailablePOSITION_WORK_ID.SelectedIndex = 0;
            ddlEUAvailableADMIN_POS_ID.SelectedIndex = 0;
            ddlEUAvailableINSIG_MIN.SelectedIndex = 0;
            ddlEUAvailableINSIG_MAX.SelectedIndex = 0;
        }
        protected void lbuMenuEUAvailable_Click(object sender, EventArgs e)
        {
            BindEUAvailable();
        }
        protected void btnInsertEUAvailable_Click(object sender, EventArgs e)
        {
            OracleConnection.ClearAllPools();
            using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
            {
                con.Open();
                using (OracleCommand com = new OracleCommand("INSERT INTO TB_INSIG_EU_AVAILABLE (IEUA_ID,POSITION_WORK_ID,ADMIN_POS_ID,INSIG_MIN,INSIG_MAX) VALUES (TB_INSIG_EU_AVAILABLE_SEQ.NEXTVAL, :POSITION_WORK_ID, :ADMIN_POS_ID, :INSIG_MIN, :INSIG_MAX)", con))
                {
                    if (ddlEUAvailablePOSITION_WORK_ID.SelectedValue != "")
                    {
                        com.Parameters.Add(new OracleParameter("POSITION_WORK_ID", ddlEUAvailablePOSITION_WORK_ID.SelectedValue));
                    }else
                    {
                        com.Parameters.Add(new OracleParameter("POSITION_WORK_ID", DBNull.Value));
                    }

                    if (ddlEUAvailableADMIN_POS_ID.SelectedValue != "")
                    {
                        com.Parameters.Add(new OracleParameter("ADMIN_POS_ID", ddlEUAvailableADMIN_POS_ID.SelectedValue));
                    }
                    else
                    {
                        com.Parameters.Add(new OracleParameter("ADMIN_POS_ID", DBNull.Value));
                    }
                    com.Parameters.Add(new OracleParameter("INSIG_MIN", ddlEUAvailableINSIG_MIN.SelectedValue));
                    com.Parameters.Add(new OracleParameter("INSIG_MAX", ddlEUAvailableINSIG_MAX.SelectedValue));
                    com.ExecuteNonQuery();
                }
            }

            //DatabaseManager.ExecuteNonQuery("INSERT INTO TB_INSIG_EU_AVAILABLE (IEUA_ID,POSITION_WORK_ID,ADMIN_POS_ID,INSIG_MIN,INSIG_MAX) VALUES (TB_INSIG_EU_AVAILABLE_SEQ.NEXTVAL," + ddlEUAvailablePOSITION_WORK_ID.SelectedValue + "," + ddlEUAvailableADMIN_POS_ID.SelectedValue + "," + ddlEUAvailableINSIG_MIN.SelectedValue + "," + ddlEUAvailableINSIG_MAX.SelectedValue + ")");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('เพิ่มข้อมูลเรียบร้อย')", true);
            BindEUAvailable();
            ClearEUAvailable();
        }
        protected void btnUpdateEUAvailable_Click(object sender, EventArgs e)
        {
            if (Session["DefaultIdEUAvailable"] == null)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('กรุณาเลือกรายการที่จะแก้ไขก่อน')", true);
                return;
            }
            else
            {
                OracleConnection.ClearAllPools();
                using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
                {
                    con.Open();
                    using (OracleCommand com = new OracleCommand("UPDATE TB_INSIG_EU_AVAILABLE SET POSITION_WORK_ID=:POSITION_WORK_ID, ADMIN_POS_ID=:ADMIN_POS_ID, INSIG_MIN=:INSIG_MIN, INSIG_MAX=:INSIG_MAX WHERE IEUA_ID=:IEUA_ID", con))
                    {
                        if (ddlEUAvailablePOSITION_WORK_ID.SelectedValue != "")
                        {
                            com.Parameters.Add(new OracleParameter("POSITION_WORK_ID", ddlEUAvailablePOSITION_WORK_ID.SelectedValue));
                        }
                        else
                        {
                            com.Parameters.Add(new OracleParameter("POSITION_WORK_ID", DBNull.Value));
                        }

                        if (ddlEUAvailableADMIN_POS_ID.SelectedValue != "")
                        {
                            com.Parameters.Add(new OracleParameter("ADMIN_POS_ID", ddlEUAvailableADMIN_POS_ID.SelectedValue));
                        }
                        else
                        {
                            com.Parameters.Add(new OracleParameter("ADMIN_POS_ID", DBNull.Value));
                        }
                        com.Parameters.Add(new OracleParameter("INSIG_MIN", ddlEUAvailableINSIG_MIN.SelectedValue));
                        com.Parameters.Add(new OracleParameter("INSIG_MAX", ddlEUAvailableINSIG_MAX.SelectedValue));
                        com.Parameters.Add(new OracleParameter("IEUA_ID", Session["DefaultIdEUAvailable"].ToString()));
                        com.ExecuteNonQuery();
                    }
                }

                //DatabaseManager.ExecuteNonQuery("UPDATE TB_INSIG_EU_AVAILABLE SET POSITION_WORK_ID = " + ddlEUAvailablePOSITION_WORK_ID.SelectedValue + ", ADMIN_POS_ID = " + ddlEUAvailableADMIN_POS_ID.SelectedValue + ", INSIG_MIN = " + ddlEUAvailableINSIG_MIN.SelectedValue + ", INSIG_MAX = " + ddlEUAvailableINSIG_MAX.SelectedValue + " WHERE IEUA_ID = '" + Session["DefaultIdEUAvailable"].ToString() + "'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('อัพเดทข้อมูลเรียบร้อย')", true);
                BindEUAvailable();
                ClearEUAvailable();
                Session.Remove("DefaultIdEUAvailable");
            }
        }
        protected void lbuClearEUAvailable_Click(object sender, EventArgs e)
        {
            BindEUAvailable();
            ClearEUAvailable();
            Session.Remove("DefaultIdEUAvailable");
        }
        protected void OnEditEUAvailable(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            string ValueHFEUAvailableID = (item.FindControl("HFEUAvailableID") as HiddenField).Value;
            string ValueHFEUAvailablePOSITION_WORK_ID = (item.FindControl("HFEUAvailablePOSITION_WORK_ID") as HiddenField).Value;
            string ValueHFEUAvailableADMIN_POS_ID = (item.FindControl("HFEUAvailableADMIN_POS_ID") as HiddenField).Value;
            string ValueHFEUAvailableINSIG_MIN = (item.FindControl("HFEUAvailableINSIG_MIN") as HiddenField).Value;
            string ValueHFEUAvailableINSIG_MAX = (item.FindControl("HFEUAvailableINSIG_MAX") as HiddenField).Value;

            ddlEUAvailablePOSITION_WORK_ID.SelectedValue = ValueHFEUAvailablePOSITION_WORK_ID;
            ddlEUAvailableADMIN_POS_ID.SelectedValue = ValueHFEUAvailableADMIN_POS_ID;
            ddlEUAvailableINSIG_MIN.SelectedValue = ValueHFEUAvailableINSIG_MIN;
            ddlEUAvailableINSIG_MAX.SelectedValue = ValueHFEUAvailableINSIG_MAX;

            Session["DefaultIdEUAvailable"] = ValueHFEUAvailableID;
        }
        protected void OnDeleteEUAvailable(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            int ValueID = int.Parse((item.FindControl("HFEUAvailableID") as HiddenField).Value);

            if (ValueID != 0)
            {
                DatabaseManager.ExecuteNonQuery("DELETE TB_INSIG_EU_AVAILABLE WHERE IEUA_ID = '" + ValueID + "'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('ลบข้อมูลเรียบร้อย')", true);
                BindEUAvailable();
            }
        }

        //
        protected void BindEUInsigYearCon()
        {
            HideAll();
            Panel14.Visible = true;
            OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING);
            OracleDataAdapter sda = new OracleDataAdapter("SELECT IEUIYC_ID, POSITION_WORK_ID, (SELECT POSITION_WORK_NAME FROM TB_POSITION_WORK WHERE TB_POSITION_WORK.POSITION_WORK_ID = TB_INSIG_EU_INSIG_YEAR_CON.POSITION_WORK_ID) POSITION_WORK_ID_NAME, ADMIN_POS_ID, (SELECT ADMIN_POSITION_NAME FROM TB_ADMIN_POSITION WHERE TB_ADMIN_POSITION.ADMIN_POSITION_ID = TB_INSIG_EU_INSIG_YEAR_CON.ADMIN_POS_ID) ADMIN_POS_ID_NAME, INSIG_TARGET, (SELECT INSIG_GRADE_NAME_L FROM TB_INSIG_GRADE WHERE INSIG_GRADE_ID = INSIG_TARGET) INSIG_TARGET_NAME, INSIG_USE, (SELECT INSIG_GRADE_NAME_L FROM TB_INSIG_GRADE WHERE INSIG_GRADE_ID = INSIG_USE) INSIG_USE_NAME, INSIG_YEAR FROM TB_INSIG_EU_INSIG_YEAR_CON ORDER BY ABS(IEUIYC_ID) ASC", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            myRepeaterEUInsigYearCon.DataSource = dt;
            myRepeaterEUInsigYearCon.DataBind();
            DatabaseManager.BindDropDown(ddlEUInsigYearConPOSITION_WORK_ID, "SELECT * FROM TB_POSITION_WORK ORDER BY ABS(POSITION_WORK_ID) ASC", "POSITION_WORK_NAME", "POSITION_WORK_ID", "--กรุณาเลือก--");
            DatabaseManager.BindDropDown(ddlEUInsigYearConADMIN_POS_ID, "SELECT * FROM TB_ADMIN_POSITION ORDER BY ABS(ADMIN_POSITION_ID) ASC", "ADMIN_POSITION_NAME", "ADMIN_POSITION_ID", "--กรุณาเลือก--");
            DatabaseManager.BindDropDown(ddlEUInsigYearConINSIG_TARGET, "SELECT * FROM TB_INSIG_GRADE ORDER BY ABS(INSIG_GRADE_ID) ASC", "INSIG_GRADE_NAME_L", "INSIG_GRADE_ID", "--กรุณาเลือก--");
            DatabaseManager.BindDropDown(ddlEUInsigYearConINSIG_USE, "SELECT * FROM TB_INSIG_GRADE ORDER BY ABS(INSIG_GRADE_ID) ASC", "INSIG_GRADE_NAME_L", "INSIG_GRADE_ID", "--กรุณาเลือก--");
        }
        protected void ClearEUInsigYearCon()
        {
            ddlEUInsigYearConPOSITION_WORK_ID.SelectedIndex = 0;
            ddlEUInsigYearConADMIN_POS_ID.SelectedIndex = 0;
            ddlEUInsigYearConINSIG_TARGET.SelectedIndex = 0;
            ddlEUInsigYearConINSIG_USE.SelectedIndex = 0;
            tbEUInsigYearConINSIG_YEAR.Text = "";
        }
        protected void lbuMenuEUInsigYearCon_Click(object sender, EventArgs e)
        {
            BindEUInsigYearCon();
        }
        protected void btnInsertEUInsigYearCon_Click(object sender, EventArgs e)
        {
            OracleConnection.ClearAllPools();
            using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
            {
                con.Open();
                using (OracleCommand com = new OracleCommand("INSERT INTO TB_INSIG_EU_INSIG_YEAR_CON (IEUIYC_ID,POSITION_WORK_ID,ADMIN_POS_ID,INSIG_TARGET,INSIG_USE,INSIG_YEAR) VALUES (TB_INSIG_EU_INSIG_YEAR_CON_SEQ.NEXTVAL, :POSITION_WORK_ID, :ADMIN_POS_ID, :INSIG_TARGET, :INSIG_USE, :INSIG_YEAR)", con))
                {
                    if (ddlEUInsigYearConPOSITION_WORK_ID.SelectedValue != "")
                    {
                        com.Parameters.Add(new OracleParameter("POSITION_WORK_ID", ddlEUInsigYearConPOSITION_WORK_ID.SelectedValue));
                    }
                    else
                    {
                        com.Parameters.Add(new OracleParameter("POSITION_WORK_ID", DBNull.Value));
                    }

                    if (ddlEUInsigYearConADMIN_POS_ID.SelectedValue != "")
                    {
                        com.Parameters.Add(new OracleParameter("ADMIN_POS_ID", ddlEUInsigYearConADMIN_POS_ID.SelectedValue));
                    }
                    else
                    {
                        com.Parameters.Add(new OracleParameter("ADMIN_POS_ID", DBNull.Value));
                    }
                    com.Parameters.Add(new OracleParameter("INSIG_TARGET", ddlEUInsigYearConINSIG_TARGET.SelectedValue));
                    com.Parameters.Add(new OracleParameter("INSIG_USE", ddlEUInsigYearConINSIG_USE.SelectedValue));
                    com.Parameters.Add(new OracleParameter("INSIG_YEAR", tbEUInsigYearConINSIG_YEAR.Text));
                    com.ExecuteNonQuery();
                }
            }

            //DatabaseManager.ExecuteNonQuery("INSERT INTO TB_INSIG_EU_INSIG_YEAR_CON (IEUIYC_ID,POSITION_WORK_ID,ADMIN_POS_ID,INSIG_TARGET,INSIG_USE,INSIG_YEAR) VALUES (TB_INSIG_EU_INSIG_YEAR_CON_SEQ.NEXTVAL," + ddlEUInsigYearConPOSITION_WORK_ID.SelectedValue + "," + ddlEUInsigYearConADMIN_POS_ID.SelectedValue + "," + ddlEUInsigYearConINSIG_TARGET.SelectedValue + "," + ddlEUInsigYearConINSIG_USE.SelectedValue + ", " + tbEUInsigYearConINSIG_YEAR.Text + ")");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('เพิ่มข้อมูลเรียบร้อย')", true);
            BindEUInsigYearCon();
            ClearEUInsigYearCon();
        }
        protected void btnUpdateEUInsigYearCon_Click(object sender, EventArgs e)
        {
            if (Session["DefaultIdEUInsigYearCon"] == null)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('กรุณาเลือกรายการที่จะแก้ไขก่อน')", true);
                return;
            }
            else
            {
                OracleConnection.ClearAllPools();
                using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
                {
                    con.Open();
                    using (OracleCommand com = new OracleCommand("UPDATE TB_INSIG_EU_INSIG_YEAR_CON SET POSITION_WORK_ID=:POSITION_WORK_ID, ADMIN_POS_ID=:ADMIN_POS_ID, INSIG_TARGET=:INSIG_TARGET, INSIG_USE=:INSIG_USE, INSIG_YEAR=:INSIG_YEAR WHERE IEUIYC_ID=:IEUIYC_ID", con))
                    {
                        if (ddlEUInsigYearConPOSITION_WORK_ID.SelectedValue != "")
                        {
                            com.Parameters.Add(new OracleParameter("POSITION_WORK_ID", ddlEUInsigYearConPOSITION_WORK_ID.SelectedValue));
                        }
                        else
                        {
                            com.Parameters.Add(new OracleParameter("POSITION_WORK_ID", DBNull.Value));
                        }

                        if (ddlEUInsigYearConADMIN_POS_ID.SelectedValue != "")
                        {
                            com.Parameters.Add(new OracleParameter("ADMIN_POS_ID", ddlEUInsigYearConADMIN_POS_ID.SelectedValue));
                        }
                        else
                        {
                            com.Parameters.Add(new OracleParameter("ADMIN_POS_ID", DBNull.Value));
                        }
                        com.Parameters.Add(new OracleParameter("INSIG_TARGET", ddlEUInsigYearConINSIG_TARGET.SelectedValue));
                        com.Parameters.Add(new OracleParameter("INSIG_USE", ddlEUInsigYearConINSIG_USE.SelectedValue));
                        com.Parameters.Add(new OracleParameter("INSIG_YEAR", tbEUInsigYearConINSIG_YEAR.Text));
                        com.Parameters.Add(new OracleParameter("IEUIYC_ID", Session["DefaultIdEUInsigYearCon"].ToString()));
                        com.ExecuteNonQuery();
                    }
                }

                //DatabaseManager.ExecuteNonQuery("UPDATE TB_INSIG_EU_INSIG_YEAR_CON SET POSITION_WORK_ID = " + ddlEUInsigYearConPOSITION_WORK_ID.SelectedValue + ", ADMIN_POS_ID = " + ddlEUInsigYearConADMIN_POS_ID.SelectedValue + ", INSIG_TARGET = " + ddlEUInsigYearConINSIG_TARGET.SelectedValue + ", INSIG_USE = " + ddlEUInsigYearConINSIG_USE.SelectedValue + ", INSIG_YEAR = '" + tbEUInsigYearConINSIG_YEAR.Text + "' WHERE IEUIYC_ID = '" + Session["DefaultIdEUInsigYearCon"].ToString() + "'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('อัพเดทข้อมูลเรียบร้อย')", true);
                BindEUInsigYearCon();
                ClearEUInsigYearCon();
                Session.Remove("DefaultIdEUInsigYearCon");
            }
        }
        protected void lbuClearEUInsigYearCon_Click(object sender, EventArgs e)
        {
            BindEUInsigYearCon();
            ClearEUInsigYearCon();
            Session.Remove("DefaultIdEUInsigYearCon");
        }
        protected void OnEditEUInsigYearCon(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            string ValueHFEUInsigYearConID = (item.FindControl("HFEUInsigYearConID") as HiddenField).Value;
            string ValueHFEUInsigYearConPOSITION_WORK_ID = (item.FindControl("HFEUInsigYearConPOSITION_WORK_ID") as HiddenField).Value;
            string ValueHFEUInsigYearConADMIN_POS_ID = (item.FindControl("HFEUInsigYearConADMIN_POS_ID") as HiddenField).Value;
            string ValueHFEUInsigYearConINSIG_TARGET = (item.FindControl("HFEUInsigYearConINSIG_TARGET") as HiddenField).Value;
            string ValueHFEUInsigYearConINSIG_USE = (item.FindControl("HFEUInsigYearConINSIG_USE") as HiddenField).Value;
            string ValuelbEUInsigYearConINSIG_YEAR = (item.FindControl("lbEUInsigYearConINSIG_YEAR") as Label).Text;

            ddlEUInsigYearConPOSITION_WORK_ID.SelectedValue = ValueHFEUInsigYearConPOSITION_WORK_ID;
            ddlEUInsigYearConADMIN_POS_ID.SelectedValue = ValueHFEUInsigYearConADMIN_POS_ID;
            ddlEUInsigYearConINSIG_TARGET.SelectedValue = ValueHFEUInsigYearConINSIG_TARGET;
            ddlEUInsigYearConINSIG_USE.SelectedValue = ValueHFEUInsigYearConINSIG_USE;
            tbEUInsigYearConINSIG_YEAR.Text = ValuelbEUInsigYearConINSIG_YEAR;

            Session["DefaultIdEUInsigYearCon"] = ValueHFEUInsigYearConID;
        }
        protected void OnDeleteEUInsigYearCon(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            int ValueID = int.Parse((item.FindControl("HFEUInsigYearConID") as HiddenField).Value);

            if (ValueID != 0)
            {
                DatabaseManager.ExecuteNonQuery("DELETE TB_INSIG_EU_INSIG_YEAR_CON WHERE IEUIYC_ID = '" + ValueID + "'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('ลบข้อมูลเรียบร้อย')", true);
                BindEUInsigYearCon();
            }
        }

    }
}