using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OracleClient;
using WEB_PERSONAL.Class;
using System.Data;

namespace WEB_PERSONAL
{
    public partial class Boss : System.Web.UI.Page
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
                using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
                {
                    con.Open();
                    using (OracleCommand com = new OracleCommand("SELECT BOSS_NODE_ID, BOSS_NODE_NAME FROM TB_BOSS_NODE", con))
                    {
                        using (OracleDataReader reader = com.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ddlHighNode.Items.Add(new ListItem(reader.GetInt32(0) + " | " + reader.GetString(1), reader.GetInt32(0) + ""));
                            }
                        }
                    }
                }
            }
            BindTable();
        }

        protected void lbuAdd_Click(object sender, EventArgs e)
        {

            bool found = false;
            using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
            {
                con.Open();
                using (OracleCommand com = new OracleCommand("SELECT * FROM PS_PERSON WHERE PS_CITIZEN_ID = '" + tbCitizenID.Text + "'", con))
                {
                    using (OracleDataReader reader = com.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            found = true;
                        }
                    }
                }
            }
            if (!found)
            {
                lbResult.Text = "ไม่พบพนักงาน";
                lbResult.ForeColor = System.Drawing.Color.Red;
                lbResult.Visible = true;
                return;
            }
            else
            {
                lbResult.Text = "เพิ่มข้อมูลสำเร็จ";
                lbResult.ForeColor = System.Drawing.Color.Green;
                lbResult.Visible = false;
            }

            int type;
            string typeID;
            int adminPositionID;
            string personUpdate;
            if (rbAtikan.Checked)
            {
                type = -1;
                typeID = null;
                adminPositionID = 1;
                personUpdate = "";
            }
            else if (rbCampus.Checked)
            {
                type = 1;
                typeID = ddlCampus.SelectedValue;
                adminPositionID = 2;
                personUpdate = ", PS_CAMPUS_ID = " + ddlCampus.SelectedValue;
            }
            else if (rbFaculty.Checked)
            {
                type = 2;
                typeID = ddlFaculty.SelectedValue;
                adminPositionID = 4;
                personUpdate = ", PS_FACULTY_ID = " + ddlFaculty.SelectedValue;
            }
            else if (rbDivision.Checked)
            {
                type = 3;
                typeID = ddlDivision.SelectedValue;
                adminPositionID = 10;
                personUpdate = ", PS_DIVISION_ID = " + ddlDivision.SelectedValue;
            }
            else
            {
                type = 4;
                typeID = ddlWorkDivision.SelectedValue;
                adminPositionID = 11;
                personUpdate = ", PS_WORK_DIVISION_ID = " + ddlWorkDivision.SelectedValue;
            }
            string bossNodeName;
            if (type == 1)
            {
                bossNodeName = "รองอธิการบดี";
            }
            else if (type == 2)
            {
                bossNodeName = "คณบดี";
            }
            else if (type == 3)
            {
                bossNodeName = "หัวหน้า";
            }
            else if (type == 4)
            {
                bossNodeName = "หัวหน้า";
            }
            else
            {
                bossNodeName = "อธิการบดี";
            }
            string bossNodeNameNuy = "";
            if (type == 1)
            {
                bossNodeNameNuy = ddlCampus.SelectedItem.Text;
            }
            else if (type == 2)
            {
                bossNodeNameNuy = ddlFaculty.SelectedItem.Text;
            }
            else if (type == 3)
            {
                bossNodeNameNuy = ddlDivision.SelectedItem.Text;
            }
            else if (type == 4)
            {
                bossNodeNameNuy = ddlWorkDivision.SelectedItem.Text;
            }
            using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
            {
                con.Open();
                using (OracleCommand com = new OracleCommand("INSERT INTO TB_BOSS_NODE (BOSS_NODE_ID, BOSS_NODE_NAME, BOSS_NODES_ID, BOSS_NODE_PARENT_ID, BOSS_NODE_TYPE, BOSS_NODE_TYPE_ID, BOSS_NODE_CITIZEN_ID) VALUES(SEQ_BOSS_NODE_ID.NEXTVAL, :BOSS_NODE_NAME, :BOSS_NODES_ID, :BOSS_NODE_PARENT_ID, :BOSS_NODE_TYPE, :BOSS_NODE_TYPE_ID, :BOSS_NODE_CITIZEN_ID)", con))
                {
                    com.Parameters.AddWithValue("BOSS_NODE_NAME", bossNodeName + bossNodeNameNuy);
                    com.Parameters.AddWithValue("BOSS_NODES_ID", 1);
                    com.Parameters.AddWithValue("BOSS_NODE_PARENT_ID", ddlHighNode.SelectedValue);
                    if (type == -1)
                    {
                        com.Parameters.AddWithValue("BOSS_NODE_TYPE", null);
                    }
                    else
                    {
                        com.Parameters.AddWithValue("BOSS_NODE_TYPE", type);
                    }
                    if (typeID == null)
                    {
                        com.Parameters.AddWithValue("BOSS_NODE_TYPE_ID", null);
                    }
                    else
                    {
                        com.Parameters.AddWithValue("BOSS_NODE_TYPE_ID", typeID);
                    }

                    com.Parameters.AddWithValue("BOSS_NODE_CITIZEN_ID", tbCitizenID.Text);
                    com.ExecuteNonQuery();
                }

                using (OracleCommand com = new OracleCommand("UPDATE PS_PERSON SET PS_ADMIN_POS_ID = :PS_ADMIN_POS_ID " + personUpdate + " WHERE PS_CITIZEN_ID = :PS_CITIZEN_ID", con))
                {
                    com.Parameters.AddWithValue("PS_ADMIN_POS_ID", adminPositionID);
                    com.Parameters.AddWithValue("PS_CITIZEN_ID", tbCitizenID.Text);
                    com.ExecuteNonQuery();
                }
            }
            lbResult.Visible = true;
            Response.Redirect("Boss.aspx");

        }

        //-------------

        private void BindTable()
        {
            tbBoss.Rows.Clear();
            {
                TableHeaderRow row = new TableHeaderRow();
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "รหัสโหนด"; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "ชื่่อโหนด"; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "รหัสโหนดสูง"; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "หน่วยงาน"; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "หัวหน้า"; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "รหัสประชาชนหัวหน้า"; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "<img src='Image/Small/wrench.png' class='icon_left'/>แก้ไข"; row.Cells.Add(cell); }
                { TableHeaderCell cell = new TableHeaderCell(); cell.Text = "<img src='Image/Small/delete.png' class='icon_left'/>ลบ"; row.Cells.Add(cell); }
                tbBoss.Rows.Add(row);
            }

            using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
            {
                con.Open();
                using (OracleCommand com = new OracleCommand("SELECT BOSS_NODE_ID, BOSS_NODE_NAME, BOSS_NODE_PARENT_ID, BOSS_NODE_TYPE, BOSS_NODE_TYPE_ID, (SELECT PS_FIRSTNAME || ' ' || PS_LASTNAME FROM PS_PERSON WHERE PS_PERSON.PS_CITIZEN_ID = BOSS_NODE_CITIZEN_ID), BOSS_NODE_CITIZEN_ID FROM TB_BOSS_NODE", con))
                {
                    using (OracleDataReader reader = com.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int bossNodeID = reader.GetInt32(0);
                            TextBox tbCitizen = new TextBox();
                            tbCitizen.CssClass = "ps-textbox";
                            tbCitizen.MaxLength = 13;
                            string citizenOld;
                            TableRow row = new TableRow();
                            tbBoss.Rows.Add(row);
                            {
                                TableCell cell = new TableCell();
                                cell.Text = bossNodeID.ToString();
                                row.Cells.Add(cell);
                            }
                            {
                                TableCell cell = new TableCell();
                                cell.Text = reader.GetValue(1).ToString();
                                row.Cells.Add(cell);
                            }
                            {
                                TableCell cell = new TableCell();
                                cell.Text = reader.IsDBNull(2) ? "-" : reader.GetInt32(2).ToString();
                                row.Cells.Add(cell);
                            }

                            if (!reader.IsDBNull(3))
                            {
                                int type = reader.GetInt32(3);
                                int typeID = reader.GetInt32(4);
                                string sql = "";
                                if (type == 1)
                                {
                                    sql = "SELECT CAMPUS_NAME FROM TB_CAMPUS WHERE CAMPUS_ID = " + typeID;
                                }
                                else if (type == 2)
                                {
                                    sql = "SELECT FACULTY_NAME FROM TB_FACULTY WHERE FACULTY_ID = " + typeID;
                                }
                                else if (type == 3)
                                {
                                    sql = "SELECT DIVISION_NAME FROM TB_DIVISION WHERE DIVISION_ID = " + typeID;
                                }
                                else if (type == 4)
                                {
                                    sql = "SELECT WORK_NAME FROM TB_WORK_DIVISION WHERE WORK_ID = " + typeID;
                                }
                                using (OracleCommand com2 = new OracleCommand(sql, con))
                                {
                                    using (OracleDataReader reader2 = com2.ExecuteReader())
                                    {
                                        if (reader2.Read())
                                        {
                                            TableCell cell = new TableCell();
                                            cell.Text = reader2.GetValue(0).ToString();
                                            row.Cells.Add(cell);
                                        }
                                    }
                                }

                            }
                            else
                            {
                                TableCell cell = new TableCell();
                                cell.Text = "-";
                                row.Cells.Add(cell);
                            }
                            {
                                TableCell cell = new TableCell();
                                cell.Text = reader.GetValue(5).ToString();
                                row.Cells.Add(cell);
                            }
                            {
                                TableCell cell = new TableCell();
                                citizenOld = reader.GetValue(6).ToString();
                                tbCitizen.Text = citizenOld;
                                cell.Controls.Add(tbCitizen);
                                row.Cells.Add(cell);
                            }
                            {
                                TableCell cell = new TableCell();
                                LinkButton bt = new LinkButton();
                                bt.CssClass = "ps-button";
                                bt.Text = "<img src='Image/Small/wrench.png' class='icon_left'/>แก้ไข";
                                bt.Click += (e2, e3) =>
                                {
                                    using (OracleConnection con2 = new OracleConnection(DatabaseManager.CONNECTION_STRING))
                                    {
                                        con2.Open();

                                        bool found = false;

                                        using (OracleCommand com2 = new OracleCommand("SELECT * FROM PS_PERSON WHERE PS_CITIZEN_ID = '" + tbCitizen.Text + "'", con2))
                                        {
                                            using (OracleDataReader reader2 = com2.ExecuteReader())
                                            {
                                                if (reader2.Read())
                                                {
                                                    found = true;
                                                }
                                            }
                                        }

                                        if (!found)
                                        {
                                            Util.Alert(this, "ไม่พบพนักผู้ใช้");
                                            return;
                                        }


                                        using (OracleCommand com2 = new OracleCommand("UPDATE TB_BOSS_NODE SET BOSS_NODE_CITIZEN_ID = :BOSS_NODE_CITIZEN_ID WHERE BOSS_NODE_ID = :BOSS_NODE_ID", con2))
                                        {
                                            com2.Parameters.AddWithValue("BOSS_NODE_CITIZEN_ID", tbCitizen.Text);
                                            com2.Parameters.AddWithValue("BOSS_NODE_ID", bossNodeID);
                                            com2.ExecuteNonQuery();
                                        }
                                        using (OracleCommand com2 = new OracleCommand("UPDATE PS_PERSON SET PS_ADMIN_POS_ID = :PS_ADMIN_POS_ID WHERE PS_CITIZEN_ID = :PS_CITIZEN_ID", con2))
                                        {
                                            int v1 = 0;
                                            com2.Parameters.AddWithValue("PS_ADMIN_POS_ID", v1);
                                            com2.Parameters.AddWithValue("PS_CITIZEN_ID", citizenOld);
                                            com2.ExecuteNonQuery();
                                        }

                                        int type = -1;
                                        int adminPositionID = -1;
                                        using (OracleCommand com2 = new OracleCommand("SELECT BOSS_NODE_TYPE FROM TB_BOSS_NODE WHERE BOSS_NODE_ID = :BOSS_NODE_ID", con2))
                                        {
                                            com2.Parameters.AddWithValue("BOSS_NODE_ID", bossNodeID);
                                            using (OracleDataReader reader2 = com2.ExecuteReader())
                                            {
                                                while (reader2.Read())
                                                {
                                                    type = reader2.GetInt32(0);
                                                }
                                            }

                                        }
                                        if (type == 1)
                                        {
                                            adminPositionID = 2;
                                        }
                                        else if (type == 2)
                                        {
                                            adminPositionID = 4;
                                        }
                                        else if (type == 3)
                                        {
                                            adminPositionID = 10;
                                        }
                                        else if (type == 4)
                                        {
                                            adminPositionID = 11;
                                        }
                                        else
                                        {
                                            adminPositionID = 1;
                                        }

                                        using (OracleCommand com2 = new OracleCommand("UPDATE PS_PERSON SET PS_ADMIN_POS_ID = :PS_ADMIN_POS_ID WHERE PS_CITIZEN_ID = :PS_CITIZEN_ID", con2))
                                        {

                                            com2.Parameters.AddWithValue("PS_ADMIN_POS_ID", adminPositionID);
                                            com2.Parameters.AddWithValue("PS_CITIZEN_ID", tbCitizen.Text);
                                            com2.ExecuteNonQuery();
                                        }
                                    }
                                    Response.Redirect("Boss.aspx");
                                };
                                cell.Controls.Add(bt);
                                row.Cells.Add(cell);
                            }
                            {
                                TableCell cell = new TableCell();
                                LinkButton bt = new LinkButton();
                                bt.CssClass = "ps-button";
                                bt.Text = "<img src='Image/Small/delete.png' class='icon_left'/>ลบ";
                                bt.Click += (e2, e3) =>
                                {
                                    using (OracleConnection con2 = new OracleConnection(DatabaseManager.CONNECTION_STRING))
                                    {
                                        con2.Open();

                                        bool found = false;

                                        using (OracleCommand com2 = new OracleCommand("SELECT * FROM PS_PERSON WHERE PS_CITIZEN_ID = '" + tbCitizen.Text + "'", con2))
                                        {
                                            using (OracleDataReader reader2 = com2.ExecuteReader())
                                            {
                                                if (reader2.Read())
                                                {
                                                    found = true;
                                                }
                                            }
                                        }

                                        if (!found)
                                        {
                                            Util.Alert(this, "ไม่พบพนักผู้ใช้");
                                            return;
                                        }

                                        using (OracleCommand com2 = new OracleCommand("DELETE FROM TB_BOSS_NODE WHERE BOSS_NODE_ID = :BOSS_NODE_ID", con2))
                                        {
                                            com2.Parameters.AddWithValue("BOSS_NODE_ID", bossNodeID);
                                            com2.ExecuteNonQuery();
                                        }
                                        using (OracleCommand com2 = new OracleCommand("UPDATE PS_PERSON SET PS_ADMIN_POS_ID = :PS_ADMIN_POS_ID WHERE PS_CITIZEN_ID = :PS_CITIZEN_ID", con2))
                                        {
                                            int v1 = 0;
                                            com2.Parameters.AddWithValue("PS_ADMIN_POS_ID", v1);
                                            com2.Parameters.AddWithValue("PS_CITIZEN_ID", citizenOld);
                                            com2.ExecuteNonQuery();
                                        }
                                    }
                                    Response.Redirect("Boss.aspx");
                                };
                                cell.Controls.Add(bt);
                                row.Cells.Add(cell);
                            }

                        }
                    }
                }
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
    }
}