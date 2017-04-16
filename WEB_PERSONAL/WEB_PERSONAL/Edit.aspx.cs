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
    public partial class Edit : System.Web.UI.Page
    {
        Person loginPerson;
        protected void Page_Load(object sender, EventArgs e)
        {
            PersonnelSystem ps = PersonnelSystem.GetPersonnelSystem(this);
            loginPerson = ps.LoginPerson;

            if (loginPerson.PERSON_ROLE_ID == "2")
            {
                SpanBack.Visible = true;
            }
            else
            {
                SpanBack.Visible = false;
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
                using (OracleCommand com = new OracleCommand("SELECT PS_HOMEADD,PS_MOO,PS_STREET,PS_DISTRICT_ID,PS_AMPHUR_ID,PS_PROVINCE_ID,PS_ZIPCODE,PS_TELEPHONE FROM PS_PERSON WHERE PS_CITIZEN_ID = '" + loginPerson.PS_CITIZEN_ID + "'", con))
                {
                    using (OracleDataReader reader = com.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int i = 0;
                         
                            lbCitizenID.Text = Util.IsBlank(loginPerson.PS_CITIZEN_ID) ? "-" : loginPerson.PS_CITIZEN_ID;
                            lbTitleID.Text = Util.IsBlank(loginPerson.PS_TITLE_NAME) ? "-" : loginPerson.PS_TITLE_NAME;
                            lbFirstName.Text = Util.IsBlank(loginPerson.PS_FIRSTNAME) ? "-" : loginPerson.PS_FIRSTNAME;
                            lbLastName.Text = Util.IsBlank(loginPerson.PS_LASTNAME) ? "-" : loginPerson.PS_LASTNAME;
                            lbGenderID.Text = Util.IsBlank(loginPerson.PS_GENDER_NAME) ? "-" : loginPerson.PS_GENDER_NAME;
                            lbBirthdayDate.Text = Util.IsBlank(loginPerson.PS_BIRTHDAY_DATE.ToString()) ? "-" : loginPerson.PS_BIRTHDAY_DATE.Value.ToLongDateString();
                            lbEmail.Text = Util.IsBlank(loginPerson.PS_EMAIL) ? "-" : loginPerson.PS_EMAIL;

                            //--
                            ddlProvinceID.SelectedValue = reader.IsDBNull(i) ? "" : reader.GetValue(i).ToString(); ++i;
                            ddlAmphurID.Items.Clear();
                            string s1 = reader.IsDBNull(i) ? "" : reader.GetValue(i).ToString(); ++i;
                            DatabaseManager.BindDropDown(ddlAmphurID, "SELECT * FROM TB_AMPHUR WHERE PROVINCE_ID = '" + ddlProvinceID.SelectedValue + "'", "AMPHUR_TH", "AMPHUR_ID", "--กรุณาเลือก--");
                            ddlAmphurID.SelectedValue = s1;

                            ddlDistrictID.Items.Clear();
                            string s2 = reader.IsDBNull(i) ? "0" : reader.GetValue(i).ToString(); ++i;
                            DatabaseManager.BindDropDown(ddlDistrictID, "SELECT * FROM TB_DISTRICT WHERE AMPHUR_ID = '" + ddlAmphurID.SelectedValue + "'", "DISTRICT_TH", "DISTRICT_ID", "--กรุณาเลือก--");
                            ddlDistrictID.SelectedValue = s2;

                            tbZipcode.Text = reader.IsDBNull(i) ? "" : reader.GetValue(i).ToString(); ++i;
                            tbTelephone.Text = reader.IsDBNull(i) ? "" : reader.GetValue(i).ToString(); ++i;
                            //--

                            lbNationID.Text = Util.IsBlank(loginPerson.PS_NATION_NAME) ? "-" : loginPerson.PS_NATION_NAME;
                            lbCampusID.Text = Util.IsBlank(loginPerson.PS_CAMPUS_NAME) ? "-" : loginPerson.PS_CAMPUS_NAME;
                            lbFacultyID.Text = Util.IsBlank(loginPerson.PS_FACULTY_NAME) ? "-" : loginPerson.PS_FACULTY_NAME;
                            lbDivisionID.Text = Util.IsBlank(loginPerson.PS_DIVISION_NAME) ? "-" : loginPerson.PS_DIVISION_NAME;
                            lbWorkDivisionID.Text = Util.IsBlank(loginPerson.PS_WORK_DIVISION_NAME) ? "-" : loginPerson.PS_WORK_DIVISION_NAME;
                            lbStafftypeID.Text = Util.IsBlank(loginPerson.PS_STAFFTYPE_NAME) ? "-" : loginPerson.PS_STAFFTYPE_NAME;
                            lbTimeContactID.Text = Util.IsBlank(loginPerson.PS_TIME_CONTACT_NAME) ? "-" : loginPerson.PS_TIME_CONTACT_NAME;
                            lbBudgetID.Text = Util.IsBlank(loginPerson.PS_BUDGET_NAME) ? "-" : loginPerson.PS_BUDGET_NAME;
                            lbSubStafftypeID.Text = Util.IsBlank(loginPerson.PS_SUBSTAFFTYPE_NAME) ? "-" : loginPerson.PS_SUBSTAFFTYPE_NAME;
                            lbAdminPosID.Text = Util.IsBlank(loginPerson.PS_ADMIN_POS_NAME) ? "-" : loginPerson.PS_ADMIN_POS_NAME;
                            lbWorkPosID.Text = Util.IsBlank(loginPerson.PS_WORK_POS_NAME) ? "-" : loginPerson.PS_WORK_POS_NAME;
                            lbDateInwork.Text = Util.IsBlank(loginPerson.PS_INWORK_DATE.ToString()) ? "-" : loginPerson.PS_INWORK_DATE.Value.ToLongDateString();
                            lbDateStartThisU.Text = Util.IsBlank(loginPerson.PS_DATE_START_THIS_U.ToString()) ? "-" : loginPerson.PS_DATE_START_THIS_U.Value.ToLongDateString();
                            lbSpecialName.Text = Util.IsBlank(loginPerson.PS_SPECIAL_NAME) ? "-" : loginPerson.PS_SPECIAL_NAME;
                            lbTeachIscedID.Text = Util.IsBlank(loginPerson.PS_TEACH_ISCED_NAME) ? "-" : loginPerson.PS_TEACH_ISCED_NAME;
                            lbGradLevID.Text = Util.IsBlank(loginPerson.PS_GRAD_LEV_NAME) ? "-" : loginPerson.PS_GRAD_LEV_NAME;
                            lbGradCurr.Text = Util.IsBlank(loginPerson.PS_GRAD_CURR) ? "-" : loginPerson.PS_GRAD_CURR;
                            lbGradIscedID.Text = Util.IsBlank(loginPerson.PS_GRAD_ISCED_NAME) ? "-" : loginPerson.PS_GRAD_ISCED_NAME;
                            lbGradProgID.Text = Util.IsBlank(loginPerson.PS_GRAD_PROG_NAME) ? "-" : loginPerson.PS_GRAD_PROG_NAME;
                            lbGradUniv.Text = Util.IsBlank(loginPerson.PS_GRAD_UNIV) ? "-" : loginPerson.PS_GRAD_UNIV;
                            lbGradCountryID.Text = Util.IsBlank(loginPerson.PS_GRAD_COUNTRY_NAME) ? "-" : loginPerson.PS_GRAD_COUNTRY_NAME;
                            lbDeformID.Text = Util.IsBlank(loginPerson.PS_DEFORM_NAME) ? "-" : loginPerson.PS_DEFORM_NAME;
                            lbSitNo.Text = Util.IsBlank(loginPerson.PS_SIT_NO) ? "-" : loginPerson.PS_SIT_NO;
                            lbReligionID.Text = Util.IsBlank(loginPerson.PS_RELIGION_NAME) ? "-" : loginPerson.PS_RELIGION_NAME;
                            lbMovementTypeID.Text = Util.IsBlank(loginPerson.PS_MOVEMENT_TYPE_NAME) ? "-" : loginPerson.PS_MOVEMENT_TYPE_NAME;
                            lbMovementDate.Text = Util.IsBlank(loginPerson.PS_MOVEMENT_DATE.ToString()) ? "-" : loginPerson.PS_MOVEMENT_DATE.Value.ToLongDateString();
                        }
                    }
                }
            }
        }

        protected void BindDDL()
        {
            DatabaseManager.BindDropDown(ddlProvinceID, "SELECT * FROM TB_PROVINCE", "PROVINCE_TH", "PROVINCE_ID", "--กรุณาเลือกจังหวัด--");
            //ddlAmphurID.Items.Insert(0, new ListItem("--กรุณาเลือกอำเภอ--", "0"));
            //ddlDistrictID.Items.Insert(0, new ListItem("--กรุณาเลือกตำบล--", "0"));
            //tbZipcode.Text = "";
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

                        ddlAmphurID.Items.Insert(0, new ListItem("--กรุณาเลือกอำเภอ--", "0"));
                        ddlDistrictID.Items.Clear();
                        ddlDistrictID.Items.Insert(0, new ListItem("--กรุณาเลือกตำบล--", "0"));
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

                        ddlDistrictID.Items.Insert(0, new ListItem("--กรุณาเลือกตำบล--", "0"));
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

        protected void btnUpdateUser_Click(object sender, EventArgs e)
        {
            UPDATE_PERSON();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('แก้ไขข้อมูลเรียบร้อย')", true);
        }

        public int UPDATE_PERSON()
        {
            int id = 0;
            OracleConnection.ClearAllPools();
            using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
            {
                con.Open();
                using (OracleCommand com = new OracleCommand("UPDATE PS_PERSON SET PS_HOMEADD=:PS_HOMEADD, PS_MOO=:PS_MOO, PS_STREET=:PS_STREET, PS_PROVINCE_ID=:PS_PROVINCE_ID, PS_AMPHUR_ID=:PS_AMPHUR_ID, PS_DISTRICT_ID=:PS_DISTRICT_ID, PS_ZIPCODE=:PS_ZIPCODE, PS_TELEPHONE=:PS_TELEPHONE WHERE PS_CITIZEN_ID = '" + loginPerson.PS_CITIZEN_ID + "'", con))
                {
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
                    id = com.ExecuteNonQuery();

                }
            }
            return id;
        }

    }
}