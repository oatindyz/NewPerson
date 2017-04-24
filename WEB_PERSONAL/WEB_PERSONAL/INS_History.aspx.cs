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
            SqlDataSource sds = DatabaseManager.CreateSQLDataSource("SELECT IP_ID รหัสการขอเครื่องราช, (SELECT  PS_FIRSTNAME || ' ' || PS_LASTNAME FROM PS_PERSON WHERE PS_CITIZEN_ID = CITIZEN_ID) ชื่อผู้ขอเครื่องราช, (SELECT INSIG_GRADE_NAME_L FROM TB_INSIG_GRADE WHERE INSIG_GRADE_ID = INSIG_ID) ระดับชั้นเครื่องราชที่ขอ, REQ_DATE วันที่ข้อมูล, (SELECT IP_STATUS_NAME FROM TB_INSIG_PERSON_STATUS WHERE TB_INSIG_PERSON_STATUS.IP_STATUS_ID = TB_INSIG_PERSON.IP_STATUS_ID) สถานะ FROM TB_INSIG_PERSON WHERE IP_STATUS_ID IN(2,4) AND CITIZEN_ID = '" + loginPerson.PS_CITIZEN_ID + "'");
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
                        DatabaseManager.ExecuteNonQuery("UPDATE TB_INSIG_PERSON SET IP_STATUS_ID = IP_STATUS_ID+1 WHERE IP_ID = " + ID);
                        Response.Redirect("INS_History.aspx");

                    };
                    cell.Controls.Add(btn);
                    gvFinish.Rows[i].Cells.Add(cell);

                   
                }

            }
            else
            {
                lbFinish.Visible = true;
            }
        }
        private void FuncGVProcessing()
        {
            OracleConnection.ClearAllPools();
            SqlDataSource sds = DatabaseManager.CreateSQLDataSource("SELECT IP_ID รหัสการขอเครื่องราช, (SELECT  PS_FIRSTNAME || ' ' || PS_LASTNAME FROM PS_PERSON WHERE PS_CITIZEN_ID = CITIZEN_ID) ชื่อผู้ขอเครื่องราช, (SELECT INSIG_GRADE_NAME_L FROM TB_INSIG_GRADE WHERE INSIG_GRADE_ID = INSIG_ID) ระดับชั้นเครื่องราชที่ขอ, REQ_DATE วันที่ข้อมูล, (SELECT IP_STATUS_NAME FROM TB_INSIG_PERSON_STATUS WHERE TB_INSIG_PERSON_STATUS.IP_STATUS_ID = TB_INSIG_PERSON.IP_STATUS_ID) สถานะ FROM TB_INSIG_PERSON WHERE IP_STATUS_ID = 1 AND CITIZEN_ID = '" + loginPerson.PS_CITIZEN_ID + "'");
            gvProgressing.DataSource = sds;
            gvProgressing.DataBind();

            if (gvProgressing.Rows.Count > 0)
            {
                lbProgressing.Visible = false;
                /*TableHeaderCell headerCell = new TableHeaderCell();
                headerCell.Text = "ดูข้อมูล";
                gvProgressing.HeaderRow.Cells.Add(headerCell);

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
                Util.NormalizeGridViewDate(gvProgressing, 4);*/
            }
            else
            {
                lbProgressing.Visible = true;
            }
        }
        private void FuncGVHistory()
        {
            OracleConnection.ClearAllPools();
            SqlDataSource sds = DatabaseManager.CreateSQLDataSource("SELECT IP_ID รหัสการขอเครื่องราช, (SELECT  PS_FIRSTNAME || ' ' || PS_LASTNAME FROM PS_PERSON WHERE PS_CITIZEN_ID = CITIZEN_ID) ชื่อผู้ขอเครื่องราช, (SELECT INSIG_GRADE_NAME_L FROM TB_INSIG_GRADE WHERE INSIG_GRADE_ID = INSIG_ID) ระดับชั้นเครื่องราชที่ขอ, REQ_DATE วันที่ข้อมูล, (SELECT IP_STATUS_NAME FROM TB_INSIG_PERSON_STATUS WHERE TB_INSIG_PERSON_STATUS.IP_STATUS_ID = TB_INSIG_PERSON.IP_STATUS_ID) สถานะ FROM TB_INSIG_PERSON WHERE IP_STATUS_ID IN(3,5) AND CITIZEN_ID = '" + loginPerson.PS_CITIZEN_ID + "'");
            gvHistory.DataSource = sds;
            gvHistory.DataBind();

            if (gvHistory.Rows.Count > 0)
            {
                lbHistory.Visible = false;
                /*TableHeaderCell headerCell = new TableHeaderCell();
                headerCell.Text = "ดูข้อมูล";
                gvHistory.HeaderRow.Cells.Add(headerCell);

                for (int i = 0; i < gvHistory.Rows.Count; ++i)
                {
                    string ID = gvHistory.Rows[i].Cells[0].Text;
                    TableCell cell = new TableCell();
                    LinkButton btn = new LinkButton();
                    btn.CssClass = "ps-button-img";
                    btn.Text = "<img src='Image/Small/search.png'></img>";
                    btn.Click += (e2, e3) =>
                    {
                        //Response.Redirect("ViewLeaveForm.aspx?LeaveID=" + ID);
                    };
                    cell.Controls.Add(btn);
                    gvHistory.Rows[i].Cells.Add(cell);

                   
                }*/

            }
            else
            {
                lbHistory.Visible = true;
            }
        }


    }
}