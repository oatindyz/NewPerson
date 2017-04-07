using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WEB_PERSONAL.Class;
using System.IO;

namespace WEB_PERSONAL
{
    public partial class Addproject : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Notsuccess.Visible = true;
            success.Visible = false;

            if (!IsPostBack)
            {
                BindDDL();
            }
        }

        protected void BindDDL()
        {
            DatabaseManager.BindDropDown(ddlCategory, "SELECT * FROM TB_PROJECT_CATEGORY ORDER BY CATEGORY_ID", "CATEGORY_NAME", "CATEGORY_ID", "--กรุณาเลือก--");
            DatabaseManager.BindDropDown(ddlCountry, "SELECT * FROM TB_PROJECT_COUNTRY ORDER BY COUNTRY_ID", "COUNTRY_NAME", "COUNTRY_ID", "--กรุณาเลือก--");
            DatabaseManager.BindDropDown(ddlSubCountry, "SELECT * FROM TB_PROJECT_COUNTRY_SUB ORDER BY SUB_COUNTRY_ID", "SUB_COUNTRY_NAME", "SUB_COUNTRY_ID", "--กรุณาเลือก--");
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
            string[] validFileTypes = { "pdf" };
            string ext = System.IO.Path.GetExtension(FUdocument.PostedFile.FileName);
            bool isValidFile = false;

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
            }

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

            PersonnelSystem ps = PersonnelSystem.GetPersonnelSystem(this);
            Person loginPerson = ps.LoginPerson;
            Project p = new Project();

            p.CITIZEN_ID = loginPerson.PS_CITIZEN_ID;
            p.CATEGORY_ID = Convert.ToInt32(ddlCategory.SelectedValue);
            p.PROJECT_NAME = tbProjectName.Text;
            p.ADDRESS_PROJECT = tbAddressProject.Text;
            p.START_DATE = DateTime.Parse(tbStartDate.Text);
            p.END_DATE = DateTime.Parse(tbEndDate.Text);
            p.EXPENSES = Convert.ToInt32(tbExpenses.Text);
            p.FUNDING = tbFunding.Text;
            p.CERTIFICATE = tbCertificate.Text;
            p.SUMMARIZE_PROJECT = tbSummarizeProject.Text;
            p.RESULT_TEACHING = tbResultTeaching.Text;
            p.RESULT_ACADEMIC = tbResultAcademic.Text;
            p.DIFFICULTY_PROJECT = tbDifficultyProject.Text;
            p.RESULT_PROJECT = tbResultProject.Text;
            p.RESULT_RESEARCHING = tbResultResearching.Text;
            p.RESULT_OTHER = tbResultOther.Text;
            p.COUNSEL = tbCounsel.Text;
            p.COUNTRY_ID = Convert.ToInt32(ddlCountry.SelectedValue);
            p.SUB_COUNTRY_ID = Convert.ToInt32(ddlSubCountry.SelectedValue);
            if (FUdocument.HasFile)
            {
                string CountBase = DatabaseManager.ExecuteString("SELECT COUNT(*) FROM TB_PROJECT WHERE CITIZEN_ID = '" + loginPerson.PS_CITIZEN_ID + "'");
                FileInfo fi = new FileInfo(FUdocument.FileName);
                string imgFile = "CID=" + loginPerson.PS_CITIZEN_ID + "&count=" + CountBase + fi.Extension;
                FUdocument.SaveAs(Server.MapPath("Upload/Project/PDF/" + imgFile));
                p.PDF_FILE = imgFile;
            }
            p.INSERT_PROJECT();

            Notsuccess.Visible = false;
            success.Visible = true;

        }

    }
}