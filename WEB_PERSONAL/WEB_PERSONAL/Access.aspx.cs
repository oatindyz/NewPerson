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

namespace WEB_PERSONAL
{
    public partial class Access : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PersonnelSystem ps = new PersonnelSystem();
                ps.LoginPerson = DatabaseManager.GetPerson(tbUsername.Text);
                Session["PersonnelSystem"] = ps;
            }
            Session.Clear();
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

        protected void tbUsername_TextChanged(object sender, EventArgs e)
        {
            OracleConnection.ClearAllPools();
            using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
            {
                con.Open();
                using (OracleCommand com = new OracleCommand("SELECT ST_LOGIN_ID FROM PS_PERSON WHERE PS_CITIZEN_ID ='" + tbUsername.Text + "'", con))
                {
                    using (OracleDataReader reader = com.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (!reader.IsDBNull(0))
                            {
                                int Login = reader.GetInt32(0);

                                if (tbUsername.Text.Length == 13)
                                {
                                    if (Login == 0)
                                    {
                                        LabelTop.Text = "รหัสบัตรประชาชนดังกล่าวเป็นการล็อคอินครั้งแรก โปรดยืนยันตัวตน ด้วยการใส่รหัสผ่านเป็นวันเกิด รูปแบบ(01/01/2500)";
                                        ScriptManager.GetCurrent(this.Page).SetFocus(this.tbPassword);
                                    }
                                    if (Login == 1)
                                    {
                                        LabelTop.Text = "";
                                        ScriptManager.GetCurrent(this.Page).SetFocus(this.tbPassword);
                                    }
                                }

                            }

                        }
                    }
                }
                using (OracleCommand com2 = new OracleCommand("SELECT COUNT(*) FROM PS_PERSON WHERE PS_CITIZEN_ID ='" + tbUsername.Text + "'", con))
                {
                    using (OracleDataReader reader2 = com2.ExecuteReader())
                    {
                        while (reader2.Read())
                        {
                            if (reader2.GetInt32(0) == 0)
                            {
                                LabelBottom.Text = "ไม่พบผู้ใช้งาน!";
                                ScriptManager.GetCurrent(this.Page).SetFocus(this.tbUsername);
                                return;
                            }
                            else
                            {
                                LabelBottom.Text = "";
                                ScriptManager.GetCurrent(this.Page).SetFocus(this.tbPassword);
                            }
                        }
                    }
                }
            }

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbUsername.Text) || string.IsNullOrEmpty(tbPassword.Text))
            {
                LabelBottom.Text = "กรุณากรอกรหัสประชาชนและรหัสผ่าน";
                return;
            }
            int count = DatabaseManager.ExecuteInt("SELECT count(*) FROM PS_PERSON WHERE PS_CITIZEN_ID = '" + tbUsername.Text + "'");
            if (count == 0)
            {
                LabelBottom.Text = "ไม่พบผู้ใช้งาน!";
                return;
            }

            OracleConnection.ClearAllPools();
            using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
            {
                con.Open();
                int First = 0;
                int NotFirst = 1;
                using (OracleCommand com = new OracleCommand("SELECT ST_LOGIN_ID,PS_PASSWORD FROM PS_PERSON WHERE PS_CITIZEN_ID ='" + tbUsername.Text + "'", con))
                {
                    using (OracleDataReader reader = com.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string source = tbPassword.Text;
                            using (MD5 md5Hash = MD5.Create())
                            {
                                string hash = GetMd5Hash(md5Hash, source);

                                if (!reader.IsDBNull(0))
                                {
                                    if (reader.GetInt32(0) == First)
                                    {
                                        if (DatabaseManager.ValidatePasswordFirst(tbUsername.Text, tbPassword.Text))
                                        {
                                            PersonnelSystem ps = new PersonnelSystem();
                                            ps.LoginPerson = DatabaseManager.GetPerson(tbUsername.Text);
                                            Session["PersonnelSystem"] = ps;
                                            Response.Redirect("ChangePassword.aspx");
                                        }
                                        else
                                        {
                                            LabelBottom.Text = "รหัสผ่านไม่ถูกต้อง!";
                                            return;
                                        }
                                    }

                                    if (reader.GetInt32(0) == NotFirst)
                                    {
                                        if (reader.GetString(1) == hash.ToString())
                                        {
                                            PersonnelSystem ps = new PersonnelSystem();
                                            ps.LoginPerson = DatabaseManager.GetPerson(tbUsername.Text);
                                            Session["PersonnelSystem"] = ps;
                                            Response.Redirect("Default.aspx");
                                        }
                                        else
                                        {
                                            LabelBottom.Text = "รหัสผ่านไม่ถูกต้อง!";
                                            return;
                                        }
                                    }

                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
