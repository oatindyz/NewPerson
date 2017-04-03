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


            lbCitizenID.Text = pp.PS_CITIZEN_ID;
            lbFirstName.Text = pp.PS_FN_TH;
            lbLastName.Text = pp.PS_LN_TH;
            lbGender.Text = pp.PS_GENDER_NAME;
            lbBirthday.Text = pp.PS_BIRTHDAY_DATE.Value.ToLongDateString();
            lbInWorkDay.Text = pp.PS_INWORK_DATE.Value.ToLongDateString();
            lbPosition.Text = pp.PS_WORK_POS_NAME;
            lbAdminPosition.Text = pp.PS_ADMIN_POS_NAME;
            lbWorkDivision.Text = Util.IsBlank(pp.PS_WORK_DIVISION_NAME) ? "-" : pp.PS_WORK_DIVISION_NAME;
            lbDept.Text = pp.PS_DIVISION_NAME;
            lbFaculty.Text = pp.PS_FACULTY_NAME;
            lbCampus.Text = pp.PS_CAMPUS_NAME;
            //lbMinistry.Text = pp.MinistryName;
            //lbGrom.Text = pp.Grom;
            //lbRace.Text = pp.RaceName;
            lbNation.Text = pp.PS_NATION_NAME;
            //lbBlood.Text = pp.BloodName;
            lbEmail.Text = pp.PS_EMAIL;
            lbPhone.Text = pp.PS_TELEPHONE;
            //lbWorkPhone.Text = pp.tele;
            lbReligion.Text = pp.PS_RELIGION_NAME;
            //lbStatusPerson.Text = pp.StatusPersonName;
            //lbStatusWork.Text = pp.StatusName;
            //lbFather.Text = pp.FatherFirstNameAndLastName;
            //lbMother.Text = pp.MotherFirstNameAndLastName;
            //lbCouple.Text = pp.CoupleFirstNameAndLastName;

            lbHomeAdd.Text = pp.PS_HOMEADD;
            //lbSoi.Text = pp.Soi;
            lbMoo.Text = pp.PS_MOO;
            lbStreet.Text = pp.PS_STREET;
            lbProvince.Text = pp.PS_PROVINCE_NAME;
            lbAmphur.Text = pp.PS_AMPHUR_NAME;
            lbDistrict.Text = pp.PS_DISTRICT_NAME;
            lbZipcode.Text = pp.PS_ZIPCODE;
            //lbCountry.Text = pp.PlaceCountryName;
            //lbState.Text = pp.PlaceState;

            /*lbHomeAddNow.Text = pp.HomeAddNow;
            lbSoiNow.Text = pp.SoiNow;
            lbMooNow.Text = pp.MooNow;
            lbStreetNow.Text = pp.StreetNow;
            lbProvinceNow.Text = pp.ProvinceNameNow;
            lbAmphurNow.Text = pp.AmphurNameNow;
            lbDistrictNow.Text = pp.DistrictNameNow;
            lbZipcodeNow.Text = pp.ZipCodeNow;
            lbCountryNow.Text = pp.PlaceCountryNowName;
            lbStateNow.Text = pp.PlaceStateNow;*/

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