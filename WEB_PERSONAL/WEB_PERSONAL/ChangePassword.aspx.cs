using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WEB_PERSONAL.Class;
using System.Data.OracleClient;
using System.Security.Cryptography;
using System.Text;

namespace WEB_PERSONAL {
    public partial class ChangePassword : System.Web.UI.Page {
        Person loginPerson;
        protected void Page_Load(object sender, EventArgs e) {
            PersonnelSystem ps = PersonnelSystem.GetPersonnelSystem(this);
            loginPerson = ps.LoginPerson;

            if(loginPerson.ST_LOGIN_ID == "0")
            {
                ShowOldPass.Visible = false;
                ShowNewPass.Visible = true;
            }
            else if(loginPerson.ST_LOGIN_ID == "1")
            {
                ShowOldPass.Visible = true;
                ShowNewPass.Visible = true;
            }
        }

        static string GetMd5Hash(MD5 md5Hash, string input)
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        static bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
        {
            string hashOfInput = GetMd5Hash(md5Hash, input);

            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected void lbuFinish_Click(object sender, EventArgs e) {
            
            PersonnelSystem ps = PersonnelSystem.GetPersonnelSystem(this);
            Person pp = ps.LoginPerson;

            lbResult.Text = "";

            if (loginPerson.ST_LOGIN_ID != "0")
            {
                if (tbOld.Text != DatabaseManager.ExecuteString("SELECT PS_PASSWORD FROM PS_PERSON WHERE PS_CITIZEN_ID = '" + pp.PS_CITIZEN_ID + "'"))
                {
                    lbResult.Text = "รหัสผ่านเก่าไม่ถูกต้อง";
                    lbResult.ForeColor = System.Drawing.Color.Red;
                    return;
                }
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

            string source = tbNew.Text;
            using (MD5 md5Hash = MD5.Create())
            {
                string hash = GetMd5Hash(md5Hash, source);
                DatabaseManager.ExecuteNonQuery("UPDATE PS_PERSON SET PS_PASSWORD = '" + hash.ToString() + "',ST_LOGIN_ID = 1 WHERE PS_CITIZEN_ID = '" + pp.PS_CITIZEN_ID + "'");
                lbResult.Text = "เปลี่ยนรหัสผ่านสำเร็จ";
                lbResult.ForeColor = System.Drawing.Color.Green;
            }
        }

    }
}