using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WEB_PERSONAL.Class;
using System.Data.OracleClient;

namespace WEB_PERSONAL {
    public partial class LeaveCancel : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            /*if (!IsPostBack) {
                ChangeNotification("info", "กรุณากรอกข้อมูล");
            }*/
                PersonnelSystem ps = PersonnelSystem.GetPersonnelSystem(this);
                Person loginPerson = ps.LoginPerson;

            //------

            {
                SqlDataSource sds = DatabaseManager.CreateSQLDataSource("SELECT LEAVE_ID รหัสการลา, (SELECT LEAVE_TYPE_NAME FROM LEV_TYPE WHERE LEV_TYPE.LEAVE_TYPE_ID = LEV_DATA.LEAVE_TYPE_ID) ประเภทการลา, REQ_DATE วันที่ข้อมูล, FROM_DATE จากวันที่, TO_DATE ถึงวันที่, TOTAL_DAY รวมวัน FROM LEV_DATA WHERE LEAVE_STATUS_ID = 1 AND PS_ID = '" + loginPerson.PS_CITIZEN_ID + "' ORDER BY LEAVE_ID DESC");
                gvLeaveProgress.DataSource = sds;
                gvLeaveProgress.DataBind();

                if (gvLeaveProgress.Rows.Count > 0) {
                    lbLeaveProgress.Visible = false;
                    TableHeaderCell headerCell = new TableHeaderCell();
                    headerCell.Text = "เลือก";
                    gvLeaveProgress.HeaderRow.Cells.Add(headerCell);

                    gvLeaveProgress.HeaderRow.Cells[0].Text = "<img src='Image/Small/ID.png' class='icon_left'/>" + gvLeaveProgress.HeaderRow.Cells[0].Text;
                    gvLeaveProgress.HeaderRow.Cells[1].Text = "<img src='Image/Small/list.png' class='icon_left'/>" + gvLeaveProgress.HeaderRow.Cells[1].Text;
                    gvLeaveProgress.HeaderRow.Cells[2].Text = "<img src='Image/Small/calendar.png' class='icon_left'/>" + gvLeaveProgress.HeaderRow.Cells[2].Text;
                    gvLeaveProgress.HeaderRow.Cells[3].Text = "<img src='Image/Small/calendar.png' class='icon_left'/>" + gvLeaveProgress.HeaderRow.Cells[3].Text;
                    gvLeaveProgress.HeaderRow.Cells[4].Text = "<img src='Image/Small/calendar.png' class='icon_left'/>" + gvLeaveProgress.HeaderRow.Cells[4].Text;
                    gvLeaveProgress.HeaderRow.Cells[6].Text = "<img src='Image/Small/pointer.png' class='icon_left'/>" + gvLeaveProgress.HeaderRow.Cells[6].Text;

                    for (int i = 0; i < gvLeaveProgress.Rows.Count; ++i) {  

                        string ID = gvLeaveProgress.Rows[i].Cells[0].Text;
                        TableCell cell = new TableCell();
                        LinkButton btn = new LinkButton();
                        btn.CssClass = "ps-button";
                        btn.Text = "<img src='Image/Small/next.png'></img>";
                        btn.Click += (e2, e3) => {

                            lbuCancelFinish.Visible = false;
                            lbuCancelProgressFinish.Visible = true;
                            trCancelReason.Visible = false;

                            LeaveData leaveData = new LeaveData();
                            leaveData.Load(int.Parse(ID));

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

                            lbLeaveID.Text = leaveData.LeaveID.ToString();
                            lbLeaveTypeName.Text = leaveData.LeaveTypeName;
                            lbReqDate.Text = leaveData.RequestDate.Value.ToLongDateString();
                            lbPSName.Text = leaveData.Person.PS_FN_TH + " " + leaveData.Person.PS_LN_TH;
                            lbPSPos.Text = leaveData.Person.PS_WORK_POS_NAME;
                            lbPSAPos.Text = leaveData.Person.PS_ADMIN_POS_NAME;
                            if (Util.IsBlank(leaveData.Person.PS_WORK_DIVISION_ID)) {
                                lbPSDept.Text = leaveData.Person.PS_DIVISION_NAME;
                            } else {
                                lbPSDept.Text = leaveData.Person.PS_WORK_DIVISION_NAME;
                            }
                            lbPSBirthDate.Text = leaveData.Person.PS_BIRTHDAY_DATE.Value.ToLongDateString();
                            lbPSWorkInDate.Text = leaveData.Person.PS_INWORK_DATE.Value.ToLongDateString();

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
                                        if (leaveBossData.Allow.HasValue) {
                                            cell3.Text = "<div style='color: #808080;'>" + leaveBossData.AllowDate.Value.ToLongDateString() + "</div>";
                                            if (leaveBossData.Allow.Value == 1) {
                                                cell3.Text += "<div style='color: green'>อนุญาต</div>";
                                            } else {
                                                cell3.Text += "<div style='color: red'>ไม่อนุญาต</div>";
                                            }
                                            cell3.Text += "<div style='color: #000000;'>" + leaveBossData.Comment + "</div>";

                                        }
                                        tr.Cells.Add(cell3);


                                    }

                                    cell2.Controls.Add(tb);

                                    row.Cells.Add(cell2);
                                }
                            }






                            Session["LeaveData"] = leaveData;

                            MV1.ActiveViewIndex = 1;
                            //Response.Redirect("ViewLeaveForm.aspx?Form=1&LeaveID=" + ID);
                        };
                        cell.Controls.Add(btn);
                        gvLeaveProgress.Rows[i].Cells.Add(cell);

                    }

                    Util.NormalizeGridViewDate(gvLeaveProgress, 2);
                    Util.NormalizeGridViewDate(gvLeaveProgress, 3);
                    Util.NormalizeGridViewDate(gvLeaveProgress, 4);
                } else {
                    lbLeaveProgress.Visible = true;
                }

            }

            //--------

            {
                SqlDataSource sds = DatabaseManager.CreateSQLDataSource("SELECT LEAVE_ID รหัสการลา, (SELECT LEAVE_TYPE_NAME FROM LEV_TYPE WHERE LEV_TYPE.LEAVE_TYPE_ID = LEV_DATA.LEAVE_TYPE_ID) ประเภทการลา, REQ_DATE วันที่ข้อมูล, FROM_DATE จากวันที่, TO_DATE ถึงวันที่, TOTAL_DAY รวมวัน FROM LEV_DATA WHERE LEAVE_STATUS_ID = 3 AND PS_ID = '" + loginPerson.PS_CITIZEN_ID + "' AND V_ALLOW = 1 AND CEIL(FROM_DATE - CURRENT_DATE) >= 3 ORDER BY LEAVE_ID DESC");
                gvLeave.DataSource = sds;
                gvLeave.DataBind();

                if (gvLeave.Rows.Count > 0) {
                    lbLeave.Visible = false;
                    TableHeaderCell headerCell = new TableHeaderCell();
                    headerCell.Text = "เลือก";
                    gvLeave.HeaderRow.Cells.Add(headerCell);

                    gvLeave.HeaderRow.Cells[0].Text = "<img src='Image/Small/ID.png' class='icon_left'/>" + gvLeave.HeaderRow.Cells[0].Text;
                    gvLeave.HeaderRow.Cells[1].Text = "<img src='Image/Small/list.png' class='icon_left'/>" + gvLeave.HeaderRow.Cells[1].Text;
                    gvLeave.HeaderRow.Cells[2].Text = "<img src='Image/Small/calendar.png' class='icon_left'/>" + gvLeave.HeaderRow.Cells[2].Text;
                    gvLeave.HeaderRow.Cells[3].Text = "<img src='Image/Small/calendar.png' class='icon_left'/>" + gvLeave.HeaderRow.Cells[3].Text;
                    gvLeave.HeaderRow.Cells[4].Text = "<img src='Image/Small/calendar.png' class='icon_left'/>" + gvLeave.HeaderRow.Cells[4].Text;
                    gvLeave.HeaderRow.Cells[6].Text = "<img src='Image/Small/pointer.png' class='icon_left'/>" + gvLeave.HeaderRow.Cells[6].Text;

                    for (int i = 0; i < gvLeave.Rows.Count; ++i) {

                        string ID = gvLeave.Rows[i].Cells[0].Text;
                        TableCell cell = new TableCell();
                        LinkButton btn = new LinkButton();
                        btn.CssClass = "ps-button";
                        btn.Text = "<img src='Image/Small/next.png'></img>";
                        btn.Click += (e2, e3) => {

                            lbuCancelFinish.Visible = true;
                            lbuCancelProgressFinish.Visible = false;
                            trCancelReason.Visible = true;

                            LeaveData leaveData = new LeaveData();
                            leaveData.Load(int.Parse(ID));

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

                            lbLeaveID.Text = leaveData.LeaveID.ToString();
                            lbLeaveTypeName.Text = leaveData.LeaveTypeName;
                            lbReqDate.Text = leaveData.RequestDate.Value.ToLongDateString();
                            lbPSName.Text = leaveData.Person.PS_FN_TH + " " + leaveData.Person.PS_LN_TH;
                            lbPSPos.Text = leaveData.Person.PS_WORK_POS_NAME;
                            lbPSAPos.Text = leaveData.Person.PS_ADMIN_POS_NAME;
                            if (Util.IsBlank(leaveData.Person.PS_WORK_DIVISION_ID)) {
                                lbPSDept.Text = leaveData.Person.PS_DIVISION_NAME;
                            } else {
                                lbPSDept.Text = leaveData.Person.PS_WORK_DIVISION_NAME;
                            }
                            lbPSBirthDate.Text = leaveData.Person.PS_BIRTHDAY_DATE.Value.ToLongDateString();
                            lbPSWorkInDate.Text = leaveData.Person.PS_INWORK_DATE.Value.ToLongDateString();

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
                                    if(leaveBossData.Allow.HasValue) {
                                        cell3.Text = "<div style='color: #808080;'>" + leaveBossData.AllowDate.Value.ToLongDateString() + "</div>";
                                        if(leaveBossData.Allow.Value == 1) {
                                            cell3.Text += "<div style='color: green'>อนุญาต</div>";
                                        } else {
                                            cell3.Text += "<div style='color: red'>ไม่อนุญาต</div>";
                                        }
                                        cell3.Text += "<div style='color: #000000;'>" + leaveBossData.Comment + "</div>";

                                    }
                                    tr.Cells.Add(cell3);


                                }
                                
                                cell2.Controls.Add(tb);

                                row.Cells.Add(cell2);
                            }
                        }





                            Session["LeaveData"] = leaveData;

                            MV1.ActiveViewIndex = 1;
                            //Response.Redirect("ViewLeaveForm.aspx?Form=1&LeaveID=" + ID);
                        };
                        cell.Controls.Add(btn);
                        gvLeave.Rows[i].Cells.Add(cell);

                    }

                    Util.NormalizeGridViewDate(gvLeave, 2);
                    Util.NormalizeGridViewDate(gvLeave, 3);
                    Util.NormalizeGridViewDate(gvLeave, 4);
                } else {
                    lbLeave.Visible = true;
                }
            }

                

            
        }

        protected void lbuCancelBack_Click(object sender, EventArgs e) {
            MV1.ActiveViewIndex = 0;
            ClearNotification();
        }

        protected void lbuCancelFinish_Click(object sender, EventArgs e) {

            LeaveData leaveData = (LeaveData)Session["LeaveData"];
            leaveData.CancelReason = tbCancelReason.Text;
            leaveData.ExecuteCancel();

            ClearNotification();
            //ChangeNotification("success", "<strong>ยกเลิกลาสำเร็จ!</strong> คุณสามารถตรวจสอบสถานะการลาได้ที่เมนู การลา -> สถานะ และ ประวัติการลา");
            MV1.ActiveViewIndex = 2;

        }

        protected void lbuBackMain_Click(object sender, EventArgs e) {
            Response.Redirect("Default.aspx");
        }

        private void ChangeNotification(string type, string text) {
            switch (type) {
                case "info": notification.Attributes["class"] = "alert alert_info"; break;
                case "success": notification.Attributes["class"] = "alert alert_success"; break;
                case "warning": notification.Attributes["class"] = "alert alert_warning"; break;
                case "danger": notification.Attributes["class"] = "alert alert_danger"; break;
                default: notification.Attributes["class"] = null; break;
            }
            notification.InnerHtml = text;
        }
        private void ClearNotification() {
            notification.Attributes["class"] = null;
            notification.InnerHtml = "";
        }
        private void AddNotification(string text) {
            notification.InnerHtml += text;
        }

        protected void lbuCancelProgressFinish_Click(object sender, EventArgs e) {
            LeaveData leaveData = (LeaveData)Session["LeaveData"];
            leaveData.ExecuteCancelByUser();
            MV1.ActiveViewIndex = 3;
        }
    }
}