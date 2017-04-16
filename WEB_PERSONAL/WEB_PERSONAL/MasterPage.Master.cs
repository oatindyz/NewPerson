using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using WEB_PERSONAL.Class;
using System.Data.OracleClient;

namespace WEB_PERSONAL
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {

        protected void Page_Init(object sender, EventArgs e)
        {
            if (PersonnelSystem.GetPersonnelSystem(this) == null)
            {
                Response.Redirect("Access.aspx");
                return;
            }
            Session.Timeout = 60;
            OracleConnection.ClearAllPools();

            PersonnelSystem ps = PersonnelSystem.GetPersonnelSystem(this);
            Person loginPerson = ps.LoginPerson;
            ps.LoginPerson = DatabaseManager.GetPerson(loginPerson.PS_CITIZEN_ID);

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            PersonnelSystem ps = PersonnelSystem.GetPersonnelSystem(this);
            Person loginPerson = ps.LoginPerson;
            ps.LoginPerson = DatabaseManager.GetPerson(loginPerson.PS_CITIZEN_ID);


            lbMasterName.Text = loginPerson.FirstNameAndLastName;
            lbMasterHeaderName.Text = loginPerson.FirstNameAndLastName;
        
            string name = loginPerson.FirstNameAndLastName;
            //profile_name.InnerText = name;

            if (loginPerson.ST_LOGIN_ID == "0")
            {
                hv1.Visible = false;
                hv2.Visible = false;
                hv3.Visible = false;
                hv4.Visible = false;
                hv5.Visible = false;
                hv6.Visible = false;
                hv7.Visible = false;
            }

            if (loginPerson.PERSON_ROLE_ID == "1")
            {
                hv5.Visible = true;
                hv7.Visible = true;
                hv6.Visible = true;
            }
            else if (loginPerson.PERSON_ROLE_ID == "2")
            {
                hv1.Visible = true;
                hv2.Visible = true;
                hv6.Visible = true;
            }
            else if (loginPerson.PERSON_ROLE_ID == "3")
            {
                hv5.Visible = true;
                hv2.Visible = true;
                hv6.Visible = true;
            }
            else if (loginPerson.PERSON_ROLE_ID == "4")
            {
                hv5.Visible = true;
                hv7.Visible = true;
                hv3.Visible = true;
            }
            else if (loginPerson.PERSON_ROLE_ID == "5")
            {

            }
            else if (loginPerson.PERSON_ROLE_ID == "99")
            {
                hv4.Visible = true;
            }

            //---------
            int count_approve = 0;
            int count_leave_finish = 0;
            int count_ins = 0;
            int count_get_ins = 0;

            OracleConnection.ClearAllPools();
            using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
            {
                con.Open();

                using (OracleCommand com = new OracleCommand("SELECT URL FROM PS_PERSON_IMAGE WHERE CITIZEN_ID = '" + loginPerson.PS_CITIZEN_ID + "' AND PRESENT = 1", con))
                {
                    using (OracleDataReader reader = com.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string fileName;
                            fileName = reader.GetValue(0).ToString();
                            string personImageFileName = DatabaseManager.GetPersonImageFileName(loginPerson.PS_CITIZEN_ID);
                            if (personImageFileName != "")
                            {
                                //profile_pic.Src = "Upload/PersonImage/" + personImageFileName;
                                //profile_pic2.Src = "Upload/PersonImage/" + personImageFileName;
                                profile_pic3.Src = "Upload/PersonImage/" + personImageFileName;
                            }
                            else
                            {
                                //profile_pic.Src = "Image/Small/person2.png";
                            }
                        }
                    }
                }


                using (OracleCommand com = new OracleCommand("SELECT COUNT(LEV_BOSS_DATA.LEAVE_BOSS_ID) FROM LEV_DATA, LEV_BOSS_DATA WHERE LEAVE_STATUS_ID IN(1,4) AND LEV_DATA.LEAVE_ID = LEV_BOSS_DATA.LEAVE_ID AND LEV_DATA.BOSS_STATE = LEV_BOSS_DATA.STATE AND LEV_BOSS_DATA.CITIZEN_ID = '" + loginPerson.PS_CITIZEN_ID + "'", con))
                {
                    using (OracleDataReader reader = com.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            count_approve = reader.GetInt32(0);
                        }
                    }
                }
                if (count_approve != 0)
                {
                    lbLeaveAllowCount.Text = "" + count_approve;
                    lbLeaveAllowCount.Visible = true;
                }
                else
                {
                    lbLeaveAllowCount.Text = "";
                    lbLeaveAllowCount.Visible = false;
                }

                using (OracleCommand com = new OracleCommand("SELECT COUNT(LEAVE_ID) FROM LEV_DATA WHERE PS_ID = '" + loginPerson.PS_CITIZEN_ID + "' AND LEAVE_STATUS_ID in(2,5)", con))
                {
                    using (OracleDataReader reader = com.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            count_leave_finish = reader.GetInt32(0);
                        }
                    }
                }
                /*using (OracleCommand com = new OracleCommand("SELECT COUNT(IR_ID) FROM TB_INSIG_REQUEST WHERE IR_CITIZEN_ID = '" + loginPerson.PS_CITIZEN_ID + "' AND IR_STATUS = 1", con)) {
                    using (OracleDataReader reader = com.ExecuteReader()) {
                        while (reader.Read()) {
                            count_ins = reader.GetInt32(0);
                        }
                    }
                }
                using (OracleCommand com = new OracleCommand("SELECT COUNT(IR_ID) FROM TB_INSIG_REQUEST WHERE IR_CITIZEN_ID = '" + loginPerson.PS_CITIZEN_ID + "' AND IR_STATUS = 3", con)) {
                    using (OracleDataReader reader = com.ExecuteReader()) {
                        while (reader.Read()) {
                            count_get_ins = reader.GetInt32(0);
                        }
                    }
                }*/


                int count = count_approve + count_leave_finish + count_ins + count_get_ins;

                noti_leave_none.Visible = false;
                noti_approve.Visible = false;
                noti_leave_finish.Visible = false;

                noti_ins_none.Visible = false;
                noti_ins.Visible = false;
                noti_get_ins.Visible = false;

                if (count_approve + count_leave_finish == 0)
                {
                    noti_leave_none.Visible = true;
                }
                else
                {
                    if (count_approve != 0)
                    {
                        noti_approve.Visible = true;
                    }
                    if (count_leave_finish != 0)
                    {
                        noti_leave_finish.Visible = true;
                    }
                }

                if (count_ins + count_get_ins == 0)
                {
                    noti_ins_none.Visible = true;
                }
                else
                {
                    if (count_ins != 0)
                    {
                        noti_ins.Visible = true;
                    }
                    if (count_get_ins != 0)
                    {
                        noti_get_ins.Visible = true;
                    }
                }

                if (count > 0)
                {
                    noti_alert.InnerText = "" + count;
                    noti_alert.Attributes["class"] = "ps-ms-main-hd-noti-alert";
                }

                //--Permission--

                /*WorkingDay.Visible = false;
                //LeaveReport.Visible = false;
                cbAddPerson1.Visible = false;
                cbAddPerson2.Visible = false;
                cbAddPerson3.Visible = false;
                cbAddPerson6.Visible = false;
                cbAddInsig1.Visible = false;
                cbAddInsig2.Visible = false;
                
                cbAddManage1.Visible = false;
                cbAddManage2.Visible = false;
                cbPersonPosition.Visible = false;
                

                using (OracleCommand com = new OracleCommand("SELECT PERMISSION_TYPE FROM TB_PERMISSION WHERE CITIZEN_ID = '" + loginPerson.PS_CITIZEN_ID + "'", con)) {
                    using (OracleDataReader reader = com.ExecuteReader()) {
                        while (reader.Read()) {
                            int type = reader.GetInt32(0);
                            if (type == 1) WorkingDay.Visible = true;
                            //else if (type == 2) LeaveReport.Visible = true;
                            else if (type == 3) cbAddPerson1.Visible = true;
                            else if (type == 4) cbAddPerson2.Visible = true;
                            else if (type == 5) cbAddPerson3.Visible = true;
                            //else if (type == 6) cbAddPerson4.Visible = true;
                            else if (type == 8) cbAddPerson6.Visible = true;
                            else if (type == 9) cbAddInsig1.Visible = true;
                            else if (type == 10) cbAddInsig2.Visible = true;
                            //else if (type == 11) cbAddInsig4.Visible = true;
                            else if (type == 12) cbAddManage1.Visible = true;
                            else if (type == 13) cbAddManage2.Visible = true;
                            else if (type == 14) cbPersonPosition.Visible = true;
                            //else if (type == 15) cbPosition.Visible = true;
                            

                        }
                    }
                }*/

                if (!IsPostBack)
                {

                }

            }


        }

        protected void lbuLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Access.aspx");
        }

        protected void lbuUser_Click(object sender, EventArgs e)
        {
            Response.Redirect("Profile.aspx");
        }
    }
}