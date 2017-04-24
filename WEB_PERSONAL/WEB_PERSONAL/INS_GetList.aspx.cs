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
            SqlDataSource sds = DatabaseManager.CreateSQLDataSource("SELECT IP_ID รหัสการขอเครื่องราช, (SELECT  PS_FIRSTNAME || ' ' || PS_LASTNAME FROM PS_PERSON WHERE PS_CITIZEN_ID = CITIZEN_ID) ชื่อผู้ขอเครื่องราช, (SELECT INSIG_GRADE_NAME_L FROM TB_INSIG_GRADE WHERE INSIG_GRADE_ID = INSIG_ID) ระดับชั้นเครื่องราชที่ขอ, REQ_DATE วันที่ข้อมูล, (SELECT IP_STATUS_NAME FROM TB_INSIG_PERSON_STATUS WHERE TB_INSIG_PERSON_STATUS.IP_STATUS_ID = TB_INSIG_PERSON.IP_STATUS_ID) สถานะ FROM TB_INSIG_PERSON WHERE IP_STATUS_ID IN(2,3)");
            gvHistory.DataSource = sds;
            gvHistory.DataBind();

            if (gvHistory.Rows.Count > 0) {
                lbHistory.Visible = false;


            } else {
                lbHistory.Visible = true;
            }
        }
    }
}