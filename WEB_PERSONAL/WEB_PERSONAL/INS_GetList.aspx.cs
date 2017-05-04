using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WEB_PERSONAL.Class;
using System.Data.OracleClient;

namespace WEB_PERSONAL {
    public partial class INS_GetList : System.Web.UI.Page {
        private Person loginPerson;
        protected void Page_Load(object sender, EventArgs e) {
            FuncGVHistory();
        }

        protected void gvHistory_PageIndexChanging(object sender, GridViewPageEventArgs e) {
            gvHistory.PageIndex = e.NewPageIndex;
            FuncGVHistory();
        }
               
        private void FuncGVHistory() {
            OracleConnection.ClearAllPools();
            SqlDataSource sds = DatabaseManager.CreateSQLDataSource("SELECT IP_ID รหัสการขอเครื่องราช, (SELECT  PS_FIRSTNAME || ' ' || PS_LASTNAME FROM PS_PERSON WHERE PS_CITIZEN_ID = CITIZEN_ID) ชื่อผู้ขอ, (SELECT (SELECT CAMPUS_NAME FROM TB_CAMPUS WHERE TB_CAMPUS.CAMPUS_ID = PS_PERSON.PS_CAMPUS_ID) FROM PS_PERSON WHERE PS_PERSON.PS_CITIZEN_ID = TB_INSIG_PERSON.CITIZEN_ID) วิทยาเขต, REQ_DATE วันที่ขอ, (SELECT INSIG_GRADE_NAME_L FROM TB_INSIG_GRADE WHERE INSIG_GRADE_ID = INSIG_ID) ระดับชั้นเครื่องราชที่ขอ, (SELECT IP_STATUS_NAME FROM TB_INSIG_PERSON_STATUS WHERE TB_INSIG_PERSON_STATUS.IP_STATUS_ID = TB_INSIG_PERSON.IP_STATUS_ID) สถานะ,NVL(I_ALLOW,0) ผลการอนุมัติ, GET_DATE วันที่อนุมัติ FROM TB_INSIG_PERSON WHERE IP_STATUS_ID IN(2,3) ORDER BY IP_ID DESC");
            gvHistory.DataSource = sds;
            gvHistory.DataBind();
            Util.NormalizeGridViewDate(gvHistory, 3);
            Util.NormalizeGridViewDate(gvHistory, 7);
            if (gvHistory.Rows.Count > 0) {
                lbHistory.Visible = false;

                for (int i = 0; i < gvHistory.Rows.Count; ++i)
                {
                    if (Util.StringEqual(gvHistory.Rows[i].Cells[6].Text, new string[] { "0" }))
                    {
                        gvHistory.Rows[i].Cells[6].Text = "-";
                        gvHistory.Rows[i].Cells[6].ForeColor = System.Drawing.Color.Black;
                    }
                    if (Util.StringEqual(gvHistory.Rows[i].Cells[6].Text, new string[] { "2" }))
                    {
                        gvHistory.Rows[i].Cells[6].Text = "ไม่ได้รับ";
                        gvHistory.Rows[i].Cells[6].ForeColor = System.Drawing.Color.Red;
                    }
                    if (Util.StringEqual(gvHistory.Rows[i].Cells[6].Text, new string[] { "1" }))
                    {
                        gvHistory.Rows[i].Cells[6].Text = "ได้รับ";
                        gvHistory.Rows[i].Cells[6].ForeColor = System.Drawing.Color.Green;
                    }
                }
            } else {
                lbHistory.Visible = true;
            }
        }
    }
}