using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OracleClient;
using WEB_PERSONAL.Class;

namespace WEB_PERSONAL {
    public partial class Permission : System.Web.UI.Page {

        protected void Page_Load(object sender, EventArgs e) {

            PersonnelSystem ps = PersonnelSystem.GetPersonnelSystem(this);
            Person loginPerson = ps.LoginPerson;
            if (loginPerson.PERSON_ROLE_ID != "99")
            {
                Server.Transfer("NoPermission.aspx");
            }

            lbSaveComplete.Visible = false;
        }

        protected void lbuSearch_Click(object sender, EventArgs e) {
            d1.Style.Add("display", "none");
            d2.Style.Add("display", "none");
            bool found = false;
            using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING)) {
                con.Open();
                using (OracleCommand com = new OracleCommand("SELECT PS_FIRSTNAME || ' ' || PS_LASTNAME FROM PS_PERSON WHERE PS_CITIZEN_ID = '" + tbCitizenID.Text + "'", con)) {
                    using (OracleDataReader reader = com.ExecuteReader()) {
                        while (reader.Read()) {
                            found = true;
                            lbName.Text = reader.GetString(0);
                        }
                    }
                }
                if (found) {
                    hfCitizenID.Value = tbCitizenID.Text;
                    d1.Style.Add("display", "block");
                    cb1.Checked = false;
                    //cb2.Checked = false;
                    using (OracleCommand com = new OracleCommand("SELECT * FROM TB_PERMISSION WHERE CITIZEN_ID = '" + tbCitizenID.Text + "'", con)) {
                        using (OracleDataReader reader = com.ExecuteReader()) {
                            while (reader.Read()) {
                                int type = reader.GetInt32(2);
                                if (type == 1) { cb1.Checked = true; }
                                //else if (type == 2) { cb2.Checked = true; }
                                else if (type == 3) { cbAddPerson1.Checked = true; }
                                else if (type == 4) { cbAddPerson2.Checked = true; }
                                else if (type == 5) { cbAddPerson3.Checked = true; }
                                //else if (type == 6) { cbAddPerson4.Checked = true; }
                                //else if (type == 7) { cbAddPerson5.Checked = true; }
                                else if (type == 8) { cbAddPerson6.Checked = true; }
                                else if (type == 9) { cbAddInsig1.Checked = true; }
                                else if (type == 10) { cbAddInsig2.Checked = true; }
                                //else if (type == 11) { cbAddInsig4.Checked = true; }
                                else if (type == 12) { cbAddManage1.Checked = true; }
                                else if (type == 13) { cbAddManage2.Checked = true; }
                                else if (type == 14) { cbPersonPosition.Checked = true; }
                                //else if (type == 15) { cbPosition.Checked = true; }
                                //else if (type == 16) { cbStatusPerson.Checked = true; }
                            }
                        }
                    }
                } else {
                    d2.Style.Add("display", "block");
                }

            }

        }

        protected void lbuSave_Click(object sender, EventArgs e) {
            string citizenID = hfCitizenID.Value;
            lbSaveComplete.Visible = true;
            using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING)) {
                con.Open();
                Exe(cb1, citizenID, 1);
                //Exe(cb2, citizenID, 2);
                Exe(cbAddPerson1, citizenID, 3);
                Exe(cbAddPerson2, citizenID, 4);
                Exe(cbAddPerson3, citizenID, 5);
                //Exe(cbAddPerson4, citizenID, 6);
                //Exe(cbAddPerson5, citizenID, 7);
                Exe(cbAddPerson6, citizenID, 8);
                Exe(cbAddInsig1, citizenID, 9);
                Exe(cbAddInsig2, citizenID, 10);
                //Exe(cbAddInsig4, citizenID, 11);
                Exe(cbAddManage1, citizenID, 12);
                Exe(cbAddManage2, citizenID, 13);
                Exe(cbPersonPosition, citizenID, 14);
                //Exe(cbPosition, citizenID, 15);
                //Exe(cbStatusPerson, citizenID, 16);

                Page.Response.Redirect(Page.Request.Url.ToString(), true);

                /*if (cb1.Checked) {
                    bool have = false;
                    using (OracleCommand com = new OracleCommand("SELECT * FROM TB_PERMISSION WHERE CITIZEN_ID = '" + citizenID + "' AND PERMISSION_TYPE = 1", con)) {
                        using (OracleDataReader reader = com.ExecuteReader()) {
                            while (reader.Read()) {
                                have = true;
                            }
                        }
                    }
                    if (!have) {
                        using (OracleCommand com = new OracleCommand("INSERT INTO TB_PERMISSION VALUES(SEQ_PERMISSION_ID.NEXTVAL, :PS, 1)", con)) {
                            com.Parameters.Add("PS", citizenID);
                            com.ExecuteNonQuery();
                        }
                    }
                } else {
                    using (OracleCommand com = new OracleCommand("DELETE LEV_ACT WHERE PS_CITIZEN_ID = :PS AND TYPE = 1", con)) {
                        com.Parameters.Add("PS", citizenID);
                        com.ExecuteNonQuery();
                    }
                }

                if (cb2.Checked) {
                    bool have = false;
                    using (OracleCommand com = new OracleCommand("SELECT * FROM LEV_ACT WHERE PS_CITIZEN_ID = '" + citizenID + "' AND TYPE = 2", con)) {
                        using (OracleDataReader reader = com.ExecuteReader()) {
                            while (reader.Read()) {
                                have = true;
                            }
                        }
                    }
                    if (!have) {
                        using (OracleCommand com = new OracleCommand("INSERT INTO LEV_ACT VALUES(SEQ_LEAVE_ACT_ID.NEXTVAL, :PS, 2)", con)) {
                            com.Parameters.Add("PS", citizenID);
                            com.ExecuteNonQuery();
                        }
                    }
                } else {
                    using (OracleCommand com = new OracleCommand("DELETE LEV_ACT WHERE PS_CITIZEN_ID = :PS AND TYPE = 2", con)) {
                        com.Parameters.Add("PS", citizenID);
                        com.ExecuteNonQuery();
                    }
                }*/


            }
        }

        private void Exe(CheckBox cb, string citizenID, int type) {
            using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING)) {
                con.Open();
                if (cb.Checked) {
                    bool have = false;
                    using (OracleCommand com = new OracleCommand("SELECT * FROM TB_PERMISSION WHERE CITIZEN_ID = '" + citizenID + "' AND PERMISSION_TYPE = " + type, con)) {
                        using (OracleDataReader reader = com.ExecuteReader()) {
                            while (reader.Read()) {
                                have = true;
                            }
                        }
                    }
                    if (!have) {
                        using (OracleCommand com = new OracleCommand("INSERT INTO TB_PERMISSION VALUES(SEQ_PERMISSION_ID.NEXTVAL, :PS, " + type + ")", con)) {
                            com.Parameters.AddWithValue("PS", citizenID);
                            com.ExecuteNonQuery();
                        }
                    }
                } else {
                    using (OracleCommand com = new OracleCommand("DELETE TB_PERMISSION WHERE CITIZEN_ID = :PS AND PERMISSION_TYPE = " + type, con)) {
                        com.Parameters.AddWithValue("PS", citizenID);
                        com.ExecuteNonQuery();
                    }
                }
            }
                
        }

    }
}