using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WEB_PERSONAL.Class;
using System.Data.OracleClient;

namespace WEB_PERSONAL {

    public partial class INS_History : System.Web.UI.Page {

        protected void Page_Load(object sender, EventArgs e) {

            PersonnelSystem ps = PersonnelSystem.GetPersonnelSystem(this);
            Person loginPerson = ps.LoginPerson;

            Table table = new Table();
            table.CssClass = "c1";
            {
                TableHeaderRow row = new TableHeaderRow();
                { TableHeaderCell cell = new TableHeaderCell(); cell.ColumnSpan = 2; cell.Text = "เครื่องรายฯ"; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "วันทีขอ"; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "วันที่ได้รับ"; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "สถานะ"; row.Cells.Add(cell); }
                table.Rows.Add(row);
            }
            
            

            

            OracleConnection.ClearAllPools();
            using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING)) {
                con.Open();
                using (OracleCommand com = new OracleCommand("SELECT TB_INSIG_PERSON.*, (SELECT INSIG_GRADE_NAME_L FROM TB_INSIG_GRADE WHERE INSIG_GRADE_ID = INSIG_ID), (SELECT IP_STATUS_NAME FROM TB_INSIG_PERSON_STATUS WHERE TB_INSIG_PERSON_STATUS.IP_STATUS_ID = TB_INSIG_PERSON.IP_STATUS_ID) FROM TB_INSIG_PERSON WHERE CITIZEN_ID = :CITIZEN_ID", con)) {

                    com.Parameters.Add(new OracleParameter("CITIZEN_ID", loginPerson.PS_CITIZEN_ID));
                    using (OracleDataReader reader = com.ExecuteReader()) {
                        while (reader.Read()) {
                            {
                                TableHeaderRow row = new TableHeaderRow();
                                {
                                    TableCell cell = new TableCell();
                                    Image image = new Image();
                                    string fileName;
                                    switch (int.Parse(reader.GetValue(2).ToString())) {
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
                                    image.Attributes["src"] = "Image/Insignia/" + fileName + ".png";
                                    cell.Controls.Add(image);
                                    row.Cells.Add(cell);
                                }
                                { TableCell cell = new TableCell(); cell.Text = reader.GetString(6); row.Cells.Add(cell); }
                                { TableCell cell = new TableCell(); cell.Text = reader.GetDateTime(3).ToLongDateString(); row.Cells.Add(cell); }
                                { TableCell cell = new TableCell(); cell.Text = reader.GetDateTime(4).ToLongDateString(); row.Cells.Add(cell); }
                                { TableCell cell = new TableCell(); cell.Text = reader.GetString(7); row.Cells.Add(cell); }
                                table.Rows.Add(row);
                            }
                        }
                    }

                }
            }

            div1.Controls.Add(table);

        }
    }
}