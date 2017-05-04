using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WEB_PERSONAL.Class;
using System.Data.OracleClient;
using System.Data;

namespace WEB_PERSONAL
{
    public partial class RequestManage : System.Web.UI.Page
    {
        Person loginPerson;
        string Citizen_id;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] == null)
            {
                Response.Redirect("ListRequest.aspx");
                return;
            }

            PersonnelSystem ps = PersonnelSystem.GetPersonnelSystem(this);
            loginPerson = ps.LoginPerson;
            Citizen_id = DatabaseManager.ExecuteString("SELECT CITIZEN_ID FROM TB_REQUEST WHERE R_ID = '" + MyCrypto.GetDecryptedQueryString(Request.QueryString["id"].ToString()) + "'");
            Person QueryString = DatabaseManager.GetPerson(Citizen_id);

            if (loginPerson.PERSON_ROLE_ID != "2")
            {
                Server.Transfer("NoPermission.aspx");
            }

            if (!IsPostBack)
            {
                BindDDL();
                ReadID();
                ReadRequest();
            }

            lbTitleID.Text = Util.IsBlank(QueryString.PS_TITLE_NAME) ? "-" : QueryString.PS_TITLE_NAME;
            lbFirstName.Text = Util.IsBlank(QueryString.PS_FIRSTNAME) ? "-" : QueryString.PS_FIRSTNAME;
            lbLastName.Text = Util.IsBlank(QueryString.PS_LASTNAME) ? "-" : QueryString.PS_LASTNAME;
            lbGenderID.Text = Util.IsBlank(QueryString.PS_GENDER_NAME) ? "-" : QueryString.PS_GENDER_NAME;
            lbBirthdayDate.Text = Util.IsBlank(QueryString.PS_BIRTHDAY_DATE.ToString()) ? "-" : QueryString.PS_BIRTHDAY_DATE.Value.ToLongDateString();
            lbEmail.Text = Util.IsBlank(QueryString.PS_EMAIL) ? "-" : QueryString.PS_EMAIL;
            lbNationID.Text = Util.IsBlank(QueryString.PS_NATION_NAME) ? "-" : QueryString.PS_NATION_NAME;
            lbCampusID.Text = Util.IsBlank(QueryString.PS_CAMPUS_NAME) ? "-" : QueryString.PS_CAMPUS_NAME;
            lbFacultyID.Text = Util.IsBlank(QueryString.PS_FACULTY_NAME) ? "-" : QueryString.PS_FACULTY_NAME;
            lbDivisionID.Text = Util.IsBlank(QueryString.PS_DIVISION_NAME) ? "-" : QueryString.PS_DIVISION_NAME;
            lbWorkDivisionID.Text = Util.IsBlank(QueryString.PS_WORK_DIVISION_NAME) ? "-" : QueryString.PS_WORK_DIVISION_NAME;
            lbStafftypeID.Text = Util.IsBlank(QueryString.PS_STAFFTYPE_NAME) ? "-" : QueryString.PS_STAFFTYPE_NAME;
            lbTimeContactID.Text = Util.IsBlank(QueryString.PS_TIME_CONTACT_NAME) ? "-" : QueryString.PS_TIME_CONTACT_NAME;
            lbBudgetID.Text = Util.IsBlank(QueryString.PS_BUDGET_NAME) ? "-" : QueryString.PS_BUDGET_NAME;
            lbSubStafftypeID.Text = Util.IsBlank(QueryString.PS_SUBSTAFFTYPE_NAME) ? "-" : QueryString.PS_SUBSTAFFTYPE_NAME;
            lbAdminPosID.Text = Util.IsBlank(QueryString.PS_ADMIN_POS_NAME) ? "-" : QueryString.PS_ADMIN_POS_NAME;
            lbWorkPosID.Text = Util.IsBlank(QueryString.PS_WORK_POS_NAME) ? "-" : QueryString.PS_WORK_POS_NAME;
            lbDateInwork.Text = Util.IsBlank(QueryString.PS_INWORK_DATE.ToString()) ? "-" : QueryString.PS_INWORK_DATE.Value.ToLongDateString();
            lbDateStartThisU.Text = Util.IsBlank(QueryString.PS_DATE_START_THIS_U.ToString()) ? "-" : QueryString.PS_DATE_START_THIS_U.Value.ToLongDateString();
            lbSpecialName.Text = Util.IsBlank(QueryString.PS_SPECIAL_NAME) ? "-" : QueryString.PS_SPECIAL_NAME;
            lbTeachIscedID.Text = Util.IsBlank(QueryString.PS_TEACH_ISCED_NAME) ? "-" : QueryString.PS_TEACH_ISCED_NAME;
            lbGradLevID.Text = Util.IsBlank(QueryString.PS_GRAD_LEV_NAME) ? "-" : QueryString.PS_GRAD_LEV_NAME;
            lbGradCurr.Text = Util.IsBlank(QueryString.PS_GRAD_CURR) ? "-" : QueryString.PS_GRAD_CURR;
            lbGradIscedID.Text = Util.IsBlank(QueryString.PS_GRAD_ISCED_NAME) ? "-" : QueryString.PS_GRAD_ISCED_NAME;
            lbGradProgID.Text = Util.IsBlank(QueryString.PS_GRAD_PROG_NAME) ? "-" : QueryString.PS_GRAD_PROG_NAME;
            lbGradUniv.Text = Util.IsBlank(QueryString.PS_GRAD_UNIV) ? "-" : QueryString.PS_GRAD_UNIV;
            lbGradCountryID.Text = Util.IsBlank(QueryString.PS_GRAD_COUNTRY_NAME) ? "-" : QueryString.PS_GRAD_COUNTRY_NAME;
            lbDeformID.Text = Util.IsBlank(QueryString.PS_DEFORM_NAME) ? "-" : QueryString.PS_DEFORM_NAME;
            lbReligionID.Text = Util.IsBlank(QueryString.PS_RELIGION_NAME) ? "-" : QueryString.PS_RELIGION_NAME;
        }

        protected void ReadID()
        {
            using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
            {
                con.Open();
                using (OracleCommand com = new OracleCommand("SELECT TITLE_ID, FIRSTNAME, LASTNAME, GENDER_ID, BIRTHDAY_DATE, EMAIL, NATION_ID, CAMPUS_ID, FACULTY_ID, DIVISION_ID, WORK_DIVISION_ID, STAFFTYPE_ID, TIME_CONTACT_ID, BUDGET_ID, SUBSTAFFTYPE_ID, ADMIN_POS_ID, WORK_POS_ID, INWORK_DATE, DATE_START_THIS_U, SPECIAL_NAME, TEACH_ISCED_ID, GRAD_LEV_ID, GRAD_CURR, GRAD_ISCED_ID, GRAD_PROG_ID, GRAD_UNIV, GRAD_COUNTRY_ID, DEFORM_ID, RELIGION_ID, (SELECT TITLE_NAME_TH FROM TB_TITLENAME WHERE TB_TITLENAME.TITLE_ID = TB_REQUEST.TITLE_ID) TITLE_NAME, (SELECT GENDER_NAME FROM TB_GENDER WHERE TB_GENDER.GENDER_ID = TB_REQUEST.GENDER_ID) GENDER_NAME, (SELECT NATION_NAME_EN FROM TB_NATION WHERE TB_NATION.NATION_ID = TB_REQUEST.NATION_ID) NATION_NAME, (SELECT CAMPUS_NAME FROM TB_CAMPUS WHERE TB_CAMPUS.CAMPUS_ID = TB_REQUEST.CAMPUS_ID) CAMPUS_NAME, (SELECT FACULTY_NAME FROM TB_FACULTY WHERE TB_FACULTY.FACULTY_ID = TB_REQUEST.FACULTY_ID) FACULTY_NAME, (SELECT DIVISION_NAME FROM TB_DIVISION WHERE TB_DIVISION.DIVISION_ID = TB_REQUEST.DIVISION_ID) DIVISION_NAME, (SELECT WORK_NAME FROM TB_WORK_DIVISION WHERE TB_WORK_DIVISION.WORK_ID = TB_REQUEST.WORK_DIVISION_ID) WORK_DIVISION_NAME, (SELECT STAFFTYPE_NAME FROM TB_STAFFTYPE WHERE TB_STAFFTYPE.STAFFTYPE_ID = TB_REQUEST.STAFFTYPE_ID) STAFFTYPE_NAME, (SELECT TIME_CONTACT_NAME FROM TB_TIME_CONTACT WHERE TB_TIME_CONTACT.TIME_CONTACT_ID = TB_REQUEST.TIME_CONTACT_ID) TIME_CONTACT_NAME, (SELECT BUDGET_NAME FROM TB_BUDGET WHERE TB_BUDGET.BUDGET_ID = TB_REQUEST.BUDGET_ID) BUDGET_NAME, (SELECT SUBSTAFFTYPE_NAME FROM TB_SUBSTAFFTYPE WHERE TB_SUBSTAFFTYPE.SUBSTAFFTYPE_ID = TB_REQUEST.SUBSTAFFTYPE_ID) SUBSTAFFTYPE_NAME, (SELECT ADMIN_POSITION_NAME FROM TB_ADMIN_POSITION WHERE TB_ADMIN_POSITION.ADMIN_POSITION_ID = TB_REQUEST.ADMIN_POS_ID) ADMIN_POSITION_NAME, (SELECT POSITION_WORK_NAME FROM TB_POSITION_WORK WHERE TB_POSITION_WORK.POSITION_WORK_ID = TB_REQUEST.WORK_POS_ID) WORK_POS_NAME, (SELECT ISCED_NAME FROM TB_ISCED WHERE TB_ISCED.ISCED_ID = TB_REQUEST.TEACH_ISCED_ID) TEACH_ISCED_NAME, (SELECT LEV_NAME_TH FROM TB_LEV WHERE TB_LEV.LEV_ID = TB_REQUEST.GRAD_LEV_ID) GRAD_LEV_NAME, (SELECT ISCED_NAME FROM TB_ISCED WHERE TB_ISCED.ISCED_ID = TB_REQUEST.GRAD_ISCED_ID) GRAD_ISCED_NAME, (SELECT PROGRAM_NAME FROM TB_PROGRAM WHERE TB_PROGRAM.PROGRAM_ID_NEW = TB_REQUEST.GRAD_PROG_ID) GRAD_PROG_NAME, (SELECT NATION_NAME_EN FROM TB_NATION WHERE TB_NATION.NATION_ID = TB_REQUEST.GRAD_COUNTRY_ID) GRAD_COUNTRY_NAME, (SELECT DEFORM_NAME FROM TB_DEFORM WHERE TB_DEFORM.DEFORM_ID = TB_REQUEST.DEFORM_ID) DEFORM_NAME, (SELECT RELIGION_NAME FROM TB_RELIGION WHERE TB_RELIGION.RELIGION_ID = TB_REQUEST.RELIGION_ID) RELIGION_NAME FROM TB_REQUEST WHERE R_ID = '" + MyCrypto.GetDecryptedQueryString(Request.QueryString["id"].ToString()) + "'", con))
                {
                    using (OracleDataReader reader = com.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int i = 0;
                            if (reader.IsDBNull(i)) { trTitleID.Visible = false; } else { ddlTitleID.SelectedValue = reader.GetValue(i).ToString(); ddlTitleID2.Text = reader.GetValue(29).ToString(); }
                            ++i;
                            if (reader.IsDBNull(i)) { trFirstName.Visible = false; } else { tbFirstName.Text = reader.GetValue(i).ToString(); tbFirstName2.Text = reader.GetValue(i).ToString(); }
                            ++i;
                            if (reader.IsDBNull(i)) { trLastName.Visible = false; } else { tbLastName.Text = reader.GetValue(i).ToString(); tbLastName2.Text = reader.GetValue(i).ToString(); }
                            ++i;
                            if (reader.IsDBNull(i)) { trGenderID.Visible = false; } else { ddlGenderID.SelectedValue = reader.GetValue(i).ToString(); ddlGenderID2.Text = reader.GetValue(30).ToString(); }
                            ++i;
                            if (reader.IsDBNull(i)) { trBirthdayDate.Visible = false; } else { tbBirthdayDate.Text = reader.GetDateTime(i).ToString("dd MMM yyyy"); tbBirthdayDate2.Text = reader.GetDateTime(i).ToString("dd MMM yyyy"); }
                            ++i;
                            if (reader.IsDBNull(i)) { trEmail.Visible = false; } else { tbEmail.Text = reader.GetValue(i).ToString(); tbEmail2.Text = reader.GetValue(i).ToString(); }
                            ++i;
                            if (reader.IsDBNull(i)) { trNationID.Visible = false; } else { ddlNationID.SelectedValue = reader.GetValue(i).ToString(); ddlNationID2.Text = reader.GetValue(31).ToString(); }
                            ++i;
                            if (reader.IsDBNull(i)) { trCampusID.Visible = false; } else { ddlCampusID.SelectedValue = reader.GetValue(i).ToString(); ddlCampusID2.Text = reader.GetValue(32).ToString(); }
                            ++i;

                            if (reader.IsDBNull(i)) { trFacultyID.Visible = false; }
                            else
                            {
                                ddlFacultyID.Items.Clear();
                                string z1 = reader.GetValue(i).ToString(); 
                                DatabaseManager.BindDropDown(ddlFacultyID, "SELECT * FROM TB_FACULTY WHERE CAMPUS_ID = '" + ddlCampusID.SelectedValue + "'", "FACULTY_NAME", "FACULTY_ID", "--กรุณาเลือก--");
                                ddlFacultyID.SelectedValue = z1;
                                ddlFacultyID2.Text = reader.GetValue(33).ToString();
                            }
                            ++i;
                            if (reader.IsDBNull(i)) { trDivisionID.Visible = false; }
                            else
                            {
                                ddlDivisionID.Items.Clear();
                                string z2 = reader.GetValue(i).ToString(); 
                                DatabaseManager.BindDropDown(ddlDivisionID, "SELECT * FROM TB_DIVISION WHERE DIVISION_ID = '" + ddlDivisionID.SelectedValue + "'", "DIVISION_NAME", "DIVISION_ID", "--กรุณาเลือก--");
                                ddlDivisionID.SelectedValue = z2;
                                ddlDivisionID2.Text = reader.GetValue(34).ToString();
                            }
                            ++i;
                            if (reader.IsDBNull(i)) { trWorkDivisionID.Visible = false; }
                            else
                            {
                                ddlWorkDivisionID.Items.Clear();
                                string z3 = reader.GetValue(i).ToString(); 
                                DatabaseManager.BindDropDown(ddlWorkDivisionID, "SELECT * FROM TB_WORK_DIVISION WHERE WORK_ID = '" + ddlWorkDivisionID.SelectedValue + "'", "WORK_NAME", "WORK_ID", "--กรุณาเลือก--");
                                ddlWorkDivisionID.SelectedValue = z3;
                                ddlWorkDivisionID2.Text = reader.GetValue(35).ToString();
                            }
                            ++i;

                            if (reader.IsDBNull(i)) { trStafftypeID.Visible = false; } else { ddlStafftypeID.SelectedValue = reader.GetValue(i).ToString(); ddlStafftypeID2.Text = reader.GetValue(36).ToString(); }
                            ++i;
                            if (reader.IsDBNull(i)) { trTimeContactID.Visible = false; } else { ddlTimeContactID.SelectedValue = reader.GetValue(i).ToString(); ddlTimeContactID2.Text = reader.GetValue(37).ToString(); }
                            ++i;
                            if (reader.IsDBNull(i)) { trBudgetID.Visible = false; } else { ddlBudgetID.SelectedValue = reader.GetValue(i).ToString(); ddlBudgetID2.Text = reader.GetValue(38).ToString(); }
                            ++i;
                            if (reader.IsDBNull(i)) { trSubStafftypeID.Visible = false; } else { ddlSubStafftypeID.SelectedValue = reader.GetValue(i).ToString(); ddlSubStafftypeID2.Text = reader.GetValue(39).ToString(); }
                            ++i;
                            if (reader.IsDBNull(i)) { trAdminPosID.Visible = false; } else { ddlAdminPosID.SelectedValue = reader.GetValue(i).ToString(); ddlAdminPosID2.Text = reader.GetValue(40).ToString(); }
                            ++i;
                            if (reader.IsDBNull(i)) { trWorkPosID.Visible = false; } else { ddlWorkPosID.SelectedValue = reader.GetValue(i).ToString(); ddlWorkPosID2.Text = reader.GetValue(41).ToString(); }
                            ++i;
                            if (reader.IsDBNull(i)) { trDateInwork.Visible = false; } else { tbDateInwork.Text = reader.GetDateTime(i).ToString("dd MMM yyyy"); tbDateInwork2.Text = reader.GetDateTime(i).ToString("dd MMM yyyy"); }
                            ++i;
                            if (reader.IsDBNull(i)) { trDateStartThisU.Visible = false; } else { tbDateStartThisU.Text = reader.GetDateTime(i).ToString("dd MMM yyyy"); tbDateStartThisU2.Text = reader.GetDateTime(i).ToString("dd MMM yyyy"); }
                            ++i;
                            if (reader.IsDBNull(i)) { trSpecialName.Visible = false; } else { tbSpecialName.Text = reader.GetValue(i).ToString(); tbSpecialName2.Text = reader.GetValue(i).ToString(); }
                            ++i;
                            if (reader.IsDBNull(i)) { trTeachIscedID.Visible = false; } else { ddlTeachIscedID.SelectedValue = reader.GetValue(i).ToString(); ddlTeachIscedID2.Text = reader.GetValue(42).ToString(); }
                            ++i;
                            if (reader.IsDBNull(i)) { trGradLevID.Visible = false; } else { ddlGradLevID.SelectedValue = reader.GetValue(i).ToString(); ddlGradLevID2.Text = reader.GetValue(43).ToString(); }
                            ++i;
                            if (reader.IsDBNull(i)) { trGradCurr.Visible = false; } else { tbGradCurr.Text = reader.GetValue(i).ToString(); tbGradCurr2.Text = reader.GetValue(i).ToString(); }
                            ++i;
                            if (reader.IsDBNull(i)) { trGradIscedID.Visible = false; } else { ddlGradIscedID.SelectedValue = reader.GetValue(i).ToString(); ddlGradIscedID2.Text = reader.GetValue(44).ToString(); }
                            ++i;
                            if (reader.IsDBNull(i)) { trGradProgID.Visible = false; } else { ddlGradProgID.SelectedValue = reader.GetValue(i).ToString(); ddlGradProgID2.Text = reader.GetValue(45).ToString(); }
                            ++i;
                            if (reader.IsDBNull(i)) { trGradUniv.Visible = false; } else { tbGradUniv.Text = reader.GetValue(i).ToString(); tbGradUniv2.Text = reader.GetValue(i).ToString(); }
                            ++i;
                            if (reader.IsDBNull(i)) { trGradCountryID.Visible = false; } else { ddlGradCountryID.SelectedValue = reader.GetValue(i).ToString(); ddlGradCountryID2.Text = reader.GetValue(46).ToString(); }
                            ++i;
                            if (reader.IsDBNull(i)) { trDeformID.Visible = false; } else { ddlDeformID.SelectedValue = reader.GetValue(i).ToString(); ddlDeformID2.Text = reader.GetValue(47).ToString(); }
                            ++i;
                            if (reader.IsDBNull(i)) { trReligionID.Visible = false; } else { ddlReligionID.SelectedValue = reader.GetValue(i).ToString(); ddlReligionID2.Text = reader.GetValue(48).ToString(); }
                            ++i;

                        }
                    }
                }
            }
        }

        protected void ReadRequest()
        {
            using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
            {
                con.Open();
                using (OracleCommand com = new OracleCommand("SELECT DATE_START FROM TB_REQUEST WHERE R_ID = '" + MyCrypto.GetDecryptedQueryString(Request.QueryString["id"].ToString()) + "'", con))
                {
                    using (OracleDataReader reader = com.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lbDateReq.Text = reader.GetDateTime(0).ToLongDateString();
                        }
                    }
                }
            }
        }

        protected void BindDDL()
        {
            DatabaseManager.BindDropDown(ddlTitleID, "SELECT * FROM TB_TITLENAME ORDER BY ABS(TITLE_ID) ASC", "TITLE_NAME_TH", "TITLE_ID", "--กรุณาเลือก--");
            DatabaseManager.BindDropDown(ddlGenderID, "SELECT * FROM TB_GENDER ORDER BY ABS(GENDER_ID) ASC", "GENDER_NAME", "GENDER_ID", "--กรุณาเลือก--");
            DatabaseManager.BindDropDown(ddlNationID, "SELECT * FROM TB_NATION ORDER BY ABS(NATION_ID) ASC", "NATION_NAME_EN", "NATION_ID", "--กรุณาเลือก--");
            DatabaseManager.BindDropDown(ddlStafftypeID, "SELECT * FROM TB_STAFFTYPE ORDER BY ABS(STAFFTYPE_ID) ASC", "STAFFTYPE_NAME", "STAFFTYPE_ID", "--กรุณาเลือก--");
            DatabaseManager.BindDropDown(ddlTimeContactID, "SELECT * FROM TB_TIME_CONTACT ORDER BY ABS(TIME_CONTACT_ID) ASC", "TIME_CONTACT_NAME", "TIME_CONTACT_ID", "--กรุณาเลือก--");
            DatabaseManager.BindDropDown(ddlBudgetID, "SELECT * FROM TB_BUDGET ORDER BY ABS(BUDGET_ID) ASC", "BUDGET_NAME", "BUDGET_ID", "--กรุณาเลือก--");
            DatabaseManager.BindDropDown(ddlSubStafftypeID, "SELECT * FROM TB_SUBSTAFFTYPE ORDER BY ABS(SUBSTAFFTYPE_ID) ASC", "SUBSTAFFTYPE_NAME", "SUBSTAFFTYPE_ID", "--กรุณาเลือก--");
            DatabaseManager.BindDropDown(ddlAdminPosID, "SELECT * FROM TB_ADMIN_POSITION ORDER BY ABS(ADMIN_POSITION_ID) ASC", "ADMIN_POSITION_NAME", "ADMIN_POSITION_ID", "--กรุณาเลือก--");
            DatabaseManager.BindDropDown(ddlWorkPosID, "SELECT * FROM TB_POSITION_WORK ORDER BY ABS(POSITION_WORK_ID) ASC", "POSITION_WORK_NAME", "POSITION_WORK_ID", "--กรุณาเลือก--");
            DatabaseManager.BindDropDown(ddlTeachIscedID, "SELECT * FROM TB_ISCED ORDER BY ABS(ISCED_ID) ASC", "ISCED_NAME", "ISCED_ID", "--กรุณาเลือก--");
            DatabaseManager.BindDropDown(ddlGradLevID, "SELECT * FROM TB_LEV ORDER BY ABS(LEV_ID) ASC", "LEV_NAME_TH", "LEV_ID", "--กรุณาเลือก--");
            DatabaseManager.BindDropDown(ddlGradIscedID, "SELECT * FROM TB_ISCED ORDER BY ABS(ISCED_ID) ASC", "ISCED_NAME", "ISCED_ID", "--กรุณาเลือก--");
            DatabaseManager.BindDropDown(ddlGradProgID, "SELECT * FROM TB_PROGRAM ORDER BY ABS(PROGRAM_ID_NEW) ASC", "PROGRAM_NAME", "PROGRAM_ID_NEW", "--กรุณาเลือก--");
            DatabaseManager.BindDropDown(ddlGradCountryID, "SELECT * FROM TB_NATION ORDER BY ABS(NATION_ID) ASC", "NATION_NAME_EN", "NATION_ID", "--กรุณาเลือก--");
            DatabaseManager.BindDropDown(ddlDeformID, "SELECT * FROM TB_DEFORM ORDER BY ABS(DEFORM_ID) ASC", "DEFORM_NAME", "DEFORM_ID", "--กรุณาเลือก--");
            DatabaseManager.BindDropDown(ddlReligionID, "SELECT * FROM TB_RELIGION ORDER BY ABS(RELIGION_ID) ASC", "RELIGION_NAME", "RELIGION_ID", "--กรุณาเลือก--");

            SQLCampus();
        }

        //Campus
        protected void SQLCampus()
        {
            try
            {
                using (OracleConnection sqlConn = new OracleConnection(DatabaseManager.CONNECTION_STRING))
                {
                    using (OracleCommand sqlCmd = new OracleCommand())
                    {
                        sqlCmd.CommandText = "select * from TB_CAMPUS";
                        sqlCmd.Connection = sqlConn;
                        sqlConn.Open();
                        OracleDataAdapter da = new OracleDataAdapter(sqlCmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        ddlCampusID.DataSource = dt;
                        ddlCampusID.DataValueField = "CAMPUS_ID";
                        ddlCampusID.DataTextField = "CAMPUS_NAME";
                        ddlCampusID.DataBind();
                        sqlConn.Close();

                        ddlCampusID.Items.Insert(0, new ListItem("--กรุณาเลือกวิทยาเขต--", ""));
                        ddlFacultyID.Items.Insert(0, new ListItem("--กรุณาเลือกสำนัก/สถาบัน/คณะ--", ""));
                        ddlDivisionID.Items.Insert(0, new ListItem("--กรุณาเลือกกอง/สำนักงานเลขา/ภาควิชา--", ""));
                        ddlWorkDivisionID.Items.Insert(0, new ListItem("--กรุณาเลือกงาน/ฝ่าย--", ""));
                    }
                }
            }
            catch { }
        }

        protected void ddlCampusID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                using (OracleConnection sqlConn = new OracleConnection(DatabaseManager.CONNECTION_STRING))
                {
                    using (OracleCommand sqlCmd = new OracleCommand())
                    {
                        sqlCmd.CommandText = "select * from TB_FACULTY where CAMPUS_ID = :CAMPUS_ID";
                        sqlCmd.Parameters.Add(":CAMPUS_ID", ddlCampusID.SelectedValue);
                        sqlCmd.Connection = sqlConn;
                        sqlConn.Open();
                        OracleDataAdapter da = new OracleDataAdapter(sqlCmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        ddlFacultyID.DataSource = dt;
                        ddlFacultyID.DataValueField = "FACULTY_ID";
                        ddlFacultyID.DataTextField = "FACULTY_NAME";
                        ddlFacultyID.DataBind();
                        sqlConn.Close();

                        ddlFacultyID.Items.Insert(0, new ListItem("--กรุณาเลือกสำนัก/สถาบัน/คณะ--", ""));
                        ddlDivisionID.Items.Clear();
                        ddlDivisionID.Items.Insert(0, new ListItem("--กรุณาเลือกกอง/สำนักงานเลขา/ภาควิชา--", ""));
                        ddlWorkDivisionID.Items.Clear();
                        ddlWorkDivisionID.Items.Insert(0, new ListItem("--กรุณาเลือกงาน/ฝ่าย--", ""));
                    }
                }
            }
            catch { }
        }

        protected void ddlFacultyID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                using (OracleConnection sqlConn = new OracleConnection(DatabaseManager.CONNECTION_STRING))
                {
                    using (OracleCommand sqlCmd = new OracleCommand())
                    {
                        sqlCmd.CommandText = "select * from TB_DIVISION where FACULTY_ID = :FACULTY_ID";
                        sqlCmd.Parameters.Add(":FACULTY_ID", ddlFacultyID.SelectedValue);
                        sqlCmd.Connection = sqlConn;
                        sqlConn.Open();
                        OracleDataAdapter da = new OracleDataAdapter(sqlCmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        ddlDivisionID.DataSource = dt;
                        ddlDivisionID.DataValueField = "DIVISION_ID";
                        ddlDivisionID.DataTextField = "DIVISION_NAME";
                        ddlDivisionID.DataBind();
                        sqlConn.Close();

                        ddlDivisionID.Items.Insert(0, new ListItem("--กรุณาเลือกกอง/สำนักงานเลขา/ภาควิชา--", ""));
                        ddlWorkDivisionID.Items.Clear();
                        ddlWorkDivisionID.Items.Insert(0, new ListItem("--กรุณาเลือกงาน/ฝ่าย--", ""));
                    }
                }
            }
            catch { }
        }

        protected void ddlDivisionID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                using (OracleConnection sqlConn = new OracleConnection(DatabaseManager.CONNECTION_STRING))
                {
                    using (OracleCommand sqlCmd = new OracleCommand())
                    {
                        sqlCmd.CommandText = "select * from TB_WORK_DIVISION where DIVISION_ID = " + ddlDivisionID.SelectedValue;
                        sqlCmd.Connection = sqlConn;
                        sqlConn.Open();
                        OracleDataAdapter da = new OracleDataAdapter(sqlCmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        ddlWorkDivisionID.DataSource = dt;
                        ddlWorkDivisionID.DataValueField = "WORK_ID";
                        ddlWorkDivisionID.DataTextField = "WORK_NAME";
                        ddlWorkDivisionID.DataBind();
                        sqlConn.Close();

                        ddlWorkDivisionID.Items.Insert(0, new ListItem("--กรุณาเลือกงาน / ฝ่าย--"));
                    }
                }
            }
            catch { }

            using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
            {
                con.Open();
                using (OracleCommand com = new OracleCommand("SELECT COUNT(*) FROM TB_WORK_DIVISION WHERE DIVISION_ID = " + ddlDivisionID.SelectedValue, con))
                {
                    using (OracleDataReader reader = com.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader.GetInt32(0) == 0)
                            {
                                ddlWorkDivisionID.Visible = false;
                                trWorkDivisionID.Visible = false;
                            }
                            else
                            {
                                ddlWorkDivisionID.Visible = true;
                                trWorkDivisionID.Visible = true;
                            }
                        }
                    }
                }
            }
        }

        public int UPDATE_PERSON()
        {
            int id = 0;
            OracleConnection.ClearAllPools();
            using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
            {
                con.Open();           
                if (trTitleID.Visible == true)
                {
                    using (OracleCommand com = new OracleCommand("UPDATE PS_PERSON SET PS_TITLE_ID=:PS_TITLE_ID WHERE PS_CITIZEN_ID = '" + Citizen_id + "'", con))
                    {
                        com.Parameters.Add(new OracleParameter("PS_TITLE_ID", ddlTitleID.SelectedValue));
                        com.ExecuteNonQuery();
                    }
                }
                if (trFirstName.Visible == true)
                {
                    using (OracleCommand com = new OracleCommand("UPDATE PS_PERSON SET PS_FIRSTNAME=:PS_FIRSTNAME WHERE PS_CITIZEN_ID = '" + Citizen_id + "'", con))
                    {
                        com.Parameters.Add(new OracleParameter("PS_FIRSTNAME", tbFirstName.Text));
                        com.ExecuteNonQuery();
                    }
                }
                if (trLastName.Visible == true)
                {
                    using (OracleCommand com = new OracleCommand("UPDATE PS_PERSON SET PS_LASTNAME=:PS_LASTNAME WHERE PS_CITIZEN_ID = '" + Citizen_id + "'", con))
                    {
                        com.Parameters.Add(new OracleParameter("PS_LASTNAME", tbLastName.Text));
                        com.ExecuteNonQuery();
                    }
                }
                if (trGenderID.Visible == true)
                {
                    using (OracleCommand com = new OracleCommand("UPDATE PS_PERSON SET PS_GENDER_ID=:PS_GENDER_ID WHERE PS_CITIZEN_ID = '" + Citizen_id + "'", con))
                    {
                        com.Parameters.Add(new OracleParameter("PS_GENDER_ID", ddlGenderID.SelectedValue));
                        com.ExecuteNonQuery();
                    }
                }
                if (trBirthdayDate.Visible == true)
                {
                    using (OracleCommand com = new OracleCommand("UPDATE PS_PERSON SET PS_BIRTHDAY_DATE=:PS_BIRTHDAY_DATE WHERE PS_CITIZEN_ID = '" + Citizen_id + "'", con))
                    {
                        com.Parameters.Add(new OracleParameter("PS_BIRTHDAY_DATE", Util.ToDateTimeOracle(tbBirthdayDate.Text)));
                        com.ExecuteNonQuery();
                    }
                }
                if (trEmail.Visible == true)
                {
                    using (OracleCommand com = new OracleCommand("UPDATE PS_PERSON SET PS_EMAIL=:PS_EMAIL WHERE PS_CITIZEN_ID = '" + Citizen_id + "'", con))
                    {
                        com.Parameters.Add(new OracleParameter("PS_EMAIL", tbEmail.Text));
                        com.ExecuteNonQuery();
                    }
                }
                if (trNationID.Visible == true)
                {
                    using (OracleCommand com = new OracleCommand("UPDATE PS_PERSON SET PS_NATION_ID=:PS_NATION_ID WHERE PS_CITIZEN_ID = '" + Citizen_id + "'", con))
                    {
                        com.Parameters.Add(new OracleParameter("PS_NATION_ID", ddlNationID.SelectedValue));
                        com.ExecuteNonQuery();
                    }
                }
                if (trCampusID.Visible == true)
                {
                    using (OracleCommand com = new OracleCommand("UPDATE PS_PERSON SET PS_CAMPUS_ID=:PS_CAMPUS_ID WHERE PS_CITIZEN_ID = '" + Citizen_id + "'", con))
                    {
                        com.Parameters.Add(new OracleParameter("PS_CAMPUS_ID", ddlCampusID.SelectedValue));
                        com.ExecuteNonQuery();
                    }
                }
                if (trFacultyID.Visible == true)
                {
                    using (OracleCommand com = new OracleCommand("UPDATE PS_PERSON SET PS_FACULTY_ID=:PS_FACULTY_ID WHERE PS_CITIZEN_ID = '" + Citizen_id + "'", con))
                    {
                        com.Parameters.Add(new OracleParameter("PS_FACULTY_ID", ddlFacultyID.SelectedValue));
                        com.ExecuteNonQuery();
                    }
                }
                if (trDivisionID.Visible == true)
                {
                    using (OracleCommand com = new OracleCommand("UPDATE PS_PERSON SET PS_DIVISION_ID=:PS_DIVISION_ID WHERE PS_CITIZEN_ID = '" + Citizen_id + "'", con))
                    {
                        com.Parameters.Add(new OracleParameter("PS_DIVISION_ID", ddlDivisionID.SelectedValue));
                        com.ExecuteNonQuery();
                    }
                }
                if (trWorkDivisionID.Visible == true)
                {
                    using (OracleCommand com = new OracleCommand("UPDATE PS_PERSON SET PS_WORK_DIVISION_ID=:PS_WORK_DIVISION_ID WHERE PS_CITIZEN_ID = '" + Citizen_id + "'", con))
                    {
                        com.Parameters.Add(new OracleParameter("PS_WORK_DIVISION_ID", ddlWorkDivisionID.SelectedValue));
                        com.ExecuteNonQuery();
                    }
                }
                if (trStafftypeID.Visible == true)
                {
                    using (OracleCommand com = new OracleCommand("UPDATE PS_PERSON SET PS_STAFFTYPE_ID=:PS_STAFFTYPE_ID WHERE PS_CITIZEN_ID = '" + Citizen_id + "'", con))
                    {
                        com.Parameters.Add(new OracleParameter("PS_STAFFTYPE_ID", ddlStafftypeID.SelectedValue));
                        com.ExecuteNonQuery();
                    }
                }
                if (trTimeContactID.Visible == true)
                {
                    using (OracleCommand com = new OracleCommand("UPDATE PS_PERSON SET PS_TIME_CONTACT_ID=:PS_TIME_CONTACT_ID WHERE PS_CITIZEN_ID = '" + Citizen_id + "'", con))
                    {
                        com.Parameters.Add(new OracleParameter("PS_TIME_CONTACT_ID", ddlTimeContactID.SelectedValue));
                        com.ExecuteNonQuery();
                    }
                }
                if (trBudgetID.Visible == true)
                {
                    using (OracleCommand com = new OracleCommand("UPDATE PS_PERSON SET PS_BUDGET_ID=:PS_BUDGET_ID WHERE PS_CITIZEN_ID = '" + Citizen_id + "'", con))
                    {
                        com.Parameters.Add(new OracleParameter("PS_BUDGET_ID", ddlBudgetID.SelectedValue));
                        com.ExecuteNonQuery();
                    }
                }
                if (trSubStafftypeID.Visible == true)
                {
                    using (OracleCommand com = new OracleCommand("UPDATE PS_PERSON SET PS_SUBSTAFFTYPE_ID=:PS_SUBSTAFFTYPE_ID WHERE PS_CITIZEN_ID = '" + Citizen_id + "'", con))
                    {
                        com.Parameters.Add(new OracleParameter("PS_SUBSTAFFTYPE_ID", ddlSubStafftypeID.SelectedValue));
                        com.ExecuteNonQuery();
                    }
                }
                if (trAdminPosID.Visible == true)
                {
                    using (OracleCommand com = new OracleCommand("UPDATE PS_PERSON SET PS_ADMIN_POS_ID=:PS_ADMIN_POS_ID WHERE PS_CITIZEN_ID = '" + Citizen_id + "'", con))
                    {
                        com.Parameters.Add(new OracleParameter("PS_ADMIN_POS_ID", ddlAdminPosID.SelectedValue));
                        com.ExecuteNonQuery();
                    }
                }
                if (trWorkPosID.Visible == true)
                {
                    using (OracleCommand com = new OracleCommand("UPDATE PS_PERSON SET PS_WORK_POS_ID=:PS_WORK_POS_ID WHERE PS_CITIZEN_ID = '" + Citizen_id + "'", con))
                    {
                        com.Parameters.Add(new OracleParameter("PS_WORK_POS_ID", ddlWorkPosID.SelectedValue));
                        com.ExecuteNonQuery();
                    }
                }
                if (trDateInwork.Visible == true)
                {
                    using (OracleCommand com = new OracleCommand("UPDATE PS_PERSON SET PS_INWORK_DATE=:PS_INWORK_DATE WHERE PS_CITIZEN_ID = '" + Citizen_id + "'", con))
                    {
                        com.Parameters.Add(new OracleParameter("PS_INWORK_DATE", Util.ToDateTimeOracle(tbDateInwork.Text)));
                        com.ExecuteNonQuery();
                    }
                }
                if (trDateStartThisU.Visible == true)
                {
                    using (OracleCommand com = new OracleCommand("UPDATE PS_PERSON SET PS_DATE_START_THIS_U=:PS_DATE_START_THIS_U WHERE PS_CITIZEN_ID = '" + Citizen_id + "'", con))
                    {
                        com.Parameters.Add(new OracleParameter("PS_DATE_START_THIS_U", Util.ToDateTimeOracle(tbDateStartThisU.Text)));
                        com.ExecuteNonQuery();
                    }
                }
                if (trSpecialName.Visible == true)
                {
                    using (OracleCommand com = new OracleCommand("UPDATE PS_PERSON SET PS_SPECIAL_NAME=:PS_SPECIAL_NAME WHERE PS_CITIZEN_ID = '" + Citizen_id + "'", con))
                    {
                        com.Parameters.Add(new OracleParameter("PS_SPECIAL_NAME", tbSpecialName.Text));
                        com.ExecuteNonQuery();
                    }
                }
                if (trTeachIscedID.Visible == true)
                {
                    using (OracleCommand com = new OracleCommand("UPDATE PS_PERSON SET PS_TEACH_ISCED_ID=:PS_TEACH_ISCED_ID WHERE PS_CITIZEN_ID = '" + Citizen_id + "'", con))
                    {
                        com.Parameters.Add(new OracleParameter("PS_TEACH_ISCED_ID", ddlTeachIscedID.SelectedValue));
                        com.ExecuteNonQuery();
                    }
                }
                if (trGradLevID.Visible == true)
                {
                    using (OracleCommand com = new OracleCommand("UPDATE PS_PERSON SET PS_GRAD_LEV_ID=:PS_GRAD_LEV_ID WHERE PS_CITIZEN_ID = '" + Citizen_id + "'", con))
                    {
                        com.Parameters.Add(new OracleParameter("PS_GRAD_LEV_ID", ddlGradLevID.SelectedValue));
                        com.ExecuteNonQuery();
                    }
                }
                if (trGradCurr.Visible == true)
                {
                    using (OracleCommand com = new OracleCommand("UPDATE PS_PERSON SET PS_GRAD_CURR=:PS_GRAD_CURR WHERE PS_CITIZEN_ID = '" + Citizen_id + "'", con))
                    {
                        com.Parameters.Add(new OracleParameter("PS_GRAD_CURR", tbGradCurr.Text));
                        com.ExecuteNonQuery();
                    }
                }
                if (trGradIscedID.Visible == true)
                {
                    using (OracleCommand com = new OracleCommand("UPDATE PS_PERSON SET PS_GRAD_ISCED_ID=:PS_GRAD_ISCED_ID WHERE PS_CITIZEN_ID = '" + Citizen_id + "'", con))
                    {
                        com.Parameters.Add(new OracleParameter("PS_GRAD_ISCED_ID", ddlGradIscedID.SelectedValue));
                        com.ExecuteNonQuery();
                    }
                }
                if (trGradProgID.Visible == true)
                {
                    using (OracleCommand com = new OracleCommand("UPDATE PS_PERSON SET PS_GRAD_PROG_ID=:PS_GRAD_PROG_ID WHERE PS_CITIZEN_ID = '" + Citizen_id + "'", con))
                    {
                        com.Parameters.Add(new OracleParameter("PS_GRAD_PROG_ID", ddlGradProgID.SelectedValue));
                        com.ExecuteNonQuery();
                    }
                }
                if (trGradUniv.Visible == true)
                {
                    using (OracleCommand com = new OracleCommand("UPDATE PS_PERSON SET PS_GRAD_UNIV=:PS_GRAD_UNIV WHERE PS_CITIZEN_ID = '" + Citizen_id + "'", con))
                    {
                        com.Parameters.Add(new OracleParameter("PS_GRAD_UNIV", tbGradUniv.Text));
                        com.ExecuteNonQuery();
                    }
                }
                if (trGradCountryID.Visible == true)
                {
                    using (OracleCommand com = new OracleCommand("UPDATE PS_PERSON SET PS_GRAD_COUNTRY_ID=:PS_GRAD_COUNTRY_ID WHERE PS_CITIZEN_ID = '" + Citizen_id + "'", con))
                    {
                        com.Parameters.Add(new OracleParameter("PS_GRAD_COUNTRY_ID", ddlGradCountryID.SelectedValue));
                        com.ExecuteNonQuery();
                    }
                }
                if (trDeformID.Visible == true)
                {
                    using (OracleCommand com = new OracleCommand("UPDATE PS_PERSON SET PS_DEFORM_ID=:PS_DEFORM_ID WHERE PS_CITIZEN_ID = '" + Citizen_id + "'", con))
                    {
                        com.Parameters.Add(new OracleParameter("PS_DEFORM_ID", ddlDeformID.SelectedValue));
                        com.ExecuteNonQuery();
                    }
                }
                if (trReligionID.Visible == true)
                {
                    using (OracleCommand com = new OracleCommand("UPDATE PS_PERSON SET PS_RELIGION_ID=:PS_RELIGION_ID WHERE PS_CITIZEN_ID = '" + Citizen_id + "'", con))
                    {
                        com.Parameters.Add(new OracleParameter("PS_RELIGION_ID", ddlReligionID.SelectedValue));
                        com.ExecuteNonQuery();
                    }
                }

                /*/*/
                using (OracleCommand com = new OracleCommand("UPDATE TB_REQUEST SET DATE_END=:DATE_END, R_STATUS_ID=:R_STATUS_ID, COMMENT_INFO=:COMMENT_INFO, R_ALLOW=:R_ALLOW WHERE R_ID = '" + MyCrypto.GetDecryptedQueryString(Request.QueryString["id"].ToString()) + "'", con))
                {
                    com.Parameters.Add(new OracleParameter("DATE_END", DateTime.Today));
                    com.Parameters.Add(new OracleParameter("R_STATUS_ID", "2"));
                    com.Parameters.Add(new OracleParameter("COMMENT_INFO", tbComment.Text));
                    com.Parameters.Add(new OracleParameter("R_ALLOW", "1"));
                    id = com.ExecuteNonQuery();
                }
            }
            return id;
        }

        protected void lbuAllow_Click(object sender, EventArgs e)
        {
            if (rbAllow.Checked)
            {
                UPDATE_PERSON();
                DataShow.Visible = false;
                Accept.Visible = true;
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('บันทึกข้อมูลเรียบร้อย')", true);
            }
            else if(rbNotAllow.Checked)
            {
                OracleConnection.ClearAllPools();
                using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
                {
                    con.Open();
                    using (OracleCommand com = new OracleCommand("UPDATE TB_REQUEST SET DATE_END=:DATE_END, R_STATUS_ID=:R_STATUS_ID, COMMENT_INFO=:COMMENT_INFO, R_ALLOW=:R_ALLOW WHERE R_ID = '" + MyCrypto.GetDecryptedQueryString(Request.QueryString["id"].ToString()) + "'", con))
                    {
                        com.Parameters.Add(new OracleParameter("DATE_END", DateTime.Today));
                        com.Parameters.Add(new OracleParameter("R_STATUS_ID", "4"));
                        com.Parameters.Add(new OracleParameter("COMMENT_INFO", tbComment.Text));
                        com.Parameters.Add(new OracleParameter("R_ALLOW", "2"));
                        com.ExecuteNonQuery();
                    }
                }
                DataShow.Visible = false;
                NoAccept.Visible = true;
            }
        }

        protected void lbuBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListRequest.aspx");
        }
    }
}