﻿using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using WEB_PERSONAL.Class;
using System.Data.OracleClient;

namespace WEB_PERSONAL
{
    public partial class AddPosition : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindPosition();
            }
        }

        //
        protected void BindPosition()
        {
            OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING);
            OracleDataAdapter sda = new OracleDataAdapter("SELECT PH_ID,P_ID,(SELECT P_NAME FROM TB_POSITION WHERE TB_POSITION.P_ID = PS_POSITION_HISTORY.P_ID)P_NAME, GET_DATE FROM PS_POSITION_HISTORY WHERE CITIZEN_ID = '" + MyCrypto.GetDecryptedQueryString(Request.QueryString["id"].ToString()) + "' ORDER BY GET_DATE ASC", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            myRepeaterPosition.DataSource = dt;
            myRepeaterPosition.DataBind();
            DatabaseManager.BindDropDown(ddlInsertIdPosition,"SELECT * FROM TB_POSITION ORDER BY ABS(P_ID) ASC", "P_NAME", "P_ID", "--กรุณาเลือก--");
        }
        protected void ClearPosition()
        {
            ddlInsertIdPosition.SelectedIndex = 0;
            tbInsertDatePosition.Text = "";
        }
        protected void lbuMenuPosition_Click(object sender, EventArgs e)
        {
            BindPosition();
        }
        protected void btnInsertPosition_Click(object sender, EventArgs e)
        {
            string oldID = DatabaseManager.ExecuteString("SELECT P_ID FROM PS_POSITION_HISTORY WHERE P_ID ='" + ddlInsertIdPosition.SelectedValue + "' AND CITIZEN_ID = '" + MyCrypto.GetDecryptedQueryString(Request.QueryString["id"].ToString()) + "'");
            if (ddlInsertIdPosition.SelectedValue == oldID)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('มีชื่อระดับตำแหน่ง " + ddlInsertIdPosition.SelectedItem.ToString() + " อยู่แล้วไม่สามารถเพิ่มได้')", true);
                return;
            }
            
            if(Util.ToDateTimeOracle(tbInsertDatePosition.Text) > DateTime.Now) {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('วันที่ไม่สามารถมากกว่าวันปัจจุบัน')", true);
                return;
            }

            OracleConnection.ClearAllPools();
            using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
            {
                con.Open();
                string CheckFrist = DatabaseManager.ExecuteString("SELECT COUNT(*) FROM PS_POSITION_HISTORY WHERE CITIZEN_ID = '" + MyCrypto.GetDecryptedQueryString(Request.QueryString["id"].ToString()) + "'");
                using (OracleCommand com = new OracleCommand("INSERT INTO PS_POSITION_HISTORY (CITIZEN_ID,P_ID,GET_DATE) VALUES (:CITIZEN_ID,:P_ID,:GET_DATE)", con))
                {
                    com.Parameters.Add(new OracleParameter("CITIZEN_ID", MyCrypto.GetDecryptedQueryString(Request.QueryString["id"].ToString())));
                    com.Parameters.Add(new OracleParameter("P_ID", ddlInsertIdPosition.SelectedValue));
                    com.Parameters.Add(new OracleParameter("GET_DATE", Util.ToDateTimeOracle(tbInsertDatePosition.Text)));
                    com.ExecuteNonQuery();
                }
                
                if (CheckFrist == "0")
                {
                    using (OracleCommand com = new OracleCommand("UPDATE PS_PERSON SET PS_POSITION_ID = :PS_POSITION_ID, PS_FIRST_POSITION_ID = :PS_FIRST_POSITION_ID WHERE PS_CITIZEN_ID = :PS_CITIZEN_ID", con))
                    {
                        com.Parameters.Add(new OracleParameter("PS_CITIZEN_ID", MyCrypto.GetDecryptedQueryString(Request.QueryString["id"].ToString())));
                        com.Parameters.Add(new OracleParameter("PS_POSITION_ID", ddlInsertIdPosition.SelectedValue));
                        com.Parameters.Add(new OracleParameter("PS_FIRST_POSITION_ID", ddlInsertIdPosition.SelectedValue));
                        com.ExecuteNonQuery();
                    }
                }
                else
                {
                    using (OracleCommand com = new OracleCommand("UPDATE PS_PERSON SET PS_POSITION_ID = :PS_POSITION_ID WHERE PS_CITIZEN_ID = :PS_CITIZEN_ID", con))
                    {
                        com.Parameters.Add(new OracleParameter("PS_CITIZEN_ID", MyCrypto.GetDecryptedQueryString(Request.QueryString["id"].ToString())));
                        com.Parameters.Add(new OracleParameter("PS_POSITION_ID", ddlInsertIdPosition.SelectedValue));
                        com.ExecuteNonQuery();
                    }
                }
                
            }

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('เพิ่มข้อมูลเรียบร้อย')", true);
            BindPosition();
            ClearPosition();
        }
        protected void btnUpdatePosition_Click(object sender, EventArgs e)
        {
            string ValueID = ddlInsertIdPosition.SelectedValue;
            string ValueDate = Util.ToDateTimeOracle(tbInsertDatePosition.Text).ToString();

            if (Session["DefaultIdPosition"] == null)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('กรุณาเลือกรายการที่จะแก้ไขก่อน')", true);
                return;
            }
            if (Util.ToDateTimeOracle(tbInsertDatePosition.Text) > DateTime.Now) {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('วันที่ไม่สามารถมากกว่าวันปัจจุบัน')", true);
                return;
            }

            if (ValueID != "" && ValueDate != "")
            {
                OracleConnection.ClearAllPools();
                using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
                {
                    con.Open();
                    using (OracleCommand com = new OracleCommand("UPDATE PS_POSITION_HISTORY SET P_ID = :P_ID, GET_DATE = :GET_DATE WHERE PH_ID = :PH_ID", con))
                    {
                        com.Parameters.Add(new OracleParameter("P_ID", ValueID));
                        com.Parameters.Add(new OracleParameter("GET_DATE", Util.ToDateTimeOracle(tbInsertDatePosition.Text)));
                        com.Parameters.Add(new OracleParameter("PH_ID", Session["DefaultIdPosition"].ToString()));
                        com.ExecuteNonQuery();
                    }
                    using (OracleCommand com = new OracleCommand("UPDATE PS_PERSON SET PS_POSITION_ID = :PS_POSITION_ID WHERE PS_CITIZEN_ID = :PS_CITIZEN_ID", con))
                    {
                        com.Parameters.Add(new OracleParameter("PS_POSITION_ID", ddlInsertIdPosition.SelectedValue));
                        com.Parameters.Add(new OracleParameter("PS_CITIZEN_ID", MyCrypto.GetDecryptedQueryString(Request.QueryString["id"].ToString())));
                        com.ExecuteNonQuery();
                    }
                    //

                    //----------
                    using (OracleCommand com = new OracleCommand("UPDATE PS_PERSON SET PS_FIRST_POSITION_ID = (SELECT P_ID FROM PS_POSITION_HISTORY WHERE CITIZEN_ID = :CITIZEN_ID AND GET_DATE = (SELECT MIN(GET_DATE) FROM PS_POSITION_HISTORY WHERE CITIZEN_ID = :CITIZEN_ID)) WHERE PS_CITIZEN_ID = :CITIZEN_ID", con)) {
                        com.Parameters.Add(new OracleParameter("CITIZEN_ID", MyCrypto.GetDecryptedQueryString(Request.QueryString["id"].ToString())));
                        com.ExecuteNonQuery();
                    }
                    //---------

                    /*using (OracleCommand com = new OracleCommand("SELECT P_ID FROM PS_POSITION_HISTORY WHERE GET_DATE = (SELECT MIN(GET_DATE) FROM PS_POSITION_HISTORY) AND CITIZEN_ID = :CITIZEN_ID", con))
                    {
                        com.Parameters.Add(new OracleParameter("CITIZEN_ID", MyCrypto.GetDecryptedQueryString(Request.QueryString["id"].ToString())));
                        using (OracleDataReader reader = com.ExecuteReader())
                        {
                            string checkFirst = "";
                            while (reader.Read())
                            {
                                if (checkFirst != null)
                                {
                                    using (OracleCommand com1 = new OracleCommand("UPDATE PS_PERSON SET PS_POSITION_ID = :PS_POSITION_ID, PS_FIRST_POSITION_ID = :PS_FIRST_POSITION_ID WHERE PS_CITIZEN_ID = :PS_CITIZEN_ID", con))
                                    {
                                        com1.Parameters.Add(new OracleParameter("PS_POSITION_ID", ddlInsertIdPosition.SelectedValue));
                                        com1.Parameters.Add(new OracleParameter("PS_FIRST_POSITION_ID", ddlInsertIdPosition.SelectedValue));
                                        com1.Parameters.Add(new OracleParameter("PS_CITIZEN_ID", MyCrypto.GetDecryptedQueryString(Request.QueryString["id"].ToString())));
                                        com1.ExecuteNonQuery();
                                    }
                                }
                            }
                        }
                    } */
                }

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('อัพเดทข้อมูลเรียบร้อย')", true);
                BindPosition();
                ClearPosition();
                Session.Remove("DefaultIdPosition");
            }
        }
        protected void lbuClearPosition_Click(object sender, EventArgs e)
        {
            BindPosition();
            ClearPosition();
            Session.Remove("DefaultIdPosition");
        }
        protected void OnEditPosition(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            string ValuePHID = (item.FindControl("HFPH_ID") as HiddenField).Value;
            string ValuePID = (item.FindControl("HFPositionID") as HiddenField).Value;
            string ValueName = (item.FindControl("lbPositionDate") as Label).Text;

            ddlInsertIdPosition.SelectedValue = ValuePID;
            tbInsertDatePosition.Text = ValueName;

            Session["DefaultIdPosition"] = ValuePHID;
        }
        protected void OnDeletePosition(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            string ValuePHID = (item.FindControl("HFPH_ID") as HiddenField).Value;

            if (ValuePHID != "") {
                DatabaseManager.ExecuteNonQuery("DELETE PS_POSITION_HISTORY WHERE PH_ID = '" + ValuePHID + "'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('ลบข้อมูลเรียบร้อย')", true);
                BindPosition();


                OracleConnection.ClearAllPools();
                using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING)) {
                    con.Open();
                    using (OracleCommand com = new OracleCommand("UPDATE PS_PERSON SET PS_POSITION_ID = (SELECT P_ID FROM PS_POSITION_HISTORY WHERE CITIZEN_ID = :CITIZEN_ID AND GET_DATE = (SELECT MIN(GET_DATE) FROM PS_POSITION_HISTORY WHERE CITIZEN_ID = :CITIZEN_ID)) WHERE PS_CITIZEN_ID = :CITIZEN_ID", con)) {
                        com.Parameters.Add(new OracleParameter("PS_CITIZEN_ID", MyCrypto.GetDecryptedQueryString(Request.QueryString["id"].ToString())));
                        com.ExecuteNonQuery();
                    }
                    using (OracleCommand com = new OracleCommand("UPDATE PS_PERSON SET PS_FIRST_POSITION_ID = (SELECT P_ID FROM PS_POSITION_HISTORY WHERE CITIZEN_ID = :CITIZEN_ID AND GET_DATE = (SELECT MIN(GET_DATE) FROM PS_POSITION_HISTORY WHERE CITIZEN_ID = :CITIZEN_ID)) WHERE PS_CITIZEN_ID = :CITIZEN_ID", con)) {
                        com.Parameters.Add(new OracleParameter("CITIZEN_ID", MyCrypto.GetDecryptedQueryString(Request.QueryString["id"].ToString())));
                        com.ExecuteNonQuery();
                    }
                }

                /*using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
                {
                    con.Open();
                    string CheckFrist = DatabaseManager.ExecuteString("SELECT COUNT(*) FROM PS_POSITION_HISTORY WHERE CITIZEN_ID = '" + MyCrypto.GetDecryptedQueryString(Request.QueryString["id"].ToString()) + "'");
                    if (CheckFrist == "0")
                    {
                        using (OracleCommand com = new OracleCommand("UPDATE PS_PERSON SET PS_POSITION_ID = :PS_POSITION_ID, PS_FIRST_POSITION_ID = :PS_FIRST_POSITION_ID WHERE PS_CITIZEN_ID = :PS_CITIZEN_ID", con))
                        {
                            com.Parameters.Add(new OracleParameter("PS_POSITION_ID", DBNull.Value));
                            com.Parameters.Add(new OracleParameter("PS_FIRST_POSITION_ID", DBNull.Value));
                            com.Parameters.Add(new OracleParameter("PS_CITIZEN_ID", MyCrypto.GetDecryptedQueryString(Request.QueryString["id"].ToString())));
                            com.ExecuteNonQuery();
                        }
                    }
                }*/

            }
        }
    }
}