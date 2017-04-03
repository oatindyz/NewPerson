using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OracleClient;
using WEB_PERSONAL.Class;

namespace WEB_PERSONAL {
    public partial class WorkingDay : System.Web.UI.Page {

        private Person loginPerson;
        protected void Page_Load(object sender, EventArgs e) {

            PersonnelSystem ps = PersonnelSystem.GetPersonnelSystem(this);
            loginPerson = ps.LoginPerson;

            LoadCalendar(Table1, DateTime.Today);
            LoadCalendar(Table2, DateTime.Today.AddMonths(1));
            if(!IsPostBack) {
                LoadAbsentLate();
            }
            
        }

        private void LoadCalendar(Table table, DateTime dtm) {
            //Session["WorkdayDatetimeSearch"] = dtm;
            int daysInMonth = DateTime.DaysInMonth(dtm.Year, dtm.Month);
            {
                TableRow row = new TableRow();
                TableCell cell = new TableCell();
                cell.CssClass = "d_txt";
                cell.ColumnSpan = 7;
                cell.Text = Util.ToThaiMonth(dtm.Month) + " " + (dtm.Year+543);
                row.Cells.Add(cell);
                table.Rows.Add(row);
            }
            {



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
                for (int i = 1; i <= daysInMonth; i++) {

                    DateTime dt = new DateTime(dtm.Year, dtm.Month, i);

                    bool have = false;
                    using(OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING)) {
                        con.Open();
                        using(OracleCommand com = new OracleCommand("SELECT COUNT(WORKDAY_ID) FROM LEV_WORKDAY WHERE WORKDAY_DATE = :WORKDAY_DATE", con)) {
                            com.Parameters.AddWithValue("WORKDAY_DATE", dt);
                            using(OracleDataReader reader = com.ExecuteReader()) {
                                while(reader.Read()) {
                                    if(reader.GetInt32(0) > 0)
                                        have = true;
                                }
                            }
                        }
                    }

                    
                    if (dt.DayOfWeek == DayOfWeek.Sunday && row.Cells.Count > 0) {
                        table.Rows.Add(row);
                        row = new TableRow();
                    }

                    TableCell cell = new TableCell();
                   
                    if(dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday) {
                        cell.CssClass = "d_ss";
                    } else {
                        if (have) {
                            cell.CssClass = "d_break";
                        } else {
                            cell.CssClass = "d_work";
                        }
                    }
                    LinkButton lbu = new LinkButton();
                    //lbu.ID = "lbSelectDate" + i;
                    lbu.Text = dt.Day.ToString();
                    lbu.Click += (e2, e3) => {
                        if(dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday) {
                            return;
                        }
                        if (have) {
                            using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING)) {
                                con.Open();
                                using (OracleCommand com = new OracleCommand("DELETE FROM LEV_WORKDAY WHERE WORKDAY_DATE = :WORKDAY_DATE", con)) {
                                    com.Parameters.AddWithValue("WORKDAY_DATE", dt);
                                    com.ExecuteNonQuery();
                                }
                            }
                        } else {
                            using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING)) {
                                con.Open();
                                using (OracleCommand com = new OracleCommand("INSERT INTO LEV_WORKDAY (WORKDAY_ID, WORKDAY_DATE, CITIZEN_ID) VALUES(SEQ_WORKDAY_ID.NEXTVAL, :WORKDAY_DATE, :CITIZEN_ID)", con)) {
                                    com.Parameters.AddWithValue("WORKDAY_DATE", dt);
                                    com.Parameters.AddWithValue("CITIZEN_ID", loginPerson.PS_CITIZEN_ID);
                                    com.ExecuteNonQuery();
                                }
                            }
                        }
                        Response.Redirect("WorkingDay.aspx");
                    };
                    cell.Controls.Add(lbu);
                    row.Cells.Add(cell);
                    
                }
                if(row.Cells.Count > 0)
                    table.Rows.Add(row);
            }
        }

        private void LoadAbsentLate() {
            using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING)) {
                con.Open();
                using (OracleCommand com = new OracleCommand("SELECT * FROM LEV_TIME", con)) {
                    using(OracleDataReader reader = com.ExecuteReader()) {
                        while(reader.Read()) {
                            if(reader.GetInt32(0) == 1) {
                                tbLateHour.Text = reader.GetInt32(1).ToString();
                                tbLateMinute.Text = reader.GetInt32(2).ToString();
                            } else if(reader.GetInt32(0) == 2) {
                                tbAbsentHour.Text = reader.GetInt32(1).ToString();
                                tbAbsentMinute.Text = reader.GetInt32(2).ToString();
                            }
                        }
                    }
                    
                }
            }
        }

        protected void lbuChangeLate_Click(object sender, EventArgs e) {
            using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING)) {
                con.Open();
                using (OracleCommand com = new OracleCommand("UPDATE LEV_TIME SET TIME_HOUR_IN = :TIME_HOUR_IN, TIME_MINUTE_IN = :TIME_MINUTE_IN, CITIZEN_ID = :CITIZEN_ID WHERE TIME_ID = 1", con)) {
                    com.Parameters.AddWithValue("TIME_HOUR_IN", tbLateHour.Text);
                    com.Parameters.AddWithValue("TIME_MINUTE_IN", tbLateMinute.Text);
                    com.Parameters.AddWithValue("CITIZEN_ID", loginPerson.PS_CITIZEN_ID);
                    com.ExecuteNonQuery();
                }
            }
        }

        protected void lbuChangeAbsent_Click(object sender, EventArgs e) {
            using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING)) {
                con.Open();
                using (OracleCommand com = new OracleCommand("UPDATE LEV_TIME SET TIME_HOUR_IN = :TIME_HOUR_IN, TIME_MINUTE_IN = :TIME_MINUTE_IN, CITIZEN_ID = :CITIZEN_ID WHERE TIME_ID = 2", con)) {
                    com.Parameters.AddWithValue("TIME_HOUR_IN", tbAbsentHour.Text);
                    com.Parameters.AddWithValue("TIME_MINUTE_IN", tbAbsentMinute.Text);
                    com.Parameters.AddWithValue("CITIZEN_ID", loginPerson.PS_CITIZEN_ID);
                    com.ExecuteNonQuery();
                }
            }
        }
    }
}