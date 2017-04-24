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
    public partial class INSG_RequestList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PersonnelSystem ps = PersonnelSystem.GetPersonnelSystem(this);
            Person loginPerson = ps.LoginPerson;
            if (loginPerson.PERSON_ROLE_ID != "4")
            {
                Server.Transfer("NoPermission.aspx");
            }

            if (!IsPostBack)
            {
                BindInsig();
            }
        }

        //
        protected void BindInsig()
        {
            OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING);
            OracleDataAdapter sda = new OracleDataAdapter("SELECT IP_ID,(SELECT PS_FIRSTNAME || ' ' || PS_LASTNAME FROM PS_PERSON WHERE PS_CITIZEN_ID = CITIZEN_ID)PS_NAME, (SELECT INSIG_GRADE_NAME_L FROM TB_INSIG_GRADE WHERE INSIG_GRADE_ID = INSIG_ID) INSIG_NAME, REQ_DATE, GET_DATE, IP_STATUS_ID, (SELECT IP_STATUS_NAME FROM TB_INSIG_PERSON_STATUS WHERE TB_INSIG_PERSON_STATUS.IP_STATUS_ID = TB_INSIG_PERSON.IP_STATUS_ID) StatusName FROM TB_INSIG_PERSON WHERE IP_STATUS_ID = 0 ORDER BY IP_ID DESC", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            myRepeaterInsig.DataSource = dt;
            myRepeaterInsig.DataBind();
            DatabaseManager.BindDropDown(ddlStatusID, "SELECT * FROM TB_INSIG_PERSON_STATUS ORDER BY ABS(IP_STATUS_ID) ASC", "IP_STATUS_NAME", "IP_STATUS_ID", "--กรุณาเลือก--");
        }
        protected void ClearInsig()
        {
            tbNameUser.Text = "";
            tbInsigReq.Text = "";
            tbInsigDateReq.Text = "";
            tbInsertDateInsig.Text = "";
            ddlStatusID.SelectedIndex = 0;
        }

        protected void btnUpdateInsig_Click(object sender, EventArgs e)
        {
            string ValueDateInsig = tbInsertDateInsig.Text;

            if (Session["DefaultIdInsig"] == null)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('กรุณาเลือกรายการที่จะแก้ไขก่อน')", true);
                return;
            }

            if (ValueDateInsig != "")
            {
                OracleConnection.ClearAllPools();
                using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
                {
                    con.Open();
                    using (OracleCommand com = new OracleCommand("UPDATE TB_INSIG_PERSON SET GET_DATE = :GET_DATE, IP_STATUS_ID = :IP_STATUS_ID WHERE IP_ID = :IP_ID", con))
                    {
                        com.Parameters.Add(new OracleParameter("GET_DATE", Util.ToDateTimeOracle(tbInsertDateInsig.Text)));
                        com.Parameters.Add(new OracleParameter("IP_STATUS_ID", ddlStatusID.SelectedValue));
                        com.Parameters.Add(new OracleParameter("IP_ID", Session["DefaultIdInsig"].ToString()));
                        com.ExecuteNonQuery();
                    }
                }

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('อัพเดทข้อมูลเรียบร้อย')", true);
                BindInsig();
                ClearInsig();
                Session.Remove("DefaultIdInsig");
            }
        }
        protected void lbuClearInsig_Click(object sender, EventArgs e)
        {
            BindInsig();
            ClearInsig();
            Session.Remove("DefaultIdInsig");
        }
        protected void OnEditInsig(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            string ValueIPID = (item.FindControl("HFIP_ID") as HiddenField).Value;
            string ValueStatusID = (item.FindControl("HFSTATUS_ID") as HiddenField).Value;
            string ValueInsigName = (item.FindControl("lbInsigName") as Label).Text;
            string ValueInsigReq = (item.FindControl("lbInsigReq") as Label).Text;
            string ValueReqDate = (item.FindControl("lbInsigReqDate") as Label).Text;
            string ValueGetDate = (item.FindControl("lbInsigGetDate") as Label).Text;
            
            tbNameUser.Text = ValueInsigName;
            tbInsigReq.Text = ValueInsigReq;
            tbInsigDateReq.Text = ValueReqDate;
            tbInsertDateInsig.Text = ValueGetDate;
            ddlStatusID.SelectedValue = ValueStatusID;

            Session["DefaultIdInsig"] = ValueIPID;
        }

    }
}