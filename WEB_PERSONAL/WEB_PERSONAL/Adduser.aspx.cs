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
    public partial class Adduser : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            BindDDL();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDDL();
            }
        }

        protected void BindDDL()
        {
            DatabaseManager.BindDropDown(ddlUniv, "SELECT * FROM TB_CAMPUS ORDER BY ABS(CAMPUS_ID) ASC", "CAMPUS_NAME", "CAMPUS_ID", "--กรุณาเลือก--");
            DatabaseManager.BindDropDown(ddlPrefixName, "SELECT * FROM TB_TITLENAME ORDER BY ABS(TITLE_ID) ASC", "TITLE_NAME_TH", "TITLE_ID", "--กรุณาเลือก--");
            DatabaseManager.BindDropDown(ddlGender, "SELECT * FROM TB_GENDER ORDER BY ABS(GENDER_ID) ASC", "GENDER_NAME", "GENDER_ID", "--กรุณาเลือก--");
            DatabaseManager.BindDropDown(ddlProvince, "SELECT * FROM TB_PROVINCE", "PROVINCE_TH", "PROVINCE_ID", "--กรุณาเลือก จังหวัด--");
            ddlDistrict.Items.Insert(0, new ListItem("--กรุณาเลือก อำเภอ--", ""));
            ddlSubDistrict.Items.Insert(0, new ListItem("--กรุณาเลือก ตำบล--", ""));
            DatabaseManager.BindDropDown(ddlNation, "SELECT * FROM TB_NATIONAL ORDER BY ABS(NATION_SEQ) ASC", "NATION_ENG", "NATION_ID", "--กรุณาเลือก--");

            DatabaseManager.BindDropDown(ddlStafftype, "SELECT * FROM TB_STAFFTYPE ORDER BY ABS(STAFFTYPE_ID) ASC", "STAFFTYPE_NAME", "STAFFTYPE_ID", "--กรุณาเลือก--");
            DatabaseManager.BindDropDown(ddlTimeContact, "SELECT * FROM TB_TIME_CONTACT ORDER BY ABS(TIME_CONTACT_ID) ASC", "TIME_CONTACT_NAME", "TIME_CONTACT_ID", "--กรุณาเลือก--");
            DatabaseManager.BindDropDown(ddlBudget, "SELECT * FROM TB_BUDGET ORDER BY ABS(BUDGET_ID) ASC", "BUDGET_NAME", "BUDGET_ID", "--กรุณาเลือก--");
            DatabaseManager.BindDropDown(ddlSubStafftype, "SELECT * FROM TB_SUBSTAFFTYPE ORDER BY ABS(SUBSTAFFTYPE_ID) ASC", "SUBSTAFFTYPE_NAME", "SUBSTAFFTYPE_ID", "--กรุณาเลือก--");
            DatabaseManager.BindDropDown(ddlAdminPosition, "SELECT * FROM TB_ADMIN_POSITION ORDER BY ABS(ADMIN_POSITION_ID) ASC", "ADMIN_POSITION_NAME", "ADMIN_POSITION_ID", "--กรุณาเลือก--");
            /*DatabaseManager.BindDropDown(ddlPosition, "SELECT * FROM TB_POSITION ORDER BY ABS(P_ID) ASC", "POSITION_NAME_TH", "POSITION_ID", "--กรุณาเลือก--");
            DatabaseManager.BindDropDown(ddlDepartment, "SELECT * FROM REF_FAC ORDER BY ABS(FAC_ID) ASC", "FAC_NAME", "FAC_ID", "--กรุณาเลือก--");
            DatabaseManager.BindDropDown(ddlTeachISCED, "SELECT * FROM REF_ISCED  ORDER BY ISCED_ID", "ISCED_NAME", "ISCED_ID", "--กรุณาเลือก--");
            DatabaseManager.BindDropDown(ddlGradLev, "SELECT * FROM REF_LEV ORDER BY ABS(LEV_ID) ASC", "LEV_NAME_TH", "LEV_ID", "--กรุณาเลือก--");
            DatabaseManager.BindDropDown(ddlGradISCED, "SELECT * FROM REF_ISCED ORDER BY ISCED_ID", "ISCED_NAME", "ISCED_ID", "--กรุณาเลือก--");
            DatabaseManager.BindDropDown(ddlGradProg, "SELECT * FROM REF_PROGRAM ORDER BY ABS(PROGRAM_ID_NEW) ASC", "PROGRAM_NAME", "PROGRAM_ID_NEW", "--กรุณาเลือก--");
            DatabaseManager.BindDropDown(ddlGradCountry, "SELECT * FROM REF_NATION ORDER BY NATION_ID", "NATION_NAME_ENG", "NATION_ID", "--กรุณาเลือก--");

            DatabaseManager.BindDropDown(ddlDeform, "SELECT * FROM REF_DEFORM ORDER BY ABS(DEFORM_ID) ASC", "DEFORM_NAME", "DEFORM_ID", "--กรุณาเลือก--");
            DatabaseManager.BindDropDown(ddlReligion, "SELECT * FROM REF_RELIGION ORDER BY ABS(RELIGION_ID) ASC", "RELIGION_NAME_TH", "RELIGION_ID", "--กรุณาเลือก--");
            DatabaseManager.BindDropDown(ddlMovementType, "SELECT * FROM REF_MOVEMENT_TYPE ORDER BY ABS(MOVEMENT_TYPE_ID) ASC", "MOVEMENT_TYPE_NAME", "MOVEMENT_TYPE_ID", "--กรุณาเลือก--");*/
        }

        public void ChangeNotification(string type)
        {
            switch (type)
            {
                case "info": notification.Attributes["class"] = "alert alert_info"; break;
                case "success": notification.Attributes["class"] = "alert alert_success"; break;
                case "warning": notification.Attributes["class"] = "alert alert_warning"; break;
                case "danger": notification.Attributes["class"] = "alert alert_danger"; break;
                default: notification.Attributes["class"] = null; break;
            }
        }

        public void ChangeNotification(string type, string text)
        {
            switch (type)
            {
                case "info": notification.Attributes["class"] = "alert alert_info"; break;
                case "success": notification.Attributes["class"] = "alert alert_success"; break;
                case "warning": notification.Attributes["class"] = "alert alert_warning"; break;
                case "danger": notification.Attributes["class"] = "alert alert_danger"; break;
                default: notification.Attributes["class"] = null; break;
            }
            notification.InnerHtml = text;
        }

        private void ClearNotification()
        {
            notification.Attributes["class"] = null;
            notification.InnerHtml = "";

            tbCitizenID.CssClass = "form-control input-sm";
            tbName.CssClass = "form-control input-sm";
            tbLastName.CssClass = "form-control input-sm";
            tbBirthday.CssClass = "form-control input-sm";
            ddlProvince.CssClass = "form-control input-sm select2";
            ddlDistrict.CssClass = "form-control input-sm select2";
            ddlSubDistrict.CssClass = "form-control input-sm select2";
            tbZipcode.CssClass = "form-control input-sm";
            ddlNation.CssClass = "form-control input-sm select2";

            ddlStafftype.CssClass = "form-control input-sm select2";
            ddlTimeContact.CssClass = "form-control input-sm select2";
            ddlBudget.CssClass = "form-control input-sm select2";
            ddlSubStafftype.CssClass = "form-control input-sm select2";
            ddlAdminPosition.CssClass = "form-control input-sm select2";
            ddlPosition.CssClass = "form-control input-sm select2";
            ddlDepartment.CssClass = "form-control input-sm select2";
            tbDateInwork.CssClass = "form-control input-sm";
            tbDateStartThisU.CssClass = "form-control input-sm";
            ddlGradLev.CssClass = "form-control input-sm select2";
            tbGradCURR.CssClass = "form-control input-sm";
            ddlGradISCED.CssClass = "form-control input-sm select2";
            ddlGradProg.CssClass = "form-control input-sm select2";
            tbGradUniv.CssClass = "form-control input-sm";
            ddlGradCountry.CssClass = "form-control input-sm select2";

            ddlDeform.CssClass = "form-control input-sm select2";
        }

        private void AddNotification(string text)
        {
            notification.InnerHtml += text;
        }

       
    }
}