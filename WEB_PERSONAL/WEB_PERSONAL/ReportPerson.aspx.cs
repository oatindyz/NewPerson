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
            if (!IsPostBack)
            {
                ddlView.Items.Add(new ListItem("แสดงจำนวนบุคลารสายวิชาการ จำแนกตามประเภทบุคลากร คณะ และตำแหน่งทางวิชาการ", "1"));
                ddlView.Items.Add(new ListItem("แสดงจำนวนบุคลารสายวิชาการ จำแนกตามประเภทบุคลากร คณะ และวุฒิการศึกษา", "2"));
                DatabaseManager.BindDropDown(ddlCampus, "SELECT * FROM TB_CAMPUS ORDER BY CAMPUS_ID", "CAMPUS_NAME", "CAMPUS_ID", "--กรุณาเลือก--");

                int minDateInwork = DatabaseManager.ExecuteInt("SELECT EXTRACT(YEAR FROM PS_INWORK_DATE)+543 FROM PS_PERSON WHERE PS_INWORK_DATE = (SELECT MIN(PS_INWORK_DATE) FROM PS_PERSON)");
                int currentYear = Util.BudgetYear();
                for (int i = minDateInwork; i <= currentYear; ++i)
                {
                    ddlYear.Items.Add(new System.Web.UI.WebControls.ListItem("" + i, "" + i));
                }
            }
        }

        private Table Bindจำนวนบุคลารสายวิชาการจำแนกตามประเภทบุคลากรคณะและตำแหน่งทางวิชาการ()
        {
            Table table = new Table();
            table.CssClass = "ps-table-1";

            {
                TableHeaderRow row = new TableHeaderRow();
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "สรุปข้อมูลจำนวนบุคลากรสายวิชาการ จำแนกตามประเภทบุคลากร คณะ และตำแหน่งทางวิชาการ (ข้อมูล ณ วันที่ " + DateTime.Today.ToLongDateString() + ")"; cell.Style["text-align"] = "center"; cell.ColumnSpan = 26; row.Cells.Add(cell); }
                table.Rows.Add(row);
            }

            {
                TableHeaderRow row = new TableHeaderRow();
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "คณะ"; cell.RowSpan = 2; cell.Style["text-align"] = "center"; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "ข้าราชการพลเรือน"; cell.ColumnSpan = 5; cell.Style["text-align"] = "center"; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "พนักงานในสถาบันฯ"; cell.ColumnSpan = 5; cell.Style["text-align"] = "center"; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "พนักงานราชการ"; cell.ColumnSpan = 5; cell.Style["text-align"] = "center"; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "ลูกจ้างชั่วคราว"; cell.ColumnSpan = 5; cell.Style["text-align"] = "center"; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "รวมทั้งสิ้น"; cell.ColumnSpan = 5; cell.Style["text-align"] = "center"; row.Cells.Add(cell); }
                table.Rows.Add(row);
            }

            {
                TableHeaderRow row = new TableHeaderRow();
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "ศ."; cell.Style["text-align"] = "center"; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "รศ."; cell.Style["text-align"] = "center"; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "ผศ."; cell.Style["text-align"] = "center"; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "อ."; cell.Style["text-align"] = "center"; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "รวม"; cell.Style["text-align"] = "center"; row.Cells.Add(cell); }

                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "ศ."; cell.Style["text-align"] = "center"; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "รศ."; cell.Style["text-align"] = "center"; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "ผศ."; cell.Style["text-align"] = "center"; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "อ."; cell.Style["text-align"] = "center"; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "รวม"; cell.Style["text-align"] = "center"; row.Cells.Add(cell); }

                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "ศ."; cell.Style["text-align"] = "center"; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "รศ."; cell.Style["text-align"] = "center"; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "ผศ."; cell.Style["text-align"] = "center"; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "อ."; cell.Style["text-align"] = "center"; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "รวม"; cell.Style["text-align"] = "center"; row.Cells.Add(cell); }

                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "ศ."; cell.Style["text-align"] = "center"; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "รศ."; cell.Style["text-align"] = "center"; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "ผศ."; cell.Style["text-align"] = "center"; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "อ."; cell.Style["text-align"] = "center"; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "รวม"; cell.Style["text-align"] = "center"; row.Cells.Add(cell); }

                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "ศ."; cell.Style["text-align"] = "center"; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "รศ."; cell.Style["text-align"] = "center"; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "ผศ."; cell.Style["text-align"] = "center"; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "อ."; cell.Style["text-align"] = "center"; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "รวม"; cell.Style["text-align"] = "center"; row.Cells.Add(cell); }
                table.Rows.Add(row);
            }

            //0วิทยาเขตบางพระ
            if (ddlCampus.SelectedValue == "1")
            {
                {
                    TableHeaderRow row = new TableHeaderRow();
                    { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "วิทยาเขตบางพระ"; cell.Style["text-align"] = "left"; cell.Width = 250; row.Cells.Add(cell); }
                    using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
                    {
                        con.Open();
                        using (OracleCommand com = new OracleCommand(
                        "SELECT" +
                        " (SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        " FROM DUAL", con))
                        {
                            using (OracleDataReader reader = com.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    //บางพระข้าราชการ
                                    if (reader.GetInt32(0) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(0); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(1) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(1); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(2) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(2); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(3) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(3); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(4) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(4); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //บางพระพนักงานในสถาบัน
                                    if (reader.GetInt32(5) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(5); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(6) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(6); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(7) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(7); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(8) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(8); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(9) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(9); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //บางพระพนักงานราชการ
                                    if (reader.GetInt32(10) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(10); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(11) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(11); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(12) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(12); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(13) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(13); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(14) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(14); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //บางพระลูกจ้างชั่วคราว
                                    if (reader.GetInt32(15) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(15); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(16) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(16); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(17) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(17); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(18) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(18); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(19) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(19); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //บางพระรวมทั้งสิ้น
                                    if (reader.GetInt32(20) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(20); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(21) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(21); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(22) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(22); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(23) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(23); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(24) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(24); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                }
                            }
                        }
                        table.Rows.Add(row);
                    }
                }
                //1คณะเกษตรศาสตร์และทรัพยากรธรรมชาติ
                {
                    TableHeaderRow row = new TableHeaderRow();
                    { TableCell cell = new TableCell(); cell.Text = "คณะเกษตรศาสตร์และทรัพยากรธรรมชาติ"; cell.Style["text-align"] = "left"; cell.Width = 250; row.Cells.Add(cell); }
                    using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
                    {
                        con.Open();
                        using (OracleCommand com = new OracleCommand(
                        "SELECT" +
                        " (SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        " FROM DUAL", con))
                        {
                            using (OracleDataReader reader = com.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    //บางพระคณะเกษตรศาสตร์และทรัพยากรธรรมชาติข้าราชการ
                                    if (reader.GetInt32(0) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(0); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(1) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(1); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(2) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(2); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(3) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(3); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(4) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(4); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //บางพระคณะเกษตรศาสตร์และทรัพยากรธรรมชาติพนักงานในสถาบัน
                                    if (reader.GetInt32(5) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(5); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(6) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(6); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(7) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(7); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(8) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(8); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(9) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(9); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //บางพระคณะเกษตรศาสตร์และทรัพยากรธรรมชาติพนักงานราชการ
                                    if (reader.GetInt32(10) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(10); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(11) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(11); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(12) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(12); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(13) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(13); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(14) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(14); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //บางพระคณะเกษตรศาสตร์และทรัพยากรธรรมชาติลูกจ้างชั่วคราว
                                    if (reader.GetInt32(15) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(15); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(16) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(16); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(17) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(17); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(18) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(18); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(19) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(19); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //บางพระคณะเกษตรศาสตร์และทรัพยากรธรรมชาติรวมทั้งสิ้น
                                    if (reader.GetInt32(20) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(20); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(21) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(21); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(22) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(22); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(23) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(23); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(24) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(24); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                }
                            }
                        }
                        table.Rows.Add(row);
                    }
                }
                //2คณะวิทยาศาสตร์และเทคโนโลยี
                {
                    TableHeaderRow row = new TableHeaderRow();
                    { TableCell cell = new TableCell(); cell.Text = "คณะวิทยาศาสตร์และเทคโนโลยี"; cell.Style["text-align"] = "left"; cell.Width = 250; row.Cells.Add(cell); }
                    using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
                    {
                        con.Open();
                        using (OracleCommand com = new OracleCommand(
                        "SELECT" +
                        " (SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        " FROM DUAL", con))
                        {
                            using (OracleDataReader reader = com.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    //บางพระคณะวิทยาศาสตร์และเทคโนโลยีข้าราชการ
                                    if (reader.GetInt32(0) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(0); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(1) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(1); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(2) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(2); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(3) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(3); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(4) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(4); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //บางพระคณะวิทยาศาสตร์และเทคโนโลยีพนักงานในสถาบัน
                                    if (reader.GetInt32(5) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(5); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(6) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(6); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(7) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(7); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(8) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(8); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(9) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(9); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //บางพระคณะวิทยาศาสตร์และเทคโนโลยีพนักงานราชการ
                                    if (reader.GetInt32(10) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(10); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(11) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(11); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(12) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(12); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(13) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(13); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(14) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(14); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //บางพระคณะวิทยาศาสตร์และเทคโนโลยีลูกจ้างชั่วคราว
                                    if (reader.GetInt32(15) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(15); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(16) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(16); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(17) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(17); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(18) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(18); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(19) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(19); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //บางพระคณะวิทยาศาสตร์และเทคโนโลยีรวมทั้งสิ้น
                                    if (reader.GetInt32(20) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(20); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(21) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(21); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(22) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(22); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(23) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(23); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(24) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(24); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                }
                            }
                        }
                        table.Rows.Add(row);
                    }
                }
                //3คณะมนุษยศาสตร์และสังคมศาสตร์
                {
                    TableHeaderRow row = new TableHeaderRow();
                    { TableCell cell = new TableCell(); cell.Text = "คณะมนุษยศาสตร์และสังคมศาสตร์"; cell.Style["text-align"] = "left"; cell.Width = 250; row.Cells.Add(cell); }
                    using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
                    {
                        con.Open();
                        using (OracleCommand com = new OracleCommand(
                        "SELECT" +
                        " (SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        " FROM DUAL", con))
                        {
                            using (OracleDataReader reader = com.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    //บางพระคณะมนุษยศาสตร์และสังคมศาสตร์ข้าราชการ
                                    if (reader.GetInt32(0) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(0); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(1) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(1); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(2) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(2); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(3) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(3); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(4) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(4); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //บางพระคณะมนุษยศาสตร์และสังคมศาสตร์พนักงานในสถาบัน
                                    if (reader.GetInt32(5) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(5); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(6) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(6); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(7) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(7); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(8) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(8); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(9) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(9); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //บางพระคณะมนุษยศาสตร์และสังคมศาสตร์พนักงานราชการ
                                    if (reader.GetInt32(10) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(10); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(11) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(11); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(12) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(12); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(13) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(13); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(14) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(14); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //บางพระคณะมนุษยศาสตร์และสังคมศาสตร์ลูกจ้างชั่วคราว
                                    if (reader.GetInt32(15) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(15); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(16) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(16); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(17) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(17); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(18) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(18); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(19) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(19); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //บางพระคณะมนุษยศาสตร์และสังคมศาสตร์รวมทั้งสิ้น
                                    if (reader.GetInt32(20) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(20); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(21) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(21); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(22) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(22); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(23) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(23); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(24) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(24); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                }
                            }
                        }
                        table.Rows.Add(row);
                    }
                }
                //4คณะสัตวแพทยศาสตร์
                {
                    TableHeaderRow row = new TableHeaderRow();
                    { TableCell cell = new TableCell(); cell.Text = "คณะสัตวแพทยศาสตร์"; cell.Style["text-align"] = "left"; cell.Width = 250; row.Cells.Add(cell); }
                    using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
                    {
                        con.Open();
                        using (OracleCommand com = new OracleCommand(
                        "SELECT" +
                        " (SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        " FROM DUAL", con))
                        {
                            using (OracleDataReader reader = com.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    //บางพระคณะสัตวแพทยศาสตร์ข้าราชการ
                                    if (reader.GetInt32(0) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(0); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(1) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(1); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(2) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(2); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(3) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(3); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(4) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(4); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //บางพระคณะสัตวแพทยศาสตร์พนักงานในสถาบัน
                                    if (reader.GetInt32(5) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(5); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(6) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(6); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(7) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(7); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(8) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(8); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(9) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(9); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //บางพระคณะสัตวแพทยศาสตร์พนักงานราชการ
                                    if (reader.GetInt32(10) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(10); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(11) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(11); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(12) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(12); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(13) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(13); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(14) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(14); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //บางพระคณะสัตวแพทยศาสตร์ลูกจ้างชั่วคราว
                                    if (reader.GetInt32(15) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(15); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(16) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(16); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(17) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(17); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(18) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(18); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(19) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(19); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //บางพระคณะสัตวแพทยศาสตร์รวมทั้งสิ้น
                                    if (reader.GetInt32(20) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(20); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(21) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(21); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(22) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(22); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(23) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(23); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(24) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(24); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                }
                            }
                        }
                        table.Rows.Add(row);
                    }
                }
                //5สถาบันเทคโนโลยีการบิน
                {
                    TableHeaderRow row = new TableHeaderRow();
                    { TableCell cell = new TableCell(); cell.Text = "สถาบันเทคโนโลยีการบิน"; cell.Style["text-align"] = "left"; cell.Width = 250; row.Cells.Add(cell); }
                    using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
                    {
                        con.Open();
                        using (OracleCommand com = new OracleCommand(
                        "SELECT" +
                        " (SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        " FROM DUAL", con))
                        {
                            using (OracleDataReader reader = com.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    //บางพระสถาบันเทคโนโลยีการบินข้าราชการ
                                    if (reader.GetInt32(0) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(0); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(1) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(1); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(2) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(2); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(3) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(3); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(4) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(4); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //บางพระสถาบันเทคโนโลยีการบินพนักงานในสถาบัน
                                    if (reader.GetInt32(5) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(5); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(6) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(6); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(7) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(7); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(8) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(8); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(9) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(9); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //บางพระสถาบันเทคโนโลยีการบินพนักงานราชการ
                                    if (reader.GetInt32(10) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(10); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(11) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(11); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(12) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(12); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(13) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(13); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(14) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(14); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //บางพระสถาบันเทคโนโลยีการบินลูกจ้างชั่วคราว
                                    if (reader.GetInt32(15) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(15); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(16) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(16); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(17) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(17); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(18) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(18); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(19) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(19); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //บางพระสถาบันเทคโนโลยีการบินรวมทั้งสิ้น
                                    if (reader.GetInt32(20) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(20); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(21) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(21); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(22) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(22); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(23) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(23); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(24) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(24); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                }
                            }
                        }
                        table.Rows.Add(row);
                    }
                }
                Panel1.Controls.Clear();
                Panel1.Controls.Add(table);
                return table;
            }
            if (ddlCampus.SelectedValue == "2")
            {
                //6วิทยาเขตจันทบุรี
                {
                    TableHeaderRow row = new TableHeaderRow();
                    { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "วิทยาเขตจันทบุรี"; cell.Style["text-align"] = "left"; cell.Width = 250; row.Cells.Add(cell); }
                    using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
                    {
                        con.Open();
                        using (OracleCommand com = new OracleCommand(
                        "SELECT" +
                        " (SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        " FROM DUAL", con))
                        {
                            using (OracleDataReader reader = com.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    //จันทบุรีข้าราชการ
                                    if (reader.GetInt32(0) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(0); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(1) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(1); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(2) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(2); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(3) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(3); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(4) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(4); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //จันทบุรีพนักงานในสถาบัน
                                    if (reader.GetInt32(5) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(5); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(6) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(6); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(7) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(7); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(8) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(8); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(9) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(9); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //จันทบุรีพนักงานราชการ
                                    if (reader.GetInt32(10) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(10); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(11) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(11); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(12) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(12); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(13) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(13); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(14) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(14); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //จันทบุรีลูกจ้างชั่วคราว
                                    if (reader.GetInt32(15) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(15); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(16) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(16); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(17) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(17); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(18) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(18); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(19) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(19); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //จันทบุรีรวมทั้งสิ้น
                                    if (reader.GetInt32(20) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(20); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(21) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(21); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(22) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(22); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(23) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(23); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(24) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(24); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                }
                            }
                        }
                        table.Rows.Add(row);
                    }
                }
                //7คณะเทคโนโลยีอุตสาหกรรมการเกษตร
                {
                    TableHeaderRow row = new TableHeaderRow();
                    { TableCell cell = new TableCell(); cell.Text = "คณะเทคโนโลยีอุตสาหกรรมการเกษตร"; cell.Style["text-align"] = "left"; cell.Width = 250; row.Cells.Add(cell); }
                    using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
                    {
                        con.Open();
                        using (OracleCommand com = new OracleCommand(
                        "SELECT" +
                        " (SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        " FROM DUAL", con))
                        {
                            using (OracleDataReader reader = com.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    //จันทบุรีคณะเทคโนโลยีอุตสาหกรรมการเกษตรข้าราชการ
                                    if (reader.GetInt32(0) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(0); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(1) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(1); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(2) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(2); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(3) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(3); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(4) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(4); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //จันทบุรีคณะเทคโนโลยีอุตสาหกรรมการเกษตรพนักงานในสถาบัน
                                    if (reader.GetInt32(5) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(5); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(6) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(6); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(7) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(7); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(8) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(8); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(9) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(9); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //จันทบุรีคณะเทคโนโลยีอุตสาหกรรมการเกษตรพนักงานราชการ
                                    if (reader.GetInt32(10) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(10); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(11) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(11); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(12) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(12); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(13) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(13); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(14) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(14); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //จันทบุรีคณะเทคโนโลยีอุตสาหกรรมการเกษตรลูกจ้างชั่วคราว
                                    if (reader.GetInt32(15) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(15); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(16) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(16); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(17) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(17); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(18) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(18); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(19) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(19); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //จันทบุรีคณะเทคโนโลยีอุตสาหกรรมการเกษตรรวมทั้งสิ้น
                                    if (reader.GetInt32(20) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(20); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(21) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(21); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(22) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(22); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(23) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(23); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(24) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(24); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                }
                            }
                        }
                        table.Rows.Add(row);
                    }
                }
                //8คณะเทคโนโลยีสังคม
                {
                    TableHeaderRow row = new TableHeaderRow();
                    { TableCell cell = new TableCell(); cell.Text = "คณะเทคโนโลยีสังคม"; cell.Style["text-align"] = "left"; cell.Width = 250; row.Cells.Add(cell); }
                    using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
                    {
                        con.Open();
                        using (OracleCommand com = new OracleCommand(
                        "SELECT" +
                        " (SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        " FROM DUAL", con))
                        {
                            using (OracleDataReader reader = com.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    //จันทบุรีคณะเทคโนโลยีสังคมข้าราชการ
                                    if (reader.GetInt32(0) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(0); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(1) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(1); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(2) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(2); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(3) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(3); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(4) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(4); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //จันทบุรีคณะเทคโนโลยีสังคมพนักงานในสถาบัน
                                    if (reader.GetInt32(5) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(5); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(6) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(6); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(7) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(7); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(8) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(8); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(9) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(9); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //จันทบุรีคณะเทคโนโลยีสังคมพนักงานราชการ
                                    if (reader.GetInt32(10) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(10); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(11) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(11); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(12) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(12); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(13) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(13); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(14) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(14); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //จันทบุรีคณะเทคโนโลยีสังคมลูกจ้างชั่วคราว
                                    if (reader.GetInt32(15) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(15); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(16) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(16); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(17) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(17); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(18) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(18); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(19) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(19); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //จันทบุรีคณะเทคโนโลยีสังคมรวมทั้งสิ้น
                                    if (reader.GetInt32(20) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(20); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(21) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(21); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(22) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(22); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(23) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(23); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(24) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(24); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                }
                            }
                        }
                        table.Rows.Add(row);
                    }
                }
                Panel1.Controls.Clear();
                Panel1.Controls.Add(table);
                return table;
            }
            if (ddlCampus.SelectedValue == "3")
            {
                //9วิทยาเขตจักรพงษภูวนารถ
                {
                    TableHeaderRow row = new TableHeaderRow();
                    { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "วิทยาเขตจักรพงษภูวนารถ"; cell.Style["text-align"] = "left"; cell.Width = 250; row.Cells.Add(cell); }
                    using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
                    {
                        con.Open();
                        using (OracleCommand com = new OracleCommand(
                        "SELECT" +
                        " (SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        " FROM DUAL", con))
                        {
                            using (OracleDataReader reader = com.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    //จักรพงษภูวนารถข้าราชการ
                                    if (reader.GetInt32(0) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(0); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(1) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(1); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(2) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(2); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(3) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(3); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(4) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(4); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //จักรพงษภูวนารถพนักงานในสถาบัน
                                    if (reader.GetInt32(5) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(5); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(6) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(6); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(7) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(7); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(8) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(8); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(9) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(9); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //จักรพงษภูวนารถพนักงานราชการ
                                    if (reader.GetInt32(10) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(10); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(11) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(11); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(12) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(12); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(13) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(13); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(14) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(14); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //จักรพงษภูวนารถลูกจ้างชั่วคราว
                                    if (reader.GetInt32(15) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(15); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(16) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(16); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(17) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(17); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(18) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(18); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(19) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(19); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //จักรพงษภูวนารถรวมทั้งสิ้น
                                    if (reader.GetInt32(20) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(20); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(21) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(21); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(22) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(22); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(23) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(23); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(24) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(24); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                }
                            }
                        }
                        table.Rows.Add(row);
                    }
                }
                //10คณะบริหารธุรกิจและเทคโนโลยีสารสนเทศ
                {
                    TableHeaderRow row = new TableHeaderRow();
                    { TableCell cell = new TableCell(); cell.Text = "คณะบริหารธุรกิจและเทคโนโลยีสารสนเทศ"; cell.Style["text-align"] = "left"; cell.Width = 250; row.Cells.Add(cell); }
                    using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
                    {
                        con.Open();
                        using (OracleCommand com = new OracleCommand(
                        "SELECT" +
                        " (SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        " FROM DUAL", con))
                        {
                            using (OracleDataReader reader = com.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    //จักรพงษภูวนารถคณะบริหารธุรกิจและเทคโนโลยีสารสนเทศข้าราชการ
                                    if (reader.GetInt32(0) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(0); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(1) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(1); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(2) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(2); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(3) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(3); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(4) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(4); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //จักรพงษภูวนารถคณะบริหารธุรกิจและเทคโนโลยีสารสนเทศพนักงานในสถาบัน
                                    if (reader.GetInt32(5) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(5); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(6) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(6); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(7) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(7); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(8) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(8); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(9) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(9); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //จักรพงษภูวนารถคณะบริหารธุรกิจและเทคโนโลยีสารสนเทศพนักงานราชการ
                                    if (reader.GetInt32(10) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(10); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(11) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(11); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(12) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(12); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(13) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(13); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(14) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(14); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //จักรพงษภูวนารถคณะบริหารธุรกิจและเทคโนโลยีสารสนเทศลูกจ้างชั่วคราว
                                    if (reader.GetInt32(15) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(15); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(16) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(16); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(17) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(17); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(18) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(18); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(19) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(19); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //จักรพงษภูวนารถคณะบริหารธุรกิจและเทคโนโลยีสารสนเทศรวมทั้งสิ้น
                                    if (reader.GetInt32(20) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(20); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(21) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(21); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(22) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(22); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(23) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(23); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(24) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(24); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                }
                            }
                        }
                        table.Rows.Add(row);
                    }
                }
                //11คณะศิลปศาสตร์
                {
                    TableHeaderRow row = new TableHeaderRow();
                    { TableCell cell = new TableCell(); cell.Text = "คณะศิลปศาสตร์"; cell.Style["text-align"] = "left"; cell.Width = 250; row.Cells.Add(cell); }
                    using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
                    {
                        con.Open();
                        using (OracleCommand com = new OracleCommand(
                        "SELECT" +
                        " (SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        " FROM DUAL", con))
                        {
                            using (OracleDataReader reader = com.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    //จักรพงษภูวนารถคณะศิลปศาสตร์ข้าราชการ
                                    if (reader.GetInt32(0) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(0); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(1) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(1); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(2) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(2); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(3) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(3); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(4) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(4); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //จักรพงษภูวนารถคณะศิลปศาสตร์พนักงานในสถาบัน
                                    if (reader.GetInt32(5) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(5); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(6) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(6); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(7) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(7); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(8) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(8); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(9) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(9); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //จักรพงษภูวนารถคณะศิลปศาสตร์พนักงานราชการ
                                    if (reader.GetInt32(10) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(10); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(11) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(11); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(12) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(12); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(13) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(13); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(14) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(14); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //จักรพงษภูวนารถคณะศิลปศาสตร์ลูกจ้างชั่วคราว
                                    if (reader.GetInt32(15) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(15); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(16) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(16); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(17) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(17); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(18) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(18); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(19) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(19); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //จักรพงษภูวนารถคณะศิลปศาสตร์รวมทั้งสิ้น
                                    if (reader.GetInt32(20) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(20); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(21) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(21); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(22) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(22); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(23) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(23); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(24) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(24); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                }
                            }
                        }
                        table.Rows.Add(row);
                    }
                }
                Panel1.Controls.Clear();
                Panel1.Controls.Add(table);
                return table;
            }
            if (ddlCampus.SelectedValue == "4")
            {
                //12วิทยาเขตอุเทนถวาย
                {
                    TableHeaderRow row = new TableHeaderRow();
                    { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "วิทยาเขตอุเทนถวาย"; cell.Style["text-align"] = "left"; cell.Width = 250; row.Cells.Add(cell); }
                    using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
                    {
                        con.Open();
                        using (OracleCommand com = new OracleCommand(
                        "SELECT" +
                        " (SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        " FROM DUAL", con))
                        {
                            using (OracleDataReader reader = com.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    //วิทยาเขตอุเทนถวายข้าราชการ
                                    if (reader.GetInt32(0) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(0); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(1) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(1); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(2) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(2); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(3) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(3); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(4) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(4); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //วิทยาเขตอุเทนถวายพนักงานในสถาบัน
                                    if (reader.GetInt32(5) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(5); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(6) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(6); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(7) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(7); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(8) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(8); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(9) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(9); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //วิทยาเขตอุเทนถวายพนักงานราชการ
                                    if (reader.GetInt32(10) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(10); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(11) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(11); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(12) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(12); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(13) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(13); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(14) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(14); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //วิทยาเขตอุเทนถวายลูกจ้างชั่วคราว
                                    if (reader.GetInt32(15) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(15); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(16) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(16); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(17) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(17); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(18) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(18); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(19) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(19); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //วิทยาเขตอุเทนถวายรวมทั้งสิ้น
                                    if (reader.GetInt32(20) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(20); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(21) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(21); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(22) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(22); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(23) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(23); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(24) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(24); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                }
                            }
                        }
                        table.Rows.Add(row);
                    }
                }
                //13คณะวิศวกรรมศาสตร์และสถาปัตยกรรมศาสตร์
                {
                    TableHeaderRow row = new TableHeaderRow();
                    { TableCell cell = new TableCell(); cell.Text = "คณะวิศวกรรมศาสตร์และสถาปัตยกรรมศาสตร์"; cell.Style["text-align"] = "left"; cell.Width = 250; row.Cells.Add(cell); }
                    using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
                    {
                        con.Open();
                        using (OracleCommand com = new OracleCommand(
                        "SELECT" +
                        " (SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        " FROM DUAL", con))
                        {
                            using (OracleDataReader reader = com.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    //อุเทนถวายคณะวิศวกรรมศาสตร์และสถาปัตยกรรมศาสตร์ข้าราชการ
                                    if (reader.GetInt32(0) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(0); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(1) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(1); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(2) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(2); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(3) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(3); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(4) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(4); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //อุเทนถวายคณะวิศวกรรมศาสตร์และสถาปัตยกรรมศาสตร์พนักงานในสถาบัน
                                    if (reader.GetInt32(5) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(5); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(6) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(6); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(7) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(7); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(8) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(8); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(9) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(9); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //อุเทนถวายคณะวิศวกรรมศาสตร์และสถาปัตยกรรมศาสตร์พนักงานราชการ
                                    if (reader.GetInt32(10) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(10); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(11) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(11); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(12) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(12); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(13) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(13); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(14) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(14); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //อุเทนถวายคณะวิศวกรรมศาสตร์และสถาปัตยกรรมศาสตร์ลูกจ้างชั่วคราว
                                    if (reader.GetInt32(15) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(15); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(16) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(16); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(17) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(17); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(18) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(18); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(19) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(19); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //อุเทนถวายคณะวิศวกรรมศาสตร์และสถาปัตยกรรมศาสตร์รวมทั้งสิ้น
                                    if (reader.GetInt32(20) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(20); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(21) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(21); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(22) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(22); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(23) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(23); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(24) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(24); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                }
                            }
                        }
                        table.Rows.Add(row);
                    }
                }
                Panel1.Controls.Clear();
                Panel1.Controls.Add(table);
                return table;
            }
            else
            {
                //0วิทยาเขตบางพระ

                {
                    TableHeaderRow row = new TableHeaderRow();
                    { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "วิทยาเขตบางพระ"; cell.Style["text-align"] = "left"; cell.Width = 250; row.Cells.Add(cell); }
                    using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
                    {
                        con.Open();
                        using (OracleCommand com = new OracleCommand(
                        "SELECT" +
                        " (SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        " FROM DUAL", con))
                        {
                            using (OracleDataReader reader = com.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    //บางพระข้าราชการ
                                    if (reader.GetInt32(0) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(0); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(1) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(1); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(2) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(2); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(3) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(3); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(4) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(4); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //บางพระพนักงานในสถาบัน
                                    if (reader.GetInt32(5) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(5); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(6) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(6); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(7) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(7); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(8) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(8); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(9) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(9); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //บางพระพนักงานราชการ
                                    if (reader.GetInt32(10) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(10); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(11) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(11); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(12) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(12); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(13) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(13); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(14) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(14); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //บางพระลูกจ้างชั่วคราว
                                    if (reader.GetInt32(15) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(15); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(16) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(16); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(17) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(17); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(18) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(18); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(19) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(19); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //บางพระรวมทั้งสิ้น
                                    if (reader.GetInt32(20) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(20); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(21) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(21); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(22) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(22); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(23) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(23); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(24) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(24); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                }
                            }
                        }
                        table.Rows.Add(row);
                    }
                }
                //1คณะเกษตรศาสตร์และทรัพยากรธรรมชาติ
                {
                    TableHeaderRow row = new TableHeaderRow();
                    { TableCell cell = new TableCell(); cell.Text = "คณะเกษตรศาสตร์และทรัพยากรธรรมชาติ"; cell.Style["text-align"] = "left"; cell.Width = 250; row.Cells.Add(cell); }
                    using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
                    {
                        con.Open();
                        using (OracleCommand com = new OracleCommand(
                        "SELECT" +
                        " (SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        " FROM DUAL", con))
                        {
                            using (OracleDataReader reader = com.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    //บางพระคณะเกษตรศาสตร์และทรัพยากรธรรมชาติข้าราชการ
                                    if (reader.GetInt32(0) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(0); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(1) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(1); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(2) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(2); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(3) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(3); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(4) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(4); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //บางพระคณะเกษตรศาสตร์และทรัพยากรธรรมชาติพนักงานในสถาบัน
                                    if (reader.GetInt32(5) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(5); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(6) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(6); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(7) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(7); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(8) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(8); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(9) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(9); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //บางพระคณะเกษตรศาสตร์และทรัพยากรธรรมชาติพนักงานราชการ
                                    if (reader.GetInt32(10) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(10); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(11) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(11); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(12) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(12); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(13) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(13); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(14) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(14); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //บางพระคณะเกษตรศาสตร์และทรัพยากรธรรมชาติลูกจ้างชั่วคราว
                                    if (reader.GetInt32(15) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(15); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(16) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(16); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(17) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(17); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(18) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(18); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(19) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(19); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //บางพระคณะเกษตรศาสตร์และทรัพยากรธรรมชาติรวมทั้งสิ้น
                                    if (reader.GetInt32(20) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(20); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(21) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(21); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(22) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(22); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(23) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(23); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(24) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(24); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                }
                            }
                        }
                        table.Rows.Add(row);
                    }
                }
                //2คณะวิทยาศาสตร์และเทคโนโลยี
                {
                    TableHeaderRow row = new TableHeaderRow();
                    { TableCell cell = new TableCell(); cell.Text = "คณะวิทยาศาสตร์และเทคโนโลยี"; cell.Style["text-align"] = "left"; cell.Width = 250; row.Cells.Add(cell); }
                    using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
                    {
                        con.Open();
                        using (OracleCommand com = new OracleCommand(
                        "SELECT" +
                        " (SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        " FROM DUAL", con))
                        {
                            using (OracleDataReader reader = com.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    //บางพระคณะวิทยาศาสตร์และเทคโนโลยีข้าราชการ
                                    if (reader.GetInt32(0) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(0); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(1) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(1); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(2) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(2); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(3) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(3); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(4) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(4); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //บางพระคณะวิทยาศาสตร์และเทคโนโลยีพนักงานในสถาบัน
                                    if (reader.GetInt32(5) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(5); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(6) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(6); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(7) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(7); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(8) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(8); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(9) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(9); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //บางพระคณะวิทยาศาสตร์และเทคโนโลยีพนักงานราชการ
                                    if (reader.GetInt32(10) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(10); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(11) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(11); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(12) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(12); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(13) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(13); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(14) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(14); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //บางพระคณะวิทยาศาสตร์และเทคโนโลยีลูกจ้างชั่วคราว
                                    if (reader.GetInt32(15) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(15); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(16) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(16); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(17) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(17); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(18) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(18); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(19) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(19); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //บางพระคณะวิทยาศาสตร์และเทคโนโลยีรวมทั้งสิ้น
                                    if (reader.GetInt32(20) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(20); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(21) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(21); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(22) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(22); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(23) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(23); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(24) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(24); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                }
                            }
                        }
                        table.Rows.Add(row);
                    }
                }
                //3คณะมนุษยศาสตร์และสังคมศาสตร์
                {
                    TableHeaderRow row = new TableHeaderRow();
                    { TableCell cell = new TableCell(); cell.Text = "คณะมนุษยศาสตร์และสังคมศาสตร์"; cell.Style["text-align"] = "left"; cell.Width = 250; row.Cells.Add(cell); }
                    using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
                    {
                        con.Open();
                        using (OracleCommand com = new OracleCommand(
                        "SELECT" +
                        " (SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        " FROM DUAL", con))
                        {
                            using (OracleDataReader reader = com.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    //บางพระคณะมนุษยศาสตร์และสังคมศาสตร์ข้าราชการ
                                    if (reader.GetInt32(0) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(0); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(1) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(1); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(2) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(2); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(3) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(3); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(4) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(4); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //บางพระคณะมนุษยศาสตร์และสังคมศาสตร์พนักงานในสถาบัน
                                    if (reader.GetInt32(5) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(5); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(6) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(6); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(7) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(7); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(8) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(8); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(9) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(9); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //บางพระคณะมนุษยศาสตร์และสังคมศาสตร์พนักงานราชการ
                                    if (reader.GetInt32(10) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(10); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(11) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(11); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(12) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(12); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(13) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(13); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(14) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(14); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //บางพระคณะมนุษยศาสตร์และสังคมศาสตร์ลูกจ้างชั่วคราว
                                    if (reader.GetInt32(15) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(15); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(16) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(16); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(17) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(17); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(18) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(18); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(19) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(19); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //บางพระคณะมนุษยศาสตร์และสังคมศาสตร์รวมทั้งสิ้น
                                    if (reader.GetInt32(20) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(20); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(21) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(21); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(22) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(22); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(23) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(23); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(24) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(24); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                }
                            }
                        }
                        table.Rows.Add(row);
                    }
                }
                //4คณะสัตวแพทยศาสตร์
                {
                    TableHeaderRow row = new TableHeaderRow();
                    { TableCell cell = new TableCell(); cell.Text = "คณะสัตวแพทยศาสตร์"; cell.Style["text-align"] = "left"; cell.Width = 250; row.Cells.Add(cell); }
                    using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
                    {
                        con.Open();
                        using (OracleCommand com = new OracleCommand(
                        "SELECT" +
                        " (SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        " FROM DUAL", con))
                        {
                            using (OracleDataReader reader = com.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    //บางพระคณะสัตวแพทยศาสตร์ข้าราชการ
                                    if (reader.GetInt32(0) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(0); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(1) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(1); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(2) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(2); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(3) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(3); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(4) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(4); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //บางพระคณะสัตวแพทยศาสตร์พนักงานในสถาบัน
                                    if (reader.GetInt32(5) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(5); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(6) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(6); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(7) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(7); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(8) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(8); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(9) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(9); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //บางพระคณะสัตวแพทยศาสตร์พนักงานราชการ
                                    if (reader.GetInt32(10) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(10); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(11) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(11); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(12) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(12); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(13) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(13); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(14) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(14); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //บางพระคณะสัตวแพทยศาสตร์ลูกจ้างชั่วคราว
                                    if (reader.GetInt32(15) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(15); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(16) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(16); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(17) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(17); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(18) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(18); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(19) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(19); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //บางพระคณะสัตวแพทยศาสตร์รวมทั้งสิ้น
                                    if (reader.GetInt32(20) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(20); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(21) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(21); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(22) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(22); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(23) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(23); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(24) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(24); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                }
                            }
                        }
                        table.Rows.Add(row);
                    }
                }
                //5สถาบันเทคโนโลยีการบิน
                {
                    TableHeaderRow row = new TableHeaderRow();
                    { TableCell cell = new TableCell(); cell.Text = "สถาบันเทคโนโลยีการบิน"; cell.Style["text-align"] = "left"; cell.Width = 250; row.Cells.Add(cell); }
                    using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
                    {
                        con.Open();
                        using (OracleCommand com = new OracleCommand(
                        "SELECT" +
                        " (SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        " FROM DUAL", con))
                        {
                            using (OracleDataReader reader = com.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    //บางพระสถาบันเทคโนโลยีการบินข้าราชการ
                                    if (reader.GetInt32(0) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(0); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(1) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(1); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(2) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(2); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(3) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(3); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(4) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(4); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //บางพระสถาบันเทคโนโลยีการบินพนักงานในสถาบัน
                                    if (reader.GetInt32(5) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(5); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(6) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(6); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(7) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(7); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(8) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(8); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(9) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(9); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //บางพระสถาบันเทคโนโลยีการบินพนักงานราชการ
                                    if (reader.GetInt32(10) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(10); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(11) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(11); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(12) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(12); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(13) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(13); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(14) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(14); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //บางพระสถาบันเทคโนโลยีการบินลูกจ้างชั่วคราว
                                    if (reader.GetInt32(15) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(15); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(16) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(16); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(17) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(17); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(18) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(18); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(19) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(19); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //บางพระสถาบันเทคโนโลยีการบินรวมทั้งสิ้น
                                    if (reader.GetInt32(20) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(20); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(21) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(21); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(22) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(22); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(23) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(23); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(24) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(24); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                }
                            }
                        }
                        table.Rows.Add(row);
                    }
                }
                //6วิทยาเขตจันทบุรี
                {
                    TableHeaderRow row = new TableHeaderRow();
                    { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "วิทยาเขตจันทบุรี"; cell.Style["text-align"] = "left"; cell.Width = 250; row.Cells.Add(cell); }
                    using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
                    {
                        con.Open();
                        using (OracleCommand com = new OracleCommand(
                        "SELECT" +
                        " (SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        " FROM DUAL", con))
                        {
                            using (OracleDataReader reader = com.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    //จันทบุรีข้าราชการ
                                    if (reader.GetInt32(0) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(0); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(1) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(1); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(2) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(2); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(3) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(3); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(4) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(4); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //จันทบุรีพนักงานในสถาบัน
                                    if (reader.GetInt32(5) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(5); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(6) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(6); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(7) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(7); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(8) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(8); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(9) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(9); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //จันทบุรีพนักงานราชการ
                                    if (reader.GetInt32(10) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(10); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(11) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(11); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(12) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(12); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(13) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(13); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(14) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(14); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //จันทบุรีลูกจ้างชั่วคราว
                                    if (reader.GetInt32(15) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(15); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(16) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(16); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(17) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(17); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(18) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(18); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(19) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(19); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //จันทบุรีรวมทั้งสิ้น
                                    if (reader.GetInt32(20) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(20); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(21) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(21); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(22) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(22); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(23) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(23); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(24) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(24); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                }
                            }
                        }
                        table.Rows.Add(row);
                    }
                }
                //7คณะเทคโนโลยีอุตสาหกรรมการเกษตร
                {
                    TableHeaderRow row = new TableHeaderRow();
                    { TableCell cell = new TableCell(); cell.Text = "คณะเทคโนโลยีอุตสาหกรรมการเกษตร"; cell.Style["text-align"] = "left"; cell.Width = 250; row.Cells.Add(cell); }
                    using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
                    {
                        con.Open();
                        using (OracleCommand com = new OracleCommand(
                        "SELECT" +
                        " (SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        " FROM DUAL", con))
                        {
                            using (OracleDataReader reader = com.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    //จันทบุรีคณะเทคโนโลยีอุตสาหกรรมการเกษตรข้าราชการ
                                    if (reader.GetInt32(0) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(0); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(1) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(1); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(2) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(2); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(3) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(3); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(4) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(4); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //จันทบุรีคณะเทคโนโลยีอุตสาหกรรมการเกษตรพนักงานในสถาบัน
                                    if (reader.GetInt32(5) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(5); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(6) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(6); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(7) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(7); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(8) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(8); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(9) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(9); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //จันทบุรีคณะเทคโนโลยีอุตสาหกรรมการเกษตรพนักงานราชการ
                                    if (reader.GetInt32(10) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(10); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(11) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(11); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(12) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(12); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(13) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(13); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(14) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(14); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //จันทบุรีคณะเทคโนโลยีอุตสาหกรรมการเกษตรลูกจ้างชั่วคราว
                                    if (reader.GetInt32(15) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(15); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(16) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(16); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(17) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(17); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(18) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(18); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(19) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(19); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //จันทบุรีคณะเทคโนโลยีอุตสาหกรรมการเกษตรรวมทั้งสิ้น
                                    if (reader.GetInt32(20) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(20); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(21) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(21); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(22) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(22); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(23) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(23); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(24) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(24); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                }
                            }
                        }
                        table.Rows.Add(row);
                    }
                }
                //8คณะเทคโนโลยีสังคม
                {
                    TableHeaderRow row = new TableHeaderRow();
                    { TableCell cell = new TableCell(); cell.Text = "คณะเทคโนโลยีสังคม"; cell.Style["text-align"] = "left"; cell.Width = 250; row.Cells.Add(cell); }
                    using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
                    {
                        con.Open();
                        using (OracleCommand com = new OracleCommand(
                        "SELECT" +
                        " (SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        " FROM DUAL", con))
                        {
                            using (OracleDataReader reader = com.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    //จันทบุรีคณะเทคโนโลยีสังคมข้าราชการ
                                    if (reader.GetInt32(0) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(0); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(1) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(1); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(2) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(2); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(3) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(3); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(4) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(4); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //จันทบุรีคณะเทคโนโลยีสังคมพนักงานในสถาบัน
                                    if (reader.GetInt32(5) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(5); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(6) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(6); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(7) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(7); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(8) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(8); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(9) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(9); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //จันทบุรีคณะเทคโนโลยีสังคมพนักงานราชการ
                                    if (reader.GetInt32(10) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(10); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(11) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(11); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(12) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(12); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(13) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(13); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(14) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(14); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //จันทบุรีคณะเทคโนโลยีสังคมลูกจ้างชั่วคราว
                                    if (reader.GetInt32(15) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(15); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(16) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(16); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(17) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(17); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(18) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(18); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(19) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(19); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //จันทบุรีคณะเทคโนโลยีสังคมรวมทั้งสิ้น
                                    if (reader.GetInt32(20) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(20); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(21) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(21); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(22) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(22); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(23) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(23); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(24) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(24); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                }
                            }
                        }
                        table.Rows.Add(row);
                    }
                }
                //9วิทยาเขตจักรพงษภูวนารถ
                {
                    TableHeaderRow row = new TableHeaderRow();
                    { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "วิทยาเขตจักรพงษภูวนารถ"; cell.Style["text-align"] = "left"; cell.Width = 250; row.Cells.Add(cell); }
                    using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
                    {
                        con.Open();
                        using (OracleCommand com = new OracleCommand(
                        "SELECT" +
                        " (SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        " FROM DUAL", con))
                        {
                            using (OracleDataReader reader = com.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    //จักรพงษภูวนารถข้าราชการ
                                    if (reader.GetInt32(0) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(0); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(1) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(1); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(2) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(2); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(3) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(3); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(4) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(4); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //จักรพงษภูวนารถพนักงานในสถาบัน
                                    if (reader.GetInt32(5) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(5); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(6) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(6); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(7) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(7); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(8) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(8); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(9) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(9); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //จักรพงษภูวนารถพนักงานราชการ
                                    if (reader.GetInt32(10) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(10); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(11) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(11); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(12) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(12); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(13) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(13); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(14) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(14); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //จักรพงษภูวนารถลูกจ้างชั่วคราว
                                    if (reader.GetInt32(15) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(15); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(16) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(16); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(17) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(17); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(18) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(18); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(19) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(19); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //จักรพงษภูวนารถรวมทั้งสิ้น
                                    if (reader.GetInt32(20) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(20); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(21) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(21); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(22) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(22); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(23) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(23); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(24) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(24); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                }
                            }
                        }
                        table.Rows.Add(row);
                    }
                }
                //10คณะบริหารธุรกิจและเทคโนโลยีสารสนเทศ
                {
                    TableHeaderRow row = new TableHeaderRow();
                    { TableCell cell = new TableCell(); cell.Text = "คณะบริหารธุรกิจและเทคโนโลยีสารสนเทศ"; cell.Style["text-align"] = "left"; cell.Width = 250; row.Cells.Add(cell); }
                    using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
                    {
                        con.Open();
                        using (OracleCommand com = new OracleCommand(
                        "SELECT" +
                        " (SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        " FROM DUAL", con))
                        {
                            using (OracleDataReader reader = com.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    //จักรพงษภูวนารถคณะบริหารธุรกิจและเทคโนโลยีสารสนเทศข้าราชการ
                                    if (reader.GetInt32(0) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(0); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(1) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(1); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(2) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(2); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(3) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(3); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(4) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(4); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //จักรพงษภูวนารถคณะบริหารธุรกิจและเทคโนโลยีสารสนเทศพนักงานในสถาบัน
                                    if (reader.GetInt32(5) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(5); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(6) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(6); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(7) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(7); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(8) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(8); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(9) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(9); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //จักรพงษภูวนารถคณะบริหารธุรกิจและเทคโนโลยีสารสนเทศพนักงานราชการ
                                    if (reader.GetInt32(10) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(10); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(11) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(11); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(12) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(12); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(13) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(13); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(14) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(14); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //จักรพงษภูวนารถคณะบริหารธุรกิจและเทคโนโลยีสารสนเทศลูกจ้างชั่วคราว
                                    if (reader.GetInt32(15) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(15); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(16) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(16); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(17) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(17); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(18) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(18); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(19) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(19); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //จักรพงษภูวนารถคณะบริหารธุรกิจและเทคโนโลยีสารสนเทศรวมทั้งสิ้น
                                    if (reader.GetInt32(20) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(20); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(21) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(21); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(22) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(22); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(23) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(23); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(24) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(24); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                }
                            }
                        }
                        table.Rows.Add(row);
                    }
                }
                //11คณะศิลปศาสตร์
                {
                    TableHeaderRow row = new TableHeaderRow();
                    { TableCell cell = new TableCell(); cell.Text = "คณะศิลปศาสตร์"; cell.Style["text-align"] = "left"; cell.Width = 250; row.Cells.Add(cell); }
                    using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
                    {
                        con.Open();
                        using (OracleCommand com = new OracleCommand(
                        "SELECT" +
                        " (SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        " FROM DUAL", con))
                        {
                            using (OracleDataReader reader = com.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    //จักรพงษภูวนารถคณะศิลปศาสตร์ข้าราชการ
                                    if (reader.GetInt32(0) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(0); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(1) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(1); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(2) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(2); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(3) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(3); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(4) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(4); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //จักรพงษภูวนารถคณะศิลปศาสตร์พนักงานในสถาบัน
                                    if (reader.GetInt32(5) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(5); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(6) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(6); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(7) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(7); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(8) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(8); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(9) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(9); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //จักรพงษภูวนารถคณะศิลปศาสตร์พนักงานราชการ
                                    if (reader.GetInt32(10) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(10); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(11) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(11); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(12) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(12); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(13) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(13); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(14) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(14); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //จักรพงษภูวนารถคณะศิลปศาสตร์ลูกจ้างชั่วคราว
                                    if (reader.GetInt32(15) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(15); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(16) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(16); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(17) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(17); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(18) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(18); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(19) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(19); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //จักรพงษภูวนารถคณะศิลปศาสตร์รวมทั้งสิ้น
                                    if (reader.GetInt32(20) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(20); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(21) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(21); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(22) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(22); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(23) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(23); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(24) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(24); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                }
                            }
                        }
                        table.Rows.Add(row);
                    }
                }
                //12วิทยาเขตอุเทนถวาย
                {
                    TableHeaderRow row = new TableHeaderRow();
                    { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "วิทยาเขตอุเทนถวาย"; cell.Style["text-align"] = "left"; cell.Width = 250; row.Cells.Add(cell); }
                    using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
                    {
                        con.Open();
                        using (OracleCommand com = new OracleCommand(
                        "SELECT" +
                        " (SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        " FROM DUAL", con))
                        {
                            using (OracleDataReader reader = com.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    //วิทยาเขตอุเทนถวายข้าราชการ
                                    if (reader.GetInt32(0) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(0); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(1) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(1); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(2) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(2); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(3) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(3); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(4) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(4); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //วิทยาเขตอุเทนถวายพนักงานในสถาบัน
                                    if (reader.GetInt32(5) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(5); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(6) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(6); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(7) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(7); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(8) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(8); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(9) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(9); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //วิทยาเขตอุเทนถวายพนักงานราชการ
                                    if (reader.GetInt32(10) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(10); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(11) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(11); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(12) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(12); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(13) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(13); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(14) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(14); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //วิทยาเขตอุเทนถวายลูกจ้างชั่วคราว
                                    if (reader.GetInt32(15) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(15); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(16) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(16); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(17) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(17); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(18) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(18); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(19) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(19); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //วิทยาเขตอุเทนถวายรวมทั้งสิ้น
                                    if (reader.GetInt32(20) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(20); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(21) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(21); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(22) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(22); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(23) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(23); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(24) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(24); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                }
                            }
                        }
                        table.Rows.Add(row);
                    }
                }
                //13คณะวิศวกรรมศาสตร์และสถาปัตยกรรมศาสตร์
                {
                    TableHeaderRow row = new TableHeaderRow();
                    { TableCell cell = new TableCell(); cell.Text = "คณะวิศวกรรมศาสตร์และสถาปัตยกรรมศาสตร์"; cell.Style["text-align"] = "left"; cell.Width = 250; row.Cells.Add(cell); }
                    using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
                    {
                        con.Open();
                        using (OracleCommand com = new OracleCommand(
                        "SELECT" +
                        " (SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10102)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10097)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10077)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10108)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID IN(10102,10097,10077,10108))" +
                        " FROM DUAL", con))
                        {
                            using (OracleDataReader reader = com.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    //อุเทนถวายคณะวิศวกรรมศาสตร์และสถาปัตยกรรมศาสตร์ข้าราชการ
                                    if (reader.GetInt32(0) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(0); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(1) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(1); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(2) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(2); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(3) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(3); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(4) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(4); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //อุเทนถวายคณะวิศวกรรมศาสตร์และสถาปัตยกรรมศาสตร์พนักงานในสถาบัน
                                    if (reader.GetInt32(5) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(5); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(6) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(6); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(7) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(7); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(8) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(8); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(9) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(9); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //อุเทนถวายคณะวิศวกรรมศาสตร์และสถาปัตยกรรมศาสตร์พนักงานราชการ
                                    if (reader.GetInt32(10) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(10); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(11) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(11); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(12) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(12); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(13) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(13); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(14) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(14); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //อุเทนถวายคณะวิศวกรรมศาสตร์และสถาปัตยกรรมศาสตร์ลูกจ้างชั่วคราว
                                    if (reader.GetInt32(15) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(15); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(16) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(16); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(17) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(17); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(18) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(18); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(19) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(19); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    //อุเทนถวายคณะวิศวกรรมศาสตร์และสถาปัตยกรรมศาสตร์รวมทั้งสิ้น
                                    if (reader.GetInt32(20) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(20); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(21) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(21); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(22) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(22); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(23) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(23); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(24) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(24); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                }
                            }
                        }
                        table.Rows.Add(row);
                    }
                }

                Panel1.Controls.Clear();
                Panel1.Controls.Add(table);
                return table;
            }


        }

        private Table Bindจำนวนบุคลารสายวิชาการจำแนกตามประเภทบุคลากรคณะและวุฒิการศึกษา()
        {
            Table table = new Table();
            table.CssClass = "ps-table-1";

            {
                TableHeaderRow row = new TableHeaderRow();
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "สรุปข้อมูลจำนวนบุคลากรสายวิชาการ จำแนกตามประเภทบุคลากร คณะ และวุฒิการศึกษา (ข้อมูล ณ วันที่ " + DateTime.Today.ToLongDateString() + ")"; cell.Style["text-align"] = "center"; cell.ColumnSpan = 26; row.Cells.Add(cell); }
                table.Rows.Add(row);
            }

            {
                TableHeaderRow row = new TableHeaderRow();
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "คณะ"; cell.RowSpan = 2; cell.Style["text-align"] = "center"; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "ข้าราชการพลเรือน"; cell.ColumnSpan = 4; cell.Style["text-align"] = "center"; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "พนักงานในสถาบันฯ"; cell.ColumnSpan = 4; cell.Style["text-align"] = "center"; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "พนักงานราชการ"; cell.ColumnSpan = 4; cell.Style["text-align"] = "center"; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "ลูกจ้างชั่วคราว"; cell.ColumnSpan = 4; cell.Style["text-align"] = "center"; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "รวมทั้งสิ้น"; cell.ColumnSpan = 4; cell.Style["text-align"] = "center"; row.Cells.Add(cell); }
                table.Rows.Add(row);
            }

            {
                TableHeaderRow row = new TableHeaderRow();
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "ป.เอก"; cell.Style["text-align"] = "center"; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "ป.โท"; cell.Style["text-align"] = "center"; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "ป.ตรี"; cell.Style["text-align"] = "center"; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "รวม"; cell.Style["text-align"] = "center"; row.Cells.Add(cell); }

                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "ป.เอก"; cell.Style["text-align"] = "center"; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "ป.โท"; cell.Style["text-align"] = "center"; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "ป.ตรี"; cell.Style["text-align"] = "center"; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "รวม"; cell.Style["text-align"] = "center"; row.Cells.Add(cell); }

                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "ป.เอก"; cell.Style["text-align"] = "center"; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "ป.โท"; cell.Style["text-align"] = "center"; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "ป.ตรี"; cell.Style["text-align"] = "center"; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "รวม"; cell.Style["text-align"] = "center"; row.Cells.Add(cell); }

                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "ป.เอก"; cell.Style["text-align"] = "center"; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "ป.โท"; cell.Style["text-align"] = "center"; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "ป.ตรี"; cell.Style["text-align"] = "center"; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "รวม"; cell.Style["text-align"] = "center"; row.Cells.Add(cell); }

                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "ป.เอก"; cell.Style["text-align"] = "center"; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "ป.โท"; cell.Style["text-align"] = "center"; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "ป.ตรี"; cell.Style["text-align"] = "center"; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "รวม"; cell.Style["text-align"] = "center"; row.Cells.Add(cell); }
                table.Rows.Add(row);
            }
            if (ddlCampus.SelectedValue == "1")
            {
                //0วิทยาเขตบางพระ
                {
                    TableHeaderRow row = new TableHeaderRow();
                    { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "วิทยาเขตบางพระ"; cell.Style["text-align"] = "left"; cell.Width = 250; row.Cells.Add(cell); }
                    using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
                    {
                        con.Open();
                        using (OracleCommand com = new OracleCommand(
                        "SELECT" +
                        " (SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        " FROM DUAL", con))
                        {
                            using (OracleDataReader reader = com.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    if (reader.GetInt32(0) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(0); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(1) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(1); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(2) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(2); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(3) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(3); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(4) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(4); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(5) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(5); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(6) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(6); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(7) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(7); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(8) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(8); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(9) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(9); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(10) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(10); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(11) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(11); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(12) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(12); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(13) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(13); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(14) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(14); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(15) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(15); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(16) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(16); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(17) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(17); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(18) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(18); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(19) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(19); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                }
                            }
                        }
                        table.Rows.Add(row);
                    }
                }

                //1คณะเกษตรศาสตร์และทรัพยากรธรรมชาติ
                {
                    TableHeaderRow row = new TableHeaderRow();
                    { TableCell cell = new TableCell(); cell.Text = "คณะเกษตรศาสตร์และทรัพยากรธรรมชาติ"; cell.Style["text-align"] = "left"; cell.Width = 250; row.Cells.Add(cell); }
                    using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
                    {
                        con.Open();
                        using (OracleCommand com = new OracleCommand(
                        "SELECT" +
                        " (SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        " FROM DUAL", con))
                        {
                            using (OracleDataReader reader = com.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    if (reader.GetInt32(0) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(0); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(1) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(1); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(2) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(2); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(3) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(3); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(4) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(4); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(5) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(5); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(6) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(6); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(7) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(7); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(8) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(8); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(9) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(9); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(10) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(10); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(11) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(11); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(12) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(12); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(13) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(13); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(14) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(14); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(15) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(15); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(16) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(16); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(17) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(17); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(18) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(18); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(19) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(19); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                }
                            }
                        }
                        table.Rows.Add(row);
                    }
                }
                //2คณะวิทยาศาสตร์และเทคโนโลยี
                {
                    TableHeaderRow row = new TableHeaderRow();
                    { TableCell cell = new TableCell(); cell.Text = "คณะวิทยาศาสตร์และเทคโนโลยี"; cell.Style["text-align"] = "left"; cell.Width = 250; row.Cells.Add(cell); }
                    using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
                    {
                        con.Open();
                        using (OracleCommand com = new OracleCommand(
                        "SELECT" +
                        " (SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        " FROM DUAL", con))
                        {
                            using (OracleDataReader reader = com.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    if (reader.GetInt32(0) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(0); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(1) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(1); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(2) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(2); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(3) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(3); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(4) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(4); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(5) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(5); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(6) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(6); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(7) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(7); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(8) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(8); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(9) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(9); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(10) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(10); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(11) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(11); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(12) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(12); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(13) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(13); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(14) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(14); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(15) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(15); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(16) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(16); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(17) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(17); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(18) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(18); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(19) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(19); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                }
                            }
                        }
                        table.Rows.Add(row);
                    }
                }
                //3คณะมนุษยศาสตร์และสังคมศาสตร์
                {
                    TableHeaderRow row = new TableHeaderRow();
                    { TableCell cell = new TableCell(); cell.Text = "คณะมนุษยศาสตร์และสังคมศาสตร์"; cell.Style["text-align"] = "left"; cell.Width = 250; row.Cells.Add(cell); }
                    using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
                    {
                        con.Open();
                        using (OracleCommand com = new OracleCommand(
                        "SELECT" +
                        " (SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        " FROM DUAL", con))
                        {
                            using (OracleDataReader reader = com.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    if (reader.GetInt32(0) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(0); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(1) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(1); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(2) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(2); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(3) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(3); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(4) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(4); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(5) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(5); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(6) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(6); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(7) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(7); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(8) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(8); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(9) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(9); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(10) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(10); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(11) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(11); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(12) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(12); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(13) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(13); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(14) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(14); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(15) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(15); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(16) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(16); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(17) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(17); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(18) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(18); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(19) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(19); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                }
                            }
                        }
                        table.Rows.Add(row);
                    }
                }
                //4คณะสัตวแพทยศาสตร์
                {
                    TableHeaderRow row = new TableHeaderRow();
                    { TableCell cell = new TableCell(); cell.Text = "คณะสัตวแพทยศาสตร์"; cell.Style["text-align"] = "left"; cell.Width = 250; row.Cells.Add(cell); }
                    using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
                    {
                        con.Open();
                        using (OracleCommand com = new OracleCommand(
                        "SELECT" +
                        " (SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        " FROM DUAL", con))
                        {
                            using (OracleDataReader reader = com.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    if (reader.GetInt32(0) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(0); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(1) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(1); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(2) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(2); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(3) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(3); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(4) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(4); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(5) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(5); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(6) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(6); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(7) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(7); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(8) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(8); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(9) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(9); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(10) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(10); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(11) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(11); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(12) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(12); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(13) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(13); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(14) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(14); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(15) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(15); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(16) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(16); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(17) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(17); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(18) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(18); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(19) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(19); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                }
                            }
                        }
                        table.Rows.Add(row);
                    }
                }
                //5สถาบันเทคโนโลยีการบิน
                {
                    TableHeaderRow row = new TableHeaderRow();
                    { TableCell cell = new TableCell(); cell.Text = "สถาบันเทคโนโลยีการบิน"; cell.Style["text-align"] = "left"; cell.Width = 250; row.Cells.Add(cell); }
                    using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
                    {
                        con.Open();
                        using (OracleCommand com = new OracleCommand(
                        "SELECT" +
                        " (SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        " FROM DUAL", con))
                        {
                            using (OracleDataReader reader = com.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    if (reader.GetInt32(0) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(0); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(1) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(1); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(2) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(2); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(3) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(3); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(4) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(4); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(5) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(5); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(6) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(6); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(7) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(7); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(8) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(8); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(9) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(9); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(10) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(10); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(11) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(11); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(12) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(12); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(13) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(13); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(14) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(14); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(15) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(15); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(16) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(16); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(17) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(17); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(18) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(18); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(19) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(19); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                }
                            }
                        }
                        table.Rows.Add(row);
                    }
                }
                Panel1.Controls.Clear();
                Panel1.Controls.Add(table);
                return table;
            }
            if (ddlCampus.SelectedValue == "2")
            {
                //6วิทยาเขตจันทบุรี
                {
                    TableHeaderRow row = new TableHeaderRow();
                    { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "วิทยาเขตจันทบุรี"; cell.Style["text-align"] = "left"; cell.Width = 250; row.Cells.Add(cell); }
                    using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
                    {
                        con.Open();
                        using (OracleCommand com = new OracleCommand(
                        "SELECT" +
                        " (SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        " FROM DUAL", con))
                        {
                            using (OracleDataReader reader = com.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    if (reader.GetInt32(0) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(0); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(1) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(1); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(2) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(2); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(3) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(3); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(4) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(4); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(5) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(5); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(6) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(6); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(7) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(7); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(8) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(8); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(9) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(9); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(10) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(10); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(11) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(11); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(12) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(12); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(13) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(13); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(14) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(14); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(15) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(15); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(16) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(16); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(17) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(17); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(18) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(18); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(19) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(19); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                }
                            }
                        }
                        table.Rows.Add(row);
                    }
                }
                //7คณะเทคโนโลยีอุตสาหกรรมการเกษตร
                {
                    TableHeaderRow row = new TableHeaderRow();
                    { TableCell cell = new TableCell(); cell.Text = "คณะเทคโนโลยีอุตสาหกรรมการเกษตร"; cell.Style["text-align"] = "left"; cell.Width = 250; row.Cells.Add(cell); }
                    using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
                    {
                        con.Open();
                        using (OracleCommand com = new OracleCommand(
                        "SELECT" +
                        " (SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        " FROM DUAL", con))
                        {
                            using (OracleDataReader reader = com.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    if (reader.GetInt32(0) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(0); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(1) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(1); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(2) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(2); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(3) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(3); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(4) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(4); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(5) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(5); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(6) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(6); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(7) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(7); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(8) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(8); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(9) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(9); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(10) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(10); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(11) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(11); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(12) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(12); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(13) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(13); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(14) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(14); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(15) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(15); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(16) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(16); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(17) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(17); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(18) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(18); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(19) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(19); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                }
                            }
                        }
                        table.Rows.Add(row);
                    }
                }
                //8คณะเทคโนโลยีสังคม
                {
                    TableHeaderRow row = new TableHeaderRow();
                    { TableCell cell = new TableCell(); cell.Text = "คณะเทคโนโลยีสังคม"; cell.Style["text-align"] = "left"; cell.Width = 250; row.Cells.Add(cell); }
                    using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
                    {
                        con.Open();
                        using (OracleCommand com = new OracleCommand(
                        "SELECT" +
                        " (SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        " FROM DUAL", con))
                        {
                            using (OracleDataReader reader = com.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    if (reader.GetInt32(0) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(0); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(1) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(1); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(2) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(2); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(3) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(3); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(4) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(4); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(5) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(5); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(6) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(6); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(7) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(7); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(8) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(8); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(9) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(9); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(10) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(10); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(11) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(11); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(12) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(12); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(13) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(13); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(14) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(14); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(15) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(15); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(16) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(16); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(17) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(17); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(18) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(18); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(19) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(19); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                }
                            }
                        }
                        table.Rows.Add(row);
                    }
                }
                Panel1.Controls.Clear();
                Panel1.Controls.Add(table);
                return table;
            }
            if (ddlCampus.SelectedValue == "3")
            {
                //9วิทยาเขตจักรพงษภูวนารถ
                {
                    TableHeaderRow row = new TableHeaderRow();
                    { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "วิทยาเขตจักรพงษภูวนารถ"; cell.Style["text-align"] = "left"; cell.Width = 250; row.Cells.Add(cell); }
                    using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
                    {
                        con.Open();
                        using (OracleCommand com = new OracleCommand(
                        "SELECT" +
                        " (SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        " FROM DUAL", con))
                        {
                            using (OracleDataReader reader = com.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    if (reader.GetInt32(0) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(0); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(1) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(1); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(2) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(2); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(3) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(3); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(4) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(4); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(5) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(5); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(6) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(6); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(7) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(7); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(8) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(8); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(9) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(9); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(10) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(10); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(11) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(11); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(12) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(12); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(13) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(13); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(14) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(14); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(15) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(15); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(16) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(16); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(17) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(17); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(18) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(18); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(19) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(19); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                }
                            }
                        }
                        table.Rows.Add(row);
                    }
                }
                //10คณะบริหารธุรกิจและเทคโนโลยีสารสนเทศ
                {
                    TableHeaderRow row = new TableHeaderRow();
                    { TableCell cell = new TableCell(); cell.Text = "คณะบริหารธุรกิจและเทคโนโลยีสารสนเทศ"; cell.Style["text-align"] = "left"; cell.Width = 250; row.Cells.Add(cell); }
                    using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
                    {
                        con.Open();
                        using (OracleCommand com = new OracleCommand(
                        "SELECT" +
                        " (SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        " FROM DUAL", con))
                        {
                            using (OracleDataReader reader = com.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    if (reader.GetInt32(0) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(0); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(1) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(1); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(2) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(2); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(3) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(3); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(4) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(4); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(5) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(5); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(6) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(6); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(7) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(7); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(8) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(8); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(9) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(9); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(10) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(10); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(11) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(11); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(12) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(12); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(13) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(13); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(14) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(14); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(15) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(15); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(16) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(16); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(17) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(17); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(18) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(18); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(19) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(19); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                }
                            }
                        }
                        table.Rows.Add(row);
                    }
                }
                //11คณะศิลปศาสตร์
                {
                    TableHeaderRow row = new TableHeaderRow();
                    { TableCell cell = new TableCell(); cell.Text = "คณะศิลปศาสตร์"; cell.Style["text-align"] = "left"; cell.Width = 250; row.Cells.Add(cell); }
                    using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
                    {
                        con.Open();
                        using (OracleCommand com = new OracleCommand(
                        "SELECT" +
                        " (SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        " FROM DUAL", con))
                        {
                            using (OracleDataReader reader = com.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    if (reader.GetInt32(0) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(0); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(1) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(1); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(2) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(2); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(3) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(3); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(4) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(4); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(5) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(5); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(6) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(6); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(7) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(7); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(8) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(8); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(9) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(9); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(10) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(10); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(11) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(11); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(12) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(12); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(13) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(13); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(14) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(14); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(15) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(15); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(16) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(16); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(17) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(17); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(18) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(18); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(19) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(19); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                }
                            }
                        }
                        table.Rows.Add(row);
                    }
                }
                Panel1.Controls.Clear();
                Panel1.Controls.Add(table);
                return table;
            }
            if (ddlCampus.SelectedValue == "4")
            {
                //12วิทยาเขตอุเทนถวาย
                {
                    TableHeaderRow row = new TableHeaderRow();
                    { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "วิทยาเขตอุเทนถวาย"; cell.Style["text-align"] = "left"; cell.Width = 250; row.Cells.Add(cell); }
                    using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
                    {
                        con.Open();
                        using (OracleCommand com = new OracleCommand(
                        "SELECT" +
                        " (SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        " FROM DUAL", con))
                        {
                            using (OracleDataReader reader = com.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    if (reader.GetInt32(0) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(0); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(1) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(1); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(2) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(2); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(3) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(3); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(4) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(4); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(5) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(5); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(6) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(6); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(7) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(7); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(8) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(8); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(9) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(9); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(10) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(10); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(11) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(11); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(12) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(12); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(13) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(13); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(14) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(14); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(15) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(15); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(16) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(16); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(17) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(17); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(18) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(18); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(19) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(19); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                }
                            }
                        }
                        table.Rows.Add(row);
                    }
                }
                //13คณะวิศวกรรมศาสตร์และสถาปัตยกรรมศาสตร์
                {
                    TableHeaderRow row = new TableHeaderRow();
                    { TableCell cell = new TableCell(); cell.Text = "คณะวิศวกรรมศาสตร์และสถาปัตยกรรมศาสตร์"; cell.Style["text-align"] = "left"; cell.Width = 250; row.Cells.Add(cell); }
                    using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
                    {
                        con.Open();
                        using (OracleCommand com = new OracleCommand(
                        "SELECT" +
                        " (SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        " FROM DUAL", con))
                        {
                            using (OracleDataReader reader = com.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    if (reader.GetInt32(0) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(0); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(1) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(1); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(2) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(2); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(3) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(3); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(4) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(4); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(5) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(5); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(6) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(6); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(7) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(7); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(8) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(8); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(9) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(9); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(10) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(10); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(11) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(11); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(12) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(12); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(13) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(13); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(14) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(14); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(15) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(15); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(16) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(16); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(17) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(17); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(18) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(18); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(19) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(19); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                }
                            }
                        }
                        table.Rows.Add(row);
                    }
                }
                Panel1.Controls.Clear();
                Panel1.Controls.Add(table);
                return table;
            }
            else
            {
                //0วิทยาเขตบางพระ
                {
                    TableHeaderRow row = new TableHeaderRow();
                    { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "วิทยาเขตบางพระ"; cell.Style["text-align"] = "left"; cell.Width = 250; row.Cells.Add(cell); }
                    using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
                    {
                        con.Open();
                        using (OracleCommand com = new OracleCommand(
                        "SELECT" +
                        " (SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID IN(11,13,12,14,309) AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        " FROM DUAL", con))
                        {
                            using (OracleDataReader reader = com.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    if (reader.GetInt32(0) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(0); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(1) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(1); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(2) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(2); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(3) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(3); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(4) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(4); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(5) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(5); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(6) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(6); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(7) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(7); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(8) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(8); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(9) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(9); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(10) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(10); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(11) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(11); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(12) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(12); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(13) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(13); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(14) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(14); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(15) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(15); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(16) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(16); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(17) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(17); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(18) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(18); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(19) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(19); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                }
                            }
                        }
                        table.Rows.Add(row);
                    }
                }

                //1คณะเกษตรศาสตร์และทรัพยากรธรรมชาติ
                {
                    TableHeaderRow row = new TableHeaderRow();
                    { TableCell cell = new TableCell(); cell.Text = "คณะเกษตรศาสตร์และทรัพยากรธรรมชาติ"; cell.Style["text-align"] = "left"; cell.Width = 250; row.Cells.Add(cell); }
                    using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
                    {
                        con.Open();
                        using (OracleCommand com = new OracleCommand(
                        "SELECT" +
                        " (SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 11 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        " FROM DUAL", con))
                        {
                            using (OracleDataReader reader = com.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    if (reader.GetInt32(0) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(0); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(1) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(1); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(2) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(2); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(3) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(3); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(4) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(4); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(5) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(5); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(6) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(6); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(7) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(7); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(8) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(8); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(9) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(9); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(10) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(10); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(11) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(11); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(12) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(12); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(13) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(13); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(14) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(14); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(15) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(15); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(16) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(16); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(17) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(17); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(18) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(18); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(19) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(19); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                }
                            }
                        }
                        table.Rows.Add(row);
                    }
                }
                //2คณะวิทยาศาสตร์และเทคโนโลยี
                {
                    TableHeaderRow row = new TableHeaderRow();
                    { TableCell cell = new TableCell(); cell.Text = "คณะวิทยาศาสตร์และเทคโนโลยี"; cell.Style["text-align"] = "left"; cell.Width = 250; row.Cells.Add(cell); }
                    using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
                    {
                        con.Open();
                        using (OracleCommand com = new OracleCommand(
                        "SELECT" +
                        " (SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 13 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        " FROM DUAL", con))
                        {
                            using (OracleDataReader reader = com.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    if (reader.GetInt32(0) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(0); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(1) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(1); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(2) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(2); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(3) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(3); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(4) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(4); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(5) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(5); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(6) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(6); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(7) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(7); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(8) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(8); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(9) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(9); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(10) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(10); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(11) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(11); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(12) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(12); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(13) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(13); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(14) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(14); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(15) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(15); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(16) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(16); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(17) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(17); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(18) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(18); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(19) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(19); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                }
                            }
                        }
                        table.Rows.Add(row);
                    }
                }
                //3คณะมนุษยศาสตร์และสังคมศาสตร์
                {
                    TableHeaderRow row = new TableHeaderRow();
                    { TableCell cell = new TableCell(); cell.Text = "คณะมนุษยศาสตร์และสังคมศาสตร์"; cell.Style["text-align"] = "left"; cell.Width = 250; row.Cells.Add(cell); }
                    using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
                    {
                        con.Open();
                        using (OracleCommand com = new OracleCommand(
                        "SELECT" +
                        " (SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 12 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        " FROM DUAL", con))
                        {
                            using (OracleDataReader reader = com.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    if (reader.GetInt32(0) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(0); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(1) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(1); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(2) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(2); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(3) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(3); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(4) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(4); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(5) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(5); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(6) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(6); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(7) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(7); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(8) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(8); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(9) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(9); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(10) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(10); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(11) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(11); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(12) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(12); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(13) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(13); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(14) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(14); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(15) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(15); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(16) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(16); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(17) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(17); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(18) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(18); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(19) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(19); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                }
                            }
                        }
                        table.Rows.Add(row);
                    }
                }
                //4คณะสัตวแพทยศาสตร์
                {
                    TableHeaderRow row = new TableHeaderRow();
                    { TableCell cell = new TableCell(); cell.Text = "คณะสัตวแพทยศาสตร์"; cell.Style["text-align"] = "left"; cell.Width = 250; row.Cells.Add(cell); }
                    using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
                    {
                        con.Open();
                        using (OracleCommand com = new OracleCommand(
                        "SELECT" +
                        " (SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 14 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        " FROM DUAL", con))
                        {
                            using (OracleDataReader reader = com.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    if (reader.GetInt32(0) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(0); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(1) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(1); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(2) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(2); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(3) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(3); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(4) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(4); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(5) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(5); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(6) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(6); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(7) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(7); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(8) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(8); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(9) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(9); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(10) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(10); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(11) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(11); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(12) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(12); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(13) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(13); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(14) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(14); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(15) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(15); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(16) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(16); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(17) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(17); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(18) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(18); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(19) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(19); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                }
                            }
                        }
                        table.Rows.Add(row);
                    }
                }
                //5สถาบันเทคโนโลยีการบิน
                {
                    TableHeaderRow row = new TableHeaderRow();
                    { TableCell cell = new TableCell(); cell.Text = "สถาบันเทคโนโลยีการบิน"; cell.Style["text-align"] = "left"; cell.Width = 250; row.Cells.Add(cell); }
                    using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
                    {
                        con.Open();
                        using (OracleCommand com = new OracleCommand(
                        "SELECT" +
                        " (SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 1 AND PS_FACULTY_ID = 309 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        " FROM DUAL", con))
                        {
                            using (OracleDataReader reader = com.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    if (reader.GetInt32(0) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(0); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(1) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(1); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(2) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(2); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(3) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(3); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(4) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(4); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(5) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(5); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(6) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(6); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(7) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(7); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(8) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(8); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(9) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(9); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(10) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(10); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(11) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(11); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(12) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(12); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(13) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(13); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(14) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(14); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(15) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(15); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(16) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(16); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(17) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(17); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(18) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(18); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(19) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(19); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                }
                            }
                        }
                        table.Rows.Add(row);
                    }
                }
                //6วิทยาเขตจันทบุรี
                {
                    TableHeaderRow row = new TableHeaderRow();
                    { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "วิทยาเขตจันทบุรี"; cell.Style["text-align"] = "left"; cell.Width = 250; row.Cells.Add(cell); }
                    using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
                    {
                        con.Open();
                        using (OracleCommand com = new OracleCommand(
                        "SELECT" +
                        " (SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID IN(16,17) AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        " FROM DUAL", con))
                        {
                            using (OracleDataReader reader = com.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    if (reader.GetInt32(0) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(0); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(1) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(1); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(2) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(2); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(3) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(3); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(4) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(4); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(5) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(5); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(6) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(6); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(7) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(7); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(8) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(8); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(9) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(9); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(10) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(10); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(11) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(11); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(12) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(12); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(13) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(13); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(14) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(14); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(15) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(15); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(16) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(16); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(17) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(17); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(18) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(18); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(19) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(19); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                }
                            }
                        }
                        table.Rows.Add(row);
                    }
                }
                //7คณะเทคโนโลยีอุตสาหกรรมการเกษตร
                {
                    TableHeaderRow row = new TableHeaderRow();
                    { TableCell cell = new TableCell(); cell.Text = "คณะเทคโนโลยีอุตสาหกรรมการเกษตร"; cell.Style["text-align"] = "left"; cell.Width = 250; row.Cells.Add(cell); }
                    using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
                    {
                        con.Open();
                        using (OracleCommand com = new OracleCommand(
                        "SELECT" +
                        " (SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 16 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        " FROM DUAL", con))
                        {
                            using (OracleDataReader reader = com.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    if (reader.GetInt32(0) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(0); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(1) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(1); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(2) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(2); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(3) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(3); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(4) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(4); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(5) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(5); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(6) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(6); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(7) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(7); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(8) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(8); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(9) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(9); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(10) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(10); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(11) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(11); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(12) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(12); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(13) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(13); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(14) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(14); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(15) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(15); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(16) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(16); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(17) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(17); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(18) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(18); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(19) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(19); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                }
                            }
                        }
                        table.Rows.Add(row);
                    }
                }
                //8คณะเทคโนโลยีสังคม
                {
                    TableHeaderRow row = new TableHeaderRow();
                    { TableCell cell = new TableCell(); cell.Text = "คณะเทคโนโลยีสังคม"; cell.Style["text-align"] = "left"; cell.Width = 250; row.Cells.Add(cell); }
                    using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
                    {
                        con.Open();
                        using (OracleCommand com = new OracleCommand(
                        "SELECT" +
                        " (SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 2 AND PS_FACULTY_ID = 17 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        " FROM DUAL", con))
                        {
                            using (OracleDataReader reader = com.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    if (reader.GetInt32(0) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(0); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(1) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(1); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(2) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(2); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(3) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(3); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(4) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(4); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(5) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(5); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(6) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(6); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(7) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(7); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(8) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(8); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(9) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(9); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(10) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(10); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(11) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(11); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(12) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(12); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(13) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(13); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(14) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(14); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(15) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(15); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(16) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(16); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(17) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(17); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(18) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(18); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(19) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(19); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                }
                            }
                        }
                        table.Rows.Add(row);
                    }
                }
                //9วิทยาเขตจักรพงษภูวนารถ
                {
                    TableHeaderRow row = new TableHeaderRow();
                    { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "วิทยาเขตจักรพงษภูวนารถ"; cell.Style["text-align"] = "left"; cell.Width = 250; row.Cells.Add(cell); }
                    using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
                    {
                        con.Open();
                        using (OracleCommand com = new OracleCommand(
                        "SELECT" +
                        " (SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID IN(20,21) AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        " FROM DUAL", con))
                        {
                            using (OracleDataReader reader = com.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    if (reader.GetInt32(0) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(0); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(1) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(1); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(2) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(2); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(3) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(3); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(4) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(4); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(5) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(5); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(6) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(6); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(7) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(7); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(8) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(8); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(9) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(9); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(10) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(10); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(11) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(11); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(12) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(12); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(13) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(13); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(14) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(14); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(15) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(15); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(16) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(16); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(17) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(17); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(18) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(18); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(19) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(19); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                }
                            }
                        }
                        table.Rows.Add(row);
                    }
                }
                //10คณะบริหารธุรกิจและเทคโนโลยีสารสนเทศ
                {
                    TableHeaderRow row = new TableHeaderRow();
                    { TableCell cell = new TableCell(); cell.Text = "คณะบริหารธุรกิจและเทคโนโลยีสารสนเทศ"; cell.Style["text-align"] = "left"; cell.Width = 250; row.Cells.Add(cell); }
                    using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
                    {
                        con.Open();
                        using (OracleCommand com = new OracleCommand(
                        "SELECT" +
                        " (SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 20 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        " FROM DUAL", con))
                        {
                            using (OracleDataReader reader = com.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    if (reader.GetInt32(0) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(0); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(1) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(1); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(2) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(2); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(3) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(3); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(4) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(4); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(5) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(5); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(6) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(6); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(7) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(7); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(8) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(8); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(9) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(9); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(10) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(10); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(11) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(11); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(12) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(12); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(13) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(13); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(14) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(14); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(15) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(15); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(16) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(16); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(17) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(17); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(18) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(18); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(19) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(19); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                }
                            }
                        }
                        table.Rows.Add(row);
                    }
                }
                //11คณะศิลปศาสตร์
                {
                    TableHeaderRow row = new TableHeaderRow();
                    { TableCell cell = new TableCell(); cell.Text = "คณะศิลปศาสตร์"; cell.Style["text-align"] = "left"; cell.Width = 250; row.Cells.Add(cell); }
                    using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
                    {
                        con.Open();
                        using (OracleCommand com = new OracleCommand(
                        "SELECT" +
                        " (SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 3 AND PS_FACULTY_ID = 21 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        " FROM DUAL", con))
                        {
                            using (OracleDataReader reader = com.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    if (reader.GetInt32(0) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(0); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(1) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(1); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(2) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(2); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(3) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(3); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(4) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(4); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(5) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(5); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(6) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(6); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(7) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(7); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(8) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(8); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(9) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(9); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(10) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(10); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(11) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(11); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(12) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(12); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(13) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(13); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(14) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(14); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(15) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(15); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(16) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(16); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(17) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(17); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(18) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(18); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(19) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(19); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                }
                            }
                        }
                        table.Rows.Add(row);
                    }
                }
                //12วิทยาเขตอุเทนถวาย
                {
                    TableHeaderRow row = new TableHeaderRow();
                    { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "วิทยาเขตอุเทนถวาย"; cell.Style["text-align"] = "left"; cell.Width = 250; row.Cells.Add(cell); }
                    using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
                    {
                        con.Open();
                        using (OracleCommand com = new OracleCommand(
                        "SELECT" +
                        " (SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID IN(24) AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        " FROM DUAL", con))
                        {
                            using (OracleDataReader reader = com.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    if (reader.GetInt32(0) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(0); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(1) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(1); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(2) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(2); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(3) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(3); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(4) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(4); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(5) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(5); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(6) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(6); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(7) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(7); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(8) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(8); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(9) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(9); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(10) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(10); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(11) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(11); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(12) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(12); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(13) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(13); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(14) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(14); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(15) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(15); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(16) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(16); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(17) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(17); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(18) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(18); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(19) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(19); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                }
                            }
                        }
                        table.Rows.Add(row);
                    }
                }
                //13คณะวิศวกรรมศาสตร์และสถาปัตยกรรมศาสตร์
                {
                    TableHeaderRow row = new TableHeaderRow();
                    { TableCell cell = new TableCell(); cell.Text = "คณะวิศวกรรมศาสตร์และสถาปัตยกรรมศาสตร์"; cell.Style["text-align"] = "left"; cell.Width = 250; row.Cells.Add(cell); }
                    using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
                    {
                        con.Open();
                        using (OracleCommand com = new OracleCommand(
                        "SELECT" +
                        " (SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 80)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 60)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 40)" +
                        ",(SELECT NVL(COUNT(PS_ID),0) FROM PS_PERSON WHERE PS_CAMPUS_ID = 4 AND PS_FACULTY_ID = 24 AND PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID IN(80,60,40))" +
                        " FROM DUAL", con))
                        {
                            using (OracleDataReader reader = com.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    if (reader.GetInt32(0) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(0); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(1) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(1); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(2) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(2); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(3) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(3); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(4) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(4); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(5) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(5); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(6) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(6); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(7) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(7); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(8) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(8); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(9) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(9); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(10) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(10); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(11) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(11); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(12) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(12); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(13) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(13); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(14) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(14); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(15) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(15); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(16) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(16); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(17) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(17); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(18) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(18); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(19) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(19); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                }
                            }
                        }
                        table.Rows.Add(row);
                    }
                }
                Panel1.Controls.Clear();
                Panel1.Controls.Add(table);
                return table;
            }
        }

        protected void lbuSearch_Click(object sender, EventArgs e)
        {
            if (ddlView.SelectedValue == "1")
            {
                Table table;
                table = Bindจำนวนบุคลารสายวิชาการจำแนกตามประเภทบุคลากรคณะและตำแหน่งทางวิชาการ();
                if (table == null)
                {
                    return;
                }
                Panel1.Controls.Clear();
                Panel1.Controls.Add(table);
            }
            else if (ddlView.SelectedValue == "2")
            {
                Table table;
                table = Bindจำนวนบุคลารสายวิชาการจำแนกตามประเภทบุคลากรคณะและวุฒิการศึกษา();
                if (table == null)
                {
                    return;
                }
                Panel1.Controls.Clear();
                Panel1.Controls.Add(table);
            }

        }

        protected void lbuExport_Click(object sender, EventArgs e)
        {
            if (ddlView.SelectedValue == "1")
            {
                Table table;
                table = Bindจำนวนบุคลารสายวิชาการจำแนกตามประเภทบุคลากรคณะและตำแหน่งทางวิชาการ();
                if (table == null)
                {
                    return;
                }

                Response.AddHeader("Content-Disposition", "attachment;filename=สรุปข้อมูล" + ddlView.SelectedItem.Text + ".xls");
                Response.ContentType = "application/x-msexcel";
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
                Table table;
                table = Bindจำนวนบุคลารสายวิชาการจำแนกตามประเภทบุคลากรคณะและวุฒิการศึกษา();
                if (table == null)
                {
                    return;
                }

                Response.AddHeader("Content-Disposition", "attachment;filename=สรุปข้อมูล" + ddlView.SelectedItem.Text + ".xls");
                Response.ContentType = "application/x-msexcel";
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

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }

    }
}