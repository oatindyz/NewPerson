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

namespace WEB_PERSONAL {
    public partial class ReportInsig : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            PersonnelSystem ps = PersonnelSystem.GetPersonnelSystem(this);
            Person loginPerson = ps.LoginPerson;
            if (loginPerson.PERSON_ROLE_ID != "4") {
                Server.Transfer("NoPermission.aspx");
            }

            if (!IsPostBack) {
                //DatabaseManager.BindDropDown(ddlV1Campus, "SELECT * FROM TB_CAMPUS", "CAMPUS_NAME", "CAMPUS_ID");

                string r = Request.QueryString["r"];
                if (r != null) {
                    if (r == "1") {
                        loadReport1();
                    }
                }

            }
        }

        protected void lbuV1Search_Click(object sender, EventArgs e) {
            Response.Redirect("ReportInsig.aspx?r=1");
        }

        private void loadReport1() {
            Panel1.Controls.Add(loadReport1Table());
        }

        private Table loadReport1Table() {
            Table table = new Table();
            table.CssClass = "ps-table-1";

            {
                TableHeaderRow row = new TableHeaderRow();
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "รายชื่อผู้ที่ขอเครื่องราชฯ"; cell.ColumnSpan = 4; row.Cells.Add(cell); }
                table.Rows.Add(row);
            }


            {
                TableHeaderRow row = new TableHeaderRow();
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "ลำดับ"; cell.ColumnSpan = 1; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "ชื่อ"; cell.ColumnSpan = 1; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "เครื่องราชฯ"; cell.ColumnSpan = 1; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "วันที่ขอ"; cell.ColumnSpan = 1; row.Cells.Add(cell); }
                table.Rows.Add(row);
            }

            using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING)) {
                con.Open();
                using (OracleCommand com = new OracleCommand("SELECT PS_FIRSTNAME || ' ' || PS_LASTNAME NAME, INSIG_GRADE_NAME_L, REQ_DATE FROM TB_INSIG_PERSON, PS_PERSON, TB_INSIG_GRADE WHERE TB_INSIG_PERSON.CITIZEN_ID = PS_CITIZEN_ID AND TB_INSIG_PERSON.INSIG_ID = TB_INSIG_GRADE.INSIG_GRADE_ID AND IP_STATUS_ID = 1", con)) {
                    using (OracleDataReader reader = com.ExecuteReader()) {
                        int i = 0;
                        while (reader.Read()) {
                            TableRow row = new TableRow();
                            { TableCell cell = new TableCell(); cell.Text = "" + (++i); row.Cells.Add(cell); }
                            { TableCell cell = new TableCell(); cell.Text = reader.GetValue(0).ToString(); row.Cells.Add(cell); }
                            { TableCell cell = new TableCell(); cell.Text = reader.GetValue(1).ToString(); row.Cells.Add(cell); }
                            { TableCell cell = new TableCell(); cell.Text = reader.GetDateTime(2).ToLongDateString(); row.Cells.Add(cell); }
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
            Response.AddHeader("Content-Disposition", "attachment;filename=รายชื่อผู้ที่ขอเครื่องราชฯ.xls");
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