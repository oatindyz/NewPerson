using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WEB_PERSONAL.Class;
using System.Data.OracleClient;

namespace WEB_PERSONAL {
    public partial class LeaveClaim : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            if(!IsPostBack) {
                PersonnelSystem ps = PersonnelSystem.GetPersonnelSystem(this);
                Person loginPerson = ps.LoginPerson;
                GridView1.DataSource = DatabaseManager.CreateSQLDataSource("SELECT * FROM LEV_CLAIM WHERE PS_CITIZEN_ID = '" + loginPerson.PS_CITIZEN_ID + "'");
                GridView1.DataBind();

                /*using(OleDbConnection con = new OleDbConnection(DatabaseManager.CONNECTION_STRING)) {
                    con.Open();
                    using(OleDbCommand com = new OleDbCommand("SELECT * FROM LEV_CLAIM WHERE PS_CITIZEN_ID = '" + loginPerson.CitizenID + "' AND YEAR = EXTRACT(YEAR FROM CURRENT_DATE)", con)) {
                        using(OleDbDataReader reader = com.ExecuteReader()) {
                            while(reader.Read()) {
                                //lbKij.Text = reader.GetValue(9).ToString() + "/" + reader.GetValue(10).ToString();
                                lbRest.Text = reader.GetValue(11).ToString() + "/" + reader.GetValue(12).ToString();
                                //lbOrdain.Text = reader.GetValue(13).ToString() + "/" + reader.GetValue(14).ToString();
                            }
                        }
                    }
                }*/
            }
        }
    }
}