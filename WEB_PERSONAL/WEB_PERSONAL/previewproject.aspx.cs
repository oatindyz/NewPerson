using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OracleClient;
using WEB_PERSONAL.Class;

namespace WEB_PERSONAL
{
    public partial class previewproject : System.Web.UI.Page
    {
        int id = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            Notsuccess.Visible = true;
            delete.Visible = false;
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    int.TryParse(MyCrypto.GetDecryptedQueryString(Request.QueryString["id"].ToString()), out id);
                    ReadSelectID();
                }
                else { Response.Redirect("listproject.aspx"); }
            }
            ReadFile();
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

                file_pdf.Controls.Add(p);
            }
        }

        private void ReadSelectID()
        {
            using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
            {
                con.Open();
                using (OracleCommand com = new OracleCommand("SELECT (SELECT COUNTRY_NAME FROM TB_PROJECT_COUNTRY WHERE TB_PROJECT_COUNTRY.COUNTRY_ID = TB_PROJECT.COUNTRY_ID) COUNTRY_NAME, (SELECT SUB_COUNTRY_NAME FROM TB_PROJECT_COUNTRY_SUB WHERE TB_PROJECT_COUNTRY_SUB.SUB_COUNTRY_ID = TB_PROJECT.SUB_COUNTRY_ID) SUB_COUNTRY_NAME, (SELECT CATEGORY_NAME FROM TB_PROJECT_CATEGORY WHERE TB_PROJECT_CATEGORY.CATEGORY_ID = TB_PROJECT.CATEGORY_ID) CATEGORY_NAME, PROJECT_NAME, ADDRESS_PROJECT, START_DATE, END_DATE, EXPENSES, FUNDING, CERTIFICATE, SUMMARIZE_PROJECT, RESULT_TEACHING, RESULT_ACADEMIC, DIFFICULTY_PROJECT, RESULT_PROJECT, RESULT_RESEARCHING, RESULT_OTHER, COUNSEL FROM TB_PROJECT WHERE PRO_ID = '" + MyCrypto.GetDecryptedQueryString(Request.QueryString["id"].ToString()) + "'", con))
                {
                    using (OracleDataReader reader = com.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int i = 0;
                            lbCountry.Text = reader.IsDBNull(i) ? "" : reader.GetString(i); ++i;
                            lbSubCountry.Text = reader.IsDBNull(i) ? "" : reader.GetString(i); ++i;
                            lbCategory.Text = reader.IsDBNull(i) ? "" : reader.GetString(i); ++i;
                            lbProjectName.Text = reader.IsDBNull(i) ? "" : reader.GetString(i); ++i;
                            lbAddressProject.Text = reader.IsDBNull(i) ? "" : reader.GetString(i); ++i;
                            lbStartDate.Text = reader.IsDBNull(i) ? "" : reader.GetDateTime(i).ToString("dd MMM yyyy"); ++i;
                            lbEndDate.Text = reader.IsDBNull(i) ? "" : reader.GetDateTime(i).ToString("dd MMM yyyy"); ++i;
                            lbExpenses.Text = reader.IsDBNull(i) ? "" : reader.GetInt32(i).ToString("#,###"); ++i;
                            lbFunding.Text = reader.IsDBNull(i) ? "" : reader.GetString(i); ++i;
                            lbCertificate.Text = reader.IsDBNull(i) ? "" : reader.GetString(i); ++i;
                            lbSummarizeProject.Text = reader.IsDBNull(i) ? "" : reader.GetString(i); ++i;
                            lbResultTeaching.Text = reader.IsDBNull(i) ? "" : reader.GetString(i); ++i;
                            lbResultAcademic.Text = reader.IsDBNull(i) ? "" : reader.GetString(i); ++i;
                            lbDifficultyProject.Text = reader.IsDBNull(i) ? "" : reader.GetString(i); ++i;
                            lbResultProject.Text = reader.IsDBNull(i) ? "" : reader.GetString(i); ++i;
                            lbResultResearching.Text = reader.IsDBNull(i) ? "" : reader.GetString(i); ++i;
                            lbResultOther.Text = reader.IsDBNull(i) ? "" : reader.GetString(i); ++i;
                            lbCounsel.Text = reader.IsDBNull(i) ? "" : reader.GetString(i); ++i;

                        }
                    }
                }
            }
        }

        protected void lbuEdit_Click(object sender, EventArgs e)
        {
            string link = MyCrypto.GetDecryptedQueryString(Request.QueryString["id"].ToString());
            string encrypt = MyCrypto.GetEncryptedQueryString(link);
            Response.Redirect("editproject.aspx?id=" + encrypt);
        }

        protected void lbuDelete_Click(object sender, EventArgs e)
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

                string pathVS = Server.MapPath("Upload/Project/PDF/" + PDF_FILE);
                if ((System.IO.File.Exists(pathVS)))
                {
                    System.IO.File.Delete(pathVS);
                }

                DatabaseManager.ExecuteNonQuery("DELETE TB_PROJECT WHERE PRO_ID = '" + int.Parse(MyCrypto.GetDecryptedQueryString(Request.QueryString["id"].ToString())) + "'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('ลบข้อมูลเรียบร้อย')", true);
            }

            string link = MyCrypto.GetDecryptedQueryString(Request.QueryString["id"].ToString());
            DatabaseManager.ExecuteNonQuery("DELETE TB_PROJECT WHERE PRO_ID = '" + link + "'");
            Notsuccess.Visible = false;
            delete.Visible = true;
        }
    }
}