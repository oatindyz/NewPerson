using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using WEB_PERSONAL.Class;
using System.Data.OracleClient;

namespace WEB_PERSONAL {
    public partial class INSG_Qualified : System.Web.UI.Page {

        private Panel pCondition;
        private Panel pBar;
        private Image img1;
        private Image img2;
        private Label lb1;
        private Label lb2;
        private bool OK = true;
        private bool OK2 = true;
        private string cccStaffType = "";
        private string cccCampus = "";
        private string cccFaculty = "";
        private string cccDivision = "";
        private string cccWorkDivision = "";

        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack)
            {
                DatabaseManager.BindDropDown(ddlStaffType, "SELECT * FROM TB_STAFFTYPE", "STAFFTYPE_NAME", "STAFFTYPE_ID", "--กรุณาประเภทบุคลากร--");
                SQLCampus();
            }
            search();        
        }

        protected void SQLCampus()
        {
            try
            {
                using (OracleConnection sqlConn = new OracleConnection(DatabaseManager.CONNECTION_STRING))
                {
                    using (OracleCommand sqlCmd = new OracleCommand())
                    {
                        sqlCmd.CommandText = "select * from TB_CAMPUS";
                        sqlCmd.Connection = sqlConn;
                        sqlConn.Open();
                        OracleDataAdapter da = new OracleDataAdapter(sqlCmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        ddlCampus.DataSource = dt;
                        ddlCampus.DataValueField = "CAMPUS_ID";
                        ddlCampus.DataTextField = "CAMPUS_NAME";
                        ddlCampus.DataBind();
                        sqlConn.Close();

                        ddlCampus.Items.Insert(0, new ListItem("--กรุณาเลือกวิทยาเขต--", "0"));
                        ddlFaculty.Items.Insert(0, new ListItem("--กรุณาเลือกสำนัก / สถาบัน / คณะ--", "0"));
                        ddlDivision.Items.Insert(0, new ListItem("--กรุณาเลือกกอง / สำนักงานเลขา / ภาควิชา--", "0"));
                        ddlWorkDivision.Items.Insert(0, new ListItem("--กรุณาเลือกงาน / ฝ่าย--", "0"));
                    }
                }
            }
            catch { }
        }

        protected void ddlCampus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                using (OracleConnection sqlConn = new OracleConnection(DatabaseManager.CONNECTION_STRING))
                {
                    using (OracleCommand sqlCmd = new OracleCommand())
                    {
                        sqlCmd.CommandText = "select * from TB_FACULTY where CAMPUS_ID = " + ddlCampus.SelectedValue;
                        sqlCmd.Connection = sqlConn;
                        sqlConn.Open();
                        OracleDataAdapter da = new OracleDataAdapter(sqlCmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        ddlFaculty.DataSource = dt;
                        ddlFaculty.DataValueField = "FACULTY_ID";
                        ddlFaculty.DataTextField = "FACULTY_NAME";
                        ddlFaculty.DataBind();
                        sqlConn.Close();

                        ddlFaculty.Items.Insert(0, new ListItem("--กรุณาเลือกสำนัก / สถาบัน / คณะ--", "0"));
                        ddlDivision.Items.Clear();
                        ddlDivision.Items.Insert(0, new ListItem("--กรุณาเลือกกอง / สำนักงานเลขา / ภาควิชา--", "0"));
                        ddlWorkDivision.Items.Clear();
                        ddlWorkDivision.Items.Insert(0, new ListItem("--กรุณาเลือกงาน / ฝ่าย--", "0"));
                    }
                }
            }
            catch { }
        }

        protected void ddlFaculty_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                using (OracleConnection sqlConn = new OracleConnection(DatabaseManager.CONNECTION_STRING))
                {
                    using (OracleCommand sqlCmd = new OracleCommand())
                    {
                        sqlCmd.CommandText = "select * from TB_DIVISION where FACULTY_ID = " + ddlFaculty.SelectedValue;
                        sqlCmd.Connection = sqlConn;
                        sqlConn.Open();
                        OracleDataAdapter da = new OracleDataAdapter(sqlCmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        ddlDivision.DataSource = dt;
                        ddlDivision.DataValueField = "DIVISION_ID";
                        ddlDivision.DataTextField = "DIVISION_NAME";
                        ddlDivision.DataBind();
                        sqlConn.Close();

                        ddlDivision.Items.Insert(0, new ListItem("--กรุณาเลือกกอง / สำนักงานเลขา / ภาควิชา--", "0"));
                        ddlWorkDivision.Items.Clear();
                        ddlWorkDivision.Items.Insert(0, new ListItem("--กรุณาเลือกงาน / ฝ่าย--", "0"));
                    }
                }
            }
            catch { }
        }

        protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                using (OracleConnection sqlConn = new OracleConnection(DatabaseManager.CONNECTION_STRING))
                {
                    using (OracleCommand sqlCmd = new OracleCommand())
                    {
                        sqlCmd.CommandText = "select * from TB_WORK_DIVISION where DIVISION_ID = " + ddlDivision.SelectedValue;
                        sqlCmd.Connection = sqlConn;
                        sqlConn.Open();
                        OracleDataAdapter da = new OracleDataAdapter(sqlCmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        ddlWorkDivision.DataSource = dt;
                        ddlWorkDivision.DataValueField = "WORK_ID";
                        ddlWorkDivision.DataTextField = "WORK_NAME";
                        ddlWorkDivision.DataBind();
                        sqlConn.Close();

                        ddlWorkDivision.Items.Insert(0, new ListItem("--กรุณาเลือกงาน / ฝ่าย--"));
                    }
                }
            }
            catch { }

            using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
            {
                con.Open();
                using (OracleCommand com = new OracleCommand("SELECT COUNT(*) FROM TB_WORK_DIVISION WHERE DIVISION_ID = " + ddlDivision.SelectedValue, con))
                {
                    using (OracleDataReader reader = com.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader.GetInt32(0) == 0)
                            {
                                ddlWorkDivision.Visible = false;
                                trWorkDivision.Visible = false;
                            }
                            else
                            {
                                ddlWorkDivision.Visible = true;
                                trWorkDivision.Visible = true;
                            }
                        }
                    }
                }
            }
        }

        private void search() {

            if(ddlStaffType.SelectedIndex > 0 ) {
                cccStaffType = " AND PS_STAFFTYPE_ID = " + ddlStaffType.SelectedValue;
            }
            if (ddlCampus.SelectedIndex > 0)
            {
                cccCampus = " AND PS_CAMPUS_ID = " + ddlCampus.SelectedValue;
            }
            if (ddlFaculty.SelectedIndex > 0)
            {
                cccFaculty = " AND PS_FACULTY_ID = " + ddlFaculty.SelectedValue;
            }
            if (ddlDivision.SelectedIndex > 0)
            {
                cccDivision = " AND PS_DIVISION_ID = " + ddlDivision.SelectedValue;
            }
            if (ddlWorkDivision.SelectedIndex > 0)
            {
                cccWorkDivision = " AND PS_WORK_DIVISION_ID = " + ddlWorkDivision.SelectedValue;
            }

            lbuSend.Visible = false;

            if (hf1.Value != "") {
                //Random r = new Random();
                lbuSend.Visible = true;
                {
                    Table1.Rows.Clear();
                    TableRow row = new TableRow();
                    {
                        TableHeaderCell cell = new TableHeaderCell();
                        CheckBox cb = new CheckBox();
                        cb.ID = "CBBX";
                        //cb.AutoPostBack = true;
                        cb.Attributes["onclick"] = "toggle('ContentPlaceHolder1_CBBX', 'CBB')";
                       /* cb.CheckedChanged += (e2, e3) => {
                            for (int i = 1; i < Table1.Rows.Count; i++) {
                                if(Table1.Rows[i].Cells[0].Controls[0] != null) {
                                    CheckBox cb2 = (CheckBox)Table1.Rows[i].Cells[0].Controls[0];
                                    cb2.Checked = cb.Checked;
                                }
                                    
                            }
                        };*/
                        cell.Controls.Add(cb);
                        Label lb = new Label();
                        lb.Text = "เลือก";
                        cell.Controls.Add(lb);
                        row.Cells.Add(cell);
                    }
                    {
                        TableHeaderCell cell = new TableHeaderCell();
                        cell.Text = "ชื่อ - สกุล";
                        row.Cells.Add(cell);
                    }
                    {
                        TableHeaderCell cell = new TableHeaderCell();
                        cell.Text = "ชั้นเครื่องราชฯ ปัจจุบัน";
                        row.Cells.Add(cell);
                    }
                    {
                        TableHeaderCell cell = new TableHeaderCell();
                        cell.Text = "เงื่อนไข";
                        row.Cells.Add(cell);
                    }
                    {
                        TableHeaderCell cell = new TableHeaderCell();
                        cell.Text = "ชั้นเครื่องราชฯ ชั้นถัดไป";
                        row.Cells.Add(cell);
                    }
                    {
                        TableHeaderCell cell = new TableHeaderCell();
                        cell.Text = "วันที่บรรจุ";
                        row.Cells.Add(cell);
                    }
                    {
                        TableHeaderCell cell = new TableHeaderCell();
                        cell.Text = "เงินเดือน";
                        row.Cells.Add(cell);
                    }
                    {
                        TableHeaderCell cell = new TableHeaderCell();
                        cell.Text = "ตำแหน่ง";
                        row.Cells.Add(cell);
                    }
                    {
                        TableHeaderCell cell = new TableHeaderCell();
                        cell.Text = "ประเภทบุคลากร";
                        row.Cells.Add(cell);
                    }
                    {
                        TableHeaderCell cell = new TableHeaderCell();
                        cell.Text = "วิทยาเขต";
                        row.Cells.Add(cell);
                    }
                    {
                        TableHeaderCell cell = new TableHeaderCell();
                        cell.Text = "สำนัก / สถาบัน / คณะ";
                        row.Cells.Add(cell);
                    }
                    Table1.Rows.Add(row);
                }
                
                using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING)) {
                    con.Open();
                    using (OracleCommand com = new OracleCommand(
                        "SELECT PS_PERSON.PS_CITIZEN_ID รหัสประชาชน" //0
                        + ", PS_PERSON.PS_FIRSTNAME || ' ' || PS_PERSON.PS_LASTNAME ชื่อ" //1
                        + ", PS_STAFFTYPE_ID" //2
                        + ", (SELECT TRUNC((SYSDATE - PS_INWORK_DATE)/365,0) from PS_PERSON A WHERE A.PS_CITIZEN_ID = PS_PERSON.PS_CITIZEN_ID) ปีทำงาน" //3
                        + ", PS_SALARY" //4
                        + ", PS_INWORK_DATE" //5
                        + ", PS_SALARY" //6
                        + ", (SELECT POSITION_WORK_NAME FROM TB_POSITION_WORK WHERE PS_PERSON.PS_WORK_POS_ID = TB_POSITION_WORK.POSITION_WORK_ID) ตำแหน่ง" //7
                        + ", PS_ACAD_POS_ID" //8
                        + ", PS_ADMIN_POS_ID" //9
                        + ", (SELECT STAFFTYPE_NAME FROM TB_STAFFTYPE WHERE PS_PERSON.PS_STAFFTYPE_ID = TB_STAFFTYPE.STAFFTYPE_ID) ชื่อประเภทบุคลากร" //10
                        + ", (SELECT CAMPUS_NAME FROM TB_CAMPUS WHERE TB_CAMPUS.CAMPUS_ID = PS_PERSON.PS_CAMPUS_ID) วิทยาเขต" //11
                        + ", (SELECT FACULTY_NAME FROM TB_FACULTY WHERE TB_FACULTY.FACULTY_ID = PS_PERSON.PS_FACULTY_ID) คณะ" //12
                        + ", PS_POSI_ADMIN" //13
                        + ", PS_POSI_DIRECT" //14
                        + ", PS_POSI_ACAD" //15
                        + ", PS_POSI_GENERAL" //16
                        + ", PS_POSI_EMP_GROUP" //17
                        + " FROM PS_PERSON WHERE PS_STAFFTYPE_ID in(1,2,3,6) " + cccStaffType + cccCampus + cccFaculty + cccDivision + cccWorkDivision, con)) {
                        using (OracleDataReader reader = com.ExecuteReader()) {
                            while (reader.Read()) {

                                OK = true;
                                OK2 = true;
                                string psID = reader.GetString(0);

                                TableRow row = new TableRow();
                                //row.CssClass = "ps-ins-item";

                                img1 = new Image();
                                img2 = new Image();
                                lb1 = new Label();
                                lb2 = new Label();
                                pBar = new Panel();
                                pCondition = new Panel();

                                lb1.Style.Add("font-weight", "bold");
                                lb1.Style.Add("font-size", "16px");
                                lb1.Text = "[ERROR]";

                                lb2.Style.Add("font-weight", "bold");
                                lb2.Style.Add("font-size", "16px");
                                lb2.Text = "[ERROR]";

                                {
                                    CheckBox cb = new CheckBox();
                                    cb.Attributes["name"] = "CBB";
                                    TableCell cell = new TableCell();
                                    cell.Controls.Add(cb);
                                    row.Cells.Add(cell);
                                }

                                LinkButton lbName = new LinkButton();
                                {
                                    
                                    lbName.Text = reader.GetString(1);
                                    lbName.Click += (e2, e3) => {
                                        Response.Redirect("INSG_Qualified_Detail.aspx?psID=" + psID);
                                    };
                                    lbName.Attributes.Add("tuu", psID);
                                    
                                    TableCell cell = new TableCell();
                                    cell.Controls.Add(lbName);
                                    row.Cells.Add(cell);
                                }

                                {
                                    Panel p1 = new Panel();
                                    p1.Style.Add("text-align", "center");

                                    p1.Controls.Add(img1);

                                    Panel p2 = new Panel();
                                    p2.Style.Add("text-align", "center");

                                    p2.Controls.Add(lb1);

                                    TableCell cell = new TableCell();
                                    cell.Controls.Add(p1);
                                    cell.Controls.Add(p2);
                                    row.Cells.Add(cell);
                                }

                                {

                                    Panel barOFF = new Panel();
                                    barOFF.Style.Add("width", "200px");
                                    barOFF.Style.Add("height", "10px");
                                    barOFF.Style.Add("display", "inline-block");
                                    barOFF.Style.Add("background", "linear-gradient(to bottom, #808080, #414141)");
                                    barOFF.Style.Add("border-radius", "10px");

                                    TableCell cell = new TableCell();
                                    cell.Controls.Add(pBar);
                                    cell.Controls.Add(pCondition);
                                    row.Cells.Add(cell);
                                }

                                {
                                    Panel p1 = new Panel();
                                    p1.CssClass = "ps-ins-container";

                                    Panel p1c = new Panel();
                                    p1c.CssClass = "ps-ins-tag";
                                    p1.Controls.Add(p1c);
                                    p1.Controls.Add(img2);

                                    Panel p2 = new Panel();
                                    p2.Style.Add("text-align", "center");


                                    p2.Controls.Add(lb2);

                                    TableCell cell = new TableCell();
                                    cell.Controls.Add(p1);
                                    cell.Controls.Add(p2);
                                    row.Cells.Add(cell);
                                }

                                {
                                    Label lblDateInwork = new Label();
                                    lblDateInwork.Text = reader.IsDBNull(5) ? "" : reader.GetDateTime(5).ToString("dd MMM yyyy");
                                    TableCell cell = new TableCell();
                                    cell.Controls.Add(lblDateInwork);
                                    row.Cells.Add(cell);
                                }

                                {
                                    Label lblSalary = new Label();
                                    lblSalary.Text = reader.IsDBNull(6) ? "-1" : reader.GetInt32(6).ToString("#,###");
                                    TableCell cell = new TableCell();
                                    cell.Controls.Add(lblSalary);
                                    row.Cells.Add(cell);
                                }

                                {
                                    //if() ddlTab4PositionWorkRow1.SelectedValue = reader.IsDBNull(i) ? "0" : reader.GetInt32(i).ToString();
                                    Label lblPosition = new Label();
                                    lblPosition.Text = reader.IsDBNull(7) ? "" : reader.GetString(7);
                                    //lblPosition.Text = reader.GetString(7);
                                    TableCell cell = new TableCell();
                                    cell.Controls.Add(lblPosition);
                                    row.Cells.Add(cell);
                                }

                                {
                                    Label lblStaffTypeName = new Label();
                                    lblStaffTypeName.Text = reader.GetString(10);
                                    TableCell cell = new TableCell();
                                    cell.Controls.Add(lblStaffTypeName);
                                    row.Cells.Add(cell);
                                }

                                {
                                    Label lblCampus = new Label();
                                    lblCampus.Text = reader.GetString(11);
                                    TableCell cell = new TableCell();
                                    cell.Controls.Add(lblCampus);
                                    row.Cells.Add(cell);
                                }

                                {
                                    Label lblFaculty = new Label();
                                    lblFaculty.Text = reader.GetString(12);
                                    TableCell cell = new TableCell();
                                    cell.Controls.Add(lblFaculty);
                                    row.Cells.Add(cell);
                                }

                                //Table1.Rows.Add(row);

                                int รหัสประเภทบุคลากร = reader.GetInt32(2);
                                //int รหัสระดับตำแหน่ง = reader.GetInt32(3);
                                int รหัสตำแหน่งทางวิชาการ = reader.IsDBNull(8) ? 0 : reader.GetInt32(8);
                                int รหัสตำแหน่งทางบริหาร = reader.IsDBNull(9) ? 0 : reader.GetInt32(9);

                                

                                int รหัสตำแหน่งประเภททางบริหาร = reader.IsDBNull(13) ? 0 : reader.GetInt32(13);
                                int รหัสตำแหน่งประเภททางอำนวยการ = reader.IsDBNull(14) ? 0 : reader.GetInt32(14);
                                int รหัสตำแหน่งประเภททางวิชาการ = reader.IsDBNull(15) ? 0 : reader.GetInt32(15);
                                int รหัสตำแหน่งประเภททางทั่วไป = reader.IsDBNull(16) ? 0 : reader.GetInt32(16);

                                int รหัสกลุ่มงานพนักงานราชการ = reader.IsDBNull(17) ? 0 : reader.GetInt32(17);

                                int รหัสเครืองราชปัจจุบัน = -1;
                                using (OracleCommand com2 = new OracleCommand("SELECT IR_INSIG_ID FROM TB_INSIG_REQUEST WHERE IR_STATUS IN(3,4) AND IR_CITIZEN_ID = '" + psID + "' AND IR_GET_STATUS = 1 ORDER BY IR_ID DESC", con)) {
                                    using (OracleDataReader reader2 = com2.ExecuteReader()) {
                                        if (reader2.Read()) {
                                            รหัสเครืองราชปัจจุบัน = reader2.GetInt32(0);
                                        }

                                    }
                                }

                                int ปีที่ทำงาน = reader.IsDBNull(3) ? -1 : reader.GetInt32(3);
                                int เงินเดือนปัจจุบัน = reader.IsDBNull(4) ? -1 : reader.GetInt32(4);

                                int เงินเดือนขั้นต่ำของระดับชำนาญงาน = -1;
                                using (OracleCommand com2 = new OracleCommand("SELECT P_SAL_MIN FROM TB_POSITION_SAL_MINMAX WHERE P_POS_ID = 14102", con)) {
                                    using (OracleDataReader reader2 = com2.ExecuteReader()) {
                                        while (reader2.Read()) {
                                            เงินเดือนขั้นต่ำของระดับชำนาญงาน = reader2.GetInt32(0);
                                        }

                                    }
                                }

                                int ปีที่ดำรงตำแหน่งระดับปฏิบัติงาน = -1;
                                using (OracleCommand com2 = new OracleCommand("SELECT TRUNC((CURRENT_DATE - PDH_DATE_START)/365,0) FROM TB_PDH_GOVER WHERE PDH_POSITION_GET = 10 AND PDH_CITIZEN_ID = '" + psID + "'", con)) {
                                    using (OracleDataReader reader2 = com2.ExecuteReader()) {
                                        while (reader2.Read()) {
                                            ปีที่ดำรงตำแหน่งระดับปฏิบัติงาน = reader2.GetInt32(0);
                                        }

                                    }
                                }

                                int ปีที่ดำรงตำแหน่งระดับชำนาญงาน = -1;
                                using (OracleCommand com2 = new OracleCommand("SELECT TRUNC((CURRENT_DATE - PDH_DATE_START)/365,0) FROM TB_PDH_GOVER WHERE PDH_POSITION_GET = 11 AND PDH_CITIZEN_ID = '" + psID + "'", con)) {
                                    using (OracleDataReader reader2 = com2.ExecuteReader()) {
                                        while (reader2.Read()) {
                                            ปีที่ดำรงตำแหน่งระดับชำนาญงาน = reader2.GetInt32(0);
                                        }

                                    }
                                }

                                int ปีที่ดำรงตำแหน่งระดับอาวุโส = -1;
                                using (OracleCommand com2 = new OracleCommand("SELECT TRUNC((CURRENT_DATE - PDH_DATE_START)/365,0) FROM TB_PDH_GOVER WHERE PDH_POSITION_GET = 12 AND PDH_CITIZEN_ID = '" + psID + "'", con)) {
                                    using (OracleDataReader reader2 = com2.ExecuteReader()) {
                                        while (reader2.Read()) {
                                            ปีที่ดำรงตำแหน่งระดับอาวุโส = reader2.GetInt32(0);
                                        }

                                    }
                                }

                                int เงินเดือนขั้นต่ำของระดับชำนาญการพิเศษ = -1;
                                using (OracleCommand com2 = new OracleCommand("SELECT P_SAL_MIN FROM TB_POSITION_SAL_MINMAX WHERE P_POS_ID = 13103", con)) {
                                    using (OracleDataReader reader2 = com2.ExecuteReader()) {
                                        while (reader2.Read()) {
                                            เงินเดือนขั้นต่ำของระดับชำนาญการพิเศษ = reader2.GetInt32(0);
                                        }

                                    }
                                }

                                int ปีที่ได้รับบม = -1;
                                int ปีที่ได้รับบช = -1;
                                int ปีที่ได้รับจม = -1;
                                int ปีที่ได้รับจช = -1;
                                int ปีที่ได้รับตม = -1;
                                int ปีที่ได้รับตช = -1;
                                int ปีที่ได้รับทม = -1;
                                int ปีที่ได้รับทช = -1;
                                int ปีที่ได้รับปม = -1;
                                int ปีที่ได้รับปช = -1;
                                int ปีที่ได้รับมวม = -1;
                                int ปีที่ได้รับมปช = -1;

                                for (int i = 1; i <= 12; i++) {
                                    using (OracleCommand com2 = new OracleCommand("SELECT TRUNC((CURRENT_DATE - IR_DATE_GET_INSIG)/365,0) FROM TB_INSIG_REQUEST WHERE IR_INSIG_ID = " + i + " AND IR_CITIZEN_ID = '" + psID + "'", con)) {
                                        using (OracleDataReader reader2 = com2.ExecuteReader()) {
                                            while (reader2.Read()) {
                                                int year = reader2.IsDBNull(0) ? 0 : reader2.GetInt32(0);
                                                if (i == 1) { ปีที่ได้รับบม = year; }
                                                else if (i == 2) { ปีที่ได้รับบช = year; }
                                                else if (i == 3) { ปีที่ได้รับจม = year; }
                                                else if (i == 4) { ปีที่ได้รับจช = year; }
                                                else if (i == 5) { ปีที่ได้รับตม = year; }
                                                else if (i == 6) { ปีที่ได้รับตช = year; }
                                                else if (i == 7) { ปีที่ได้รับทม = year; }
                                                else if (i == 8) { ปีที่ได้รับทช = year; }
                                                else if (i == 9) { ปีที่ได้รับปม = year; }
                                                else if (i == 10) { ปีที่ได้รับปช = year; }
                                                else if (i == 11) { ปีที่ได้รับมวม = year; }
                                                else if (i == 12) { ปีที่ได้รับมปช = year; }

                                            }

                                        }
                                    }
                                }

                                lbName.Attributes.Add("tuu2", "-1");
                                if (รหัสประเภทบุคลากร == 1) {//ข้าราชการ
                                    if (รหัสตำแหน่งประเภททางทั่วไป == 10) {//ระดับปฏิบัติงาน
                                        if (รหัสเครืองราชปัจจุบัน == -1) {
                                            ConditionExecute(new string[] {
                                                "","บ.ม."
                                            }, new bool[] {
                                                ปีที่ทำงาน >= 5
                                            }, new string[] {
                                                "อายุงานครบ 5 ปี"
                                            });
                                            lbName.Attributes.Add("tuu2", "12");
                                        } else if (รหัสเครืองราชปัจจุบัน == 12) {
                                            ConditionExecute(new string[] {
                                                "บ.ม.","บ.ช."
                                            }, new bool[] {
                                                ปีที่ทำงาน >= 5,
                                                เงินเดือนปัจจุบัน <= เงินเดือนขั้นต่ำของระดับชำนาญงาน,
                                                ปีที่ดำรงตำแหน่งระดับปฏิบัติงาน >= 10
                                        }, new string[] {
                                                "อายุงานครบ 5 ปี",
                                                "เงินเดือนน้อยกว่าเงินเดือนขั้นต่ำของระดับชำนาญงาน",
                                                "ปีที่ดำรงตำแหน่งระดับปฏิบัติงานไม่น้อยกว่า 10 ปี"
                                            });
                                            lbName.Attributes.Add("tuu2", "11");
                                        } else if (รหัสเครืองราชปัจจุบัน == 11) {
                                            ConditionExecute(new string[] {
                                                "บ.ช.","จ.ม."
                                            }, new bool[] {
                                                ปีที่ทำงาน >= 5,
                                                เงินเดือนปัจจุบัน > เงินเดือนขั้นต่ำของระดับชำนาญงาน,
                                                ปีที่ดำรงตำแหน่งระดับปฏิบัติงาน >= 10
                                        }, new string[] {
                                                "อายุงานครบ 5 ปี",
                                                "เงินเดือนมากกว่าเงินเดือนขั้นต่ำของระดับชำนาญงาน",
                                                "ปีที่ดำรงตำแหน่งระดับปฏิบัติงานไม่น้อยกว่า 10 ปี"
                                            });
                                            lbName.Attributes.Add("tuu2", "10");
                                        } else if (รหัสเครืองราชปัจจุบัน == 10) {
                                            ConditionExecute(new string[] {
                                                "จ.ม.","จ.ช."
                                            }, new bool[] {
                                                ปีที่ทำงาน >= 5,
                                                เงินเดือนปัจจุบัน > เงินเดือนขั้นต่ำของระดับชำนาญงาน,
                                                ปีที่ดำรงตำแหน่งระดับปฏิบัติงาน >= 10
                                        }, new string[] {
                                                "อายุงานครบ 5 ปี",
                                                "เงินเดือนมากกว่าเงินเดือนขั้นต่ำของระดับชำนาญงาน",
                                                "ปีที่ดำรงตำแหน่งระดับปฏิบัติงานไม่น้อยกว่า 10 ปี"
                                            });
                                            lbName.Attributes.Add("tuu2", "9");
                                        } else if (รหัสเครืองราชปัจจุบัน == 9) {
                                            ConditionExecute(new string[] { "จ.ช.", "" });
                                            lbName.Attributes.Add("tuu2", "0");
                                        }
                                    } else if (รหัสตำแหน่งประเภททางทั่วไป == 11) {//ระดับชำนาญงาน
                                        if (รหัสเครืองราชปัจจุบัน == -1) {
                                            ConditionExecute(new string[] {
                                                "","ต.ม."
                                            }, new bool[] {
                                                ปีที่ทำงาน >= 5
                                            }, new string[] {
                                                "อายุงานครบ 5 ปี"
                                            });
                                            lbName.Attributes.Add("tuu2", "8");
                                        } else if (รหัสเครืองราชปัจจุบัน == 8) {
                                            ConditionExecute(new string[] {
                                                "ต.ม.","ต.ช."
                                            }, new bool[] {
                                                ปีที่ทำงาน >= 5,
                                                ปีที่ดำรงตำแหน่งระดับชำนาญงาน >= 5
                                        }, new string[] {
                                                "อายุงานครบ 5 ปี",
                                                "ปีที่ดำรงตำแหน่งระดับชำนาญงานไม่น้อยกว่า 5 ปี"
                                            });
                                            lbName.Attributes.Add("tuu2", "7");
                                        } else if (รหัสเครืองราชปัจจุบัน == 7) {
                                            ConditionExecute(new string[] { "ต.ช.", "" });
                                            lbName.Attributes.Add("tuu2", "0");
                                        }
                                    } else if (รหัสตำแหน่งประเภททางทั่วไป == 12) {//ระดับอาวุโส
                                        if (รหัสเครืองราชปัจจุบัน == -1) {
                                            ConditionExecute(new string[] {
                                                "","ท.ม."
                                            }, new bool[] {
                                                ปีที่ทำงาน >= 5
                                            }, new string[] {
                                                "อายุงานครบ 5 ปี"
                                            });
                                            lbName.Attributes.Add("tuu2", "6");
                                        } else if (รหัสเครืองราชปัจจุบัน == 6) {
                                            ConditionExecute(new string[] {
                                                "ท.ม.","ท.ช."
                                            }, new bool[] {
                                                ปีที่ทำงาน >= 5,
                                                ปีที่ดำรงตำแหน่งระดับอาวุโส >= 5
                                        }, new string[] {
                                                "อายุงานครบ 5 ปี",
                                                "ปีที่ดำรงตำแหน่งระดับอาวุโสไม่น้อยกว่า 5 ปี"
                                            });
                                            lbName.Attributes.Add("tuu2", "5");
                                        } else if (รหัสเครืองราชปัจจุบัน == 5) {
                                            ConditionExecute(new string[] { "ท.ช.", "" });
                                            lbName.Attributes.Add("tuu2", "0");
                                        }
                                    } else if (รหัสตำแหน่งประเภททางทั่วไป == 13) {//ระดับทักษะพิเศษ
                                        if (รหัสเครืองราชปัจจุบัน == 3) {
                                            ConditionExecute(new string[] {
                                                "ป.ช.","ม.ว.ม"
                                            }, new bool[] {
                                                ปีที่ทำงาน >= 5,
                                                ปีที่ได้รับทช >= 3,
                                                ปีที่ได้รับปม >= 3,
                                                ปีที่ได้รับปช >= 5
                                            }, new string[] {
                                                "อายุงานครบ 5 ปี",
                                                "ได้ ท.ช. มาแล้วไม่น้อยกว่า 3 ปี",
                                                "ได้ ป.ม. มาแล้วไม่น้อยกว่า 3 ปี",
                                                "ได้ ป.ช. มาแล้วไม่น้อยกว่า 5 ปี"
                                            });
                                            lbName.Attributes.Add("tuu2", "8");
                                        } else if (รหัสเครืองราชปัจจุบัน == 8) {
                                            ConditionExecute(new string[] { "ต.ม.", "" });
                                            lbName.Attributes.Add("tuu2", "0");
                                        }
                                    } else if (รหัสตำแหน่งประเภททางวิชาการ == 5) {//ระดับปฏิบัติการ
                                        if (รหัสเครืองราชปัจจุบัน == -1) {
                                            ConditionExecute(new string[] {
                                                "","ต.ม."
                                            }, new bool[] {
                                                ปีที่ทำงาน >= 5
                                            }, new string[] {
                                                "อายุงานครบ 5 ปี"
                                            });
                                            lbName.Attributes.Add("tuu2", "8");
                                        } else if (รหัสเครืองราชปัจจุบัน == 8) {
                                            ConditionExecute(new string[] { "ต.ม.", "" });
                                            lbName.Attributes.Add("tuu2", "0");
                                        }
                                    } else if (รหัสตำแหน่งประเภททางวิชาการ == 6) {//ระดับชำนาญการ
                                        if (รหัสเครืองราชปัจจุบัน == -1) {
                                            ConditionExecute(new string[] {
                                                "","ต.ช."
                                            }, new bool[] {
                                                ปีที่ทำงาน >= 5
                                            }, new string[] {
                                                "อายุงานครบ 5 ปี"
                                            });
                                            lbName.Attributes.Add("tuu2", "7");
                                        } else if (รหัสเครืองราชปัจจุบัน == 7) {
                                            ConditionExecute(new string[] {
                                                "ต.ช.","ท.ม."
                                            }, new bool[] {
                                                ปีที่ทำงาน >= 5,
                                                เงินเดือนปัจจุบัน > เงินเดือนขั้นต่ำของระดับชำนาญการพิเศษ,
                                        }, new string[] {
                                                "อายุงานครบ 5 ปี",
                                                "เงินเดือนมากกว่าเงินเดือนขั้นต่ำของระดับชำนาญการพิเศษ"
                                            });
                                            lbName.Attributes.Add("tuu2", "6");
                                        } else if (รหัสเครืองราชปัจจุบัน == 6) {
                                            ConditionExecute(new string[] {
                                                "ท.ม.","ท.ช."
                                            }, new bool[] {
                                                ปีที่ทำงาน >= 5,
                                                เงินเดือนปัจจุบัน > เงินเดือนขั้นต่ำของระดับชำนาญการพิเศษ,
                                                1 == 1 /*--------------------------*/
                                        }, new string[] {
                                                "อายุงานครบ 5 ปี",
                                                "เงินเดือนมากกว่าเงินเดือนขั้นต่ำของระดับชำนาญการพิเศษ",
                                                "ได้รับเงินเดือนไม่ต่ำกว่าขั้นต่ำของระดับชำนาญการพิเศษมาแล้วไม่น้อยกว่า 5 ปี"
                                            });
                                            lbName.Attributes.Add("tuu2", "5");
                                        } else if (รหัสเครืองราชปัจจุบัน == 5) {
                                            ConditionExecute(new string[] { "ท.ช.", "" });
                                            lbName.Attributes.Add("tuu2", "0");
                                        }
                                    } else if (รหัสตำแหน่งประเภททางวิชาการ == 7) {//ระดับชำนาญการพิเศษ
                                        if (รหัสเครืองราชปัจจุบัน == -1) {
                                            ConditionExecute(new string[] {
                                                "","ท.ช."
                                            }, new bool[] {
                                                ปีที่ทำงาน >= 5
                                            }, new string[] {
                                                "อายุงานครบ 5 ปี"
                                            });
                                            lbName.Attributes.Add("tuu2", "5");
                                        } else if (รหัสเครืองราชปัจจุบัน == 5) {
                                            ConditionExecute(new string[] {
                                                "ท.ช.","ป.ม."
                                            }, new bool[] {
                                                ปีที่ทำงาน >= 5,
                                                เงินเดือนปัจจุบัน > เงินเดือนขั้นต่ำของระดับชำนาญการพิเศษ,
                                                ปีที่ได้รับทช >= 5 /*--------------------------*/
                                        }, new string[] {
                                                "อายุงานครบ 5 ปี",
                                                "เงินเดือนมากกว่าเงินเดือนขั้นต่ำของระดับชำนาญการพิเศษ",
                                                "ได้รับเงินเดือนขั้นสูงและได้ ท.ช. มาแล้วไม่น้อยกว่า 5 ปี"
                                            });
                                            lbName.Attributes.Add("tuu2", "4");
                                        } else if (รหัสเครืองราชปัจจุบัน == 4) {
                                            ConditionExecute(new string[] { "ป.ม.", "" });
                                            lbName.Attributes.Add("tuu2", "0");
                                        }
                                    } else if (รหัสตำแหน่งประเภททางวิชาการ == 8) {//ระดับเชี่ยวชาญ
                                        if (รหัสเครืองราชปัจจุบัน == -1) {
                                            ConditionExecute(new string[] {
                                                "","ม.ว.ม."
                                            }, new bool[] {
                                                ปีที่ทำงาน >= 5,
                                                ปีที่ได้รับทช >= 3,
                                                ปีที่ได้รับปม >= 3,
                                                ปีที่ได้รับปช >= 5
                                            }, new string[] {
                                                "อายุงานครบ 5 ปี",
                                                "ได้ ท.ช. มาแล้วไม่น้อยกว่า 3 ปี",
                                                "ได้ ป.ม. มาแล้วไม่น้อยกว่า 3 ปี",
                                                "ได้ ป.ช. มาแล้วไม่น้อยกว่า 5 ปี",
                                            });
                                            lbName.Attributes.Add("tuu2", "2");
                                        } else if (รหัสเครืองราชปัจจุบัน == 2) {
                                            ConditionExecute(new string[] { "ม.ว.ม.", "" });
                                            lbName.Attributes.Add("tuu2", "0");
                                        }
                                    } else if (รหัสตำแหน่งประเภททางวิชาการ == 9) {//ระดับทรงคุณวุฒิ 13000
                                        if (รหัสเครืองราชปัจจุบัน == -1) {
                                            ConditionExecute(new string[] {
                                                "","ม.ป.ช."
                                            }, new bool[] {
                                                ปีที่ทำงาน >= 5,
                                                ปีที่ได้รับปม >= 3,
                                                ปีที่ได้รับปช >= 3,
                                                ปีที่ได้รับมวม >= 5
                                            }, new string[] {
                                                "อายุงานครบ 5 ปี",
                                                "ได้ ป.ม. มาแล้วไม่น้อยกว่า 3 ปี",
                                                "ได้ ป.ช. มาแล้วไม่น้อยกว่า 3 ปี",
                                                "ได้ ม.ว.ม. มาแล้วไม่น้อยกว่า 5 ปี",
                                            });
                                            lbName.Attributes.Add("tuu2", "2");
                                        } else if (รหัสเครืองราชปัจจุบัน == 2) {
                                            ConditionExecute(new string[] { "ม.ป.ช.", "" });
                                            lbName.Attributes.Add("tuu2", "0");
                                        }
                                    } else if (รหัสตำแหน่งประเภททางอำนวยการ == 3) {//อำนวยการ ระดับต้น
                                        if (รหัสเครืองราชปัจจุบัน == -1) {
                                            ConditionExecute(new string[] {
                                                "","ท.ช."
                                            }, new bool[] {
                                                ปีที่ทำงาน >= 5,
                                            }, new string[] {
                                                "อายุงานครบ 5 ปี",
                                            });
                                            lbName.Attributes.Add("tuu2", "5");
                                        } else if (รหัสเครืองราชปัจจุบัน == 5) {
                                            ConditionExecute(new string[] {
                                                "ท.ช.","ป.ม."
                                            }, new bool[] {
                                                ปีที่ทำงาน >= 5,
                                                เงินเดือนปัจจุบัน > เงินเดือนขั้นต่ำของระดับชำนาญการพิเศษ,
                                                ปีที่ได้รับทช >= 3 , /*--------------------------*/
                                        }, new string[] {
                                                "อายุงานครบ 5 ปี",
                                                "เงินเดือนมากกว่าเงินเดือนขั้นต่ำของระดับชำนาญการพิเศษ",
                                                "ได้รับเงินเดือนขั้นสูงและได้ ท.ช. มาแล้วไม่น้อยกว่า 3 ปี"
                                            });
                                            lbName.Attributes.Add("tuu2", "4");
                                        } else if (รหัสเครืองราชปัจจุบัน == 4) {
                                            ConditionExecute(new string[] { "ป.ม.", "" });
                                            lbName.Attributes.Add("tuu2", "0");
                                        }
                                    } else if (รหัสตำแหน่งประเภททางอำนวยการ == 4) {//อำนวยการ ระดับสูง
                                        if (รหัสเครืองราชปัจจุบัน == -1) {
                                            ConditionExecute(new string[] {
                                                "","ม.ว.ม."
                                            }, new bool[] {
                                                ปีที่ทำงาน >= 5,
                                                ปีที่ได้รับทช >= 3,
                                                ปีที่ได้รับปม >= 3,
                                                ปีที่ได้รับปช >= 5
                                            }, new string[] {
                                                "อายุงานครบ 5 ปี",
                                                "ได้ ท.ช. มาแล้วไม่น้อยกว่า 3 ปี",
                                                "ได้ ป.ม. มาแล้วไม่น้อยกว่า 3 ปี",
                                                "ได้ ป.ช. มาแล้วไม่น้อยกว่า 5 ปี",
                                            });
                                            lbName.Attributes.Add("tuu2", "2");
                                        } else if (รหัสเครืองราชปัจจุบัน == 2) {
                                            ConditionExecute(new string[] { "ม.ว.ม.", "" });
                                            lbName.Attributes.Add("tuu2", "0");
                                        }
                                    }
                                } else if (รหัสประเภทบุคลากร == 2) {//พนง ในสถาบัน
                                    if (รหัสตำแหน่งทางบริหาร == 4) {// 2.หัวหน้าแผนก / ฝ่าย
                                        if (รหัสเครืองราชปัจจุบัน == -1) {
                                            ConditionExecute(new string[] {
                                                "","จ.ม."
                                            }, new bool[] {
                                                ปีที่ทำงาน >= 5
                                            }, new string[] {
                                                "อายุงานครบ 5 ปี"
                                            });
                                            lbName.Attributes.Add("tuu2", "10");
                                        } else if (รหัสเครืองราชปัจจุบัน == 10) {
                                            ConditionExecute(new string[] {
                                                "จ.ม.","จ.ช."
                                            }, new bool[] {
                                                ปีที่ทำงาน >= 5,
                                        }, new string[] {
                                                "อายุงานครบ 5 ปี",
                                            });
                                            lbName.Attributes.Add("tuu2", "9");
                                        } else if (รหัสเครืองราชปัจจุบัน == 9) {
                                            ConditionExecute(new string[] {
                                                "จ.ช.","ต.ม."
                                            }, new bool[] {
                                                ปีที่ทำงาน >= 5,
                                        }, new string[] {
                                                "อายุงานครบ 5 ปี",
                                            });
                                            lbName.Attributes.Add("tuu2", "8");
                                        } else if (รหัสเครืองราชปัจจุบัน == 8) {
                                            ConditionExecute(new string[] { "ต.ม.", "" });
                                            lbName.Attributes.Add("tuu2", "0");
                                        }
                                    } else if (รหัสตำแหน่งทางวิชาการ == 1 || รหัสตำแหน่งทางวิชาการ == 2) {// 3.ผศ. หรือ อาจารย์
                                        if (รหัสเครืองราชปัจจุบัน == -1) {
                                            ConditionExecute(new string[] {
                                                "","จ.ช."
                                            }, new bool[] {
                                                ปีที่ทำงาน >= 5
                                            }, new string[] {
                                                "อายุงานครบ 5 ปี"
                                            });
                                            lbName.Attributes.Add("tuu2", "9");
                                        } else if (รหัสเครืองราชปัจจุบัน == 9) {
                                            ConditionExecute(new string[] {
                                                "จ.ช.","ต.ม."
                                            }, new bool[] {
                                                ปีที่ทำงาน >= 5,
                                        }, new string[] {
                                                "อายุงานครบ 5 ปี",
                                            });
                                            lbName.Attributes.Add("tuu2", "8");
                                        } else if (รหัสเครืองราชปัจจุบัน == 8) {
                                            ConditionExecute(new string[] {
                                                "ต.ม.","ต.ช."
                                            }, new bool[] {
                                                ปีที่ทำงาน >= 5,
                                        }, new string[] {
                                                "อายุงานครบ 5 ปี",
                                            });
                                            lbName.Attributes.Add("tuu2", "7");
                                        } else if (รหัสเครืองราชปัจจุบัน == 7) {
                                            ConditionExecute(new string[] {
                                                "ต.ช.","ท.ม."
                                            }, new bool[] {
                                                ปีที่ทำงาน >= 5,
                                        }, new string[] {
                                                "อายุงานครบ 5 ปี",
                                            });
                                            lbName.Attributes.Add("tuu2", "6");
                                        } else if (รหัสเครืองราชปัจจุบัน == 6) {
                                            ConditionExecute(new string[] { "ท.ม.", "" });
                                            lbName.Attributes.Add("tuu2", "0");
                                        }
                                    } else if (รหัสตำแหน่งทางบริหาร == 5 || รหัสตำแหน่งทางบริหาร == 6) {// 4.ผช อธิการบดี หรือ รอง คณบดี
                                        if (รหัสเครืองราชปัจจุบัน == -1) {
                                            ConditionExecute(new string[] {
                                                "","ต.ม."
                                            }, new bool[] {
                                                ปีที่ทำงาน >= 5
                                            }, new string[] {
                                                "อายุงานครบ 5 ปี"
                                            });
                                            lbName.Attributes.Add("tuu2", "8");
                                        } else if (รหัสเครืองราชปัจจุบัน == 8) {
                                            ConditionExecute(new string[] {
                                                "ต.ม.","ต.ช."
                                            }, new bool[] {
                                                ปีที่ทำงาน >= 5,
                                        }, new string[] {
                                                "อายุงานครบ 5 ปี",
                                            });
                                            lbName.Attributes.Add("tuu2", "7");
                                        } else if (รหัสเครืองราชปัจจุบัน == 7) {
                                            ConditionExecute(new string[] {
                                                "ต.ช.","ท.ม."
                                            }, new bool[] {
                                                ปีที่ทำงาน >= 5,
                                        }, new string[] {
                                                "อายุงานครบ 5 ปี",
                                            });
                                            lbName.Attributes.Add("tuu2", "6");
                                        } else if (รหัสเครืองราชปัจจุบัน == 6) {
                                            ConditionExecute(new string[] { "ท.ม.", "" });
                                            lbName.Attributes.Add("tuu2", "0");
                                        }
                                    } else if (รหัสตำแหน่งทางวิชาการ == 3) {// 5.รศ
                                        if (รหัสเครืองราชปัจจุบัน == -1) {
                                            ConditionExecute(new string[] {
                                                "","ต.ม."
                                            }, new bool[] {
                                                ปีที่ทำงาน >= 5
                                            }, new string[] {
                                                "อายุงานครบ 5 ปี"
                                            });
                                            lbName.Attributes.Add("tuu2", "8");
                                        } else if (รหัสเครืองราชปัจจุบัน == 8) {
                                            ConditionExecute(new string[] {
                                                "ต.ม.","ต.ช."
                                            }, new bool[] {
                                                ปีที่ทำงาน >= 5,
                                        }, new string[] {
                                                "อายุงานครบ 5 ปี",
                                            });
                                            lbName.Attributes.Add("tuu2", "7");
                                        } else if (รหัสเครืองราชปัจจุบัน == 7) {
                                            ConditionExecute(new string[] {
                                                "ต.ช.","ท.ม."
                                            }, new bool[] {
                                                ปีที่ทำงาน >= 5,
                                        }, new string[] {
                                                "อายุงานครบ 5 ปี",
                                            });
                                            lbName.Attributes.Add("tuu2", "6");
                                        } else if (รหัสเครืองราชปัจจุบัน == 6) {
                                            ConditionExecute(new string[] {
                                                "ท.ม.","ท.ช."
                                            }, new bool[] {
                                                ปีที่ทำงาน >= 5,
                                        }, new string[] {
                                                "อายุงานครบ 5 ปี",
                                            });
                                            lbName.Attributes.Add("tuu2", "5");
                                        } else if (รหัสเครืองราชปัจจุบัน == 5) {
                                            ConditionExecute(new string[] {
                                                "ท.ช.","ป.ม."
                                            }, new bool[] {
                                                ปีที่ทำงาน >= 5,
                                        }, new string[] {
                                                "อายุงานครบ 5 ปี",
                                            });
                                            lbName.Attributes.Add("tuu2", "4");
                                        } else if (รหัสเครืองราชปัจจุบัน == 4) {
                                            ConditionExecute(new string[] { "ป.ม.", "" });
                                            lbName.Attributes.Add("tuu2", "0");
                                        }
                                    } else if (รหัสตำแหน่งทางบริหาร == 2 || รหัสตำแหน่งทางบริหาร == 3) {// 6.รองอธิการบดี คณบดี
                                        if (รหัสเครืองราชปัจจุบัน == -1) {
                                            ConditionExecute(new string[] {
                                                "","ท.ม."
                                            }, new bool[] {
                                                ปีที่ทำงาน >= 5
                                            }, new string[] {
                                                "อายุงานครบ 5 ปี"
                                            });
                                            lbName.Attributes.Add("tuu2", "6");
                                        } else if (รหัสเครืองราชปัจจุบัน == 6) {
                                            ConditionExecute(new string[] {
                                                "ท.ม.","ท.ช."
                                            }, new bool[] {
                                                ปีที่ทำงาน >= 5,
                                        }, new string[] {
                                                "อายุงานครบ 5 ปี",
                                            });
                                            lbName.Attributes.Add("tuu2", "5");
                                        } else if (รหัสเครืองราชปัจจุบัน == 5) {
                                            ConditionExecute(new string[] { "ท.ช.", "" });
                                            lbName.Attributes.Add("tuu2", "0");
                                        }
                                    } else if (รหัสตำแหน่งทางวิชาการ == 4) {// 7.ศาสตราจารย์
                                        if (รหัสเครืองราชปัจจุบัน == -1) {
                                            ConditionExecute(new string[] {
                                                "","ท.ม."
                                            }, new bool[] {
                                                ปีที่ทำงาน >= 5
                                            }, new string[] {
                                                "อายุงานครบ 5 ปี"
                                            });
                                            lbName.Attributes.Add("tuu2", "6");
                                        } else if (รหัสเครืองราชปัจจุบัน == 6) {
                                            ConditionExecute(new string[] {
                                                "ท.ม.","ท.ช."
                                            }, new bool[] {
                                                ปีที่ทำงาน >= 5,
                                        }, new string[] {
                                                "อายุงานครบ 5 ปี",
                                            });
                                            lbName.Attributes.Add("tuu2", "5");
                                        } else if (รหัสเครืองราชปัจจุบัน == 5) {
                                            ConditionExecute(new string[] {
                                                "ท.ช.","ป.ม."
                                            }, new bool[] {
                                                ปีที่ทำงาน >= 5,
                                        }, new string[] {
                                                "อายุงานครบ 5 ปี",
                                            });
                                            lbName.Attributes.Add("tuu2", "4");
                                        } else if (รหัสเครืองราชปัจจุบัน == 4) {
                                            ConditionExecute(new string[] {
                                                "ป.ม.","ป.ช."
                                            }, new bool[] {
                                                ปีที่ทำงาน >= 5,
                                        }, new string[] {
                                                "อายุงานครบ 5 ปี",
                                            });
                                            lbName.Attributes.Add("tuu2", "3");
                                        } else if (รหัสเครืองราชปัจจุบัน == 3) {
                                            ConditionExecute(new string[] { "ป.ช.", "" });
                                            lbName.Attributes.Add("tuu2", "0");
                                        }
                                    } else if (รหัสตำแหน่งทางบริหาร == 1) {// 8.อธิการบดี
                                        if (รหัสเครืองราชปัจจุบัน == -1) {
                                            ConditionExecute(new string[] {
                                                "","ท.ม."
                                            }, new bool[] {
                                                ปีที่ทำงาน >= 5
                                            }, new string[] {
                                                "อายุงานครบ 5 ปี"
                                            });
                                            lbName.Attributes.Add("tuu2", "6");
                                        } else if (รหัสเครืองราชปัจจุบัน == 6) {
                                            ConditionExecute(new string[] {
                                                "ท.ม.","ท.ช."
                                            }, new bool[] {
                                                ปีที่ทำงาน >= 5,
                                        }, new string[] {
                                                "อายุงานครบ 5 ปี",
                                            });
                                            lbName.Attributes.Add("tuu2", "5");
                                        } else if (รหัสเครืองราชปัจจุบัน == 5) {
                                            ConditionExecute(new string[] {
                                                "ท.ช.","ป.ม."
                                            }, new bool[] {
                                                ปีที่ทำงาน >= 5,
                                        }, new string[] {
                                                "อายุงานครบ 5 ปี",
                                            });
                                            lbName.Attributes.Add("tuu2", "4");
                                        } else if (รหัสเครืองราชปัจจุบัน == 4) {
                                            ConditionExecute(new string[] { "ป.ม.", "" });
                                            lbName.Attributes.Add("tuu2", "0");
                                        }
                                    } else {
                                        if (รหัสเครืองราชปัจจุบัน == -1) {
                                            ConditionExecute(new string[] {
                                                "","บ.ม."
                                            }, new bool[] {
                                                ปีที่ทำงาน >= 5
                                            }, new string[] {
                                                "อายุงานครบ 5 ปี"
                                            });
                                            lbName.Attributes.Add("tuu2", "12");
                                        } else if (รหัสเครืองราชปัจจุบัน == 12) {
                                            ConditionExecute(new string[] {
                                                "บ.ม.","บ.ช."
                                            }, new bool[] {
                                                ปีที่ทำงาน >= 5,
                                        }, new string[] {
                                                "อายุงานครบ 5 ปี",
                                            });
                                            lbName.Attributes.Add("tuu2", "11");
                                        } else if (รหัสเครืองราชปัจจุบัน == 11) {
                                            ConditionExecute(new string[] {
                                                "บ.ช.","จ.ม."
                                            }, new bool[] {
                                                ปีที่ทำงาน >= 5,
                                        }, new string[] {
                                                "อายุงานครบ 5 ปี",
                                            });
                                            lbName.Attributes.Add("tuu2", "10");
                                        } else if (รหัสเครืองราชปัจจุบัน == 10) {
                                            ConditionExecute(new string[] {
                                                "จ.ม.","จ.ช."
                                            }, new bool[] {
                                                ปีที่ทำงาน >= 5,
                                        }, new string[] {
                                                "อายุงานครบ 5 ปี",
                                            });
                                            lbName.Attributes.Add("tuu2", "9");
                                        } else if (รหัสเครืองราชปัจจุบัน == 9) {
                                            ConditionExecute(new string[] { "จ.ช.", "" });
                                        }
                                    }
                                } else if (รหัสประเภทบุคลากร == 6) {//พนักงานราชการ
                                    if (รหัสกลุ่มงานพนักงานราชการ == 14 || รหัสกลุ่มงานพนักงานราชการ == 15) { //บริการ เทคนิค
                                        if (รหัสเครืองราชปัจจุบัน == -1) { //1
                                            ConditionExecute(new string[] {
                                                "","บ.ม."
                                            }, new bool[] {
                                                ปีที่ทำงาน >= 5
                                            }, new string[] {
                                                "อายุงานครบ 5 ปี"
                                            });
                                            lbName.Attributes.Add("tuu2", "12");
                                        } else if (รหัสเครืองราชปัจจุบัน == 12) {
                                            ConditionExecute(new string[] {
                                                "บ.ม.","บ.ช."
                                            }, new bool[] {
                                                ปีที่ทำงาน >= 5,
                                                ปีที่ได้รับบม >= 5
                                        }, new string[] {
                                                "อายุงานครบ 5 ปี",
                                                "ได้ บ.ม. มาแล้วไม่น้อยกว่า 5 ปี",
                                            });
                                            lbName.Attributes.Add("tuu2", "11");
                                        } else if (รหัสเครืองราชปัจจุบัน == 11) {
                                            ConditionExecute(new string[] {
                                                "บ.ช.","จ.ม."
                                            }, new bool[] {
                                                ปีที่ทำงาน >= 5,
                                                ปีที่ได้รับบช >= 5
                                        }, new string[] {
                                                "อายุงานครบ 5 ปี",
                                                "ได้ บ.ช. มาแล้วไม่น้อยกว่า 5 ปี"
                                            });
                                            lbName.Attributes.Add("tuu2", "10");
                                        } else if (รหัสเครืองราชปัจจุบัน == 10) {
                                            ConditionExecute(new string[] {
                                                "จ.ม.","จ.ช."
                                            }, new bool[] {
                                                ปีที่ทำงาน >= 5,
                                                ปีที่ได้รับจม >= 5
                                        }, new string[] {
                                                "อายุงานครบ 5 ปี",
                                                "ได้ จ.ม. มาแล้วไม่น้อยกว่า 5 ปี"
                                            });
                                            lbName.Attributes.Add("tuu2", "9");
                                        } else if (รหัสเครืองราชปัจจุบัน == 9) {
                                            ConditionExecute(new string[] { "จ.ช.", "" });
                                            lbName.Attributes.Add("tuu2", "0");
                                        }
                                    } else if (รหัสกลุ่มงานพนักงานราชการ == 16) { //2 บริหารงานทั่วไป
                                        if (รหัสเครืองราชปัจจุบัน == -1) {
                                            ConditionExecute(new string[] {
                                                "","บ.ช."
                                            }, new bool[] {
                                                ปีที่ทำงาน >= 5
                                            }, new string[] {
                                                "อายุงานครบ 5 ปี"
                                            });
                                            lbName.Attributes.Add("tuu2", "11");
                                        } else if (รหัสเครืองราชปัจจุบัน == 11) {
                                            ConditionExecute(new string[] {
                                                "บ.ช.","จ.ม."
                                            }, new bool[] {
                                                ปีที่ทำงาน >= 5,
                                                ปีที่ได้รับบช >= 5
                                        }, new string[] {
                                                "อายุงานครบ 5 ปี",
                                                "ได้ บ.ช. มาแล้วไม่น้อยกว่า 5 ปี",
                                            });
                                            lbName.Attributes.Add("tuu2", "10");
                                        } else if (รหัสเครืองราชปัจจุบัน == 10) {
                                            ConditionExecute(new string[] {
                                                "จ.ม.","จ.ช."
                                            }, new bool[] {
                                                ปีที่ทำงาน >= 5,
                                                ปีที่ได้รับจม >= 5
                                        }, new string[] {
                                                "อายุงานครบ 5 ปี",
                                                "ได้ จ.ม. มาแล้วไม่น้อยกว่า 5 ปี"
                                            });
                                            lbName.Attributes.Add("tuu2", "9");
                                        } else if (รหัสเครืองราชปัจจุบัน == 9) {
                                            ConditionExecute(new string[] {
                                                "จ.ช.","ต.ม."
                                            }, new bool[] {
                                                ปีที่ทำงาน >= 5,
                                                ปีที่ได้รับจช >= 5
                                        }, new string[] {
                                                "อายุงานครบ 5 ปี",
                                                "ได้ จ.ช. มาแล้วไม่น้อยกว่า 5 ปี"
                                            });
                                        } else if (รหัสเครืองราชปัจจุบัน == 8) {
                                            ConditionExecute(new string[] { "ต.ม.", "" });
                                            lbName.Attributes.Add("tuu2", "0");
                                        }
                                    } else if (รหัสกลุ่มงานพนักงานราชการ == 17) { //3 กลุ่มงานวิชาชีพเฉพาะ
                                        if (รหัสเครืองราชปัจจุบัน == -1) {
                                            ConditionExecute(new string[] {
                                                "","จ.ม."
                                            }, new bool[] {
                                                ปีที่ทำงาน >= 5
                                            }, new string[] {
                                                "อายุงานครบ 5 ปี"
                                            });
                                            lbName.Attributes.Add("tuu2", "10");
                                        } else if (รหัสเครืองราชปัจจุบัน == 10) {
                                            ConditionExecute(new string[] {
                                                "จ.ม.","จ.ช."
                                            }, new bool[] {
                                                ปีที่ทำงาน >= 5,
                                                ปีที่ได้รับจม >= 5
                                        }, new string[] {
                                                "อายุงานครบ 5 ปี",
                                                "ได้ จ.ม. มาแล้วไม่น้อยกว่า 5 ปี"
                                            });
                                            lbName.Attributes.Add("tuu2", "9");
                                        } else if (รหัสเครืองราชปัจจุบัน == 9) {
                                            ConditionExecute(new string[] {
                                                "จ.ช.","ต.ม."
                                            }, new bool[] {
                                                ปีที่ทำงาน >= 5,
                                                ปีที่ได้รับจช >= 5
                                        }, new string[] {
                                                "อายุงานครบ 5 ปี",
                                                "ได้ จ.ช. มาแล้วไม่น้อยกว่า 5 ปี"
                                            });
                                            lbName.Attributes.Add("tuu2", "8");
                                        } else if (รหัสเครืองราชปัจจุบัน == 8) {
                                            ConditionExecute(new string[] {
                                                "ต.ม.","ต.ช."
                                            }, new bool[] {
                                                ปีที่ทำงาน >= 5,
                                                ปีที่ได้รับตม >= 5
                                        }, new string[] {
                                                "อายุงานครบ 5 ปี",
                                                "ได้ ต.ม. มาแล้วไม่น้อยกว่า 5 ปี"
                                            });
                                            lbName.Attributes.Add("tuu2", "7");
                                        } else if (รหัสเครืองราชปัจจุบัน == 7) {
                                            ConditionExecute(new string[] { "ต.ช.", "" });
                                            lbName.Attributes.Add("tuu2", "0");
                                        }
                                    } else if (รหัสกลุ่มงานพนักงานราชการ == 18) { //4 กลุ่มงานเชี่ยวชาญเฉพาะ
                                        if (รหัสเครืองราชปัจจุบัน == -1) {
                                            ConditionExecute(new string[] {
                                                "","จ.ช."
                                            }, new bool[] {
                                                ปีที่ทำงาน >= 5
                                            }, new string[] {
                                                "อายุงานครบ 5 ปี"
                                            });
                                            lbName.Attributes.Add("tuu2", "9");
                                        } else if (รหัสเครืองราชปัจจุบัน == 9) {
                                            ConditionExecute(new string[] {
                                                "จ.ช.","ต.ม."
                                            }, new bool[] {
                                                ปีที่ทำงาน >= 5,
                                                ปีที่ได้รับจช >= 5
                                        }, new string[] {
                                                "อายุงานครบ 5 ปี",
                                                "ได้ จ.ช. มาแล้วไม่น้อยกว่า 5 ปี"
                                            });
                                            lbName.Attributes.Add("tuu2", "8");
                                        } else if (รหัสเครืองราชปัจจุบัน == 8) {
                                            ConditionExecute(new string[] {
                                                "ต.ม.","ต.ช."
                                            }, new bool[] {
                                                ปีที่ทำงาน >= 5,
                                                ปีที่ได้รับตม >= 5
                                        }, new string[] {
                                                "อายุงานครบ 5 ปี",
                                                "ได้ ต.ม. มาแล้วไม่น้อยกว่า 5 ปี"
                                            });
                                            lbName.Attributes.Add("tuu2", "7");
                                        } else if (รหัสเครืองราชปัจจุบัน == 7) {
                                            ConditionExecute(new string[] {
                                                "ต.ช.","ท.ม."
                                            }, new bool[] {
                                                ปีที่ทำงาน >= 5,
                                                ปีที่ได้รับตช >= 5
                                        }, new string[] {
                                                "อายุงานครบ 5 ปี",
                                                "ได้ ต.ช. มาแล้วไม่น้อยกว่า 5 ปี"
                                            });
                                            lbName.Attributes.Add("tuu2", "6");
                                        } else if (รหัสเครืองราชปัจจุบัน == 6) {
                                            ConditionExecute(new string[] { "ท.ม.", "" });
                                            lbName.Attributes.Add("tuu2", "0");
                                        }
                                    } else if (รหัสกลุ่มงานพนักงานราชการ == 19) { //5 กลุ่มงานเชี่ยวชาญพิเศษ ทั่วไป
                                        if (รหัสเครืองราชปัจจุบัน == -1) {
                                            ConditionExecute(new string[] {
                                                "","ต.ม."
                                            }, new bool[] {
                                                ปีที่ทำงาน >= 5
                                            }, new string[] {
                                                "อายุงานครบ 5 ปี"
                                            });
                                            lbName.Attributes.Add("tuu2", "8");
                                        } else if (รหัสเครืองราชปัจจุบัน == 8) {
                                            ConditionExecute(new string[] {
                                                "ต.ม.","ต.ช."
                                            }, new bool[] {
                                                ปีที่ทำงาน >= 5,
                                                ปีที่ได้รับตม >= 5
                                        }, new string[] {
                                                "อายุงานครบ 5 ปี",
                                                "ได้ ต.ม. มาแล้วไม่น้อยกว่า 5 ปี"
                                            });
                                            lbName.Attributes.Add("tuu2", "7");
                                        } else if (รหัสเครืองราชปัจจุบัน == 7) {
                                            ConditionExecute(new string[] {
                                                "ต.ช.","ท.ม."
                                            }, new bool[] {
                                                ปีที่ทำงาน >= 5,
                                                ปีที่ได้รับตช >= 5
                                        }, new string[] {
                                                "อายุงานครบ 5 ปี",
                                                "ได้ ต.ช. มาแล้วไม่น้อยกว่า 5 ปี"
                                            });
                                            lbName.Attributes.Add("tuu2", "6");
                                        } else if (รหัสเครืองราชปัจจุบัน == 6) {
                                            ConditionExecute(new string[] {
                                                "ท.ม.","ท.ช."
                                            }, new bool[] {
                                                ปีที่ทำงาน >= 5,
                                                ปีที่ได้รับทม >= 5
                                        }, new string[] {
                                                "อายุงานครบ 5 ปี",
                                                "ได้ ท.ม. มาแล้วไม่น้อยกว่า 5 ปี"
                                            });
                                            lbName.Attributes.Add("tuu2", "5");
                                        } else if (รหัสเครืองราชปัจจุบัน == 5) {
                                            ConditionExecute(new string[] { "ท.ช.", "" });
                                            lbName.Attributes.Add("tuu2", "0");
                                        }
                                    } else if (รหัสกลุ่มงานพนักงานราชการ == 20) { //6 กลุ่มงานเชี่ยวชาญพิเศษ ประเทศ
                                        if (รหัสเครืองราชปัจจุบัน == -1) {
                                            ConditionExecute(new string[] {
                                                "","ต.ช."
                                            }, new bool[] {
                                                ปีที่ทำงาน >= 5
                                            }, new string[] {
                                                "อายุงานครบ 5 ปี"
                                            });
                                            lbName.Attributes.Add("tuu2", "7");
                                        } else if (รหัสเครืองราชปัจจุบัน == 7) {
                                            ConditionExecute(new string[] {
                                                "ต.ช.","ท.ม."
                                            }, new bool[] {
                                                ปีที่ทำงาน >= 5,
                                                ปีที่ได้รับตช >= 5
                                        }, new string[] {
                                                "อายุงานครบ 5 ปี",
                                                "ได้ ต.ช. มาแล้วไม่น้อยกว่า 5 ปี"
                                            });
                                            lbName.Attributes.Add("tuu2", "6");
                                        } else if (รหัสเครืองราชปัจจุบัน == 6) {
                                            ConditionExecute(new string[] {
                                                "ท.ม.","ท.ช."
                                            }, new bool[] {
                                                ปีที่ทำงาน >= 5,
                                                ปีที่ได้รับทม >= 5
                                        }, new string[] {
                                                "อายุงานครบ 5 ปี",
                                                "ได้ ท.ม. มาแล้วไม่น้อยกว่า 5 ปี"
                                            });
                                            lbName.Attributes.Add("tuu2", "5");
                                        } else if (รหัสเครืองราชปัจจุบัน == 5) {
                                            ConditionExecute(new string[] {
                                                "ท.ช.","ป.ม."
                                            }, new bool[] {
                                                ปีที่ทำงาน >= 5,
                                                ปีที่ได้รับทช >= 3
                                        }, new string[] {
                                                "อายุงานครบ 5 ปี",
                                                "ได้ ท.ช. มาแล้วไม่น้อยกว่า 3 ปี"
                                            });
                                            lbName.Attributes.Add("tuu2", "4");
                                        } else if (รหัสเครืองราชปัจจุบัน == 4) {
                                            ConditionExecute(new string[] { "ป.ม.", "" });
                                            lbName.Attributes.Add("tuu2", "0");
                                        }
                                    } else if (รหัสกลุ่มงานพนักงานราชการ == 21) { //7 กลุ่มงานเชี่ยวชาญพิเศษ สากล
                                        if (รหัสเครืองราชปัจจุบัน == -1) {
                                            ConditionExecute(new string[] {
                                                "","ท.ม."
                                            }, new bool[] {
                                                ปีที่ทำงาน >= 5
                                            }, new string[] {
                                                "อายุงานครบ 5 ปี"
                                            });
                                            lbName.Attributes.Add("tuu2", "6");
                                        } else if (รหัสเครืองราชปัจจุบัน == 6) {
                                            ConditionExecute(new string[] {
                                                "ท.ม.","ท.ช."
                                            }, new bool[] {
                                                ปีที่ทำงาน >= 5,
                                                ปีที่ได้รับทม >= 5
                                        }, new string[] {
                                                "อายุงานครบ 5 ปี",
                                                "ได้ ท.ม. มาแล้วไม่น้อยกว่า 5 ปี"
                                            });
                                            lbName.Attributes.Add("tuu2", "5");
                                        } else if (รหัสเครืองราชปัจจุบัน == 5) {
                                            ConditionExecute(new string[] {
                                                "ท.ช.","ป.ม."
                                            }, new bool[] {
                                                ปีที่ทำงาน >= 5,
                                                ปีที่ได้รับทช >= 3
                                        }, new string[] {
                                                "อายุงานครบ 5 ปี",
                                                "ได้ ท.ช. มาแล้วไม่น้อยกว่า 3 ปี"
                                            });
                                            lbName.Attributes.Add("tuu2", "4");
                                        } else if (รหัสเครืองราชปัจจุบัน == 4) {
                                            ConditionExecute(new string[] {
                                                "ป.ม.","ป.ช."
                                            }, new bool[] {
                                                ปีที่ทำงาน >= 5,
                                                ปีที่ได้รับปม >= 3
                                        }, new string[] {
                                                "อายุงานครบ 5 ปี",
                                                "ได้ ป.ม. มาแล้วไม่น้อยกว่า 3 ปี"
                                            });
                                            lbName.Attributes.Add("tuu2", "3");
                                        } else if (รหัสเครืองราชปัจจุบัน == 3) {
                                            ConditionExecute(new string[] { "ป.ช.", "" });
                                            lbName.Attributes.Add("tuu2", "0");
                                        }
                                    }
                                } else if (รหัสประเภทบุคลากร == 3) {//ลูกจ้างประจำ
                                    if(เงินเดือนปัจจุบัน >= 8340 && เงินเดือนปัจจุบัน <= 15050) {
                                        if (รหัสเครืองราชปัจจุบัน == -1) {
                                            ConditionExecute(new string[] {
                                                "","บ.ม."
                                            }, new bool[] {
                                                ปีที่ทำงาน >= 6
                                            }, new string[] {
                                                "อายุงานครบ 6 ปี"
                                            });
                                            lbName.Attributes.Add("tuu2", "12");
                                        } else if (รหัสเครืองราชปัจจุบัน == 12) {
                                            ConditionExecute(new string[] {
                                                "บ.ม.","บ.ช."
                                            }, new bool[] {
                                                ปีที่ทำงาน >= 6,
                                                ปีที่ได้รับบม >= 5
                                        }, new string[] {
                                                "อายุงานครบ 6 ปี",
                                                "ได้ บ.ม. มาแล้วไม่น้อยกว่า 5 ปี"
                                            });
                                            lbName.Attributes.Add("tuu2", "11");
                                        } else if (รหัสเครืองราชปัจจุบัน == 11) {
                                            ConditionExecute(new string[] {
                                                "บ.ช.","จ.ม."
                                            }, new bool[] {
                                                ปีที่ทำงาน >= 6,
                                                ปีที่ได้รับบช >= 5
                                        }, new string[] {
                                                "อายุงานครบ 6 ปี",
                                                "ได้ บ.ช. มาแล้วไม่น้อยกว่า 5 ปี"
                                            });
                                            lbName.Attributes.Add("tuu2", "10");
                                        } else if (รหัสเครืองราชปัจจุบัน == 10) {
                                            ConditionExecute(new string[] { "จ.ม.", "" });
                                            lbName.Attributes.Add("tuu2", "0");
                                        }
                                    } else if (เงินเดือนปัจจุบัน >= 15050) {
                                        if (รหัสเครืองราชปัจจุบัน == -1) {
                                            ConditionExecute(new string[] {
                                                "","บ.ช."
                                            }, new bool[] {
                                                ปีที่ทำงาน >= 6
                                            }, new string[] {
                                                "อายุงานครบ 6 ปี"
                                            });
                                            lbName.Attributes.Add("tuu2", "11");
                                        } else if (รหัสเครืองราชปัจจุบัน == 11) {
                                            ConditionExecute(new string[] {
                                                "บ.ช.","จ.ม."
                                            }, new bool[] {
                                                ปีที่ทำงาน >= 6,
                                                ปีที่ได้รับบช >= 5
                                        }, new string[] {
                                                "อายุงานครบ 6 ปี",
                                                "ได้ บ.ช. มาแล้วไม่น้อยกว่า 5 ปี"
                                            });
                                            lbName.Attributes.Add("tuu2", "10");
                                        } else if (รหัสเครืองราชปัจจุบัน == 10) {
                                            ConditionExecute(new string[] {
                                                "จ.ม.","จ.ช."
                                            }, new bool[] {
                                                ปีที่ทำงาน >= 6,
                                                ปีที่ได้รับจม >= 5
                                        }, new string[] {
                                                "อายุงานครบ 6 ปี",
                                                "ได้ จ.ม. มาแล้วไม่น้อยกว่า 5 ปี",
                                            });
                                            lbName.Attributes.Add("tuu2", "9");
                                        } else if (รหัสเครืองราชปัจจุบัน == 9) {
                                            ConditionExecute(new string[] { "จ.ช.", "" });
                                            lbName.Attributes.Add("tuu2", "0");
                                        }
                                    }
                                }
                                //Error
                                int insID = int.Parse(lbName.Attributes["tuu2"]);
                                if(insID == -1) {
                                    OK = false;
                                } else {
                                    using (OracleCommand com2 = new OracleCommand("SELECT COUNT(*) FROM TB_INSIG_REQUEST WHERE IR_STATUS IN(1,2) AND IR_CITIZEN_ID = '" + psID + "'", con)) {
                                        using (OracleDataReader reader2 = com2.ExecuteReader()) {
                                            while (reader2.Read()) {
                                                if (reader2.GetInt32(0) != 0) {
                                                    OK = false;
                                                }
                                            }

                                        }
                                    }
                                }
                                


                                if (OK) {
                                    Table1.Rows.Add(row);
                                }

                                if(!OK2) {
                                    row.Cells[0].Controls.Clear();
                                }

                            }
                        }
                    }

                }

            }
        }
        private void ConditionExecute(string[] ins) {
            if (ins[0] == "") {
                img1.Visible = false;
                lb1.Text = "ไม่เคยได้รับ";
            } else {
                img1.Attributes["src"] = "Image/Insignia/" + ins[0] + ".png";
                lb1.Text = ins[0];
            }
            if (ins[1] == "") {
                img2.Visible = false;
                lb2.Text = "-";
            } else {
                img2.Attributes["src"] = "Image/Insignia/" + ins[1] + ".png";
                lb2.Text = ins[1];
            }
        }
        private void ConditionExecute(string[] ins, bool[] b, string[] s) {
            if(ins[0] == "") {
                img1.Visible = false;
                lb1.Text = "ไม่เคยได้รับ";
            } else {
                img1.Attributes["src"] = "Image/Insignia/" + ins[0] + ".png";
                lb1.Text = ins[0];
            }
            if (ins[1] == "") {
                img2.Visible = false;
                lb2.Text = "-";
            } else {
                img2.Attributes["src"] = "Image/Insignia/" + ins[1] + ".png";
                lb2.Text = ins[1];
            }

            int size = b.Length;
            int get = 0;
            for (int i = 0; i < size; i++) {
                get += Convert.ToInt32(b[i]);
                ConditionLabel(pCondition, s[i], b[i]);
            }
            ConditionBar(pBar, get, size);
            if(get != size) {
                OK2 = false;
            }
        }
        private void ConditionLabel(Panel p, string word, bool b) {
            if (b) {
                Panel pp = new Panel();
                Label lb = new Label();
                lb.Text = "<img src='Image/Small/correct.png' class='icon_left'/>" + word;
                pp.Controls.Add(lb);
                p.Controls.Add(pp);
            } else {
                Panel pp = new Panel();
                Label lb = new Label();
                lb.Text = "<img src='Image/Small/delete.png' class='icon_left'/>" + word;
                pp.Controls.Add(lb);
                p.Controls.Add(pp);
                if (hf1.Value == "1") {
                    OK = false;
                }
                 
            }
        }
        private void ConditionBar(Panel p, int get, int max) {

            int width = 200;
            int height = 10;
            double widthON = (double)width * ((double)get / (double)max);
            double widthOFF = (double)width * (((double)max - (double)get) / (double)max);


            Panel bar = new Panel();
            Panel barON = new Panel();
            Panel barOFF = new Panel();
            barON.Style.Add("width", widthON + "px");
            barON.Style.Add("height", height + "px");
            barON.Style.Add("display", "inline-block");
            barON.Style.Add("background", "linear-gradient(to bottom, #22ff00, #1ac600)");
            barON.Style.Add("border-top-left-radius", height + "px");
            barON.Style.Add("border-bottom-left-radius", height + "px");

            barOFF.Style.Add("width", widthOFF + "px");
            barOFF.Style.Add("height", height + "px");
            barOFF.Style.Add("display", "inline-block");
            barOFF.Style.Add("background", "linear-gradient(to bottom, #808080, #636363)");
            barOFF.Style.Add("border-top-right-radius", height + "px");
            barOFF.Style.Add("border-bottom-right-radius", height + "px");

            if (get == 0) {
                barOFF.Style.Add("border-top-left-radius", height + "px");
                barOFF.Style.Add("border-bottom-left-radius", height + "px");
            }
            if (get == max) {
                barON.Style.Add("border-top-right-radius", height + "px");
                barON.Style.Add("border-bottom-right-radius", height + "px");
            }

            bar.Controls.Add(barON);
            bar.Controls.Add(barOFF);
            p.Controls.Add(bar);
        }

        protected void lbuSearch_Click(object sender, EventArgs e) {
            hf1.Value = "1";
            Page_Load(sender, e);
        }

        protected void lbuSend_Click(object sender, EventArgs e) {
            
            for (int i = 1; i < Table1.Rows.Count; ++i)
            {
                CheckBox cb = (CheckBox)Table1.Rows[i].Cells[0].Controls[0];
                LinkButton lbu = (LinkButton)Table1.Rows[i].Cells[1].Controls[0];
                if (cb != null && cb.Checked)
                {
                    int id = 0;
                    using (OracleConnection conn = Util.OC())
                    {
                        PersonnelSystem ps = PersonnelSystem.GetPersonnelSystem(this);
                        Person loginPerson = ps.LoginPerson;
                        using (OracleCommand command = new OracleCommand("INSERT INTO TB_INSIG_REQUEST (IR_STATUS,IR_CITIZEN_ID,IR_INSIG_ID) VALUES (:IR_STATUS,:IR_CITIZEN_ID,:IR_INSIG_ID) ", conn))
                        {

                            try
                            {
                                if (conn.State != ConnectionState.Open)
                                {
                                    conn.Open();
                                }
                                command.Parameters.Add(new OracleParameter("IR_STATUS", 1));
                                command.Parameters.Add(new OracleParameter("IR_CITIZEN_ID", lbu.Attributes["tuu"]));
                                command.Parameters.Add(new OracleParameter("IR_INSIG_ID", lbu.Attributes["tuu2"]));
                                id = command.ExecuteNonQuery();
                            }

                            catch (Exception ex)
                            {
                                throw ex;
                            }
                            finally
                            {
                                command.Dispose();
                                conn.Close();
                            }
                        }
                    }
                } 
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('ส่งรายชื่อเรียบร้อยแล้ว !')", true);
        }

        protected void lbuSearchAll_Click(object sender, EventArgs e) {
            hf1.Value = "2";
            Page_Load(sender, e);
        }
    }
}