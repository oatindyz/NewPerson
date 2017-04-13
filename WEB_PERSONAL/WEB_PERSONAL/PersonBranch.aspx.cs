using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OracleClient;
using WEB_PERSONAL.Class;

namespace WEB_PERSONAL
{
    public partial class PersonBranch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PersonnelSystem ps = PersonnelSystem.GetPersonnelSystem(this);
            Person loginPerson = ps.LoginPerson;
            if (loginPerson.PERSON_ROLE_ID != "2")
            {
                Server.Transfer("NoPermission.aspx");
            }

            if (!IsPostBack)
            {
                SQLCampus();
            }


            /*using (OracleCommand com = new OracleCommand("SELECT (SELECT URL FROM PS_PERSON_IMAGE WHERE PS_PERSON.PS_CITIZEN_ID = PS_PERSON_IMAGE.CITIZEN_ID AND PRESENT = 1) URL, PS_PERSON.PS_FN_TH || ' ' || PS_PERSON.PS_LN_TH NAME, PS_EMAIL FROM PS_PERSON", con)) {
                using(OracleDataReader reader = com.ExecuteReader()) {
                    while(reader.Read()) {
                        Panel pm = new Panel();
                        {
                            string imageURL = reader.IsDBNull(0) ? null : reader.GetString(0);
                            Panel p1 = new Panel();
                            Image img = new Image();
                            img.CssClass = "ps-ms-main-drop-profile-pic";
                            if (imageURL != null) {
                                img.Attributes["src"] = "Upload/PersonImage/" + imageURL;
                            } else {
                                img.Attributes["src"] = "Image/no_image.png";
                            }
                            p1.Controls.Add(img);
                            pm.Controls.Add(p1);
                        }

                        {
                            Label lb = new Label();
                            lb.Text = reader.GetString(1) + "<br />" + reader.GetString(2);
                            pm.Controls.Add(lb);
                        }
                        Panel1.Controls.Add(pm);

                    }
                }
            }*/

            if (Request.QueryString["q"] != null && Request.QueryString["t"] != null)
            {
                string q = Request.QueryString["q"];
                string t = Request.QueryString["t"];
                funcSearch(q, t);
            }

        }

        protected void SQLCampus()
        {
            try
            {
                using (OracleConnection sqlConn = new OracleConnection(DatabaseManager.CONNECTION_STRING))
                {
                    using (OracleCommand sqlCmd = new OracleCommand())
                    {
                        sqlCmd.CommandText = "select * from TB_CAMPUS";
                        sqlCmd.Connection = sqlConn;
                        sqlConn.Open();
                        OracleDataAdapter da = new OracleDataAdapter(sqlCmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        ddlCampus.DataSource = dt;
                        ddlCampus.DataValueField = "CAMPUS_ID";
                        ddlCampus.DataTextField = "CAMPUS_NAME";
                        ddlCampus.DataBind();
                        sqlConn.Close();

                        ddlCampus.Items.Insert(0, new ListItem("--กรุณาเลือกวิทยาเขต--", "0"));
                        ddlFaculty.Items.Insert(0, new ListItem("--กรุณาเลือกสำนัก / สถาบัน / คณะ--", "0"));
                        ddlDivision.Items.Insert(0, new ListItem("--กรุณาเลือกกอง / สำนักงานเลขา / ภาควิชา--", "0"));
                        ddlWorkDivision.Items.Insert(0, new ListItem("--กรุณาเลือกงาน / ฝ่าย--", "0"));
                    }
                }
            }
            catch { }
        }

        protected void ddlCampus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                using (OracleConnection sqlConn = new OracleConnection(DatabaseManager.CONNECTION_STRING))
                {
                    using (OracleCommand sqlCmd = new OracleCommand())
                    {
                        sqlCmd.CommandText = "select * from TB_FACULTY where CAMPUS_ID = " + ddlCampus.SelectedValue;
                        sqlCmd.Connection = sqlConn;
                        sqlConn.Open();
                        OracleDataAdapter da = new OracleDataAdapter(sqlCmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        ddlFaculty.DataSource = dt;
                        ddlFaculty.DataValueField = "FACULTY_ID";
                        ddlFaculty.DataTextField = "FACULTY_NAME";
                        ddlFaculty.DataBind();
                        sqlConn.Close();

                        ddlFaculty.Items.Insert(0, new ListItem("--กรุณาเลือกสำนัก / สถาบัน / คณะ--", "0"));
                        ddlDivision.Items.Clear();
                        ddlDivision.Items.Insert(0, new ListItem("--กรุณาเลือกกอง / สำนักงานเลขา / ภาควิชา--", "0"));
                        ddlWorkDivision.Items.Clear();
                        ddlWorkDivision.Items.Insert(0, new ListItem("--กรุณาเลือกงาน / ฝ่าย--", "0"));
                    }
                }
            }
            catch { }
        }

        protected void ddlFaculty_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                using (OracleConnection sqlConn = new OracleConnection(DatabaseManager.CONNECTION_STRING))
                {
                    using (OracleCommand sqlCmd = new OracleCommand())
                    {
                        sqlCmd.CommandText = "select * from TB_DIVISION where FACULTY_ID = " + ddlFaculty.SelectedValue;
                        sqlCmd.Connection = sqlConn;
                        sqlConn.Open();
                        OracleDataAdapter da = new OracleDataAdapter(sqlCmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        ddlDivision.DataSource = dt;
                        ddlDivision.DataValueField = "DIVISION_ID";
                        ddlDivision.DataTextField = "DIVISION_NAME";
                        ddlDivision.DataBind();
                        sqlConn.Close();

                        ddlDivision.Items.Insert(0, new ListItem("--กรุณาเลือกกอง / สำนักงานเลขา / ภาควิชา--", "0"));
                        ddlWorkDivision.Items.Clear();
                        ddlWorkDivision.Items.Insert(0, new ListItem("--กรุณาเลือกงาน / ฝ่าย--", "0"));
                    }
                }
            }
            catch { }

        }

        protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                using (OracleConnection sqlConn = new OracleConnection(DatabaseManager.CONNECTION_STRING))
                {
                    using (OracleCommand sqlCmd = new OracleCommand())
                    {
                        sqlCmd.CommandText = "select * from TB_WORK_DIVISION where DIVISION_ID = " + ddlDivision.SelectedValue;
                        sqlCmd.Connection = sqlConn;
                        sqlConn.Open();
                        OracleDataAdapter da = new OracleDataAdapter(sqlCmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        ddlWorkDivision.DataSource = dt;
                        ddlWorkDivision.DataValueField = "WORK_ID";
                        ddlWorkDivision.DataTextField = "WORK_NAME";
                        ddlWorkDivision.DataBind();
                        sqlConn.Close();

                        ddlWorkDivision.Items.Insert(0, new ListItem("--กรุณาเลือกงาน / ฝ่าย--", "0"));
                    }
                }
            }
            catch { }

            using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
            {
                con.Open();
                using (OracleCommand com = new OracleCommand("SELECT COUNT(*) FROM TB_WORK_DIVISION WHERE DIVISION_ID = " + ddlDivision.SelectedValue, con))
                {
                    using (OracleDataReader reader = com.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader.GetInt32(0) == 0)
                            {
                                ddlWorkDivision.Visible = false;
                                trWorkDivision.Visible = false;
                            }
                            else
                            {
                                ddlWorkDivision.Visible = true;
                                trWorkDivision.Visible = true;
                            }
                        }
                    }
                }
            }
        }

        protected void lbuSearch_Click(object sender, EventArgs e)
        {
            if (ddlWorkDivision.Visible)
            {
                Response.Redirect("PersonBranch.aspx?q=" + ddlWorkDivision.SelectedValue + "&t=WD");
            }
            else
            {
                Response.Redirect("PersonBranch.aspx?q=" + ddlDivision.SelectedValue + "&t=DV");
            }

        }

        private void funcSearch(string id, string type)
        {
            ccc.Style.Add("display", "block");

            string searchID = id;
            string searchName = "";
            string searchWhere;
            //string searchBossType;
            string searchAdminPosID;

            if (type == "WD")
            {
                //searchID = ddlWorkDivision.SelectedValue;
                //searchName = ddlWorkDivision.SelectedItem.Text;
                searchWhere = "PS_WORK_DIVISION_ID";
                //searchBossType = "WD";
                searchAdminPosID = "4";
            }
            else
            {
                //searchID = ddlDivision.SelectedValue;
                //searchName = ddlDivision.SelectedItem.Text;
                searchWhere = "PS_DIVISION_ID";
                //searchBossType = "DV";
                searchAdminPosID = "7";
            }



            OracleConnection.ClearAllPools();
            using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
            {
                con.Open();

                if (type == "WD")
                {
                    using (OracleCommand com = new OracleCommand("SELECT WORK_NAME FROM TB_WORK_DIVISION WHERE WORK_ID = " + id, con))
                    {
                        using (OracleDataReader reader = com.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                searchName = reader.GetString(0);
                            }
                        }
                    }
                }
                else
                {
                    using (OracleCommand com = new OracleCommand("SELECT DIVISION_NAME FROM TB_DIVISION WHERE DIVISION_ID = " + id, con))
                    {
                        using (OracleDataReader reader = com.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                searchName = reader.GetString(0);
                            }
                        }
                    }
                }

                using (OracleCommand com = new OracleCommand("SELECT (SELECT URL FROM PS_PERSON_IMAGE WHERE PS_PERSON.PS_CITIZEN_ID = PS_PERSON_IMAGE.CITIZEN_ID AND PRESENT = 1) URL, PS_PERSON.PS_FIRSTNAME || ' ' || PS_PERSON.PS_LASTNAME NAME, PS_EMAIL, PS_CITIZEN_ID, (SELECT POSITION_WORK_NAME FROM TB_POSITION_WORK WHERE TB_POSITION_WORK.POSITION_WORK_ID = PS_PERSON.PS_WORK_POS_ID), (SELECT ADMIN_POSITION_NAME FROM TB_ADMIN_POSITION WHERE TB_ADMIN_POSITION.ADMIN_POSITION_ID = PS_PERSON.PS_ADMIN_POS_ID), (SELECT CAMPUS_NAME FROM TB_CAMPUS WHERE PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID), (SELECT FACULTY_NAME FROM TB_FACULTY WHERE PS_PERSON.PS_FACULTY_ID = TB_FACULTY.FACULTY_ID), (SELECT DIVISION_NAME FROM TB_DIVISION WHERE PS_PERSON.PS_DIVISION_ID = TB_DIVISION.DIVISION_ID), (SELECT WORK_NAME FROM TB_WORK_DIVISION WHERE PS_PERSON.PS_WORK_DIVISION_ID = TB_WORK_DIVISION.WORK_ID) FROM PS_PERSON WHERE " + searchWhere + " = " + searchID, con))
                {
                    using (OracleDataReader reader = com.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            string citizenID = reader.GetString(3);
                            string workPositionName = reader.IsDBNull(4) ? "" : reader.GetString(4);
                            string adminPositionName = reader.IsDBNull(5) ? "" : reader.GetString(5);
                            string campusName = reader.IsDBNull(6) ? "" : reader.GetString(6);
                            string FacultyName = reader.IsDBNull(7) ? "" : reader.GetString(7);
                            string DivisionName = reader.IsDBNull(8) ? "" : reader.GetString(8);
                            string WorkDivisionName = reader.IsDBNull(9) ? "" : reader.GetString(9);
                            bool isBoss = false;

                            //using (OracleCommand com2 = new OracleCommand("SELECT COUNT(*) FROM PS_BOSS WHERE CITIZEN_ID = '" + citizenID + "' AND BOS_TYPE = '" + searchBossType + "' AND BOS_TYPE_ID = " + searchID, con)) {
                            using (OracleCommand com2 = new OracleCommand("SELECT PS_ADMIN_POS_ID FROM PS_PERSON WHERE PS_CITIZEN_ID = '" + citizenID + "'", con))
                            {
                                using (OracleDataReader reader2 = com2.ExecuteReader())
                                {
                                    while (reader2.Read())
                                    {
                                        if (reader2.IsDBNull(0))
                                        {

                                        }
                                        else
                                        {
                                            if (reader2.GetInt32(0) == 4 && type == "WD")
                                            {
                                                isBoss = true;
                                            }
                                            else if (reader2.GetInt32(0) == 10 && type == "DV")
                                            {
                                                isBoss = true;
                                            }
                                        }


                                    }
                                }
                            }



                            Panel pm = new Panel();
                            pm.CssClass = "ps-person-display";


                            if (isBoss)
                            {
                                spWorkDivisionName.InnerText = searchName;
                            }

                            {
                                string imageURL = reader.IsDBNull(0) ? null : reader.GetString(0);
                                Panel p1 = new Panel();
                                Image img = new Image();
                                img.CssClass = "ps-ms-main-drop-profile-pic";
                                if (imageURL != null)
                                {
                                    img.Attributes["src"] = "Upload/PersonImage/" + imageURL;
                                }
                                else
                                {
                                    img.Attributes["src"] = "Image/no_image.png";
                                }
                                p1.Controls.Add(img);
                                pm.Controls.Add(p1);
                            }

                            {
                                Label lb = new Label();

                                string email = "";
                                if (!reader.IsDBNull(2))
                                {
                                    email = reader.GetString(2);
                                }
                                lb.Text = reader.GetString(1) + "<br /><span style='color:#808080'>" + email + "</span><br /><span style='color:#404040'>ตำแหน่ง : " + workPositionName + "</span><br /><span style='color:#404040'>" + campusName + "</span><br /><span style='color:#808080'>" + FacultyName + "</span><br /><span style='color:#404040'>" + DivisionName + "</span>";
                                if (WorkDivisionName != "")
                                {
                                    lb.Text += "<br /><span style='color:#808080'>งาน/ฝ่าย : " + WorkDivisionName + "</span>";
                                }
                                pm.Controls.Add(lb);
                            }
                            if (isBoss)
                            {
                                pBoss.Controls.Add(pm);
                            }
                            else
                            {
                                pMember.Controls.Add(pm);
                            }


                        }
                    }
                }

            }
        }
    }
}