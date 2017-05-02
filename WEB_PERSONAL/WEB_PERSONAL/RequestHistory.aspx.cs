using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WEB_PERSONAL.Class;
using System.Data.OracleClient;
using System.Data;

namespace WEB_PERSONAL
{
    public partial class RequestHistory : System.Web.UI.Page
    {
        Person loginPerson;
        protected void Page_Load(object sender, EventArgs e)
        {
            PersonnelSystem ps = PersonnelSystem.GetPersonnelSystem(this);
            loginPerson = ps.LoginPerson;

            if (!IsPostBack)
            {
                ReadRequest();
            }
        }

        protected void ReadRequest()
        {
            using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
            {
                con.Open();
                using (OracleCommand com = new OracleCommand("SELECT R_ID, CITIZEN_ID, R_STATUS_ID, DATE_START, DATE_END, (SELECT TITLE_NAME_TH FROM TB_TITLENAME WHERE TB_TITLENAME.TITLE_ID = TB_REQUEST.TITLE_ID) TITLE_NAME, FIRSTNAME, LASTNAME, (SELECT GENDER_NAME FROM TB_GENDER WHERE TB_GENDER.GENDER_ID = TB_REQUEST.GENDER_ID) GENDER_NAME, TO_CHAR(ADD_MONTHS(BIRTHDAY_DATE,6516),'DD MON YYYY') BIRTHDAY_DATE, EMAIL, (SELECT NATION_NAME_EN FROM TB_NATION WHERE TB_NATION.NATION_ID = TB_REQUEST.NATION_ID) NATION_NAME, (SELECT CAMPUS_NAME FROM TB_CAMPUS WHERE TB_CAMPUS.CAMPUS_ID = TB_REQUEST.CAMPUS_ID) CAMPUS_NAME, (SELECT FACULTY_NAME FROM TB_FACULTY WHERE TB_FACULTY.FACULTY_ID = TB_REQUEST.FACULTY_ID) FACULTY_NAME, (SELECT DIVISION_NAME FROM TB_DIVISION WHERE TB_DIVISION.DIVISION_ID = TB_REQUEST.DIVISION_ID) DIVISION_NAME, (SELECT WORK_NAME FROM TB_WORK_DIVISION WHERE TB_WORK_DIVISION.WORK_ID = TB_REQUEST.WORK_DIVISION_ID) WORK_DIVISION_NAME, (SELECT STAFFTYPE_NAME FROM TB_STAFFTYPE WHERE TB_STAFFTYPE.STAFFTYPE_ID = TB_REQUEST.STAFFTYPE_ID) STAFFTYPE_NAME, (SELECT TIME_CONTACT_NAME FROM TB_TIME_CONTACT WHERE TB_TIME_CONTACT.TIME_CONTACT_ID = TB_REQUEST.TIME_CONTACT_ID) TIME_CONTACT_NAME, (SELECT BUDGET_NAME FROM TB_BUDGET WHERE TB_BUDGET.BUDGET_ID = TB_REQUEST.BUDGET_ID) BUDGET_NAME, (SELECT SUBSTAFFTYPE_NAME FROM TB_SUBSTAFFTYPE WHERE TB_SUBSTAFFTYPE.SUBSTAFFTYPE_ID = TB_REQUEST.SUBSTAFFTYPE_ID) SUBSTAFFTYPE_NAME, (SELECT ADMIN_POSITION_NAME FROM TB_ADMIN_POSITION WHERE TB_ADMIN_POSITION.ADMIN_POSITION_ID = TB_REQUEST.ADMIN_POS_ID) ADMIN_POSITION_NAME, (SELECT POSITION_WORK_NAME FROM TB_POSITION_WORK WHERE TB_POSITION_WORK.POSITION_WORK_ID = TB_REQUEST.WORK_POS_ID) WORK_POS_NAME, TO_CHAR(ADD_MONTHS(INWORK_DATE,6516),'DD MON YYYY') INWORK_DATE, TO_CHAR(ADD_MONTHS(DATE_START_THIS_U,6516),'DD MON YYYY') DATE_START_THIS_U, SPECIAL_NAME, (SELECT ISCED_NAME FROM TB_ISCED WHERE TB_ISCED.ISCED_ID = TB_REQUEST.TEACH_ISCED_ID) TEACH_ISCED_NAME, (SELECT LEV_NAME_TH FROM TB_LEV WHERE TB_LEV.LEV_ID = TB_REQUEST.GRAD_LEV_ID) GRAD_LEV_NAME, GRAD_CURR, (SELECT ISCED_NAME FROM TB_ISCED WHERE TB_ISCED.ISCED_ID = TB_REQUEST.GRAD_ISCED_ID) GRAD_ISCED_NAME, (SELECT PROGRAM_NAME FROM TB_PROGRAM WHERE TB_PROGRAM.PROGRAM_ID_NEW = TB_REQUEST.GRAD_PROG_ID) GRAD_PROG_NAME, GRAD_UNIV, (SELECT NATION_NAME_EN FROM TB_NATION WHERE TB_NATION.NATION_ID = TB_REQUEST.GRAD_COUNTRY_ID) GRAD_COUNTRY_NAME, (SELECT DEFORM_NAME FROM TB_DEFORM WHERE TB_DEFORM.DEFORM_ID = TB_REQUEST.DEFORM_ID) DEFORM_NAME, SIT_NO, (SELECT RELIGION_NAME FROM TB_RELIGION WHERE TB_RELIGION.RELIGION_ID = TB_REQUEST.RELIGION_ID) RELIGION_NAME, (SELECT MOVEMENT_TYPE_NAME FROM TB_MOVEMENT_TYPE WHERE TB_MOVEMENT_TYPE.MOVEMENT_TYPE_ID = TB_REQUEST.MOVEMENT_TYPE_ID) MOVEMENT_TYPE_NAME, TO_CHAR(ADD_MONTHS(MOVEMENT_DATE,6516),'DD MON YYYY') MOVEMENT_DATE, COMMENT_INFO, (SELECT R_STATUS_NAME FROM TB_REQUEST_STATUS WHERE TB_REQUEST_STATUS.R_STATUS_ID = TB_REQUEST.R_STATUS_ID) FROM TB_REQUEST WHERE CITIZEN_ID = '" + loginPerson.PS_CITIZEN_ID + "' ORDER BY R_ID DESC", con))
                {
                    using (OracleDataReader reader = com.ExecuteReader())
                    {
                        int i = 1;
                        while (reader.Read())
                        {
                            Table tb1 = new Table();
                            tb1.CssClass = "ps-table-1";
                            tb1.Style.Add("margin-bottom", "20px");
                            div1.Controls.Add(tb1);

                            {
                                TableHeaderRow row = new TableHeaderRow();
                                tb1.Controls.Add(row);
                                tb1.Rows.Add(row);

                                TableHeaderCell cell = new TableHeaderCell();
                                cell.ColumnSpan = 2;
                                cell.Text = "<strong>" + (i++) + ")</strong> วันที่แก้ไข " + reader.GetDateTime(4).ToLongDateString() + " / ";
                                if(reader.GetValue(2).ToString() == "2" || reader.GetValue(2).ToString() == "3") {
                                    cell.Text += "<span style=\"color: rgb(34, 177, 76);\">อนุมัติ</span>";
                                } else if (reader.GetValue(2).ToString() == "4" || reader.GetValue(2).ToString() == "5") {
                                    cell.Text += "<span style=\"color: rgb(237, 28, 36);\">ไม่อนุมัติ</span>";
                                }
                                row.Cells.Add(cell);

                            }

                            

                            if (!reader.IsDBNull(5)) {
                                TableRow row = new TableRow();
                                tb1.Controls.Add(row);
                                tb1.Rows.Add(row);

                                TableCell cell = new TableCell();
                                cell.Text = "คำนำหน้า";
                                row.Cells.Add(cell);

                                cell = new TableCell();
                                cell.Text = reader.GetValue(5).ToString();
                                row.Cells.Add(cell);
                            }

                            if (!reader.IsDBNull(6))
                            {
                                TableRow row = new TableRow();
                                tb1.Controls.Add(row);
                                tb1.Rows.Add(row);

                                TableCell cell = new TableCell();
                                cell.Text = "ชื่อ";
                                row.Cells.Add(cell);

                                cell = new TableCell();
                                cell.Text = reader.GetValue(6).ToString();
                                row.Cells.Add(cell);
                            }

                            if (!reader.IsDBNull(7))
                            {
                                TableRow row = new TableRow();
                                tb1.Controls.Add(row);
                                tb1.Rows.Add(row);

                                TableCell cell = new TableCell();
                                cell.Text = "นามสกุล";
                                row.Cells.Add(cell);

                                cell = new TableCell();
                                cell.Text = reader.GetValue(7).ToString();
                                row.Cells.Add(cell);
                            }

                            if (!reader.IsDBNull(8))
                            {
                                TableRow row = new TableRow();
                                tb1.Controls.Add(row);
                                tb1.Rows.Add(row);

                                TableCell cell = new TableCell();
                                cell.Text = "เพศ";
                                row.Cells.Add(cell);

                                cell = new TableCell();
                                cell.Text = reader.GetValue(8).ToString();
                                row.Cells.Add(cell);
                            }

                            if (!reader.IsDBNull(9))
                            {
                                TableRow row = new TableRow();
                                tb1.Controls.Add(row);
                                tb1.Rows.Add(row);

                                TableCell cell = new TableCell();
                                cell.Text = "วันเกิด";
                                row.Cells.Add(cell);

                                cell = new TableCell();
                                cell.Text = reader.GetValue(9).ToString();
                                row.Cells.Add(cell);
                            }

                            if (!reader.IsDBNull(10))
                            {
                                TableRow row = new TableRow();
                                tb1.Controls.Add(row);
                                tb1.Rows.Add(row);

                                TableCell cell = new TableCell();
                                cell.Text = "อีเมล";
                                row.Cells.Add(cell);

                                cell = new TableCell();
                                cell.Text = reader.GetValue(10).ToString();
                                row.Cells.Add(cell);
                            }

                            if (!reader.IsDBNull(11))
                            {
                                TableRow row = new TableRow();
                                tb1.Controls.Add(row);
                                tb1.Rows.Add(row);

                                TableCell cell = new TableCell();
                                cell.Text = "สัญชาติ";
                                row.Cells.Add(cell);

                                cell = new TableCell();
                                cell.Text = reader.GetValue(11).ToString();
                                row.Cells.Add(cell);
                            }

                            if (!reader.IsDBNull(12))
                            {
                                TableRow row = new TableRow();
                                tb1.Controls.Add(row);
                                tb1.Rows.Add(row);

                                TableCell cell = new TableCell();
                                cell.Text = "วิทยาเขต";
                                row.Cells.Add(cell);

                                cell = new TableCell();
                                cell.Text = reader.GetValue(12).ToString();
                                row.Cells.Add(cell);
                            }

                            if (!reader.IsDBNull(13))
                            {
                                TableRow row = new TableRow();
                                tb1.Controls.Add(row);
                                tb1.Rows.Add(row);

                                TableCell cell = new TableCell();
                                cell.Text = "สำนัก/สถาบัน/คณะ";
                                row.Cells.Add(cell);

                                cell = new TableCell();
                                cell.Text = reader.GetValue(13).ToString();
                                row.Cells.Add(cell);
                            }

                            if (!reader.IsDBNull(14))
                            {
                                TableRow row = new TableRow();
                                tb1.Controls.Add(row);
                                tb1.Rows.Add(row);

                                TableCell cell = new TableCell();
                                cell.Text = "กอง/สำนักงานเลขา/ภาควิชา";
                                row.Cells.Add(cell);

                                cell = new TableCell();
                                cell.Text = reader.GetValue(14).ToString();
                                row.Cells.Add(cell);
                            }

                            if (!reader.IsDBNull(15))
                            {
                                TableRow row = new TableRow();
                                tb1.Controls.Add(row);
                                tb1.Rows.Add(row);

                                TableCell cell = new TableCell();
                                cell.Text = "งาน/ฝ่าย";
                                row.Cells.Add(cell);

                                cell = new TableCell();
                                cell.Text = reader.GetValue(15).ToString();
                                row.Cells.Add(cell);
                            }

                            if (!reader.IsDBNull(16))
                            {
                                TableRow row = new TableRow();
                                tb1.Controls.Add(row);
                                tb1.Rows.Add(row);

                                TableCell cell = new TableCell();
                                cell.Text = "ประเภทบุคลากร";
                                row.Cells.Add(cell);

                                cell = new TableCell();
                                cell.Text = reader.GetValue(16).ToString();
                                row.Cells.Add(cell);
                            }

                            if (!reader.IsDBNull(17))
                            {
                                TableRow row = new TableRow();
                                tb1.Controls.Add(row);
                                tb1.Rows.Add(row);

                                TableCell cell = new TableCell();
                                cell.Text = "ระยะเวลาจ้าง";
                                row.Cells.Add(cell);

                                cell = new TableCell();
                                cell.Text = reader.GetValue(17).ToString();
                                row.Cells.Add(cell);
                            }

                            if (!reader.IsDBNull(18))
                            {
                                TableRow row = new TableRow();
                                tb1.Controls.Add(row);
                                tb1.Rows.Add(row);

                                TableCell cell = new TableCell();
                                cell.Text = "ประเภทเงินจ้าง";
                                row.Cells.Add(cell);

                                cell = new TableCell();
                                cell.Text = reader.GetValue(18).ToString();
                                row.Cells.Add(cell);
                            }

                            if (!reader.IsDBNull(19))
                            {
                                TableRow row = new TableRow();
                                tb1.Controls.Add(row);
                                tb1.Rows.Add(row);

                                TableCell cell = new TableCell();
                                cell.Text = "ประเภทบุคลากรย่อย";
                                row.Cells.Add(cell);

                                cell = new TableCell();
                                cell.Text = reader.GetValue(19).ToString();
                                row.Cells.Add(cell);
                            }

                            if (!reader.IsDBNull(20))
                            {
                                TableRow row = new TableRow();
                                tb1.Controls.Add(row);
                                tb1.Rows.Add(row);

                                TableCell cell = new TableCell();
                                cell.Text = "ตำแหน่งทางบริหาร";
                                row.Cells.Add(cell);

                                cell = new TableCell();
                                cell.Text = reader.GetValue(20).ToString();
                                row.Cells.Add(cell);
                            }

                            if (!reader.IsDBNull(21))
                            {
                                TableRow row = new TableRow();
                                tb1.Controls.Add(row);
                                tb1.Rows.Add(row);

                                TableCell cell = new TableCell();
                                cell.Text = "ตำแหน่งในสายงาน";
                                row.Cells.Add(cell);

                                cell = new TableCell();
                                cell.Text = reader.GetValue(21).ToString();
                                row.Cells.Add(cell);
                            }

                            if (!reader.IsDBNull(22))
                            {
                                TableRow row = new TableRow();
                                tb1.Controls.Add(row);
                                tb1.Rows.Add(row);

                                TableCell cell = new TableCell();
                                cell.Text = "วันที่เข้าทำงานครั้งแรก";
                                row.Cells.Add(cell);

                                cell = new TableCell();
                                cell.Text = reader.GetValue(22).ToString();
                                row.Cells.Add(cell);
                            }

                            if (!reader.IsDBNull(23))
                            {
                                TableRow row = new TableRow();
                                tb1.Controls.Add(row);
                                tb1.Rows.Add(row);

                                TableCell cell = new TableCell();
                                cell.Text = "วันที่เข้าทำงาน ณ สถานที่ปัจจุบัน";
                                row.Cells.Add(cell);

                                cell = new TableCell();
                                cell.Text = reader.GetValue(23).ToString();
                                row.Cells.Add(cell);
                            }

                            if (!reader.IsDBNull(24))
                            {
                                TableRow row = new TableRow();
                                tb1.Controls.Add(row);
                                tb1.Rows.Add(row);

                                TableCell cell = new TableCell();
                                cell.Text = "สาขางานที่เชี่ยวชาญ";
                                row.Cells.Add(cell);

                                cell = new TableCell();
                                cell.Text = reader.GetValue(24).ToString();
                                row.Cells.Add(cell);
                            }

                            if (!reader.IsDBNull(25))
                            {
                                TableRow row = new TableRow();
                                tb1.Controls.Add(row);
                                tb1.Rows.Add(row);

                                TableCell cell = new TableCell();
                                cell.Text = "กลุ่มสาขาวิชาที่สอน(ISCED)";
                                row.Cells.Add(cell);

                                cell = new TableCell();
                                cell.Text = reader.GetValue(25).ToString();
                                row.Cells.Add(cell);
                            }

                            if (!reader.IsDBNull(26))
                            {
                                TableRow row = new TableRow();
                                tb1.Controls.Add(row);
                                tb1.Rows.Add(row);

                                TableCell cell = new TableCell();
                                cell.Text = "ระดับการศึกษาที่จบสูงสุด";
                                row.Cells.Add(cell);

                                cell = new TableCell();
                                cell.Text = reader.GetValue(26).ToString();
                                row.Cells.Add(cell);
                            }

                            if (!reader.IsDBNull(27))
                            {
                                TableRow row = new TableRow();
                                tb1.Controls.Add(row);
                                tb1.Rows.Add(row);

                                TableCell cell = new TableCell();
                                cell.Text = "หลักสูตรที่จบการศึกษาสูงสุด";
                                row.Cells.Add(cell);

                                cell = new TableCell();
                                cell.Text = reader.GetValue(27).ToString();
                                row.Cells.Add(cell);
                            }

                            if (!reader.IsDBNull(28))
                            {
                                TableRow row = new TableRow();
                                tb1.Controls.Add(row);
                                tb1.Rows.Add(row);

                                TableCell cell = new TableCell();
                                cell.Text = "กลุ่มสาขาวิชาที่จบสูงสุด(ISCED)";
                                row.Cells.Add(cell);

                                cell = new TableCell();
                                cell.Text = reader.GetValue(28).ToString();
                                row.Cells.Add(cell);
                            }

                            if (!reader.IsDBNull(29))
                            {
                                TableRow row = new TableRow();
                                tb1.Controls.Add(row);
                                tb1.Rows.Add(row);

                                TableCell cell = new TableCell();
                                cell.Text = "สาขาวิชาที่จบสูงสุด";
                                row.Cells.Add(cell);

                                cell = new TableCell();
                                cell.Text = reader.GetValue(29).ToString();
                                row.Cells.Add(cell);
                            }

                            if (!reader.IsDBNull(30))
                            {
                                TableRow row = new TableRow();
                                tb1.Controls.Add(row);
                                tb1.Rows.Add(row);

                                TableCell cell = new TableCell();
                                cell.Text = "ชื่อสถาบันที่จบการศึกษาสูงสุด";
                                row.Cells.Add(cell);

                                cell = new TableCell();
                                cell.Text = reader.GetValue(30).ToString();
                                row.Cells.Add(cell);
                            }

                            if (!reader.IsDBNull(31))
                            {
                                TableRow row = new TableRow();
                                tb1.Controls.Add(row);
                                tb1.Rows.Add(row);

                                TableCell cell = new TableCell();
                                cell.Text = "ประเทศที่จบการศึกษาสูงสุด";
                                row.Cells.Add(cell);

                                cell = new TableCell();
                                cell.Text = reader.GetValue(31).ToString();
                                row.Cells.Add(cell);
                            }

                            if (!reader.IsDBNull(32))
                            {
                                TableRow row = new TableRow();
                                tb1.Controls.Add(row);
                                tb1.Rows.Add(row);

                                TableCell cell = new TableCell();
                                cell.Text = "ความพิการ";
                                row.Cells.Add(cell);

                                cell = new TableCell();
                                cell.Text = reader.GetValue(32).ToString();
                                row.Cells.Add(cell);
                            }

                            if (!reader.IsDBNull(33))
                            {
                                TableRow row = new TableRow();
                                tb1.Controls.Add(row);
                                tb1.Rows.Add(row);

                                TableCell cell = new TableCell();
                                cell.Text = "เลขที่ตำแหน่ง";
                                row.Cells.Add(cell);

                                cell = new TableCell();
                                cell.Text = reader.GetValue(33).ToString();
                                row.Cells.Add(cell);
                            }

                            if (!reader.IsDBNull(34))
                            {
                                TableRow row = new TableRow();
                                tb1.Controls.Add(row);
                                tb1.Rows.Add(row);

                                TableCell cell = new TableCell();
                                cell.Text = "ศาสนา";
                                row.Cells.Add(cell);

                                cell = new TableCell();
                                cell.Text = reader.GetValue(34).ToString();
                                row.Cells.Add(cell);
                            }

                            if (!reader.IsDBNull(35))
                            {
                                TableRow row = new TableRow();
                                tb1.Controls.Add(row);
                                tb1.Rows.Add(row);

                                TableCell cell = new TableCell();
                                cell.Text = "ประเภทการดำรงตำแหน่งปัจจุบัน";
                                row.Cells.Add(cell);

                                cell = new TableCell();
                                cell.Text = reader.GetValue(35).ToString();
                                row.Cells.Add(cell);
                            }

                            if (!reader.IsDBNull(36))
                            {
                                TableRow row = new TableRow();
                                tb1.Controls.Add(row);
                                tb1.Rows.Add(row);

                                TableCell cell = new TableCell();
                                cell.Text = "วันที่มีผลบังคับใช้'ประเภทการดำรงตำแหน่งปัจจุบัน'";
                                row.Cells.Add(cell);

                                cell = new TableCell();
                                cell.Text = reader.GetValue(36).ToString();
                                row.Cells.Add(cell);
                            }
                            if (!reader.IsDBNull(37)) {
                                TableRow row = new TableRow();
                                tb1.Controls.Add(row);
                                tb1.Rows.Add(row);

                                TableCell cell = new TableCell();
                                cell.Text = "<strong>ความเห็น</strong>";
                                row.Cells.Add(cell);

                                cell = new TableCell();
                                cell.Text = reader.GetValue(37).ToString();
                                row.Cells.Add(cell);
                            }

                            if (!reader.IsDBNull(38)) {
                                TableRow row = new TableRow();
                                tb1.Controls.Add(row);
                                tb1.Rows.Add(row);

                                TableCell cell = new TableCell();
                                cell.Text = "<strong>ผลการอนุมัติ</strong>";
                                row.Cells.Add(cell);

                                cell = new TableCell();
                                cell.Text = reader.GetValue(38).ToString();
                                row.Cells.Add(cell);
                            }


                            

                            

                        }
                    }
                }
            }
        }
    }
}