using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using WEB_PERSONAL.Class;
using System.Data.OracleClient;

namespace WEB_PERSONAL
{
    public partial class INSG_Qualififed_Detail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Random r = new Random();
            {
                Table1.Rows.Clear();
                TableRow row = new TableRow();
                {
                    TableHeaderCell cell = new TableHeaderCell();
                    cell.Text = "เครื่องราชฯที่ได้รับ";
                    row.Cells.Add(cell);
                }
                {
                    TableHeaderCell cell = new TableHeaderCell();
                    cell.Text = "วันที่ได้รับเครื่องราชฯ";
                    row.Cells.Add(cell);
                }

                {
                    TableHeaderCell cell = new TableHeaderCell();
                    cell.Text = "ตำแหน่ง ณ ตอนที่ได้รับ";
                    row.Cells.Add(cell);
                }
                {
                    TableHeaderCell cell = new TableHeaderCell();
                    cell.Text = "เงินเดือน";
                    row.Cells.Add(cell);
                }
                {
                    TableHeaderCell cell = new TableHeaderCell();
                    cell.Text = "เอกสารอ้างอิง";
                    row.Cells.Add(cell);
                }

                Table1.Rows.Add(row);
            }

            PersonnelSystem ps = PersonnelSystem.GetPersonnelSystem(this);
            Person loginPerson = ps.LoginPerson;

            string psID = Request.QueryString["psID"];
            if (psID == null)
            {
                psID = loginPerson.PS_CITIZEN_ID;
            }

            OracleConnection.ClearAllPools();
            using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
            {
                con.Open();
                using (OracleCommand com = new OracleCommand("SELECT PS_CITIZEN_ID, PS_FN_TH, PS_LN_TH, (SELECT STAFFTYPE_NAME FROM TB_STAFFTYPE WHERE PS_PERSON.PS_STAFFTYPE_ID = TB_STAFFTYPE.STAFFTYPE_ID) ประเภทบุคลากร, (SELECT CAMPUS_NAME FROM TB_CAMPUS WHERE PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID) || ' ' || (SELECT FACULTY_NAME FROM TB_FACULTY WHERE PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID) || ' ' || (SELECT DIVISION_NAME FROM TB_DIVISION WHERE PS_PERSON.PS_DIVISION_ID = TB_DIVISION.DIVISION_ID) || ' ' || (SELECT WORK_NAME FROM TB_WORK_DIVISION WHERE PS_PERSON.PS_WORK_DIVISION_ID = TB_WORK_DIVISION.WORK_ID) \"งาน / ฝ่าย\", (SELECT POSITION_WORK_NAME FROM TB_POSITION_WORK WHERE PS_PERSON.PS_WORK_POS_ID = TB_POSITION_WORK.POSITION_WORK_ID) ตำแหน่ง, (SELECT SW_NAME FROM TB_STATUS_WORK WHERE PS_PERSON.PS_SW_ID = TB_STATUS_WORK.SW_ID) สถานะการทำงาน FROM PS_PERSON WHERE PS_CITIZEN_ID = '" + psID + "'", con))
                {
                    using (OracleDataReader reader = com.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lbCitizenID.Text = reader.IsDBNull(0) ? "" : reader.GetString(0);
                            lblName.Text = reader.IsDBNull(1) ? "" : reader.GetString(1);
                            lblLastName.Text = reader.IsDBNull(2) ? "" : reader.GetString(2);
                            lblStafftype.Text = reader.IsDBNull(3) ? "" : reader.GetString(3);
                            lblCampus.Text = reader.IsDBNull(4) ? "" : reader.GetString(4);
                            lblPosition.Text = reader.IsDBNull(5) ? "" : reader.GetString(5);
                            lblStatusPersonWork.Text = reader.IsDBNull(6) ? "" : reader.GetString(6);
                        }
                    }
                }

                using (OracleCommand com = new OracleCommand("SELECT (SELECT NAME_GRADEINSIGNIA_THA FROM INS_GRADEINSIGNIA WHERE ID_GRADEINSIGNIA = TB_INSIG_REQUEST.IR_INSIG_ID) ชื่อเครื่องราช,IR_DATE_GET_INSIG, IR_CURRENT_POSITION ,IR_CURRENT_SALARY,IR_REFERENCE, IR_INSIG_ID FROM TB_INSIG_REQUEST WHERE IR_CITIZEN_ID = '" + psID + "' AND IR_STATUS IN(3,4) AND IR_GET_STATUS = 1", con))
                {
                    using (OracleDataReader reader = com.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TableRow row = new TableRow();

                            {
                                Label lblInsigName = new Label();
                                lblInsigName.Text = reader.IsDBNull(0) ? "" : reader.GetString(0);
                                TableCell cell = new TableCell();
                                cell.Controls.Add(lblInsigName);
                                row.Cells.Add(cell);

                                Panel p = new Panel();
                                p.Style.Add("text-align", "center");
                                cell.Controls.Add(p);

                                Image img = new Image();
                                img.Style.Add("width", "100px");
                                img.Style.Add("height", "50px");
                                img.Style.Add("object-fit", "contain");
                                string fileName;
                                switch (reader.GetInt32(5))
                                {
                                    case 12: fileName = "บ.ม."; break;
                                    case 11: fileName = "บ.ช."; break;
                                    case 10: fileName = "จ.ม."; break;
                                    case 9: fileName = "จ.ช."; break;
                                    case 8: fileName = "ต.ม."; break;
                                    case 7: fileName = "ต.ช."; break;
                                    case 6: fileName = "ท.ม."; break;
                                    case 5: fileName = "ท.ช."; break;
                                    case 4: fileName = "ป.ม."; break;
                                    case 3: fileName = "ป.ช."; break;
                                    case 2: fileName = "ม.ว.ม."; break;
                                    default: fileName = "ม.ป.ช."; break;
                                }
                                img.Attributes["src"] = "Image/Insignia/" + fileName + ".png";
                                p.Controls.Add(img);
                            }

                            {
                                Label lblInsigDateGet = new Label();
                                lblInsigDateGet.Text = reader.IsDBNull(1) ? "" : reader.GetDateTime(1).ToLongDateString();
                                TableCell cell = new TableCell();
                                cell.Controls.Add(lblInsigDateGet);
                                row.Cells.Add(cell);
                            }

                            {
                                Label lblPositionGet = new Label();
                                lblPositionGet.Text = reader.IsDBNull(2) ? "" : reader.GetString(2);
                                TableCell cell = new TableCell();
                                cell.Controls.Add(lblPositionGet);
                                row.Cells.Add(cell);
                            }

                            {
                                Label lblSalaryGet = new Label();
                                lblSalaryGet.Text = reader.IsDBNull(3) ? "" : reader.GetInt32(3).ToString("#,###");
                                TableCell cell = new TableCell();
                                cell.Controls.Add(lblSalaryGet);
                                row.Cells.Add(cell);
                            }

                            {
                                Label lblRef = new Label();
                                lblRef.Text = reader.IsDBNull(4) ? "" : reader.GetString(4);
                                TableCell cell = new TableCell();
                                cell.Controls.Add(lblRef);
                                row.Cells.Add(cell);
                            }

                            Table1.Rows.Add(row);
                        }
                    }
                }

                if (Request.QueryString["psID"] == null)
                {
                    using (OracleCommand com = new OracleCommand("UPDATE TB_INSIG_REQUEST SET IR_STATUS = 4 WHERE IR_CITIZEN_ID = '" + loginPerson.PS_CITIZEN_ID + "' AND IR_STATUS = 3", con))
                    {
                        com.ExecuteNonQuery();
                    }
                }

            }
        }
    }

}
