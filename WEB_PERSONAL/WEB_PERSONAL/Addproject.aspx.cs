using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WEB_PERSONAL.Class;
using System.IO;
using System.Data.OracleClient;

namespace WEB_PERSONAL
{
    public partial class Addproject : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PersonnelSystem ps = PersonnelSystem.GetPersonnelSystem(this);
            Person loginPerson = ps.LoginPerson;

            Notsuccess.Visible = true;
            success.Visible = false;

            if (!IsPostBack)
            {
                BindDDL();
            }
        }

        protected void BindDDL()
        {
            DatabaseManager.BindDropDown(ddlCategory, "SELECT * FROM TB_PROJECT_CATEGORY ORDER BY ABS(CATEGORY_ID)", "CATEGORY_NAME", "CATEGORY_ID", "--กรุณาเลือก--");
            DatabaseManager.BindDropDown(ddlCountry, "SELECT * FROM TB_PROJECT_COUNTRY ORDER BY ABS(COUNTRY_ID)", "COUNTRY_NAME", "COUNTRY_ID", "--กรุณาเลือก--");
            DatabaseManager.BindDropDown(ddlSubCountry, "SELECT * FROM TB_PROJECT_COUNTRY_SUB ORDER BY ABS(SUB_COUNTRY_ID)", "SUB_COUNTRY_NAME", "SUB_COUNTRY_ID", "--กรุณาเลือก--");
        }

        public void ChangeNotification(string type)
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

        public void ChangeNotification(string type, string text)
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

        protected void btnAddProject_Click(object sender, EventArgs e)
        {
            /*string[] validFileTypes = { "pdf" };
            string ext = System.IO.Path.GetExtension(FUdocument.PostedFile.FileName);
            //bool isValidFile = false;

           for (int i = 0; i < validFileTypes.Length; i++)
           {
               if (ext == "." + validFileTypes[i])
               {
                   isValidFile = true;
                   break;
               }
           }
          if (!isValidFile)
           {
               ScriptManager.GetCurrent(this.Page).SetFocus(this.FUdocument);
               ChangeNotification("danger", "กรุณาแนบไฟล์นามสกุล " + string.Join(",", validFileTypes) + " เท่านั้น");
               return;
           }

           else if (FUdocument.PostedFile.ContentLength > 26214400)
           {
               ScriptManager.GetCurrent(this.Page).SetFocus(this.FUdocument);
               ChangeNotification("danger", "กรุณาแนบไฟล์ไม่เกิน 25 MB");
               return;
           }
           else
           {
               ChangeNotification("", "");
           }*/

            if (tbStartDate.Text != "" && tbEndDate.Text != "")
            {
                DateTime dtEndDate = DateTime.Parse(tbEndDate.Text);
                DateTime dtStartDate = DateTime.Parse(tbStartDate.Text);
                int totalDay = (int)(dtEndDate - dtStartDate).TotalDays + 1;

                if (totalDay <= 0)
                {
                    notification.Attributes["class"] = "alert alert_danger";
                    notification.InnerHtml = "";
                    notification.InnerHtml += "<div> <img src='Image/Small/red_alert.png' /> วันที่เริ่มโครงการ - วันที่สิ้นสุดโครงการ : วันที่ไม่ถูกต้อง !</div>";
                    ScriptManager.GetCurrent(this.Page).SetFocus(this.tbStartDate);
                    return;
                }
                else
                {
                    notification.Attributes["class"] = "none";
                    notification.InnerHtml = "";
                }
            }

            if (Util.ToDateTimeOracle(tbStartDate.Text) > DateTime.Now)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('วันที่ไม่สามารถมากกว่าวันปัจจุบัน')", true);
                return;
            }
            if (Util.ToDateTimeOracle(tbEndDate.Text) > DateTime.Now)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('วันที่ไม่สามารถมากกว่าวันปัจจุบัน')", true);
                return;
            }

            PersonnelSystem ps = PersonnelSystem.GetPersonnelSystem(this);
            Person loginPerson = ps.LoginPerson;

            OracleConnection.ClearAllPools();
            using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
            {
                con.Open();
                using (OracleCommand com = new OracleCommand("INSERT INTO TB_PROJECT (CITIZEN_ID,CATEGORY_ID,COUNTRY_ID,SUB_COUNTRY_ID,PROJECT_NAME,ADDRESS_PROJECT,START_DATE,END_DATE,EXPENSES,FUNDING,CERTIFICATE,SUMMARIZE_PROJECT,RESULT_TEACHING,RESULT_ACADEMIC,DIFFICULTY_PROJECT,RESULT_PROJECT,RESULT_RESEARCHING,RESULT_OTHER,COUNSEL,PDF_FILE) VALUES (:CITIZEN_ID,:CATEGORY_ID,:COUNTRY_ID,:SUB_COUNTRY_ID,:PROJECT_NAME,:ADDRESS_PROJECT,:START_DATE,:END_DATE,:EXPENSES,:FUNDING,:CERTIFICATE,:SUMMARIZE_PROJECT,:RESULT_TEACHING,:RESULT_ACADEMIC,:DIFFICULTY_PROJECT,:RESULT_PROJECT,:RESULT_RESEARCHING,:RESULT_OTHER,:COUNSEL,:PDF_FILE)", con))
                {
                    com.Parameters.Add(new OracleParameter("CITIZEN_ID", loginPerson.PS_CITIZEN_ID));
                    com.Parameters.Add(new OracleParameter("CATEGORY_ID", Convert.ToInt32(ddlCategory.SelectedValue)));
                    com.Parameters.Add(new OracleParameter("COUNTRY_ID", Convert.ToInt32(ddlCountry.SelectedValue)));
                    com.Parameters.Add(new OracleParameter("SUB_COUNTRY_ID", Convert.ToInt32(ddlSubCountry.SelectedValue)));
                    com.Parameters.Add(new OracleParameter("PROJECT_NAME", tbProjectName.Text));
                    com.Parameters.Add(new OracleParameter("ADDRESS_PROJECT", tbAddressProject.Text));
                    com.Parameters.Add(new OracleParameter("START_DATE", DateTime.Parse(tbStartDate.Text)));
                    com.Parameters.Add(new OracleParameter("END_DATE", DateTime.Parse(tbEndDate.Text)));
                    com.Parameters.Add(new OracleParameter("EXPENSES", Convert.ToInt32(tbExpenses.Text)));
                    com.Parameters.Add(new OracleParameter("FUNDING", tbFunding.Text));
                    com.Parameters.Add(new OracleParameter("CERTIFICATE", tbCertificate.Text));
                    com.Parameters.Add(new OracleParameter("SUMMARIZE_PROJECT", tbSummarizeProject.Text));
                    com.Parameters.Add(new OracleParameter("RESULT_TEACHING", tbResultTeaching.Text));
                    com.Parameters.Add(new OracleParameter("RESULT_ACADEMIC", tbResultAcademic.Text));
                    com.Parameters.Add(new OracleParameter("DIFFICULTY_PROJECT", tbDifficultyProject.Text));
                    com.Parameters.Add(new OracleParameter("RESULT_PROJECT", tbResultProject.Text));
                    com.Parameters.Add(new OracleParameter("RESULT_RESEARCHING", tbResultResearching.Text));
                    com.Parameters.Add(new OracleParameter("RESULT_OTHER", tbResultOther.Text));
                    com.Parameters.Add(new OracleParameter("COUNSEL", tbCounsel.Text));
                    if (FUdocument.HasFile)
                    {
                        string CountBase = DatabaseManager.ExecuteString("SELECT COUNT(*) FROM TB_PROJECT WHERE CITIZEN_ID = '" + loginPerson.PS_CITIZEN_ID + "'");
                        FileInfo fi = new FileInfo(FUdocument.FileName);
                        string imgFile = "CID=" + loginPerson.PS_CITIZEN_ID + "&count=" + CountBase + fi.Extension;
                        FUdocument.SaveAs(Server.MapPath("Upload/Project/PDF/" + imgFile));
                        com.Parameters.Add(new OracleParameter("PDF_FILE", imgFile));
                    }
                    else
                    {
                        com.Parameters.Add(new OracleParameter("PDF_FILE", DBNull.Value));
                    }
                    com.ExecuteNonQuery();
                }
            }


            Notsuccess.Visible = false;
            success.Visible = true;

        }

    }
}