using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OracleClient;
using WEB_PERSONAL.Class;
using System.IO;

namespace WEB_PERSONAL
{
    public partial class Reportproject : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] == null)
            {
                Response.Redirect("listproject-admin.aspx");
            }

            if (!IsPostBack)
            {
                ReadSelectID();
                
                using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
                {
                    con.Open();
                    using (OracleCommand com = new OracleCommand("SELECT START_DATE,END_DATE FROM TB_PROJECT WHERE PRO_ID = '" + MyCrypto.GetDecryptedQueryString(Request.QueryString["id"].ToString()) + "'", con))
                    {
                        using (OracleDataReader reader = com.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int i = 0;
                                string start = reader.GetDateTime(i).ToString("dd MMM yyyy"); ++i;
                                string end = reader.GetDateTime(i).ToString("dd MMM yyyy"); ++i;
                                if (!reader.IsDBNull(0) && !reader.IsDBNull(1))
                                {
                                    DateTime df = DateTime.Parse(start);
                                    DateTime dt = DateTime.Parse(end);
                                    int day = (int)(dt - df).TotalDays + 1;

                                    int year = (day / 365);
                                    int month = (day % 365) / 30;
                                    day = (day % 365) % 30;

                                    lbcalYear.Text = "" + year;
                                    lbcalMonth.Text = "" + month;
                                    lbcalDay.Text = "" + day;
                                }

                            }
                        }
                    }
                }
            }
        }

        private void ReadSelectID()
        {
            using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
            {
                con.Open();
                using (OracleCommand com = new OracleCommand("SELECT (SELECT (SELECT TITLE_NAME_TH FROM TB_TITLENAME WHERE PS_PERSON.PS_TITLE_ID = TB_TITLENAME.TITLE_ID) || ' ' || PS_FIRSTNAME || ' ' || PS_LASTNAME FROM PS_PERSON WHERE PS_PERSON.PS_CITIZEN_ID = TB_PROJECT.CITIZEN_ID) NAME, (SELECT (SELECT POSITION_WORK_NAME FROM TB_POSITION_WORK WHERE TB_POSITION_WORK.POSITION_WORK_ID = PS_PERSON.PS_WORK_POS_ID) FROM PS_PERSON WHERE PS_PERSON.PS_CITIZEN_ID = TB_PROJECT.CITIZEN_ID) POSITION_NAME, (SELECT (SELECT ADMIN_POSITION_NAME FROM TB_ADMIN_POSITION WHERE TB_ADMIN_POSITION.ADMIN_POSITION_ID = PS_PERSON.PS_ADMIN_POS_ID) FROM PS_PERSON WHERE PS_PERSON.PS_CITIZEN_ID = TB_PROJECT.CITIZEN_ID) DEGREE_NAME, (SELECT (SELECT CAMPUS_NAME FROM TB_CAMPUS WHERE TB_CAMPUS.CAMPUS_ID = PS_PERSON.PS_CAMPUS_ID) || ' ' || (SELECT FACULTY_NAME FROM TB_FACULTY WHERE TB_FACULTY.FACULTY_ID = PS_PERSON.PS_FACULTY_ID) || ' ' || (SELECT DIVISION_NAME FROM TB_DIVISION WHERE TB_DIVISION.DIVISION_ID = PS_PERSON.PS_DIVISION_ID) || ' ' || (SELECT WORK_NAME FROM TB_WORK_DIVISION WHERE TB_WORK_DIVISION.WORK_ID = PS_PERSON.PS_WORK_DIVISION_ID) FROM PS_PERSON WHERE PS_PERSON.PS_CITIZEN_ID = TB_PROJECT.CITIZEN_ID) DEPARTMENT_NAME, PROJECT_NAME, ADDRESS_PROJECT, START_DATE, END_DATE, EXPENSES, FUNDING, CERTIFICATE, SUMMARIZE_PROJECT, RESULT_TEACHING, RESULT_ACADEMIC, DIFFICULTY_PROJECT, RESULT_PROJECT, RESULT_RESEARCHING, RESULT_OTHER, COUNSEL FROM TB_PROJECT WHERE PRO_ID = '" + MyCrypto.GetDecryptedQueryString(Request.QueryString["id"].ToString()) + "'", con))
                {
                    using (OracleDataReader reader = com.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int i = 0;
                            lbName.Text = reader.IsDBNull(i) ? "" : reader.GetString(i); ++i;
                            lbPosition.Text = reader.IsDBNull(i) ? "" : reader.GetString(i); ++i;
                            lbDegree.Text = reader.IsDBNull(i) ? "" : reader.GetString(i); ++i;
                            lbDepartment.Text = reader.IsDBNull(i) ? "" : reader.GetString(i); ++i;
                            lbNameProject.Text = reader.IsDBNull(i) ? "" : reader.GetString(i); ++i;
                            lbAddressProject.Text = reader.IsDBNull(i) ? "" : reader.GetString(i); ++i;
                            lbDateStart.Text = reader.IsDBNull(i) ? "" : reader.GetDateTime(i).ToString("dd MMM yyyy"); ++i;
                            lbDateEnd.Text = reader.IsDBNull(i) ? "" : reader.GetDateTime(i).ToString("dd MMM yyyy"); ++i;
                            lbExpense.Text = reader.IsDBNull(i) ? "0" : reader.GetInt32(i).ToString("#,###"); ++i;
                            lbFunding.Text = reader.IsDBNull(i) ? "" : reader.GetString(i); ++i;
                            lbCertificate.Text = reader.IsDBNull(i) ? "" : reader.GetString(i); ++i;
                            lbSummaryProject.Text = reader.IsDBNull(i) ? "" : reader.GetString(i); ++i;
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

        protected void lbuExport_Click(object sender, EventArgs e)
        {
            string strBody = string.Empty;
            strBody = @"<html xmlns:o='urn:schemas-microsoft-com:office:office' " +
            "xmlns:w='urn:schemas-microsoft-com:office:word'" +
            "xmlns='http://www.w3.org/TR/REC-html40'>";

            strBody = strBody + "<!--[if gte mso 9]>" +
            "<xml>" +
            "<w:WordDocument>" +
            "<w:View>Print</w:View>" +
            "<w:Zoom>100</w:Zoom>" +
            "</w:WordDocument>" +
            "</xml>" +
            "<![endif]-->";
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Charset = "";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-word";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "inline; filename=แบบรายงานการฝึกอบรม/สัมมนา/ดูงาน.doc");

            StringBuilder htmlCode = new StringBuilder();
            htmlCode.Append("<html>");
            htmlCode.Append("<head>" + strBody + " <style type=\"text/css\">body {font-family:TH Sarabun New;font-size:16;}</style></head>");
            htmlCode.Append("<body>");

            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            tb.RenderControl(hw);

            htmlCode.Append("</body></html>");
            HttpContext.Current.Response.Write(htmlCode.ToString() + sw);
            HttpContext.Current.Response.End();
            HttpContext.Current.Response.Flush();
        }

    }
}