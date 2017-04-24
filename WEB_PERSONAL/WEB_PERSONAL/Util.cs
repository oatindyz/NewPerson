using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Globalization;
using WEB_PERSONAL.Class;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Mail;
using System.Data.OracleClient;

namespace WEB_PERSONAL {

    public class Util {

        public static string ToOracleDate2(string date) {
            string[] s = date.Split('/');
            return s[0] + " " + ToOracleMonth(s[1]) + " " + (Convert.ToInt32(s[2]) - 543);
        }
        public static System.Data.DataTable dt;
        /*public static string YearDown543(DateTime date) {

        }*/
        public static string ToOracleDate(string date) {
            string[] s = date.Split('/');
            return s[0] + " " + ToOracleMonth(s[1]) + " " + s[2];
        }
        public static string ToOracleDate(DateTime date) {
            return date.Day.ToString("00") + " " + ToOracleMonth(date.Month) + " " + date.Year.ToString("0000");
        }
        public static DateTime toOracleDateTime(string date) {
            return DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        }
        public static DateTime toOracleDateTime(DateTime date) {
            return DateTime.ParseExact(date.ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
        }
        public static DateTime ODT(string date) {

            string[] ss = date.Split(' ');

            switch (ss[1]) {
                case "ม.ค.": ss[1] = "01"; break;
                case "ก.พ.": ss[1] = "02"; break;
                case "มี.ค.": ss[1] = "03"; break;
                case "เม.ย.": ss[1] = "04"; break;
                case "พ.ค.": ss[1] = "05"; break;
                case "มิ.ย.": ss[1] = "06"; break;
                case "ก.ค.": ss[1] = "07"; break;
                case "ส.ค.": ss[1] = "08"; break;
                case "ก.ย.": ss[1] = "09"; break;
                case "ต.ค.": ss[1] = "10"; break;
                case "พ.ย.": ss[1] = "11"; break;
                case "ธ.ค.": ss[1] = "12"; break;
            }
            string d1 = ss[0] + "/" + ss[1] + "/" + ss[2];

            return DateTime.ParseExact(d1, "dd/MM/yyyy", CultureInfo.CurrentCulture);
        }
        public static DateTime ODTT() {
            return DateTime.ParseExact(DateTime.Today.ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
        }
        public static DateTime ODTN() {
            return DateTime.ParseExact(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
        }
        public static string NDT(string date) {
            string[] ss = date.Split(' ');
            if (ss.Length == 3) {
                return ss[0] + " " + ss[1] + " " + ss[2];
            } else {
                return ss[0] + " " + ss[1] + " " + ss[3];
            }
        }
        public static string ToOracleMonth(string month) {
            return ToOracleMonth(Int32.Parse(month));
        }
        public static string ToOracleMonth(int month) {
            switch (month) {
                case 1:
                default:
                    return "ม.ค.";
                case 2:
                    return "ก.พ.";
                case 3:
                    return "มี.ค.";
                case 4:
                    return "เม.ย.";
                case 5:
                    return "พ.ค.";
                case 6:
                    return "มิ.ย.";
                case 7:
                    return "ก.ก.";
                case 8:
                    return "ส.ค.";
                case 9:
                    return "ก.ย.";
                case 10:
                    return "ต.ค.";
                case 11:
                    return "พ.ย.";
                case 12:
                    return "ธ.ค.";
            }
        }
        public static string ToThaiWord(string s) {
            string[] ss = s.Split(' ');
            string s_day = ss[0];
            string s_month = ss[1];
            string s_year = ss[2];
            return "วันที่" + NormalizeThaiWord(NumberToThaiWord(s_day)) + "เดือน" + shortToLong(s_month) + "ปี" + NormalizeThaiWord(NumberToThaiWord(s_year));
        }
        public static string ToThaiMonth(int month) {
            switch (month) {
                case 1: return "มกราคม";
                case 2: return "กุมภาพันธ์";
                case 3: return "มีนาคม";
                case 4: return "เมษายน";
                case 5: return "พฤษภาคม";
                case 6: return "มิถุนายน";
                case 7: return "กรกฎาคม";
                case 8: return "สิงหาคม";
                case 9: return "กันยายน";
                case 10: return "ตุลาคม";
                case 11: return "พฤศจิกายน";
                case 12: return "ธันวาคม";
                default: return "[ERROR]";
            }
        }
        public static string ToThaiMonth(string s) {
            return ToThaiMonth(int.Parse(s));
        }
        public static string shortToLong(string s) {
            switch (s) {
                case "ม.ค.": return "มกราคม";
                case "ก.พ.": return "กุมภาพันธ์";
                case "มี.ค.": return "มีนาคม";
                case "เม.ย.": return "เมษายน";
                case "พ.ค.": return "พฤษภาคม";
                case "มิ.ย.": return "มิถุนายน";
                case "ก.ค.": return "กรกฎาคม";
                case "ส.ค.": return "สิงหาคม";
                case "ก.ย.": return "กันยายน";
                case "ต.ค.": return "ตุลาคม";
                case "พ.ย.": return "พฤศจิกายน";
                case "ธ.ค.": return "ธันวาคม";
                default: return "[ERROR]";
            }
        }
        public static string ToShortMonth(string full) {
            string[] split = full.Split(' ');
            switch (split[1]) {
                case "มกราคม": split[1] = "ม.ค."; break;
                case "กุมภาพันธ์": split[1] = "ก.พ."; break;
                case "มีนาคม": split[1] = "มี.ค."; break;
                case "เมษายน": split[1] = "เม.ย."; break;
                case "พฤษภาคม": split[1] = "พ.ค."; break;
                case "มิถุนายน": split[1] = "มิ.ย."; break;
                case "กรกฎาคม": split[1] = "ก.ค."; break;
                case "สิงหาคม": split[1] = "ส.ค."; break;
                case "กันยายน": split[1] = "ก.ย."; break;
                case "ตุลาคม": split[1] = "ต.ค."; break;
                case "พฤศจิกายน": split[1] = "พ.ย."; break;
                case "ธันวาคม": split[1] = "ธ.ค."; break;
                default: split[1] =  "[ERROR]"; break;
            }
            return split[0] + " " + split[1] + " " + split[2];
        }
        public static int MonthToNumber(string s) {

            switch (s) {
                case "ม.ค.": return 1;
                case "ก.พ.": return 2;
                case "มี.ค.": return 3;
                case "เม.ย.": return 4;
                case "พ.ค.": return 5;
                case "มิ.ย.": return 6;
                case "ก.ค.": return 7;
                case "ส.ค.": return 8;
                case "ก.ย.": return 9;
                case "ต.ค.": return 10;
                case "พ.ย.": return 11;
                case "ธ.ค.": return 12;
                default: return -1;
            }
        }
        public static string NormalizeThaiWord(string s) {
            s = s.Replace("หนึ่งสิบ", "สิบ");
            s = s.Replace("สองสิบ", "ยี่สิบ");
            s = s.Replace("สิบหนึ่ง", "สิบเอ็ด");
            return s;
        }
        public static string NumberToThaiWord(string s) {
            string sout = "";
            for (int i = 0; i < s.Length; ++i) {
                if (s[i] != '0')
                    sout += SingleNumberToThaiWord(s[i]) + ColumnNumberToThaiWord(s.Length - i - 1);
            }
            return sout;
        }
        public static string SingleNumberToThaiWord(char c) {
            switch (c) {
                case '0': return "ศูนย์";
                case '1': return "หนึ่ง";
                case '2': return "สอง";
                case '3': return "สาม";
                case '4': return "สี่";
                case '5': return "ห้า";
                case '6': return "หก";
                case '7': return "เจ็ด";
                case '8': return "แปด";
                case '9': return "เก้า";
                default: return "[ERROR]";
            }
        }
        public static string ColumnNumberToThaiWord(int column) {
            if (column == 0) {
                return "";
            } else {
                column = column % 6;
            }
            switch (column) {
                case 0: return "ล้าน";
                case 1: return "สิบ";
                case 2: return "ร้อย";
                case 3: return "พัน";
                case 4: return "หมื่น";
                case 5: return "แสน";
                default: return "[ERROR]";
            }
        }
        public static OracleConnection OC() {
            OracleConnection.ClearAllPools();
            OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING);
            con.Open();
            return con;
        }
        public static string CS() {
            return "DATA SOURCE=ORCL_RMUTTO;USER ID=RMUTTO;PASSWORD=Zxcvbnm;";
        }
        public static void Alert(Control control, string message) {
            string script2 = "alert('" + message + "');";
            ScriptManager.RegisterStartupScript(control, control.GetType(), "ServerControlScript", script2, true);
        }
        public static void RunScript(Control control, string script) {
            ScriptManager.RegisterClientScriptBlock(control, control.GetType(), "ServerControlScript", script, true);
        }

        //-------------------

        public static string ToOracleDateTime(DateTime dt) {
            string s = dt.ToString("dd/MM/yyyy");
            string[] s2 = s.Split('/');
            switch (s2[1]) {
                case "01": s2[1] = "ม.ค."; break;
                case "02": s2[1] = "ก.พ."; break;
                case "03": s2[1] = "มี.ค."; break;
                case "04": s2[1] = "เม.ย."; break;
                case "05": s2[1] = "พ.ค."; break;
                case "06": s2[1] = "มิ.ย."; break;
                case "07": s2[1] = "ก.ค."; break;
                case "08": s2[1] = "ส.ค."; break;
                case "09": s2[1] = "ก.ย."; break;
                case "10": s2[1] = "ต.ค."; break;
                case "11": s2[1] = "พ.ย."; break;
                case "12": s2[1] = "ธ.ค."; break;
            }
            return s2[0] + " " + s2[1] + " " + s2[2];
            //return s;
        }
        public static string DatabaseToDate(string s) {
            if (s == null) {
                return "''";
            }
            string[] ss = s.Split(' ');
            if (ss.Length != 3)
                return "''";
            switch (ss[1]) {
                case "ม.ค.": ss[1] = "01"; break;
                case "ก.พ.": ss[1] = "02"; break;
                case "มี.ค.": ss[1] = "03"; break;
                case "เม.ย.": ss[1] = "04"; break;
                case "พ.ค.": ss[1] = "05"; break;
                case "มิ.ย.": ss[1] = "06"; break;
                case "ก.ค.": ss[1] = "07"; break;
                case "ส.ค.": ss[1] = "08"; break;
                case "ก.ย.": ss[1] = "09"; break;
                case "ต.ค.": ss[1] = "10"; break;
                case "พ.ย.": ss[1] = "11"; break;
                case "ธ.ค.": ss[1] = "12"; break;
            }
            return "TO_DATE('" + ss[0] + "/" + ss[1] + "/" + (int.Parse(ss[2]) - 543) + "', 'DD/MM/YYYY')";
        }
        public static string TodayDatabaseToDate() {
            string s = "-";
            OracleConnection.ClearAllPools();
            using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING)) {
                con.Open();
                using (OracleCommand com = new OracleCommand("SELECT TO_CHAR(CURRENT_DATE, 'DD/MM/YYYY') FROM DUAL", con)) {
                    using (OracleDataReader reader = com.ExecuteReader()) {
                        while (reader.Read()) {
                            s = reader.GetValue(0).ToString();
                        }
                    }
                }
            }
            //string[] ss = s.Split('/');
            //s = ss[0] + " " + ss[1] + " " + (int.Parse(ss[2]) - 543);
            return "TO_DATE('" + s + "', 'DD/MM/YYYY')";
        }
        public static string PureDatabaseToThaiDate(string s) {
            if (s == null) {
                return "";
            }
            string[] ss = s.Split('/');
            if (ss.Length != 3)
                return "";
            switch (int.Parse(ss[1])) {
                case 1: ss[1] = "ม.ค."; break;
                case 2: ss[1] = "ก.พ."; break;
                case 3: ss[1] = "มี.ค."; break;
                case 4: ss[1] = "เม.ย."; break;
                case 5: ss[1] = "พ.ค."; break;
                case 6: ss[1] = "มิ.ย."; break;
                case 7: ss[1] = "ก.ค."; break;
                case 8: ss[1] = "ส.ค."; break;
                case 9: ss[1] = "ก.ย."; break;
                case 10: ss[1] = "ต.ค."; break;
                case 11: ss[1] = "พ.ย."; break;
                case 12: ss[1] = "ธ.ค."; break;
            }
            return int.Parse(ss[0]).ToString("00") + " " + ss[1] + " " + ss[2].Substring(0, 4);
        }
        public static void alertF(Page page, string message) {
            page.Page.ClientScript.RegisterStartupScript(page.GetType(), "ALERT", "alert('" + message + "')", true);
        }
        public static void alertF(MasterPage page, string message) {
            page.Page.ClientScript.RegisterStartupScript(page.GetType(), "ALERT", "alert('" + message + "')", true);
        }
        public static bool IsDateValid(string date) {
            try {
                string[] split = date.Split(' ');

                if (split.Length != 3) {
                    return false;
                }

                string day = split[0];
                string month = split[1];
                string year = split[2];

                int iDay = int.Parse(day);
                if (iDay < 1 || iDay > 31) {
                    return false;
                }
                switch (month) {
                    case "ม.ค.":
                    case "ก.พ.":
                    case "มี.ค.":
                    case "เม.ย.":
                    case "พ.ค.":
                    case "มิ.ย.":
                    case "ก.ค.":
                    case "ส.ค.":
                    case "ก.ย.":
                    case "ต.ค.":
                    case "พ.ย.":
                    case "ธ.ค.":
                        break;
                    default: return false;
                }
                int iYear = int.Parse(year);
                if (iYear < 0 || iYear > 9999) {
                    return false;
                }


                return true;
            } catch {
                return false;
            }
        }
        public static DateTime ToDateTime(string date) {
            string[] split = date.Split(' ');
            string day = split[0];
            string month = split[1];
            string year = split[2];
            string sMonth = "";
            switch (month) {
                case "ม.ค.": sMonth = "01"; break;
                case "ก.พ.": sMonth = "02"; break;
                case "มี.ค.": sMonth = "03"; break;
                case "เม.ย.": sMonth = "04"; break;
                case "พ.ค.": sMonth = "05"; break;
                case "มิ.ย.": sMonth = "06"; break;
                case "ก.ค.": sMonth = "07"; break;
                case "ส.ค.": sMonth = "08"; break;
                case "ก.ย.": sMonth = "09"; break;
                case "ต.ค.": sMonth = "10"; break;
                case "พ.ย.": sMonth = "11"; break;
                case "ธ.ค.": sMonth = "12"; break;
            }
            DateTime dt = new DateTime(int.Parse(year), int.Parse(sMonth), int.Parse(day));
            return dt;

        }
        public static DateTime ToDateTimeOracle(string date) {
            string[] split = date.Split(' ');
            string day = split[0];
            string month = split[1];
            string year = split[2];
            string sMonth = "";
            switch (month) {
                case "ม.ค.": sMonth = "01"; break;
                case "ก.พ.": sMonth = "02"; break;
                case "มี.ค.": sMonth = "03"; break;
                case "เม.ย.": sMonth = "04"; break;
                case "พ.ค.": sMonth = "05"; break;
                case "มิ.ย.": sMonth = "06"; break;
                case "ก.ค.": sMonth = "07"; break;
                case "ส.ค.": sMonth = "08"; break;
                case "ก.ย.": sMonth = "09"; break;
                case "ต.ค.": sMonth = "10"; break;
                case "พ.ย.": sMonth = "11"; break;
                case "ธ.ค.": sMonth = "12"; break;
            }
            DateTime dt = new DateTime(int.Parse(year)-543, int.Parse(sMonth), int.Parse(day));
            return dt;

        }
        public static string ToThaiMonthShort(string monthNum) {
            switch (int.Parse(monthNum)) {
                case 1: return "ม.ค.";
                case 2: return "ก.พ.";
                case 3: return "มี.ค.";
                case 4: return "เม.ย.";
                case 5: return "พ.ค.";
                case 6: return "มิ.ย.";
                case 7: return "ก.ค.";
                case 8: return "ส.ค.";
                case 9: return "ก.ย.";
                case 10: return "ต.ค.";
                case 11: return "พ.ย.";
                case 12: return "ธ.ค.";
                default: return "[error]";
            }
        }
        public static void NormalizeGridViewDate(GridView gv, int rowIndex) {
            for (int i = 0; i < gv.Rows.Count; ++i) {
                string s = gv.Rows[i].Cells[rowIndex].Text;
                string[] ss = s.Split('/');
                gv.Rows[i].Cells[rowIndex].Text = PureDatabaseToThaiDate(s);
            }
        }
        public static void NormalizeGridViewDate7D(GridView gv, int rowIndex) {
            for (int i = 0; i < gv.Rows.Count; ++i) {
                string s = gv.Rows[i].Cells[rowIndex].Text;
                string[] ss1 = s.Split(' ');
                string[] ss2 = ss1[0].Split('-');
                string year = ss2[0];
                string month = ToThaiMonthShort(ss2[1]);
                string day = ss2[2];
                string day7 = ss1[2];
                switch(day7) {
                    case "1": day7 = "อาทิตย์"; break;
                    case "2": day7 = "จันทร์"; break;
                    case "3": day7 = "อังคาร"; break;
                    case "4": day7 = "พุธ"; break;
                    case "5": day7 = "พฤหัสบดี"; break;
                    case "6": day7 = "ศุกร์"; break;
                    case "7": day7 = "เสาร์"; break;
                    default: day = "[error]"; break;
                }
                gv.Rows[i].Cells[rowIndex].Text = day + " " + month + " " + year + " " + day7;
            }
        }
        public static void NormalizeGridViewDate(GridView gv, int[] rowIndex) {
            for (int i = 0; i < gv.Rows.Count; ++i) {
                for (int j = 0; j < rowIndex.Length; ++j) {
                    string s = gv.Rows[i].Cells[rowIndex[j]].Text;
                    string[] ss = s.Split('/');
                    gv.Rows[i].Cells[rowIndex[j]].Text = PureDatabaseToThaiDate(s);
                }

            }
        }
        public static void GridViewAddCheckBox(GridView gv) {
            TableHeaderCell headerFrontCell = new TableHeaderCell();
            headerFrontCell.Text = "เลือก";
            gv.HeaderRow.Cells.AddAt(0, headerFrontCell);

            for (int i = 0; i < gv.Rows.Count; ++i) {
                TableCell frontCell = new TableCell();
                CheckBox cb = new CheckBox();
                frontCell.Controls.Add(cb);
                gv.Rows[i].Cells.AddAt(0, frontCell);
            }


        }
     
        public static string BirthdayToRetireDate(string birthday) {
            string[] ss = birthday.Split(' ');
            return "30 ก.ย. " + (int.Parse(ss[2]) + 60);
        }
        public static string MinusYear543(string date) {
            string[] ss = date.Split(' ');
            return ss[0] + " " + ss[1] + " " + (int.Parse(ss[2]));
        }
        public static string ToThaiWordBirthday(string birthday) {
            return ToThaiWord(MinusYear543(birthday));
        }
        public static string ToThaiWordRetire(string birthday) {
            return ToThaiWord(MinusYear543(BirthdayToRetireDate(birthday)));
        }

        public static string RandomPassword(int n) {
            Random r = new Random();
            string password = "";
            for (int i = 0; i < n; ++i) {
                password += r.Next(10);
            }
            return password;
        }

        public static string RandomString(int length) {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static string RandomFileName() {
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, 24)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static bool StringEqual(string source, string[] target) {
            for(int i=0; i<target.Length; ++i) {
                if(source == target[i]) {
                    return true;
                }
            }
            return false;
        }
        public static string DatabaseToDateSearch(string v1) {
            string[] v2 = v1.Split(' ');
            switch(v2[1]) {
                /*case "ม.ค.": v2[1] = "JAN"; break;
                case "ก.พ.": v2[1] = "FEB"; break;
                case "มี.ค.": v2[1] = "MAR"; break;
                case "เม.ย.": v2[1] = "APR"; break;
                case "พ.ค.": v2[1] = "MAY"; break;
                case "มิ.ย.": v2[1] = "JUN"; break;
                case "ก.ค.": v2[1] = "JUL"; break;
                case "ส.ค.": v2[1] = "AUG"; break;
                case "ก.ย.": v2[1] = "SEP"; break;
                case "ต.ค.": v2[1] = "OCT"; break;
                case "พ.ย.": v2[1] = "NOV"; break;
                case "ธ.ค.": v2[1] = "DEC"; break;*/
            }
            return "TO_DATE('" + v2[0] + " " + v2[1] + " " + (int.Parse(v2[2]) - 543) + "', 'DD MON YYYY')";
        }
        public static int BudgetYear() {
            DateTime dtToday = DateTime.Today;
            int budgetYear = dtToday.Year;
            if (dtToday.Month >= 10) {
                ++budgetYear;
            }
            return budgetYear;
        }

        public static bool CreateSelectPersonPageLoad(Page page, Panel pPerson, string pageURL) {

            string p = null;
            string ps = null;
            if (page.Request.QueryString["p"] != null) {
                p = page.Request.QueryString["p"];
            }
            if (page.Request.QueryString["ps"] != null) {
                ps = page.Request.QueryString["ps"];
            }

            if (ps != null) {
                CreateSelectPersonPanel(page, pPerson, pageURL, ps);
                return true;
            }
            if (p == null) {
                CreateSelectPersonPanel(page, pPerson, pageURL);
                return true;
            }
            return false;
        }
        public static void CreateSelectPersonPanel(Page page, Panel panel, string pageURL) {
            CreateSelectPersonPanel(page, panel, pageURL, null);
        }
        public static void CreateSelectPersonPanel(Page page, Panel panel, string pageURL, string ps) {
            Panel pp = new Panel();
            panel.Controls.Add(pp);

            {
                Panel ppp = new Panel();
                ppp.CssClass = "ps-div-title-red";
                pp.Controls.Add(ppp);

                Image img = new Image();
                img.Attributes["src"] = "Image/Small/Search.png";
                img.CssClass = "icon_left";
                ppp.Controls.Add(img);

                Label lb = new Label();
                lb.Text = "ค้นหารายชื่อพนักงาน";
                ppp.Controls.Add(lb);
            }
            {
                Panel ppp = new Panel();
                ppp.Style.Add("text-align", "center");
                ppp.DefaultButton = "pppSearch";
                pp.Controls.Add(ppp);

                Label lb = new Label();
                lb.Text = "รหัสบัตรประชาชน : ";
                ppp.Controls.Add(lb);

                TextBox tbSearchCitizenID = new TextBox();
                tbSearchCitizenID.Style.Add("margin-right", "5px");
                tbSearchCitizenID.CssClass = "ps-textbox";
                tbSearchCitizenID.MaxLength = 13;
                ppp.Controls.Add(tbSearchCitizenID);

                LinkButton lbuSearchPerson = new LinkButton();
                lbuSearchPerson.ID = "pppSearch";
                lbuSearchPerson.Style.Add("margin-right", "5px");
                lbuSearchPerson.CssClass = "ps-button";
                lbuSearchPerson.Click += (ee, eee) => {
                    page.Response.Redirect(pageURL + "?ps=" + tbSearchCitizenID.Text);
                };
                lbuSearchPerson.Text = "<img src='Image/Small/search.png' class='icon_left'/>ค้นหา";
                ppp.Controls.Add(lbuSearchPerson);

                LinkButton lbuRefresh = new LinkButton();
                lbuRefresh.CssClass = "ps-button";
                lbuRefresh.Click += (ee, eee) => {
                    page.Response.Redirect(pageURL);
                };
                lbuRefresh.Text = "<img src='Image/Small/refresh.png' class='icon_left'/>รีเฟรช";
                ppp.Controls.Add(lbuRefresh);

                Panel pSeparator = new Panel();
                pSeparator.CssClass = "ps-separator";
                ppp.Controls.Add(pSeparator);

            }


            Table tbPerson = new Table();
            tbPerson.CssClass = "ps-table-1";
            tbPerson.Style.Add("margin", "0 auto");
            pp.Controls.Add(tbPerson);
            {
                TableHeaderRow row = new TableHeaderRow();
                tbPerson.Rows.Add(row);
                {
                    TableHeaderCell cell = new TableHeaderCell();
                    cell.Text = "ลำดับ";
                    row.Cells.Add(cell);
                }
                {
                    TableHeaderCell cell = new TableHeaderCell();
                    cell.Text = "เลขประจำตัวประชาชน";
                    row.Cells.Add(cell);
                }
                {
                    TableHeaderCell cell = new TableHeaderCell();
                    cell.Text = "ชื่อ";
                    row.Cells.Add(cell);
                }
                {
                    TableHeaderCell cell = new TableHeaderCell();
                    cell.Text = "วิทยาเขต";
                    row.Cells.Add(cell);
                }
                {
                    TableHeaderCell cell = new TableHeaderCell();
                    cell.Text = "สำนัก / สถาบัน / คณะ";
                    row.Cells.Add(cell);
                }
                {
                    TableHeaderCell cell = new TableHeaderCell();
                    cell.Text = "กอง / สำนักงานเลขา / ภาควิชา";
                    row.Cells.Add(cell);
                }
                {
                    TableHeaderCell cell = new TableHeaderCell();
                    cell.Text = "งาน / ฝ่าย";
                    row.Cells.Add(cell);
                }
                {
                    TableHeaderCell cell = new TableHeaderCell();
                    cell.Text = "เลือก";
                    row.Cells.Add(cell);
                }
            }

            string select = "SELECT"
                + " PS_ID" //0
                + ", PS_CITIZEN_ID" //1
                + ", PS_FN_TH || ' ' || PS_LN_TH" //2
                + ", (SELECT CAMPUS_NAME FROM TB_CAMPUS WHERE PS_CAMPUS_ID = CAMPUS_ID)" //3
                + ", (SELECT FACULTY_NAME FROM TB_FACULTY WHERE PS_FACULTY_ID = FACULTY_ID)" //4
                + ", NVL((SELECT DIVISION_NAME FROM TB_DIVISION WHERE PS_DIVISION_ID = DIVISION_ID),'-')" //5
                + ", NVL((SELECT WORK_NAME FROM TB_WORK_DIVISION WHERE PS_WORK_DIVISION_ID = WORK_ID),'-')" //6
                + " FROM PS_PERSON";
            if (ps != null) {
                select += " WHERE PS_CITIZEN_ID = '" + ps + "'";
            }


            OracleConnection.ClearAllPools();
            using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING)) {
                con.Open();
                using (OracleCommand com = new OracleCommand(select, con)) {
                    using (OracleDataReader reader = com.ExecuteReader()) {
                        while (reader.Read()) {
                            string psID = reader.GetString(1);

                            TableRow row = new TableRow();
                            tbPerson.Rows.Add(row);

                            {
                                TableCell cell = new TableCell();
                                cell.Text = reader.GetInt32(0).ToString();
                                row.Cells.Add(cell);
                            }
                            {
                                TableCell cell = new TableCell();
                                cell.Text = reader.GetString(1);
                                row.Cells.Add(cell);
                            }
                            {
                                TableCell cell = new TableCell();
                                cell.Text = reader.GetString(2);
                                row.Cells.Add(cell);
                            }
                            {
                                TableCell cell = new TableCell();
                                cell.Text = reader.GetString(3);
                                row.Cells.Add(cell);
                            }
                            {
                                TableCell cell = new TableCell();
                                cell.Text = reader.GetString(4);
                                row.Cells.Add(cell);
                            }
                            {
                                TableCell cell = new TableCell();
                                cell.Text = reader.GetString(5);
                                row.Cells.Add(cell);
                            }
                            {
                                TableCell cell = new TableCell();
                                cell.Text = reader.GetString(6);
                                row.Cells.Add(cell);
                            }
                            {
                                TableCell cell = new TableCell();
                                LinkButton lbu = new LinkButton();
                                lbu.CssClass = "ps-button";
                                lbu.Text = "เลือก";
                                lbu.Click += (e1, e2) => {
                                    page.Response.Redirect(pageURL + "?p=" + psID + "&state=2");
                                };
                                cell.Controls.Add(lbu);
                                row.Cells.Add(cell);
                            }
                        }

                    }
                }

            }

            
        }

        public static bool IsBlank(string str) {
            if(str == null || str == "") {
                return true;
            }
            return false;
        }

    }



}