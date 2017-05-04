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
    public partial class ReportPerson_Admin : System.Web.UI.Page
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

                int minDateInwork = DatabaseManager.ExecuteInt("SELECT EXTRACT(YEAR FROM PS_INWORK_DATE)+543 FROM PS_PERSON WHERE PS_INWORK_DATE = (SELECT MIN(PS_INWORK_DATE) FROM PS_PERSON)");
                int currentYear = Util.BudgetYear() + 543;

                for (int i = minDateInwork; i <= currentYear; ++i)
                {
                    ddlYear.Items.Add(new System.Web.UI.WebControls.ListItem("" + i, "" + i));
                }

                ddlView.Items.Add(new ListItem("แสดงจำนวนบุคลารสายวิชาการ จำแนกตามประเภทบุคลากร คณะ และตำแหน่งทางวิชาการ", "1"));
                ddlView.Items.Add(new ListItem("แสดงจำนวนบุคลารสายวิชาการ จำแนกตามประเภทบุคลากร คณะ และวุฒิการศึกษา", "2"));
                DatabaseManager.BindDropDown(ddlCampus, "SELECT * FROM TB_CAMPUS ORDER BY CAMPUS_ID", "CAMPUS_NAME", "CAMPUS_ID", "--กรุณาเลือก--");
            }
        }

        private Table Bindจำนวนบุคลารสายวิชาการจำแนกตามประเภทบุคลากรคณะและตำแหน่งทางวิชาการ()
        {
            Table table = new Table();
            table.CssClass = "ps-table-1";

            {
                TableHeaderRow row = new TableHeaderRow();
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "สรุปข้อมูลจำนวนบุคลากรสายวิชาการ จำแนกตามประเภทบุคลากร คณะ และตำแหน่งทางวิชาการ ประจำปีงบประมาณ พ.ศ. " + ddlYear.SelectedValue; cell.Style["text-align"] = "center"; cell.ColumnSpan = 26; row.Cells.Add(cell); }
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

            if (ddlCampus.SelectedValue != "1" && ddlCampus.SelectedValue != "2" && ddlCampus.SelectedValue != "3" && ddlCampus.SelectedValue != "4")
            {

                using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
                {
                    con.Open();

                    List<int> list = new List<int>();
                    list.Add(1);
                    list.Add(2);
                    list.Add(3);
                    list.Add(4);

                    foreach (int campusID in list)
                    {
                        using (OracleCommand com = new OracleCommand(
                        "SELECT " +
                            "CAMPUS_NAME AAA, " +
                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10102 AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10097 AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10077 AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10108 AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID IN(10102, 10097, 10077, 10108) AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +

                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10102 AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10097 AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10077 AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10108 AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID IN(10102, 10097, 10077, 10108) AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +

                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10102 AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10097 AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10077 AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10108 AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID IN(10102, 10097, 10077, 10108) AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +

                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10102 AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10097 AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10077 AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10108 AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID IN(10102, 10097, 10077, 10108) AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1),  " +

                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10102 AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1)  ," +
                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10097 AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1)  ," +
                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10077 AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1)  ," +
                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10108 AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1)  ," +
                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID IN(10102, 10097, 10077, 10108) AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) " +

                            "FROM " +
                            "TB_CAMPUS " +
                            "WHERE CAMPUS_ID = " + campusID, con))
                        {
                            int budgetYear = int.Parse(ddlYear.SelectedValue) - 543;
                            com.Parameters.AddWithValue("BUDGET_YEAR", budgetYear);
                            using (OracleDataReader reader = com.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    TableHeaderRow row = new TableHeaderRow();
                                    { TableHeaderCell cell = new TableHeaderCell(); cell.Text = reader.GetString(0); cell.Style["text-align"] = "left"; cell.Width = 250; row.Cells.Add(cell); }

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
                                    if (reader.GetInt32(20) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(20); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(21) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(21); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(22) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(22); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(23) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(23); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(24) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(24); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(25) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(25); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    table.Rows.Add(row);
                                }
                            }
                        }

                        using (OracleCommand com = new OracleCommand(
                        "SELECT " +
                            "FACULTY_NAME AAA, " +
                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10102 AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10097 AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10077 AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10108 AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID IN(10102, 10097, 10077, 10108) AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +

                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10102 AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10097 AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10077 AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10108 AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID IN(10102, 10097, 10077, 10108) AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +

                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10102 AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10097 AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10077 AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10108 AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID IN(10102, 10097, 10077, 10108) AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +

                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10102 AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10097 AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10077 AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10108 AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID IN(10102, 10097, 10077, 10108) AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1),  " +

                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10102 AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1)  ," +
                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10097 AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1)  ," +
                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10077 AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1)  ," +
                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10108 AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1)  ," +
                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID IN(10102, 10097, 10077, 10108) AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) " +

                            "FROM " +
                            "TB_FACULTY " +
                            "WHERE CAMPUS_ID = " + campusID, con))
                        {
                            int budgetYear = int.Parse(ddlYear.SelectedValue) - 543;
                            com.Parameters.AddWithValue("BUDGET_YEAR", budgetYear);
                            using (OracleDataReader reader = com.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    TableRow row = new TableRow();
                                    { TableCell cell = new TableCell(); cell.Text = reader.GetString(0); cell.Style["text-align"] = "left"; cell.Width = 250; row.Cells.Add(cell); }

                                    if (reader.GetInt32(1) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(1); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(2) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(2); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(3) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(3); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(4) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(4); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(5) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(5); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(6) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(6); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(7) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(7); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(8) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(8); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(9) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(9); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(10) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(10); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(11) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(11); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(12) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(12); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(13) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(13); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(14) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(14); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(15) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(15); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(16) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(16); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(17) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(17); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(18) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(18); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(19) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(19); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(20) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(20); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(21) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(21); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(22) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(22); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(23) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(23); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(24) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(24); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(25) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(25); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    table.Rows.Add(row);
                                }
                            }
                        }
                    }

                }

                Panel1.Controls.Clear();
                Panel1.Controls.Add(table);
                return table;

            }
            else
            {
                //TableHeaderRow row = new TableHeaderRow();
                //{ TableHeaderCell cell = new TableHeaderCell(); cell.Text = "วิทยาเขตบางพระ"; cell.Style["text-align"] = "left"; cell.Width = 250; row.Cells.Add(cell); }
                using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
                {
                    con.Open();

                    using (OracleCommand com = new OracleCommand(
                    "SELECT " +
                        "CAMPUS_NAME AAA, " +
                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10102 AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10097 AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10077 AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10108 AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID IN(10102, 10097, 10077, 10108) AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +

                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10102 AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10097 AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10077 AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10108 AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID IN(10102, 10097, 10077, 10108) AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +

                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10102 AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10097 AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10077 AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10108 AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID IN(10102, 10097, 10077, 10108) AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +

                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10102 AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10097 AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10077 AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10108 AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID IN(10102, 10097, 10077, 10108) AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1),  " +

                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10102 AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1)  ," +
                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10097 AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1)  ," +
                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10077 AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1)  ," +
                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10108 AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1)  ," +
                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID IN(10102, 10097, 10077, 10108) AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) " +

                        "FROM " +
                        "TB_CAMPUS " +
                        "WHERE CAMPUS_ID = " + ddlCampus.SelectedValue, con))
                    {
                        int budgetYear = int.Parse(ddlYear.SelectedValue) - 543;
                        com.Parameters.AddWithValue("BUDGET_YEAR", budgetYear);
                        using (OracleDataReader reader = com.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                TableHeaderRow row = new TableHeaderRow();
                                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = reader.GetString(0); cell.Style["text-align"] = "left"; cell.Width = 250; row.Cells.Add(cell); }

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
                                if (reader.GetInt32(20) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(20); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                if (reader.GetInt32(21) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(21); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                if (reader.GetInt32(22) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(22); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                if (reader.GetInt32(23) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(23); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                if (reader.GetInt32(24) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(24); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                if (reader.GetInt32(25) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(25); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                table.Rows.Add(row);
                            }
                        }
                    }

                    using (OracleCommand com = new OracleCommand(
                    "SELECT " +
                        "FACULTY_NAME AAA, " +
                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10102 AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10097 AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10077 AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID = 10108 AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 1 AND PS_WORK_POS_ID IN(10102, 10097, 10077, 10108) AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +

                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10102 AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10097 AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10077 AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID = 10108 AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 2 AND PS_WORK_POS_ID IN(10102, 10097, 10077, 10108) AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +

                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10102 AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10097 AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10077 AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID = 10108 AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 5 AND PS_WORK_POS_ID IN(10102, 10097, 10077, 10108) AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +

                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10102 AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10097 AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10077 AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID = 10108 AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 4 AND PS_WORK_POS_ID IN(10102, 10097, 10077, 10108) AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1),  " +

                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10102 AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1)  ," +
                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10097 AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1)  ," +
                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10077 AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1)  ," +
                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID = 10108 AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1)  ," +
                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_WORK_POS_ID IN(10102, 10097, 10077, 10108) AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) " +

                        "FROM " +
                        "TB_FACULTY " +
                        "WHERE CAMPUS_ID = " + ddlCampus.SelectedValue, con))
                    {
                        int budgetYear = int.Parse(ddlYear.SelectedValue) - 543;
                        com.Parameters.AddWithValue("BUDGET_YEAR", budgetYear);
                        using (OracleDataReader reader = com.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                TableRow row = new TableRow();
                                { TableCell cell = new TableCell(); cell.Text = reader.GetString(0); cell.Style["text-align"] = "left"; cell.Width = 250; row.Cells.Add(cell); }

                                if (reader.GetInt32(1) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(1); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                if (reader.GetInt32(2) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(2); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                if (reader.GetInt32(3) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(3); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                if (reader.GetInt32(4) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(4); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                if (reader.GetInt32(5) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(5); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                if (reader.GetInt32(6) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(6); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                if (reader.GetInt32(7) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(7); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                if (reader.GetInt32(8) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(8); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                if (reader.GetInt32(9) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(9); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                if (reader.GetInt32(10) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(10); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                if (reader.GetInt32(11) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(11); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                if (reader.GetInt32(12) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(12); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                if (reader.GetInt32(13) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(13); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                if (reader.GetInt32(14) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(14); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                if (reader.GetInt32(15) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(15); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                if (reader.GetInt32(16) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(16); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                if (reader.GetInt32(17) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(17); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                if (reader.GetInt32(18) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(18); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                if (reader.GetInt32(19) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(19); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                if (reader.GetInt32(20) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(20); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                if (reader.GetInt32(21) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(21); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                if (reader.GetInt32(22) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(22); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                if (reader.GetInt32(23) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(23); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                if (reader.GetInt32(24) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(24); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                if (reader.GetInt32(25) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(25); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                table.Rows.Add(row);
                            }
                        }
                    }

                }

                Panel1.Controls.Clear();
                Panel1.Controls.Add(table);
                return table;
            }
        }
        //------------------------------------------------------------------
        private Table Bindจำนวนบุคลารสายวิชาการจำแนกตามประเภทบุคลากรคณะและวุฒิการศึกษา()
        {
            Table table = new Table();
            table.CssClass = "ps-table-1";

            {
                TableHeaderRow row = new TableHeaderRow();
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "สรุปข้อมูลจำนวนบุคลากรสายวิชาการ จำแนกตามประเภทบุคลากร คณะ และวุฒิการศึกษา ประจำปีงบประมาณ พ.ศ. " + ddlYear.SelectedValue; cell.Style["text-align"] = "center"; cell.ColumnSpan = 26; row.Cells.Add(cell); }
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

            if (ddlCampus.SelectedValue != "1" && ddlCampus.SelectedValue != "2" && ddlCampus.SelectedValue != "3" && ddlCampus.SelectedValue != "4")
            {

                using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
                {
                    con.Open();

                    List<int> list = new List<int>();
                    list.Add(1);
                    list.Add(2);
                    list.Add(3);
                    list.Add(4);

                    foreach (int campusID in list)
                    {
                        using (OracleCommand com = new OracleCommand(
                        "SELECT " +
                            "CAMPUS_NAME AAA, " +
                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 80 AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 60 AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 40 AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID IN(80, 60, 40) AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +

                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 80 AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 60 AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 40 AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID IN(80, 60, 40) AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +

                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 80 AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 60 AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 40 AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID IN(80, 60, 40) AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +

                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 80 AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 60 AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 40 AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID IN(80, 60, 40) AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1),  " +

                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 80 AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1)  ," +
                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 60 AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1)  ," +
                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 40 AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1)  ," +
                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID IN(80, 60, 40) AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) " +

                            "FROM " +
                            "TB_CAMPUS " +
                            "WHERE CAMPUS_ID = " + campusID, con))
                        {
                            int budgetYear = int.Parse(ddlYear.SelectedValue) - 543;
                            com.Parameters.AddWithValue("BUDGET_YEAR", budgetYear);
                            using (OracleDataReader reader = com.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    TableHeaderRow row = new TableHeaderRow();
                                    { TableHeaderCell cell = new TableHeaderCell(); cell.Text = reader.GetString(0); cell.Style["text-align"] = "left"; cell.Width = 250; row.Cells.Add(cell); }

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
                                    if (reader.GetInt32(20) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(20); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    table.Rows.Add(row);
                                }
                            }
                        }

                        using (OracleCommand com = new OracleCommand(
                        "SELECT " +
                            "FACULTY_NAME AAA, " +
                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 80 AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 60 AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 40 AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID IN(80, 60, 40) AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +

                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 80 AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 60 AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 40 AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID IN(80, 60, 40) AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +

                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 80 AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 60 AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 40 AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID IN(80, 60, 40) AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +

                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 80 AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 60 AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 40 AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID IN(80, 60, 40) AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1),  " +

                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 80 AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1)  ," +
                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 60 AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1)  ," +
                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 40 AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1)  ," +
                            "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID IN(80, 60, 40) AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) " +

                            "FROM " +
                            "TB_FACULTY " +
                            "WHERE CAMPUS_ID = " + campusID, con))
                        {
                            int budgetYear = int.Parse(ddlYear.SelectedValue) - 543;
                            com.Parameters.AddWithValue("BUDGET_YEAR", budgetYear);
                            using (OracleDataReader reader = com.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    TableRow row = new TableRow();
                                    { TableCell cell = new TableCell(); cell.Text = reader.GetString(0); cell.Style["text-align"] = "left"; cell.Width = 250; row.Cells.Add(cell); }

                                    if (reader.GetInt32(1) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(1); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(2) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(2); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(3) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(3); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(4) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(4); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(5) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(5); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(6) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(6); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(7) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(7); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(8) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(8); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(9) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(9); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(10) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(10); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(11) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(11); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(12) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(12); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(13) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(13); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(14) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(14); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(15) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(15); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    if (reader.GetInt32(16) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(16); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(17) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(17); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(18) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(18); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(19) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(19); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                    if (reader.GetInt32(20) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(20); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                    table.Rows.Add(row);
                                }
                            }
                        }
                    }

                }

                Panel1.Controls.Clear();
                Panel1.Controls.Add(table);
                return table;

            }
            else
            {
                //TableHeaderRow row = new TableHeaderRow();
                //{ TableHeaderCell cell = new TableHeaderCell(); cell.Text = "วิทยาเขตบางพระ"; cell.Style["text-align"] = "left"; cell.Width = 250; row.Cells.Add(cell); }
                using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
                {
                    con.Open();

                    using (OracleCommand com = new OracleCommand(
                    "SELECT " +
                        "CAMPUS_NAME AAA, " +
                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 80 AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 60 AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 40 AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID IN(80, 60, 40) AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +

                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 80 AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 60 AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 40 AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID IN(80, 60, 40) AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +

                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 80 AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 60 AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 40 AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID IN(80, 60, 40) AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +

                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 80 AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 60 AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 40 AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID IN(80, 60, 40) AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1),  " +

                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 80 AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1)  ," +
                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 60 AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1)  ," +
                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 40 AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1)  ," +
                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID IN(80, 60, 40) AND PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) " +

                        "FROM " +
                        "TB_CAMPUS " +
                        "WHERE CAMPUS_ID = " + ddlCampus.SelectedValue, con))
                    {
                        int budgetYear = int.Parse(ddlYear.SelectedValue) - 543;
                        com.Parameters.AddWithValue("BUDGET_YEAR", budgetYear);
                        using (OracleDataReader reader = com.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                TableHeaderRow row = new TableHeaderRow();
                                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = reader.GetString(0); cell.Style["text-align"] = "left"; cell.Width = 250; row.Cells.Add(cell); }

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
                                if (reader.GetInt32(20) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(20); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                table.Rows.Add(row);
                            }
                        }
                    }

                    using (OracleCommand com = new OracleCommand(
                    "SELECT " +
                        "FACULTY_NAME AAA, " +
                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 80 AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 60 AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID = 40 AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 1 AND PS_GRAD_LEV_ID IN(80, 60, 40) AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +

                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 80 AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 60 AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID = 40 AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 2 AND PS_GRAD_LEV_ID IN(80, 60, 40) AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +

                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 80 AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 60 AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID = 40 AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 5 AND PS_GRAD_LEV_ID IN(80, 60, 40) AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +

                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 80 AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 60 AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID = 40 AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) ," +
                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID = 4 AND PS_GRAD_LEV_ID IN(80, 60, 40) AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1),  " +

                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 80 AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1)  ," +
                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 60 AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1)  ," +
                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID = 40 AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1)  ," +
                        "(SELECT COUNT(*) FROM PS_PERSON WHERE PS_STAFFTYPE_ID IN(1,2,5,4) AND PS_GRAD_LEV_ID IN(80, 60, 40) AND PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID AND FUNC_PS_WORKING(PS_CITIZEN_ID, :BUDGET_YEAR) = 1) " +

                        "FROM " +
                        "TB_FACULTY " +
                        "WHERE CAMPUS_ID = " + ddlCampus.SelectedValue, con))
                    {
                        int budgetYear = int.Parse(ddlYear.SelectedValue) - 543;
                        com.Parameters.AddWithValue("BUDGET_YEAR", budgetYear);
                        using (OracleDataReader reader = com.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                TableRow row = new TableRow();
                                { TableCell cell = new TableCell(); cell.Text = reader.GetString(0); cell.Style["text-align"] = "left"; cell.Width = 250; row.Cells.Add(cell); }

                                if (reader.GetInt32(1) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(1); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                if (reader.GetInt32(2) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(2); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                if (reader.GetInt32(3) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(3); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                if (reader.GetInt32(4) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(4); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                if (reader.GetInt32(5) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(5); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                if (reader.GetInt32(6) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(6); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                if (reader.GetInt32(7) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(7); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                if (reader.GetInt32(8) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(8); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                if (reader.GetInt32(9) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(9); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                if (reader.GetInt32(10) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(10); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                if (reader.GetInt32(11) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(11); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                if (reader.GetInt32(12) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(12); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                if (reader.GetInt32(13) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(13); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                if (reader.GetInt32(14) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(14); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                if (reader.GetInt32(15) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(15); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                if (reader.GetInt32(16) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(16); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                if (reader.GetInt32(17) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(17); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                if (reader.GetInt32(18) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(18); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                if (reader.GetInt32(19) != 0) { { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(19); row.Cells.Add(cell); } } else { TableCell cell = new TableCell(); cell.Text = "-"; row.Cells.Add(cell); }
                                if (reader.GetInt32(20) != 0) { { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "" + reader.GetInt32(20); row.Cells.Add(cell); } } else { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "-"; row.Cells.Add(cell); }

                                table.Rows.Add(row);
                            }
                        }
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

                Response.AddHeader("Content-Disposition", "attachment;filename=สรุปข้อมูล" + ddlView.SelectedItem.Text + "ประจำปีงบประมาณ พ.ศ. " + ddlYear.SelectedValue + ".xls");
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

                Response.AddHeader("Content-Disposition", "attachment;filename=สรุปข้อมูล" + ddlView.SelectedItem.Text + "ประจำปีงบประมาณ พ.ศ. " + ddlYear.SelectedValue + ".xls");
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