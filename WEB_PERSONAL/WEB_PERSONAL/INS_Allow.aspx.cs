using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WEB_PERSONAL.Class;
using System.Data.OracleClient;

namespace WEB_PERSONAL {
    public partial class INS_Allow : System.Web.UI.Page {
        string Citizen_id;
        Person QueryString;

        protected void Page_Load(object sender, EventArgs e) {

            PersonnelSystem ps = PersonnelSystem.GetPersonnelSystem(this);
            Person loginPerson = ps.LoginPerson;

            

            int count = DatabaseManager.ExecuteInt("SELECT COUNT(*) FROM TB_INSIG_PERSON WHERE IP_STATUS_ID = 1");
            /* using (OleDbConnection con = new OleDbConnection(DatabaseManager.CONNECTION_STRING)) {
                 con.Open();
                 using (OleDbCommand com = new OleDbCommand("SELECT COUNT(LEAVE_ID) FROM LEV_LEAVE WHERE CMD_HIGH_ID = '" + loginPerson.CitizenID + "' AND LEV_LEAVE.STATE_ID = 2", con)) {
                     using (OleDbDataReader reader = com.ExecuteReader()) {
                         while (reader.Read()) {
                             count = int.Parse(reader.GetValue(0).ToString());
                         }
                     }
                 }
             }*/
            if (count == 0) {
                error_area.InnerHtml = "ไม่มีรายการที่ท่านต้องอนุมัติ";
            } else {
                error_area.InnerHtml = "กรุณาเลือกรายการที่ต้องการอนุมัติ";
            }
            error_area.Attributes["class"] = null;
            error_area.InnerHtml = "";

            if (count > 0) {

                SqlDataSource sds = DatabaseManager.CreateSQLDataSource("SELECT IP_ID รหัสการขอเครื่องราช, (SELECT  PS_FIRSTNAME || ' ' || PS_LASTNAME FROM PS_PERSON WHERE PS_CITIZEN_ID = CITIZEN_ID) ชื่อผู้ขอเครื่องราช, (SELECT INSIG_GRADE_NAME_L FROM TB_INSIG_GRADE WHERE INSIG_GRADE_ID = INSIG_ID) ระดับชั้นเครื่องราชที่ขอ, REQ_DATE วันที่ข้อมูล, (SELECT IP_STATUS_NAME FROM TB_INSIG_PERSON_STATUS WHERE TB_INSIG_PERSON_STATUS.IP_STATUS_ID = TB_INSIG_PERSON.IP_STATUS_ID) สถานะ FROM TB_INSIG_PERSON WHERE IP_STATUS_ID = 1");
                GridView1.DataSource = sds;
                GridView1.DataBind();

                Util.NormalizeGridViewDate(GridView1, 3);

                TableHeaderCell newHeader = new TableHeaderCell();
                newHeader.Text = "เลือก";
                GridView1.HeaderRow.Cells.Add(newHeader);

                /*GridView1.HeaderRow.Cells[0].Text = "<img src='Image/Small/ID.png' class='icon_left'/>" + GridView1.HeaderRow.Cells[0].Text;
                GridView1.HeaderRow.Cells[1].Text = "<img src='Image/Small/person2.png' class='icon_left'/>" + GridView1.HeaderRow.Cells[1].Text;
                GridView1.HeaderRow.Cells[2].Text = "<img src='Image/Small/list.png' class='icon_left'/>" + GridView1.HeaderRow.Cells[2].Text;
                GridView1.HeaderRow.Cells[3].Text = "<img src='Image/Small/calendar.png' class='icon_left'/>" + GridView1.HeaderRow.Cells[3].Text;
                GridView1.HeaderRow.Cells[4].Text = "<img src='Image/Small/question.png' class='icon_left'/>" + GridView1.HeaderRow.Cells[4].Text;
                GridView1.HeaderRow.Cells[5].Text = "<img src='Image/Small/pointer.png' class='icon_left'/>" + GridView1.HeaderRow.Cells[5].Text;*/

                for (int i = 0; i < GridView1.Rows.Count; ++i) {

                    string id = GridView1.Rows[i].Cells[0].Text;
                    string insigName = GridView1.Rows[i].Cells[2].Text;
                    string dateReq = GridView1.Rows[i].Cells[3].Text;

                    LinkButton lbu = new LinkButton();
                    lbu.Text = "<img src='Image/Small/next.png'></img>";
                    lbu.CssClass = "ps-button";
                    lbu.Click += (e2, e3) => {


                        Citizen_id = DatabaseManager.ExecuteString("SELECT CITIZEN_ID FROM TB_INSIG_PERSON WHERE IP_ID = " + int.Parse(id));
                        QueryString = DatabaseManager.GetPerson(Citizen_id);
                        int insigTargetID = DatabaseManager.ExecuteInt("SELECT INSIG_ID FROM TB_INSIG_PERSON WHERE IP_ID = " + int.Parse(id));


                        lbTitleName.Text = Util.IsBlank(QueryString.PS_TITLE_NAME) ? "-" : QueryString.PS_TITLE_NAME;
                        lbName.Text = Util.IsBlank(QueryString.PS_FIRSTNAME) ? "-" : QueryString.PS_FIRSTNAME;
                        lbLastName.Text = Util.IsBlank(QueryString.PS_LASTNAME) ? "-" : QueryString.PS_LASTNAME;
                        lbGender.Text = Util.IsBlank(QueryString.PS_GENDER_NAME) ? "-" : QueryString.PS_GENDER_NAME;
                        lbBirthDate.Text = Util.IsBlank(QueryString.PS_BIRTHDAY_DATE.ToString()) ? "-" : QueryString.PS_BIRTHDAY_DATE.Value.ToLongDateString();
                        lbDateInwork.Text = Util.IsBlank(QueryString.PS_INWORK_DATE.ToString()) ? "-" : QueryString.PS_INWORK_DATE.Value.ToLongDateString();
                        lbFirstPosition.Text = Util.IsBlank(QueryString.FIRST_POSITION_NAME) ? "-" : QueryString.FIRST_POSITION_NAME;
                        lbPositionCurrent.Text = Util.IsBlank(QueryString.PS_POSITION_NAME) ? "-" : QueryString.PS_POSITION_NAME;
                        lbType.Text = Util.IsBlank(QueryString.PS_STAFFTYPE_NAME) ? "-" : QueryString.PS_STAFFTYPE_NAME;
                        lbDegree.Text = Util.IsBlank(QueryString.PS_ADMIN_POS_NAME) ? "-" : QueryString.PS_ADMIN_POS_NAME;
                        lbInsigReq.Text = insigName;
                        
                        string fileName;
                        switch (insigTargetID) {
                            case 12: fileName = "บ.ม."; break;
                            case 11: fileName = "บ.ช."; break;
                            case 10: fileName = "จ.ม."; break;
                            case 9: fileName = "จ.ช."; break;
                            case 8: fileName = "ต.ม."; break;
                            case 7: fileName = "ต.ช."; break;
                            case 6: fileName = "ท.ม."; break;
                            case 5: fileName = "ท.ช."; break;
                            case 4: fileName = "ป.ม."; break;
                            case 3: fileName = "ป.ช."; break;
                            case 2: fileName = "ม.ว.ม."; break;
                            default: fileName = "ม.ป.ช."; break;
                        }
                        imgInsigReq.Attributes["src"] = "Image/Insignia/" + fileName + ".png";

                        lbDateReq.Text = dateReq;

                        Session["IP_ID"] = int.Parse(id);

                        MultiView1.ActiveViewIndex = 1;

                        error_area.Attributes["class"] = null;
                        error_area.InnerHtml = "";
                    };
                    TableCell cell = new TableCell();
                    cell.Controls.Add(lbu);
                    GridView1.Rows[i].Cells.Add(cell);
                }

                lbNoData.Visible = false;
            } else {
                lbNoData.Visible = true;
            }
        }

        protected void lbuAllow_Click(object sender, EventArgs e) {
            /*if (tbF1Comment.Text == "" || (!rbAllow.Checked && !rbNotAllow.Checked)) {
                error_area.Attributes["class"] = "alert alert_danger";
                error_area.InnerHtml = "";
                error_area.InnerHtml += "<strong>เกิดข้อผิดพลาด!</strong><br>";
                if (tbF1Comment.Text == "") {
                    error_area.InnerHtml += "<div class='hm_tab'></div>- กรุณากรอก <strong>ความเห็น</strong><br>";
                }
                if (!rbAllow.Checked && !rbNotAllow.Checked) {
                    error_area.InnerHtml += "<div class='hm_tab'></div>- กรุณาเลือก <strong>การอนุมัติ</strong><br>";
                }

            } else {
                error_area.Attributes["class"] = null;
                error_area.InnerHtml = "";
                MultiView1.ActiveViewIndex = 2;
                int allow = 1;
                if (rbNotAllow.Checked) {
                    allow = 2;
                }
                LeaveData leaveData = (LeaveData)Session["LeaveData"];
                LeaveBossData leaveBossData = leaveData.GetCurrentBoss();

                if (leaveData.LeaveStatusID == 1) {
                    leaveBossData.Comment = tbF1Comment.Text;
                    leaveBossData.Allow = allow;
                    leaveBossData.AllowDate = DateTime.Today;
                    leaveData.ExecuteAllow();
                } else if (leaveData.LeaveStatusID == 4) {
                    leaveBossData.CancelComment = tbF1Comment.Text;
                    leaveBossData.CancelAllow = allow;
                    leaveBossData.CancelAllowDate = DateTime.Today;
                    leaveData.ExecuteCancelAllow();
                }

            }*/

            int ipID = (int)Session["IP_ID"];
            int allow = 2;
            if (rbNotAllow.Checked) {
                allow = 4;
            }

            bool ok = false;
            using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING)) {
                con.Open();
                using (OracleCommand com = new OracleCommand("SELECT REQ_DATE FROM TB_INSIG_PERSON WHERE IP_ID = " + ipID, con)) {
                    using (OracleDataReader reader = com.ExecuteReader()) {
                        while (reader.Read()) {
                            DateTime reqDate = reader.GetDateTime(0);
                            if(Util.ToDateTimeOracle(tbDateAllow.Text) > reqDate) {
                                ok = true;
                            } else {
                                ok = false;
                            }
                        }
                    }
                }
            }

            if(ok) {

                using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING)) {
                    con.Open();
                    using (OracleCommand com = new OracleCommand("UPDATE TB_INSIG_PERSON SET GET_DATE = :GET_DATE, IP_STATUS_ID = :IP_STATUS_ID WHERE IP_ID = :IP_ID", con)) {
                        com.Parameters.AddWithValue("GET_DATE", Util.ToDateTimeOracle(tbDateAllow.Text));
                        com.Parameters.AddWithValue("IP_STATUS_ID", allow);
                        com.Parameters.AddWithValue("IP_ID", ipID);
                        com.ExecuteNonQuery();
                    }
                }

                MultiView1.ActiveViewIndex = 2;
            } else {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('วันที่ไม่ถูกต้อง')", true);
            }
            


        }

        protected void lbu1_Click(object sender, EventArgs e) {
            Response.Redirect("Default.aspx");
        }

        protected void lbu2_Click(object sender, EventArgs e) {
            Response.Redirect("INS_Allow.aspx");
        }

        protected void lbuBack_Click(object sender, EventArgs e) {
            PersonnelSystem ps = PersonnelSystem.GetPersonnelSystem(this);
            Person loginPerson = ps.LoginPerson;

            int count = DatabaseManager.ExecuteInt("SELECT COUNT(*) FROM TB_INSIG_PERSON WHERE IP_STATUS_ID = 1");

            if (count == 0) {
                error_area.InnerHtml = "ไม่มีรายการที่ท่านต้องอนุมัติ";
            } else {
                error_area.InnerHtml = "กรุณาเลือกรายการที่ต้องการอนุมัติ";
            }
            error_area.Attributes["class"] = null;
            error_area.InnerHtml = "";
            MultiView1.ActiveViewIndex = 0;
        }
    }
}