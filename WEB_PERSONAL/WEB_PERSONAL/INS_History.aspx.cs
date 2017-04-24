using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WEB_PERSONAL.Class;
using System.Data.OracleClient;

namespace WEB_PERSONAL
{

    public partial class INS_History : System.Web.UI.Page
    {
        private Person loginPerson;
        protected void Page_Load(object sender, EventArgs e)
        {
            PersonnelSystem ps = PersonnelSystem.GetPersonnelSystem(this);
            loginPerson = ps.LoginPerson;

            FuncGVFinish();
            FuncGVProcessing();
            FuncGVHistory();

            if (!IsPostBack)
            {
               
            }
        }

        protected void gvHistory_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvHistory.PageIndex = e.NewPageIndex;
            FuncGVHistory();
        }
        protected void gvProgressing_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvProgressing.PageIndex = e.NewPageIndex;
            FuncGVProcessing();
        }

        protected void gvFinish_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvFinish.PageIndex = e.NewPageIndex;
            FuncGVFinish();
        }

        private void FuncGVFinish()
        {
            
            OracleConnection.ClearAllPools();
            SqlDataSource sds = DatabaseManager.CreateSQLDataSource("SELECT LEAVE_ID รหัสการลา, (SELECT LEAVE_TYPE_NAME FROM LEV_TYPE WHERE LEV_TYPE.LEAVE_TYPE_ID = LEV_DATA.LEAVE_TYPE_ID) ประเภทการลา, REQ_DATE วันที่ข้อมูล, FROM_DATE จากวันที่, TO_DATE ถึงวันที่, TOTAL_DAY รวมวัน, (SELECT LEAVE_STATUS_NAME FROM LEV_STATUS WHERE LEV_STATUS.LEAVE_STATUS_ID = LEV_DATA.LEAVE_STATUS_ID) สถานะ, NVL(V_ALLOW,0) ผลการอนุมัติ FROM LEV_DATA WHERE LEAVE_STATUS_ID in(2,5) AND PS_ID = '" + loginPerson.PS_CITIZEN_ID + "' ORDER BY LEAVE_ID DESC");
            gvFinish.DataSource = sds;
            gvFinish.DataBind();

            if (gvFinish.Rows.Count > 0)
            {
                lbFinish.Visible = false;
                TableHeaderCell headerCell = new TableHeaderCell();
                headerCell.Text = "ตกลง";
                gvFinish.HeaderRow.Cells.Add(headerCell);

                gvFinish.HeaderRow.Cells[0].Text = "<img src='Image/Small/ID.png' class='icon_left'/>" + gvFinish.HeaderRow.Cells[0].Text;
                gvFinish.HeaderRow.Cells[1].Text = "<img src='Image/Small/list.png' class='icon_left'/>" + gvFinish.HeaderRow.Cells[1].Text;
                gvFinish.HeaderRow.Cells[2].Text = "<img src='Image/Small/calendar.png' class='icon_left'/>" + gvFinish.HeaderRow.Cells[2].Text;
                gvFinish.HeaderRow.Cells[3].Text = "<img src='Image/Small/calendar.png' class='icon_left'/>" + gvFinish.HeaderRow.Cells[3].Text;
                gvFinish.HeaderRow.Cells[4].Text = "<img src='Image/Small/calendar.png' class='icon_left'/>" + gvFinish.HeaderRow.Cells[4].Text;
                gvFinish.HeaderRow.Cells[6].Text = "<img src='Image/Small/question.png' class='icon_left'/>" + gvFinish.HeaderRow.Cells[6].Text;
                gvFinish.HeaderRow.Cells[7].Text = "<img src='Image/Small/correct.png' class='icon_left'/>" + gvFinish.HeaderRow.Cells[7].Text;

                for (int i = 0; i < gvFinish.Rows.Count; ++i)
                {
                    string ID = gvFinish.Rows[i].Cells[0].Text;
                    TableCell cell = new TableCell();
                    LinkButton btn = new LinkButton();
                    btn.CssClass = "ps-button-img";
                    btn.Text = "ตกลง";
                    btn.Click += (e2, e3) =>
                    {
                        LeaveData leaveData = new LeaveData();
                        leaveData.Load(int.Parse(ID));

                        if (leaveData.LeaveStatusID == 2)
                        {
                            DatabaseManager.ExecuteNonQuery("UPDATE LEV_DATA SET LEAVE_STATUS_ID = 3 WHERE LEAVE_ID = " + ID);
                        }
                        else if (leaveData.LeaveStatusID == 5)
                        {
                            DatabaseManager.ExecuteNonQuery("UPDATE LEV_DATA SET LEAVE_STATUS_ID = 6 WHERE LEAVE_ID = " + ID);
                        }
                        Response.Redirect("LeaveHistory.aspx");
                    };
                    cell.Controls.Add(btn);
                    gvFinish.Rows[i].Cells.Add(cell);

                    if (Util.StringEqual(gvFinish.Rows[i].Cells[7].Text, new string[] { "2" }))
                    {
                        gvFinish.Rows[i].Cells[7].Text = "ไม่อนุญาต";
                        gvFinish.Rows[i].Cells[7].ForeColor = System.Drawing.Color.Red;
                    }
                    if (Util.StringEqual(gvFinish.Rows[i].Cells[7].Text, new string[] { "1" }))
                    {
                        gvFinish.Rows[i].Cells[7].Text = "อนุญาต";
                        gvFinish.Rows[i].Cells[7].ForeColor = System.Drawing.Color.Green;
                    }
                }

                Util.NormalizeGridViewDate(gvFinish, 2);
                Util.NormalizeGridViewDate(gvFinish, 3);
                Util.NormalizeGridViewDate(gvFinish, 4);
            }
            else
            {
                lbFinish.Visible = true;
            }
        }
        private void FuncGVProcessing()
        {
            OracleConnection.ClearAllPools();
            SqlDataSource sds = DatabaseManager.CreateSQLDataSource("SELECT LEAVE_ID รหัสการลา, (SELECT LEAVE_TYPE_NAME FROM LEV_TYPE WHERE LEV_TYPE.LEAVE_TYPE_ID = LEV_DATA.LEAVE_TYPE_ID) ประเภทการลา, REQ_DATE วันที่ข้อมูล, FROM_DATE จากวันที่, TO_DATE ถึงวันที่, TOTAL_DAY รวมวัน, (SELECT LEAVE_STATUS_NAME FROM LEV_STATUS WHERE LEV_STATUS.LEAVE_STATUS_ID = LEV_DATA.LEAVE_STATUS_ID) สถานะ FROM LEV_DATA WHERE LEAVE_STATUS_ID in(1,4) AND PS_ID = '" + loginPerson.PS_CITIZEN_ID + "' ORDER BY LEAVE_ID DESC");
            gvProgressing.DataSource = sds;
            gvProgressing.DataBind();

            if (gvProgressing.Rows.Count > 0)
            {
                lbProgressing.Visible = false;
                TableHeaderCell headerCell = new TableHeaderCell();
                headerCell.Text = "ดูข้อมูล";
                gvProgressing.HeaderRow.Cells.Add(headerCell);

                gvProgressing.HeaderRow.Cells[0].Text = "<img src='Image/Small/ID.png' class='icon_left'/>" + gvProgressing.HeaderRow.Cells[0].Text;
                gvProgressing.HeaderRow.Cells[1].Text = "<img src='Image/Small/list.png' class='icon_left'/>" + gvProgressing.HeaderRow.Cells[1].Text;
                gvProgressing.HeaderRow.Cells[2].Text = "<img src='Image/Small/calendar.png' class='icon_left'/>" + gvProgressing.HeaderRow.Cells[2].Text;
                gvProgressing.HeaderRow.Cells[3].Text = "<img src='Image/Small/calendar.png' class='icon_left'/>" + gvProgressing.HeaderRow.Cells[3].Text;
                gvProgressing.HeaderRow.Cells[4].Text = "<img src='Image/Small/calendar.png' class='icon_left'/>" + gvProgressing.HeaderRow.Cells[4].Text;
                gvProgressing.HeaderRow.Cells[6].Text = "<img src='Image/Small/question.png' class='icon_left'/>" + gvProgressing.HeaderRow.Cells[6].Text;

                for (int i = 0; i < gvProgressing.Rows.Count; ++i)
                {
                    string ID = gvProgressing.Rows[i].Cells[0].Text;
                    TableCell cell = new TableCell();
                    LinkButton btn = new LinkButton();
                    btn.CssClass = "ps-button-img";
                    btn.Text = "<img src='Image/Small/search.png'></img>";
                    btn.Click += (e2, e3) =>
                    {
                        Response.Redirect("ViewLeaveForm.aspx?LeaveID=" + ID);
                    };
                    cell.Controls.Add(btn);
                    gvProgressing.Rows[i].Cells.Add(cell);

                }

                Util.NormalizeGridViewDate(gvProgressing, 2);
                Util.NormalizeGridViewDate(gvProgressing, 3);
                Util.NormalizeGridViewDate(gvProgressing, 4);
            }
            else
            {
                lbProgressing.Visible = true;
            }
        }
        private void FuncGVHistory()
        {
            OracleConnection.ClearAllPools();
            SqlDataSource sds = DatabaseManager.CreateSQLDataSource("SELECT LEAVE_ID รหัสการลา, (SELECT LEAVE_TYPE_NAME FROM LEV_TYPE WHERE LEV_TYPE.LEAVE_TYPE_ID = LEV_DATA.LEAVE_TYPE_ID) ประเภทการลา, REQ_DATE วันที่ข้อมูล, FROM_DATE จากวันที่, TO_DATE ถึงวันที่, TOTAL_DAY รวมวัน, (SELECT LEAVE_STATUS_NAME FROM LEV_STATUS WHERE LEV_STATUS.LEAVE_STATUS_ID = LEV_DATA.LEAVE_STATUS_ID) สถานะ, NVL(V_ALLOW,0) ผลการอนุมัติ FROM LEV_DATA WHERE LEAVE_STATUS_ID in(3,6,7,8) AND PS_ID = '" + loginPerson.PS_CITIZEN_ID + "' ORDER BY LEAVE_ID DESC");
            gvHistory.DataSource = sds;
            gvHistory.DataBind();

            if (gvHistory.Rows.Count > 0)
            {
                lbHistory.Visible = false;
                TableHeaderCell headerCell = new TableHeaderCell();
                headerCell.Text = "ดูข้อมูล";
                gvHistory.HeaderRow.Cells.Add(headerCell);

                gvHistory.HeaderRow.Cells[0].Text = "<img src='Image/Small/ID.png' class='icon_left'/>" + gvHistory.HeaderRow.Cells[0].Text;
                gvHistory.HeaderRow.Cells[1].Text = "<img src='Image/Small/list.png' class='icon_left'/>" + gvHistory.HeaderRow.Cells[1].Text;
                gvHistory.HeaderRow.Cells[2].Text = "<img src='Image/Small/calendar.png' class='icon_left'/>" + gvHistory.HeaderRow.Cells[2].Text;
                gvHistory.HeaderRow.Cells[3].Text = "<img src='Image/Small/calendar.png' class='icon_left'/>" + gvHistory.HeaderRow.Cells[3].Text;
                gvHistory.HeaderRow.Cells[4].Text = "<img src='Image/Small/calendar.png' class='icon_left'/>" + gvHistory.HeaderRow.Cells[4].Text;
                gvHistory.HeaderRow.Cells[6].Text = "<img src='Image/Small/question.png' class='icon_left'/>" + gvHistory.HeaderRow.Cells[6].Text;
                gvHistory.HeaderRow.Cells[7].Text = "<img src='Image/Small/correct.png' class='icon_left'/>" + gvHistory.HeaderRow.Cells[7].Text;

                for (int i = 0; i < gvHistory.Rows.Count; ++i)
                {
                    string ID = gvHistory.Rows[i].Cells[0].Text;
                    TableCell cell = new TableCell();
                    LinkButton btn = new LinkButton();
                    btn.CssClass = "ps-button-img";
                    btn.Text = "<img src='Image/Small/search.png'></img>";
                    btn.Click += (e2, e3) =>
                    {
                        Response.Redirect("ViewLeaveForm.aspx?LeaveID=" + ID);
                    };
                    cell.Controls.Add(btn);
                    gvHistory.Rows[i].Cells.Add(cell);

                    if (Util.StringEqual(gvHistory.Rows[i].Cells[7].Text, new string[] { "0" }))
                    {
                        gvHistory.Rows[i].Cells[7].Text = "-";
                        gvHistory.Rows[i].Cells[7].ForeColor = System.Drawing.Color.Black;
                    }
                    if (Util.StringEqual(gvHistory.Rows[i].Cells[7].Text, new string[] { "2" }))
                    {
                        gvHistory.Rows[i].Cells[7].Text = "ไม่อนุญาต";
                        gvHistory.Rows[i].Cells[7].ForeColor = System.Drawing.Color.Red;
                    }
                    if (Util.StringEqual(gvHistory.Rows[i].Cells[7].Text, new string[] { "1" }))
                    {
                        gvHistory.Rows[i].Cells[7].Text = "อนุญาต";
                        gvHistory.Rows[i].Cells[7].ForeColor = System.Drawing.Color.Green;
                    }
                }

                Util.NormalizeGridViewDate(gvHistory, 2);
                Util.NormalizeGridViewDate(gvHistory, 3);
                Util.NormalizeGridViewDate(gvHistory, 4);
            }
            else
            {
                lbHistory.Visible = true;
            }
        }


    }
}