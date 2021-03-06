﻿using System;
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
            if (loginPerson != null)
            {
                ps.LoginPerson = DatabaseManager.GetPerson(loginPerson.PS_CITIZEN_ID);
            }
            

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

            if (loginPerson.PERSON_ROLE_ID == "1")
            {
                hv5.Visible = true;
                hv7.Visible = true;
                hv6.Visible = true;
            }
            else if (loginPerson.PERSON_ROLE_ID == "2")
            {
                hv1.Visible = true;
                hv7.Visible = true;
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

            //---------
            int count_approve = 0;
            int count_leave_finish = 0;
            int count_ins = 0;
            int count_get_ins = 0;
            int count_req_ins = 0;
            int count_person_edit = 0;
            int count_person_get = 0;

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

                if (loginPerson.ST_LOGIN_ID != "0")
                {
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
                        lbLeaveAllowCount2.Text = "" + count_approve;
                        lbLeaveAllowCount2.Visible = true;
                    }
                    else
                    {
                        lbLeaveAllowCount.Text = "";
                        lbLeaveAllowCount.Visible = false;
                        lbLeaveAllowCount2.Text = "";
                        lbLeaveAllowCount2.Visible = false;
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
                    }*/
                    using (OracleCommand com = new OracleCommand("SELECT COUNT(IP_ID) FROM TB_INSIG_PERSON WHERE CITIZEN_ID = '" + loginPerson.PS_CITIZEN_ID + "' AND IP_STATUS_ID IN(2, 4)", con))
                    {
                        using (OracleDataReader reader = com.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                count_get_ins = reader.GetInt32(0);
                            }
                        }
                    }
                    if (InsigCheckGet.Check(loginPerson))
                    {
                        count_ins = 1;
                    }
                    if (loginPerson.PERSON_ROLE_ID == "4")
                    {
                        using (OracleCommand com = new OracleCommand("SELECT COUNT(IP_ID) FROM TB_INSIG_PERSON WHERE TB_INSIG_PERSON.IP_STATUS_ID = 1", con))
                        {
                            using (OracleDataReader reader = com.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    count_req_ins = reader.GetInt32(0);
                                }
                            }
                        }
                    }
                    if (count_req_ins != 0)
                    {
                        lbInsigCount.Text = "" + count_req_ins;
                        lbInsigCount.Visible = true;
                    }
                    else
                    {
                        lbInsigCount.Text = "";
                        lbInsigCount.Visible = false;
                    }

                    //Person-get
                    if (loginPerson.PERSON_ROLE_ID == "2")
                    {
                        using (OracleCommand com = new OracleCommand("SELECT COUNT(R_ID) FROM TB_REQUEST WHERE TB_REQUEST.R_STATUS_ID = 1", con))
                        {
                            using (OracleDataReader reader = com.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    count_person_edit = reader.GetInt32(0);
                                }
                            }
                        }
                    }
                    if (count_person_edit != 0)
                    {
                        lbPersonRequestCount.Text = "" + count_person_edit;
                        lbPersonRequestCount.Visible = true;
                    }
                    else
                    {
                        lbPersonRequestCount.Text = "";
                        lbPersonRequestCount.Visible = false;
                    }

                    //person-finish
                    using (OracleCommand com = new OracleCommand("SELECT COUNT(R_ID) FROM TB_REQUEST WHERE CITIZEN_ID = '" + loginPerson.PS_CITIZEN_ID + "' AND R_STATUS_ID IN(2, 4)", con))
                    {
                        using (OracleDataReader reader = com.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                count_person_get = reader.GetInt32(0);
                            }
                        }
                    }

                    int count = count_approve + count_leave_finish + count_ins + count_get_ins + count_req_ins + count_person_edit + count_person_get;

                    noti_person_none.Visible = false;
                    noti_person_request.Visible = false;
                    noti_person_finish.Visible = false;

                    noti_leave_none.Visible = false;
                    noti_approve.Visible = false;
                    noti_leave_finish.Visible = false;

                    noti_ins_none.Visible = false;
                    noti_ins.Visible = false;
                    noti_get_ins.Visible = false;
                    noti_req_ins.Visible = false;

                    if (count_person_edit + count_person_get == 0)
                    {
                        noti_person_none.Visible = true;
                    }
                    else
                    {
                        if (count_person_edit != 0)
                        {
                            noti_person_request.Visible = true;
                        }
                        if (count_person_get != 0)
                        {
                            noti_person_finish.Visible = true;
                        }
                    }

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

                    if (count_ins + count_get_ins + count_req_ins == 0)
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
                        if (count_req_ins != 0)
                        {
                            noti_req_ins.Visible = true;
                        }
                    }

                    if (count > 0)
                    {
                        noti_alert.InnerText = "" + count;
                        noti_alert.Attributes["class"] = "ps-ms-main-hd-noti-alert";
                    }
                }

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