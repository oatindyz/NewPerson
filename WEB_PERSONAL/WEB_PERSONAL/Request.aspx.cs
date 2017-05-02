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
    public partial class Request : System.Web.UI.Page
    {
        private Person loginPerson;
        protected void Page_Load(object sender, EventArgs e)
        {
            PersonnelSystem ps = PersonnelSystem.GetPersonnelSystem(this);
            loginPerson = ps.LoginPerson;

            using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
            {
                con.Open();
                using (OracleCommand com = new OracleCommand("SELECT R_STATUS_ID FROM TB_REQUEST WHERE R_STATUS_ID = 1 AND CITIZEN_ID = '" + ps.LoginPerson.PS_CITIZEN_ID + "'", con))
                {
                    using (OracleDataReader reader = com.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (!reader.IsDBNull(0) && reader.GetValue(0).ToString() == "1")
                            {
                                InProcess.Visible = true;
                                DataShow.Visible = false;
                                SaveShow.Visible = false;
                            }
                        }
                    }
                }
            }

            if (!IsPostBack)
            {
                BindDDL();
            }

            lbTitleID.Text = Util.IsBlank(ps.LoginPerson.PS_TITLE_NAME) ? "-" : ps.LoginPerson.PS_TITLE_NAME;
            lbFirstName.Text = Util.IsBlank(ps.LoginPerson.PS_FIRSTNAME) ? "-" : ps.LoginPerson.PS_FIRSTNAME;
            lbLastName.Text = Util.IsBlank(ps.LoginPerson.PS_LASTNAME) ? "-" : ps.LoginPerson.PS_LASTNAME;
            lbGenderID.Text = Util.IsBlank(ps.LoginPerson.PS_GENDER_NAME) ? "-" : ps.LoginPerson.PS_GENDER_NAME;
            lbBirthdayDate.Text = Util.IsBlank(ps.LoginPerson.PS_BIRTHDAY_DATE.ToString()) ? "-" : ps.LoginPerson.PS_BIRTHDAY_DATE.Value.ToLongDateString();
            lbEmail.Text = Util.IsBlank(ps.LoginPerson.PS_EMAIL) ? "-" : ps.LoginPerson.PS_EMAIL;
            lbNationID.Text = Util.IsBlank(ps.LoginPerson.PS_NATION_NAME) ? "-" : ps.LoginPerson.PS_NATION_NAME;
            lbCampusID.Text = Util.IsBlank(ps.LoginPerson.PS_CAMPUS_NAME) ? "-" : ps.LoginPerson.PS_FIRSTNAME;
            lbFacultyID.Text = Util.IsBlank(ps.LoginPerson.PS_FACULTY_NAME) ? "-" : ps.LoginPerson.PS_FACULTY_NAME;
            lbDivisionID.Text = Util.IsBlank(ps.LoginPerson.PS_DIVISION_NAME) ? "-" : ps.LoginPerson.PS_DIVISION_NAME;
            lbWorkDivisionID.Text = Util.IsBlank(ps.LoginPerson.PS_WORK_DIVISION_NAME) ? "-" : ps.LoginPerson.PS_WORK_DIVISION_NAME;
            lbStafftypeID.Text = Util.IsBlank(ps.LoginPerson.PS_STAFFTYPE_NAME) ? "-" : ps.LoginPerson.PS_STAFFTYPE_NAME;
            lbTimeContactID.Text = Util.IsBlank(ps.LoginPerson.PS_TIME_CONTACT_NAME) ? "-" : ps.LoginPerson.PS_TIME_CONTACT_NAME;
            lbBudgetID.Text = Util.IsBlank(ps.LoginPerson.PS_BUDGET_NAME) ? "-" : ps.LoginPerson.PS_BUDGET_NAME;
            lbSubStafftypeID.Text = Util.IsBlank(ps.LoginPerson.PS_SUBSTAFFTYPE_NAME) ? "-" : ps.LoginPerson.PS_SUBSTAFFTYPE_NAME;
            lbAdminPosID.Text = Util.IsBlank(ps.LoginPerson.PS_ADMIN_POS_NAME) ? "-" : ps.LoginPerson.PS_ADMIN_POS_NAME;
            lbWorkPosID.Text = Util.IsBlank(ps.LoginPerson.PS_WORK_POS_NAME) ? "-" : ps.LoginPerson.PS_WORK_POS_NAME;
            lbDateInwork.Text = Util.IsBlank(ps.LoginPerson.PS_INWORK_DATE.ToString()) ? "-" : ps.LoginPerson.PS_INWORK_DATE.Value.ToLongDateString();
            lbDateStartThisU.Text = Util.IsBlank(ps.LoginPerson.PS_DATE_START_THIS_U.ToString()) ? "-" : ps.LoginPerson.PS_DATE_START_THIS_U.Value.ToLongDateString();
            lbSpecialName.Text = Util.IsBlank(ps.LoginPerson.PS_SPECIAL_NAME) ? "-" : ps.LoginPerson.PS_SPECIAL_NAME;
            lbTeachIscedID.Text = Util.IsBlank(ps.LoginPerson.PS_TEACH_ISCED_NAME) ? "-" : ps.LoginPerson.PS_TEACH_ISCED_NAME;
            lbGradLevID.Text = Util.IsBlank(ps.LoginPerson.PS_GRAD_LEV_NAME) ? "-" : ps.LoginPerson.PS_GRAD_LEV_NAME;
            lbGradCurr.Text = Util.IsBlank(ps.LoginPerson.PS_GRAD_CURR) ? "-" : ps.LoginPerson.PS_GRAD_CURR;
            lbGradIscedID.Text = Util.IsBlank(ps.LoginPerson.PS_GRAD_ISCED_NAME) ? "-" : ps.LoginPerson.PS_GRAD_ISCED_NAME;
            lbGradProgID.Text = Util.IsBlank(ps.LoginPerson.PS_GRAD_PROG_NAME) ? "-" : ps.LoginPerson.PS_GRAD_PROG_NAME;
            lbGradUniv.Text = Util.IsBlank(ps.LoginPerson.PS_GRAD_UNIV) ? "-" : ps.LoginPerson.PS_GRAD_UNIV;
            lbGradCountryID.Text = Util.IsBlank(ps.LoginPerson.PS_GRAD_COUNTRY_NAME) ? "-" : ps.LoginPerson.PS_GRAD_COUNTRY_NAME;
            lbDeformID.Text = Util.IsBlank(ps.LoginPerson.PS_DEFORM_NAME) ? "-" : ps.LoginPerson.PS_DEFORM_NAME;
            lbSitNo.Text = Util.IsBlank(ps.LoginPerson.PS_SIT_NO) ? "-" : ps.LoginPerson.PS_SIT_NO;
            lbReligionID.Text = Util.IsBlank(ps.LoginPerson.PS_RELIGION_NAME) ? "-" : ps.LoginPerson.PS_RELIGION_NAME;
            lbMovementTypeID.Text = Util.IsBlank(ps.LoginPerson.PS_MOVEMENT_TYPE_NAME) ? "-" : ps.LoginPerson.PS_MOVEMENT_TYPE_NAME;
            lbMovementDate.Text = Util.IsBlank(ps.LoginPerson.PS_MOVEMENT_DATE.ToString()) ? "-" : ps.LoginPerson.PS_MOVEMENT_DATE.Value.ToLongDateString();
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
            DatabaseManager.BindDropDown(ddlMovementTypeID, "SELECT * FROM TB_MOVEMENT_TYPE ORDER BY ABS(MOVEMENT_TYPE_ID) ASC", "MOVEMENT_TYPE_NAME", "MOVEMENT_TYPE_ID", "--กรุณาเลือก--");

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

        protected void btnSaveRequest_Click(object sender, EventArgs e)
        {
            OracleConnection.ClearAllPools();
            using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
            {
                con.Open();
                using (OracleCommand com = new OracleCommand("INSERT INTO TB_REQUEST (R_ID, CITIZEN_ID, R_STATUS_ID, DATE_START, TITLE_ID, FIRSTNAME, LASTNAME, GENDER_ID, BIRTHDAY_DATE, EMAIL, NATION_ID, CAMPUS_ID, FACULTY_ID, DIVISION_ID, WORK_DIVISION_ID, STAFFTYPE_ID, TIME_CONTACT_ID, BUDGET_ID, SUBSTAFFTYPE_ID, ADMIN_POS_ID, WORK_POS_ID, INWORK_DATE, DATE_START_THIS_U, SPECIAL_NAME, TEACH_ISCED_ID, GRAD_LEV_ID, GRAD_CURR, GRAD_ISCED_ID, GRAD_PROG_ID, GRAD_UNIV, GRAD_COUNTRY_ID, DEFORM_ID, SIT_NO, RELIGION_ID, MOVEMENT_TYPE_ID, MOVEMENT_DATE) VALUES (TB_REQUEST_SEQ.NEXTVAL, :CITIZEN_ID, :R_STATUS_ID, :DATE_START, :TITLE_ID, :FIRSTNAME, :LASTNAME, :GENDER_ID, :BIRTHDAY_DATE, :EMAIL, :NATION_ID, :CAMPUS_ID, :FACULTY_ID, :DIVISION_ID, :WORK_DIVISION_ID, :STAFFTYPE_ID, :TIME_CONTACT_ID, :BUDGET_ID, :SUBSTAFFTYPE_ID, :ADMIN_POS_ID, :WORK_POS_ID, :INWORK_DATE, :DATE_START_THIS_U, :SPECIAL_NAME, :TEACH_ISCED_ID, :GRAD_LEV_ID, :GRAD_CURR, :GRAD_ISCED_ID, :GRAD_PROG_ID, :GRAD_UNIV, :GRAD_COUNTRY_ID, :DEFORM_ID, :SIT_NO, :RELIGION_ID, :MOVEMENT_TYPE_ID, :MOVEMENT_DATE)", con))
                //using (OracleCommand com = new OracleCommand("INSERT INTO TB_REQUEST (R_ID, CITIZEN_ID, R_STATUS_ID, DATE_START, TITLE_ID, FIRSTNAME, LASTNAME, GENDER_ID, BIRTHDAY_DATE, EMAIL, NATION_ID, CAMPUS_ID, FACULTY_ID, DIVISION_ID, WORK_DIVISION_ID, STAFFTYPE_ID, TIME_CONTACT_ID, BUDGET_ID, SUBSTAFFTYPE_ID, ADMIN_POS_ID, WORK_POS_ID, INWORK_DATE, DATE_START_THIS_U, SPECIAL_NAME, TEACH_ISCED_ID, GRAD_LEV_ID, GRAD_CURR, GRAD_ISCED_ID, GRAD_PROG_ID, GRAD_UNIV, GRAD_COUNTRY_ID, DEFORM_ID, SIT_NO, RELIGION_ID, MOVEMENT_TYPE_ID, MOVEMENT_DATE) VALUES (TB_REQUEST_SEQ.NEXTVAL, :CITIZEN_ID, :R_STATUS_ID, :DATE_START, :TITLE_ID, :FIRSTNAME, :LASTNAME, :GENDER_ID, :BIRTHDAY_DATE, :EMAIL, :NATION_ID, :CAMPUS_ID, :FACULTY_ID, :DIVISION_ID, :WORK_DIVISION_ID, :STAFFTYPE_ID, :TIME_CONTACT_ID, :BUDGET_ID, :SUBSTAFFTYPE_ID, :ADMIN_POS_ID, :WORK_POS_ID, :INWORK_DATE, :DATE_START_THIS_U, :SPECIAL_NAME, :TEACH_ISCED_ID, :GRAD_LEV_ID, :GRAD_CURR, :GRAD_ISCED_ID, :GRAD_PROG_ID, :GRAD_UNIV, :GRAD_COUNTRY_ID, :DEFORM_ID, :SIT_NO, :RELIGION_ID, :MOVEMENT_TYPE_ID, :MOVEMENT_DATE)", con))
                {
                    com.Parameters.Add(new OracleParameter("CITIZEN_ID", loginPerson.PS_CITIZEN_ID));
                    com.Parameters.Add(new OracleParameter("R_STATUS_ID", "1"));
                    com.Parameters.Add(new OracleParameter("DATE_START", DateTime.Today));

                    com.Parameters.Add(new OracleParameter("TITLE_ID", ddlTitleID.SelectedValue));
                    com.Parameters.Add(new OracleParameter("FIRSTNAME", tbFirstName.Text));
                    com.Parameters.Add(new OracleParameter("LASTNAME", tbLastName.Text));
                    com.Parameters.Add(new OracleParameter("GENDER_ID", ddlGenderID.SelectedValue));

                    if (tbBirthdayDate.Text == "") { com.Parameters.Add(new OracleParameter("BIRTHDAY_DATE", DBNull.Value)); } else { com.Parameters.Add(new OracleParameter("BIRTHDAY_DATE", Util.ToDateTimeOracle(tbBirthdayDate.Text))); }

                    com.Parameters.Add(new OracleParameter("EMAIL", tbEmail.Text));
                    com.Parameters.Add(new OracleParameter("NATION_ID", ddlNationID.SelectedValue));
                    com.Parameters.Add(new OracleParameter("CAMPUS_ID", ddlCampusID.SelectedValue));
                    com.Parameters.Add(new OracleParameter("FACULTY_ID", ddlFacultyID.SelectedValue));
                    com.Parameters.Add(new OracleParameter("DIVISION_ID", ddlDivisionID.SelectedValue));
                    if (ddlWorkDivisionID.SelectedIndex == 0) { com.Parameters.Add(new OracleParameter("WORK_DIVISION_ID", DBNull.Value)); }
                    else
                    {
                        com.Parameters.Add(new OracleParameter("WORK_DIVISION_ID", ddlWorkDivisionID.SelectedValue));
                    }
                    com.Parameters.Add(new OracleParameter("STAFFTYPE_ID", ddlStafftypeID.SelectedValue));
                    com.Parameters.Add(new OracleParameter("TIME_CONTACT_ID", ddlTimeContactID.SelectedValue));
                    com.Parameters.Add(new OracleParameter("BUDGET_ID", ddlBudgetID.SelectedValue));
                    com.Parameters.Add(new OracleParameter("SUBSTAFFTYPE_ID", ddlSubStafftypeID.SelectedValue));

                    if (ddlAdminPosID.SelectedIndex == 0) { com.Parameters.Add(new OracleParameter("ADMIN_POS_ID", DBNull.Value)); }
                    else
                    {
                        com.Parameters.Add(new OracleParameter("ADMIN_POS_ID", ddlAdminPosID.SelectedValue));
                    }

                    com.Parameters.Add(new OracleParameter("WORK_POS_ID", ddlWorkPosID.SelectedValue));
                    
                    if (tbDateInwork.Text == "") { com.Parameters.Add(new OracleParameter("INWORK_DATE", DBNull.Value)); } else { com.Parameters.Add(new OracleParameter("INWORK_DATE", Util.ToDateTimeOracle(tbDateInwork.Text))); }
                    if (tbDateStartThisU.Text == "") { com.Parameters.Add(new OracleParameter("DATE_START_THIS_U", DBNull.Value)); } else { com.Parameters.Add(new OracleParameter("DATE_START_THIS_U", Util.ToDateTimeOracle(tbDateStartThisU.Text))); }


                    com.Parameters.Add(new OracleParameter("SPECIAL_NAME", tbSpecialName.Text));

                    if (ddlTeachIscedID.SelectedIndex == 0) { com.Parameters.Add(new OracleParameter("TEACH_ISCED_ID", DBNull.Value)); }
                    else
                    {
                        com.Parameters.Add(new OracleParameter("TEACH_ISCED_ID", ddlTeachIscedID.SelectedValue));
                    }

                    com.Parameters.Add(new OracleParameter("GRAD_LEV_ID", ddlGradLevID.SelectedValue));
                    com.Parameters.Add(new OracleParameter("GRAD_CURR", tbGradCurr.Text));

                    if (ddlGradIscedID.SelectedIndex == 0) { com.Parameters.Add(new OracleParameter("GRAD_ISCED_ID", DBNull.Value)); }
                    else
                    {
                        com.Parameters.Add(new OracleParameter("GRAD_ISCED_ID", ddlGradIscedID.SelectedValue));
                    }
                    if (ddlGradProgID.SelectedIndex == 0) { com.Parameters.Add(new OracleParameter("GRAD_PROG_ID", DBNull.Value)); }
                    else
                    {
                        com.Parameters.Add(new OracleParameter("GRAD_PROG_ID", ddlGradProgID.SelectedValue));
                    }

                    com.Parameters.Add(new OracleParameter("GRAD_UNIV", tbGradUniv.Text));
                    com.Parameters.Add(new OracleParameter("GRAD_COUNTRY_ID", ddlGradCountryID.SelectedValue));
                    com.Parameters.Add(new OracleParameter("DEFORM_ID", ddlDeformID.SelectedValue));
                    com.Parameters.Add(new OracleParameter("SIT_NO", tbSitNo.Text));
                    com.Parameters.Add(new OracleParameter("RELIGION_ID", ddlReligionID.SelectedValue));
                    com.Parameters.Add(new OracleParameter("MOVEMENT_TYPE_ID", ddlMovementTypeID.SelectedValue));
                    if (tbMovementDate.Text == "") { com.Parameters.Add(new OracleParameter("MOVEMENT_DATE", DBNull.Value)); } else { com.Parameters.Add(new OracleParameter("MOVEMENT_DATE", Util.ToDateTimeOracle(tbMovementDate.Text))); }
                    
                    com.ExecuteNonQuery();
                }
            }

            DataShow.Visible = false;
            SaveShow.Visible = true;
        }


    }
}