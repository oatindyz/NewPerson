using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WEB_PERSONAL.Class;
using System.Data.OracleClient;

namespace WEB_PERSONAL.Class {

    public class InsigCheckGet {

        

        public static bool Check(Person loginPerson) {

            int insigOldID;
            int insigNewID;

            if (loginPerson.PS_STAFFTYPE_ID == "3") {
                string SalaryJa1 = DatabaseManager.ExecuteString("SELECT SALARY FROM PS_SALARY WHERE CITIZEN_ID = '" + loginPerson.PS_CITIZEN_ID + "'");
                if (string.IsNullOrEmpty(SalaryJa1)) {
                    return false;
                }
            }
            if (loginPerson.PS_STAFFTYPE_ID == "5") {
                if (string.IsNullOrEmpty(loginPerson.PS_POSITION_ID)) {
                    return false;
                }
            }
            if (loginPerson.PS_STAFFTYPE_ID == "1") {
                string SalaryJa = DatabaseManager.ExecuteString("SELECT SALARY FROM PS_SALARY WHERE CITIZEN_ID = '" + loginPerson.PS_CITIZEN_ID + "'");
                if (string.IsNullOrEmpty(loginPerson.PS_POSITION_ID) || string.IsNullOrEmpty(SalaryJa)) {
                    return false;
                }
            }


            using (OracleConnection con = new OracleConnection(DatabaseManager.CONNECTION_STRING)) {
                con.Open();

                //SAL
                int salary = -1;
                int positionsalary = -1;
                using (OracleCommand com = new OracleCommand("SELECT SALARY,POSITION_SALARY FROM PS_SALARY WHERE CITIZEN_ID = :CITIZEN_ID ORDER BY DO_DATE DESC", con)) {
                    com.Parameters.Add(new OracleParameter("CITIZEN_ID", loginPerson.PS_CITIZEN_ID));
                    using (OracleDataReader reader = com.ExecuteReader()) {
                        if (reader.Read()) {
                            salary = reader.IsDBNull(0) ? -1 : int.Parse(reader.GetValue(0).ToString());
                            positionsalary = reader.IsDBNull(1) ? -1 : int.Parse(reader.GetValue(1).ToString());
                        }
                    }
                }

                //INSIG MIN MAX
                int insig_min = int.MaxValue;
                int insig_max = int.MinValue;
                bool checkInsigPosYearSal = false;
                int sal_row_count = 0;

                if (loginPerson.PS_STAFFTYPE_ID == "1") {
                    using (OracleCommand com = new OracleCommand("SELECT * FROM TB_INSIG_GOV_AVAILABLE WHERE P_ID = :P_ID", con)) {
                        com.Parameters.Add(new OracleParameter("P_ID", loginPerson.PS_POSITION_ID));
                        using (OracleDataReader reader = com.ExecuteReader()) {
                            while (reader.Read()) {
                                insig_min = int.Parse(reader.GetValue(3).ToString());
                                insig_max = int.Parse(reader.GetValue(4).ToString());
                                ++sal_row_count;
                            }
                        }
                    }
                    if (sal_row_count > 1) {
                        insig_min = int.MaxValue;
                        insig_max = int.MinValue;
                        checkInsigPosYearSal = true;
                        using (OracleCommand com = new OracleCommand("SELECT * FROM TB_INSIG_GOV_AVAILABLE WHERE P_ID = :P_ID AND POS_SALARY >= :POS_SALARY", con)) {
                            com.Parameters.Add(new OracleParameter("P_ID", loginPerson.PS_POSITION_ID));
                            com.Parameters.Add(new OracleParameter("POS_SALARY", positionsalary));
                            using (OracleDataReader reader = com.ExecuteReader()) {
                                while (reader.Read()) {
                                    insig_min = int.Parse(reader.GetValue(3).ToString());
                                    insig_max = int.Parse(reader.GetValue(4).ToString());

                                }
                            }
                        }
                    }
                } else if (loginPerson.PS_STAFFTYPE_ID == "3") {
                    using (OracleCommand com = new OracleCommand("SELECT * FROM TB_INSIG_EMP_AVAILABLE WHERE :SAL >= SALARY_MIN AND :SAL <= SALARY_MAX", con)) {
                        com.Parameters.Add(new OracleParameter("SAL", salary));
                        using (OracleDataReader reader = com.ExecuteReader()) {
                            while (reader.Read()) {
                                insig_min = int.Parse(reader.GetValue(3).ToString());
                                insig_max = int.Parse(reader.GetValue(4).ToString());
                                ++sal_row_count;
                            }
                        }
                    }
                } else if (loginPerson.PS_STAFFTYPE_ID == "5") {
                    using (OracleCommand com = new OracleCommand("SELECT * FROM TB_INSIG_GOVEMP_AVAILABLE WHERE P_ID = :P_ID", con)) {
                        com.Parameters.Add(new OracleParameter("P_ID", loginPerson.PS_POSITION_ID));
                        using (OracleDataReader reader = com.ExecuteReader()) {
                            while (reader.Read()) {
                                insig_min = int.Parse(reader.GetValue(2).ToString());
                                insig_max = int.Parse(reader.GetValue(3).ToString());
                                ++sal_row_count;
                            }
                        }
                    }
                }



                //Is Requesting
                bool insigRequest = false;
                using (OracleCommand com = new OracleCommand("SELECT * FROM TB_INSIG_PERSON WHERE IP_STATUS_ID = 1 AND CITIZEN_ID = :CITIZEN_ID", con)) {
                    com.Parameters.Add(new OracleParameter("CITIZEN_ID", loginPerson.PS_CITIZEN_ID));
                    using (OracleDataReader reader = com.ExecuteReader()) {
                        if (reader.Read()) {
                            insigRequest = true;
                        }
                    }
                }


                //OLD
                insigOldID = -1;
                insigNewID = -1;
                bool foundInsig = false;
                using (OracleCommand com = new OracleCommand("SELECT INSIG_ID, INSIG_GRADE_NAME_L, IP_STATUS_ID FROM TB_INSIG_PERSON, TB_INSIG_GRADE WHERE TB_INSIG_PERSON.INSIG_ID = TB_INSIG_GRADE.INSIG_GRADE_ID AND CITIZEN_ID = :CITIZEN_ID ORDER BY ABS(INSIG_ID) ASC", con)) {
                    com.Parameters.Add(new OracleParameter("CITIZEN_ID", loginPerson.PS_CITIZEN_ID));
                    using (OracleDataReader reader = com.ExecuteReader()) {
                        while (reader.Read()) {
                            if (reader.GetInt32(2) == 3) {
                                insigOldID = int.Parse(reader.GetValue(0).ToString());
                                insigNewID = insigOldID - 1;
                                foundInsig = true;

                                
                                break;
                            }
                        }
                    }
                }
                if (!foundInsig) {
                    insigNewID = insig_min;

                } else {
                    if (insigNewID > insig_min) {
                        insigNewID = insig_min;
                    }
                }



                //NEW

                if (loginPerson.PS_STAFFTYPE_ID == "1") {  //-------------------------------------------------------------------------------------------------------------



                    if (insigNewID >= insig_max) {

                        // Check Inwork Year

                        bool inWorkYearCon = true;
                        {
                            DateTime currentDate = DateTime.Now;
                            DateTime inworkDate = loginPerson.PS_INWORK_DATE.Value;
                            double year = (new DateTime(DateTime.Now.Year, 10, 6) - inworkDate).TotalDays / 365;
                            
                            if (year < 5) {    
                                inWorkYearCon = false;
                            } 
                        }


                        // Check Insig Year Con
                        bool insigYearCon = true;
                        string insigYearConSql;
                        if (checkInsigPosYearSal) {
                            insigYearConSql = "SELECT * FROM TB_INSIG_GOV_INSIG_YEAR_CON WHERE INSIG_TARGET = :INSIG_TARGET AND P_ID = :P_ID AND SALARY = :SALARY";
                        } else {
                            insigYearConSql = "SELECT * FROM TB_INSIG_GOV_INSIG_YEAR_CON WHERE INSIG_TARGET = :INSIG_TARGET AND P_ID = :P_ID";
                        }
                        using (OracleCommand com = new OracleCommand(insigYearConSql, con)) {
                            com.Parameters.Add(new OracleParameter("INSIG_TARGET", insigNewID));
                            com.Parameters.Add(new OracleParameter("P_ID", loginPerson.PS_POSITION_ID));
                            if (checkInsigPosYearSal) {
                                com.Parameters.Add(new OracleParameter("SALARY", positionsalary));
                            }
                            using (OracleDataReader reader = com.ExecuteReader()) {
                                while (reader.Read()) {
                                    int insigUseID = int.Parse(reader.GetValue(4).ToString());
                                    int insigUseYear = int.Parse(reader.GetValue(5).ToString());
                                    bool insigPass = false;

                                    using (OracleCommand com2 = new OracleCommand("SELECT GET_DATE FROM TB_INSIG_PERSON WHERE CITIZEN_ID = :CITIZEN_ID AND INSIG_ID = :INSIG_ID AND IP_STATUS_ID = 3", con)) {
                                        com2.Parameters.Add(new OracleParameter("CITIZEN_ID", loginPerson.PS_CITIZEN_ID));
                                        com2.Parameters.Add(new OracleParameter("INSIG_ID", insigUseID));
                                        using (OracleDataReader reader2 = com2.ExecuteReader()) {
                                            while (reader2.Read()) {
                                                DateTime getDate = reader2.GetDateTime(0);
                                                DateTime currentDate = DateTime.Now;
                                                double year = (currentDate - getDate).TotalDays / 365;

                                                if (year >= insigUseYear) {
                                                    insigPass = true;
                                                    string Insig_name = "?";
                                                    using (OracleCommand com3 = new OracleCommand("SELECT INSIG_GRADE_NAME_S FROM TB_INSIG_GRADE WHERE INSIG_GRADE_ID = :INSIG_GRADE_ID", con)) {
                                                        com3.Parameters.Add(new OracleParameter("INSIG_GRADE_ID", insigUseID));
                                                        using (OracleDataReader reader3 = com3.ExecuteReader()) {
                                                            while (reader3.Read()) {
                                                                Insig_name = reader3.GetValue(0).ToString();
                                                            }

                                                        }
                                                    }

                                                }
                                            }
                                        }
                                    }

                                    if (!insigPass) {
                                        insigYearCon = false;
                                        string Insig_name = "?";
                                        using (OracleCommand com3 = new OracleCommand("SELECT INSIG_GRADE_NAME_S FROM TB_INSIG_GRADE WHERE INSIG_GRADE_ID = :INSIG_GRADE_ID", con)) {
                                            com3.Parameters.Add(new OracleParameter("INSIG_GRADE_ID", insigUseID));
                                            using (OracleDataReader reader3 = com3.ExecuteReader()) {
                                                while (reader3.Read()) {
                                                    Insig_name = reader3.GetValue(0).ToString();
                                                }

                                            }
                                        }


                                    }
                                }
                            }
                        }

                        // Check Insig Salary Con
                        bool insigSalaryCon = true;
                        using (OracleCommand com = new OracleCommand("SELECT TB_INSIG_GOV_SALARY_CON.*, TB_POSITION.P_SAL_MIN, TB_POSITION.P_NAME FROM TB_INSIG_GOV_SALARY_CON, TB_POSITION WHERE TB_INSIG_GOV_SALARY_CON.P_ID_USE = TB_POSITION.P_ID AND TB_INSIG_GOV_SALARY_CON.P_ID = :P_ID AND INSIG_TARGET = :INSIG_TARGET", con)) {
                            com.Parameters.Add(new OracleParameter("P_ID", loginPerson.PS_POSITION_ID));
                            com.Parameters.Add(new OracleParameter("INSIG_TARGET", insigNewID));
                            using (OracleDataReader reader = com.ExecuteReader()) {
                                while (reader.Read()) {
                                    int salaryUse = int.Parse(reader.GetValue(5).ToString());
                                    int salaryCon = int.Parse(reader.GetValue(4).ToString());
                                    string posName = reader.GetValue(6).ToString();
                                    bool salaryPass = false;

                                    using (OracleCommand com2 = new OracleCommand("SELECT SALARY FROM PS_SALARY WHERE CITIZEN_ID = :CITIZEN_ID ORDER BY DO_DATE DESC", con)) {
                                        com2.Parameters.Add(new OracleParameter("CITIZEN_ID", loginPerson.PS_CITIZEN_ID));
                                        using (OracleDataReader reader2 = com2.ExecuteReader()) {
                                            if (reader2.Read()) {

                                                int salaryPS = int.Parse(reader2.GetValue(0).ToString());
                                                if (salaryCon == 1) {
                                                    if (salaryPS < salaryUse) {
                                                        salaryPass = true;

                                                    }
                                                    
                                                } else if (salaryCon == 2) {
                                                    if (salaryPS >= salaryUse) {
                                                        salaryPass = true;
                                                    
                                                    }
                                                    
                                                }

                                                
                                            }
                                        }
                                    }

                                    if (!salaryPass) {
                                        insigSalaryCon = false;

                                    }
                                }
                            }
                        }

                        // Check Insig Pos  
                        bool insigPosYearCon = true;
                        using (OracleCommand com = new OracleCommand("SELECT * FROM TB_INSIG_GOV_POS_YEAR_CON WHERE INSIG_TARGET = :INSIG_TARGET AND P_ID = :P_ID", con)) {
                            com.Parameters.Add(new OracleParameter("INSIG_TARGET", insigNewID));
                            com.Parameters.Add(new OracleParameter("P_ID", loginPerson.PS_POSITION_ID));
                            using (OracleDataReader reader = com.ExecuteReader()) {
                                while (reader.Read()) {
                                    int posUseID = int.Parse(reader.GetValue(3).ToString());
                                    int posUseYear = int.Parse(reader.GetValue(4).ToString());

                                    bool posPass = false;

                                    using (OracleCommand com2 = new OracleCommand("SELECT GET_DATE FROM PS_POSITION_HISTORY WHERE CITIZEN_ID = :CITIZEN_ID AND P_ID = :P_ID", con)) {
                                        com2.Parameters.Add(new OracleParameter("CITIZEN_ID", loginPerson.PS_CITIZEN_ID));
                                        com2.Parameters.Add(new OracleParameter("P_ID", loginPerson.PS_POSITION_ID));
                                        using (OracleDataReader reader2 = com2.ExecuteReader()) {
                                            while (reader2.Read()) {
                                                DateTime getDate = reader2.GetDateTime(0);
                                                DateTime currentDate = DateTime.Now;
                                                double year = (currentDate - getDate).TotalDays / 365;

                                                string posName = DatabaseManager.ExecuteString("SELECT P_NAME FROM TB_POSITION WHERE TB_POSITION.P_ID = " + posUseID);

                                                if (year >= posUseYear) {
                                                    posPass = true;
                                                }
                                            }
                                        }
                                    }

                                    if (!posPass) {
                                        insigPosYearCon = false;
                                    }
                                }
                            }
                        }

                        // Check Insig Salary Year Con
                        bool insigSalaryYearCon = true;
                        using (OracleCommand com = new OracleCommand("SELECT GET_YEAR, P_ID_USE FROM TB_INSIG_GOV_SALARY_YEAR_CON WHERE P_ID = :P_ID AND INSIG_TARGET = :INSIG_TARGET", con)) {
                            com.Parameters.Add(new OracleParameter("P_ID", loginPerson.PS_POSITION_ID));
                            com.Parameters.Add(new OracleParameter("INSIG_TARGET", insigNewID));
                            using (OracleDataReader reader = com.ExecuteReader()) {
                                while (reader.Read()) {
                                    string posUse = reader.GetValue(0).ToString();
                                    int yearUse = int.Parse(reader.GetValue(0).ToString());
                                    bool yearPass = false;

                                    using (OracleCommand com2 = new OracleCommand("SELECT DO_DATE FROM PS_SALARY WHERE CITIZEN_ID = :CITIZEN_ID AND :P_ID = :P_ID ORDER BY DO_DATE DESC", con)) {
                                        com2.Parameters.Add(new OracleParameter("CITIZEN_ID", loginPerson.PS_CITIZEN_ID));
                                        com2.Parameters.Add(new OracleParameter("P_ID", loginPerson.PS_POSITION_ID));
                                        using (OracleDataReader reader2 = com2.ExecuteReader()) {
                                            if (reader2.Read()) {

                                                DateTime getDate = reader2.GetDateTime(0);
                                                DateTime currentDate = DateTime.Now;
                                                double year = (currentDate - getDate).TotalDays / 365;

                                                //--
                                                string posName = DatabaseManager.ExecuteString("SELECT P_NAME FROM TB_POSITION WHERE TB_POSITION.P_ID = " + posUse);

                                                //--

                                                if (year >= yearUse) {
                                                    yearPass = true;
                                                }
                                            }
                                        }
                                    }

                                    if (!yearPass) {
                                        insigSalaryYearCon = false;
                                    }
                                }
                            }
                        }

                        // Check Insig High Salary Con //////////////////////////////
                        bool insigHighSalaryCon = true;
                        using (OracleCommand com = new OracleCommand("SELECT TB_INSIG_GOV_HIGH_SALARY_CON.*, TB_POSITION.P_SAL_MAX FROM TB_INSIG_GOV_HIGH_SALARY_CON, TB_POSITION WHERE TB_INSIG_GOV_HIGH_SALARY_CON.P_ID_USE = TB_POSITION.P_ID AND TB_INSIG_GOV_HIGH_SALARY_CON.P_ID = :P_ID AND TB_INSIG_GOV_HIGH_SALARY_CON.INSIG_TARGET = :INSIG_TARGET", con)) {
                            com.Parameters.Add(new OracleParameter("P_ID", loginPerson.PS_POSITION_ID));
                            com.Parameters.Add(new OracleParameter("INSIG_TARGET", insigNewID));
                            using (OracleDataReader reader = com.ExecuteReader()) {
                                while (reader.Read()) {
                                    int salaryMaxUse = int.Parse(reader.GetValue(4).ToString());
                                    int p_id_use = int.Parse(reader.GetValue(3).ToString());
                                    bool salaryPass = false;

                                    using (OracleCommand com2 = new OracleCommand("SELECT SALARY FROM PS_SALARY WHERE CITIZEN_ID = :CITIZEN_ID ORDER BY DO_DATE DESC", con)) {
                                        com2.Parameters.Add(new OracleParameter("CITIZEN_ID", loginPerson.PS_CITIZEN_ID));
                                        using (OracleDataReader reader2 = com2.ExecuteReader()) {
                                            if (reader2.Read()) {
                                                int salaryPS = int.Parse(reader2.GetValue(0).ToString());
                                                string posName = DatabaseManager.ExecuteString("SELECT P_NAME FROM TB_POSITION WHERE TB_POSITION.P_ID = " + p_id_use);

                                                if (salaryPS >= salaryMaxUse) {
                                                    salaryPass = true;
                                                }
                                            }
                                        }
                                    }

                                    if (!salaryPass) {
                                        insigHighSalaryCon = false;
                                    }
                                }
                            }
                        }
                        // New Image
                        
                        bool retiring = false;
                        {
                            int todayYear = DateTime.Now.Year;
                            int retireYear = loginPerson.PS_BIRTHDAY_DATE.Value.Year + 60;

                            if (todayYear - retireYear >= -1 && todayYear - retireYear <= 0) {
                                retiring = true;
                            }
                            


                        }
                        if (retiring) {

                            int maxAddOne = DatabaseManager.ExecuteInt("SELECT INSIG_MAX FROM TB_INSIG_GOV_ADD_ONE WHERE P_ID = " + loginPerson.PS_POSITION_ID + " AND INSIG_TARGET = " + insigNewID);
                            if (insigNewID - 1 >= maxAddOne) {
                                --insigNewID;
                            }
                        }

                        
                        // Final
                        if (inWorkYearCon && insigYearCon && insigSalaryCon && insigPosYearCon && insigSalaryYearCon && insigHighSalaryCon && !insigRequest) {
                            return true;
                        } else {
                            return false;
                        }



                    }
                } else if (loginPerson.PS_STAFFTYPE_ID == "3") { //-------------------------------------------------------------------------------------------------------------

                    if (insigNewID >= insig_max) {

                        // Check Inwork Year

                        bool inWorkYearCon = true;
                        {
                            DateTime currentDate = DateTime.Now;
                            DateTime inworkDate = loginPerson.PS_INWORK_DATE.Value;
                            //double year = (DateTime.Now - inworkDate).TotalDays / 365;
                            double year = (new DateTime(DateTime.Now.Year, 10, 6) - inworkDate).TotalDays / 365;
                            //lbTest.Text += "---------------(" + DateTime.Now.Year + ", " + inworkDate.Year + ")";
                            
                            if (year < 8) {
                                inWorkYearCon = false;
                            }
                        }


                        // Check Insig Year Con
                        bool insigYearCon = true;
                        string insigYearConSql;

                        insigYearConSql = "SELECT * FROM TB_INSIG_EMP_INSIG_YEAR_CON WHERE INSIG_TARGET = :INSIG_TARGET AND :SAL >= SALARY_MIN AND :SAL <= SALARY_MAX";

                        using (OracleCommand com = new OracleCommand(insigYearConSql, con)) {
                            com.Parameters.Add(new OracleParameter("INSIG_TARGET", insigNewID));
                            com.Parameters.Add(new OracleParameter("SAL", salary));
                            using (OracleDataReader reader = com.ExecuteReader()) {
                                while (reader.Read()) {
                                    int insigUseID = int.Parse(reader.GetValue(4).ToString());
                                    int insigUseYear = int.Parse(reader.GetValue(5).ToString());
                                    bool insigPass = false;

                                    using (OracleCommand com2 = new OracleCommand("SELECT GET_DATE FROM TB_INSIG_PERSON WHERE CITIZEN_ID = :CITIZEN_ID AND INSIG_ID = :INSIG_ID AND IP_STATUS_ID = 3", con)) {
                                        com2.Parameters.Add(new OracleParameter("CITIZEN_ID", loginPerson.PS_CITIZEN_ID));
                                        com2.Parameters.Add(new OracleParameter("INSIG_ID", insigUseID));
                                        using (OracleDataReader reader2 = com2.ExecuteReader()) {
                                            while (reader2.Read()) {
                                                DateTime getDate = reader2.GetDateTime(0);
                                                DateTime currentDate = DateTime.Now;
                                                double year = (currentDate - getDate).TotalDays / 365;

                                                if (year >= insigUseYear) {
                                                    insigPass = true;
                                                    string Insig_name = "?";
                                                    using (OracleCommand com3 = new OracleCommand("SELECT INSIG_GRADE_NAME_S FROM TB_INSIG_GRADE WHERE INSIG_GRADE_ID = :INSIG_GRADE_ID", con)) {
                                                        com3.Parameters.Add(new OracleParameter("INSIG_GRADE_ID", insigUseID));
                                                        using (OracleDataReader reader3 = com3.ExecuteReader()) {
                                                            while (reader3.Read()) {
                                                                Insig_name = reader3.GetValue(0).ToString();
                                                            }

                                                        }
                                                    }

                                                }
                                            }
                                        }
                                    }

                                    if (!insigPass) {
                                        insigYearCon = false;
                                        string Insig_name = "?";
                                        using (OracleCommand com3 = new OracleCommand("SELECT INSIG_GRADE_NAME_S FROM TB_INSIG_GRADE WHERE INSIG_GRADE_ID = :INSIG_GRADE_ID", con)) {
                                            com3.Parameters.Add(new OracleParameter("INSIG_GRADE_ID", insigUseID));
                                            using (OracleDataReader reader3 = com3.ExecuteReader()) {
                                                while (reader3.Read()) {
                                                    Insig_name = reader3.GetValue(0).ToString();
                                                }

                                            }
                                        }

                                    }
                                }
                            }
                        }

                        //Final
                        if (inWorkYearCon && insigYearCon && !insigRequest) {
                            return true;
                        } else {
                            return false;
                        }






                    }

                } else if (loginPerson.PS_STAFFTYPE_ID == "5") { //-------------------------------------------------------------------------------------------------------------

                    int gov_emp_year_use = -1;
                    using (OracleCommand com = new OracleCommand("SELECT * FROM TB_INSIG_GOVEMP_INWORK_YEAR WHERE :P_ID >= P_ID_MIN AND :P_ID <= P_ID_MAX", con)) {
                        com.Parameters.Add(new OracleParameter("P_ID", loginPerson.PS_POSITION_ID));
                        using (OracleDataReader reader = com.ExecuteReader()) {
                            while (reader.Read()) {
                                gov_emp_year_use = int.Parse(reader.GetValue(3).ToString());
                            }

                        }
                    }

                    if (insigNewID >= insig_max) {

                        // Check Inwork Year

                        bool inWorkYearCon = true;
                        {
                            DateTime currentDate = DateTime.Now;
                            DateTime inworkDate = loginPerson.PS_INWORK_DATE.Value;
                            //double year = (DateTime.Now - inworkDate).TotalDays / 365;
                            double year = (new DateTime(DateTime.Now.Year, 10, 6) - inworkDate).TotalDays / 365;
                            //lbTest.Text += "---------------(" + DateTime.Now.Year + ", " + inworkDate.Year + ")";
                            
                            if (year < gov_emp_year_use) {
                            
                                inWorkYearCon = false;
                            }
                        }


                        // Check Insig Year Con
                        bool insigYearCon = true;
                        string insigYearConSql;

                        insigYearConSql = "SELECT * FROM TB_INSIG_GOVEMP_INSIG_YEAR_CON WHERE INSIG_TARGET = :INSIG_TARGET AND P_ID = :P_ID";

                        using (OracleCommand com = new OracleCommand(insigYearConSql, con)) {
                            com.Parameters.Add(new OracleParameter("INSIG_TARGET", insigNewID));
                            com.Parameters.Add(new OracleParameter("P_ID", loginPerson.PS_POSITION_ID));
                            using (OracleDataReader reader = com.ExecuteReader()) {
                                while (reader.Read()) {
                                    int insigUseID = int.Parse(reader.GetValue(3).ToString());
                                    int insigUseYear = int.Parse(reader.GetValue(4).ToString());
                                    bool insigPass = false;

                                    using (OracleCommand com2 = new OracleCommand("SELECT GET_DATE FROM TB_INSIG_PERSON WHERE CITIZEN_ID = :CITIZEN_ID AND INSIG_ID = :INSIG_ID AND IP_STATUS_ID = 3", con)) {
                                        com2.Parameters.Add(new OracleParameter("CITIZEN_ID", loginPerson.PS_CITIZEN_ID));
                                        com2.Parameters.Add(new OracleParameter("INSIG_ID", insigUseID));
                                        using (OracleDataReader reader2 = com2.ExecuteReader()) {
                                            while (reader2.Read()) {
                                                DateTime getDate = reader2.GetDateTime(0);
                                                DateTime currentDate = DateTime.Now;
                                                double year = (currentDate - getDate).TotalDays / 365;

                                                if (year >= insigUseYear) {
                                                    insigPass = true;
                                                    string Insig_name = "?";
                                                    using (OracleCommand com3 = new OracleCommand("SELECT INSIG_GRADE_NAME_S FROM TB_INSIG_GRADE WHERE INSIG_GRADE_ID = :INSIG_GRADE_ID", con)) {
                                                        com3.Parameters.Add(new OracleParameter("INSIG_GRADE_ID", insigUseID));
                                                        using (OracleDataReader reader3 = com3.ExecuteReader()) {
                                                            while (reader3.Read()) {
                                                                Insig_name = reader3.GetValue(0).ToString();
                                                            }

                                                        }
                                                    }

                                                    
                                                }
                                            }
                                        }
                                    }

                                    if (!insigPass) {
                                        insigYearCon = false;
                                        string Insig_name = "?";
                                        using (OracleCommand com3 = new OracleCommand("SELECT INSIG_GRADE_NAME_S FROM TB_INSIG_GRADE WHERE INSIG_GRADE_ID = :INSIG_GRADE_ID", con)) {
                                            com3.Parameters.Add(new OracleParameter("INSIG_GRADE_ID", insigUseID));
                                            using (OracleDataReader reader3 = com3.ExecuteReader()) {
                                                while (reader3.Read()) {
                                                    Insig_name = reader3.GetValue(0).ToString();
                                                }

                                            }
                                        }

                                        
                                    }
                                }
                            }
                        }



                        // Final
                        if (inWorkYearCon && insigYearCon && !insigRequest) {
                            return true;
                        } else {
                            return false;
                        }






                    }

                }
              
               

               
            }

            return false;
         
        }

    }
}