﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WEB_PERSONAL.Class;
using System.Data.OracleClient;
using System.Security.Cryptography;
using System.Text;
using System.Net;
using System.Net.Mail;

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


                if (Request.QueryString["ID"] != null && Request.QueryString["Password"] != null && Request.QueryString["Action"] != null)
                {
                    if (DatabaseManager.ValidateUser(Request.QueryString["ID"], Util.ToDateTimeOracle(Server.UrlDecode(Request.QueryString["Password"]))))
                    {
                        ps.LoginPerson = DatabaseManager.GetPerson(Request.QueryString["ID"].ToString());
                        Session["PersonnelSystem"] = ps;
                        if (Request.QueryString["Action"] == "1")
                        {
                            Response.Redirect("ChangePassword.aspx");
                        }
                        else
                        {
                            Response.Redirect("Default.aspx");
                        }
                    }
                    else
                    {
                        LabelBottom.Text = "รหัสผ่านไม่ถูกต้อง!";
                    }
                }
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

        /*protected void tbUsername_TextChanged(object sender, EventArgs e)
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
                                        LabelTop.Text = "รหัสบัตรประชาชนดังกล่าวเป็นการล็อคอินครั้งแรก โปรดยืนยันตัวตน ด้วยการใส่รหัสผ่านเป็นวันเกิด";
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

        }*/

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

                using (OracleCommand com = new OracleCommand("SELECT ST_LOGIN_ID FROM PS_PERSON WHERE PS_CITIZEN_ID ='" + tbUsername.Text + "'", con)) {
                    using (OracleDataReader reader = com.ExecuteReader()) {
                        while (reader.Read()) {
                            if (!reader.IsDBNull(0)) {
                                int Login = reader.GetInt32(0);

                                if (tbUsername.Text.Length == 13) {
                                    if (Login == 0) {
                                        LabelTop.Text = "รหัสบัตรประชาชนดังกล่าวเป็นการล็อคอินครั้งแรก โปรดยืนยันตัวตน ด้วยการใส่รหัสผ่านเป็นวันเกิด รูปแบบ(01 ม.ค. 2500)";
                                        ScriptManager.GetCurrent(this.Page).SetFocus(this.tbPassword);
                                    }
                                    if (Login == 1) {
                                        LabelTop.Text = "";
                                        ScriptManager.GetCurrent(this.Page).SetFocus(this.tbPassword);
                                    }
                                }

                            }

                        }
                    }
                }

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
                                        if (!Util.IsDateValid(tbPassword.Text))
                                        {
                                            LabelBottom.Text = "รหัสผ่านไม่ถูกต้อง!";
                                            return;
                                        }
                                        if (DatabaseManager.ValidatePasswordFirst(tbUsername.Text, Util.ToDateTimeOracle(Server.UrlDecode(tbPassword.Text))))
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

                                            //
                                            //
                                            if (DatabaseManager.ExecuteInt("SELECT COUNT(*) FROM LEV_CLAIM WHERE PS_CITIZEN_ID = '" + ps.LoginPerson.PS_CITIZEN_ID + "' AND YEAR = " + Util.BudgetYear()) == 0)
                                            {
                                                using (OracleCommand com3 = new OracleCommand("INSERT INTO LEV_CLAIM (LEAVE_CLAIM_ID, PS_CITIZEN_ID, YEAR, SICK_NOW, SICK_REQ, BUSINESS_NOW, BUSINESS_REQ, GB_NOW, GB_REQ, REST_NOW, REST_REQ, REST_SAVE, REST_SAVE_FIX, REST_THIS, REST_THIS_FIX, REST_MAX, HGB_NOW, HGB_REQ, ORDAIN_NOW, ORDAIN_REQ, HUJ_NOW, HUJ_REQ, SICK_MAX, BUSINESS_MAX, HUJ_MAX, ORDAIN_MAX) VALUES (SEQ_LEV_CLAIM_ID.NEXTVAL, :PS_CITIZEN_ID, :YEAR, :SICK_NOW, :SICK_REQ, :BUSINESS_NOW, :BUSINESS_REQ, :GB_NOW, :GB_REQ, :REST_NOW, :REST_REQ, :REST_SAVE, :REST_SAVE_FIX, :REST_THIS, :REST_THIS_FIX, :REST_MAX, :HGB_NOW, :HGB_REQ, :ORDAIN_NOW, :ORDAIN_REQ, :HUJ_NOW, :HUJ_REQ, :SICK_MAX, :BUSINESS_MAX, :HUJ_MAX, :ORDAIN_MAX)", con))
                                                {

                                                    com3.Parameters.AddWithValue("PS_CITIZEN_ID", ps.LoginPerson.PS_CITIZEN_ID);
                                                    com3.Parameters.AddWithValue("YEAR", Util.BudgetYear());
                                                    int v1 = 0;
                                                    int v2 = 10;
                                                    int v60 = 60;
                                                    int v45 = 45;
                                                    int v120 = 120;
                                                    com3.Parameters.AddWithValue("SICK_NOW", v1);
                                                    com3.Parameters.AddWithValue("SICK_REQ", v1);
                                                    com3.Parameters.AddWithValue("BUSINESS_NOW", v1);
                                                    com3.Parameters.AddWithValue("BUSINESS_REQ", v1);
                                                    com3.Parameters.AddWithValue("GB_NOW", v1);
                                                    com3.Parameters.AddWithValue("GB_REQ", v1);
                                                    com3.Parameters.AddWithValue("REST_NOW", v1);
                                                    com3.Parameters.AddWithValue("REST_REQ", v1);
                                                    com3.Parameters.AddWithValue("REST_SAVE", v1);
                                                    com3.Parameters.AddWithValue("REST_SAVE_FIX", v1);
                                                    com3.Parameters.AddWithValue("REST_THIS", v2);
                                                    com3.Parameters.AddWithValue("REST_THIS_FIX", v2);
                                                    com3.Parameters.AddWithValue("REST_MAX", v2);
                                                    com3.Parameters.AddWithValue("HGB_NOW", v1);
                                                    com3.Parameters.AddWithValue("HGB_REQ", v1);
                                                    com3.Parameters.AddWithValue("ORDAIN_NOW", v1);
                                                    com3.Parameters.AddWithValue("ORDAIN_REQ", v1);
                                                    com3.Parameters.AddWithValue("HUJ_NOW", v1);
                                                    com3.Parameters.AddWithValue("HUJ_REQ", v1);
                                                    com3.Parameters.AddWithValue("SICK_MAX", v60);
                                                    com3.Parameters.AddWithValue("BUSINESS_MAX", v45);
                                                    com3.Parameters.AddWithValue("HUJ_MAX", v120);
                                                    com3.Parameters.AddWithValue("ORDAIN_MAX", v120);
                                                    com3.ExecuteNonQuery();
                                                }
                                            }

                                            //
                                            using (OracleCommand com4 = new OracleCommand("SELECT LEAVE_ID FROM LEV_DATA WHERE CURRENT_DATE >= FROM_DATE AND LEAVE_TYPE_ID IN(2,4,6,7) AND LEAVE_STATUS_ID = 1", con))
                                            {
                                                using (OracleDataReader reader4 = com.ExecuteReader())
                                                {
                                                    while (reader4.Read())
                                                    {
                                                        int leaveID = reader4.GetInt32(0);
                                                        LeaveData leaveData = new LeaveData();
                                                        leaveData.Load(leaveID);
                                                        leaveData.ExecuteCancelBySystem();
                                                    }
                                                }
                                            }

                                            using (OracleCommand com5 = new OracleCommand("SELECT LEAVE_ID FROM LEV_DATA WHERE LEAVE_STATUS_ID = 1 AND TRUNC(CURRENT_DATE - REQ_DATE, 0) >= 3", con))
                                            {
                                                using (OracleDataReader reader5 = com5.ExecuteReader())
                                                {
                                                    while (reader5.Read())
                                                    {
                                                        int leaveID = reader5.GetInt32(0);
                                                        LeaveData leaveData = new LeaveData();
                                                        leaveData.Load(leaveID);
                                                        leaveData.ExecuteCancelBySystem();
                                                    }
                                                }
                                            }


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

        protected void lbuForget_Click(object sender, EventArgs e)
        {
            using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
            {
                con.Open();
                using (OracleCommand com = new OracleCommand("SELECT PS_EMAIL FROM PS_PERSON WHERE PS_CITIZEN_ID = '" + tbCitizenID.Text + "' AND PS_EMAIL = '" + tbEmail.Text + "'", con))
                {
                    using (OracleDataReader reader = com.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader.IsDBNull(0))
                            {
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('ข้อมูลของรหัสบัตรประชาชน'" + tbCitizenID.Text + "' ไม่ตรงกับอีเมลในระบบ')", true);
                                return;
                            }
                            else
                            {
                                string psID;
                                string Birthday;

                                using (OracleCommand com1 = new OracleCommand("SELECT PS_ID,PS_BIRTHDAY_DATE FROM PS_PERSON WHERE PS_CITIZEN_ID = '" + tbCitizenID.Text + "' AND PS_EMAIL = '" + tbEmail.Text + "'", con))
                                {
                                    using (OracleDataReader reader1 = com1.ExecuteReader())
                                    {
                                        while (reader1.Read())
                                        {
                                            psID = reader1.GetInt32(0).ToString();
                                            Birthday = reader1.GetDateTime(1).ToShortDateString();
                                            string Reset = DatabaseManager.ExecuteString("UPDATE PS_PERSON SET PS_PASSWORD = '" + DBNull.Value + "', ST_LOGIN_ID = 0 WHERE PS_CITIZEN_ID = '" + psID + "'");

                                            var fromAddress = new MailAddress("zplaygiirlz1@hotmail.com", "From Name");
                                            var toAddress = new MailAddress(tbEmail.Text, "To Name");
                                            string fromPassword = "A1a2a3a4a5a6a7a8";
                                            string subject = "กู้คืนรหัสผ่านของระบบบุคลากรมหาวิทยาลัยราชมงคลตะวันออก";
                                            string body =
                                                "<div>เจ้าหน้าที่บุคลากรได้ทำการเพิ่มข้อมูลของคุณแล้ว</div>" +
                                                "<div style='border-bottom: 1px solid #c0c0c0' margin: 10px 0;></div>" +
                                                "<div><a href='http://localhost:12188/Access.aspx?ID=" + psID + "&Password=" + Util.ToOracleDateTime(DateTime.Parse(Birthday)) + "&Action=1'>รีเซ็ตรหัสผ่านได้ที่นี่</a></div>";

                                            var smtp = new SmtpClient
                                            {
                                                Host = "smtp.live.com",
                                                Port = 587,
                                                EnableSsl = true,
                                                DeliveryMethod = SmtpDeliveryMethod.Network,
                                                UseDefaultCredentials = false,
                                                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                                            };
                                            MailMessage ms = new MailMessage(fromAddress, toAddress);
                                            ms.IsBodyHtml = true;
                                            ms.Subject = subject;
                                            ms.Body = body;
                                            smtp.Send(ms);

                                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('ระบบได้ส่งการรีเซ็ตรหัสผ่านไปยังเมลของคุณแล้ว')", true);
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

