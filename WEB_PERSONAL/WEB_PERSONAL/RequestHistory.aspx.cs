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
    public partial class RequestHistory : System.Web.UI.Page
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
            SqlDataSource sds = DatabaseManager.CreateSQLDataSource("SELECT R_ID รหัสคำร้องแก้ไขข้อมูล, DATE_START วันที่ข้อมูล, (SELECT R_STATUS_NAME FROM TB_REQUEST_STATUS WHERE TB_REQUEST_STATUS.R_STATUS_ID = TB_REQUEST.R_STATUS_ID) สถานะ, NVL(R_ALLOW,0) ผลการอนุมัติ, DATE_END วันที่อนุมัติ FROM TB_REQUEST WHERE R_STATUS_ID IN(2,4) AND CITIZEN_ID = '" + loginPerson.PS_CITIZEN_ID + "' ORDER BY R_ID DESC");
            gvFinish.DataSource = sds;
            gvFinish.DataBind();

            if (gvFinish.Rows.Count > 0)
            {
                lbFinish.Visible = false;
                TableHeaderCell headerCell = new TableHeaderCell();
                headerCell.Text = "ตกลง";
                gvFinish.HeaderRow.Cells.Add(headerCell);

                for (int i = 0; i < gvFinish.Rows.Count; ++i)
                {
                    string ID = gvFinish.Rows[i].Cells[0].Text;
                    TableCell cell = new TableCell();
                    LinkButton btn = new LinkButton();
                    btn.CssClass = "ps-button-img";
                    btn.Text = "ตกลง";
                    btn.Click += (e2, e3) =>
                    {
                        DatabaseManager.ExecuteNonQuery("UPDATE TB_REQUEST SET R_STATUS_ID = R_STATUS_ID+1 WHERE R_ID = " + ID);
                        Response.Redirect("RequestHistory.aspx");

                    };
                    cell.Controls.Add(btn);
                    gvFinish.Rows[i].Cells.Add(cell);

                    if (Util.StringEqual(gvFinish.Rows[i].Cells[3].Text, new string[] { "0" }))
                    {
                        gvFinish.Rows[i].Cells[3].Text = "-";
                        gvFinish.Rows[i].Cells[3].ForeColor = System.Drawing.Color.Black;
                    }
                    if (Util.StringEqual(gvFinish.Rows[i].Cells[3].Text, new string[] { "2" }))
                    {
                        gvFinish.Rows[i].Cells[3].Text = "ไม่อนุมัติ";
                        gvFinish.Rows[i].Cells[3].ForeColor = System.Drawing.Color.Red;
                    }
                    if (Util.StringEqual(gvFinish.Rows[i].Cells[3].Text, new string[] { "1" }))
                    {
                        gvFinish.Rows[i].Cells[3].Text = "อนุมัติ";
                        gvFinish.Rows[i].Cells[3].ForeColor = System.Drawing.Color.Green;
                    }
                }

                Util.NormalizeGridViewDate(gvFinish, 1);
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
            SqlDataSource sds = DatabaseManager.CreateSQLDataSource("SELECT R_ID รหัสคำร้องแก้ไขข้อมูล, DATE_START วันที่ข้อมูล, (SELECT R_STATUS_NAME FROM TB_REQUEST_STATUS WHERE TB_REQUEST_STATUS.R_STATUS_ID = TB_REQUEST.R_STATUS_ID) สถานะ FROM TB_REQUEST WHERE R_STATUS_ID = 1 AND CITIZEN_ID = '" + loginPerson.PS_CITIZEN_ID + "' ORDER BY R_ID DESC");
            gvProgressing.DataSource = sds;
            gvProgressing.DataBind();

            if (gvProgressing.Rows.Count > 0)
            {
                lbProgressing.Visible = false;
                Util.NormalizeGridViewDate(gvProgressing, 1);
            }
            else
            {
                lbProgressing.Visible = true;
            }
        }
        private void FuncGVHistory()
        {
            OracleConnection.ClearAllPools();
            SqlDataSource sds = DatabaseManager.CreateSQLDataSource("SELECT R_ID รหัสคำร้องแก้ไขข้อมูล, DATE_START วันที่ข้อมูล, (SELECT R_STATUS_NAME FROM TB_REQUEST_STATUS WHERE TB_REQUEST_STATUS.R_STATUS_ID = TB_REQUEST.R_STATUS_ID) สถานะ, NVL(R_ALLOW,0) ผลการอนุมัติ, DATE_END วันที่อนุมัติ FROM TB_REQUEST WHERE R_STATUS_ID IN(3,5) AND CITIZEN_ID = '" + loginPerson.PS_CITIZEN_ID + "' ORDER BY R_ID DESC");
            gvHistory.DataSource = sds;
            gvHistory.DataBind();

            if (gvHistory.Rows.Count > 0)
            {
                lbHistory.Visible = false;
                TableHeaderCell headerCell = new TableHeaderCell();
                headerCell.Text = "ดูข้อมูล";
                gvHistory.HeaderRow.Cells.Add(headerCell);

                for (int i = 0; i < gvHistory.Rows.Count; ++i)
                {
                    string ID = gvHistory.Rows[i].Cells[0].Text;
                    TableCell cell = new TableCell();
                    LinkButton btn = new LinkButton();
                    btn.CssClass = "ps-button-img";
                    btn.Text = "<img src='Image/Small/search.png'></img>";
                    btn.Click += (e2, e3) => {
                        Response.Redirect("ViewRequestForm.aspx?id=" + MyCrypto.GetEncryptedQueryString(ID).ToString());
                    };
                    cell.Controls.Add(btn);
                    gvHistory.Rows[i].Cells.Add(cell);

                    if (Util.StringEqual(gvHistory.Rows[i].Cells[3].Text, new string[] { "0" }))
                    {
                        gvHistory.Rows[i].Cells[3].Text = "-";
                        gvHistory.Rows[i].Cells[3].ForeColor = System.Drawing.Color.Black;
                    }
                    if (Util.StringEqual(gvHistory.Rows[i].Cells[3].Text, new string[] { "2" }))
                    {
                        gvHistory.Rows[i].Cells[3].Text = "ไม่อนุมัติ";
                        gvHistory.Rows[i].Cells[3].ForeColor = System.Drawing.Color.Red;
                    }
                    if (Util.StringEqual(gvHistory.Rows[i].Cells[3].Text, new string[] { "1" }))
                    {
                        gvHistory.Rows[i].Cells[3].Text = "อนุมัติ";
                        gvHistory.Rows[i].Cells[3].ForeColor = System.Drawing.Color.Green;
                    }
                }

                Util.NormalizeGridViewDate(gvHistory, 1);
                Util.NormalizeGridViewDate(gvHistory, 4);
            }
            else
            {
                lbHistory.Visible = true;
            }


        }
    }
}