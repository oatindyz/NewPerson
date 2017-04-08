using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OracleClient;
using System.IO;
using WEB_PERSONAL.Class;

namespace WEB_PERSONAL {
    public partial class Profile : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            PersonnelSystem ps = PersonnelSystem.GetPersonnelSystem(this);
            Person pp;
            if (Request.QueryString["psID"] != null) {
                pp = DatabaseManager.GetPerson(Request.QueryString["psID"]);
                id1.Visible = false;
                if(pp == null) {
                    pp = ps.LoginPerson;
                }
            } else {    
                pp = ps.LoginPerson;
            }

            profile_images.InnerHtml = "";

            profile_main.Src = "Upload/PersonImage/" + DatabaseManager.GetPersonImageFileName(pp.PS_CITIZEN_ID);

            List<int> ids = new List<int>();
            List<string> urls = new List<string>();

            using(OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING)) {
                con.Open();
                using(OracleCommand com = new OracleCommand("SELECT ID, URL FROM PS_PERSON_IMAGE WHERE CITIZEN_ID = '" + pp.PS_CITIZEN_ID + "'", con)) {
                    using(OracleDataReader reader = com.ExecuteReader()) {
                        while(reader.Read()) {
                            ids.Add(reader.GetInt32(0));
                            urls.Add(reader.GetString(1));
                        }
                    }
                }
            }

            for (int i = 0; i < ids.Count; i++) {
                string path = "Upload/PersonImage/" + urls[i];
                int ID = ids[i];
                string url = urls[i];

                Panel p = new Panel();
                p.Style.Add("display", "inline-block");

                LinkButton lb = new LinkButton();
                lb.Attributes["href"] = path;
                p.Controls.Add(lb);

                Image img = new Image();
                img.Attributes["src"] = path;
                lb.Controls.Add(img);

                profile_images.Controls.Add(p);

                Panel p2 = new Panel();
                p.Controls.Add(p2);

                LinkButton lbSelectImagePresent = new LinkButton();
                lbSelectImagePresent.CssClass = "ps-button";
                lbSelectImagePresent.Text = "<img src='Image/Small/pointer.png' class='icon_left' />เลือก";
                lbSelectImagePresent.Click += (e1, e2) => {
                    DatabaseManager.ExecuteNonQuery("UPDATE PS_PERSON_IMAGE SET PRESENT = 0 WHERE CITIZEN_ID = '" + pp.PS_CITIZEN_ID + "'");
                    DatabaseManager.ExecuteNonQuery("UPDATE PS_PERSON_IMAGE SET PRESENT = 1 WHERE CITIZEN_ID = '" + pp.PS_CITIZEN_ID + "' AND ID = " + ID);
                    Response.Redirect("Profile.aspx");
                };
                if(id1.Visible)
                    p2.Controls.Add(lbSelectImagePresent);

                LinkButton lbDeleteImagePresent = new LinkButton();
                lbDeleteImagePresent.CssClass = "ps-button";
                lbDeleteImagePresent.Text = "<img src='Image/Small/delete.png' class='icon_left' />ลบ";
                lbDeleteImagePresent.Click += (e1, e2) => {
                    FileInfo FileIn = new FileInfo(Server.MapPath("~/Upload/PersonImage/" + url));
                    if (FileIn.Exists) {
                        FileIn.Delete();
                    }
                    DatabaseManager.ExecuteNonQuery("DELETE FROM PS_PERSON_IMAGE WHERE ID = " + ID);
                    Response.Redirect("Profile.aspx");
                };
                if (id1.Visible)
                    p2.Controls.Add(lbDeleteImagePresent);
            }

            lbCitizenID.Text = Util.IsBlank(pp.PS_CITIZEN_ID) ? "-" : pp.PS_CITIZEN_ID ;
            lbTitleID.Text = Util.IsBlank(pp.PS_TITLE_NAME) ? "-" : pp.PS_TITLE_NAME;
            lbFirstName.Text = Util.IsBlank(pp.PS_FIRSTNAME) ? "-" : pp.PS_FIRSTNAME;
            lbLastName.Text = Util.IsBlank(pp.PS_LASTNAME) ? "-" : pp.PS_LASTNAME;
            lbGenderID.Text = Util.IsBlank(pp.PS_GENDER_NAME) ? "-" : pp.PS_GENDER_NAME;
            lbBirthdayDate.Text = Util.IsBlank(pp.PS_BIRTHDAY_DATE.ToString()) ? "-" : pp.PS_BIRTHDAY_DATE.Value.ToLongDateString();
            lbEmail.Text = Util.IsBlank(pp.PS_EMAIL) ? "-" : pp.PS_EMAIL;
            lbHomeAdd.Text = Util.IsBlank(pp.PS_HOMEADD) ? "-" : pp.PS_HOMEADD;
            lbMoo.Text = Util.IsBlank(pp.PS_MOO) ? "-" : pp.PS_MOO;
            lbStreet.Text = Util.IsBlank(pp.PS_STREET) ? "-" : pp.PS_STREET;
            lbProvinceID.Text = Util.IsBlank(pp.PS_PROVINCE_NAME) ? "-" : pp.PS_PROVINCE_NAME;
            lbAmphurID.Text = Util.IsBlank(pp.PS_AMPHUR_NAME) ? "-" : pp.PS_AMPHUR_NAME;
            lbDistrictID.Text = Util.IsBlank(pp.PS_DISTRICT_NAME) ? "-" : pp.PS_DISTRICT_NAME;
            lbZipcode.Text = Util.IsBlank(pp.PS_ZIPCODE) ? "-" : pp.PS_ZIPCODE;
            lbTelephone.Text = Util.IsBlank(pp.PS_TELEPHONE) ? "-" : pp.PS_TELEPHONE;
            lbNationID.Text = Util.IsBlank(pp.PS_NATION_NAME) ? "-" : pp.PS_NATION_NAME;
            lbCampusID.Text = Util.IsBlank(pp.PS_CAMPUS_NAME) ? "-" : pp.PS_CAMPUS_NAME;
            lbFacultyID.Text = Util.IsBlank(pp.PS_FACULTY_NAME) ? "-" : pp.PS_FACULTY_NAME;
            lbDivisionID.Text = Util.IsBlank(pp.PS_DIVISION_NAME) ? "-" : pp.PS_DIVISION_NAME;
            lbWorkDivisionID.Text = Util.IsBlank(pp.PS_WORK_DIVISION_NAME) ? "-" : pp.PS_WORK_DIVISION_NAME;
            lbStafftypeID.Text = Util.IsBlank(pp.PS_STAFFTYPE_NAME) ? "-" : pp.PS_STAFFTYPE_NAME;
            lbTimeContactID.Text = Util.IsBlank(pp.PS_TIME_CONTACT_NAME) ? "-" : pp.PS_TIME_CONTACT_NAME;
            lbBudgetID.Text = Util.IsBlank(pp.PS_BUDGET_NAME) ? "-" : pp.PS_BUDGET_NAME;
            lbSubStafftypeID.Text = Util.IsBlank(pp.PS_SUBSTAFFTYPE_NAME) ? "-" : pp.PS_SUBSTAFFTYPE_NAME;
            lbAdminPosID.Text = Util.IsBlank(pp.PS_ADMIN_POS_NAME) ? "-" : pp.PS_ADMIN_POS_NAME;
            lbPositionID.Text = Util.IsBlank(pp.PS_POSITION_NAME) ? "-" : pp.PS_POSITION_NAME;
            lbWorkPosID.Text = Util.IsBlank(pp.PS_WORK_POS_NAME) ? "-" : pp.PS_WORK_POS_NAME;
            lbDateInwork.Text = Util.IsBlank(pp.PS_INWORK_DATE.ToString()) ? "-" : pp.PS_INWORK_DATE.Value.ToLongDateString();
            lbDateStartThisU.Text = Util.IsBlank(pp.PS_DATE_START_THIS_U.ToString()) ? "-" : pp.PS_DATE_START_THIS_U.Value.ToLongDateString();
            lbSpecialName.Text = Util.IsBlank(pp.PS_SPECIAL_NAME) ? "-" : pp.PS_SPECIAL_NAME;
            lbTeachIscedID.Text = Util.IsBlank(pp.PS_TEACH_ISCED_NAME) ? "-" : pp.PS_TEACH_ISCED_NAME;
            lbGradLevID.Text = Util.IsBlank(pp.PS_GRAD_LEV_NAME) ? "-" : pp.PS_GRAD_LEV_NAME;
            lbGradCurr.Text = Util.IsBlank(pp.PS_GRAD_CURR) ? "-" : pp.PS_GRAD_CURR;
            lbGradIscedID.Text = Util.IsBlank(pp.PS_GRAD_ISCED_NAME) ? "-" : pp.PS_GRAD_ISCED_NAME;
            lbGradProgID.Text = Util.IsBlank(pp.PS_GRAD_PROG_NAME) ? "-" : pp.PS_GRAD_PROG_NAME;
            lbGradUniv.Text = Util.IsBlank(pp.PS_GRAD_UNIV) ? "-" : pp.PS_GRAD_UNIV;
            lbGradCountryID.Text = Util.IsBlank(pp.PS_GRAD_COUNTRY_NAME) ? "-" : pp.PS_GRAD_COUNTRY_NAME;
            lbDeformID.Text = Util.IsBlank(pp.PS_DEFORM_NAME) ? "-" : pp.PS_DEFORM_NAME;
            lbSitNo.Text = Util.IsBlank(pp.PS_SIT_NO) ? "-" : pp.PS_SIT_NO;
            lbReligionID.Text = Util.IsBlank(pp.PS_RELIGION_NAME) ? "-" : pp.PS_RELIGION_NAME;
            lbMovementTypeID.Text = Util.IsBlank(pp.PS_MOVEMENT_TYPE_NAME) ? "-" : pp.PS_MOVEMENT_TYPE_NAME;
            lbMovementDate.Text = Util.IsBlank(pp.PS_MOVEMENT_DATE.ToString()) ? "-" : pp.PS_MOVEMENT_DATE.Value.ToLongDateString();
    
        }

        protected void lbuUploadPicture_Click(object sender, EventArgs e) {
            PersonnelSystem ps = PersonnelSystem.GetPersonnelSystem(this);
            Person pp = ps.LoginPerson;

            if (FileUpload1.HasFile) {
                FileInfo fi = new FileInfo(FileUpload1.FileName);
                string fname = Util.RandomFileName() + fi.Extension;
                FileUpload1.SaveAs(Server.MapPath("~/Upload/PersonImage/" + fname));
                using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING)) {
                    con.Open();
                    using(OracleCommand com = new OracleCommand("INSERT INTO PS_PERSON_IMAGE (ID, CITIZEN_ID, URL, PRESENT) VALUES(SEQ_PERSON_IMAGE_ID.NEXTVAL, :CITIZEN_ID, :URL, :PRESENT)", con)) {
                        com.Parameters.AddWithValue("CITIZEN_ID", pp.PS_CITIZEN_ID);
                        com.Parameters.AddWithValue("URL", fname);
                        int v1 = 0;
                        com.Parameters.AddWithValue("PRESENT", v1);
                        com.ExecuteNonQuery();
                    }
                }
            }
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }

        
    }
}