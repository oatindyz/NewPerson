using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WEB_PERSONAL.Class;
using System.Data.OracleClient;

namespace WEB_PERSONAL
{
    public partial class Request : System.Web.UI.Page
    {
        private Person loginPerson;
        protected void Page_Load(object sender, EventArgs e)
        {
            PersonnelSystem ps = PersonnelSystem.GetPersonnelSystem(this);
            loginPerson = ps.LoginPerson;

            if (loginPerson.PERSON_ROLE_ID == "2")
            {
                SpanBack.Visible = true;
            }else
            {
                SpanBack.Visible = false;
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
        }

        protected void BindDDL()
        {
            DatabaseManager.BindDropDown(ddlTitleID, "SELECT * FROM TB_TITLENAME ORDER BY ABS(TITLE_ID) ASC", "TITLE_NAME_TH", "TITLE_ID", "--กรุณาเลือก--");
            DatabaseManager.BindDropDown(ddlGenderID, "SELECT * FROM TB_GENDER ORDER BY ABS(GENDER_ID) ASC", "GENDER_NAME", "GENDER_ID", "--กรุณาเลือก--");
        }

        protected void btnSaveRequest_Click(object sender, EventArgs e)
        {
            OracleConnection.ClearAllPools();
            using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
            {
                con.Open();
                using (OracleCommand com = new OracleCommand("INSERT INTO TB_REQUEST (CITIZEN_ID,R_STATUS_ID,DATE_START,TITLE_ID,FIRSTNAME,LASTNAME,GENDER_ID,BIRTHDAY_DATE,EMAIL) VALUES (:CITIZEN_ID,:R_STATUS_ID,:DATE_START,:TITLE_ID,:FIRSTNAME,:LASTNAME,:GENDER_ID,:BIRTHDAY_DATE,:EMAIL)", con))
                {
                    com.Parameters.Add(new OracleParameter("CITIZEN_ID", loginPerson.PS_CITIZEN_ID));
                    com.Parameters.Add(new OracleParameter("R_STATUS_ID", "0"));
                    com.Parameters.Add(new OracleParameter("DATE_START", DateTime.Today));
                    if (Util.IsBlank(ddlTitleID.SelectedValue)) { com.Parameters.Add(new OracleParameter("TITLE_ID", DBNull.Value)); }
                    else { com.Parameters.Add(new OracleParameter("TITLE_ID", ddlTitleID.SelectedValue)); }

                    if (Util.IsBlank(tbFirstName.Text)) { com.Parameters.Add(new OracleParameter("FIRSTNAME", DBNull.Value)); }
                    else
                    {
                        com.Parameters.Add(new OracleParameter("FIRSTNAME", tbFirstName.Text));

                        if (Util.IsBlank(tbLastName.Text)) { com.Parameters.Add(new OracleParameter("LASTNAME", DBNull.Value)); }
                        else { com.Parameters.Add(new OracleParameter("LASTNAME", tbLastName.Text)); }

                        if (Util.IsBlank(ddlGenderID.SelectedValue)) { com.Parameters.Add(new OracleParameter("GENDER_ID", DBNull.Value)); }
                        else { com.Parameters.Add(new OracleParameter("GENDER_ID", ddlGenderID.SelectedValue)); }

                        if (Util.IsBlank(tbBirthdayDate.Text)) { com.Parameters.Add(new OracleParameter("BIRTHDAY_DATE", DBNull.Value)); }
                        else { com.Parameters.Add(new OracleParameter("BIRTHDAY_DATE", Util.ToDateTimeOracle(tbBirthdayDate.Text))); }

                        if (Util.IsBlank(tbEmail.Text)) { com.Parameters.Add(new OracleParameter("EMAIL", DBNull.Value)); }
                        else { com.Parameters.Add(new OracleParameter("EMAIL", tbEmail.Text)); }

                        com.ExecuteNonQuery();
                    }
                }
            }

            DataShow.Visible = false;
            SaveShow.Visible = true;
        }


    }
}