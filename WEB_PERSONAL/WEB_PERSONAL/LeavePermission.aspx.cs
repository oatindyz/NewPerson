using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WEB_PERSONAL.Class;
using System.Data.OracleClient;

namespace WEB_PERSONAL {
    public partial class LeavePermission : System.Web.UI.Page {

        protected void Page_Load(object sender, EventArgs e) {
            
            using(OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING)) {
                con.Open();
                using(OracleCommand com = new OracleCommand("SELECT (SELECT WM_CONCAT(ADMIN_POSITION_NAME) FROM TB_ADMIN_POSITION WHERE ADMIN_POSITION_POWER = LEV_PERMISSION.ADMIN_POSITION_POWER GROUP BY ADMIN_POSITION_POWER) ระดับ, REPLACE(PUY_MAX, 999, 'ตามที่เห็นสมควร') ลาป่วย, REPLACE(KIJ_MAX, 999, 'ตามที่เห็นสมควร') ลากิจ, FUNC_LEV_PM(GIVE_BIRTH) ลาคลอดบุตร, FUNC_LEV_PM(HELP_GIVE_BIRTH) ลาไปช่วยเหลือภริยาที่คลอดบุตร, FUNC_LEV_PM(REST) ลาพักผ่อน, FUNC_LEV_PM(ORDAIN_HUJ) ลาไปอุปสมบทหรือประกอบพิธีฮัจย์ FROM LEV_PERMISSION", con)) {
                    using(OracleDataReader reader = com.ExecuteReader()) {
                        while(reader.Read()) {
                            TableRow r = new TableRow();
                            TableCell c;

                            for (int i = 0; i < 7; i++) {
                                c = new TableCell();
                                string txt = reader.GetString(i);
                                if(txt == "มีอำนาจ") {
                                    c.Text = "<img src='Image/Small/correct.png' />";
                                } else if(txt == "ไม่มีอำนาจ"){
                                    c.Text = "<img src='Image/Small/delete.png' />";
                                } else {
                                    c.Text = txt;
                                }
                                r.Cells.Add(c);
                            }

                            Table1.Rows.Add(r);
                        }
                    }
                }
            }
        }

    }
}