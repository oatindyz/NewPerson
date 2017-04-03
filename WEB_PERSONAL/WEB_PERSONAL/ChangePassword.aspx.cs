using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WEB_PERSONAL.Class;

namespace WEB_PERSONAL {
    public partial class ChangePassword : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {

        }

        protected void lbuFinish_Click(object sender, EventArgs e) {
            
            PersonnelSystem ps = PersonnelSystem.GetPersonnelSystem(this);
            Person pp = ps.LoginPerson;

            lbResult.Text = "";

            if (tbOld.Text != DatabaseManager.ExecuteString("SELECT PS_PASSWORD FROM PS_PERSON WHERE PS_CITIZEN_ID = '" + pp.PS_CITIZEN_ID + "'")) {
                lbResult.Text = "รหัสผ่านเก่าไม่ถูกต้อง";
                lbResult.ForeColor = System.Drawing.Color.Red;
                return;
            }
            if(tbNew.Text == "" || tbNew2.Text == "") {
                lbResult.Text = "กรุณากรอกรหัสใหม่ให้ครบถ้วน";
                lbResult.ForeColor = System.Drawing.Color.Red;
                return;
            }
            if (tbNew.Text != tbNew2.Text) {
                lbResult.Text = "รหัสผ่านใหม่ไม่ตรงกัน";
                lbResult.ForeColor = System.Drawing.Color.Red;
                return;
            }
            if(tbNew.Text == tbOld.Text || tbNew2.Text == tbOld.Text) {
                lbResult.Text = "รหัสผ่านไม่ควรซ้ำรหัสผ่านเดิม";
                lbResult.ForeColor = System.Drawing.Color.Red;
                return;
            }


            DatabaseManager.ExecuteNonQuery("UPDATE PS_PERSON SET PS_PASSWORD = '" + tbNew.Text + "' WHERE PS_CITIZEN_ID = '" + pp.PS_CITIZEN_ID + "'");
            lbResult.Text = "เปลี่ยนรหัสผ่านสำเร็จ";
            lbResult.ForeColor = System.Drawing.Color.Green;
        }
    }
}