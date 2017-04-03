using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WEB_PERSONAL.Class;

namespace WEB_PERSONAL
{
    public partial class INS_Result : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlDataSource sds = DatabaseManager.CreateSQLDataSource("SELECT INS_REQ_ID รหัส,PS_CITIZEN_ID รหัสบัตรประชาชน,INS_GRADEINSIGNIA_ID ลำดับเครื่องราช,REQ_DATE วันที่ขอ,STATE สถานะการขอ FROM INSG_REQUEST ");
            GridView1.DataSource = sds;
            GridView1.DataBind();

            Util.NormalizeGridViewDate(GridView1, 3);

            TableHeaderCell newHeader = new TableHeaderCell();
            newHeader.Text = "เลือก";
            GridView1.HeaderRow.Cells.Add(newHeader);

            for (int i = 0; i < GridView1.Rows.Count; ++i)
            {

                string id = GridView1.Rows[i].Cells[0].Text;
                //Form1Package f1 = DatabaseManager.GetForm1Package(id);

                LinkButton lbu = new LinkButton();
                lbu.Text = "เลือก";
                lbu.CssClass = "ps-button";
                lbu.Click += (e2, e3) =>
                {
                    MultiView1.ActiveViewIndex = 1;
                };
                TableCell cell = new TableCell();
                cell.Controls.Add(lbu);
                GridView1.Rows[i].Cells.Add(cell);
            }
        }

        protected void lbuV1Back_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
        }
    }
}