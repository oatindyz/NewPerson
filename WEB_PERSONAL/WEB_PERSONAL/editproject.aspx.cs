using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OracleClient;
using System.Data;
using System.Text;
using WEB_PERSONAL.Class;
using System.IO;

namespace WEB_PERSONAL
{
    public partial class editproject : System.Web.UI.Page
    {
        Person loginPerson;
        protected void Page_Load(object sender, EventArgs e)
        {
            PersonnelSystem ps = PersonnelSystem.GetPersonnelSystem(this);
            loginPerson = ps.LoginPerson;

            Notsuccess.Visible = true;
            success.Visible = false;

            if (Request.QueryString["id"] == null)
            {
                Response.Redirect("listproject.aspx");
            }

            if (!IsPostBack)
            {
                BindDDL();
                ReadSelectID();
            }
            ReadFile();
        }

        protected void BindDDL()
        {
            DatabaseManager.BindDropDown(ddlCategory, "SELECT * FROM TB_PROJECT_CATEGORY ORDER BY ABS(CATEGORY_ID)", "CATEGORY_NAME", "CATEGORY_ID", "--กรุณาเลือก--");
            DatabaseManager.BindDropDown(ddlCountry, "SELECT * FROM TB_PROJECT_COUNTRY ORDER BY ABS(COUNTRY_ID)", "COUNTRY_NAME", "COUNTRY_ID", "--กรุณาเลือก--");
            DatabaseManager.BindDropDown(ddlSubCountry, "SELECT * FROM TB_PROJECT_COUNTRY_SUB ORDER BY ABS(SUB_COUNTRY_ID)", "SUB_COUNTRY_NAME", "SUB_COUNTRY_ID", "--กรุณาเลือก--");
        }

        private void ReadFile()
        {
            List<int> pro_id = new List<int>();
            List<string> pdf_file = new List<string>();

            using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
            {
                con.Open();
                using (OracleCommand com = new OracleCommand("SELECT PRO_ID, PDF_FILE FROM TB_PROJECT WHERE PRO_ID = " + int.Parse(MyCrypto.GetDecryptedQueryString(Request.QueryString["id"].ToString())), con))
                {
                    using (OracleDataReader reader = com.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (!reader.IsDBNull(1))
                            {
                                pro_id.Add(reader.GetInt32(0));
                                pdf_file.Add(reader.GetString(1));
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < pro_id.Count; i++)
            {
                string path = "Upload/Project/PDF/" + pdf_file[i];
                int PRO_ID = pro_id[i];
                string PDF_FILE = pdf_file[i];

                Panel p = new Panel();
                p.Style.Add("display", "inline-block");

                LinkButton lb = new LinkButton();
                lb.Attributes["href"] = path;
                lb.Text = "ดูไฟล์แนบ (รูปภาพ,เอกสาร ประกอบการอบรม)";
                p.Controls.Add(lb);

                LinkButton lbDelete = new LinkButton();
                lbDelete.CssClass = "ps-button";
                lbDelete.Text = "<img src='Image/Small/delete.png' class='icon_left' />ลบ";
                lbDelete.Click += (e1, e2) =>
                {
                    lbDelete.Attributes.Add("onclick", "javascript:if(!confirm('คุณต้องการที่จะลบใช่หรือไม่'))return false;");
                    FileInfo FileIn = new FileInfo(Server.MapPath("Upload/Project/PDF/" + PDF_FILE));
                    if (FileIn.Exists)
                    {
                        FileIn.Delete();
                    }
                    DatabaseManager.ExecuteNonQuery("UPDATE TB_PROJECT SET PDF_FILE = (null) WHERE PRO_ID = " + int.Parse(MyCrypto.GetDecryptedQueryString(Request.QueryString["id"].ToString())));
                    Page.Response.Redirect(Page.Request.Url.ToString(), true);
                };

                p.Controls.Add(lbDelete);
                file_pdf.Controls.Add(p);
            }
        }

        private void ReadSelectID()
        {
            using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
            {
                con.Open();
                using (OracleCommand com = new OracleCommand("SELECT COUNTRY_ID, SUB_COUNTRY_ID, CATEGORY_ID, PROJECT_NAME, ADDRESS_PROJECT, START_DATE, END_DATE, EXPENSES, FUNDING, CERTIFICATE, SUMMARIZE_PROJECT, RESULT_TEACHING, RESULT_ACADEMIC, DIFFICULTY_PROJECT, RESULT_PROJECT, RESULT_RESEARCHING, RESULT_OTHER, COUNSEL FROM TB_PROJECT WHERE PRO_ID = '" + MyCrypto.GetDecryptedQueryString(Request.QueryString["id"].ToString()) + "'", con))
                {
                    using (OracleDataReader reader = com.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int i = 0;
                            ddlCountry.SelectedValue = reader.IsDBNull(i) ? "" : reader.GetInt32(i).ToString(); ++i;
                            ddlSubCountry.SelectedValue = reader.IsDBNull(i) ? "" : reader.GetInt32(i).ToString(); ++i;
                            ddlCategory.SelectedValue = reader.IsDBNull(i) ? "" : reader.GetInt32(i).ToString(); ++i;
                            tbProjectName.Text = reader.IsDBNull(i) ? "" : reader.GetString(i); ++i;
                            tbAddressProject.Text = reader.IsDBNull(i) ? "" : reader.GetString(i); ++i;
                            tbStartDate.Text = reader.IsDBNull(i) ? "" : reader.GetDateTime(i).ToString("dd MMM yyyy"); ++i;
                            tbEndDate.Text = reader.IsDBNull(i) ? "" : reader.GetDateTime(i).ToString("dd MMM yyyy"); ++i;
                            tbExpenses.Text = reader.IsDBNull(i) ? "" : reader.GetInt32(i).ToString(); ++i;
                            tbFunding.Text = reader.IsDBNull(i) ? "" : reader.GetString(i); ++i;
                            tbCertificate.Text = reader.IsDBNull(i) ? "" : reader.GetString(i); ++i;
                            tbSummarizeProject.Text = reader.IsDBNull(i) ? "" : reader.GetString(i); ++i;
                            tbResultTeaching.Text = reader.IsDBNull(i) ? "" : reader.GetString(i); ++i;
                            tbResultAcademic.Text = reader.IsDBNull(i) ? "" : reader.GetString(i); ++i;
                            tbDifficultyProject.Text = reader.IsDBNull(i) ? "" : reader.GetString(i); ++i;
                            tbResultProject.Text = reader.IsDBNull(i) ? "" : reader.GetString(i); ++i;
                            tbResultResearching.Text = reader.IsDBNull(i) ? "" : reader.GetString(i); ++i;
                            tbResultOther.Text = reader.IsDBNull(i) ? "" : reader.GetString(i); ++i;
                            tbCounsel.Text = reader.IsDBNull(i) ? "" : reader.GetString(i); ++i;

                        }
                    }
                }
            }
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

        protected void btnUpdateProject_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null)
            {
                OracleConnection.ClearAllPools();
                using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
                {
                    con.Open();
                    using (OracleCommand com = new OracleCommand("SELECT PRO_ID FROM TB_PROJECT WHERE START_DATE BETWEEN " + Util.DatabaseToDateSearch(tbStartDate.Text) + " AND " + Util.DatabaseToDateSearch(tbEndDate.Text) + " AND CITIZEN_ID = '" + loginPerson.PS_CITIZEN_ID + "'", con))
                    {
                        using (OracleDataReader reader = com.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (!reader.IsDBNull(0))
                                {
                                    Project ProjectData = new Project();
                                    ProjectData.Load(reader.GetInt32(0));
                                    ChangeNotification("danger", "ไม่สามารถเพิ่มข้อมูลได้ พบวันซ้อนทับกัน (รหัสโครงการ " + ProjectData.PRO_ID + ", " + ProjectData.START_DATE.Value.ToLongDateString() + " ถึง " + ProjectData.END_DATE.Value.ToLongDateString() + ")");
                                    return;
                                }
                            }
                        }
                    }
                }

                /*string[] validFileTypes = { "pdf" };
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

                using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
                {
                    PersonnelSystem ps = PersonnelSystem.GetPersonnelSystem(this);
                    Person loginPerson = ps.LoginPerson;

                    con.Open();

                    string query = "Update TB_PROJECT Set";
                    query += " CATEGORY_ID = :CATEGORY_ID ,";
                    query += " COUNTRY_ID = :COUNTRY_ID ,";
                    query += " SUB_COUNTRY_ID = :SUB_COUNTRY_ID ,";
                    query += " PROJECT_NAME = :PROJECT_NAME ,";
                    query += " ADDRESS_PROJECT = :ADDRESS_PROJECT ,";
                    query += " START_DATE = :START_DATE ,";
                    query += " END_DATE = :END_DATE ,";
                    query += " EXPENSES = :EXPENSES ,";
                    query += " FUNDING = :FUNDING ,";
                    query += " CERTIFICATE = :CERTIFICATE ,";
                    query += " SUMMARIZE_PROJECT = :SUMMARIZE_PROJECT ,";
                    query += " RESULT_TEACHING = :RESULT_TEACHING ,";
                    query += " RESULT_ACADEMIC = :RESULT_ACADEMIC ,";
                    query += " DIFFICULTY_PROJECT = :DIFFICULTY_PROJECT ,";
                    query += " RESULT_PROJECT = :RESULT_PROJECT ,";
                    query += " RESULT_RESEARCHING = :RESULT_RESEARCHING ,";
                    query += " RESULT_OTHER = :RESULT_OTHER ,";
                    query += " COUNSEL = :COUNSEL ,";
                    query += " PDF_FILE = :PDF_FILE ";
                    query += " where PRO_ID = :PRO_ID ";

                    using (OracleCommand com = new OracleCommand(query, con))
                    {
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
                        com.Parameters.Add(new OracleParameter("PRO_ID", int.Parse(MyCrypto.GetDecryptedQueryString(Request.QueryString["id"].ToString()))));
                        com.ExecuteNonQuery();
                    }
                }

                ChangeNotification("", "");
                Notsuccess.Visible = false;
                success.Visible = true;
            }
        }
    }
}