using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WEB_PERSONAL.Class;
using System.Data.OracleClient;

namespace WEB_PERSONAL {
    public partial class LeaveAllow : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
    
            PersonnelSystem ps = PersonnelSystem.GetPersonnelSystem(this);
            Person loginPerson = ps.LoginPerson;

            int count = DatabaseManager.GetLeaveRequiredCountByCommander(loginPerson.PS_CITIZEN_ID);
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

                SqlDataSource sds = DatabaseManager.CreateSQLDataSource("SELECT LEV_DATA.LEAVE_ID รหัสการลา, (SELECT PS_FN_TH || ' ' || PS_LN_TH FROM PS_PERSON WHERE PS_CITIZEN_ID = LEV_DATA.PS_ID) ชื่อผู้ลา, (SELECT LEAVE_TYPE_NAME FROM LEV_TYPE WHERE LEV_TYPE.LEAVE_TYPE_ID = LEV_DATA.LEAVE_TYPE_ID) ประเภทการลา, REQ_DATE วันที่ข้อมูล, (SELECT LEAVE_STATUS_NAME FROM LEV_STATUS WHERE LEAVE_STATUS_ID = LEV_DATA.LEAVE_STATUS_ID) สถานะ FROM LEV_DATA, LEV_BOSS_DATA WHERE LEAVE_STATUS_ID IN(1,4) AND LEV_DATA.LEAVE_ID = LEV_BOSS_DATA.LEAVE_ID AND LEV_DATA.BOSS_STATE = LEV_BOSS_DATA.STATE AND LEV_BOSS_DATA.CITIZEN_ID = '" + loginPerson.PS_CITIZEN_ID + "'");
                GridView1.DataSource = sds;
                GridView1.DataBind();

                Util.NormalizeGridViewDate(GridView1, 3);

                TableHeaderCell newHeader = new TableHeaderCell();
                newHeader.Text = "เลือก";
                GridView1.HeaderRow.Cells.Add(newHeader);

                GridView1.HeaderRow.Cells[0].Text = "<img src='Image/Small/ID.png' class='icon_left'/>" + GridView1.HeaderRow.Cells[0].Text;
                GridView1.HeaderRow.Cells[1].Text = "<img src='Image/Small/person2.png' class='icon_left'/>" + GridView1.HeaderRow.Cells[1].Text;
                GridView1.HeaderRow.Cells[2].Text = "<img src='Image/Small/list.png' class='icon_left'/>" + GridView1.HeaderRow.Cells[2].Text;
                GridView1.HeaderRow.Cells[3].Text = "<img src='Image/Small/calendar.png' class='icon_left'/>" + GridView1.HeaderRow.Cells[3].Text;
                GridView1.HeaderRow.Cells[4].Text = "<img src='Image/Small/question.png' class='icon_left'/>" + GridView1.HeaderRow.Cells[4].Text;
                GridView1.HeaderRow.Cells[5].Text = "<img src='Image/Small/pointer.png' class='icon_left'/>" + GridView1.HeaderRow.Cells[5].Text;

                for (int i = 0; i < GridView1.Rows.Count; ++i) {

                    string id = GridView1.Rows[i].Cells[0].Text;

                    LinkButton lbu = new LinkButton();
                    lbu.Text = "<img src='Image/Small/next.png'></img>";
                    lbu.CssClass = "ps-button";
                    lbu.Click += (e2, e3) => {

                        LeaveData leaveData = new LeaveData();
                        leaveData.Load(int.Parse(id));
                        Session["LeaveData"] = leaveData;

                        trPSBirthDate.Visible = false;
                        trPSWorkInDate.Visible = false;
                        trWifeName.Visible = false;
                        trGBDate.Visible = false;
                        trOrdained.Visible = false;
                        trTempleName.Visible = false;
                        trTempleLocation.Visible = false;
                        trOrdainDate.Visible = false;
                        trHujed.Visible = false;
                        trReason.Visible = false;
                        trContact.Visible = false;
                        trPhone.Visible = false;
                        trRestSave.Visible = false;
                        trRestLeft.Visible = false;
                        trRestTotal.Visible = false;
                        trStatistic.Visible = false;
                        //trCLOldComment.Visible = false;
                        //trCLOldDate.Visible = false;
                        //trCHOldComment.Visible = false;
                        //trCHOldDate.Visible = false;
                        trCancelReason.Visible = false;

                        if (leaveData.LeaveTypeID == 1) {
                            trStatistic.Visible = true;
                            trReason.Visible = true;
                            trContact.Visible = true;
                            trPhone.Visible = true;
                        } else if (leaveData.LeaveTypeID == 2) {
                            trStatistic.Visible = true;
                            trReason.Visible = true;
                            trContact.Visible = true;
                            trPhone.Visible = true;
                        } else if (leaveData.LeaveTypeID == 3) {
                            trStatistic.Visible = true;
                            trReason.Visible = true;
                            trContact.Visible = true;
                            trPhone.Visible = true;
                        } else if (leaveData.LeaveTypeID == 4) {
                            trRestSave.Visible = true;
                            trRestLeft.Visible = true;
                            trRestTotal.Visible = true;
                            trContact.Visible = true;
                            trPhone.Visible = true;
                        } else if (leaveData.LeaveTypeID == 5) {
                            trWifeName.Visible = true;
                            trGBDate.Visible = true;
                            trContact.Visible = true;
                            trPhone.Visible = true;
                        } else if (leaveData.LeaveTypeID == 6) {
                            trPSBirthDate.Visible = true;
                            trPSWorkInDate.Visible = true;
                            trOrdained.Visible = true;
                            trTempleName.Visible = true;
                            trTempleLocation.Visible = true;
                            trOrdainDate.Visible = true;
                        } else if (leaveData.LeaveTypeID == 7) {
                            trPSBirthDate.Visible = true;
                            trPSWorkInDate.Visible = true;
                            trHujed.Visible = true;
                        }

                        if (leaveData.LeaveStatusID == 2) {

                        } else if (leaveData.LeaveStatusID == 4) {
                            //trCLOldComment.Visible = true;
                            //trCLOldDate.Visible = true;
                            //trCHOldComment.Visible = true;
                            //trCHOldDate.Visible = true;
                            trCancelReason.Visible = true;
                        }

                        lbLeaveID.Text = leaveData.LeaveID.ToString();
                        lbLeaveTypeName.Text = leaveData.LeaveTypeName;
                        lbReqDate.Text = leaveData.RequestDate.Value.ToLongDateString();
                        lbPSName.Text = leaveData.Person.PS_FN_TH + " " + leaveData.Person.PS_LN_TH;
                        lbPSPos.Text = leaveData.Person.PS_WORK_POS_NAME;
                        lbPSAPos.Text = leaveData.Person.PS_ADMIN_POS_NAME;
                        if(Util.IsBlank(leaveData.Person.PS_WORK_DIVISION_ID)) {
                            lbPSDept.Text = leaveData.Person.PS_DIVISION_NAME;
                        } else {
                            lbPSDept.Text = leaveData.Person.PS_WORK_DIVISION_NAME;
                        }
                        

                        //if (leaveData.PS_BirthDate.HasValue) {
                            lbPSBirthDate.Text = leaveData.Person.PS_BIRTHDAY_DATE.Value.ToLongDateString();
                        //} else {
                        //    lbPSBirthDate.Text = "-";
                        //}
                        //if (leaveData.PS_WorkInDate.HasValue) {
                            lbPSWorkInDate.Text = leaveData.Person.PS_INWORK_DATE.Value.ToLongDateString();
                        //} else {
                        //    lbPSWorkInDate.Text = "-";
                       // }

                        lbRestSave.Text = leaveData.RestSave + " วัน";
                        lbRestLeft.Text = leaveData.RestLeft + " วัน";
                        lbRestTotal.Text = leaveData.RestTotal + " วัน";

                        lbWifeName.Text = leaveData.WifeFirstName + " " + leaveData.WifeLastName;
                        if (leaveData.GiveBirthDate.HasValue) {
                            lbGBDate.Text = leaveData.GiveBirthDate.Value.ToLongDateString();
                        } else {
                            lbGBDate.Text = "-";
                        }

                        lbOrdained.Text = leaveData.Ordained == 1 ? "เคย" : "ไม่เคย";
                        lbTempleName.Text = leaveData.TempleName;
                        lbTempleLocation.Text = leaveData.TempleLocation;
                        if (leaveData.OrdainDate.HasValue) {
                            lbOrdainDate.Text = leaveData.OrdainDate.Value.ToLongDateString();
                        } else {
                            lbOrdainDate.Text = "-";
                        }

                        lbHujed.Text = leaveData.Hujed == 1 ? "เคย" : "ไม่เคย";

                        if (leaveData.FromDate.HasValue) {
                            lbFTTDate.Text = leaveData.FromDate.Value.ToLongDateString() + " ถึง " + leaveData.ToDate.Value.ToLongDateString() + " รวม " + leaveData.TotalDay + " วัน";
                        } else {
                            lbFTTDate.Text = "ไม่เคยลา";
                        }
                        lbStatistic.Text = "ลามาแล้ว " + leaveData.CountPast + " วัน / ลาครั้งนี้ " + leaveData.CountNow + " วัน / รวม " + leaveData.CountTotal + " วัน";

                        lbReason.Text = leaveData.Reason;
                        lbContact.Text = leaveData.Contact;
                        lbPhone.Text = leaveData.Telephone;

                        if (leaveData.LastFromDate.HasValue) {
                            lbLastFTTDate.Text = leaveData.LastFromDate.Value.ToLongDateString() + " ถึง " + leaveData.LastToDate.Value.ToLongDateString() + " รวม " + leaveData.LastTotalDay + " วัน";
                        } else {
                            lbLastFTTDate.Text = "ไม่เคยลา";
                        }

                        

                        if (leaveData.DocterCertificationFileName != "") {
                            divDrCer.InnerHtml = "<a href='Upload/Drcer/" + leaveData.DocterCertificationFileName + "'><img src='Upload/DrCer/" + leaveData.DocterCertificationFileName + "' style='width: 200px;' /></a>";
                        }

                        /*lbCLComment.Text = "";
                        for (int j = 0; j < leaveData.LeaveBossDataList.Count; j++) {
                            if(!leaveData.LeaveBossDataList[j].Allow.HasValue) {
                                continue;
                            }
                            LeaveBossData leaveBossData = leaveData.LeaveBossDataList[j];
                            lbCLComment.Text += "<div style='color: #808080'>" + leaveBossData.Person.FirstNameAndLastName + " / " + leaveBossData.AllowDate.Value.ToLongDateString() +  "</div><div style='margin-bottom: 10px;'> - " + leaveBossData.Comment + "</div>";
                        }*/

                        {
                            TableRow row = new TableRow();
                            TableCell cell2;
                            Image image;
                            tbBoss.Rows.Add(row);

                            for (int j = 0; j < leaveData.BossStateMax; j++) {

                                LeaveBossData leaveBossData = leaveData.LeaveBossDataList[j];

                                cell2 = new TableCell();
                                cell2.Style.Add("vertical-align", "top");
                                


                                Table tb = new Table();
                                tb.CssClass = "ps-table-1";
                                tb.Style.Add("text-align", "left");
                                {
                                    TableRow tr;
                                    TableCell cell3;

                                    tr = new TableRow();
                                    tb.Rows.Add(tr);

                                    cell3 = new TableCell();
                                    cell3.ColumnSpan = 2;
                                    cell3.Style.Add("text-align", "center");
                                    image = new Image();
                                    image.CssClass = "ps-ms-main-drop-profile-pic";

                                    string imagePath = DatabaseManager.GetPersonImageFileName(leaveBossData.CitizenID);
                                    if (imagePath != "") {
                                        image.Attributes["src"] = "Upload/PersonImage/" + imagePath;
                                        cell3.Controls.Add(image);
                                    }
                                    tr.Cells.Add(cell3);

                                    tr = new TableRow();
                                    tb.Rows.Add(tr);

                                    cell3 = new TableCell();
                                    cell3.Text = "ชื่อ";
                                    tr.Cells.Add(cell3);

                                    cell3 = new TableCell();
                                    cell3.Text = leaveBossData.Person.FirstNameAndLastName;
                                    tr.Cells.Add(cell3);

                                    tr = new TableRow();
                                    tb.Rows.Add(tr);

                                    cell3 = new TableCell();
                                    cell3.Text = "ตำแหน่ง";
                                    tr.Cells.Add(cell3);

                                    cell3 = new TableCell();
                                    cell3.Text = leaveBossData.Person.PS_WORK_POS_NAME;
                                    tr.Cells.Add(cell3);

                                    tr = new TableRow();
                                    tb.Rows.Add(tr);

                                    cell3 = new TableCell();
                                    cell3.Text = "ระดับ";
                                    tr.Cells.Add(cell3);

                                    cell3 = new TableCell();
                                    cell3.Text = leaveBossData.Person.PS_ADMIN_POS_NAME;// + "<br />" + leaveBossData.Person.AdminPositionNameExtra();
                                    tr.Cells.Add(cell3);

                                    tr = new TableRow();
                                    tb.Rows.Add(tr);

                                    cell3 = new TableCell();
                                    cell3.Text = "การอนุมัติ";
                                    tr.Cells.Add(cell3);

                                    cell3 = new TableCell();                                    
                                    if (leaveData.LeaveStatusID == 1) {
                                        if (leaveBossData.Allow.HasValue) {
                                            cell3.Text = "<div style='color: #808080;'>" + leaveBossData.AllowDate.Value.ToLongDateString() + "</div>";
                                            if (leaveBossData.Allow.Value == 1) {
                                                cell3.Text += "<div style='color: green'>อนุญาต</div>";
                                            } else {
                                                cell3.Text += "<div style='color: red'>ไม่อนุญาต</div>";
                                            }
                                            cell3.Text += "<div style='color: #000000;'>" + leaveBossData.Comment + "</div>";

                                        }
                                    } else if(leaveData.LeaveStatusID == 4) {
                                        if (leaveBossData.CancelAllow.HasValue) {
                                            cell3.Text = "<div style='color: #808080;'>" + leaveBossData.CancelAllowDate.Value.ToLongDateString() + "</div>";
                                            if (leaveBossData.CancelAllow.Value == 1) {
                                                cell3.Text += "<div style='color: green'>อนุญาต</div>";
                                            } else {
                                                cell3.Text += "<div style='color: red'>ไม่อนุญาต</div>";
                                            }
                                            cell3.Text += "<div style='color: #000000;'>" + leaveBossData.CancelComment + "</div>";

                                        }
                                    }
                                    
                                    tr.Cells.Add(cell3);


                                }
                                
                                cell2.Controls.Add(tb);

                                row.Cells.Add(cell2);
                            }
                        }

                        lbCancelReason.Text = leaveData.CancelReason;

                        /*if (leaveData.LeaveStatusID >= 1 && leaveData.LeaveStatusID <= 4) {
                            if(leaveData.CL_ID == null) {
                                lbCLComment.Text = "-";
                                lbCLDate.Text = "-";
                            } else {
                                lbCLComment.Text = leaveData.CL_Comment;
                                lbCLDate.Text = leaveData.CL_Date.Value.ToLongDateString();
                            }
                            
                        } else if (leaveData.LeaveStatusID >= 5 && leaveData.LeaveStatusID <= 8) {
                            if(leaveData.CL_ID == null) {
                                lbCLOldComment.Text = "-";
                                lbCLOldDate.Text = "-";
                                lbCHOldComment.Text = leaveData.CH_Comment;
                                lbCHOldDate.Text = leaveData.CH_Date.Value.ToLongDateString();
                                lbCancelReason.Text = leaveData.CancelReason;
                                lbCLComment.Text = "-";
                                lbCLDate.Text = "-";
                            } else {
                                lbCLOldComment.Text = leaveData.CL_Comment;
                                lbCLOldDate.Text = leaveData.CL_Date.Value.ToLongDateString();
                                lbCHOldComment.Text = leaveData.CH_Comment;
                                lbCHOldDate.Text = leaveData.CH_Date.Value.ToLongDateString();
                                lbCancelReason.Text = leaveData.CancelReason;
                                lbCLComment.Text = leaveData.CL_CancelComment;
                                lbCLDate.Text = leaveData.CL_CancelDate.Value.ToLongDateString();
                            }
                            
                        }*/


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

        protected void lbuAddComment_Click(object sender, EventArgs e) {
            if (tbF1Comment.Text == "" || (!rbAllow.Checked && !rbNotAllow.Checked)) {
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

                if(leaveData.LeaveStatusID == 1) {
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

                

                /*if (leaveData.LeaveStatusID == 2) {
                    leaveData.CH_Comment = tbF1Comment.Text;
                    leaveData.CH_Allow = allow;
                    leaveData.CH_Date = DateTime.Today;
                    leaveData.ExecuteAllow();
                } else if (leaveData.LeaveStatusID == 6) {
                    leaveData.CH_CancelComment = tbF1Comment.Text;
                    leaveData.CH_CancelAllow = allow;
                    leaveData.CH_CancelDate = DateTime.Today;
                    leaveData.ExecuteCancelAllow();
                }*/
                    

                
            }
        }

        protected void lbu1_Click(object sender, EventArgs e) {
            Response.Redirect("Default.aspx");
        }

        protected void lbu2_Click(object sender, EventArgs e) {
            Response.Redirect("LeaveAllow.aspx");
        }

        protected void lbuBack_Click(object sender, EventArgs e) {
            PersonnelSystem ps = PersonnelSystem.GetPersonnelSystem(this);
            Person loginPerson = ps.LoginPerson;

            int count = 0;
            using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING)) {
                con.Open();
                using (OracleCommand com = new OracleCommand("SELECT COUNT(LEAVE_ID) FROM LEV_LEAVE WHERE CMD_HIGH_ID = '" + loginPerson.PS_CITIZEN_ID + "' AND LEV_LEAVE.STATE_ID = 3", con)) {
                    using (OracleDataReader reader = com.ExecuteReader()) {
                        while (reader.Read()) {
                            count = int.Parse(reader.GetValue(0).ToString());
                        }
                    }
                }
            }
            if (count == 0) {
                error_area.InnerHtml = "ไม่มีรายการที่ท่านต้องอนุมัติ";
            } else {
                error_area.InnerHtml = "กรุณาเลือกรายการที่ต้องอนุมัติ";
            }
            //error_area.Attributes["class"] = "alert alert_info";
            error_area.Attributes["class"] = null;
            error_area.InnerHtml = "";
            MultiView1.ActiveViewIndex = 0;
        }
    }
}