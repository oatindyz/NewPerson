using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WEB_PERSONAL.Class;
using System.Data.OracleClient;

namespace WEB_PERSONAL {
    public partial class WorkingTime : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            if(!IsPostBack) {
                DatabaseManager.BindDropDown(ddlVX1WorktimeSec, "SELECT * FROM LEV_WORKTIME_SEC ORDER BY WORKTIME_SEC_ID ASC", "WORKTIME_SEC_NAME", "WORKTIME_SEC_ID", "--กรุณาเลือกกะงาน--");
            }
        }

        protected void ddlVX1WorktimeSec_SelectedIndexChanged(object sender, EventArgs e) {
            if(ddlVX1WorktimeSec.SelectedIndex == 0) {
                lbVX1WorkTimeTime.Text = "";
                lbVX1WorkTimeDes.Text = "";
                return;
            }
            using(OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING)) {
                con.Open();
                using(OracleCommand com = new OracleCommand("SELECT * FROM LEV_WORKTIME_SEC WHERE WORKTIME_SEC_ID = " + ddlVX1WorktimeSec.SelectedValue, con)) {
                    using(OracleDataReader reader = com.ExecuteReader()) {
                        while(reader.Read()) {
                            string shi = int.Parse(reader.GetValue(1).ToString()).ToString("00");
                            string smi = int.Parse(reader.GetValue(2).ToString()).ToString("00");
                            string sho = int.Parse(reader.GetValue(3).ToString()).ToString("00");
                            string smo = int.Parse(reader.GetValue(4).ToString()).ToString("00");
                            string time = shi + ":" + smi + " - " + sho + ":" + smo;
                            string des = reader.GetValue(6).ToString();
                            lbVX1WorkTimeTime.Text = time;
                            lbVX1WorkTimeDes.Text = des;
                            hfHI.Value = shi;
                            hfMI.Value = smi;
                            hfHO.Value = sho;
                            hfMO.Value = smo;
                        }
                    }
                }
            }
        }

        protected void lbuVX1Next_Click(object sender, EventArgs e) {
            if(tbVX1CitizenID.Text == "" ||
                tbVX1Date.Text == "" ||
                ddlVX1WorktimeSec.SelectedIndex == 0 ||
                (
                    !cbVX1Late.Checked &&
                    (
                        tbVX1HourIn.Text == "" ||
                        tbVX1MinuteIn.Text == "" ||
                        tbVX1HourOut.Text == "" ||
                        tbVX1MinuteOut.Text == ""
                    )
                )
            ) {
                if(tbVX1CitizenID.Text == "") {
                    lbVX1CitizenIDVD.Visible = true;
                } else {
                    lbVX1CitizenIDVD.Visible = false;
                }
                if (tbVX1Date.Text == "") {
                    lbVX1DateVD.Visible = true;
                } else {
                    lbVX1DateVD.Visible = false;
                }
                if (ddlVX1WorktimeSec.SelectedIndex == 0) {
                    lbVX1KaVD.Visible = true;
                } else {
                    lbVX1KaVD.Visible = false;
                }
                if(!cbVX1Late.Checked) {
                    if (tbVX1HourIn.Text == "") {
                        lbVX1HourInVD.Visible = true;
                    } else {
                        lbVX1HourInVD.Visible = false;
                    }
                    if (tbVX1MinuteIn.Text == "") {
                        lbVX1MinuteInVD.Visible = true;
                    } else {
                        lbVX1MinuteInVD.Visible = false;
                    }
                    if (tbVX1HourOut.Text == "") {
                        lbVX1HourOutVD.Visible = true;
                    } else {
                        lbVX1HourOutVD.Visible = false;
                    }
                    if (tbVX1MinuteOut.Text == "") {
                        lbVX1MinuteOutVD.Visible = true;
                    } else {
                        lbVX1MinuteOutVD.Visible = false;
                    }
                } else {
                    lbVX1HourInVD.Visible = false;
                    lbVX1MinuteInVD.Visible = false;
                    lbVX1HourOutVD.Visible = false;
                    lbVX1MinuteOutVD.Visible = false;
                }
                
                return;
            }

            lbVX1CitizenIDVD.Visible = false;
            lbVX1DateVD.Visible = false;
            lbVX1KaVD.Visible = false;
            lbVX1HourInVD.Visible = false;
            lbVX1MinuteInVD.Visible = false;
            lbVX1HourOutVD.Visible = false;
            lbVX1MinuteOutVD.Visible = false;

            MultiView1.ActiveViewIndex = 1;
            using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING)) {
                con.Open();
                using (OracleCommand com = new OracleCommand("SELECT PS_FN_TH || ' ' || PS_LN_TH FROM PS_PERSON WHERE PS_CITIZEN_ID = '" + tbVX1CitizenID.Text + "'", con)) {
                    using (OracleDataReader reader = com.ExecuteReader()) {
                        while (reader.Read()) {
                            lbVX2Name.Text = reader.GetValue(0).ToString();
                        }
                    }
                }
            }

            lbVX2CitizenID.Text = tbVX1CitizenID.Text;
            lbVX2Date.Text = tbVX1Date.Text;
            lbVX2Ka.Text = ddlVX1WorktimeSec.SelectedItem.Text + " / " + lbVX1WorkTimeTime.Text;
            lbVX2Comment.Text = tbVX1Comment.Text;

            if (cbVX1Late.Checked) {
                lbVX2Time.Text = "ขาดงาน";
                lbVX2Late.Visible = false;
                lbVX2Over.Visible = false;

                string sql = "INSERT INTO LEV_WORKTIME(WORKTIME_ID, CITIZEN_ID, TODAY, WORKTIME_SEC_ID, HOUR_IN, MINUTE_IN, HOUR_OUT, MINUTE_OUT, ABSENT, \"COMMENT\", LATE_IN, LATE_OUT) VALUES({0},'{1}',{2},{3},{4},{5},{6},{7},{8},'{9}',{10},{11})";
                sql = string.Format(sql, "SEQ_WORKTIME_ID.NEXTVAL", tbVX1CitizenID.Text, Util.DatabaseToDate(tbVX1Date.Text), ddlVX1WorktimeSec.SelectedValue, "''", "''", "''", "''", 1, tbVX1Comment.Text, "''", "''");
                hfSql.Value = sql;
                //tbTest.Text = sql;

            } else {
                lbVX2Late.Visible = true;
                lbVX2Over.Visible = true;
                lbVX2Time.Text = int.Parse(tbVX1HourIn.Text).ToString("00") + ":" + int.Parse(tbVX1MinuteIn.Text).ToString("00") + " - " + int.Parse(tbVX1HourOut.Text).ToString("00") + ":" + int.Parse(tbVX1MinuteOut.Text).ToString("00");

                int hi = int.Parse(hfHI.Value);
                int mi = int.Parse(hfMI.Value);
                int ci = hi * 60 + mi;
                int ho = int.Parse(hfHO.Value);
                int mo = int.Parse(hfMO.Value);
                int co = ho * 60 + mo;

                int phi = int.Parse(tbVX1HourIn.Text);
                int pmi = int.Parse(tbVX1MinuteIn.Text);
                int pci = phi * 60 + pmi;
                int pho = int.Parse(tbVX1HourOut.Text);
                int pmo = int.Parse(tbVX1MinuteOut.Text);
                int pco = pho * 60 + pmo;

                int vi = pci - ci;
                int vo = co - pco;

                if(vi == 0) {
                    lbVX2Late.Text = "เข้าตรงเวลา";
                    lbVX2Late.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0);
                } else if (vi > 0) {
                    lbVX2Late.Text = "สาย " + vi + " นาที";
                    lbVX2Late.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0);
                } else {
                    lbVX2Late.Text = "ไม่สาย";
                    lbVX2Late.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0);
                }

                if(vo == 0) {
                    lbVX2Over.Text = "ออกตรงเวลา";
                    lbVX2Over.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0);
                } else if(vo > 0) {
                    lbVX2Over.Text = "ออกก่อน " + vo + " นาที";
                    lbVX2Over.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0);
                } else {
                    lbVX2Over.Text = "ล่วงเวลา " + (-vo) + " นาที";
                    lbVX2Over.ForeColor = System.Drawing.Color.FromArgb(0, 0, 255);
                }

                string sql = "INSERT INTO LEV_WORKTIME(WORKTIME_ID, CITIZEN_ID, TODAY, WORKTIME_SEC_ID, HOUR_IN, MINUTE_IN, HOUR_OUT, MINUTE_OUT, ABSENT, \"COMMENT\", LATE_IN, LATE_OUT) VALUES({0},'{1}',{2},{3},{4},{5},{6},{7},{8},'{9}',{10},{11})";
                sql = string.Format(sql, "SEQ_WORKTIME_ID.NEXTVAL", tbVX1CitizenID.Text, Util.DatabaseToDate(tbVX1Date.Text), ddlVX1WorktimeSec.SelectedValue, tbVX1HourIn.Text, tbVX1MinuteIn.Text, tbVX1HourOut.Text, tbVX1MinuteOut.Text, 0, tbVX1Comment.Text, vi, vo);
                hfSql.Value = sql;
                //tbTest.Text = sql;
            }

            

            
        }

        protected void lbuVX2Back_Click(object sender, EventArgs e) {
            MultiView1.ActiveViewIndex = 0;
        }

        protected void lbuVX2Finish_Click(object sender, EventArgs e) {
            MultiView1.ActiveViewIndex = 2;
            DatabaseManager.ExecuteNonQuery(hfSql.Value);
        }

        protected void lbuVX3New_Click(object sender, EventArgs e) {
            Response.Redirect("WorkingTime.aspx");
        }
    }
}