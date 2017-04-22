using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OracleClient;
using WEB_PERSONAL.Class;
using System.Text;
using System.IO;
using System.Data;

namespace WEB_PERSONAL
{
    public partial class ReportPerson : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PersonnelSystem ps = PersonnelSystem.GetPersonnelSystem(this);
            Person loginPerson = ps.LoginPerson;
            if (loginPerson.PERSON_ROLE_ID != "2")
            {
                Server.Transfer("NoPermission.aspx");
            }

            if (!IsPostBack)
            {
                //DatabaseManager.BindDropDown(ddlV1Campus, "SELECT * FROM TB_CAMPUS", "CAMPUS_NAME", "CAMPUS_ID");

                string r = Request.QueryString["r"];
                if (r != null) {
                    if (r == "1") {
                        loadReport1();
                    } else if (r == "2") {
                        loadReport2();
                    }

                }

            }
        }

        protected void lbuV1Search_Click(object sender, EventArgs e) {
            //Response.Redirect("ReportPerson.aspx?r=1&c=" + ddlV1Campus.SelectedValue);
            Response.Redirect("ReportPerson.aspx?r=1");
        }

        protected void lbuV2Search_Click(object sender, EventArgs e) {
            Response.Redirect("ReportPerson.aspx?r=2");
        }

        private void loadReport1() {
            Panel1.Controls.Add(loadReport1Table());
        }

        private void loadReport2() {
            Panel1.Controls.Add(loadReport2Table());
        }

        private Table loadReport1Table() {
            Table table = new Table();
            table.CssClass = "ps-table-1";

            {
                TableHeaderRow row = new TableHeaderRow();
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "จำนวนผู้สำเร็จการศึกษาระบบ ปริญญาตรี ปริญญาโท และปริญญาเอก"; cell.ColumnSpan = 4; row.Cells.Add(cell); }
                table.Rows.Add(row);
            }


            {
                TableHeaderRow row = new TableHeaderRow();
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "วิทยาเขต"; cell.ColumnSpan = 1; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "ป.ตรี"; cell.ColumnSpan = 1; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "ป.โท"; cell.ColumnSpan = 1; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "ป.เอก"; cell.ColumnSpan = 1; row.Cells.Add(cell); }
                table.Rows.Add(row);
            }

            using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING)) {
                con.Open();
                using (OracleCommand com = new OracleCommand("SELECT " +
                        "CAMPUS_NAME, " +
                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_GRAD_LEV_ID = 40 AND PS_CAMPUS_ID = CAMPUS_ID) AA, " +
                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_GRAD_LEV_ID = 60 AND PS_CAMPUS_ID = CAMPUS_ID) BB, " +
                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_GRAD_LEV_ID = 80 AND PS_CAMPUS_ID = CAMPUS_ID) CC " +
                        "FROM TB_CAMPUS", con)) {
                    using (OracleDataReader reader = com.ExecuteReader()) {
                        while (reader.Read()) {
                            TableRow row = new TableRow();
                            { TableCell cell = new TableCell(); cell.Text = reader.GetString(0); row.Cells.Add(cell); }
                            { TableCell cell = new TableCell(); cell.Text = reader.GetValue(1).ToString(); row.Cells.Add(cell); }
                            { TableCell cell = new TableCell(); cell.Text = reader.GetValue(2).ToString(); row.Cells.Add(cell); }
                            { TableCell cell = new TableCell(); cell.Text = reader.GetValue(3).ToString(); row.Cells.Add(cell); }
                            table.Rows.Add(row);
                        }
                    }
                }
            }
            return table;
        }

        private Table loadReport2Table() {
            Table table = new Table();
            table.CssClass = "ps-table-1";

            {
                TableHeaderRow row = new TableHeaderRow();
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "ข้อมูลบุคลากร"; cell.ColumnSpan = 41; row.Cells.Add(cell); }
                table.Rows.Add(row);
            }


            {
                TableHeaderRow row = new TableHeaderRow();
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "CITIZEN_ID"; cell.ColumnSpan = 1; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "TITLE_ID"; cell.ColumnSpan = 1; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "STF_FNAME"; cell.ColumnSpan = 1; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "STF_LNAME"; cell.ColumnSpan = 1; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "GENDER_ID"; cell.ColumnSpan = 1; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "BIRTHDATE"; cell.ColumnSpan = 1; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "HOMEADD"; cell.ColumnSpan = 1; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "MOO"; cell.ColumnSpan = 1; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "STREET"; cell.ColumnSpan = 1; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "DISTRICT_ID"; cell.ColumnSpan = 1; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "AMPHUR_ID"; cell.ColumnSpan = 1; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "PROVINCE_ID"; cell.ColumnSpan = 1; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "ZIPCODE"; cell.ColumnSpan = 1; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "TELEPHONE"; cell.ColumnSpan = 1; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "NATION_ID"; cell.ColumnSpan = 1; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "CAMPUS_ID"; cell.ColumnSpan = 1; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "FACULTY_ID"; cell.ColumnSpan = 1; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "DIVISION_ID"; cell.ColumnSpan = 1; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "WORK_DIVISION_ID"; cell.ColumnSpan = 1; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "STAFFTYPE_ID"; cell.ColumnSpan = 1; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "TIME_CONTACT_ID"; cell.ColumnSpan = 1; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "BUDGET_ID"; cell.ColumnSpan = 1; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "SUBSTAFFTYPE_ID"; cell.ColumnSpan = 1; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "ADMIN_ID"; cell.ColumnSpan = 1; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "POSITION_ID"; cell.ColumnSpan = 1; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "WORK_POS_ID"; cell.ColumnSpan = 1; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "INWORK_DATE"; cell.ColumnSpan = 1; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "DATE_START_THIS_U"; cell.ColumnSpan = 1; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "SPECIAL_NAME"; cell.ColumnSpan = 1; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "TEACH_ISCED_ID"; cell.ColumnSpan = 1; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "GRAD_LEV_ID"; cell.ColumnSpan = 1; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "GRAD_CURR"; cell.ColumnSpan = 1; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "GRAD_ISCED_ID"; cell.ColumnSpan = 1; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "GRAD_PROG_ID"; cell.ColumnSpan = 1; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "GRAD_UNIV"; cell.ColumnSpan = 1; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "GRAD_COUNTRY_ID"; cell.ColumnSpan = 1; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "DEFORM_ID"; cell.ColumnSpan = 1; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "SIT_NO"; cell.ColumnSpan = 1; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "RELIGION_ID"; cell.ColumnSpan = 1; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "MOVEMENT_TYPE_ID"; cell.ColumnSpan = 1; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "MOVEMENT_TYPE_DATE"; cell.ColumnSpan = 1; row.Cells.Add(cell); }
                table.Rows.Add(row);
            }

            using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING)) {
                con.Open();
                using (OracleCommand com = new OracleCommand("SELECT PS_CITIZEN_ID,PS_TITLE_ID,PS_FIRSTNAME,PS_LASTNAME,PS_GENDER_ID,PS_BIRTHDAY_DATE,PS_HOMEADD,PS_MOO,PS_STREET,PS_DISTRICT_ID,PS_AMPHUR_ID,PS_PROVINCE_ID,PS_ZIPCODE,PS_TELEPHONE,PS_NATION_ID,PS_CAMPUS_ID,PS_FACULTY_ID,PS_DIVISION_ID,PS_WORK_DIVISION_ID,PS_STAFFTYPE_ID,PS_TIME_CONTACT_ID,PS_BUDGET_ID,PS_SUBSTAFFTYPE_ID,PS_ADMIN_POS_ID,PS_POSITION_ID,PS_WORK_POS_ID,PS_INWORK_DATE,PS_DATE_START_THIS_U,PS_SPECIAL_NAME,PS_TEACH_ISCED_ID,PS_GRAD_LEV_ID,PS_GRAD_CURR,PS_GRAD_ISCED_ID,PS_GRAD_PROG_ID,PS_GRAD_UNIV,PS_GRAD_COUNTRY_ID,PS_DEFORM_ID,PS_SIT_NO,PS_RELIGION_ID,PS_MOVEMENT_TYPE_ID,PS_MOVEMENT_DATE FROM PS_PERSON", con)) {
                    using (OracleDataReader reader = com.ExecuteReader()) {
                        while (reader.Read()) {
                            int i = 0;
                            TableRow row = new TableRow();
                            { TableCell cell = new TableCell(); cell.Text = reader.GetValue(i).ToString(); row.Cells.Add(cell); }
                            ++i;
                            { TableCell cell = new TableCell(); cell.Text = reader.GetValue(i).ToString(); row.Cells.Add(cell); }
                            ++i;
                            { TableCell cell = new TableCell(); cell.Text = reader.GetValue(i).ToString(); row.Cells.Add(cell); }
                            ++i;
                            { TableCell cell = new TableCell(); cell.Text = reader.GetValue(i).ToString(); row.Cells.Add(cell); }
                            ++i;
                            { TableCell cell = new TableCell(); cell.Text = reader.GetValue(i).ToString(); row.Cells.Add(cell); }
                            ++i;
                            { TableCell cell = new TableCell(); cell.Text = reader.IsDBNull(i) ? "" : reader.GetDateTime(i).ToString("dd/MMM/yyyy"); row.Cells.Add(cell); }
                            ++i;
                            { TableCell cell = new TableCell(); cell.Text = reader.GetValue(i).ToString(); row.Cells.Add(cell); }
                            ++i;
                            { TableCell cell = new TableCell(); cell.Text = reader.GetValue(i).ToString(); row.Cells.Add(cell); }
                            ++i;
                            { TableCell cell = new TableCell(); cell.Text = reader.GetValue(i).ToString(); row.Cells.Add(cell); }
                            ++i;
                            { TableCell cell = new TableCell(); cell.Text = reader.GetValue(i).ToString(); row.Cells.Add(cell); }
                            ++i;
                            { TableCell cell = new TableCell(); cell.Text = reader.GetValue(i).ToString(); row.Cells.Add(cell); }
                            ++i;
                            { TableCell cell = new TableCell(); cell.Text = reader.GetValue(i).ToString(); row.Cells.Add(cell); }
                            ++i;
                            { TableCell cell = new TableCell(); cell.Text = reader.GetValue(i).ToString(); row.Cells.Add(cell); }
                            ++i;
                            { TableCell cell = new TableCell(); cell.Text = reader.GetValue(i).ToString(); row.Cells.Add(cell); }
                            ++i;
                            { TableCell cell = new TableCell(); cell.Text = reader.GetValue(i).ToString(); row.Cells.Add(cell); }
                            ++i;
                            { TableCell cell = new TableCell(); cell.Text = reader.GetValue(i).ToString(); row.Cells.Add(cell); }
                            ++i;
                            { TableCell cell = new TableCell(); cell.Text = reader.GetValue(i).ToString(); row.Cells.Add(cell); }
                            ++i;
                            { TableCell cell = new TableCell(); cell.Text = reader.GetValue(i).ToString(); row.Cells.Add(cell); }
                            ++i;
                            { TableCell cell = new TableCell(); cell.Text = reader.GetValue(i).ToString(); row.Cells.Add(cell); }
                            ++i;
                            { TableCell cell = new TableCell(); cell.Text = reader.GetValue(i).ToString(); row.Cells.Add(cell); }
                            ++i;
                            { TableCell cell = new TableCell(); cell.Text = reader.GetValue(i).ToString(); row.Cells.Add(cell); }
                            ++i;
                            { TableCell cell = new TableCell(); cell.Text = reader.GetValue(i).ToString(); row.Cells.Add(cell); }
                            ++i;
                            { TableCell cell = new TableCell(); cell.Text = reader.GetValue(i).ToString(); row.Cells.Add(cell); }
                            ++i;
                            { TableCell cell = new TableCell(); cell.Text = reader.GetValue(i).ToString(); row.Cells.Add(cell); }
                            ++i;
                            { TableCell cell = new TableCell(); cell.Text = reader.GetValue(i).ToString(); row.Cells.Add(cell); }
                            ++i;
                            { TableCell cell = new TableCell(); cell.Text = reader.GetValue(i).ToString(); row.Cells.Add(cell); }
                            ++i;
                            { TableCell cell = new TableCell(); cell.Text = reader.IsDBNull(i) ? "" : reader.GetDateTime(i).ToString("dd/MM/yyyy"); row.Cells.Add(cell); }
                            ++i;
                            { TableCell cell = new TableCell(); cell.Text = reader.IsDBNull(i) ? "" : reader.GetDateTime(i).ToString("dd/MM/yyyy"); row.Cells.Add(cell); }
                            ++i;
                            { TableCell cell = new TableCell(); cell.Text = reader.GetValue(i).ToString(); row.Cells.Add(cell); }
                            ++i;
                            { TableCell cell = new TableCell(); cell.Text = reader.GetValue(i).ToString(); row.Cells.Add(cell); }
                            ++i;
                            { TableCell cell = new TableCell(); cell.Text = reader.GetValue(i).ToString(); row.Cells.Add(cell); }
                            ++i;
                            { TableCell cell = new TableCell(); cell.Text = reader.GetValue(i).ToString(); row.Cells.Add(cell); }
                            ++i;
                            { TableCell cell = new TableCell(); cell.Text = reader.GetValue(i).ToString(); row.Cells.Add(cell); }
                            ++i;
                            { TableCell cell = new TableCell(); cell.Text = reader.GetValue(i).ToString(); row.Cells.Add(cell); }
                            ++i;
                            { TableCell cell = new TableCell(); cell.Text = reader.GetValue(i).ToString(); row.Cells.Add(cell); }
                            ++i;
                            { TableCell cell = new TableCell(); cell.Text = reader.GetValue(i).ToString(); row.Cells.Add(cell); }
                            ++i;
                            { TableCell cell = new TableCell(); cell.Text = reader.GetValue(i).ToString(); row.Cells.Add(cell); }
                            ++i;
                            { TableCell cell = new TableCell(); cell.Text = reader.GetValue(i).ToString(); row.Cells.Add(cell); }
                            ++i;
                            { TableCell cell = new TableCell(); cell.Text = reader.GetValue(i).ToString(); row.Cells.Add(cell); }
                            ++i;
                            { TableCell cell = new TableCell(); cell.Text = reader.GetValue(i).ToString(); row.Cells.Add(cell); }
                            ++i;
                            { TableCell cell = new TableCell(); cell.Text = reader.IsDBNull(i) ? "": reader.GetDateTime(i).ToString("dd/MM/yyyy"); row.Cells.Add(cell); }
                            ++i;
                            table.Rows.Add(row);
                        }
                    }
                }
            }
            return table;
        }

        protected void lbuV1Export_Click(object sender, EventArgs e) {

            Table table = loadReport1Table();

            Response.ContentType = "application/x-msexcel";
            Response.AddHeader("Content-Disposition", "attachment;filename=จำนวนผู้สำเร็จการศึกษาระบบ ปริญญาตรี ปริญญาโท และปริญญาเอก.xls");
            Response.ContentEncoding = Encoding.UTF8;

            StringWriter tw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(tw);
            tw.WriteLine("<html>");
            tw.WriteLine("<meta http-equiv='Content-Type' content='text/html; charset=UTF-8' />");
            tw.WriteLine("<style>");
            tw.WriteLine("table { border-collapse:collapse; }");
            tw.WriteLine("td { border-collapse:collapse; border: thin solid black; }");
            tw.WriteLine("</style>");

            table.RenderControl(hw);

            tw.WriteLine("</html>");
            Response.Write(tw.ToString());
            Response.End();
        }

        protected void lbuV2Export_Click(object sender, EventArgs e) {
            Table table = loadReport2Table();

            Response.ContentType = "application/x-msexcel";
            Response.AddHeader("Content-Disposition", "attachment;filename=แสดงข้อมูลจัดส่ง สกอ.xls");
            Response.ContentEncoding = Encoding.UTF8;

            StringWriter tw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(tw);
            tw.WriteLine("<html>");
            tw.WriteLine("<meta http-equiv='Content-Type' content='text/html; charset=UTF-8' />");
            tw.WriteLine("<style>");
            tw.WriteLine("table { border-collapse:collapse; }");
            tw.WriteLine("td { border-collapse:collapse; border: thin solid black; }");
            tw.WriteLine("</style>");

            table.RenderControl(hw);

            tw.WriteLine("</html>");
            Response.Write(tw.ToString());
            Response.End();
        }
    }
}