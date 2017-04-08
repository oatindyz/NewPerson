using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using WEB_PERSONAL.Class;
using System.Data.OracleClient;

namespace WEB_PERSONAL
{
    public partial class DataManage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindTitle();
            }
             
            if (Request.QueryString["ID"] == "Title") { BindTitle(); }
            else if (Request.QueryString["ID"] == "Gender") { BindGender(); }
            else if (Request.QueryString["ID"] == "Province") { BindProvince(); }
            else if (Request.QueryString["ID"] == "Amphur") { BindAmphur(); }
            else if (Request.QueryString["ID"] == "Tambon") { BindTambon(); }
            else if (Request.QueryString["ID"] == "Nation") { BindNation(); }
            else if (Request.QueryString["ID"] == "Campus") { BindCampus(); }
            else if (Request.QueryString["ID"] == "Faculty") { BindFaculty(); }
            else if (Request.QueryString["ID"] == "Division") { BindDivision(); }
            else if (Request.QueryString["ID"] == "WorkDivision") { BindWorkDivision(); }
            else if (Request.QueryString["ID"] == "Stafftype") { BindStafftype(); }
            else if (Request.QueryString["ID"] == "TimeContact") { BindTimeContact(); }
            else if (Request.QueryString["ID"] == "Budget") { BindBudget(); }
            else if (Request.QueryString["ID"] == "SubStafftype") { BindSubStafftype(); }
            else if (Request.QueryString["ID"] == "AdminPosition") { BindAdminPosition(); }
            else if (Request.QueryString["ID"] == "Position") { BindPosition(); }
            else if (Request.QueryString["ID"] == "WorkPos") { BindWorkPos(); }
            else if (Request.QueryString["ID"] == "TeachISCED") { BindTeachISCED(); }
            else if (Request.QueryString["ID"] == "GradLev") { BindGradLev(); }
            else if (Request.QueryString["ID"] == "GradProg") { BindGradProg(); }
            else if (Request.QueryString["ID"] == "Deform") { BindDeform(); }
            else if (Request.QueryString["ID"] == "Religion") { BindReligion(); }
            else if (Request.QueryString["ID"] == "MovementType") { BindMovementType(); }
        }


        private void HideAll() {
            Panel1.Visible = false;
            Panel2.Visible = false;
            Panel3.Visible = false;
            Panel4.Visible = false;
            Panel5.Visible = false;
            Panel6.Visible = false;
            Panel7.Visible = false;
            Panel8.Visible = false;
            Panel9.Visible = false;
            Panel10.Visible = false;
            Panel11.Visible = false;
            Panel12.Visible = false;
            Panel13.Visible = false;
            Panel14.Visible = false;
            Panel15.Visible = false;
            Panel16.Visible = false;
            Panel17.Visible = false;
            Panel18.Visible = false;
            Panel19.Visible = false;
            Panel20.Visible = false;
            Panel21.Visible = false;
            Panel22.Visible = false;
            Panel23.Visible = false;
        }

        //
        protected void BindTitle()
        {
            HideAll();
            Panel1.Visible = true;
            OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING);
            OracleDataAdapter sda = new OracleDataAdapter("SELECT TITLE_ID,TITLE_NAME_TH FROM TB_TITLENAME ORDER BY ABS(TITLE_ID) ASC", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            myRepeaterTitle.DataSource = dt;
            myRepeaterTitle.DataBind();
        }
        protected void ClearTitle()
        {
            tbInsertIdTitle.Text = "";
            tbInsertNameTitle.Text = "";
        }
        protected void lbuMenuTitle_Click(object sender, EventArgs e)
        {
            BindTitle();
        }
        protected void btnInsertTitle_Click(object sender, EventArgs e)
        {
            string oldID = DatabaseManager.ExecuteString("SELECT TITLE_ID FROM TB_TITLENAME WHERE TITLE_ID ='" + tbInsertIdTitle.Text + "'");
            if (tbInsertIdTitle.Text == oldID)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('มีรหัสคำนำหน้า " + tbInsertIdTitle.Text + " อยู่แล้วในระบบ ไม่สามารถเพิ่มได้')", true);
                return;
            }

            DatabaseManager.ExecuteNonQuery("INSERT INTO TB_TITLENAME (TITLE_ID,TITLE_NAME_TH) VALUES (" + tbInsertIdTitle.Text + "," + tbInsertNameTitle.Text + ")");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('เพิ่มข้อมูลเรียบร้อย')", true);
            BindTitle();
            ClearTitle();
        }
        protected void btnUpdateTitle_Click(object sender, EventArgs e)
        {
            string ValueID = tbInsertIdTitle.Text;
            string ValueName = tbInsertNameTitle.Text;

            if (Session["DefaultIdTitle"] == null)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('กรุณาเลือกรายการที่จะแก้ไขก่อน')", true);
                return;
            }

            if (ValueID != "" && ValueName != "")
            {
                DatabaseManager.ExecuteNonQuery("UPDATE TB_TITLENAME SET TITLE_ID = '" + ValueID + "', TITLE_NAME_TH = '" + ValueName + "' WHERE TITLE_ID = '" + Session["DefaultIdTitle"].ToString() + "'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('อัพเดทข้อมูลเรียบร้อย')", true);
                BindTitle();
                ClearTitle();
                Session.Remove("DefaultIdTitle");
            }
        }
        protected void lbuClearTitle_Click(object sender, EventArgs e)
        {
            BindTitle();
            ClearTitle();
            Session.Remove("DefaultIdTitle");
        }
        protected void OnEditTitle(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            string ValueID = (item.FindControl("lbTitleID") as Label).Text;
            string ValueName = (item.FindControl("lbTitleName") as Label).Text;

            tbInsertIdTitle.Text = ValueID;
            tbInsertNameTitle.Text = ValueName;

            Session["DefaultIdTitle"] = ValueID;
        }
        protected void OnDeleteTitle(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            int ValueID = int.Parse((item.FindControl("lbTitleID") as Label).Text);

            if (ValueID != 0)
            {
                DatabaseManager.ExecuteNonQuery("DELETE TB_TITLENAME WHERE TITLE_ID = '" + ValueID + "'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('ลบข้อมูลเรียบร้อย')", true);
                BindTitle();
            }
        }

        //
        protected void BindGender()
        {
            HideAll();
            Panel2.Visible = true;
            OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING);
            OracleDataAdapter sda = new OracleDataAdapter("SELECT GENDER_ID,GENDER_NAME FROM TB_GENDER ORDER BY ABS(GENDER_ID) ASC", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            myRepeaterGender.DataSource = dt;
            myRepeaterGender.DataBind();
        }
        protected void ClearGender()
        {
            tbInsertIdGender.Text = "";
            tbInsertNameGender.Text = "";
        }
        protected void lbuMenuGender_Click(object sender, EventArgs e)
        {
            BindGender();
        }
        protected void btnInsertGender_Click(object sender, EventArgs e)
        {
            string oldID = DatabaseManager.ExecuteString("SELECT GENDER_ID FROM TB_GENDER WHERE GENDER_ID ='" + tbInsertIdGender.Text + "'");
            if (tbInsertIdGender.Text == oldID)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('มีรหัสเพศ " + tbInsertIdGender.Text + " อยู่แล้วในระบบ ไม่สามารถเพิ่มได้')", true);
                return;
            }

            DatabaseManager.ExecuteNonQuery("INSERT INTO TB_GENDER (GENDER_ID,GENDER_NAME) VALUES (" + tbInsertIdGender.Text + "," + tbInsertNameGender.Text + ")");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('เพิ่มข้อมูลเรียบร้อย')", true);
            BindGender();
            ClearGender();
        }
        protected void btnUpdateGender_Click(object sender, EventArgs e)
        {
            string ValueID = tbInsertIdGender.Text;
            string ValueName = tbInsertNameGender.Text;

            if (Session["DefaultIdGender"] == null)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('กรุณาเลือกรายการที่จะแก้ไขก่อน')", true);
                return;
            }

            if (ValueID != "" && ValueName != "")
            {
                DatabaseManager.ExecuteNonQuery("UPDATE TB_GENDER SET GENDER_ID = '" + ValueID + "', GENDER_NAME = '" + ValueName + "' WHERE GENDER_ID = '" + Session["DefaultIdGender"].ToString() + "'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('อัพเดทข้อมูลเรียบร้อย')", true);
                BindGender();
                ClearGender();
                Session.Remove("DefaultIdGender");
            }
        }
        protected void lbuClearGender_Click(object sender, EventArgs e)
        {
            BindGender();
            ClearGender();
            Session.Remove("DefaultIdGender");
        }
        protected void OnEditGender(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            string ValueID = (item.FindControl("lbGenderID") as Label).Text;
            string ValueName = (item.FindControl("lbGenderName") as Label).Text;

            tbInsertIdGender.Text = ValueID;
            tbInsertNameGender.Text = ValueName;

            Session["DefaultIdGender"] = ValueID;
        }
        protected void OnDeleteGender(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            int ValueID = int.Parse((item.FindControl("lbGenderID") as Label).Text);

            if (ValueID != 0)
            {
                DatabaseManager.ExecuteNonQuery("DELETE TB_GENDER WHERE GENDER_ID = '" + ValueID + "'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('ลบข้อมูลเรียบร้อย')", true);
                BindGender();
            }
        }

        //
        protected void BindProvince()
        {
            HideAll();
            Panel3.Visible = true;
            OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING);
            OracleDataAdapter sda = new OracleDataAdapter("SELECT PROVINCE_ID,PROVINCE_TH FROM TB_PROVINCE ORDER BY ABS(PROVINCE_ID) ASC", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            myRepeaterProvince.DataSource = dt;
            myRepeaterProvince.DataBind();
        }
        protected void ClearProvince()
        {
            tbInsertIdProvince.Text = "";
            tbInsertNameProvince.Text = "";
        }
        protected void lbuMenuProvince_Click(object sender, EventArgs e)
        {
            BindProvince();
        }
        protected void btnInsertProvince_Click(object sender, EventArgs e)
        {
            string oldID = DatabaseManager.ExecuteString("SELECT PROVINCE_ID FROM TB_PROVINCE WHERE PROVINCE_ID ='" + tbInsertIdProvince.Text + "'");
            if (tbInsertIdProvince.Text == oldID)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('มีรหัสจังหวัด " + tbInsertIdProvince.Text + " อยู่แล้วในระบบ ไม่สามารถเพิ่มได้')", true);
                return;
            }

            DatabaseManager.ExecuteNonQuery("INSERT INTO TB_PROVINCE (PROVINCE_ID,PROVINCE_TH) VALUES (" + tbInsertIdProvince.Text + "," + tbInsertNameProvince.Text + ")");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('เพิ่มข้อมูลเรียบร้อย')", true);
            BindProvince();
            ClearProvince();
        }
        protected void btnUpdateProvince_Click(object sender, EventArgs e)
        {
            string ValueID = tbInsertIdProvince.Text;
            string ValueName = tbInsertNameProvince.Text;

            if (Session["DefaultIdProvince"] == null)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('กรุณาเลือกรายการที่จะแก้ไขก่อน')", true);
                return;
            }

            if (ValueID != "" && ValueName != "")
            {
                DatabaseManager.ExecuteNonQuery("UPDATE TB_PROVINCE SET PROVINCE_ID = '" + ValueID + "', PROVINCE_NAME_TH = '" + ValueName + "' WHERE PROVINCE_ID = '" + Session["DefaultIdProvince"].ToString() + "'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('อัพเดทข้อมูลเรียบร้อย')", true);
                BindProvince();
                ClearProvince();
                Session.Remove("DefaultIdProvince");
            }
        }
        protected void lbuClearProvince_Click(object sender, EventArgs e)
        {
            BindProvince();
            ClearProvince();
            Session.Remove("DefaultIdProvince");
        }
        protected void OnEditProvince(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            string ValueID = (item.FindControl("lbProvinceID") as Label).Text;
            string ValueName = (item.FindControl("lbProvinceName") as Label).Text;

            tbInsertIdProvince.Text = ValueID;
            tbInsertNameProvince.Text = ValueName;

            Session["DefaultIdProvince"] = ValueID;
        }
        protected void OnDeleteProvince(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            int ValueID = int.Parse((item.FindControl("lbProvinceID") as Label).Text);

            if (ValueID != 0)
            {
                DatabaseManager.ExecuteNonQuery("DELETE TB_PROVINCE WHERE PROVINCE_ID = '" + ValueID + "'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('ลบข้อมูลเรียบร้อย')", true);
                BindProvince();
            }
        }

        //
        protected void BindAmphur()
        {
            HideAll();
            Panel4.Visible = true;
            OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING);
            OracleDataAdapter sda = new OracleDataAdapter("SELECT AMPHUR_ID,AMPHUR_TH FROM TB_AMPHUR ORDER BY ABS(AMPHUR_ID) ASC", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            myRepeaterAmphur.DataSource = dt;
            myRepeaterAmphur.DataBind();
        }
        protected void ClearAmphur()
        {
            tbInsertIdAmphur.Text = "";
            tbInsertNameAmphur.Text = "";
        }
        protected void lbuMenuAmphur_Click(object sender, EventArgs e)
        {
            BindAmphur();
        }
        protected void btnInsertAmphur_Click(object sender, EventArgs e)
        {
            string oldID = DatabaseManager.ExecuteString("SELECT AMPHUR_ID FROM TB_AMPHUR WHERE AMPHUR_ID ='" + tbInsertIdAmphur.Text + "'");
            if (tbInsertIdAmphur.Text == oldID)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('มีรหัสเขต/อำเภอ " + tbInsertIdAmphur.Text + " อยู่แล้วในระบบ ไม่สามารถเพิ่มได้')", true);
                return;
            }

            DatabaseManager.ExecuteNonQuery("INSERT INTO TB_AMPHUR (AMPHUR_ID,AMPHUR_TH) VALUES (" + tbInsertIdAmphur.Text + "," + tbInsertNameAmphur.Text + ")");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('เพิ่มข้อมูลเรียบร้อย')", true);
            BindAmphur();
            ClearAmphur();
        }
        protected void btnUpdateAmphur_Click(object sender, EventArgs e)
        {
            string ValueID = tbInsertIdAmphur.Text;
            string ValueName = tbInsertNameAmphur.Text;

            if (Session["DefaultIdAmphur"] == null)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('กรุณาเลือกรายการที่จะแก้ไขก่อน')", true);
                return;
            }

            if (ValueID != "" && ValueName != "")
            {
                DatabaseManager.ExecuteNonQuery("UPDATE TB_AMPHUR SET AMPHUR_ID = '" + ValueID + "', AMPHUR_TH = '" + ValueName + "' WHERE AMPHUR_ID = '" + Session["DefaultIdAmphur"].ToString() + "'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('อัพเดทข้อมูลเรียบร้อย')", true);
                BindAmphur();
                ClearAmphur();
                Session.Remove("DefaultIdAmphur");
            }
        }
        protected void lbuClearAmphur_Click(object sender, EventArgs e)
        {
            BindAmphur();
            ClearAmphur();
            Session.Remove("DefaultIdAmphur");
        }
        protected void OnEditAmphur(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            string ValueID = (item.FindControl("lbAmphurID") as Label).Text;
            string ValueName = (item.FindControl("lbAmphurName") as Label).Text;

            tbInsertIdAmphur.Text = ValueID;
            tbInsertNameAmphur.Text = ValueName;

            Session["DefaultIdAmphur"] = ValueID;
        }
        protected void OnDeleteAmphur(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            int ValueID = int.Parse((item.FindControl("lbAmphurID") as Label).Text);

            if (ValueID != 0)
            {
                DatabaseManager.ExecuteNonQuery("DELETE TB_AMPHUR WHERE AMPHUR_ID = '" + ValueID + "'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('ลบข้อมูลเรียบร้อย')", true);
                BindAmphur();
            }
        }

        //
        protected void BindTambon()
        {
            HideAll();
            Panel5.Visible = true;
            OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING);
            OracleDataAdapter sda = new OracleDataAdapter("SELECT DISTRICT_ID,DISTRICT_TH FROM TB_DISTRICT ORDER BY ABS(DISTRICT_ID) ASC", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            myRepeaterTambon.DataSource = dt;
            myRepeaterTambon.DataBind();
        }
        protected void ClearTambon()
        {
            tbInsertIdTambon.Text = "";
            tbInsertNameTambon.Text = "";
        }
        protected void lbuMenuTambon_Click(object sender, EventArgs e)
        {
            BindTambon();
        }
        protected void btnInsertTambon_Click(object sender, EventArgs e)
        {
            string oldID = DatabaseManager.ExecuteString("SELECT DISTRICT_ID FROM TB_DISTRICT WHERE DISTRICT_ID ='" + tbInsertIdTambon.Text + "'");
            if (tbInsertIdTambon.Text == oldID)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('มีรหัสแขวง/ตำบล " + tbInsertIdTambon.Text + " อยู่แล้วในระบบ ไม่สามารถเพิ่มได้')", true);
                return;
            }

            DatabaseManager.ExecuteNonQuery("INSERT INTO TB_DISTRICT (DISTRICT_ID,DISTRICT_TH) VALUES (" + tbInsertIdTambon.Text + "," + tbInsertNameTambon.Text + ")");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('เพิ่มข้อมูลเรียบร้อย')", true);
            BindTambon();
            ClearTambon();
        }
        protected void btnUpdateTambon_Click(object sender, EventArgs e)
        {
            string ValueID = tbInsertIdTambon.Text;
            string ValueName = tbInsertNameTambon.Text;

            if (Session["DefaultIdTambon"] == null)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('กรุณาเลือกรายการที่จะแก้ไขก่อน')", true);
                return;
            }

            if (ValueID != "" && ValueName != "")
            {
                DatabaseManager.ExecuteNonQuery("UPDATE TB_DISTRICT SET DISTRICT_ID = '" + ValueID + "', DISTRICT_TH = '" + ValueName + "' WHERE DISTRICT_ID = '" + Session["DefaultIdTambon"].ToString() + "'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('อัพเดทข้อมูลเรียบร้อย')", true);
                BindTambon();
                ClearTambon();
                Session.Remove("DefaultIdTambon");
            }
        }
        protected void lbuClearTambon_Click(object sender, EventArgs e)
        {
            BindTambon();
            ClearTambon();
            Session.Remove("DefaultIdTambon");
        }
        protected void OnEditTambon(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            string ValueID = (item.FindControl("lbTambonID") as Label).Text;
            string ValueName = (item.FindControl("lbTambonName") as Label).Text;

            tbInsertIdTambon.Text = ValueID;
            tbInsertNameTambon.Text = ValueName;

            Session["DefaultIdTambon"] = ValueID;
        }
        protected void OnDeleteTambon(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            int ValueID = int.Parse((item.FindControl("lbTambonID") as Label).Text);

            if (ValueID != 0)
            {
                DatabaseManager.ExecuteNonQuery("DELETE TB_DISTRICT WHERE DISTRICT_ID = '" + ValueID + "'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('ลบข้อมูลเรียบร้อย')", true);
                BindTambon();
            }
        }

        //
        protected void BindNation()
        {
            HideAll();
            Panel6.Visible = true;
            OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING);
            OracleDataAdapter sda = new OracleDataAdapter("SELECT NATION_ID,NATION_NAME_EN FROM TB_NATION ORDER BY ABS(NATION_ID) ASC", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            myRepeaterNation.DataSource = dt;
            myRepeaterNation.DataBind();
        }
        protected void ClearNation()
        {
            tbInsertIdNation.Text = "";
            tbInsertNameNation.Text = "";
        }
        protected void lbuMenuNation_Click(object sender, EventArgs e)
        {
            BindNation();
        }
        protected void btnInsertNation_Click(object sender, EventArgs e)
        {
            string oldID = DatabaseManager.ExecuteString("SELECT NATION_ID FROM TB_NATION WHERE NATION_ID ='" + tbInsertIdNation.Text + "'");
            if (tbInsertIdNation.Text == oldID)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('มีรหัสสัญชาติ " + tbInsertIdNation.Text + " อยู่แล้วในระบบ ไม่สามารถเพิ่มได้')", true);
                return;
            }

            DatabaseManager.ExecuteNonQuery("INSERT INTO TB_NATION (NATION_ID,NATION_NAME_EN) VALUES (" + tbInsertIdNation.Text + "," + tbInsertNameNation.Text + ")");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('เพิ่มข้อมูลเรียบร้อย')", true);
            BindNation();
            ClearNation();
        }
        protected void btnUpdateNation_Click(object sender, EventArgs e)
        {
            string ValueID = tbInsertIdNation.Text;
            string ValueName = tbInsertNameNation.Text;

            if (Session["DefaultIdNation"] == null)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('กรุณาเลือกรายการที่จะแก้ไขก่อน')", true);
                return;
            }

            if (ValueID != "" && ValueName != "")
            {
                DatabaseManager.ExecuteNonQuery("UPDATE TB_NATION SET NATION_ID = '" + ValueID + "', NATION_NAME_EN = '" + ValueName + "' WHERE NATION_ID = '" + Session["DefaultIdNation"].ToString() + "'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('อัพเดทข้อมูลเรียบร้อย')", true);
                BindNation();
                ClearNation();
                Session.Remove("DefaultIdNation");
            }
        }
        protected void lbuClearNation_Click(object sender, EventArgs e)
        {
            BindNation();
            ClearNation();
            Session.Remove("DefaultIdNation");
        }
        protected void OnEditNation(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            string ValueID = (item.FindControl("lbNationID") as Label).Text;
            string ValueName = (item.FindControl("lbNationName") as Label).Text;

            tbInsertIdNation.Text = ValueID;
            tbInsertNameNation.Text = ValueName;

            Session["DefaultIdNation"] = ValueID;
        }
        protected void OnDeleteNation(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            int ValueID = int.Parse((item.FindControl("lbNationID") as Label).Text);

            if (ValueID != 0)
            {
                DatabaseManager.ExecuteNonQuery("DELETE TB_NATION WHERE NATION_ID = '" + ValueID + "'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('ลบข้อมูลเรียบร้อย')", true);
                BindNation();
            }
        }

        //
        protected void BindCampus()
        {
            HideAll();
            Panel7.Visible = true;
            OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING);
            OracleDataAdapter sda = new OracleDataAdapter("SELECT CAMPUS_ID,CAMPUS_NAME FROM TB_CAMPUS ORDER BY ABS(CAMPUS_ID) ASC", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            myRepeaterCampus.DataSource = dt;
            myRepeaterCampus.DataBind();
        }
        protected void ClearCampus()
        {
            tbInsertIdCampus.Text = "";
            tbInsertNameCampus.Text = "";
        }
        protected void lbuMenuCampus_Click(object sender, EventArgs e)
        {
            BindCampus();
        }
        protected void btnInsertCampus_Click(object sender, EventArgs e)
        {
            string oldID = DatabaseManager.ExecuteString("SELECT CAMPUS_ID FROM TB_CAMPUS WHERE CAMPUS_ID ='" + tbInsertIdCampus.Text + "'");
            if (tbInsertIdCampus.Text == oldID)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('มีรหัสวิทยาเขต " + tbInsertIdCampus.Text + " อยู่แล้วในระบบ ไม่สามารถเพิ่มได้')", true);
                return;
            }

            DatabaseManager.ExecuteNonQuery("INSERT INTO TB_CAMPUS (CAMPUS_ID,CAMPUS_NAME) VALUES (" + tbInsertIdCampus.Text + "," + tbInsertNameCampus.Text + ")");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('เพิ่มข้อมูลเรียบร้อย')", true);
            BindCampus();
            ClearCampus();
        }
        protected void btnUpdateCampus_Click(object sender, EventArgs e)
        {
            string ValueID = tbInsertIdCampus.Text;
            string ValueName = tbInsertNameCampus.Text;

            if (Session["DefaultIdCampus"] == null)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('กรุณาเลือกรายการที่จะแก้ไขก่อน')", true);
                return;
            }

            if (ValueID != "" && ValueName != "")
            {
                DatabaseManager.ExecuteNonQuery("UPDATE TB_CAMPUS SET CAMPUS_ID = '" + ValueID + "', CAMPUS_NAME = '" + ValueName + "' WHERE CAMPUS_ID = '" + Session["DefaultIdCampus"].ToString() + "'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('อัพเดทข้อมูลเรียบร้อย')", true);
                BindCampus();
                ClearCampus();
                Session.Remove("DefaultIdCampus");
            }
        }
        protected void lbuClearCampus_Click(object sender, EventArgs e)
        {
            BindCampus();
            ClearCampus();
            Session.Remove("DefaultIdCampus");
        }
        protected void OnEditCampus(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            string ValueID = (item.FindControl("lbCampusID") as Label).Text;
            string ValueName = (item.FindControl("lbCampusName") as Label).Text;

            tbInsertIdCampus.Text = ValueID;
            tbInsertNameCampus.Text = ValueName;

            Session["DefaultIdCampus"] = ValueID;
        }
        protected void OnDeleteCampus(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            int ValueID = int.Parse((item.FindControl("lbCampusID") as Label).Text);

            if (ValueID != 0)
            {
                DatabaseManager.ExecuteNonQuery("DELETE TB_CAMPUS WHERE CAMPUS_ID = '" + ValueID + "'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('ลบข้อมูลเรียบร้อย')", true);
                BindCampus();
            }
        }

        //
        protected void BindFaculty()
        {
            HideAll();
            Panel8.Visible = true;
            OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING);
            OracleDataAdapter sda = new OracleDataAdapter("SELECT FACULTY_ID,FACULTY_NAME FROM TB_FACULTY ORDER BY ABS(FACULTY_ID) ASC", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            myRepeaterFaculty.DataSource = dt;
            myRepeaterFaculty.DataBind();
        }
        protected void ClearFaculty()
        {
            tbInsertIdFaculty.Text = "";
            tbInsertNameFaculty.Text = "";
        }
        protected void lbuMenuFaculty_Click(object sender, EventArgs e)
        {
            BindFaculty();
        }
        protected void btnInsertFaculty_Click(object sender, EventArgs e)
        {
            string oldID = DatabaseManager.ExecuteString("SELECT FACULTY_ID FROM TB_FACULTY WHERE FACULTY_ID ='" + tbInsertIdFaculty.Text + "'");
            if (tbInsertIdFaculty.Text == oldID)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('มีรหัสสำนัก/สถาบัน/คณะ " + tbInsertIdFaculty.Text + " อยู่แล้วในระบบ ไม่สามารถเพิ่มได้')", true);
                return;
            }

            DatabaseManager.ExecuteNonQuery("INSERT INTO TB_FACULTY (FACULTY_ID,FACULTY_NAME) VALUES (" + tbInsertIdFaculty.Text + "," + tbInsertNameFaculty.Text + ")");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('เพิ่มข้อมูลเรียบร้อย')", true);
            BindFaculty();
            ClearFaculty();
        }
        protected void btnUpdateFaculty_Click(object sender, EventArgs e)
        {
            string ValueID = tbInsertIdFaculty.Text;
            string ValueName = tbInsertNameFaculty.Text;

            if (Session["DefaultIdFaculty"] == null)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('กรุณาเลือกรายการที่จะแก้ไขก่อน')", true);
                return;
            }

            if (ValueID != "" && ValueName != "")
            {
                DatabaseManager.ExecuteNonQuery("UPDATE TB_FACULTY SET FACULTY_ID = '" + ValueID + "', FACULTY_NAME = '" + ValueName + "' WHERE FACULTY_ID = '" + Session["DefaultIdFaculty"].ToString() + "'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('อัพเดทข้อมูลเรียบร้อย')", true);
                BindFaculty();
                ClearFaculty();
                Session.Remove("DefaultIdFaculty");
            }
        }
        protected void lbuClearFaculty_Click(object sender, EventArgs e)
        {
            BindFaculty();
            ClearFaculty();
            Session.Remove("DefaultIdFaculty");
        }
        protected void OnEditFaculty(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            string ValueID = (item.FindControl("lbFacultyID") as Label).Text;
            string ValueName = (item.FindControl("lbFacultyName") as Label).Text;

            tbInsertIdFaculty.Text = ValueID;
            tbInsertNameFaculty.Text = ValueName;

            Session["DefaultIdFaculty"] = ValueID;
        }
        protected void OnDeleteFaculty(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            int ValueID = int.Parse((item.FindControl("lbFacultyID") as Label).Text);

            if (ValueID != 0)
            {
                DatabaseManager.ExecuteNonQuery("DELETE TB_FACULTY WHERE FACULTY_ID = '" + ValueID + "'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('ลบข้อมูลเรียบร้อย')", true);
                BindFaculty();
            }
        }

        //
        protected void BindDivision()
        {
            HideAll();
            Panel9.Visible = true;
            OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING);
            OracleDataAdapter sda = new OracleDataAdapter("SELECT DIVISION_ID,DIVISION_NAME FROM TB_DIVISION ORDER BY ABS(DIVISION_ID) ASC", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            myRepeaterDivision.DataSource = dt;
            myRepeaterDivision.DataBind();
        }
        protected void ClearDivision()
        {
            tbInsertIdDivision.Text = "";
            tbInsertNameDivision.Text = "";
        }
        protected void lbuMenuDivision_Click(object sender, EventArgs e)
        {
            BindDivision();
        }
        protected void btnInsertDivision_Click(object sender, EventArgs e)
        {
            string oldID = DatabaseManager.ExecuteString("SELECT DIVISION_ID FROM TB_DIVISION WHERE DIVISION_ID ='" + tbInsertIdDivision.Text + "'");
            if (tbInsertIdDivision.Text == oldID)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('มีรหัสกอง/สำนักงานเลขา/ภาควิชา " + tbInsertIdDivision.Text + " อยู่แล้วในระบบ ไม่สามารถเพิ่มได้')", true);
                return;
            }

            DatabaseManager.ExecuteNonQuery("INSERT INTO TB_DIVISION (DIVISION_ID,DIVISION_NAME) VALUES (" + tbInsertIdDivision.Text + "," + tbInsertNameDivision.Text + ")");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('เพิ่มข้อมูลเรียบร้อย')", true);
            BindDivision();
            ClearDivision();
        }
        protected void btnUpdateDivision_Click(object sender, EventArgs e)
        {
            string ValueID = tbInsertIdDivision.Text;
            string ValueName = tbInsertNameDivision.Text;

            if (Session["DefaultIdDivision"] == null)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('กรุณาเลือกรายการที่จะแก้ไขก่อน')", true);
                return;
            }

            if (ValueID != "" && ValueName != "")
            {
                DatabaseManager.ExecuteNonQuery("UPDATE TB_DIVISION SET DIVISION_ID = '" + ValueID + "', DIVISION_NAME = '" + ValueName + "' WHERE DIVISION_ID = '" + Session["DefaultIdDivision"].ToString() + "'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('อัพเดทข้อมูลเรียบร้อย')", true);
                BindDivision();
                ClearDivision();
                Session.Remove("DefaultIdDivision");
            }
        }
        protected void lbuClearDivision_Click(object sender, EventArgs e)
        {
            BindDivision();
            ClearDivision();
            Session.Remove("DefaultIdDivision");
        }
        protected void OnEditDivision(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            string ValueID = (item.FindControl("lbDivisionID") as Label).Text;
            string ValueName = (item.FindControl("lbDivisionName") as Label).Text;

            tbInsertIdDivision.Text = ValueID;
            tbInsertNameDivision.Text = ValueName;

            Session["DefaultIdDivision"] = ValueID;
        }
        protected void OnDeleteDivision(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            int ValueID = int.Parse((item.FindControl("lbDivisionID") as Label).Text);

            if (ValueID != 0)
            {
                DatabaseManager.ExecuteNonQuery("DELETE TB_DIVISION WHERE DIVISION_ID = '" + ValueID + "'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('ลบข้อมูลเรียบร้อย')", true);
                BindDivision();
            }
        }

        //
        protected void BindWorkDivision()
        {
            HideAll();
            Panel10.Visible = true;
            OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING);
            OracleDataAdapter sda = new OracleDataAdapter("SELECT WORK_ID,WORK_NAME FROM TB_WORK_DIVISION ORDER BY ABS(WORK_ID) ASC", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            myRepeaterWorkDivision.DataSource = dt;
            myRepeaterWorkDivision.DataBind();
        }
        protected void ClearWorkDivision()
        {
            tbInsertIdWorkDivision.Text = "";
            tbInsertNameWorkDivision.Text = "";
        }
        protected void lbuMenuWorkDivision_Click(object sender, EventArgs e)
        {
            BindWorkDivision();
        }
        protected void btnInsertWorkDivision_Click(object sender, EventArgs e)
        {
            string oldID = DatabaseManager.ExecuteString("SELECT WORK_ID FROM TB_WORK_DIVISION WHERE WORK_ID ='" + tbInsertIdWorkDivision.Text + "'");
            if (tbInsertIdWorkDivision.Text == oldID)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('มีรหัสงาน/ฝ่าย " + tbInsertIdWorkDivision.Text + " อยู่แล้วในระบบ ไม่สามารถเพิ่มได้')", true);
                return;
            }

            DatabaseManager.ExecuteNonQuery("INSERT INTO TB_WORK_DIVISION (WORK_ID,WORK_NAME) VALUES (" + tbInsertIdWorkDivision.Text + "," + tbInsertNameWorkDivision.Text + ")");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('เพิ่มข้อมูลเรียบร้อย')", true);
            BindWorkDivision();
            ClearWorkDivision();
        }
        protected void btnUpdateWorkDivision_Click(object sender, EventArgs e)
        {
            string ValueID = tbInsertIdWorkDivision.Text;
            string ValueName = tbInsertNameWorkDivision.Text;

            if (Session["DefaultIdWorkDivision"] == null)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('กรุณาเลือกรายการที่จะแก้ไขก่อน')", true);
                return;
            }

            if (ValueID != "" && ValueName != "")
            {
                DatabaseManager.ExecuteNonQuery("UPDATE TB_WORK_DIVISION SET WORK_ID = '" + ValueID + "', WORK_NAME = '" + ValueName + "' WHERE WORK_ID = '" + Session["DefaultIdWorkDivision"].ToString() + "'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('อัพเดทข้อมูลเรียบร้อย')", true);
                BindWorkDivision();
                ClearWorkDivision();
                Session.Remove("DefaultIdWorkDivision");
            }
        }
        protected void lbuClearWorkDivision_Click(object sender, EventArgs e)
        {
            BindWorkDivision();
            ClearWorkDivision();
            Session.Remove("DefaultIdWorkDivision");
        }
        protected void OnEditWorkDivision(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            string ValueID = (item.FindControl("lbWorkDivisionID") as Label).Text;
            string ValueName = (item.FindControl("lbWorkDivisionName") as Label).Text;

            tbInsertIdWorkDivision.Text = ValueID;
            tbInsertNameWorkDivision.Text = ValueName;

            Session["DefaultIdWorkDivision"] = ValueID;
        }
        protected void OnDeleteWorkDivision(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            int ValueID = int.Parse((item.FindControl("lbWorkDivisionID") as Label).Text);

            if (ValueID != 0)
            {
                DatabaseManager.ExecuteNonQuery("DELETE TB_WORK_DIVISION WHERE WORK_ID = '" + ValueID + "'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('ลบข้อมูลเรียบร้อย')", true);
                BindWorkDivision();
            }
        }

        //
        protected void BindStafftype()
        {
            HideAll();
            Panel11.Visible = true;
            OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING);
            OracleDataAdapter sda = new OracleDataAdapter("SELECT STAFFTYPE_ID,STAFFTYPE_NAME FROM TB_STAFFTYPE ORDER BY ABS(STAFFTYPE_ID) ASC", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            myRepeaterStafftype.DataSource = dt;
            myRepeaterStafftype.DataBind();
        }
        protected void ClearStafftype()
        {
            tbInsertIdStafftype.Text = "";
            tbInsertNameStafftype.Text = "";
        }
        protected void lbuMenuStafftype_Click(object sender, EventArgs e)
        {
            BindStafftype();
        }
        protected void btnInsertStafftype_Click(object sender, EventArgs e)
        {
            string oldID = DatabaseManager.ExecuteString("SELECT STAFFTYPE_ID FROM TB_STAFFTYPE WHERE STAFFTYPE_ID ='" + tbInsertIdStafftype.Text + "'");
            if (tbInsertIdStafftype.Text == oldID)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('มีรหัสประเภทบุคลากร " + tbInsertIdStafftype.Text + " อยู่แล้วในระบบ ไม่สามารถเพิ่มได้')", true);
                return;
            }

            DatabaseManager.ExecuteNonQuery("INSERT INTO TB_STAFFTYPE (STAFFTYPE_ID,STAFFTYPE_NAME) VALUES (" + tbInsertIdStafftype.Text + "," + tbInsertNameStafftype.Text + ")");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('เพิ่มข้อมูลเรียบร้อย')", true);
            BindStafftype();
            ClearStafftype();
        }
        protected void btnUpdateStafftype_Click(object sender, EventArgs e)
        {
            string ValueID = tbInsertIdStafftype.Text;
            string ValueName = tbInsertNameStafftype.Text;

            if (Session["DefaultIdStafftype"] == null)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('กรุณาเลือกรายการที่จะแก้ไขก่อน')", true);
                return;
            }

            if (ValueID != "" && ValueName != "")
            {
                DatabaseManager.ExecuteNonQuery("UPDATE TB_STAFFTYPE SET STAFFTYPE_ID = '" + ValueID + "', STAFFTYPE_NAME = '" + ValueName + "' WHERE STAFFTYPE_ID = '" + Session["DefaultIdStafftype"].ToString() + "'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('อัพเดทข้อมูลเรียบร้อย')", true);
                BindStafftype();
                ClearStafftype();
                Session.Remove("DefaultIdStafftype");
            }
        }
        protected void lbuClearStafftype_Click(object sender, EventArgs e)
        {
            BindStafftype();
            ClearStafftype();
            Session.Remove("DefaultIdStafftype");
        }
        protected void OnEditStafftype(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            string ValueID = (item.FindControl("lbStafftypeID") as Label).Text;
            string ValueName = (item.FindControl("lbStafftypeName") as Label).Text;

            tbInsertIdStafftype.Text = ValueID;
            tbInsertNameStafftype.Text = ValueName;

            Session["DefaultIdStafftype"] = ValueID;
        }
        protected void OnDeleteStafftype(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            int ValueID = int.Parse((item.FindControl("lbStafftypeID") as Label).Text);

            if (ValueID != 0)
            {
                DatabaseManager.ExecuteNonQuery("DELETE TB_STAFFTYPE WHERE STAFFTYPE_ID = '" + ValueID + "'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('ลบข้อมูลเรียบร้อย')", true);
                BindStafftype();
            }
        }

        //
        protected void BindTimeContact()
        {
            HideAll();
            Panel12.Visible = true;

            OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING);
            OracleDataAdapter sda = new OracleDataAdapter("SELECT TIME_CONTACT_ID,TIME_CONTACT_NAME FROM TB_TIME_CONTACT ORDER BY ABS(TIME_CONTACT_ID) ASC", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            myRepeaterTimeContact.DataSource = dt;
            myRepeaterTimeContact.DataBind();
        }
        protected void ClearTimeContact()
        {
            tbInsertIdTimeContact.Text = "";
            tbInsertNameTimeContact.Text = "";
        }

        protected void lbuMenuTimeContact_Click(object sender, EventArgs e)
        {
            BindTimeContact();
        }
        protected void btnInsertTimeContact_Click(object sender, EventArgs e)
        {
            string oldID = DatabaseManager.ExecuteString("SELECT TIME_CONTACT_ID FROM TB_TIME_CONTACT WHERE TIME_CONTACT_ID ='" + tbInsertIdTimeContact.Text + "'");
            if (tbInsertIdTimeContact.Text == oldID)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('มีรหัสระยะเวลาจ้าง " + tbInsertIdTimeContact.Text + " อยู่แล้วในระบบ ไม่สามารถเพิ่มได้')", true);
                return;
            }

            DatabaseManager.ExecuteNonQuery("INSERT INTO TB_TIME_CONTACT (TIME_CONTACT_ID,TIME_CONTACT_NAME) VALUES (" + tbInsertIdTimeContact.Text + "," + tbInsertNameTimeContact.Text + ")");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('เพิ่มข้อมูลเรียบร้อย')", true);
            BindTimeContact();
            ClearTimeContact();
        }
        protected void btnUpdateTimeContact_Click(object sender, EventArgs e)
        {
            string ValueID = tbInsertIdTimeContact.Text;
            string ValueName = tbInsertNameTimeContact.Text;

            if (Session["DefaultIdTimeContact"] == null)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('กรุณาเลือกรายการที่จะแก้ไขก่อน')", true);
                return;
            }

            if (ValueID != "" && ValueName != "")
            {
                DatabaseManager.ExecuteNonQuery("UPDATE TB_TIME_CONTACT SET TIME_CONTACT_ID = '" + ValueID + "', TIME_CONTACT_NAME = '" + ValueName + "' WHERE TIME_CONTACT_ID = '" + Session["DefaultIdTimeContact"].ToString() + "'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('อัพเดทข้อมูลเรียบร้อย')", true);
                BindTimeContact();
                ClearTimeContact();
                Session.Remove("DefaultIdTimeContact");
            }
        }
        protected void lbuClearTimeContact_Click(object sender, EventArgs e)
        {
            BindTimeContact();
            ClearTimeContact();
            Session.Remove("DefaultIdTimeContact");
        }
        protected void OnEditTimeContact(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            string ValueID = (item.FindControl("lbTimeContactID") as Label).Text;
            string ValueName = (item.FindControl("lbTimeContactName") as Label).Text;

            tbInsertIdTimeContact.Text = ValueID;
            tbInsertNameTimeContact.Text = ValueName;

            Session["DefaultIdTimeContact"] = ValueID;
        }
        protected void OnDeleteTimeContact(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            int ValueID = int.Parse((item.FindControl("lbTimeContactID") as Label).Text);

            if (ValueID != 0)
            {
                DatabaseManager.ExecuteNonQuery("DELETE TB_TIME_CONTACT WHERE TIME_CONTACT_ID = '" + ValueID + "'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('ลบข้อมูลเรียบร้อย')", true);
                BindTimeContact();
            }
        }

        //
        protected void BindBudget()
        {
            HideAll();
            Panel13.Visible = true;
            OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING);
            OracleDataAdapter sda = new OracleDataAdapter("SELECT BUDGET_ID,BUDGET_NAME FROM TB_BUDGET ORDER BY ABS(BUDGET_ID) ASC", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            myRepeaterBudget.DataSource = dt;
            myRepeaterBudget.DataBind();
        }
        protected void ClearBudget()
        {
            tbInsertIdBudget.Text = "";
            tbInsertNameBudget.Text = "";
        }
        protected void lbuMenuBudget_Click(object sender, EventArgs e)
        {
            BindBudget();
        }

        protected void btnInsertBudget_Click(object sender, EventArgs e)
        {
            string oldID = DatabaseManager.ExecuteString("SELECT BUDGET_ID FROM TB_BUDGET WHERE BUDGET_ID ='" + tbInsertIdBudget.Text + "'");
            if (tbInsertIdBudget.Text == oldID)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('มีรหัสประเภทเงินจ้าง " + tbInsertIdBudget.Text + " อยู่แล้วในระบบ ไม่สามารถเพิ่มได้')", true);
                return;
            }

            DatabaseManager.ExecuteNonQuery("INSERT INTO TB_BUDGET (BUDGET_ID,BUDGET_NAME) VALUES (" + tbInsertIdBudget.Text + "," + tbInsertNameBudget.Text + ")");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('เพิ่มข้อมูลเรียบร้อย')", true);
            BindBudget();
            ClearBudget();
        }
        protected void btnUpdateBudget_Click(object sender, EventArgs e)
        {
            string ValueID = tbInsertIdBudget.Text;
            string ValueName = tbInsertNameBudget.Text;

            if (Session["DefaultIdBudget"] == null)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('กรุณาเลือกรายการที่จะแก้ไขก่อน')", true);
                return;
            }

            if (ValueID != "" && ValueName != "")
            {
                DatabaseManager.ExecuteNonQuery("UPDATE TB_BUDGET SET BUDGET_ID = '" + ValueID + "', BUDGET_NAME = '" + ValueName + "' WHERE BUDGET_ID = '" + Session["DefaultIdBudget"].ToString() + "'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('อัพเดทข้อมูลเรียบร้อย')", true);
                BindBudget();
                ClearBudget();
                Session.Remove("DefaultIdBudget");
            }
        }
        protected void lbuClearBudget_Click(object sender, EventArgs e)
        {
            BindBudget();
            ClearBudget();
            Session.Remove("DefaultIdBudget");
        }
        protected void OnEditBudget(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            string ValueID = (item.FindControl("lbBudgetID") as Label).Text;
            string ValueName = (item.FindControl("lbBudgetName") as Label).Text;

            tbInsertIdBudget.Text = ValueID;
            tbInsertNameBudget.Text = ValueName;

            Session["DefaultIdBudget"] = ValueID;
        }
        protected void OnDeleteBudget(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            int ValueID = int.Parse((item.FindControl("lbBudgetID") as Label).Text);

            if (ValueID != 0)
            {
                DatabaseManager.ExecuteNonQuery("DELETE TB_BUDGET WHERE BUDGET_ID = '" + ValueID + "'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('ลบข้อมูลเรียบร้อย')", true);
                BindBudget();
            }
        }

        //
        protected void BindSubStafftype()
        {
            HideAll();
            Panel14.Visible = true;
            OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING);
            OracleDataAdapter sda = new OracleDataAdapter("SELECT SUBSTAFFTYPE_ID,SUBSTAFFTYPE_NAME FROM TB_SUBSTAFFTYPE ORDER BY ABS(SUBSTAFFTYPE_ID) ASC", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            myRepeaterSubStafftype.DataSource = dt;
            myRepeaterSubStafftype.DataBind();
        }
        protected void ClearSubStafftype()
        {
            tbInsertIdSubStafftype.Text = "";
            tbInsertNameSubStafftype.Text = "";
        }
        protected void lbuMenuSubStafftype_Click(object sender, EventArgs e)
        {
            BindSubStafftype();
        }
        protected void btnInsertSubStafftype_Click(object sender, EventArgs e)
        {
            string oldID = DatabaseManager.ExecuteString("SELECT SUBSTAFFTYPE_ID FROM TB_SUBSTAFFTYPE WHERE SUBSTAFFTYPE_ID ='" + tbInsertIdSubStafftype.Text + "'");
            if (tbInsertIdSubStafftype.Text == oldID)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('มีรหัสประเภทบุคลากรย่อย " + tbInsertIdSubStafftype.Text + " อยู่แล้วในระบบ ไม่สามารถเพิ่มได้')", true);
                return;
            }

            DatabaseManager.ExecuteNonQuery("INSERT INTO TB_SUBSTAFFTYPE (SUBSTAFFTYPE_ID,SUBSTAFFTYPE_NAME) VALUES (" + tbInsertIdSubStafftype.Text + "," + tbInsertNameSubStafftype.Text + ")");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('เพิ่มข้อมูลเรียบร้อย')", true);
            BindSubStafftype();
            ClearSubStafftype();
        }
        protected void btnUpdateSubStafftype_Click(object sender, EventArgs e)
        {
            string ValueID = tbInsertIdSubStafftype.Text;
            string ValueName = tbInsertNameSubStafftype.Text;

            if (Session["DefaultIdSubStafftype"] == null)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('กรุณาเลือกรายการที่จะแก้ไขก่อน')", true);
                return;
            }

            if (ValueID != "" && ValueName != "")
            {
                DatabaseManager.ExecuteNonQuery("UPDATE TB_SUBSTAFFTYPE SET SUBSTAFFTYPE_ID = '" + ValueID + "', SUBSTAFFTYPE_NAME = '" + ValueName + "' WHERE SUBSTAFFTYPE_ID = '" + Session["DefaultIdSubStafftype"].ToString() + "'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('อัพเดทข้อมูลเรียบร้อย')", true);
                BindSubStafftype();
                ClearSubStafftype();
                Session.Remove("DefaultIdSubStafftype");
            }
        }
        protected void lbuClearSubStafftype_Click(object sender, EventArgs e)
        {
            BindSubStafftype();
            ClearSubStafftype();
            Session.Remove("DefaultIdSubStafftype");
        }
        protected void OnEditSubStafftype(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            string ValueID = (item.FindControl("lbSubStafftypeID") as Label).Text;
            string ValueName = (item.FindControl("lbSubStafftypeName") as Label).Text;

            tbInsertIdSubStafftype.Text = ValueID;
            tbInsertNameSubStafftype.Text = ValueName;

            Session["DefaultIdSubStafftype"] = ValueID;
        }
        protected void OnDeleteSubStafftype(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            int ValueID = int.Parse((item.FindControl("lbSubStafftypeID") as Label).Text);

            if (ValueID != 0)
            {
                DatabaseManager.ExecuteNonQuery("DELETE TB_SUBSTAFFTYPE WHERE SUBSTAFFTYPE_ID = '" + ValueID + "'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('ลบข้อมูลเรียบร้อย')", true);
                BindSubStafftype();
            }
        }

        //
        protected void BindAdminPosition()
        {
            HideAll();
            Panel15.Visible = true;
            OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING);
            OracleDataAdapter sda = new OracleDataAdapter("SELECT ADMIN_POSITION_ID,ADMIN_POSITION_NAME FROM TB_ADMIN_POSITION ORDER BY ABS(ADMIN_POSITION_ID) ASC", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            myRepeaterAdminPosition.DataSource = dt;
            myRepeaterAdminPosition.DataBind();
        }
        protected void ClearAdminPosition()
        {
            tbInsertIdAdminPosition.Text = "";
            tbInsertNameAdminPosition.Text = "";
        }
        protected void lbuMenuAdminPosition_Click(object sender, EventArgs e)
        {
            BindAdminPosition();
        }
        protected void btnInsertAdminPosition_Click(object sender, EventArgs e)
        {
            string oldID = DatabaseManager.ExecuteString("SELECT ADMIN_POSITION_ID FROM TB_ADMIN_POSITION WHERE ADMIN_POSITION_ID ='" + tbInsertIdAdminPosition.Text + "'");
            if (tbInsertIdAdminPosition.Text == oldID)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('มีรหัสตำแหน่งบริหาร " + tbInsertIdAdminPosition.Text + " อยู่แล้วในระบบ ไม่สามารถเพิ่มได้')", true);
                return;
            }

            DatabaseManager.ExecuteNonQuery("INSERT INTO TB_ADMIN_POSITION (ADMIN_POSITION_ID,ADMIN_POSITION_NAME) VALUES (" + tbInsertIdAdminPosition.Text + "," + tbInsertNameAdminPosition.Text + ")");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('เพิ่มข้อมูลเรียบร้อย')", true);
            BindAdminPosition();
            ClearAdminPosition();
        }
        protected void btnUpdateAdminPosition_Click(object sender, EventArgs e)
        {
            string ValueID = tbInsertIdAdminPosition.Text;
            string ValueName = tbInsertNameAdminPosition.Text;

            if (Session["DefaultIdAdminPosition"] == null)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('กรุณาเลือกรายการที่จะแก้ไขก่อน')", true);
                return;
            }

            if (ValueID != "" && ValueName != "")
            {
                DatabaseManager.ExecuteNonQuery("UPDATE TB_ADMIN_POSITION SET ADMIN_POSITION_ID = '" + ValueID + "', ADMIN_POSITION_NAME = '" + ValueName + "' WHERE ADMIN_POSITION_ID = '" + Session["DefaultIdAdminPosition"].ToString() + "'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('อัพเดทข้อมูลเรียบร้อย')", true);
                BindAdminPosition();
                ClearAdminPosition();
                Session.Remove("DefaultIdAdminPosition");
            }
        }
        protected void lbuClearAdminPosition_Click(object sender, EventArgs e)
        {
            BindAdminPosition();
            ClearAdminPosition();
            Session.Remove("DefaultIdAdminPosition");
        }
        protected void OnEditAdminPosition(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            string ValueID = (item.FindControl("lbAdminPositionID") as Label).Text;
            string ValueName = (item.FindControl("lbAdminPositionName") as Label).Text;

            tbInsertIdAdminPosition.Text = ValueID;
            tbInsertNameAdminPosition.Text = ValueName;

            Session["DefaultIdAdminPosition"] = ValueID;
        }
        protected void OnDeleteAdminPosition(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            int ValueID = int.Parse((item.FindControl("lbAdminPositionID") as Label).Text);

            if (ValueID != 0)
            {
                DatabaseManager.ExecuteNonQuery("DELETE TB_ADMIN_POSITION WHERE ADMIN_POSITION_ID = '" + ValueID + "'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('ลบข้อมูลเรียบร้อย')", true);
                BindAdminPosition();
            }
        }

        //
        protected void BindPosition()
        {
            HideAll();
            Panel16.Visible = true;
            OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING);
            OracleDataAdapter sda = new OracleDataAdapter("SELECT P_ID,P_NAME FROM TB_POSITION ORDER BY ABS(P_ID) ASC", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            myRepeaterPosition.DataSource = dt;
            myRepeaterPosition.DataBind();
        }
        protected void ClearPosition()
        {
            tbInsertIdPosition.Text = "";
            tbInsertNamePosition.Text = "";
        }
        protected void lbuMenuPosition_Click(object sender, EventArgs e)
        {
            BindPosition();
        }
        protected void btnInsertPosition_Click(object sender, EventArgs e)
        {
            string oldID = DatabaseManager.ExecuteString("SELECT P_ID FROM TB_POSITION WHERE P_ID ='" + tbInsertIdPosition.Text + "'");
            if (tbInsertIdPosition.Text == oldID)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('มีรหัสระดับตำแหน่ง " + tbInsertIdPosition.Text + " อยู่แล้วในระบบ ไม่สามารถเพิ่มได้')", true);
                return;
            }

            DatabaseManager.ExecuteNonQuery("INSERT INTO TB_POSITION (P_ID,P_NAME) VALUES (" + tbInsertIdPosition.Text + "," + tbInsertNamePosition.Text + ")");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('เพิ่มข้อมูลเรียบร้อย')", true);
            BindPosition();
            ClearPosition();
        }
        protected void btnUpdatePosition_Click(object sender, EventArgs e)
        {
            string ValueID = tbInsertIdPosition.Text;
            string ValueName = tbInsertNamePosition.Text;

            if (Session["DefaultIdPosition"] == null)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('กรุณาเลือกรายการที่จะแก้ไขก่อน')", true);
                return;
            }

            if (ValueID != "" && ValueName != "")
            {
                DatabaseManager.ExecuteNonQuery("UPDATE TB_POSITION SET P_ID = '" + ValueID + "', P_NAME = '" + ValueName + "' WHERE P_ID = '" + Session["DefaultIdPosition"].ToString() + "'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('อัพเดทข้อมูลเรียบร้อย')", true);
                BindPosition();
                ClearPosition();
                Session.Remove("DefaultIdPosition");
            }
        }
        protected void lbuClearPosition_Click(object sender, EventArgs e)
        {
            BindPosition();
            ClearPosition();
            Session.Remove("DefaultIdPosition");
        }
        protected void OnEditPosition(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            string ValueID = (item.FindControl("lbPositionID") as Label).Text;
            string ValueName = (item.FindControl("lbPositionName") as Label).Text;

            tbInsertIdPosition.Text = ValueID;
            tbInsertNamePosition.Text = ValueName;

            Session["DefaultIdPosition"] = ValueID;
        }
        protected void OnDeletePosition(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            int ValueID = int.Parse((item.FindControl("lbPositionID") as Label).Text);

            if (ValueID != 0)
            {
                DatabaseManager.ExecuteNonQuery("DELETE TB_POSITION WHERE P_ID = '" + ValueID + "'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('ลบข้อมูลเรียบร้อย')", true);
                BindPosition();
            }
        }

        //
        protected void BindWorkPos()
        {
            HideAll();
            Panel17.Visible = true;
            OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING);
            OracleDataAdapter sda = new OracleDataAdapter("SELECT POSITION_WORK_ID,POSITION_WORK_NAME FROM TB_POSITION_WORK ORDER BY ABS(POSITION_WORK_ID) ASC", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            myRepeaterWorkPos.DataSource = dt;
            myRepeaterWorkPos.DataBind();
        }
        protected void ClearWorkPos()
        {
            tbInsertIdWorkPos.Text = "";
            tbInsertNameWorkPos.Text = "";
        }
        protected void lbuMenuWorkPos_Click(object sender, EventArgs e)
        {
            BindWorkPos();
        }
        protected void btnInsertWorkPos_Click(object sender, EventArgs e)
        {
            string oldID = DatabaseManager.ExecuteString("SELECT POSITION_WORK_ID FROM TB_POSITION_WORK WHERE POSITION_WORK_ID ='" + tbInsertIdWorkPos.Text + "'");
            if (tbInsertIdWorkPos.Text == oldID)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('มีรหัสตำแหน่งในสายงาน " + tbInsertIdWorkPos.Text + " อยู่แล้วในระบบ ไม่สามารถเพิ่มได้')", true);
                return;
            }

            DatabaseManager.ExecuteNonQuery("INSERT INTO TB_POSITION_WORK (POSITION_WORK_ID,POSITION_WORK_NAME) VALUES (" + tbInsertIdWorkPos.Text + "," + tbInsertNameWorkPos.Text + ")");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('เพิ่มข้อมูลเรียบร้อย')", true);
            BindWorkPos();
            ClearWorkPos();
        }
        protected void btnUpdateWorkPos_Click(object sender, EventArgs e)
        {
            string ValueID = tbInsertIdWorkPos.Text;
            string ValueName = tbInsertNameWorkPos.Text;

            if (Session["DefaultIdWorkPos"] == null)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('กรุณาเลือกรายการที่จะแก้ไขก่อน')", true);
                return;
            }

            if (ValueID != "" && ValueName != "")
            {
                DatabaseManager.ExecuteNonQuery("UPDATE TB_POSITION_WORK SET POSITION_WORK_ID = '" + ValueID + "', POSITION_WORK_NAME = '" + ValueName + "' WHERE POSITION_WORK_ID = '" + Session["DefaultIdWorkPos"].ToString() + "'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('อัพเดทข้อมูลเรียบร้อย')", true);
                BindWorkPos();
                ClearWorkPos();
                Session.Remove("DefaultIdWorkPos");
            }
        }
        protected void lbuClearWorkPos_Click(object sender, EventArgs e)
        {
            BindWorkPos();
            ClearWorkPos();
            Session.Remove("DefaultIdWorkPos");
        }
        protected void OnEditWorkPos(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            string ValueID = (item.FindControl("lbWorkPosID") as Label).Text;
            string ValueName = (item.FindControl("lbWorkPosName") as Label).Text;

            tbInsertIdWorkPos.Text = ValueID;
            tbInsertNameWorkPos.Text = ValueName;

            Session["DefaultIdWorkPos"] = ValueID;
        }
        protected void OnDeleteWorkPos(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            int ValueID = int.Parse((item.FindControl("lbWorkPosID") as Label).Text);

            if (ValueID != 0)
            {
                DatabaseManager.ExecuteNonQuery("DELETE TB_POSITION_WORK WHERE POSITION_WORK_ID = '" + ValueID + "'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('ลบข้อมูลเรียบร้อย')", true);
                BindWorkPos();
            }
        }

        //
        protected void BindTeachISCED()
        {
            HideAll();
            Panel18.Visible = true;
            OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING);
            OracleDataAdapter sda = new OracleDataAdapter("SELECT ISCED_CODE,ISCED_NAME FROM TB_ISCED ORDER BY ABS(ISCED_CODE) ASC", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            myRepeaterTeachISCED.DataSource = dt;
            myRepeaterTeachISCED.DataBind();
        }
        protected void ClearTeachISCED()
        {
            tbInsertIdTeachISCED.Text = "";
            tbInsertNameTeachISCED.Text = "";
        }
        protected void lbuMenuTeachISCED_Click(object sender, EventArgs e)
        {
            BindTeachISCED();
        }
        protected void btnInsertTeachISCED_Click(object sender, EventArgs e)
        {
            string oldID = DatabaseManager.ExecuteString("SELECT ISCED_CODE FROM TB_ISCED WHERE ISCED_CODE ='" + tbInsertIdTeachISCED.Text + "'");
            if (tbInsertIdTeachISCED.Text == oldID)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('มีรหัสกลุ่มสาขาวิชา " + tbInsertIdTeachISCED.Text + " อยู่แล้วในระบบ ไม่สามารถเพิ่มได้')", true);
                return;
            }

            DatabaseManager.ExecuteNonQuery("INSERT INTO TB_ISCED (ISCED_CODE,ISCED_NAME) VALUES (" + tbInsertIdTeachISCED.Text + "," + tbInsertNameTeachISCED.Text + ")");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('เพิ่มข้อมูลเรียบร้อย')", true);
            BindTeachISCED();
            ClearTeachISCED();
        }
        protected void btnUpdateTeachISCED_Click(object sender, EventArgs e)
        {
            string ValueID = tbInsertIdTeachISCED.Text;
            string ValueName = tbInsertNameTeachISCED.Text;

            if (Session["DefaultIdTeachISCED"] == null)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('กรุณาเลือกรายการที่จะแก้ไขก่อน')", true);
                return;
            }

            if (ValueID != "" && ValueName != "")
            {
                DatabaseManager.ExecuteNonQuery("UPDATE TB_ISCED SET ISCED_CODE = '" + ValueID + "', ISCED_NAME = '" + ValueName + "' WHERE ISCED_CODE = '" + Session["DefaultIdTeachISCED"].ToString() + "'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('อัพเดทข้อมูลเรียบร้อย')", true);
                BindTeachISCED();
                ClearTeachISCED();
                Session.Remove("DefaultIdTeachISCED");
            }
        }
        protected void lbuClearTeachISCED_Click(object sender, EventArgs e)
        {
            BindTeachISCED();
            ClearTeachISCED();
            Session.Remove("DefaultIdTeachISCED");
        }
        protected void OnEditTeachISCED(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            string ValueID = (item.FindControl("lbTeachISCEDID") as Label).Text;
            string ValueName = (item.FindControl("lbTeachISCEDName") as Label).Text;

            tbInsertIdTeachISCED.Text = ValueID;
            tbInsertNameTeachISCED.Text = ValueName;

            Session["DefaultIdTeachISCED"] = ValueID;
        }
        protected void OnDeleteTeachISCED(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            int ValueID = int.Parse((item.FindControl("lbTeachISCEDID") as Label).Text);

            if (ValueID != 0)
            {
                DatabaseManager.ExecuteNonQuery("DELETE TB_ISCED WHERE ISCED_CODE = '" + ValueID + "'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('ลบข้อมูลเรียบร้อย')", true);
                BindTeachISCED();
            }
        }

        //
        protected void BindGradLev()
        {
            HideAll();
            Panel19.Visible = true;
            OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING);
            OracleDataAdapter sda = new OracleDataAdapter("SELECT LEV_ID,LEV_NAME_TH FROM TB_LEV ORDER BY ABS(LEV_ID) ASC", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            myRepeaterGradLev.DataSource = dt;
            myRepeaterGradLev.DataBind();
        }
        protected void ClearGradLev()
        {
            tbInsertIdGradLev.Text = "";
            tbInsertNameGradLev.Text = "";
        }
        protected void lbuMenuGradLev_Click(object sender, EventArgs e)
        {
            BindGradLev();
        }
        protected void btnInsertGradLev_Click(object sender, EventArgs e)
        {
            string oldID = DatabaseManager.ExecuteString("SELECT LEV_ID FROM TB_LEV WHERE LEV_ID ='" + tbInsertIdGradLev.Text + "'");
            if (tbInsertIdGradLev.Text == oldID)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('มีรหัสระดับการศึกษา " + tbInsertIdGradLev.Text + " อยู่แล้วในระบบ ไม่สามารถเพิ่มได้')", true);
                return;
            }

            DatabaseManager.ExecuteNonQuery("INSERT INTO TB_LEV (LEV_ID,LEV_NAME_TH) VALUES (" + tbInsertIdGradLev.Text + "," + tbInsertNameGradLev.Text + ")");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('เพิ่มข้อมูลเรียบร้อย')", true);
            BindGradLev();
            ClearGradLev();
        }
        protected void btnUpdateGradLev_Click(object sender, EventArgs e)
        {
            string ValueID = tbInsertIdGradLev.Text;
            string ValueName = tbInsertNameGradLev.Text;

            if (Session["DefaultIdGradLev"] == null)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('กรุณาเลือกรายการที่จะแก้ไขก่อน')", true);
                return;
            }

            if (ValueID != "" && ValueName != "")
            {
                DatabaseManager.ExecuteNonQuery("UPDATE TB_LEV SET LEV_ID = '" + ValueID + "', LEV_NAME_TH = '" + ValueName + "' WHERE LEV_ID = '" + Session["DefaultIdGradLev"].ToString() + "'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('อัพเดทข้อมูลเรียบร้อย')", true);
                BindGradLev();
                ClearGradLev();
                Session.Remove("DefaultIdGradLev");
            }
        }
        protected void lbuClearGradLev_Click(object sender, EventArgs e)
        {
            BindGradLev();
            ClearGradLev();
            Session.Remove("DefaultIdGradLev");
        }
        protected void OnEditGradLev(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            string ValueID = (item.FindControl("lbGradLevID") as Label).Text;
            string ValueName = (item.FindControl("lbGradLevName") as Label).Text;

            tbInsertIdGradLev.Text = ValueID;
            tbInsertNameGradLev.Text = ValueName;

            Session["DefaultIdGradLev"] = ValueID;
        }
        protected void OnDeleteGradLev(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            int ValueID = int.Parse((item.FindControl("lbGradLevID") as Label).Text);

            if (ValueID != 0)
            {
                DatabaseManager.ExecuteNonQuery("DELETE TB_LEV WHERE LEV_ID = '" + ValueID + "'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('ลบข้อมูลเรียบร้อย')", true);
                BindGradLev();
            }
        }

        //
        protected void BindGradProg()
        {
            HideAll();
            Panel20.Visible = true;
            OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING);
            OracleDataAdapter sda = new OracleDataAdapter("SELECT PROGRAM_ID_NEW,PROGRAM_NAME FROM TB_PROGRAM ORDER BY ABS(PROGRAM_ID_NEW) ASC", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            myRepeaterGradProg.DataSource = dt;
            myRepeaterGradProg.DataBind();
        }
        protected void ClearGradProg()
        {
            tbInsertIdGradProg.Text = "";
            tbInsertNameGradProg.Text = "";
        }
        protected void lbuMenuGradProg_Click(object sender, EventArgs e)
        {
            BindGradProg();
        }

        protected void btnInsertGradProg_Click(object sender, EventArgs e)
        {
            string oldID = DatabaseManager.ExecuteString("SELECT PROGRAM_ID_NEW FROM TB_PROGRAM WHERE PROGRAM_ID_NEW ='" + tbInsertIdGradProg.Text + "'");
            if (tbInsertIdGradProg.Text == oldID)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('มีรหัสสาขาวิชา " + tbInsertIdGradProg.Text + " อยู่แล้วในระบบ ไม่สามารถเพิ่มได้')", true);
                return;
            }

            DatabaseManager.ExecuteNonQuery("INSERT INTO TB_PROGRAM (PROGRAM_ID_NEW,PROGRAM_NAME) VALUES (" + tbInsertIdGradProg.Text + "," + tbInsertNameGradProg.Text + ")");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('เพิ่มข้อมูลเรียบร้อย')", true);
            BindGradProg();
            ClearGradProg();
        }
        protected void btnUpdateGradProg_Click(object sender, EventArgs e)
        {
            string ValueID = tbInsertIdGradProg.Text;
            string ValueName = tbInsertNameGradProg.Text;

            if (Session["DefaultIdGradProg"] == null)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('กรุณาเลือกรายการที่จะแก้ไขก่อน')", true);
                return;
            }

            if (ValueID != "" && ValueName != "")
            {
                DatabaseManager.ExecuteNonQuery("UPDATE TB_PROGRAM SET PROGRAM_ID_NEW = '" + ValueID + "', PROGRAM_NAME = '" + ValueName + "' WHERE PROGRAM_ID_NEW = '" + Session["DefaultIdGradProg"].ToString() + "'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('อัพเดทข้อมูลเรียบร้อย')", true);
                BindGradProg();
                ClearGradProg();
                Session.Remove("DefaultIdGradProg");
            }
        }
        protected void lbuClearGradProg_Click(object sender, EventArgs e)
        {
            BindGradProg();
            ClearGradProg();
            Session.Remove("DefaultIdGradProg");
        }
        protected void OnEditGradProg(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            string ValueID = (item.FindControl("lbGradProgID") as Label).Text;
            string ValueName = (item.FindControl("lbGradProgName") as Label).Text;

            tbInsertIdGradProg.Text = ValueID;
            tbInsertNameGradProg.Text = ValueName;

            Session["DefaultIdGradProg"] = ValueID;
        }
        protected void OnDeleteGradProg(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            int ValueID = int.Parse((item.FindControl("lbGradProgID") as Label).Text);

            if (ValueID != 0)
            {
                DatabaseManager.ExecuteNonQuery("DELETE TB_PROGRAM WHERE PROGRAM_ID_NEW = '" + ValueID + "'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('ลบข้อมูลเรียบร้อย')", true);
                BindGradProg();
            }
        }

        //
        protected void BindDeform()
        {
            HideAll();
            Panel21.Visible = true;
            OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING);
            OracleDataAdapter sda = new OracleDataAdapter("SELECT DEFORM_ID,DEFORM_NAME FROM TB_DEFORM ORDER BY ABS(DEFORM_ID) ASC", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            myRepeaterDeform.DataSource = dt;
            myRepeaterDeform.DataBind();
        }
        protected void ClearDeform()
        {
            tbInsertIdDeform.Text = "";
            tbInsertNameDeform.Text = "";
        }
        protected void lbuMenuDeform_Click(object sender, EventArgs e)
        {
            BindDeform();
        }
        protected void btnInsertDeform_Click(object sender, EventArgs e)
        {
            string oldID = DatabaseManager.ExecuteString("SELECT DEFORM_ID FROM TB_DEFORM WHERE DEFORM_ID ='" + tbInsertIdDeform.Text + "'");
            if (tbInsertIdDeform.Text == oldID)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('มีรหัสความพิการ " + tbInsertIdDeform.Text + " อยู่แล้วในระบบ ไม่สามารถเพิ่มได้')", true);
                return;
            }

            DatabaseManager.ExecuteNonQuery("INSERT INTO TB_DEFORM (DEFORM_ID,DEFORM_NAME) VALUES (" + tbInsertIdDeform.Text + "," + tbInsertNameDeform.Text + ")");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('เพิ่มข้อมูลเรียบร้อย')", true);
            BindDeform();
            ClearDeform();
        }
        protected void btnUpdateDeform_Click(object sender, EventArgs e)
        {
            string ValueID = tbInsertIdDeform.Text;
            string ValueName = tbInsertNameDeform.Text;

            if (Session["DefaultIdDeform"] == null)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('กรุณาเลือกรายการที่จะแก้ไขก่อน')", true);
                return;
            }

            if (ValueID != "" && ValueName != "")
            {
                DatabaseManager.ExecuteNonQuery("UPDATE TB_DEFORM SET DEFORM_ID = '" + ValueID + "', DEFORM_NAME = '" + ValueName + "' WHERE DEFORM_ID = '" + Session["DefaultIdDeform"].ToString() + "'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('อัพเดทข้อมูลเรียบร้อย')", true);
                BindDeform();
                ClearDeform();
                Session.Remove("DefaultIdDeform");
            }
        }
        protected void lbuClearDeform_Click(object sender, EventArgs e)
        {
            BindDeform();
            ClearDeform();
            Session.Remove("DefaultIdDeform");
        }
        protected void OnEditDeform(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            string ValueID = (item.FindControl("lbDeformID") as Label).Text;
            string ValueName = (item.FindControl("lbDeformName") as Label).Text;

            tbInsertIdDeform.Text = ValueID;
            tbInsertNameDeform.Text = ValueName;

            Session["DefaultIdDeform"] = ValueID;
        }
        protected void OnDeleteDeform(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            int ValueID = int.Parse((item.FindControl("lbDeformID") as Label).Text);

            if (ValueID != 0)
            {
                DatabaseManager.ExecuteNonQuery("DELETE TB_DEFORM WHERE DEFORM_ID = '" + ValueID + "'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('ลบข้อมูลเรียบร้อย')", true);
                BindDeform();
            }
        }

        //
        protected void BindReligion()
        {
            HideAll();
            Panel22.Visible = true;
            OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING);
            OracleDataAdapter sda = new OracleDataAdapter("SELECT RELIGION_ID,RELIGION_NAME FROM TB_RELIGION ORDER BY ABS(RELIGION_ID) ASC", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            myRepeaterReligion.DataSource = dt;
            myRepeaterReligion.DataBind();
        }
        protected void ClearReligion()
        {
            tbInsertIdReligion.Text = "";
            tbInsertNameReligion.Text = "";
        }
        protected void lbuMenuReligion_Click(object sender, EventArgs e)
        {
            BindReligion();
        }
        protected void btnInsertReligion_Click(object sender, EventArgs e)
        {
            string oldID = DatabaseManager.ExecuteString("SELECT RELIGION_ID FROM TB_RELIGION WHERE RELIGION_ID ='" + tbInsertIdReligion.Text + "'");
            if (tbInsertIdReligion.Text == oldID)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('มีรหัสศาสนา " + tbInsertIdReligion.Text + " อยู่แล้วในระบบ ไม่สามารถเพิ่มได้')", true);
                return;
            }

            DatabaseManager.ExecuteNonQuery("INSERT INTO TB_RELIGION (RELIGION_ID,RELIGION_NAME) VALUES (" + tbInsertIdReligion.Text + "," + tbInsertNameReligion.Text + ")");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('เพิ่มข้อมูลเรียบร้อย')", true);
            BindReligion();
            ClearReligion();
        }
        protected void btnUpdateReligion_Click(object sender, EventArgs e)
        {
            string ValueID = tbInsertIdReligion.Text;
            string ValueName = tbInsertNameReligion.Text;

            if (Session["DefaultIdReligion"] == null)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('กรุณาเลือกรายการที่จะแก้ไขก่อน')", true);
                return;
            }

            if (ValueID != "" && ValueName != "")
            {
                DatabaseManager.ExecuteNonQuery("UPDATE TB_RELIGION SET RELIGION_ID = '" + ValueID + "', RELIGION_NAME = '" + ValueName + "' WHERE RELIGION_ID = '" + Session["DefaultIdReligion"].ToString() + "'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('อัพเดทข้อมูลเรียบร้อย')", true);
                BindReligion();
                ClearReligion();
                Session.Remove("DefaultIdReligion");
            }
        }
        protected void lbuClearReligion_Click(object sender, EventArgs e)
        {
            BindReligion();
            ClearReligion();
            Session.Remove("DefaultIdReligion");
        }
        protected void OnEditReligion(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            string ValueID = (item.FindControl("lbReligionID") as Label).Text;
            string ValueName = (item.FindControl("lbReligionName") as Label).Text;

            tbInsertIdReligion.Text = ValueID;
            tbInsertNameReligion.Text = ValueName;

            Session["DefaultIdReligion"] = ValueID;
        }
        protected void OnDeleteReligion(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            int ValueID = int.Parse((item.FindControl("lbReligionID") as Label).Text);

            if (ValueID != 0)
            {
                DatabaseManager.ExecuteNonQuery("DELETE TB_RELIGION WHERE RELIGION_ID = '" + ValueID + "'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('ลบข้อมูลเรียบร้อย')", true);
                BindReligion();
            }
        }

        //
        protected void BindMovementType()
        {
            HideAll();
            Panel23.Visible = true;
            OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING);
            OracleDataAdapter sda = new OracleDataAdapter("SELECT MOVEMENT_TYPE_ID,MOVEMENT_TYPE_NAME FROM TB_MOVEMENT_TYPE ORDER BY ABS(MOVEMENT_TYPE_ID) ASC", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            myRepeaterMovementType.DataSource = dt;
            myRepeaterMovementType.DataBind();
        }
        protected void ClearMovementType()
        {
            tbInsertIdMovementType.Text = "";
            tbInsertNameMovementType.Text = "";
        }
        protected void lbuMenuMovementType_Click(object sender, EventArgs e)
        {
            BindMovementType();
        }
        protected void btnInsertMovementType_Click(object sender, EventArgs e)
        {
            string oldID = DatabaseManager.ExecuteString("SELECT MOVEMENT_TYPE_ID FROM TB_MOVEMENT_TYPE WHERE MOVEMENT_TYPE_ID ='" + tbInsertIdMovementType.Text + "'");
            if (tbInsertIdMovementType.Text == oldID)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('มีรหัสประเภทการดำรงตำแหน่งปัจจุบัน " + tbInsertIdMovementType.Text + " อยู่แล้วในระบบ ไม่สามารถเพิ่มได้')", true);
                return;
            }

            DatabaseManager.ExecuteNonQuery("INSERT INTO TB_MOVEMENT_TYPE (MOVEMENT_TYPE_ID,MOVEMENT_TYPE_NAME) VALUES (" + tbInsertIdMovementType.Text + "," + tbInsertNameMovementType.Text + ")");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('เพิ่มข้อมูลเรียบร้อย')", true);
            BindMovementType();
            ClearMovementType();
        }
        protected void btnUpdateMovementType_Click(object sender, EventArgs e)
        {
            string ValueID = tbInsertIdMovementType.Text;
            string ValueName = tbInsertNameMovementType.Text;

            if (Session["DefaultIdMovementType"] == null)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('กรุณาเลือกรายการที่จะแก้ไขก่อน')", true);
                return;
            }

            if (ValueID != "" && ValueName != "")
            {
                DatabaseManager.ExecuteNonQuery("UPDATE TB_MOVEMENT_TYPE SET MOVEMENT_TYPE_ID = '" + ValueID + "', MOVEMENT_TYPE_NAME = '" + ValueName + "' WHERE MOVEMENT_TYPE_ID = '" + Session["DefaultIdMovementType"].ToString() + "'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('อัพเดทข้อมูลเรียบร้อย')", true);
                BindMovementType();
                ClearMovementType();
                Session.Remove("DefaultIdMovementType");
            }
        }
        protected void lbuClearMovementType_Click(object sender, EventArgs e)
        {
            BindMovementType();
            ClearMovementType();
            Session.Remove("DefaultIdMovementType");
        }
        protected void OnEditMovementType(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            string ValueID = (item.FindControl("lbMovementTypeID") as Label).Text;
            string ValueName = (item.FindControl("lbMovementTypeName") as Label).Text;

            tbInsertIdMovementType.Text = ValueID;
            tbInsertNameMovementType.Text = ValueName;

            Session["DefaultIdMovementType"] = ValueID;
        }
        protected void OnDeleteMovementType(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            int ValueID = int.Parse((item.FindControl("lbMovementTypeID") as Label).Text);

            if (ValueID != 0)
            {
                DatabaseManager.ExecuteNonQuery("DELETE TB_MOVEMENT_TYPE WHERE MOVEMENT_TYPE_ID = '" + ValueID + "'");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('ลบข้อมูลเรียบร้อย')", true);
                BindMovementType();
            }
        }
        //

    }
}