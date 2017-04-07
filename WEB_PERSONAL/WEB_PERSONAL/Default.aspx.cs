using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using WEB_PERSONAL.Class;
using System.Data.OracleClient;

namespace WEB_PERSONAL {
    public partial class Default : System.Web.UI.Page {

        protected void Page_Load(object sender, EventArgs e) {
            /*List<Person> bossList = DatabaseManager.รหัสหัวหน้า(PersonnelSystem.GetPersonnelSystem(this).LoginPerson.CitizenID);
            if(bossList != null) {
                for (int i = 0; i < bossList.Count; i++) {
                    tbOutput.Text += bossList[i].CitizenID + " / " + bossList[i].FirstNameAndLastName + "/" + bossList[i].AdminPositionName + "\n";
                }
            }
            
            PersonnelSystem ps = PersonnelSystem.GetPersonnelSystem(this);
            Person loginPerson = ps.LoginPerson;
            TextBox1.Text = loginPerson.PS_BIRTHDAY_DATE.Value.ToShortDateString();*/
        }
    }
}