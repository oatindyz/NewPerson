using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WEB_PERSONAL.Class;
using System.Data.OracleClient;

namespace WEB_PERSONAL {
    public partial class WorkingTimeHistory : System.Web.UI.Page {

        private Person pp;
        //private List<DateTime> datetimeList;

        protected void Page_Load(object sender, EventArgs e) {
            PersonnelSystem ps = PersonnelSystem.GetPersonnelSystem(this);
            pp = ps.LoginPerson;
            //datetimeList = DatabaseManager.GetLeaveDateTimeFromToDate(pp.CitizenID);
            LoadCalendar(Panel1, DateTime.Today);
            
            if(!IsPostBack) {
                DateTime dt = DateTime.Today;
                ddlMonth.SelectedValue = "" + dt.Month;
                tbYear.Text = "" + (dt.Year + 543);
            }
        }

        

        private void LoadCalendar(Panel p, DateTime dtm) {
            //Session["WorkdayDatetimeSearch"] = dtm;
            int daysInMonth = DateTime.DaysInMonth(dtm.Year, dtm.Month);
            Panel p2 = new Panel();
            p2.Style.Add("display", "inline-block");
            //p2.Style.Add("border", "1px solid rgb(235,235,235)");
            p2.Style.Add("padding", "5px");
            p2.Style.Add("margin", "5px");
            p.Controls.Add(p2);
            Table table = new Table();
            table.CssClass = "d_table";

            {
                Panel p3 = new Panel();
                TableCell cell = new TableCell();
                p3.CssClass = "d_txt";
                Label lb = new Label();
                lb.Text = Util.ToThaiMonth(dtm.Month) + " " + (dtm.Year + 543);
                p3.Controls.Add(lb);
                p2.Controls.Add(p3);
            }
            {

                p2.Controls.Add(table);

                TableRow row = new TableRow();
                for (int i = 0; i < 7; i++) {

                    TableCell cell = new TableCell();
                    cell.CssClass = "d_day_head";
                    switch (i) {
                        case 0: cell.Text = "อาทิตย์"; break;
                        case 1: cell.Text = "จันทร์"; break;
                        case 2: cell.Text = "อังคาร"; break;
                        case 3: cell.Text = "พุธ"; break;
                        case 4: cell.Text = "พฤหัสบดี"; break;
                        case 5: cell.Text = "ศุกร์"; break;
                        case 6: cell.Text = "เสาร์"; break;
                    }
                    row.Cells.Add(cell);
                }
                table.Rows.Add(row);
            }

            /* {
                 DateTime dt = new DateTime(dtm.Year, dtm.Month, 1);
                 TableRow row = new TableRow();
                 for (int i = 0; i < (int)dt.DayOfWeek; i++) {
                     TableCell cell = new TableCell();
                     row.Cells.Add(cell);
                 }
             }*/
            {
                TableRow row = new TableRow();
                {
                    DateTime dt = new DateTime(dtm.Year, dtm.Month, 1);
                    for (int i = 0; i < (int)dt.DayOfWeek; i++) {
                        TableCell cell = new TableCell();
                        cell.CssClass = "d_blank";
                        row.Cells.Add(cell);
                    }
                }
                using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING)) {
                    con.Open();

                    for (int i = 1; i <= daysInMonth; i++) {

                        DateTime dt = new DateTime(dtm.Year, dtm.Month, i);

                        bool break_day = false;
                        bool have = false;
                        bool late = false;
                        bool absent = false;
                        string timeIn = "";
                        string timeOut = "";
                        bool leave = false;

                        /*if (datetimeList.Contains(dt)) {
                            leave = true;
                        }*/

                        using (OracleCommand com = new OracleCommand("SELECT COUNT(WORKDAY_ID) FROM LEV_WORKDAY WHERE WORKDAY_DATE = :WORKDAY_DATE", con)) {
                            com.Parameters.AddWithValue("WORKDAY_DATE", dt);
                            using (OracleDataReader reader = com.ExecuteReader()) {
                                while (reader.Read()) {
                                    if (reader.GetInt32(0) > 0)
                                        break_day = true;
                                }
                            }
                        }

                        using (OracleCommand com = new OracleCommand("SELECT COUNT(WORKTIME_ID) FROM LEV_WORKTIME WHERE TODAY = :TODAY AND CITIZEN_ID = '" + pp.PS_CITIZEN_ID + "'", con)) {
                            com.Parameters.AddWithValue("TODAY", dt);
                            using (OracleDataReader reader = com.ExecuteReader()) {
                                while (reader.Read()) {
                                    if (reader.GetInt32(0) > 0)
                                        have = true;
                                }
                            }
                        }
                        if (have) {
                            using (OracleCommand com = new OracleCommand("SELECT LATE FROM LEV_WORKTIME WHERE TODAY = :TODAY AND CITIZEN_ID = '" + pp.PS_CITIZEN_ID + "'", con)) {
                                com.Parameters.AddWithValue("TODAY", dt);
                                using (OracleDataReader reader = com.ExecuteReader()) {
                                    while (reader.Read()) {
                                        if (!reader.IsDBNull(0)) {
                                            late = Convert.ToBoolean(reader.GetInt32(0));
                                        }
                                    }
                                }
                            }
                            using (OracleCommand com = new OracleCommand("SELECT ABSENT FROM LEV_WORKTIME WHERE TODAY = :TODAY AND CITIZEN_ID = '" + pp.PS_CITIZEN_ID + "'", con)) {
                                com.Parameters.AddWithValue("TODAY", dt);
                                using (OracleDataReader reader = com.ExecuteReader()) {
                                    while (reader.Read()) {
                                        if (!reader.IsDBNull(0)) {
                                            absent = Convert.ToBoolean(reader.GetInt32(0));
                                        }
                                    }
                                }
                            }
                            using (OracleCommand com = new OracleCommand("SELECT LEAVE FROM LEV_WORKTIME WHERE TODAY = :TODAY AND CITIZEN_ID = '" + pp.PS_CITIZEN_ID + "'", con)) {
                                com.Parameters.AddWithValue("TODAY", dt);
                                using (OracleDataReader reader = com.ExecuteReader()) {
                                    while (reader.Read()) {
                                        if(!reader.IsDBNull(0)) {
                                            leave = Convert.ToBoolean(reader.GetInt32(0));
                                        }
                                        
                                    }
                                }
                            }
                            using (OracleCommand com = new OracleCommand("SELECT HOUR_IN || ':' || MINUTE_IN, HOUR_OUT || ':' || MINUTE_OUT FROM LEV_WORKTIME WHERE TODAY = :TODAY AND CITIZEN_ID = '" + pp.PS_CITIZEN_ID + "'", con)) {
                                com.Parameters.AddWithValue("TODAY", dt);
                                using (OracleDataReader reader = com.ExecuteReader()) {
                                    while (reader.Read()) {
                                        timeIn = reader.GetString(0);
                                        timeOut = reader.GetString(1);
                                    }
                                }
                            }
                        }


                        if (dt.DayOfWeek == DayOfWeek.Sunday && row.Cells.Count > 0) {
                            table.Rows.Add(row);
                            row = new TableRow();
                        }

                        TableCell cell = new TableCell();

                        Label lb = new Label();

                        if (dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday) {
                            cell.CssClass = "d_ss";
                            lb.Text = "<div>" + dt.Day.ToString() + "</div>";
                        } else {
                            if(leave) {
                                lb.Text = "<div>" + dt.Day.ToString() + "</div><div>ลาหยุด</div>";
                                cell.CssClass = "d_leave";
                            } else if (break_day) {
                                lb.Text = "<div>" + dt.Day.ToString() + "</div><div>วันหยุด</div>";
                                cell.CssClass = "d_break";
                            } else if (!have) {
                                lb.Text = "<div>" + dt.Day.ToString() + "</div><div>ไม่มีข้อมูล</div>";
                                cell.CssClass = "d_no_data";
                            } else if (late) {
                                lb.Text = "<div>" + dt.Day.ToString() + "</div><div>" + timeIn + " - " + timeOut + "</div>";
                                cell.CssClass = "d_late";
                            } else if (absent) {
                                lb.Text = "<div>" + dt.Day.ToString() + "</div><div>ขาด</div>";
                                cell.CssClass = "d_absent";
                            } else {
                                lb.Text = "<div>" + dt.Day.ToString() + "</div><div>" + timeIn + " - " + timeOut + "</div>";
                                cell.CssClass = "d_normal";
                            }
                        }

                        //lbu.ID = "lbSelectDate" + i;


                        cell.Controls.Add(lb);
                        row.Cells.Add(cell);

                    }
                    if (row.Cells.Count > 0)
                        table.Rows.Add(row);
                }
            }

        }

        protected void lbuSearchV2_Click(object sender, EventArgs e) {
            Panel1.Controls.Clear();
            if (tbYear.Text == "") {
                lbError.Text = "*กรุณากรอกปี";
            } else {
                lbError.Text = "";
                DateTime dt = new DateTime(int.Parse(tbYear.Text) - 543, int.Parse(ddlMonth.SelectedValue), 1);
                LoadCalendar(Panel1, dt);
            }
            

        }
    }
}