using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OracleClient;
using System.IO;
using WEB_PERSONAL.Class;
using System.Text;

namespace WEB_PERSONAL {
    public partial class LeaveReport1 : System.Web.UI.Page {

        private Person loginPerson;
        protected void Page_Load(object sender, EventArgs e) {

            loginPerson = PersonnelSystem.GetPersonnelSystem(this).LoginPerson;

            if (!IsPostBack)
            {
                //int yearMin = DatabaseManager.ExecuteInt("select EXTRACT(YEAR FROM PS_INWORK_DATE)+543 from ps_person where ps_citizen_id = '" + loginPerson.PS_CITIZEN_ID + "'") + 543;
                //DateTime CurrentYear = DateTime.Today.AddYears(+543);
                int minDateInwork = DatabaseManager.ExecuteInt("SELECT EXTRACT(YEAR FROM PS_INWORK_DATE)+543 FROM PS_PERSON WHERE PS_CITIZEN_ID = '" + loginPerson.PS_CITIZEN_ID + "'");
                int currentYear = Util.BudgetYear();
                //int yearMin = DatabaseManager.ExecuteInt("SELECT MIN(EXTRACT(YEAR FROM FROM_DATE)) FROM LEV_DATA") + 543;
                //int yearMax = DatabaseManager.ExecuteInt("SELECT MAX(EXTRACT(YEAR FROM FROM_DATE)) FROM LEV_DATA") + 543;

                for (int i = minDateInwork; i <= currentYear; ++i)
                {
                    DropDownList1.Items.Add(new System.Web.UI.WebControls.ListItem("" + i, "" + i));
                }

                DropDownList1.SelectedValue = "" + (Util.BudgetYear() + 543);

                ddlView.Items.Add(new ListItem("แสดงทั้งหมด", "1"));
                ddlView.Items.Add(new ListItem("แสดงเฉพาะภายในวิทยาเขต", "2"));
                ddlView.Items.Add(new ListItem("แสดงเฉพาะภายใน สำนัก / สถาบัน / คณะ", "3"));
                ddlView.Items.Add(new ListItem("แสดงเฉพาะภายใน กอง / สำนักงานเลขา / ภาควิชา", "4"));
                ddlView.Items.Add(new ListItem("แสดงเฉพาะภายใน งาน / ฝ่าย", "5"));
                ddlView.Items.Add(new ListItem("แสดงเฉพาะตนเอง", "6"));

                ddlSelfView.Items.Add(new ListItem("แสดงการลาทั้งหมด", "1"));
                ddlSelfView.Items.Add(new ListItem("แสดงตามรหัสการลา", "2"));

            }
        }  

        private Table BindToTable() {

            //Label1.Text = "(1 ตุลาคม " + (int.Parse(DropDownList1.SelectedValue) - 1) + " - 30 กันยายน " + DropDownList1.SelectedValue + ")";
            Table table = new Table();
            table.CssClass = "ps-table-1";
            {
                TableHeaderRow row = new TableHeaderRow();
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "สรุปข้อมูลการลาหยุดราชการ ขาดราชการ มาสาย ประจำปีงบประมาณ ประจำปีงบประมาณ พ.ศ. " + DropDownList1.SelectedValue; cell.ColumnSpan = 14; row.Cells.Add(cell); }
                table.Rows.Add(row);
            }
            {
                TableHeaderRow row = new TableHeaderRow();
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "มหาวิทยาลัยเทคโนโลยีราชมงคลตะวันออก"; cell.ColumnSpan = 14; row.Cells.Add(cell); }
                table.Rows.Add(row);
            }

            TableHeaderRow rowXT = new TableHeaderRow();
            TableHeaderCell cellXT = new TableHeaderCell();
            {
                {  cellXT.Text = ""; cellXT.ColumnSpan = 14; rowXT.Cells.Add(cellXT); }
                table.Rows.Add(rowXT);

            }
            {
                TableHeaderRow row = new TableHeaderRow();
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = ""; cell.ColumnSpan = 3; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "ลาป่วย"; cell.ColumnSpan = 2; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "ลากิจ"; cell.ColumnSpan = 2; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "ลาพักผ่อน"; cell.ColumnSpan = 2; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = ""; cell.ColumnSpan = 5; row.Cells.Add(cell); }
                table.Rows.Add(row);
            }

            {
                TableHeaderRow row = new TableHeaderRow();
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "ลำดับที่"; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "ชื่อ - นามสกุล"; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "ตำแหน่ง"; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "ครั้ง"; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "วัน"; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "ครั้ง"; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "วัน"; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "ครั้ง"; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "วัน"; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "มาสาย(ครั้ง)"; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "ขาดราชการ(วัน)"; row.Cells.Add(cell); }
                //{ TableHeaderCell cell = new TableHeaderCell(); cell.Text = "ลาศึกษาต่อ(ระบุวันที่ลา)"; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "ลาคลอดบุตร(ระบุวันที่ลา)"; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "ลาอุปสมบทฯ(ระบุวันที่ลา)"; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "หมายเหตุ"; row.Cells.Add(cell); }
                table.Rows.Add(row);
            }

            List<string> persons = new List<string>();
            OracleConnection.ClearAllPools();
            using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING)) {
                con.Open();

                string sql;
                if (ddlView.SelectedValue == "1") {
                    sql = "SELECT PS_CITIZEN_ID FROM PS_PERSON ORDER BY PS_CITIZEN_ID ASC";
                    table.Rows.Remove(rowXT);
                } else if (ddlView.SelectedValue == "2") {
                    sql = "SELECT PS_CITIZEN_ID FROM PS_PERSON WHERE PS_CAMPUS_ID = " + loginPerson.PS_CAMPUS_ID + " ORDER BY PS_CITIZEN_ID ASC";
                    cellXT.Text = loginPerson.PS_CAMPUS_NAME;
                } else if (ddlView.SelectedValue == "3") {
                    sql = "SELECT PS_CITIZEN_ID FROM PS_PERSON WHERE PS_FACULTY_ID = " + loginPerson.PS_FACULTY_ID + " ORDER BY PS_CITIZEN_ID ASC";
                    cellXT.Text = loginPerson.PS_FACULTY_NAME;
                } else if (ddlView.SelectedValue == "4") {
                    sql = "SELECT PS_CITIZEN_ID FROM PS_PERSON WHERE PS_DIVISION_ID = " + loginPerson.PS_DIVISION_ID + " ORDER BY PS_CITIZEN_ID ASC";
                    cellXT.Text = loginPerson.PS_DIVISION_NAME;
                } else if (ddlView.SelectedValue == "5") {
                    sql = "SELECT PS_CITIZEN_ID FROM PS_PERSON WHERE PS_WORK_DIVISION_ID = " + loginPerson.PS_WORK_DIVISION_ID + " ORDER BY PS_CITIZEN_ID ASC";
                    cellXT.Text = loginPerson.PS_WORK_DIVISION_NAME;
                } else {
                    sql = "SELECT PS_CITIZEN_ID FROM PS_PERSON WHERE PS_CITIZEN_ID = " + loginPerson.PS_CITIZEN_ID + " ORDER BY PS_CITIZEN_ID ASC";
                    table.Rows.Remove(rowXT);
                }

                using (OracleCommand com = new OracleCommand(sql, con)) {
                    using (OracleDataReader reader = com.ExecuteReader()) {
                        while (reader.Read()) {
                            //Person ps = DatabaseManager.GetPerson(reader.GetString(0));
                            //persons.Add(ps);
                            persons.Add(reader.GetString(0));
                        }
                    }
                }

                int budgetYear = int.Parse(DropDownList1.SelectedValue) - 543;

                for (int i = 0; i < persons.Count; i++) {
                    //Person ps = persons[i];
                    string citizenID = persons[i];
                    TableRow row = new TableRow();
                    {
                        TableCell cell = new TableCell();
                        cell.Text = "" + (i + 1);
                        row.Cells.Add(cell);
                    }
                    {
                        TableCell cell = new TableCell();
                        //cell.Text = ps.FirstNameAndLastName;
                        using (OracleCommand com = new OracleCommand("SELECT PS_FIRSTNAME || ' ' || PS_LASTNAME FROM PS_PERSON WHERE PS_CITIZEN_ID = '" + citizenID + "'", con)) {
                            using (OracleDataReader reader = com.ExecuteReader()) {
                                while (reader.Read()) {
                                    cell.Text = reader.GetString(0);
                                }
                            }
                        }
                        row.Cells.Add(cell);
                    }
                    {
                        TableCell cell = new TableCell();
                        //cell.Text = ps.PositionName;
                        using (OracleCommand com = new OracleCommand("SELECT TB_POSITION_WORK.POSITION_WORK_NAME FROM PS_PERSON, TB_POSITION_WORK WHERE PS_CITIZEN_ID = '" + citizenID + "' AND PS_WORK_POS_ID = TB_POSITION_WORK.POSITION_WORK_ID", con)) {
                            using (OracleDataReader reader = com.ExecuteReader()) {
                                while (reader.Read()) {
                                    cell.Text = reader.GetString(0);
                                }
                            }
                        }
                        row.Cells.Add(cell);
                    }



                    {
                        
                        using (OracleCommand com = new OracleCommand(
                            "SELECT" +
                            " (SELECT NVL(COUNT(LEAVE_ID),0) FROM LEV_DATA WHERE LEAVE_STATUS_ID IN(2,3,4) AND LEAVE_TYPE_ID = 1 AND PS_ID = '" + citizenID + "' AND BUDGET_YEAR = " + budgetYear + " AND V_ALLOW = 1 )" +
                            ", (SELECT NVL(SUM(TOTAL_DAY),0) FROM LEV_DATA WHERE LEAVE_STATUS_ID IN(2,3,4) AND LEAVE_TYPE_ID = 1 AND PS_ID = '" + citizenID + "' AND BUDGET_YEAR = " + budgetYear + " AND V_ALLOW = 1 )" +
                            ", (SELECT NVL(COUNT(LEAVE_ID),0) FROM LEV_DATA WHERE LEAVE_STATUS_ID IN(2,3,4) AND LEAVE_TYPE_ID = 2 AND PS_ID = '" + citizenID + "' AND BUDGET_YEAR = " + budgetYear + " AND V_ALLOW = 1 )" +
                            ", (SELECT NVL(SUM(TOTAL_DAY),0) FROM LEV_DATA WHERE LEAVE_STATUS_ID IN(2,3,4) AND LEAVE_TYPE_ID = 2 AND PS_ID = '" + citizenID + "' AND BUDGET_YEAR = " + budgetYear + " AND V_ALLOW = 1 )" +
                            ", (SELECT NVL(COUNT(LEAVE_ID),0) FROM LEV_DATA WHERE LEAVE_STATUS_ID IN(2,3,4) AND LEAVE_TYPE_ID = 4 AND PS_ID = '" + citizenID + "' AND BUDGET_YEAR = " + budgetYear + " AND V_ALLOW = 1 )" +
                            ", (SELECT NVL(SUM(TOTAL_DAY),0) FROM LEV_DATA WHERE LEAVE_STATUS_ID IN(2,3,4) AND LEAVE_TYPE_ID = 4 AND PS_ID = '" + citizenID + "' AND BUDGET_YEAR = " + budgetYear + " AND V_ALLOW = 1 )" +
                            ", (SELECT NVL(COUNT(WORKTIME_ID),0) FROM LEV_WORKTIME WHERE LATE = 1 AND CITIZEN_ID = '" + citizenID + "' AND TODAY > TO_DATE('30-09-" + (budgetYear - 1) + "', 'DD-MM-YYYY') AND TODAY < TO_DATE('01-10-" + (budgetYear) + "', 'DD-MM-YYYY')" + ")" +
                            ", (SELECT NVL(COUNT(WORKTIME_ID),0) FROM LEV_WORKTIME WHERE ABSENT = 1 AND CITIZEN_ID = '" + citizenID + "' AND TODAY > TO_DATE('30-09-" + (budgetYear - 1) + "', 'DD-MM-YYYY') AND TODAY < TO_DATE('01-10-" + (budgetYear) + "', 'DD-MM-YYYY')" + ")" +
                            " FROM DUAL",
                            con)) {
                            using (OracleDataReader reader = com.ExecuteReader()) {
                                while (reader.Read()) {

                                    TableCell cell;
                                    cell = new TableCell(); cell.Text = "" + reader.GetInt32(0); row.Cells.Add(cell);
                                    cell = new TableCell(); cell.Text = "" + reader.GetInt32(1); row.Cells.Add(cell);
                                    cell = new TableCell(); cell.Text = "" + reader.GetInt32(2); row.Cells.Add(cell);
                                    cell = new TableCell(); cell.Text = "" + reader.GetInt32(3); row.Cells.Add(cell);
                                    cell = new TableCell(); cell.Text = "" + reader.GetInt32(4); row.Cells.Add(cell);
                                    cell = new TableCell(); cell.Text = "" + reader.GetInt32(5); row.Cells.Add(cell);
                                    cell = new TableCell(); cell.Text = "" + reader.GetInt32(6); row.Cells.Add(cell);
                                    cell = new TableCell(); cell.Text = "" + reader.GetInt32(7); row.Cells.Add(cell);
                                }
                            }
                        }
                        
                    }



                    {
                        TableCell cell = new TableCell();
                        cell.Text = "";
                        using (OracleCommand command = new OracleCommand("SELECT FROM_DATE FROM LEV_DATA WHERE PS_ID = '" + citizenID + "' AND LEAVE_TYPE_ID = 3 AND BUDGET_YEAR = " + budgetYear + " AND V_ALLOW = 1 ORDER BY LEAVE_ID DESC", con)) {
                            using (OracleDataReader reader = command.ExecuteReader()) {
                                if (reader.Read()) {
                                    cell.Text = reader.GetDateTime(0).ToLongDateString();
                                }
                            }
                        }
                        row.Cells.Add(cell);
                    }
                    {
                        TableCell cell = new TableCell();
                        cell.Text = "";
                        using (OracleCommand command = new OracleCommand("SELECT FROM_DATE FROM LEV_DATA WHERE PS_ID = '" + citizenID + "' AND LEAVE_TYPE_ID = 6 AND BUDGET_YEAR = " + budgetYear + " AND V_ALLOW = 1 ORDER BY LEAVE_ID DESC", con)) {
                            using (OracleDataReader reader = command.ExecuteReader()) {
                                if (reader.Read()) {
                                    cell.Text = reader.GetDateTime(0).ToLongDateString();
                                }
                            }
                        }
                        row.Cells.Add(cell);
                    }

                    {
                        TableCell cell = new TableCell();
                        cell.Text = "";
                        row.Cells.Add(cell);
                    }
                    table.Rows.Add(row);


                }

            }

            if (persons.Count > 0) {
                return table;
            } else {
                return null;
            }


        }


        private Table BindToTableOnlyMe() {

            //Label1.Text = "(1 ตุลาคม " + (int.Parse(DropDownList1.SelectedValue) - 1) + " - 30 กันยายน " + DropDownList1.SelectedValue + ")";

            Table table = new Table();
            table.CssClass = "ps-table-1";

            int budgetYear = int.Parse(DropDownList1.SelectedValue) - 543;

            OracleConnection.ClearAllPools();
            using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING)) {
                con.Open();

                const int maxCol = 9;

                {
                    TableHeaderRow row = new TableHeaderRow();
                    { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "สรุปข้อมูลการลาหยุดราชการ ขาดราชการ มาสาย ประจำปีงบประมาณ ประจำปีงบประมาณ พ.ศ. " + DropDownList1.SelectedValue; cell.ColumnSpan = maxCol; row.Cells.Add(cell); }
                    table.Rows.Add(row);
                }
                {
                    TableHeaderRow row = new TableHeaderRow();
                    { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "มหาวิทยาลัยเทคโนโลยีราชมงคลตะวันออก"; cell.ColumnSpan = maxCol; row.Cells.Add(cell); }
                    table.Rows.Add(row);
                }

                TableHeaderRow rowXT = new TableHeaderRow();
                TableHeaderCell cellXT = new TableHeaderCell();
                {

                    { cellXT.Text = loginPerson.FirstNameAndLastName; cellXT.ColumnSpan = maxCol; rowXT.Cells.Add(cellXT); }
                    table.Rows.Add(rowXT);

                }

                {
                    TableHeaderRow row = new TableHeaderRow();
                    { TableHeaderCell cell = new TableHeaderCell(); cell.Text = ""; cell.ColumnSpan = maxCol; row.Cells.Add(cell); }
                    table.Rows.Add(row);
                }

                {
                    TableHeaderRow row = new TableHeaderRow();
                    { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "สถิติการลา"; cell.ColumnSpan = maxCol; row.Cells.Add(cell); }
                    table.Rows.Add(row);
                }

                {
                    int leaveCount = 0;
                    int totalDay = 0;
                    using (OracleCommand com = new OracleCommand("SELECT COUNT(LEAVE_ID), NVL(SUM(TOTAL_DAY),0) FROM LEV_DATA WHERE LEAVE_STATUS_ID IN(2,3) AND V_ALLOW = 1 AND PS_ID = '" + loginPerson.PS_CITIZEN_ID + "' AND BUDGET_YEAR = " + budgetYear + " ORDER BY LEAVE_ID ASC", con)) {
                        using (OracleDataReader reader = com.ExecuteReader()) {
                            while (reader.Read()) {
                                leaveCount = reader.GetInt32(0);
                                totalDay = reader.GetInt32(1);
                            }
                        }
                    }

                    {
                        TableRow row = new TableRow();
                        { TableCell cell = new TableCell(); cell.Text = "ลาทั้งหมด : <b>" + leaveCount + "</b> ครั้ง"; cell.ColumnSpan = maxCol; row.Cells.Add(cell); }
                        table.Rows.Add(row);
                    }

                    {
                        TableRow row = new TableRow();
                        { TableCell cell = new TableCell(); cell.Text = "รวมจำนวนวันลา : <b>" + totalDay + "</b> วัน"; cell.ColumnSpan = maxCol; row.Cells.Add(cell); }
                        table.Rows.Add(row);
                    }


                }

                {
                    TableHeaderRow row = new TableHeaderRow();
                    { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "ลาป่วย"; row.Cells.Add(cell); }
                    { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "ลากิจ"; row.Cells.Add(cell); }
                    { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "ลาคลอดบุตร"; row.Cells.Add(cell); }
                    { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "ลาพักผ่อน"; row.Cells.Add(cell); }
                    { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "ลาไปช่วยเหลือภริยาที่คลอดบุตร"; row.Cells.Add(cell); }
                    { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "ลาไปอุปสมบท"; row.Cells.Add(cell); }
                    { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "ลาไปประกอบพิธีฮัจย์"; cell.ColumnSpan = 3; row.Cells.Add(cell); }
                    table.Rows.Add(row);
                }

                {
                    int sickNow = -1;
                    int sickMax = -1;
                    int businessNow = -1;
                    int businessMax = -1;
                    int gbNow = -1;
                    int restNow = -1;
                    int restMax = -1;
                    int hgbNow = -1;
                    int ordainNow = -1;
                    int ordainMax = -1;
                    int hujNow = -1;
                    int hujMax = -1;
                    using (OracleCommand com = new OracleCommand("SELECT SICK_NOW, SICK_MAX, BUSINESS_NOW, BUSINESS_MAX, GB_NOW, REST_NOW, REST_MAX, HGB_NOW, ORDAIN_NOW, ORDAIN_MAX, HUJ_NOW, HUJ_MAX FROM LEV_CLAIM WHERE PS_CITIZEN_ID = '" + loginPerson.PS_CITIZEN_ID + "' AND YEAR = " + budgetYear, con)) {
                        using (OracleDataReader reader = com.ExecuteReader()) {
                            while (reader.Read()) {
                                sickNow = reader.GetInt32(0);
                                sickMax = reader.GetInt32(1);
                                businessNow = reader.GetInt32(2);
                                businessMax = reader.GetInt32(3);
                                gbNow = reader.GetInt32(4);
                                restNow = reader.GetInt32(5);
                                restMax = reader.GetInt32(6);
                                hgbNow = reader.GetInt32(7);
                                ordainNow = reader.GetInt32(8);
                                ordainMax = reader.GetInt32(9);
                                hujNow = reader.GetInt32(10);
                                hujMax = reader.GetInt32(11);
                            }
                        }
                    }

                    {
                        TableRow row = new TableRow();
                        { TableCell cell = new TableCell(); cell.Text = "<b>" + sickNow + "</b> / " + sickMax; row.Cells.Add(cell); }
                        { TableCell cell = new TableCell(); cell.Text = "<b>" + businessNow + "</b> / " + businessMax; row.Cells.Add(cell); }
                        { TableCell cell = new TableCell(); cell.Text = "<b>" + gbNow; row.Cells.Add(cell); }
                        { TableCell cell = new TableCell(); cell.Text = "<b>" + restNow + "</b> / " + restMax; row.Cells.Add(cell); }
                        { TableCell cell = new TableCell(); cell.Text = "<b>" + hgbNow; row.Cells.Add(cell); }
                        { TableCell cell = new TableCell(); cell.Text = "<b>" + ordainNow + "</b> / " + ordainMax; row.Cells.Add(cell); }
                        { TableCell cell = new TableCell(); cell.Text = "<b>" + hujNow + "</b> / " + hujMax; cell.ColumnSpan = 3; row.Cells.Add(cell); }
                        table.Rows.Add(row);
                    }

                }

                {
                    TableHeaderRow row = new TableHeaderRow();
                    { TableHeaderCell cell = new TableHeaderCell(); cell.Text = ""; cell.ColumnSpan = maxCol; row.Cells.Add(cell); }
                    table.Rows.Add(row);
                }

                {
                    TableHeaderRow row = new TableHeaderRow();
                    { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "ข้อมูลการลา"; cell.ColumnSpan = maxCol; row.Cells.Add(cell); }
                    table.Rows.Add(row);
                }

                

                {
                    TableHeaderRow row = new TableHeaderRow();
                    { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "ลำดับที่"; row.Cells.Add(cell); }
                    { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "รหัสการลา"; row.Cells.Add(cell); }
                    { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "ประเภทการลา"; row.Cells.Add(cell); }
                    { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "วันที่ข้อมูล"; row.Cells.Add(cell); }
                    { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "จากวันที่"; row.Cells.Add(cell); }
                    { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "ถึงวันที่"; row.Cells.Add(cell); }
                    { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "รวม"; row.Cells.Add(cell); }
                    { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "สถานะ"; row.Cells.Add(cell); }
                    { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "ผลการอนุมัติ"; row.Cells.Add(cell); }
                    table.Rows.Add(row);
                }

                string sql = "SELECT LEAVE_ID, (SELECT LEAVE_TYPE_NAME FROM LEV_TYPE WHERE LEV_TYPE.LEAVE_TYPE_ID = LEV_DATA.LEAVE_TYPE_ID) LEAVE_TYPE_NAME, REQ_DATE, FROM_DATE, TO_DATE, TOTAL_DAY, (SELECT LEAVE_STATUS_NAME FROM LEV_STATUS WHERE LEV_STATUS.LEAVE_STATUS_ID = LEV_DATA.LEAVE_STATUS_ID) LEAVE_STATUS_NAME, NVL((SELECT LEAVE_ALLOW_NAME FROM LEV_ALLOW WHERE LEV_ALLOW.LEAVE_ALLOW_ID = LEV_DATA.V_ALLOW), '-') LEAVE_ALLOW_NAME FROM LEV_DATA WHERE PS_ID = '" + loginPerson.PS_CITIZEN_ID + "' AND BUDGET_YEAR = " + budgetYear + " ORDER BY LEAVE_ID ASC";
                using (OracleCommand com = new OracleCommand(sql, con)) {
                    using (OracleDataReader reader = com.ExecuteReader()) {
                        int i = 1;
                        while (reader.Read()) {
                            TableRow row = new TableRow();
                            { TableCell cell = new TableCell(); cell.Text = "" + i++; row.Cells.Add(cell); }
                            { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(0); row.Cells.Add(cell); }
                            { TableCell cell = new TableCell(); cell.Text = "" + reader.GetString(1); row.Cells.Add(cell); }
                            { TableCell cell = new TableCell(); cell.Text = "" + reader.GetDateTime(2).ToLongDateString(); row.Cells.Add(cell); }
                            { TableCell cell = new TableCell(); cell.Text = "" + reader.GetDateTime(3).ToLongDateString(); row.Cells.Add(cell); }
                            { TableCell cell = new TableCell(); cell.Text = "" + reader.GetDateTime(4).ToLongDateString(); row.Cells.Add(cell); }
                            { TableCell cell = new TableCell(); cell.Text = "" + reader.GetInt32(5); row.Cells.Add(cell); }
                            { TableCell cell = new TableCell(); cell.Text = "" + reader.GetString(6); row.Cells.Add(cell); }
                            { TableCell cell = new TableCell(); cell.Text = "" + reader.GetString(7); row.Cells.Add(cell); }
                            table.Rows.Add(row);
                        }
                    }
                } 

            }

        
             return table;



        }

        private Table BindToTableLeaveID(int leaveID) {

            //Label1.Text = "(1 ตุลาคม " + (int.Parse(DropDownList1.SelectedValue) - 1) + " - 30 กันยายน " + DropDownList1.SelectedValue + ")";

            Table table = new Table();
            table.CssClass = "ps-table-1";

            int budgetYear = int.Parse(DropDownList1.SelectedValue) - 543;

            OracleConnection.ClearAllPools();
            using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING)) {
                con.Open();

                const int maxCol = 2;

                {
                    TableHeaderRow row = new TableHeaderRow();
                    { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "สรุปข้อมูลการลาหยุดราชการ ขาดราชการ มาสาย ประจำปีงบประมาณ ประจำปีงบประมาณ พ.ศ. " + DropDownList1.SelectedValue; cell.ColumnSpan = maxCol; row.Cells.Add(cell); }
                    table.Rows.Add(row);
                }
                {
                    TableHeaderRow row = new TableHeaderRow();
                    { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "มหาวิทยาลัยเทคโนโลยีราชมงคลตะวันออก"; cell.ColumnSpan = maxCol; row.Cells.Add(cell); }
                    table.Rows.Add(row);
                }

                TableHeaderRow rowXT = new TableHeaderRow();
                TableHeaderCell cellXT = new TableHeaderCell();
                {

                    { cellXT.Text = loginPerson.FirstNameAndLastName; cellXT.ColumnSpan = maxCol; rowXT.Cells.Add(cellXT); }
                    table.Rows.Add(rowXT);

                }

                {
                    TableHeaderRow row = new TableHeaderRow();
                    { TableHeaderCell cell = new TableHeaderCell(); cell.Text = ""; cell.ColumnSpan = maxCol; row.Cells.Add(cell); }
                    table.Rows.Add(row);
                }

                {
                    TableHeaderRow row = new TableHeaderRow();
                    { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "ข้อมูลการลา"; cell.ColumnSpan = maxCol; row.Cells.Add(cell); }
                    table.Rows.Add(row);
                }

                LeaveData leaveData = new LeaveData();
                leaveData.Load(leaveID);

                {
                    TableRow row = new TableRow();
                    { TableCell cell = new TableCell(); cell.Text = "รหัสการลา"; row.Cells.Add(cell); }
                    { TableCell cell = new TableCell(); cell.Text = "" + leaveData.LeaveID; row.Cells.Add(cell); }
                    table.Rows.Add(row);
                }

                {
                    TableRow row = new TableRow();
                    { TableCell cell = new TableCell(); cell.Text = "สถานะการลา"; row.Cells.Add(cell); }
                    { TableCell cell = new TableCell(); cell.Text = "" + leaveData.LeaveStatusName; row.Cells.Add(cell); }
                    table.Rows.Add(row);
                }

                {
                    TableRow row = new TableRow();
                    { TableCell cell = new TableCell(); cell.Text = "ประเภทการลา"; row.Cells.Add(cell); }
                    { TableCell cell = new TableCell(); cell.Text = "" + leaveData.LeaveTypeName; row.Cells.Add(cell); }
                    table.Rows.Add(row);
                }

                {
                    TableRow row = new TableRow();
                    { TableCell cell = new TableCell(); cell.Text = "วันที่ข้อมูล"; row.Cells.Add(cell); }
                    { TableCell cell = new TableCell(); cell.Text = "" + leaveData.RequestDate.Value.ToLongDateString(); row.Cells.Add(cell); }
                    table.Rows.Add(row);
                }

                {
                    TableRow row = new TableRow();
                    { TableCell cell = new TableCell(); cell.Text = "ชื่อผู้ลา"; row.Cells.Add(cell); }
                    { TableCell cell = new TableCell(); cell.Text = "" + leaveData.Person.FirstNameAndLastName; row.Cells.Add(cell); }
                    table.Rows.Add(row);
                }

                {
                    TableRow row = new TableRow();
                    { TableCell cell = new TableCell(); cell.Text = "ตำแหน่ง"; row.Cells.Add(cell); }
                    { TableCell cell = new TableCell(); cell.Text = "" + leaveData.Person.PS_WORK_POS_NAME; row.Cells.Add(cell); }
                    table.Rows.Add(row);
                }

                {
                    TableRow row = new TableRow();
                    { TableCell cell = new TableCell(); cell.Text = "ระดับ"; row.Cells.Add(cell); }
                    { TableCell cell = new TableCell(); cell.Text = "" + leaveData.Person.PS_ADMIN_POS_NAME + leaveData.Person.AdminPositionNameExtra() == "ไม่มี" ? "" : leaveData.Person.AdminPositionNameExtra(); row.Cells.Add(cell); }
                    table.Rows.Add(row);
                }

                {
                    TableRow row = new TableRow();
                    { TableCell cell = new TableCell(); cell.Text = "สังกัด"; row.Cells.Add(cell); }
                    { TableCell cell = new TableCell(); cell.Text = Util.IsBlank(leaveData.Person.PS_WORK_DIVISION_NAME) ? leaveData.Person.PS_DIVISION_NAME : leaveData.Person.PS_WORK_DIVISION_NAME; row.Cells.Add(cell); }
                    table.Rows.Add(row);
                }

                if (leaveData.LeaveTypeID == 4) {
                    {
                        TableRow row = new TableRow();
                        { TableCell cell = new TableCell(); cell.Text = "วันลาพักผ่อนสะสม"; row.Cells.Add(cell); }
                        { TableCell cell = new TableCell(); cell.Text = leaveData.RestSave + " วัน"; row.Cells.Add(cell); }
                        table.Rows.Add(row);
                    }

                    {
                        TableRow row = new TableRow();
                        { TableCell cell = new TableCell(); cell.Text = "มีสิทธิลาประจำปีนี้อีก"; row.Cells.Add(cell); }
                        { TableCell cell = new TableCell(); cell.Text = leaveData.RestLeft + " วัน"; row.Cells.Add(cell); }
                        table.Rows.Add(row);
                    }

                    {
                        TableRow row = new TableRow();
                        { TableCell cell = new TableCell(); cell.Text = "รวม"; row.Cells.Add(cell); }
                        { TableCell cell = new TableCell(); cell.Text = leaveData.RestTotal + " วัน"; row.Cells.Add(cell); }
                        table.Rows.Add(row);
                    }
                }

                if (leaveData.LeaveTypeID == 6) {
                    {
                        TableRow row = new TableRow();
                        { TableCell cell = new TableCell(); cell.Text = "การอุปสมบท"; row.Cells.Add(cell); }
                        { TableCell cell = new TableCell(); cell.Text = leaveData.Ordained == 1 ? "เคย" : "ไม่เคย"; row.Cells.Add(cell); }
                        table.Rows.Add(row);
                    }

                    {
                        TableRow row = new TableRow();
                        { TableCell cell = new TableCell(); cell.Text = "ชื่อวัด"; row.Cells.Add(cell); }
                        { TableCell cell = new TableCell(); cell.Text = leaveData.TempleName; row.Cells.Add(cell); }
                        table.Rows.Add(row);
                    }

                    {
                        TableRow row = new TableRow();
                        { TableCell cell = new TableCell(); cell.Text = "สถานที่"; row.Cells.Add(cell); }
                        { TableCell cell = new TableCell(); cell.Text = leaveData.TempleLocation; row.Cells.Add(cell); }
                        table.Rows.Add(row);
                    }

                    {
                        TableRow row = new TableRow();
                        { TableCell cell = new TableCell(); cell.Text = "อุปสมบทวันที่"; row.Cells.Add(cell); }
                        { TableCell cell = new TableCell(); cell.Text = leaveData.OrdainDate.Value.ToLongDateString(); row.Cells.Add(cell); }
                        table.Rows.Add(row);
                    }
                }

                {
                    TableRow row = new TableRow();
                    { TableCell cell = new TableCell(); cell.Text = "วันที่ลาล่าสุด"; row.Cells.Add(cell); }
                    { TableCell cell = new TableCell(); cell.Text = leaveData.LastFromDate.HasValue ? leaveData.LastFromDate.Value.ToLongDateString() + " ถึง " + leaveData.LastToDate.Value.ToLongDateString() + " รวม " + leaveData.LastTotalDay + " วัน " : "ยังไม่เคยลา"; row.Cells.Add(cell); }
                    table.Rows.Add(row);
                }

                {
                    TableRow row = new TableRow();
                    { TableCell cell = new TableCell(); cell.Text = "วันที่ลา"; row.Cells.Add(cell); }
                    { TableCell cell = new TableCell(); cell.Text = leaveData.FromDate.Value.ToLongDateString() + " ถึง " + leaveData.ToDate.Value.ToLongDateString() + " รวม " + leaveData.TotalDay + " วัน "; row.Cells.Add(cell); }
                    table.Rows.Add(row);
                }

                {
                    TableRow row = new TableRow();
                    { TableCell cell = new TableCell(); cell.Text = "สถิติการลา"; row.Cells.Add(cell); }
                    { TableCell cell = new TableCell(); cell.Text = "ลามาแล้ว " + leaveData.CountPast + " วัน / รวมครั้งนี้ " + leaveData.CountNow + " วัน / รวม " + leaveData.CountTotal + " วัน "; row.Cells.Add(cell); }
                    table.Rows.Add(row);
                }

                

                if(leaveData.LeaveTypeID == 1 || leaveData.LeaveTypeID == 2 || leaveData.LeaveTypeID == 3) {
                    TableRow row = new TableRow();
                    { TableCell cell = new TableCell(); cell.Text = "เหตุผล"; row.Cells.Add(cell); }
                    { TableCell cell = new TableCell(); cell.Text = leaveData.Reason; row.Cells.Add(cell); }
                    table.Rows.Add(row);
                }

                if (leaveData.LeaveStatusID >= 4 && leaveData.LeaveStatusID <= 6){
                    TableRow row = new TableRow();
                    { TableCell cell = new TableCell(); cell.Text = "เหตุผลที่ยกเลิก"; row.Cells.Add(cell); }
                    { TableCell cell = new TableCell(); cell.Text = leaveData.CancelReason; row.Cells.Add(cell); }
                    table.Rows.Add(row);
                }

                {
                    TableRow row = new TableRow();
                    { TableCell cell = new TableCell(); cell.Text = "ติดต่อได้ที่"; row.Cells.Add(cell); }
                    { TableCell cell = new TableCell(); cell.Text = leaveData.Contact; row.Cells.Add(cell); }
                    table.Rows.Add(row);
                }

                {
                    TableRow row = new TableRow();
                    { TableCell cell = new TableCell(); cell.Text = "เบอร์โทรศัพท์"; row.Cells.Add(cell); }
                    { TableCell cell = new TableCell(); cell.Text = "'" + leaveData.Telephone; row.Cells.Add(cell); }
                    table.Rows.Add(row);
                }

                {
                    TableHeaderRow row = new TableHeaderRow();
                    { TableHeaderCell cell = new TableHeaderCell(); cell.Text = ""; cell.ColumnSpan = maxCol; row.Cells.Add(cell); }
                    table.Rows.Add(row);
                }

                {
                    TableHeaderRow row = new TableHeaderRow();
                    { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "ผู้อนุมัติการลา"; cell.ColumnSpan = maxCol; row.Cells.Add(cell); }
                    table.Rows.Add(row);
                }

                List<LeaveBossData> leaveBossDataList = leaveData.LeaveBossDataList;
                for (int i = 0; i < leaveBossDataList.Count; i++) {
                    LeaveBossData leaveBossData = leaveBossDataList[i];
                    {
                        TableRow row = new TableRow();
                        { TableCell cell = new TableCell(); cell.Text = "ชื่อ"; row.Cells.Add(cell); }
                        { TableCell cell = new TableCell(); cell.Text = leaveBossData.Person.FirstNameAndLastName; row.Cells.Add(cell); }
                        table.Rows.Add(row);
                    }
                    {
                        TableRow row = new TableRow();
                        { TableCell cell = new TableCell(); cell.Text = "ตำแหน่ง"; row.Cells.Add(cell); }
                        { TableCell cell = new TableCell(); cell.Text = leaveBossData.Person.PS_WORK_POS_NAME; row.Cells.Add(cell); }
                        table.Rows.Add(row);
                    }
                    {
                        TableRow row = new TableRow();
                        { TableCell cell = new TableCell(); cell.Text = "ระดับ"; row.Cells.Add(cell); }
                        { TableCell cell = new TableCell(); cell.Text = leaveBossData.Person.PS_ADMIN_POS_NAME; row.Cells.Add(cell); }
                        table.Rows.Add(row);
                    }
                    {
                        TableRow row = new TableRow();
                        { TableCell cell = new TableCell(); cell.Text = "การอนุมัติ"; row.Cells.Add(cell); }
                        string sss = "";
                        sss += "<div>" + leaveBossData.AllowDate.Value.ToLongDateString() + "</div>";
                        if(leaveBossData.Allow == 1) {
                            sss += "<div style='color:green;'>อนุญาต</div>";
                        } else {
                            sss += "<div style='color:red;'>ไม่อนุญาต</div>";
                        }
                        sss += "<div>" + leaveBossData.Comment + "</div>";
                        { TableCell cell = new TableCell(); cell.Text = sss; row.Cells.Add(cell); }
                        table.Rows.Add(row);
                    }

                    if (leaveData.LeaveStatusID >= 4 && leaveData.LeaveStatusID <= 6) {
                        {
                            TableRow row = new TableRow();
                            { TableCell cell = new TableCell(); cell.Text = "การอนุมัติยกเลิก"; row.Cells.Add(cell); }
                            string sss = "";
                            sss += "<div>" + leaveBossData.CancelAllowDate.Value.ToLongDateString() + "</div>";
                            if (leaveBossData.CancelAllow == 1) {
                                sss += "<div style='color:green;'>อนุญาต</div>";
                            } else {
                                sss += "<div style='color:red;'>ไม่อนุญาต</div>";
                            }
                            sss += "<div>" + leaveBossData.CancelComment + "</div>";
                            { TableCell cell = new TableCell(); cell.Text = sss; row.Cells.Add(cell); }
                            table.Rows.Add(row);
                        }
                    }
                    {
                        TableRow row = new TableRow();
                        { TableCell cell = new TableCell(); cell.Text = ""; cell.ColumnSpan = 2; row.Cells.Add(cell); }
                        table.Rows.Add(row);
                    }

                }



            }


            return table;



        }


        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e) {
            //Label1.Text = "(1 ตุลาคม " + DropDownList1.SelectedValue + " - 30 กันยายน " + (Convert.ToInt32(DropDownList1.SelectedValue) + 1) + ")";
            //BindGridView1();
        }

        protected void lbuExport_Click(object sender, EventArgs e) {
            /* Response.Clear();
             Response.Buffer = true;
             Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
             Response.Charset = "";
             Response.ContentType = "application/vnd.ms-excel";
             StringWriter sw = new StringWriter();
             HtmlTextWriter hw = new HtmlTextWriter(sw);
             GridView1.AllowPaging = false;
             GridView1.DataBind();
             //Change the Header Row back to white color
             GridView1.HeaderRow.Style.Add("background-color", "#FFFFFF");
             //Apply style to Individual Cells
             GridView1.HeaderRow.Cells[0].Style.Add("background-color", "green");
             GridView1.HeaderRow.Cells[1].Style.Add("background-color", "green");
             GridView1.HeaderRow.Cells[2].Style.Add("background-color", "green");
             GridView1.HeaderRow.Cells[3].Style.Add("background-color", "green");
             for (int i = 0; i < GridView1.Rows.Count; i++) {
                 GridViewRow row = GridView1.Rows[i];
                 //Change Color back to white
                 row.BackColor = System.Drawing.Color.White;
                 //Apply text style to each Row
                 row.Attributes.Add("class", "textmode");
                 //Apply style to Individual Cells of Alternating Row
                 if (i % 2 != 0) {
                     row.Cells[0].Style.Add("background-color", "#C2D69B");
                     row.Cells[1].Style.Add("background-color", "#C2D69B");
                     row.Cells[2].Style.Add("background-color", "#C2D69B");
                     row.Cells[3].Style.Add("background-color", "#C2D69B");
                 }
             }
             GridView1.RenderControl(hw);
             //style to format numbers to string
             string style = @"<style> .textmode { mso-number-format:\@; } </style>";
             Response.Write(style);
             Response.Output.Write(sw.ToString());
             Response.Flush();
             Response.End();*/

            /*tb.Rows[1].Cells[0].Style.Add("background-color", "green");
            tb.Rows[1].Cells[1].Style.Add("background-color", "green");
            tb.Rows[1].Cells[2].Style.Add("background-color", "green");
            tb.Rows[1].Cells[3].Style.Add("background-color", "green");*/

            /*for (int i = 0; i < 5; i++) {
                tb.Rows[0].Cells[i].Style.Add("border", "1px solid #000000");
            }
            for (int i = 0; i < 15; i++) {
                tb.Rows[1].Cells[i].Style.Add("border", "1px solid #000000");
            }
            for (int i = 2; i < tb.Rows.Count; i++) {
                for (int j = 0; j < 15; j++) {
                    tb.Rows[i].Cells[j].Style.Add("border", "1px solid #000000");
                }
                
            }*/

            Table table;
            if(ddlView.SelectedValue == "6") {
                if (ddlSelfView.SelectedValue == "2") {
                    table = BindToTableLeaveID(int.Parse(ddlSelfViewLeaveID.SelectedValue));
                } else {
                    table = BindToTableOnlyMe();
                }
            } else {
                table = BindToTable();
            }
            
            if(table == null) {
                return;
            }

            Response.ContentType = "application/x-msexcel";
            Response.AddHeader("Content-Disposition", "attachment;filename=LeaveReport" + DropDownList1.SelectedValue + ".xls");
            Response.ContentEncoding = Encoding.UTF8;

            //StringWriter tw = new StringWriter();
            //HtmlTextWriter hw = new HtmlTextWriter(tw);
            //tb.RenderControl(hw);
            //Response.Write(tw.ToString());
            StringWriter tw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(tw);
            tw.WriteLine("<html>");
            tw.WriteLine("<meta http-equiv='Content-Type' content='text/html; charset=UTF-8' />");
            tw.WriteLine("<style>");
            tw.WriteLine("table { border-collapse:collapse; }");
            tw.WriteLine("td { border-collapse:collapse; border: thin solid black; }");
            tw.WriteLine("</style>");
            //tw.WriteLine("<table>");

            //tw.WriteLine("<tr><th colspan='3'></th><th colspan='2'>ลาป่วย</th><th colspan='2'>ลากิจ</th><th colspan='2'>ลาพักผ่อน</th><th colspan='6'></th></tr>");
            //tw.WriteLine("<tr><th>ลำดับที่</th><th>ชื่อ - นามสกุล</th><th>ตำแหน่ง</th><th>ครั้ง</th><th>วัน</th><th>ครั้ง</th><th>วัน</th><th>ครั้ง</th><th>วัน</th><th>มาสาย(ครั้ง)</th><th>ขาดราชการ(วัน)</th><th>ลาคลอดบุตร(ระบุวันที่ลา)</th><th>ลาอุปสมบทฯ(ระบุวันที่ลา)</th><th>หมายเหตุ</th></tr>");

            table.RenderControl(hw);

           // tw.WriteLine("</table>");
            tw.WriteLine("</html>");
            Response.Write(tw.ToString());
            Response.End();


        }

        public override void VerifyRenderingInServerForm(Control control) {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }

        protected void lbuShow_Click(object sender, EventArgs e) {
            Table table;
            if (ddlView.SelectedValue == "6") {
                if(ddlSelfView.SelectedValue == "2") {
                    table = BindToTableLeaveID(int.Parse(ddlSelfViewLeaveID.SelectedValue));
                } else {
                    table = BindToTableOnlyMe();
                }
                
            } else {
                table = BindToTable();
            }
           /* Session["LeaveReportTable"] = table;*/
            Panel1.Controls.Clear();
            Panel1.Controls.Add(table);
        }

        protected void ddlSelfView_SelectedIndexChanged(object sender, EventArgs e) {
            ddlSelfViewLeaveID.Items.Clear();
            OracleConnection.ClearAllPools();
            using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING)) {
                con.Open();
                using (OracleCommand com = new OracleCommand("SELECT LEAVE_ID, (SELECT LEAVE_TYPE_NAME FROM LEV_TYPE WHERE LEV_TYPE.LEAVE_TYPE_ID = LEV_DATA.LEAVE_TYPE_ID) LEAVE_TYPE_NAME, REQ_DATE, FROM_DATE, TO_DATE, TOTAL_DAY FROM LEV_DATA WHERE PS_ID = '" + loginPerson.PS_CITIZEN_ID + "' ORDER BY LEAVE_ID ASC", con)) {
                    using (OracleDataReader reader = com.ExecuteReader()) {
                        while (reader.Read()) {
                            ddlSelfViewLeaveID.Items.Add(new ListItem("" + reader.GetInt32(0) + " | " + reader.GetString(1) + " , " + reader.GetDateTime(2).ToLongDateString() + " | " + reader.GetDateTime(3).ToLongDateString() + " ถึง " + reader.GetDateTime(4).ToLongDateString() + " รวม " + reader.GetInt32(5) + " วัน ", "" + reader.GetInt32(0)));
                        }
                    }
                }
            }

            /*if(ddlView.SelectedValue == "6") {
                ddlSelfView.Style.Add("display", "block");
                if(ddlSelfView.SelectedValue == "2") {
                    ddlSelfViewLeaveID.Style.Add("display", "block");
                }
            }*/

        }
    }
}