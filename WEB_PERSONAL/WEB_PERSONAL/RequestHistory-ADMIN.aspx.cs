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
    public partial class RequestHistory_ADMIN : System.Web.UI.Page
    {
        private Person loginPerson;
        protected void Page_Load(object sender, EventArgs e)
        {
            PersonnelSystem ps = PersonnelSystem.GetPersonnelSystem(this);
            Person loginPerson = ps.LoginPerson;
            if (loginPerson.PERSON_ROLE_ID != "2")
            {
                Server.Transfer("NoPermission.aspx");
            }

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
        
        private void FuncGVHistory()
        {
            OracleConnection.ClearAllPools();
            SqlDataSource sds = DatabaseManager.CreateSQLDataSource("SELECT R_ID รหัสคำร้องแก้ไขข้อมูล, (SELECT PS_FIRSTNAME || ' ' || PS_LASTNAME FROM PS_PERSON WHERE PS_PERSON.PS_CITIZEN_ID = TB_REQUEST.CITIZEN_ID) ชื่อ, (SELECT (SELECT CAMPUS_NAME FROM TB_CAMPUS WHERE PS_PERSON.PS_CAMPUS_ID = TB_CAMPUS.CAMPUS_ID) FROM PS_PERSON WHERE PS_PERSON.PS_CITIZEN_ID = TB_REQUEST.CITIZEN_ID) วิทยาเขต, (SELECT (SELECT STAFFTYPE_NAME FROM TB_STAFFTYPE WHERE PS_PERSON.PS_STAFFTYPE_ID = TB_STAFFTYPE.STAFFTYPE_ID) FROM PS_PERSON WHERE PS_PERSON.PS_CITIZEN_ID = TB_REQUEST.CITIZEN_ID) ประเภทบุคลากร, DATE_START วันที่ข้อมูล, (SELECT R_STATUS_NAME FROM TB_REQUEST_STATUS WHERE TB_REQUEST_STATUS.R_STATUS_ID = TB_REQUEST.R_STATUS_ID) สถานะ, NVL(R_ALLOW,0) ผลการอนุมัติ, DATE_END วันที่อนุมัติ FROM TB_REQUEST WHERE R_STATUS_ID IN(2,3,4,5) ORDER BY R_ID DESC");
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

                    if (Util.StringEqual(gvHistory.Rows[i].Cells[6].Text, new string[] { "0" }))
                    {
                        gvHistory.Rows[i].Cells[6].Text = "-";
                        gvHistory.Rows[i].Cells[6].ForeColor = System.Drawing.Color.Black;
                    }
                    if (Util.StringEqual(gvHistory.Rows[i].Cells[6].Text, new string[] { "2" }))
                    {
                        gvHistory.Rows[i].Cells[6].Text = "ไม่อนุมัติ";
                        gvHistory.Rows[i].Cells[6].ForeColor = System.Drawing.Color.Red;
                    }
                    if (Util.StringEqual(gvHistory.Rows[i].Cells[6].Text, new string[] { "1" }))
                    {
                        gvHistory.Rows[i].Cells[6].Text = "อนุมัติ";
                        gvHistory.Rows[i].Cells[6].ForeColor = System.Drawing.Color.Green;
                    }
                }

                Util.NormalizeGridViewDate(gvHistory, 4);
                Util.NormalizeGridViewDate(gvHistory, 7);
            }
            else
            {
                lbHistory.Visible = true;
            }


        }
    }
}