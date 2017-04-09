using System;
using System.Collections.Generic;
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
    public partial class INSG_Request : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            PersonnelSystem ps = PersonnelSystem.GetPersonnelSystem(this);
            Person loginPerson = ps.LoginPerson;
            using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
            {
                con.Open();

                //SAL
                int salary = -1;
                using (OracleCommand com = new OracleCommand("SELECT SALARY FROM PS_SALARY WHERE CITIZEN_ID = :CITIZEN_ID ORDER BY DO_DATE DESC", con))
                {
                    com.Parameters.Add(new OracleParameter("CITIZEN_ID", loginPerson.PS_CITIZEN_ID));
                    using (OracleDataReader reader = com.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            salary = int.Parse(reader.GetValue(0).ToString());
                        }
                    }
                }

                //INSIG MIN MAX
                int insig_min = int.MaxValue;
                int insig_max = int.MinValue;
                int sal_row_count = 0;
                using (OracleCommand com = new OracleCommand("SELECT * FROM TB_INSIG_AVAIABLE WHERE P_ID = :P_ID", con))
                {
                    com.Parameters.Add(new OracleParameter("P_ID", loginPerson.PS_POSITION_ID));
                    using (OracleDataReader reader = com.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            insig_min = int.Parse(reader.GetValue(3).ToString());
                            insig_max = int.Parse(reader.GetValue(4).ToString());
                            ++sal_row_count;
                        }
                    }
                }

                if (sal_row_count > 1)
                {
                    insig_min = int.MaxValue;
                    insig_max = int.MinValue;
                    using (OracleCommand com = new OracleCommand("SELECT * FROM TB_INSIG_AVAIABLE WHERE P_ID = :P_ID AND POS_SALARY >= :POS_SALARY", con))
                    {
                        com.Parameters.Add(new OracleParameter("P_ID", loginPerson.PS_POSITION_ID));
                        com.Parameters.Add(new OracleParameter("SALARY", salary));
                        using (OracleDataReader reader = com.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                insig_min = int.Parse(reader.GetValue(3).ToString());
                                insig_max = int.Parse(reader.GetValue(4).ToString());
                            }
                        }
                    }
                }

                //OLD
                int insigOldID = -1;
                int insigNewID = -1;
                bool foundInsig = false;
                using (OracleCommand com = new OracleCommand("SELECT INSIG_ID, INSIG_GRADE_NAME_L, IP_STATUS_ID FROM TB_INSIG_PERSON, TB_INSIG_GRADE WHERE TB_INSIG_PERSON.INSIG_ID = TB_INSIG_GRADE.INSIG_GRADE_ID AND CITIZEN_ID = :CITIZEN_ID ORDER BY INSIG_ID ASC", con))
                {
                    com.Parameters.Add(new OracleParameter("CITIZEN_ID", loginPerson.PS_CITIZEN_ID));
                    using (OracleDataReader reader = com.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader.GetInt32(2) == 3)
                            {

                                insigOldID = int.Parse(reader.GetValue(0).ToString());
                                insigNewID = insigOldID - 1;
                                foundInsig = true;

                                //Pic
                                string fileName;
                                switch (insigOldID)
                                {
                                    case 12: fileName = "บ.ม."; break;
                                    case 11: fileName = "บ.ช."; break;
                                    case 10: fileName = "จ.ม."; break;
                                    case 9: fileName = "จ.ช."; break;
                                    case 8: fileName = "ต.ม."; break;
                                    case 7: fileName = "ต.ช."; break;
                                    case 6: fileName = "ท.ม."; break;
                                    case 5: fileName = "ท.ช."; break;
                                    case 4: fileName = "ป.ม."; break;
                                    case 3: fileName = "ป.ช."; break;
                                    case 2: fileName = "ม.ว.ม."; break;
                                    default: fileName = "ม.ป.ช."; break;
                                }
                                imgOldInsig.Attributes["src"] = "Image/Insignia/" + fileName + ".png";

                                //Name
                                lbOldInsigName.Text = reader.GetString(1);
                                break;
                            }
                        }
                    }

                    MultiView1.ActiveViewIndex = 2;
                }
                if (!foundInsig)
                {
                    insigNewID = insig_min;
                    
                }
                else
                {
                    if (insigNewID > insig_min)
                    {
                        insigNewID = insig_min;
                    }
                }
                lbOldInsigName.Text = insigOldID.ToString();
                lbNewInsigName.Text = insigNewID.ToString();
                //NEW
                if (insigNewID >= insig_max)
                {
                    // Check Insig Year Con
                    bool insigYearCon = true;
                    using (OracleCommand com = new OracleCommand("SELECT * FROM TB_INSIG_GOV_INSIG_YEAR_CON WHERE INSIG_TARGET = :INSIG_TARGET AND P_ID = :P_ID", con))
                    {
                        com.Parameters.Add(new OracleParameter("INSIG_TARGET", insigNewID));
                        com.Parameters.Add(new OracleParameter("P_ID", loginPerson.PS_POSITION_ID));
                        using (OracleDataReader reader = com.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int insigUseID = int.Parse(reader.GetValue(4).ToString());
                                int insigUseYear = int.Parse(reader.GetValue(5).ToString());
                                bool insigPass = false;

                                using (OracleCommand com2 = new OracleCommand("SELECT GET_DATE FROM TB_INSIG_PERSON WHERE CITIZEN_ID = :CITIZEN_ID AND INSIG_ID = :INSIG_ID AND IP_STATUS_ID = 3", con))
                                {
                                    com2.Parameters.Add(new OracleParameter("CITIZEN_ID", loginPerson.PS_CITIZEN_ID));
                                    com2.Parameters.Add(new OracleParameter("INSIG_ID", insigUseID));
                                    using (OracleDataReader reader2 = com2.ExecuteReader())
                                    {
                                        while (reader2.Read())
                                        {
                                            DateTime getDate = reader2.GetDateTime(0);
                                            DateTime currentDate = DateTime.Now;
                                            double year = (currentDate - getDate).TotalDays / 365;

                                            if (year > insigUseYear)
                                            {
                                                insigPass = true;
                                                string Insig_name = "?";
                                                using (OracleCommand com3 = new OracleCommand("SELECT INSIG_GRADE_NAME_S FROM TB_INSIG_GRADE WHERE INSIG_GRADE_ID = :INSIG_GRADE_ID", con))
                                                {
                                                    com3.Parameters.Add(new OracleParameter("INSIG_GRADE_ID", insigUseID));
                                                    using (OracleDataReader reader3 = com3.ExecuteReader())
                                                    {
                                                        while (reader3.Read())
                                                        {
                                                            Insig_name = reader3.GetValue(0).ToString();
                                                        }

                                                    }
                                                }

                                                TableRow row = new TableRow();
                                                TableCell cell = new TableCell();
                                                
                                                cell.ForeColor = System.Drawing.Color.Green;
                                                cell.Text = "ต้องมีระยะเวลาการครองเครื่องราชฯ ชั้น " + Insig_name + " มากกว่า " + insigUseYear + " ปี";

                                                row.Cells.Add(cell);
                                                TableCondition.Rows.Add(row);
                                            }
                                        }
                                    }
                                }

                                if (!insigPass)
                                {
                                    insigYearCon = false;
                                    string Insig_name = "?";
                                    using (OracleCommand com3 = new OracleCommand("SELECT INSIG_GRADE_NAME_S FROM TB_INSIG_GRADE WHERE INSIG_GRADE_ID = :INSIG_GRADE_ID", con))
                                    {
                                        com3.Parameters.Add(new OracleParameter("INSIG_GRADE_ID", insigUseID));
                                        using (OracleDataReader reader3 = com3.ExecuteReader())
                                        {
                                            while (reader3.Read())
                                            {
                                                Insig_name = reader3.GetValue(0).ToString();
                                            }

                                        }
                                    }

                                    TableRow row = new TableRow();
                                    TableCell cell = new TableCell();

                                    cell.ForeColor = System.Drawing.Color.Red;
                                    cell.Text = "ต้องมีระยะเวลาการครองเครื่องราชฯ ชั้น " + Insig_name + " มากกว่า " + insigUseYear + " ปี";

                                    row.Cells.Add(cell);
                                    TableCondition.Rows.Add(row);
                                }
                            }
                        }
                    }

                    // Check Insig Salary Con
                    bool insigSalaryCon = true;
                    using (OracleCommand com = new OracleCommand("SELECT TB_INSIG_GOV_SALARY_CON.*, TB_POSITION.P_SAL_MIN, TB_POSITION.P_NAME FROM TB_INSIG_GOV_SALARY_CON, TB_POSITION WHERE TB_INSIG_GOV_SALARY_CON.P_ID_USE = TB_POSITION.P_ID AND TB_INSIG_GOV_SALARY_CON.P_ID = :P_ID AND INSIG_TARGET = :INSIG_TARGET", con))
                    {
                        com.Parameters.Add(new OracleParameter("P_ID", loginPerson.PS_POSITION_ID));
                        com.Parameters.Add(new OracleParameter("INSIG_TARGET", insigNewID));
                        using (OracleDataReader reader = com.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int salaryUse = int.Parse(reader.GetValue(5).ToString());
                                int salaryCon = int.Parse(reader.GetValue(4).ToString());
                                string posName = reader.GetValue(6).ToString();
                                bool salaryPass = false;

                                using (OracleCommand com2 = new OracleCommand("SELECT SALARY FROM PS_SALARY WHERE CITIZEN_ID = :CITIZEN_ID ORDER BY DO_DATE DESC", con))
                                {
                                    com2.Parameters.Add(new OracleParameter("CITIZEN_ID", loginPerson.PS_CITIZEN_ID));
                                    using (OracleDataReader reader2 = com2.ExecuteReader())
                                    {
                                        while (reader2.Read())
                                        {

                                            TableRow row = new TableRow();
                                            TableCell cell = new TableCell();

                                            int salaryPS = int.Parse(reader2.GetValue(0).ToString());
                                            if (salaryCon == 1)
                                            {
                                                if (salaryPS < salaryUse)
                                                {
                                                    salaryPass = true;
                                                    cell.ForeColor = System.Drawing.Color.Green;
                                                }
                                                else
                                                {
                                                    cell.ForeColor = System.Drawing.Color.Red;
                                                }
                                                cell.Text = "ได้รับเงินเดือนต่ำกว่าขั้นต่ำของ" + posName + " (" + salaryUse + ")";
                                            }
                                            else if (salaryCon == 2)
                                            {
                                                if (salaryPS > salaryUse)
                                                {
                                                    salaryPass = true;
                                                    cell.ForeColor = System.Drawing.Color.Green;
                                                }
                                                else
                                                {
                                                    cell.ForeColor = System.Drawing.Color.Red;
                                                }
                                                cell.Text = "ได้รับเงินเดือนไม่ต่ำกว่าขั้นต่ำของ" + posName + " (" + salaryUse + ")";
                                            }

                                            row.Cells.Add(cell);
                                            TableCondition.Rows.Add(row);
                                        }
                                    }
                                }

                                if (!salaryPass)
                                {
                                    insigSalaryCon = false;

                                }
                            }
                        }
                    }

                    // Check Insig Pos Year Con
                    bool insigPosYearCon = true;
                    using (OracleCommand com = new OracleCommand("SELECT * FROM TB_INSIG_GOV_POS_YEAR_CON WHERE INSIG_TARGET = :INSIG_TARGET AND P_ID = :P_ID", con))
                    {
                        com.Parameters.Add(new OracleParameter("INSIG_TARGET", insigNewID));
                        com.Parameters.Add(new OracleParameter("P_ID", loginPerson.PS_POSITION_ID));
                        using (OracleDataReader reader = com.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int posUseID = int.Parse(reader.GetValue(3).ToString());
                                int posUseYear = int.Parse(reader.GetValue(4).ToString());
                                bool posPass = false;

                                using (OracleCommand com2 = new OracleCommand("SELECT GET_DATE FROM PS_POSITION_HISTORY WHERE CITIZEN_ID = :CITIZEN_ID AND P_ID = :P_ID", con))
                                {
                                    com2.Parameters.Add(new OracleParameter("CITIZEN_ID", loginPerson.PS_CITIZEN_ID));
                                    com2.Parameters.Add(new OracleParameter("P_ID", loginPerson.PS_POSITION_ID));
                                    using (OracleDataReader reader2 = com2.ExecuteReader())
                                    {
                                        while (reader2.Read())
                                        {
                                            DateTime getDate = reader2.GetDateTime(0);
                                            DateTime currentDate = DateTime.Now;
                                            double year = (currentDate - getDate).TotalDays / 365;
                                            if (year > posUseYear)
                                            {
                                                posPass = true;
                                            }
                                        }
                                    }
                                }

                                if (!posPass)
                                {
                                    insigPosYearCon = false;
                                }
                            }
                        }
                    }

                    // Check Insig Salary Year Con
                    bool insigSalaryYearCon = true;
                    using (OracleCommand com = new OracleCommand("SELECT GET_YEAR FROM TB_INSIG_GOV_SALARY_YEAR_CON WHERE P_ID_USE = :P_ID_USE", con))
                    {
                        com.Parameters.Add(new OracleParameter("P_ID_USE", loginPerson.PS_POSITION_ID));
                        using (OracleDataReader reader = com.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int yearUse = int.Parse(reader.GetValue(0).ToString());
                                bool yearPass = false;

                                using (OracleCommand com2 = new OracleCommand("SELECT DO_DATE FROM PS_SALARY WHERE CITIZEN_ID = :CITIZEN_ID AND :P_ID = :P_ID ORDER BY DO_DATE DESC", con))
                                {
                                    com2.Parameters.Add(new OracleParameter("CITIZEN_ID", loginPerson.PS_CITIZEN_ID));
                                    com2.Parameters.Add(new OracleParameter("P_ID", loginPerson.PS_POSITION_ID));
                                    using (OracleDataReader reader2 = com2.ExecuteReader())
                                    {
                                        while (reader2.Read())
                                        {
                                            DateTime getDate = reader2.GetDateTime(0);
                                            DateTime currentDate = DateTime.Now;
                                            double year = (currentDate - getDate).TotalDays / 365;
                                            if (year > yearUse)
                                            {
                                                yearPass = true;
                                            }
                                        }
                                    }
                                }

                                if (!yearPass)
                                {
                                    insigSalaryYearCon = false;
                                }
                            }
                        }
                    }

                    // Check Insig High Salary Con //////////////////////////////
                    bool insigHighSalaryCon = true;
                    using (OracleCommand com = new OracleCommand("SELECT TB_INSIG_GOV_SALARY_CON.*, TB_POSITION.P_SAL_MAX FROM TB_INSIG_GOV_SALARY_CON, TB_POSITION WHERE TB_INSIG_GOV_SALARY_CON.P_ID_USE = TB_POSITION.P_ID AND P_ID_USE = :P_ID_USE", con))
                    {
                        com.Parameters.Add(new OracleParameter("P_ID_USE", loginPerson.PS_POSITION_ID));
                        using (OracleDataReader reader = com.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int salaryMaxUse = int.Parse(reader.GetValue(5).ToString());
                                bool salaryPass = false;

                                using (OracleCommand com2 = new OracleCommand("SELECT SALARY FROM PS_SALARY WHERE CITIZEN_ID = :CITIZEN_ID ORDER BY DO_DATE DESC", con))
                                {
                                    com2.Parameters.Add(new OracleParameter("CITIZEN_ID", loginPerson.PS_CITIZEN_ID));
                                    using (OracleDataReader reader2 = com2.ExecuteReader())
                                    {
                                        while (reader2.Read())
                                        {
                                            int salaryPS = int.Parse(reader2.GetValue(0).ToString());
                                            if (salaryPS > salaryMaxUse)
                                            {
                                                salaryPass = true;
                                            }
                                        }
                                    }
                                }

                                if (!salaryPass)
                                {
                                    insigHighSalaryCon = false;
                                }
                            }
                        }
                    }
                    // New Image
                    using (OracleCommand com = new OracleCommand("SELECT INSIG_GRADE_ID, INSIG_GRADE_NAME_L FROM TB_INSIG_GRADE WHERE INSIG_GRADE_ID = :AAA", con))
                    {
                        com.Parameters.Add(new OracleParameter("AAA", insigNewID));
                        using (OracleDataReader reader = com.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                //Pic
                                string fileName;
                                switch (insigNewID)
                                {
                                    case 12: fileName = "บ.ม."; break;
                                    case 11: fileName = "บ.ช."; break;
                                    case 10: fileName = "จ.ม."; break;
                                    case 9: fileName = "จ.ช."; break;
                                    case 8: fileName = "ต.ม."; break;
                                    case 7: fileName = "ต.ช."; break;
                                    case 6: fileName = "ท.ม."; break;
                                    case 5: fileName = "ท.ช."; break;
                                    case 4: fileName = "ป.ม."; break;
                                    case 3: fileName = "ป.ช."; break;
                                    case 2: fileName = "ม.ว.ม."; break;
                                    default: fileName = "ม.ป.ช."; break;
                                }
                                imgNewInsig.Attributes["src"] = "Image/Insignia/" + fileName + ".png";

                                //Name
                                lbNewInsigName.Text = reader.GetString(1);
                                break;

                            }
                        }
                    }

                    // Final
                    if (insigYearCon && insigSalaryCon && insigPosYearCon && insigSalaryYearCon && insigHighSalaryCon)
                    {

                    }else
                    {
                        lbuSubmitView2.Visible = false;
                    }
                }
            }

            /*
            PersonnelSystem ps = PersonnelSystem.GetPersonnelSystem(this);
            Person loginPerson = ps.LoginPerson;
            using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
            {
                con.Open();
                using (OracleCommand com = new OracleCommand("SELECT COUNT(*) FROM TB_INSIG_REQUEST WHERE IR_STATUS = 1 AND IR_CITIZEN_ID = '" + loginPerson.PS_CITIZEN_ID + "'", con))
                {
                    using (OracleDataReader reader = com.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader.GetInt32(0) == 0)
                            {
                                return;
                            }
                        }
                    }
                }
            }*/

            /*lbRank.Text = loginPerson.RankName;
            lbTitleName.Text = loginPerson.TitleName;
            lbName.Text = loginPerson.FirstName;
            lbLastName.Text = loginPerson.LastName;
            lbGender.Text = loginPerson.GenderName;
            lbBirthDate.Text = loginPerson.BirthDate.Value.ToLongDateString();
            lbDateInwork.Text = loginPerson.InWorkDate.Value.ToLongDateString();
            lbPosiDateInwork.Text = loginPerson.StartPositionWorkName;
            lbDegreeDateInwork.Text = loginPerson.StartAdminPositionName;
            lbPositionCurrent.Text = loginPerson.PositionWorkName;
            lbType.Text = loginPerson.StaffTypeName;
            lbDegree.Text = loginPerson.AdminPositionName;
            lbSalaryCurrent.Text = loginPerson.Salary.ToString();
            lbPositionSalary.Text = loginPerson.PositionSalary;*/

            /*lbCitizen.Text = loginPerson.PS_CITIZEN_ID;

            int รหัสเครื่องราชปัจจุบัน = 0;
            int รหัสเครื่องราชทที่ขอ = 0;
            string ชื่อเครื่องราชปัจจุบัน = "-";
            string ชื่อเครื่องราชที่ขอ = "-";
            OracleConnection.ClearAllPools();
            using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
            {
                con.Open();
                using (OracleCommand com = new OracleCommand("SELECT IR_INSIG_ID FROM TB_INSIG_REQUEST WHERE IR_GET_STATUS = 1 AND IR_CITIZEN_ID = '" + loginPerson.PS_CITIZEN_ID + "' ORDER BY IR_ID DESC", con))
                {
                    using (OracleDataReader reader = com.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            รหัสเครื่องราชปัจจุบัน = reader.GetInt32(0);
                        }
                    }
                }
                using (OracleCommand com = new OracleCommand("SELECT IR_INSIG_ID FROM TB_INSIG_REQUEST WHERE IR_STATUS = 1 AND IR_CITIZEN_ID = '" + loginPerson.PS_CITIZEN_ID + "'", con))
                {
                    using (OracleDataReader reader = com.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            รหัสเครื่องราชทที่ขอ = reader.GetInt32(0);
                        }
                    }
                }
                using (OracleCommand com = new OracleCommand("SELECT NAME_GRADEINSIGNIA_THA FROM INS_GRADEINSIGNIA WHERE ID_GRADEINSIGNIA = " + รหัสเครื่องราชปัจจุบัน, con))
                {
                    using (OracleDataReader reader = com.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ชื่อเครื่องราชปัจจุบัน = reader.GetString(0);
                        }
                    }
                }
                using (OracleCommand com = new OracleCommand("SELECT NAME_GRADEINSIGNIA_THA FROM INS_GRADEINSIGNIA WHERE ID_GRADEINSIGNIA = " + รหัสเครื่องราชทที่ขอ, con))
                {
                    using (OracleDataReader reader = com.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ชื่อเครื่องราชที่ขอ = reader.GetString(0);
                        }
                    }
                }


            }

            if (รหัสเครื่องราชปัจจุบัน == 0)
            {
                imgOldInsig.Visible = false;
            }
            else
            {
                string fileName;
                switch (รหัสเครื่องราชปัจจุบัน)
                {
                    case 12: fileName = "บ.ม."; break;
                    case 11: fileName = "บ.ช."; break;
                    case 10: fileName = "จ.ม."; break;
                    case 9: fileName = "จ.ช."; break;
                    case 8: fileName = "ต.ม."; break;
                    case 7: fileName = "ต.ช."; break;
                    case 6: fileName = "ท.ม."; break;
                    case 5: fileName = "ท.ช."; break;
                    case 4: fileName = "ป.ม."; break;
                    case 3: fileName = "ป.ช."; break;
                    case 2: fileName = "ม.ว.ม."; break;
                    default: fileName = "ม.ป.ช."; break;
                }
                imgOldInsig.Attributes["src"] = "Image/Insignia/" + fileName + ".png";
            }

            if (รหัสเครื่องราชทที่ขอ == 0)
            {
                imgNewInsig.Visible = false;
            }
            else
            {
                string fileName;
                switch (รหัสเครื่องราชทที่ขอ)
                {
                    case 12: fileName = "บ.ม."; break;
                    case 11: fileName = "บ.ช."; break;
                    case 10: fileName = "จ.ม."; break;
                    case 9: fileName = "จ.ช."; break;
                    case 8: fileName = "ต.ม."; break;
                    case 7: fileName = "ต.ช."; break;
                    case 6: fileName = "ท.ม."; break;
                    case 5: fileName = "ท.ช."; break;
                    case 4: fileName = "ป.ม."; break;
                    case 3: fileName = "ป.ช."; break;
                    case 2: fileName = "ม.ว.ม."; break;
                    default: fileName = "ม.ป.ช."; break;
                }
                imgNewInsig.Attributes["src"] = "Image/Insignia/" + fileName + ".png";
            }

            lbOldInsigName.Text = ชื่อเครื่องราชปัจจุบัน;
            lbNewInsigName.Text = ชื่อเครื่องราชที่ขอ;*/

            MultiView1.ActiveViewIndex = 1;
        }

        protected void lbuSubmitView2_Click(object sender, EventArgs e)
        {

            /*PersonnelSystem ps = PersonnelSystem.GetPersonnelSystem(this);
            Person loginPerson = ps.LoginPerson;

            using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING))
            {
                con.Open();
                using (OracleCommand command = new OracleCommand("UPDATE TB_INSIG_REQUEST SET IR_STATUS = :IR_STATUS, IR_CITIZEN_ID = :IR_CITIZEN_ID, IR_DATE_START = :IR_DATE_START, IR_RANK = :IR_RANK, IR_TITLE = :IR_TITLE, IR_NAME = :IR_NAME, IR_LASTNAME = :IR_LASTNAME, IR_GENDER = :IR_GENDER, IR_BIRTHDATE = :IR_BIRTHDATE, IR_DATE_INWORK = :IR_DATE_INWORK, IR_START_POSITION = :IR_START_POSITION, IR_START_DEGREE = :IR_START_DEGREE, IR_CURRENT_POSITION = :IR_CURRENT_POSITION, IR_TYPE = :IR_TYPE, IR_DEGREE = :IR_DEGREE, IR_CURRENT_SALARY = :IR_CURRENT_SALARY, IR_POSITION_SALARY = :IR_POSITION_SALARY WHERE IR_CITIZEN_ID = '" + loginPerson.PS_CITIZEN_ID + "' AND IR_STATUS = 1", con))
                {
                    command.Parameters.Add(new OracleParameter("IR_STATUS", 2));
                    command.Parameters.Add(new OracleParameter("IR_CITIZEN_ID", lbCitizen.Text));
                    command.Parameters.Add(new OracleParameter("IR_DATE_START", DateTime.Now));
                    command.Parameters.Add(new OracleParameter("IR_RANK", lbRank.Text));
                    command.Parameters.Add(new OracleParameter("IR_TITLE", lbTitleName.Text));
                    command.Parameters.Add(new OracleParameter("IR_NAME", lbName.Text));
                    command.Parameters.Add(new OracleParameter("IR_LASTNAME", lbLastName.Text));
                    command.Parameters.Add(new OracleParameter("IR_GENDER", lbGender.Text));
                    command.Parameters.Add(new OracleParameter("IR_BIRTHDATE", Util.ToDateTimeOracle(Util.ToShortMonth(lbBirthDate.Text))));
                    command.Parameters.Add(new OracleParameter("IR_DATE_INWORK", Util.ToDateTimeOracle(Util.ToShortMonth(lbDateInwork.Text))));
                    command.Parameters.Add(new OracleParameter("IR_START_POSITION", lbPosiDateInwork.Text));
                    command.Parameters.Add(new OracleParameter("IR_START_DEGREE", lbDegreeDateInwork.Text));
                    command.Parameters.Add(new OracleParameter("IR_CURRENT_POSITION", lbPositionCurrent.Text));
                    command.Parameters.Add(new OracleParameter("IR_TYPE", lbType.Text));
                    command.Parameters.Add(new OracleParameter("IR_DEGREE", lbDegree.Text));
                    command.Parameters.Add(new OracleParameter("IR_CURRENT_SALARY", lbSalaryCurrent.Text));
                    command.Parameters.Add(new OracleParameter("IR_POSITION_SALARY", lbPositionSalary.Text));
                    command.ExecuteNonQuery();
                    MultiView1.ActiveViewIndex = 2;
                }


            }*/
        }

    }
}