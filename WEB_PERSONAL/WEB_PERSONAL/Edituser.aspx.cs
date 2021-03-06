﻿using System;
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
    public partial class Edituser : System.Web.UI.Page
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
                BindDDL();
                ReadID();
            }
        }

        protected void ReadID()
        {
            using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
            {
                con.Open();
                using (OracleCommand com = new OracleCommand("SELECT PS_CITIZEN_ID,PS_TITLE_ID,PS_FIRSTNAME,PS_LASTNAME,PS_GENDER_ID,PS_BIRTHDAY_DATE,PS_EMAIL,PS_HOMEADD,PS_MOO,PS_STREET,PS_DISTRICT_ID,PS_AMPHUR_ID,PS_PROVINCE_ID,PS_ZIPCODE,PS_TELEPHONE,PS_NATION_ID,PS_CAMPUS_ID,PS_FACULTY_ID,PS_DIVISION_ID,PS_WORK_DIVISION_ID,PS_STAFFTYPE_ID,PS_TIME_CONTACT_ID,PS_BUDGET_ID,PS_SUBSTAFFTYPE_ID,PS_ADMIN_POS_ID,PS_WORK_POS_ID,PS_INWORK_DATE,PS_DATE_START_THIS_U,PS_SPECIAL_NAME,PS_TEACH_ISCED_ID,PS_GRAD_LEV_ID,PS_GRAD_CURR,PS_GRAD_ISCED_ID,PS_GRAD_PROG_ID,PS_GRAD_UNIV,PS_GRAD_COUNTRY_ID,PS_DEFORM_ID,PS_SIT_NO,PS_RELIGION_ID,PS_MOVEMENT_TYPE_ID,PS_MOVEMENT_DATE FROM PS_PERSON WHERE PS_CITIZEN_ID = '" + MyCrypto.GetDecryptedQueryString(Request.QueryString["id"].ToString()) + "'", con))
                {
                    using (OracleDataReader reader = com.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int i = 0;
                            tbCitizenID.Text = reader.IsDBNull(i) ? "" : reader.GetValue(i).ToString(); ++i;
                            ddlTitleID.SelectedValue = reader.IsDBNull(i) ? "" : reader.GetValue(i).ToString(); ++i;
                            tbFirstName.Text = reader.IsDBNull(i) ? "" : reader.GetValue(i).ToString(); ++i;
                            tbLastName.Text = reader.IsDBNull(i) ? "" : reader.GetValue(i).ToString(); ++i;
                            ddlGenderID.SelectedValue = reader.IsDBNull(i) ? "" : reader.GetValue(i).ToString(); ++i;
                            tbBirthdayDate.Text = reader.IsDBNull(i) ? "" : reader.GetDateTime(i).ToString("dd MMM yyyy"); ++i;
                            tbEmail.Text = reader.IsDBNull(i) ? "" : reader.GetValue(i).ToString(); ++i;
                            tbHomeAdd.Text = reader.IsDBNull(i) ? "" : reader.GetValue(i).ToString(); ++i;
                            tbMoo.Text = reader.IsDBNull(i) ? "" : reader.GetValue(i).ToString(); ++i;
                            tbStreet.Text = reader.IsDBNull(i) ? "" : reader.GetValue(i).ToString(); ++i;
                            
                            //--
                            ddlProvinceID.SelectedValue = reader.IsDBNull(i) ? "" : reader.GetValue(i).ToString(); ++i;
                            ddlAmphurID.Items.Clear();
                            string s1 = reader.IsDBNull(i) ? "" : reader.GetValue(i).ToString(); ++i;
                            DatabaseManager.BindDropDown(ddlAmphurID, "SELECT * FROM TB_AMPHUR WHERE PROVINCE_ID = '" + ddlProvinceID.SelectedValue + "'", "AMPHUR_TH", "AMPHUR_ID", "--กรุณาเลือก--");
                            ddlAmphurID.SelectedValue = s1;
                            //
                            ddlDistrictID.Items.Clear();
                            string s2 = reader.IsDBNull(i) ? "0" : reader.GetValue(i).ToString(); ++i;
                            DatabaseManager.BindDropDown(ddlDistrictID, "SELECT * FROM TB_DISTRICT WHERE AMPHUR_ID = '" + ddlAmphurID.SelectedValue + "'", "DISTRICT_TH", "DISTRICT_ID", "--กรุณาเลือก--");
                            ddlDistrictID.SelectedValue = s2;
                            //
                            tbZipcode.Text = reader.IsDBNull(i) ? "" : reader.GetValue(i).ToString(); ++i;
                            tbTelephone.Text = reader.IsDBNull(i) ? "" : reader.GetValue(i).ToString(); ++i;
                            ddlNationID.SelectedValue = reader.IsDBNull(i) ? "" : reader.GetValue(i).ToString(); ++i;
                            //--

                            ddlCampusID.SelectedValue = reader.IsDBNull(i) ? "" : reader.GetValue(i).ToString(); ++i;

                            ddlFacultyID.Items.Clear();
                            string z1 = reader.IsDBNull(i) ? "" : reader.GetValue(i).ToString(); ++i;
                            DatabaseManager.BindDropDown(ddlFacultyID, "SELECT * FROM TB_FACULTY WHERE CAMPUS_ID = '" + ddlCampusID.SelectedValue + "'", "FACULTY_NAME", "FACULTY_ID", "--กรุณาเลือก--");
                            ddlFacultyID.SelectedValue = z1;
                            //
                            ddlDivisionID.Items.Clear();
                            string z2 = reader.IsDBNull(i) ? "" : reader.GetValue(i).ToString(); ++i;
                            DatabaseManager.BindDropDown(ddlDivisionID, "SELECT * FROM TB_DIVISION WHERE FACULTY_ID = '" + ddlFacultyID.SelectedValue + "'", "DIVISION_NAME", "DIVISION_ID", "--กรุณาเลือก--");
                            ddlDivisionID.SelectedValue = z2;
                            //
                            ddlWorkDivisionID.Items.Clear();
                            string z3 = reader.IsDBNull(i) ? "" : reader.GetValue(i).ToString(); ++i;
                            DatabaseManager.BindDropDown(ddlWorkDivisionID, "SELECT * FROM TB_WORK_DIVISION WHERE DIVISION_ID = '" + ddlDivisionID.SelectedValue + "'", "WORK_NAME", "WORK_ID", "--กรุณาเลือก--");
                            ddlWorkDivisionID.SelectedValue = z3;
                            //--

                            ddlStafftypeID.SelectedValue = reader.IsDBNull(i) ? "" : reader.GetValue(i).ToString(); ++i;
                            ddlTimeContactID.SelectedValue = reader.IsDBNull(i) ? "" : reader.GetValue(i).ToString(); ++i;
                            ddlBudgetID.SelectedValue = reader.IsDBNull(i) ? "" : reader.GetValue(i).ToString(); ++i;
                            ddlSubStafftypeID.SelectedValue = reader.IsDBNull(i) ? "" : reader.GetValue(i).ToString(); ++i;
                            ddlAdminPosID.SelectedValue = reader.IsDBNull(i) ? "" : reader.GetValue(i).ToString(); ++i;
                            ddlWorkPosID.SelectedValue = reader.IsDBNull(i) ? "" : reader.GetValue(i).ToString(); ++i;
                            tbDateInwork.Text = reader.IsDBNull(i) ? "" : reader.GetDateTime(i).ToString("dd MMM yyyy"); ++i;
                            tbDateStartThisU.Text = reader.IsDBNull(i) ? "" : reader.GetDateTime(i).ToString("dd MMM yyyy"); ++i;
                            tbSpecialName.Text = reader.IsDBNull(i) ? "" : reader.GetValue(i).ToString(); ++i;
                            ddlTeachIscedID.SelectedValue = reader.IsDBNull(i) ? "" : reader.GetValue(i).ToString(); ++i;
                            ddlGradLevID.SelectedValue = reader.IsDBNull(i) ? "" : reader.GetValue(i).ToString(); ++i;
                            tbGradCurr.Text = reader.IsDBNull(i) ? "" : reader.GetValue(i).ToString(); ++i;
                            ddlGradIscedID.SelectedValue = reader.IsDBNull(i) ? "" : reader.GetValue(i).ToString(); ++i;
                            ddlGradProgID.SelectedValue = reader.IsDBNull(i) ? "" : reader.GetValue(i).ToString(); ++i;
                            tbGradUniv.Text = reader.IsDBNull(i) ? "" : reader.GetValue(i).ToString(); ++i;
                            ddlGradCountryID.SelectedValue = reader.IsDBNull(i) ? "" : reader.GetValue(i).ToString(); ++i;
                            ddlDeformID.SelectedValue = reader.IsDBNull(i) ? "" : reader.GetValue(i).ToString(); ++i;
                            tbSitNo.Text = reader.IsDBNull(i) ? "" : reader.GetValue(i).ToString(); ++i;
                            ddlReligionID.SelectedValue = reader.IsDBNull(i) ? "" : reader.GetValue(i).ToString(); ++i;
                            ddlMovementTypeID.SelectedValue = reader.IsDBNull(i) ? "" : reader.GetValue(i).ToString(); ++i;
                            tbMovementDate.Text = reader.IsDBNull(i) ? "" : reader.GetDateTime(i).ToString("dd MMM yyyy"); ++i;
                        }
                    }
                }
            }
        }

        protected void BindDDL()
        {
            DatabaseManager.BindDropDown(ddlTitleID, "SELECT * FROM TB_TITLENAME ORDER BY ABS(TITLE_ID) ASC", "TITLE_NAME_TH", "TITLE_ID", "--กรุณาเลือก--");
            DatabaseManager.BindDropDown(ddlGenderID, "SELECT * FROM TB_GENDER ORDER BY ABS(GENDER_ID) ASC", "GENDER_NAME", "GENDER_ID", "--กรุณาเลือก--");

            DatabaseManager.BindDropDown(ddlProvinceID, "SELECT * FROM TB_PROVINCE", "PROVINCE_TH", "PROVINCE_ID", "--กรุณาเลือกจังหวัด--");
            ddlAmphurID.Items.Insert(0, new ListItem("--กรุณาเลือกอำเภอ--", ""));
            ddlDistrictID.Items.Insert(0, new ListItem("--กรุณาเลือกตำบล--", ""));
            tbZipcode.Text = "";

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

        //Province
        protected void ddlProvinceID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                using (OracleConnection sqlConn = new OracleConnection(DatabaseManager.CONNECTION_STRING))
                {
                    using (OracleCommand sqlCmd = new OracleCommand())
                    {
                        sqlCmd.CommandText = "select * from TB_AMPHUR where PROVINCE_ID=:PROVINCE_ID";
                        sqlCmd.Parameters.Add(":PROVINCE_ID", ddlProvinceID.SelectedValue);
                        sqlCmd.Connection = sqlConn;
                        sqlConn.Open();
                        OracleDataAdapter da = new OracleDataAdapter(sqlCmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        ddlAmphurID.DataSource = dt;
                        ddlAmphurID.DataValueField = "AMPHUR_ID";
                        ddlAmphurID.DataTextField = "AMPHUR_TH";
                        ddlAmphurID.DataBind();
                        sqlConn.Close();

                        ddlAmphurID.Items.Insert(0, new ListItem("--กรุณาเลือกอำเภอ--", ""));
                        ddlDistrictID.Items.Clear();
                        ddlDistrictID.Items.Insert(0, new ListItem("--กรุณาเลือกตำบล--", ""));
                        tbZipcode.Text = "";
                    }
                }
            }
            catch { }
        }

        protected void ddlAmphurID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                using (OracleConnection sqlConn = new OracleConnection(DatabaseManager.CONNECTION_STRING))
                {
                    using (OracleCommand sqlCmd = new OracleCommand())
                    {
                        sqlCmd.CommandText = "select * from TB_DISTRICT where AMPHUR_ID=:DISTRICT_ID";
                        sqlCmd.Parameters.Add(":DISTRICT_ID", ddlAmphurID.SelectedValue);
                        sqlCmd.Connection = sqlConn;
                        sqlConn.Open();
                        OracleDataAdapter da = new OracleDataAdapter(sqlCmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        ddlDistrictID.DataSource = dt;
                        ddlDistrictID.DataValueField = "DISTRICT_ID";
                        ddlDistrictID.DataTextField = "DISTRICT_TH";
                        ddlDistrictID.DataBind();
                        sqlConn.Close();

                        ddlDistrictID.Items.Insert(0, new ListItem("--กรุณาเลือกตำบล--", ""));
                        tbZipcode.Text = "";

                    }
                }
            }
            catch { }
        }

        protected void ddlDistrictID_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ZIPCODE = DatabaseManager.ExecuteString("select POST_CODE from TB_DISTRICT where DISTRICT_ID = " + ddlDistrictID.SelectedValue + "");
            tbZipcode.Text = ZIPCODE;
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
                                trWorkDivision.Visible = false;
                            }
                            else
                            {
                                ddlWorkDivisionID.Visible = true;
                                trWorkDivision.Visible = true;
                            }
                        }
                    }
                }
            }
        }

        protected void btnUpdateUser_Click(object sender, EventArgs e)
        {
            DateTime bd = Util.ToDateTimeOracle(tbBirthdayDate.Text);
            DateTime iw = Util.ToDateTimeOracle(tbDateInwork.Text);
            DateTime stu = Util.ToDateTimeOracle(tbDateStartThisU.Text);
            DateTime now = DateTime.Today;

            double year = (now - bd).TotalDays / 365.0;
            if (year < 18)
            {
                ScriptManager.GetCurrent(this.Page).SetFocus(this.tbBirthdayDate);
                ChangeNotification("danger", "ต้องมีอายุอย่างน้อย 18 ปี");
                return;
            }
            if (iw.CompareTo(bd) < 0)
            {
                ScriptManager.GetCurrent(this.Page).SetFocus(this.tbDateInwork);
                ChangeNotification("danger", "วันที่เข้าทำงานครั้งแรกต้องอยู่หลังวันเกิด");
                return;
            }
            if (stu.CompareTo(bd) < 0)
            {
                ScriptManager.GetCurrent(this.Page).SetFocus(this.tbDateStartThisU);
                ChangeNotification("danger", "วันที่เข้าทำงาน ณ สถานที่ปัจจุบันต้องอยู่หลังวันเกิด");
                return;
            }

            UPDATE_PERSON();

            ClearNotification();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('แก้ไขข้อมูลเรียบร้อย')", true);

        }

        public int UPDATE_PERSON()
        {
            int id = 0;
            OracleConnection.ClearAllPools();
            using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
            {
                con.Open();
                using (OracleCommand com = new OracleCommand("UPDATE PS_PERSON SET PS_TITLE_ID=:PS_TITLE_ID, PS_FIRSTNAME=:PS_FIRSTNAME, PS_LASTNAME=:PS_LASTNAME, PS_GENDER_ID=:PS_GENDER_ID, PS_BIRTHDAY_DATE=:PS_BIRTHDAY_DATE, PS_EMAIL=:PS_EMAIL, PS_HOMEADD=:PS_HOMEADD, PS_MOO=:PS_MOO, PS_STREET=:PS_STREET, PS_PROVINCE_ID=:PS_PROVINCE_ID, PS_AMPHUR_ID=:PS_AMPHUR_ID, PS_DISTRICT_ID=:PS_DISTRICT_ID, PS_ZIPCODE=:PS_ZIPCODE, PS_TELEPHONE=:PS_TELEPHONE, PS_NATION_ID=:PS_NATION_ID, PS_CAMPUS_ID=:PS_CAMPUS_ID, PS_FACULTY_ID=:PS_FACULTY_ID, PS_DIVISION_ID=:PS_DIVISION_ID, PS_WORK_DIVISION_ID=:PS_WORK_DIVISION_ID, PS_STAFFTYPE_ID=:PS_STAFFTYPE_ID, PS_TIME_CONTACT_ID=:PS_TIME_CONTACT_ID, PS_BUDGET_ID=:PS_BUDGET_ID, PS_SUBSTAFFTYPE_ID=:PS_SUBSTAFFTYPE_ID, PS_ADMIN_POS_ID=:PS_ADMIN_POS_ID, PS_WORK_POS_ID=:PS_WORK_POS_ID, PS_INWORK_DATE=:PS_INWORK_DATE, PS_DATE_START_THIS_U=:PS_DATE_START_THIS_U, PS_SPECIAL_NAME=:PS_SPECIAL_NAME, PS_TEACH_ISCED_ID=:PS_TEACH_ISCED_ID, PS_GRAD_LEV_ID=:PS_GRAD_LEV_ID, PS_GRAD_CURR=:PS_GRAD_CURR, PS_GRAD_ISCED_ID=:PS_GRAD_ISCED_ID, PS_GRAD_PROG_ID=:PS_GRAD_PROG_ID, PS_GRAD_UNIV=:PS_GRAD_UNIV, PS_GRAD_COUNTRY_ID=:PS_GRAD_COUNTRY_ID, PS_DEFORM_ID=:PS_DEFORM_ID, PS_SIT_NO=:PS_SIT_NO, PS_RELIGION_ID=:PS_RELIGION_ID, PS_MOVEMENT_TYPE_ID=:PS_MOVEMENT_TYPE_ID, PS_MOVEMENT_DATE=:PS_MOVEMENT_DATE WHERE PS_CITIZEN_ID = '" + MyCrypto.GetDecryptedQueryString(Request.QueryString["id"].ToString()) + "'", con))
                {
                    com.Parameters.Add(new OracleParameter("PS_TITLE_ID", ddlTitleID.SelectedValue));
                    com.Parameters.Add(new OracleParameter("PS_FIRSTNAME", tbFirstName.Text));
                    com.Parameters.Add(new OracleParameter("PS_LASTNAME", tbLastName.Text));
                    com.Parameters.Add(new OracleParameter("PS_GENDER_ID", ddlGenderID.SelectedValue));
                    com.Parameters.Add(new OracleParameter("PS_BIRTHDAY_DATE", Util.ToDateTimeOracle(tbBirthdayDate.Text)));
                    com.Parameters.Add(new OracleParameter("PS_EMAIL", tbEmail.Text));
                    com.Parameters.Add(new OracleParameter("PS_HOMEADD", tbHomeAdd.Text));
                    com.Parameters.Add(new OracleParameter("PS_MOO", tbMoo.Text));
                    com.Parameters.Add(new OracleParameter("PS_STREET", tbStreet.Text));

                    if (ddlProvinceID.SelectedIndex == 0) { com.Parameters.Add(new OracleParameter("PS_PROVINCE_ID", DBNull.Value)); }
                    else
                    {
                        com.Parameters.Add(new OracleParameter("PS_PROVINCE_ID", ddlProvinceID.SelectedValue));
                    }

                    if (ddlAmphurID.SelectedIndex == 0) { com.Parameters.Add(new OracleParameter("PS_AMPHUR_ID", DBNull.Value)); }
                    else
                    {
                        com.Parameters.Add(new OracleParameter("PS_AMPHUR_ID", ddlAmphurID.SelectedValue));
                    }

                    if (ddlDistrictID.SelectedIndex == 0) { com.Parameters.Add(new OracleParameter("PS_DISTRICT_ID", DBNull.Value)); }
                    else
                    {
                        com.Parameters.Add(new OracleParameter("PS_DISTRICT_ID", ddlDistrictID.SelectedValue));
                    }

                    com.Parameters.Add(new OracleParameter("PS_ZIPCODE", tbZipcode.Text));
                    com.Parameters.Add(new OracleParameter("PS_TELEPHONE", tbTelephone.Text));
                    com.Parameters.Add(new OracleParameter("PS_NATION_ID", ddlNationID.SelectedValue));
                    com.Parameters.Add(new OracleParameter("PS_CAMPUS_ID", ddlCampusID.SelectedValue));
                    com.Parameters.Add(new OracleParameter("PS_FACULTY_ID", ddlFacultyID.SelectedValue));
                    com.Parameters.Add(new OracleParameter("PS_DIVISION_ID", ddlDivisionID.SelectedValue));
                    if (ddlWorkDivisionID.SelectedIndex == 0) { com.Parameters.Add(new OracleParameter("PS_WORK_DIVISION_ID", DBNull.Value)); }
                    else
                    {
                        com.Parameters.Add(new OracleParameter("PS_WORK_DIVISION_ID", ddlWorkDivisionID.SelectedValue));
                    }
                    com.Parameters.Add(new OracleParameter("PS_STAFFTYPE_ID", ddlStafftypeID.SelectedValue));
                    com.Parameters.Add(new OracleParameter("PS_TIME_CONTACT_ID", ddlTimeContactID.SelectedValue));
                    com.Parameters.Add(new OracleParameter("PS_BUDGET_ID", ddlBudgetID.SelectedValue));
                    com.Parameters.Add(new OracleParameter("PS_SUBSTAFFTYPE_ID", ddlSubStafftypeID.SelectedValue));
                    com.Parameters.Add(new OracleParameter("PS_ADMIN_POS_ID", ddlAdminPosID.SelectedValue));
                    com.Parameters.Add(new OracleParameter("PS_WORK_POS_ID", ddlWorkPosID.SelectedValue));
                    com.Parameters.Add(new OracleParameter("PS_INWORK_DATE", Util.ToDateTimeOracle(tbDateInwork.Text)));
                    com.Parameters.Add(new OracleParameter("PS_DATE_START_THIS_U", Util.ToDateTimeOracle(tbDateStartThisU.Text)));
                    com.Parameters.Add(new OracleParameter("PS_SPECIAL_NAME", tbSpecialName.Text));

                    if (ddlTeachIscedID.SelectedIndex == 0) { com.Parameters.Add(new OracleParameter("PS_TEACH_ISCED_ID", DBNull.Value)); }
                    else
                    {
                        com.Parameters.Add(new OracleParameter("PS_TEACH_ISCED_ID", ddlTeachIscedID.SelectedValue));
                    }

                    com.Parameters.Add(new OracleParameter("PS_GRAD_LEV_ID", ddlGradLevID.SelectedValue));
                    com.Parameters.Add(new OracleParameter("PS_GRAD_CURR", tbGradCurr.Text));

                    if (ddlGradIscedID.SelectedIndex == 0) { com.Parameters.Add(new OracleParameter("PS_GRAD_ISCED_ID", DBNull.Value)); }
                    else
                    {
                        com.Parameters.Add(new OracleParameter("PS_GRAD_ISCED_ID", ddlGradIscedID.SelectedValue));
                    }
                    if (ddlGradProgID.SelectedIndex == 0) { com.Parameters.Add(new OracleParameter("PS_GRAD_PROG_ID", DBNull.Value)); }
                    else
                    {
                        com.Parameters.Add(new OracleParameter("PS_GRAD_PROG_ID", ddlGradProgID.SelectedValue));
                    }

                    com.Parameters.Add(new OracleParameter("PS_GRAD_UNIV", tbGradUniv.Text));
                    com.Parameters.Add(new OracleParameter("PS_GRAD_COUNTRY_ID", ddlGradCountryID.SelectedValue));
                    com.Parameters.Add(new OracleParameter("PS_DEFORM_ID", ddlDeformID.SelectedValue));
                    com.Parameters.Add(new OracleParameter("PS_SIT_NO", tbSitNo.Text));
                    com.Parameters.Add(new OracleParameter("PS_RELIGION_ID", ddlReligionID.SelectedValue));
                    com.Parameters.Add(new OracleParameter("PS_MOVEMENT_TYPE_ID", ddlMovementTypeID.SelectedValue));
                    if (tbMovementDate.Text == "") { com.Parameters.Add(new OracleParameter("PS_MOVEMENT_DATE", DBNull.Value)); } else { com.Parameters.Add(new OracleParameter("PS_MOVEMENT_DATE", Util.ToDateTimeOracle(tbMovementDate.Text))); }
                    id = com.ExecuteNonQuery();

                }
            }
            return id;
        }

        protected void ddlTitleID_SelectedIndexChanged(object sender, EventArgs e)
        {
            int IsFemale = DatabaseManager.ExecuteInt("SELECT TITLE_ID FROM TB_TITLENAME WHERE TITLE_ID in(1,2)");
            int IsMale = DatabaseManager.ExecuteInt("SELECT TITLE_ID FROM TB_TITLENAME WHERE TITLE_ID = 3");
            if (ddlTitleID.SelectedValue == IsFemale.ToString())
            {
                ddlGenderID.SelectedIndex = 2;
            }
            else if (ddlTitleID.SelectedValue == IsMale.ToString())
            {
                ddlGenderID.SelectedIndex = 1;
            }
        }

        protected void ddlSubStafftypeID_SelectedIndexChanged(object sender, EventArgs e)
        {
            int subStaffID = DatabaseManager.ExecuteInt("SELECT SUBSTAFFTYPE_ID FROM TB_SUBSTAFFTYPE WHERE SUBSTAFFTYPE_ID = 2");
            if (ddlSubStafftypeID.SelectedValue == subStaffID.ToString())
            {
                trddlTeachIscedID.Visible = false;
            }
            else
            {
                trddlTeachIscedID.Visible = true;
            }
        }

        protected void ddlGradLevID_SelectedIndexChanged(object sender, EventArgs e)
        {
            int LevID = DatabaseManager.ExecuteInt("SELECT LEV_ID FROM TB_LEV WHERE LEV_ID = 25");
            if (ddlGradLevID.SelectedValue == LevID.ToString())
            {
                trGradIscedID.Visible = false;
                trGradProgID.Visible = false;
            }
            else
            {
                trGradIscedID.Visible = true;
                trGradProgID.Visible = true;
            }
        }

        private void ChangeNotification(string type)
        {
            switch (type)
            {
                case "info": notification.Attributes["class"] = "alert alert_info"; break;
                case "success": notification.Attributes["class"] = "alert alert_success"; break;
                case "warning": notification.Attributes["class"] = "alert alert_warning"; break;
                case "danger": notification.Attributes["class"] = "alert alert_danger"; break;
                default: notification.Attributes["class"] = null; break;
            }
        }
        private void ChangeNotification(string type, string text)
        {
            switch (type)
            {
                case "info": notification.Attributes["class"] = "alert alert_info"; break;
                case "success": notification.Attributes["class"] = "alert alert_success"; break;
                case "warning": notification.Attributes["class"] = "alert alert_warning"; break;
                case "danger": notification.Attributes["class"] = "alert alert_danger"; break;
                default: notification.Attributes["class"] = null; break;
            }
            notification.InnerHtml = text;
        }
        private void ClearNotification()
        {
            notification.Attributes["class"] = null;
            notification.InnerHtml = "";
        }
        private void AddNotification(string text)
        {
            notification.InnerHtml += text;
        }


    }
}