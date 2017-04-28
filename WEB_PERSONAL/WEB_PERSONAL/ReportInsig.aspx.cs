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
    public partial class ReportInsig : System.Web.UI.Page
    {
        Person loginPerson;
        protected void Page_Load(object sender, EventArgs e)
        {
            PersonnelSystem ps = PersonnelSystem.GetPersonnelSystem(this);
            loginPerson = ps.LoginPerson;

            if (!IsPostBack)
            {
                if (loginPerson.PERSON_ROLE_ID == "4")
                {
                    divOfficer.Visible = true;
                    divUser.Visible = false;
                    DatabaseManager.BindDropDown(ddlCampus, "SELECT * FROM TB_CAMPUS", "CAMPUS_NAME", "CAMPUS_ID", "--กรุณาเลือก--");
                    ddlView.Items.Add(new ListItem("--กรุณาเลือก--", ""));
                    ddlView.Items.Add(new ListItem("แสดงรายชื่อผู้ที่กำลังขอเครื่องราชฯ", "1"));
                    ddlView.Items.Add(new ListItem("แสดงรายชื่อผู้ที่ได้รับเครื่องราชฯ", "2"));

                    int yearMin = DatabaseManager.ExecuteInt("SELECT MIN(EXTRACT(YEAR FROM GET_DATE)) FROM TB_INSIG_PERSON") + 543;
                    int yearMax = DatabaseManager.ExecuteInt("SELECT MAX(EXTRACT(YEAR FROM GET_DATE)) FROM TB_INSIG_PERSON") + 543;
                    
                    for (int i = yearMin; i <= yearMax; ++i) {
                        ddlBudgetYear.Items.Add(new System.Web.UI.WebControls.ListItem("" + i, "" + (i-543)));
                    }
                    ddlBudgetYear.Items.Insert(0, new ListItem("--กรุณาเลือก--", ""));
                }
                else
                {
                    string CheckNull = DatabaseManager.ExecuteString("SELECT COUNT(*) FROM TB_INSIG_PERSON WHERE CITIZEN_ID = '" + loginPerson.PS_CITIZEN_ID + "'");
                    if (CheckNull != "0")
                    {
                        divOfficer.Visible = false;
                        divUser.Visible = true;
                        lbFinish.Visible = false;
                        Table table;
                        table = loadReportSelf();
                        if (table == null)
                        {
                            return;
                        }
                        Panel2.Controls.Clear();
                        Panel2.Controls.Add(loadReportSelf());
                    }
                    else
                    {
                        divUser.Visible = true;
                        lbuV2Export.Visible = false;
                    }
                }
               
            }
        }

        protected void lbuV1Search_Click(object sender, EventArgs e)
        {
            if (ddlView.SelectedValue == "1")
            {
                Table table;
                table = loadReport1Table();
                if (table == null)
                {
                    return;
                }
                Panel1.Controls.Clear();
                Panel1.Controls.Add(loadReport1Table());
            }
            else if (ddlView.SelectedValue == "2")
            {
                Table table;
                table = loadReport2Table();
                if (table == null)
                {
                    return;
                }
                Panel1.Controls.Clear();
                Panel1.Controls.Add(loadReport2Table());
            }
        }

        private Table loadReport1Table()
        {
            Table table = new Table();
            table.CssClass = "ps-table-1";

            {
                TableHeaderRow row = new TableHeaderRow();
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "สรุปข้อมูลรายชื่อผู้ที่กำลังขอเครื่องราชฯ ณ วันที่ " + DateTime.Today.ToLongDateString(); cell.ColumnSpan = 8; row.Cells.Add(cell); }
                table.Rows.Add(row);
            }

            {
                TableHeaderRow row = new TableHeaderRow();
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "มหาวิทยาลัยเทคโนโลยีราชมงคลตะวันออก"; cell.ColumnSpan = 8; row.Cells.Add(cell); }
                table.Rows.Add(row);
            }

            {
                TableHeaderRow row = new TableHeaderRow();
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "ลำดับที่"; cell.ColumnSpan = 1; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "รหัสการขอเครื่องราช"; cell.ColumnSpan = 1; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "ชื่อผู้ขอเครื่องราช"; cell.ColumnSpan = 1; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "วิทยาเขต"; cell.ColumnSpan = 1; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "คณะ"; cell.ColumnSpan = 1; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "ระดับชั้นเครื่องราชที่ขอ"; cell.ColumnSpan = 1; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "วันที่ข้อมูล"; cell.ColumnSpan = 1; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "สถานะ"; cell.ColumnSpan = 1; row.Cells.Add(cell); }
                
                table.Rows.Add(row);
            }

            using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
            {
                con.Open();

                string where = "";
                if (ddlCampus.SelectedValue != "")
                {
                    where += " AND (SELECT PS_CAMPUS_ID FROM PS_PERSON WHERE PS_CITIZEN_ID = CITIZEN_ID) = " + ddlCampus.SelectedValue + "";
                }
                if (ddlBudgetYear.SelectedValue != "") {
                    where += " AND EXTRACT(YEAR FROM GET_DATE) = " + ddlBudgetYear.SelectedValue + "";
                }

                using (OracleCommand com = new OracleCommand("SELECT IP_ID รหัสการขอเครื่องราช, (SELECT  PS_FIRSTNAME || ' ' || PS_LASTNAME FROM PS_PERSON WHERE PS_CITIZEN_ID = CITIZEN_ID) ชื่อผู้ขอเครื่องราช, (SELECT (SELECT CAMPUS_NAME FROM TB_CAMPUS WHERE TB_CAMPUS.CAMPUS_ID = PS_PERSON.PS_CAMPUS_ID) FROM PS_PERSON WHERE PS_PERSON.PS_CITIZEN_ID = TB_INSIG_PERSON.CITIZEN_ID) CAMPUS_NAME, (SELECT (SELECT FACULTY_NAME FROM TB_FACULTY WHERE TB_FACULTY.FACULTY_ID = PS_PERSON.PS_FACULTY_ID) FROM PS_PERSON WHERE PS_PERSON.PS_CITIZEN_ID = TB_INSIG_PERSON.CITIZEN_ID) FACULTY_NAME, (SELECT INSIG_GRADE_NAME_L FROM TB_INSIG_GRADE WHERE INSIG_GRADE_ID = INSIG_ID) ระดับชั้นเครื่องราชที่ขอ, REQ_DATE วันที่ข้อมูล, (SELECT IP_STATUS_NAME FROM TB_INSIG_PERSON_STATUS WHERE TB_INSIG_PERSON_STATUS.IP_STATUS_ID = TB_INSIG_PERSON.IP_STATUS_ID) สถานะ FROM TB_INSIG_PERSON WHERE IP_STATUS_ID = 1 " + where + " ORDER BY ABS(CITIZEN_ID) ASC, ABS(INSIG_ID) DESC", con))
                {
                    using (OracleDataReader reader = com.ExecuteReader())
                    {
                        int i = 0;
                        while (reader.Read())
                        {
                            TableRow row = new TableRow();
                            { TableCell cell = new TableCell(); cell.Text = "" + (++i); row.Cells.Add(cell); }
                            { TableCell cell = new TableCell(); cell.Text = reader.GetValue(0).ToString(); row.Cells.Add(cell); }
                            { TableCell cell = new TableCell(); cell.Text = reader.GetValue(1).ToString(); row.Cells.Add(cell); }
                            { TableCell cell = new TableCell(); cell.Text = reader.GetValue(2).ToString(); row.Cells.Add(cell); }
                            { TableCell cell = new TableCell(); cell.Text = reader.GetValue(3).ToString(); row.Cells.Add(cell); }
                            { TableCell cell = new TableCell(); cell.Text = reader.GetValue(4).ToString(); row.Cells.Add(cell); }
                            { TableCell cell = new TableCell(); cell.Text = reader.GetDateTime(5).ToLongDateString(); row.Cells.Add(cell); }
                            { TableCell cell = new TableCell(); cell.Text = reader.GetValue(6).ToString(); row.Cells.Add(cell); } 
                            table.Rows.Add(row);
                        }
                    }
                }
            }
            return table;
        }

        private Table loadReport2Table()
        {
            Table table = new Table();
            table.CssClass = "ps-table-1";

            {
                TableHeaderRow row = new TableHeaderRow();
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "สรุปข้อมูลรายชื่อผู้ที่ได้รับเครื่องราชฯ ณ วันที่ " + DateTime.Today.ToLongDateString(); cell.ColumnSpan = 9; row.Cells.Add(cell); }
                table.Rows.Add(row);
            }

            {
                TableHeaderRow row = new TableHeaderRow();
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "มหาวิทยาลัยเทคโนโลยีราชมงคลตะวันออก"; cell.ColumnSpan = 9; row.Cells.Add(cell); }
                table.Rows.Add(row);
            }

            {
                TableHeaderRow row = new TableHeaderRow();
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "ลำดับที่"; cell.ColumnSpan = 1; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "รหัสการขอเครื่องราช"; cell.ColumnSpan = 1; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "ชื่อผู้ขอเครื่องราช"; cell.ColumnSpan = 1; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "วิทยาเขต"; cell.ColumnSpan = 1; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "คณะ"; cell.ColumnSpan = 1; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "ระดับชั้นเครื่องราชที่ขอ"; cell.ColumnSpan = 1; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "วันที่ข้อมูล"; cell.ColumnSpan = 1; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "สถานะ"; cell.ColumnSpan = 1; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "วันที่ได้รับ"; cell.ColumnSpan = 1; row.Cells.Add(cell); }
                table.Rows.Add(row);
            }

            using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
            {
                con.Open();

                string where = "";
                if (ddlCampus.SelectedValue != "")
                {
                    where += " AND (SELECT PS_CAMPUS_ID FROM PS_PERSON WHERE PS_CITIZEN_ID = CITIZEN_ID) = " + ddlCampus.SelectedValue + "";
                }
                if (ddlBudgetYear.SelectedValue != "") {
                    where += " AND EXTRACT(YEAR FROM GET_DATE) = " + ddlBudgetYear.SelectedValue + "";
                }

                using (OracleCommand com = new OracleCommand("SELECT IP_ID รหัสการขอเครื่องราช, (SELECT  PS_FIRSTNAME || ' ' || PS_LASTNAME FROM PS_PERSON WHERE PS_CITIZEN_ID = CITIZEN_ID) ชื่อผู้ขอเครื่องราช, (SELECT (SELECT CAMPUS_NAME FROM TB_CAMPUS WHERE TB_CAMPUS.CAMPUS_ID = PS_PERSON.PS_CAMPUS_ID) FROM PS_PERSON WHERE PS_PERSON.PS_CITIZEN_ID = TB_INSIG_PERSON.CITIZEN_ID) CAMPUS_NAME, (SELECT (SELECT FACULTY_NAME FROM TB_FACULTY WHERE TB_FACULTY.FACULTY_ID = PS_PERSON.PS_FACULTY_ID) FROM PS_PERSON WHERE PS_PERSON.PS_CITIZEN_ID = TB_INSIG_PERSON.CITIZEN_ID) FACULTY_NAME, (SELECT INSIG_GRADE_NAME_L FROM TB_INSIG_GRADE WHERE INSIG_GRADE_ID = INSIG_ID) ระดับชั้นเครื่องราชที่ขอ, REQ_DATE วันที่ข้อมูล, (SELECT IP_STATUS_NAME FROM TB_INSIG_PERSON_STATUS WHERE TB_INSIG_PERSON_STATUS.IP_STATUS_ID = TB_INSIG_PERSON.IP_STATUS_ID) สถานะ, GET_DATE วันที่ได้รับ FROM TB_INSIG_PERSON WHERE IP_STATUS_ID IN(2,3) " + where + " ORDER BY ABS(CITIZEN_ID) ASC, ABS(INSIG_ID) DESC", con))
                {
                    using (OracleDataReader reader = com.ExecuteReader())
                    {
                        int i = 0;
                        while (reader.Read())
                        {
                            TableRow row = new TableRow();
                            { TableCell cell = new TableCell(); cell.Text = "" + (++i); row.Cells.Add(cell); }
                            { TableCell cell = new TableCell(); cell.Text = reader.GetValue(0).ToString(); row.Cells.Add(cell); }
                            { TableCell cell = new TableCell(); cell.Text = reader.GetValue(1).ToString(); row.Cells.Add(cell); }
                            { TableCell cell = new TableCell(); cell.Text = reader.GetValue(2).ToString(); row.Cells.Add(cell); }
                            { TableCell cell = new TableCell(); cell.Text = reader.GetValue(3).ToString(); row.Cells.Add(cell); }
                            { TableCell cell = new TableCell(); cell.Text = reader.GetValue(4).ToString(); row.Cells.Add(cell); }
                            { TableCell cell = new TableCell(); cell.Text = reader.GetDateTime(5).ToLongDateString(); row.Cells.Add(cell); }
                            { TableCell cell = new TableCell(); cell.Text = reader.GetValue(6).ToString(); row.Cells.Add(cell); }
                            { TableCell cell = new TableCell(); cell.Text = reader.GetDateTime(7).ToLongDateString(); row.Cells.Add(cell); }
                            table.Rows.Add(row);
                        }
                    }
                }
            }
            return table;
        }

        private Table loadReportSelf()
        {
            Table table = new Table();
            table.CssClass = "ps-table-1";

            {
                TableHeaderRow row = new TableHeaderRow();
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "สรุปข้อมูลการขอเครื่องราชฯของตนเอง ณ วันที่ " + DateTime.Today.ToLongDateString(); cell.ColumnSpan = 9; row.Cells.Add(cell); }
                table.Rows.Add(row);
            }

            {
                TableHeaderRow row = new TableHeaderRow();
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "มหาวิทยาลัยเทคโนโลยีราชมงคลตะวันออก"; cell.ColumnSpan = 9; row.Cells.Add(cell); }
                table.Rows.Add(row);
            }

            {
                TableHeaderRow row = new TableHeaderRow();
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "ลำดับที่"; cell.ColumnSpan = 1; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "รหัสการขอเครื่องราช"; cell.ColumnSpan = 1; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "ชื่อผู้ขอเครื่องราช"; cell.ColumnSpan = 1; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "วิทยาเขต"; cell.ColumnSpan = 1; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "คณะ"; cell.ColumnSpan = 1; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "ระดับชั้นเครื่องราชที่ขอ"; cell.ColumnSpan = 1; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "วันที่ข้อมูล"; cell.ColumnSpan = 1; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "สถานะ"; cell.ColumnSpan = 1; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "วันที่ได้รับ"; cell.ColumnSpan = 1; row.Cells.Add(cell); }
                table.Rows.Add(row);
            }

            using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
            {
                con.Open();

                string where = "";
                if (ddlCampus.SelectedValue != "")
                {
                    where += " AND (SELECT PS_CAMPUS_ID FROM PS_PERSON WHERE PS_CITIZEN_ID = CITIZEN_ID) = " + ddlCampus.SelectedValue + "";
                }

                using (OracleCommand com = new OracleCommand("SELECT IP_ID รหัสการขอเครื่องราช, (SELECT  PS_FIRSTNAME || ' ' || PS_LASTNAME FROM PS_PERSON WHERE PS_CITIZEN_ID = CITIZEN_ID) ชื่อผู้ขอเครื่องราช, (SELECT (SELECT CAMPUS_NAME FROM TB_CAMPUS WHERE TB_CAMPUS.CAMPUS_ID = PS_PERSON.PS_CAMPUS_ID) FROM PS_PERSON WHERE PS_PERSON.PS_CITIZEN_ID = TB_INSIG_PERSON.CITIZEN_ID) CAMPUS_NAME, (SELECT (SELECT FACULTY_NAME FROM TB_FACULTY WHERE TB_FACULTY.FACULTY_ID = PS_PERSON.PS_FACULTY_ID) FROM PS_PERSON WHERE PS_PERSON.PS_CITIZEN_ID = TB_INSIG_PERSON.CITIZEN_ID) FACULTY_NAME, (SELECT INSIG_GRADE_NAME_L FROM TB_INSIG_GRADE WHERE INSIG_GRADE_ID = INSIG_ID) ระดับชั้นเครื่องราชที่ขอ, REQ_DATE วันที่ข้อมูล, (SELECT IP_STATUS_NAME FROM TB_INSIG_PERSON_STATUS WHERE TB_INSIG_PERSON_STATUS.IP_STATUS_ID = TB_INSIG_PERSON.IP_STATUS_ID) สถานะ, GET_DATE วันที่ได้รับ FROM TB_INSIG_PERSON WHERE CITIZEN_ID = " + loginPerson.PS_CITIZEN_ID + " ORDER BY ABS(CITIZEN_ID) ASC, ABS(INSIG_ID) DESC", con))
                {
                    using (OracleDataReader reader = com.ExecuteReader())
                    {
                        int i = 0;
                        while (reader.Read())
                        {
                            TableRow row = new TableRow();
                            { TableCell cell = new TableCell(); cell.Text = "" + (++i); row.Cells.Add(cell); }
                            { TableCell cell = new TableCell(); cell.Text = reader.GetValue(0).ToString(); row.Cells.Add(cell); }
                            { TableCell cell = new TableCell(); cell.Text = reader.GetValue(1).ToString(); row.Cells.Add(cell); }
                            { TableCell cell = new TableCell(); cell.Text = reader.GetValue(2).ToString(); row.Cells.Add(cell); }
                            { TableCell cell = new TableCell(); cell.Text = reader.GetValue(3).ToString(); row.Cells.Add(cell); }
                            { TableCell cell = new TableCell(); cell.Text = reader.GetValue(4).ToString(); row.Cells.Add(cell); }
                            { TableCell cell = new TableCell(); cell.Text = reader.GetDateTime(5).ToLongDateString(); row.Cells.Add(cell); }
                            { TableCell cell = new TableCell(); cell.Text = reader.GetValue(6).ToString(); row.Cells.Add(cell); }
                            { TableCell cell = new TableCell(); cell.Text = reader.GetDateTime(7).ToLongDateString(); row.Cells.Add(cell); }
                            table.Rows.Add(row);
                        }
                    }
                }
            }
            return table;
        }

        protected void lbuV1Export_Click(object sender, EventArgs e)
        {
            if (ddlView.SelectedValue == "1")
            {
                Table table = loadReport1Table();
                if (table == null)
                {
                    return;
                }
                Panel1.Controls.Clear();
                Panel1.Controls.Add(loadReport1Table());

                Response.ContentType = "application/x-msexcel";
                Response.AddHeader("Content-Disposition", "attachment;filename=สรุปข้อมูล'" + ddlView.SelectedItem.Text + "' ณ วันที่ " + DateTime.Today.ToLongDateString() + ".xls");
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
            else if (ddlView.SelectedValue == "2")
            {
                Table table = loadReport2Table();
                if (table == null)
                {
                    return;
                }
                Panel1.Controls.Clear();
                Panel1.Controls.Add(loadReport2Table());

                Response.ContentType = "application/x-msexcel";
                Response.AddHeader("Content-Disposition", "attachment;filename=สรุปข้อมูล'" + ddlView.SelectedItem.Text + "' ณ วันที่ " + DateTime.Today.ToLongDateString() + ".xls");
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

            if (loginPerson.PERSON_ROLE_ID != "4")
            {
                Table table = loadReportSelf();
                if (table == null)
                {
                    return;
                }
                Panel2.Controls.Clear();
                Panel2.Controls.Add(loadReport1Table());

                Response.ContentType = "application/x-msexcel";
                Response.AddHeader("Content-Disposition", "attachment;filename=สรุปข้อมูลการขอเครื่องราชฯของตนเอง ณ วันที่ " + DateTime.Today.ToLongDateString() + ".xls");
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

        protected void lbuV2Export_Click(object sender, EventArgs e) {
            if (loginPerson.PERSON_ROLE_ID != "4") {
                Table table = loadReportSelf();
                if (table == null) {
                    return;
                }
                Panel2.Controls.Clear();
                Panel2.Controls.Add(loadReport1Table());

                Response.ContentType = "application/x-msexcel";
                Response.AddHeader("Content-Disposition", "attachment;filename=สรุปข้อมูลการขอเครื่องราชฯของตนเอง ณ วันที่ " + DateTime.Today.ToLongDateString() + ".xls");
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
}