using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.OracleClient;

namespace WEB_PERSONAL.Class {
    
    public class DatabaseManager {

        public static readonly string PROVIDER = "System.Data.OracleClient";
        //public static readonly string DATA_SOURCE = "203.158.140.67";
        public static readonly string DATA_SOURCE = "192.168.1.55";
        //public static readonly string DATA_SOURCE = "192.168.100.4";
        //public static readonly string DATA_SOURCE = "localhost";
        public static readonly string PORT = "1521";
        public static readonly string SID = "orcl";
        public static readonly string USER_ID = "rmutto";
        public static readonly string PASSWORD = "Zxcvbnm";
        //public static readonly string CONNECTION_STRING_OLE = @"Provider=" + PROVIDER + "; Data Source = " + DATA_SOURCE + ":" + PORT + "/" + SID + ";USER ID=" + USER_ID + ";PASSWORD=" + PASSWORD;
        public static readonly string CONNECTION_STRING = @"Data Source = " + DATA_SOURCE + ":" + PORT + "/" + SID + ";USER ID=" + USER_ID + ";PASSWORD=" + PASSWORD + ";";
        //public static readonly string CONNECTION_STRING_FIXED = @"Provider=OraOLEDB.Oracle; Data Source = 203.158.140.67:1521/orcl;USER ID=rmutto;PASSWORD=Zxcvbnm";

        public static void ExecuteNonQuery(string sql) {
            OracleConnection.ClearAllPools();
            using (OracleConnection con = new OracleConnection(CONNECTION_STRING)) {
                con.Open();
                using (OracleCommand com = new OracleCommand(sql, con)) {
                    com.ExecuteNonQuery();
                }
            } 
        }
        public static int ExecuteInt(string sql) {
            OracleConnection.ClearAllPools();
            int output = -1;
            using (OracleConnection con = new OracleConnection(CONNECTION_STRING)) {
                con.Open();
                using (OracleCommand com = new OracleCommand(sql, con)) {
                    using(OracleDataReader reader = com.ExecuteReader()) {
                        while(reader.Read()) {
                            output = int.Parse(reader.GetValue(0).ToString());
                        }
                    }
                }
            }
            return output;
        }
        public static string ExecuteString(string sql) {
            OracleConnection.ClearAllPools();
            string output = null;
            using (OracleConnection con = new OracleConnection(CONNECTION_STRING)) {
                con.Open();
                using (OracleCommand com = new OracleCommand(sql, con)) {
                    using (OracleDataReader reader = com.ExecuteReader()) {
                        while (reader.Read()) {
                            output = reader.GetValue(0).ToString();
                        }
                    }
                }
            }
            return output;
        }
        public static int ExecuteSequence(string seq_name) {
            OracleConnection.ClearAllPools();
            int seq;
            using (OracleConnection con = new OracleConnection(CONNECTION_STRING)) {
                con.Open();
                using (OracleCommand com = new OracleCommand("SELECT " + seq_name + ".NEXTVAL FROM DUAL", con)) {
                    using (OracleDataReader reader = com.ExecuteReader()) {
                        reader.Read();
                        seq = int.Parse(reader.GetValue(0).ToString());
                    }
                }
            }
            return seq;
        }
        public static void BindDropDown(DropDownList ddl, string sql, string text, string value) {
            OracleConnection.ClearAllPools();
            ddl.DataSource = CreateSQLDataSource(sql);
            ddl.DataTextField = text;
            ddl.DataValueField = value;
            ddl.DataBind();
        }
        public static void BindDropDown(DropDownList ddl, string sql, string text, string value, string first) {
            OracleConnection.ClearAllPools();
            ddl.DataSource = CreateSQLDataSource(sql);
            ddl.DataTextField = text;
            ddl.DataValueField = value;
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem(first, ""));
        }
        public static void BindGridView(GridView gv, string sql) {
            OracleConnection.ClearAllPools();
            SqlDataSource sds = CreateSQLDataSource(sql);
            gv.DataSource = sds;
            gv.DataBind();
        }
        public static SqlDataSource CreateSQLDataSource(string sql) {
            return new SqlDataSource("Oracle.DataAccess.Client", CONNECTION_STRING, sql);
        }
        public static bool ValidateUser(string personID, DateTime password) {
            OracleConnection.ClearAllPools();
            using (OracleConnection con = new OracleConnection(CONNECTION_STRING)) {
                con.Open();
                using (OracleCommand com = new OracleCommand("SELECT PS_CITIZEN_ID, PS_BIRTHDAY_DATE FROM PS_PERSON", con)) {
                    using (OracleDataReader reader = com.ExecuteReader()) {
                        while (reader.Read()) {
                            if (reader.GetString(0) == personID && reader.GetDateTime(1) == password) {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }
        public static Person GetPerson(string citizenID) {
            OracleConnection.ClearAllPools();

            using (OracleConnection con = new OracleConnection(CONNECTION_STRING)) {
                con.Open();
                using (OracleCommand com = new OracleCommand(

                    "SELECT PS_CITIZEN_ID,PS_ID,PS_TITLE_ID,PS_FIRSTNAME,PS_LASTNAME,PS_GENDER_ID,PS_BIRTHDAY_DATE,PS_EMAIL,PS_HOMEADD,PS_MOO,PS_STREET,PS_DISTRICT_ID,PS_AMPHUR_ID,PS_PROVINCE_ID,PS_ZIPCODE,PS_TELEPHONE,PS_NATION_ID,PS_CAMPUS_ID,PS_FACULTY_ID,PS_DIVISION_ID,PS_WORK_DIVISION_ID,PS_STAFFTYPE_ID,PS_TIME_CONTACT_ID,PS_BUDGET_ID,PS_SUBSTAFFTYPE_ID,PS_ADMIN_POS_ID,PS_POSITION_ID,PS_WORK_POS_ID,PS_INWORK_DATE,PS_DATE_START_THIS_U,PS_SPECIAL_NAME,PS_TEACH_ISCED_ID,PS_GRAD_LEV_ID,PS_GRAD_CURR,PS_GRAD_ISCED_ID,PS_GRAD_PROG_ID,PS_GRAD_UNIV,PS_GRAD_COUNTRY_ID,PS_DEFORM_ID,PS_SIT_NO,PS_RELIGION_ID,PS_MOVEMENT_TYPE_ID,PS_MOVEMENT_DATE,PS_PASSWORD,ST_LOGIN_ID,PERSON_ROLE_ID,PS_FIRST_POSITION_ID," +
                    "(SELECT ADMIN_POSITION_POWER FROM TB_ADMIN_POSITION WHERE TB_ADMIN_POSITION.ADMIN_POSITION_ID = PS_PERSON.PS_ADMIN_POS_ID) ADMIN_POSITION_POWER, "+
                    "(SELECT TITLE_NAME_TH FROM TB_TITLENAME WHERE TB_TITLENAME.TITLE_ID = PS_PERSON.PS_TITLE_ID) PS_TITLE_NAME," +
                    "(SELECT GENDER_NAME FROM TB_GENDER WHERE TB_GENDER.GENDER_ID = PS_PERSON.PS_GENDER_ID) PS_GENDER_NAME," +
                    "(SELECT CAMPUS_NAME FROM TB_CAMPUS WHERE TB_CAMPUS.CAMPUS_ID = PS_PERSON.PS_CAMPUS_ID) PS_CAMPUS_NAME," +
                    "(SELECT FACULTY_NAME FROM TB_FACULTY WHERE TB_FACULTY.FACULTY_ID = PS_PERSON.PS_FACULTY_ID) PS_FACULTY_NAME," +
                    "(SELECT DIVISION_NAME FROM TB_DIVISION WHERE TB_DIVISION.DIVISION_ID = PS_PERSON.PS_DIVISION_ID) PS_DIVISION_NAME," +
                    "(SELECT WORK_NAME FROM TB_WORK_DIVISION WHERE TB_WORK_DIVISION.WORK_ID = PS_PERSON.PS_WORK_DIVISION_ID) PS_WORK_DIVISION_NAME," +
                    "(SELECT ADMIN_POSITION_NAME FROM TB_ADMIN_POSITION WHERE TB_ADMIN_POSITION.ADMIN_POSITION_ID = PS_PERSON.PS_ADMIN_POS_ID) PS_ADMIN_POSITION_NAME," +
                    "(SELECT POSITION_WORK_NAME FROM TB_POSITION_WORK WHERE TB_POSITION_WORK.POSITION_WORK_ID = PS_PERSON.PS_WORK_POS_ID) PS_WORK_POS_NAME," +
                    "(SELECT P_NAME FROM TB_POSITION WHERE TB_POSITION.P_ID = PS_PERSON.PS_POSITION_ID) PS_POSITION_NAME," +
                    "(SELECT STAFFTYPE_NAME FROM TB_STAFFTYPE WHERE TB_STAFFTYPE.STAFFTYPE_ID = PS_PERSON.PS_STAFFTYPE_ID) PS_STAFFTYPE_NAME," +
                    "(SELECT ST_LOGIN_NAME FROM TB_STATUS_LOGIN WHERE TB_STATUS_LOGIN.ST_LOGIN_ID = PS_PERSON.ST_LOGIN_ID) ST_LOGIN_NAME," +
                    "(SELECT DISTRICT_TH FROM TB_DISTRICT WHERE TB_DISTRICT.DISTRICT_ID = PS_PERSON.PS_DISTRICT_ID) PS_DISTRICT_NAME," +
                    "(SELECT AMPHUR_TH FROM TB_AMPHUR WHERE TB_AMPHUR.AMPHUR_ID = PS_PERSON.PS_AMPHUR_ID) PS_AMPHUR_NAME," +
                    "(SELECT PROVINCE_TH FROM TB_PROVINCE WHERE TB_PROVINCE.PROVINCE_ID = PS_PERSON.PS_PROVINCE_ID) PS_PROVINCE_NAME," +
                    "(SELECT NATION_NAME_EN FROM TB_NATION WHERE TB_NATION.NATION_ID = PS_PERSON.PS_NATION_ID) PS_NATION_NAME," +
                    "(SELECT TIME_CONTACT_NAME FROM TB_TIME_CONTACT WHERE TB_TIME_CONTACT.TIME_CONTACT_ID = PS_PERSON.PS_TIME_CONTACT_ID) PS_TIME_CONTACT_NAME," +
                    "(SELECT BUDGET_NAME FROM TB_BUDGET WHERE TB_BUDGET.BUDGET_ID = PS_PERSON.PS_BUDGET_ID) PS_BUDGET_NAME," +
                    "(SELECT SUBSTAFFTYPE_NAME FROM TB_SUBSTAFFTYPE WHERE TB_SUBSTAFFTYPE.SUBSTAFFTYPE_ID = PS_PERSON.PS_SUBSTAFFTYPE_ID) PS_SUBSTAFFTYPE_NAME," +
                    "(SELECT ISCED_NAME FROM TB_ISCED WHERE TB_ISCED.ISCED_ID = PS_PERSON.PS_TEACH_ISCED_ID) PS_TEACH_ISCED_NAME," +
                    "(SELECT LEV_NAME_TH FROM TB_LEV WHERE TB_LEV.LEV_ID = PS_PERSON.PS_GRAD_LEV_ID) PS_GRAD_LEV_NAME," +
                    "(SELECT ISCED_NAME FROM TB_ISCED WHERE TB_ISCED.ISCED_ID = PS_PERSON.PS_TEACH_ISCED_ID) PS_GRAD_ISCED_ID," +
                    "(SELECT PROGRAM_NAME FROM TB_PROGRAM WHERE TB_PROGRAM.PROGRAM_ID_NEW = PS_PERSON.PS_GRAD_PROG_ID) PS_GRAD_PROG_NAME," +
                    "(SELECT NATION_NAME_EN FROM TB_NATION WHERE TB_NATION.NATION_ID = PS_PERSON.PS_GRAD_COUNTRY_ID) PS_GRAD_COUNTRY_NAME," +
                    "(SELECT DEFORM_NAME FROM TB_DEFORM WHERE TB_DEFORM.DEFORM_ID = PS_PERSON.PS_DEFORM_ID) PS_DEFORM_NAME," +
                    "(SELECT RELIGION_NAME FROM TB_RELIGION WHERE TB_RELIGION.RELIGION_ID = PS_PERSON.PS_RELIGION_ID) PS_RELIGION_NAME," +
                    "(SELECT MOVEMENT_TYPE_NAME FROM TB_MOVEMENT_TYPE WHERE TB_MOVEMENT_TYPE.MOVEMENT_TYPE_ID = PS_PERSON.PS_MOVEMENT_TYPE_ID) PS_MOVEMENT_TYPE_NAME," +
                    "(SELECT PERSON_ROLE_NAME FROM TB_PERSON_ROLE WHERE TB_PERSON_ROLE.PERSON_ROLE_ID = PS_PERSON.PERSON_ROLE_ID) PERSON_ROLE_NAME," +
                    "(SELECT P_NAME FROM TB_POSITION WHERE TB_POSITION.P_ID = PS_PERSON.PS_FIRST_POSITION_ID) PS_FIRST_POSITION_NAME" +
                    " FROM PS_PERSON WHERE PS_CITIZEN_ID = '" + citizenID + "'"
                    , con)) {
                    using (OracleDataReader reader = com.ExecuteReader()) {
                        while (reader.Read()) {

                            Person person = new Person();
                            int i = 0;
                            //BASE
                            person.PS_CITIZEN_ID = reader.GetValue(i++).ToString();
                            person.PS_ID = reader.GetValue(i++).ToString();
                            person.PS_TITLE_ID = reader.GetValue(i++).ToString();
                            person.PS_FIRSTNAME = reader.GetValue(i++).ToString();
                            person.PS_LASTNAME = reader.GetValue(i++).ToString();
                            person.PS_GENDER_ID = reader.GetValue(i++).ToString();
                            if (reader.IsDBNull(i)) { person.PS_BIRTHDAY_DATE = null; } else { person.PS_BIRTHDAY_DATE = reader.GetDateTime(i); } ++i;
                            person.PS_EMAIL = reader.GetValue(i++).ToString();
                            person.PS_HOMEADD = reader.GetValue(i++).ToString();
                            person.PS_MOO = reader.GetValue(i++).ToString();
                            person.PS_STREET = reader.GetValue(i++).ToString();
                            person.PS_DISTRICT_ID = reader.GetValue(i++).ToString();
                            person.PS_AMPHUR_ID = reader.GetValue(i++).ToString();
                            person.PS_PROVINCE_ID = reader.GetValue(i++).ToString();
                            person.PS_ZIPCODE = reader.GetValue(i++).ToString();
                            person.PS_TELEPHONE = reader.GetValue(i++).ToString();
                            person.PS_NATION_ID = reader.GetValue(i++).ToString();
                            person.PS_CAMPUS_ID = reader.GetValue(i++).ToString();
                            person.PS_FACULTY_ID = reader.GetValue(i++).ToString();
                            person.PS_DIVISION_ID = reader.GetValue(i++).ToString();
                            person.PS_WORK_DIVISION_ID = reader.GetValue(i++).ToString();
                            person.PS_STAFFTYPE_ID = reader.GetValue(i++).ToString();
                            person.PS_TIME_CONTACT_ID = reader.GetValue(i++).ToString();
                            person.PS_BUDGET_ID = reader.GetValue(i++).ToString();
                            person.PS_SUBSTAFFTYPE_ID = reader.GetValue(i++).ToString();
                            person.PS_ADMIN_POS_ID = reader.GetValue(i++).ToString();
                            person.PS_POSITION_ID = reader.GetValue(i++).ToString();
                            person.PS_WORK_POS_ID = reader.GetValue(i++).ToString();
                            if (reader.IsDBNull(i)) { person.PS_INWORK_DATE = null; } else { person.PS_INWORK_DATE = reader.GetDateTime(i); } ++i;
                            if (reader.IsDBNull(i)) { person.PS_DATE_START_THIS_U = null; } else { person.PS_DATE_START_THIS_U = reader.GetDateTime(i); } ++i;
                            person.PS_SPECIAL_NAME = reader.GetValue(i++).ToString();
                            person.PS_TEACH_ISCED_ID = reader.GetValue(i++).ToString();
                            person.PS_GRAD_LEV_ID = reader.GetValue(i++).ToString();
                            person.PS_GRAD_CURR = reader.GetValue(i++).ToString();
                            person.PS_GRAD_ISCED_ID = reader.GetValue(i++).ToString();
                            person.PS_GRAD_PROG_ID = reader.GetValue(i++).ToString();
                            person.PS_GRAD_UNIV = reader.GetValue(i++).ToString();
                            person.PS_GRAD_COUNTRY_ID = reader.GetValue(i++).ToString();
                            person.PS_DEFORM_ID = reader.GetValue(i++).ToString();
                            person.PS_SIT_NO = reader.GetValue(i++).ToString();
                            person.PS_RELIGION_ID = reader.GetValue(i++).ToString();
                            person.PS_MOVEMENT_TYPE_ID = reader.GetValue(i++).ToString();
                            if (reader.IsDBNull(i)) { person.PS_MOVEMENT_DATE = null; } else { person.PS_MOVEMENT_DATE = reader.GetDateTime(i); } ++i;
                            person.PS_PASSWORD = reader.GetValue(i++).ToString();
                            person.ST_LOGIN_ID = reader.GetValue(i++).ToString();
                            person.PERSON_ROLE_ID = reader.GetValue(i++).ToString();
                            person.FIRST_POSITION_ID = reader.GetValue(i++).ToString();

                            //NAME
                            person.AdminPositionPower = reader.GetValue(i++).ToString();
                            person.PS_TITLE_NAME = reader.GetValue(i++).ToString();
                            person.PS_GENDER_NAME = reader.GetValue(i++).ToString();
                            person.PS_CAMPUS_NAME = reader.GetValue(i++).ToString();
                            person.PS_FACULTY_NAME = reader.GetValue(i++).ToString();
                            person.PS_DIVISION_NAME = reader.GetValue(i++).ToString();
                            person.PS_WORK_DIVISION_NAME = reader.GetValue(i++).ToString();
                            person.PS_ADMIN_POS_NAME = reader.GetValue(i++).ToString();
                            person.PS_WORK_POS_NAME = reader.GetValue(i++).ToString();
                            person.PS_POSITION_NAME = reader.GetValue(i++).ToString();
                            person.PS_STAFFTYPE_NAME = reader.GetValue(i++).ToString();
                            person.ST_LOGIN_NAME = reader.GetValue(i++).ToString();
                            person.PS_DISTRICT_NAME = reader.GetValue(i++).ToString();
                            person.PS_AMPHUR_NAME = reader.GetValue(i++).ToString();
                            person.PS_PROVINCE_NAME = reader.GetValue(i++).ToString();
                            person.PS_NATION_NAME = reader.GetValue(i++).ToString();
                            person.PS_TIME_CONTACT_NAME = reader.GetValue(i++).ToString();
                            person.PS_BUDGET_NAME = reader.GetValue(i++).ToString();
                            person.PS_SUBSTAFFTYPE_NAME = reader.GetValue(i++).ToString();
                            person.PS_TEACH_ISCED_NAME = reader.GetValue(i++).ToString();
                            person.PS_GRAD_LEV_NAME = reader.GetValue(i++).ToString();
                            person.PS_GRAD_ISCED_ID = reader.GetValue(i++).ToString();
                            person.PS_GRAD_PROG_NAME = reader.GetValue(i++).ToString();
                            person.PS_GRAD_COUNTRY_NAME = reader.GetValue(i++).ToString();
                            person.PS_DEFORM_NAME = reader.GetValue(i++).ToString();
                            person.PS_RELIGION_NAME = reader.GetValue(i++).ToString();
                            person.PS_MOVEMENT_TYPE_NAME = reader.GetValue(i++).ToString();
                            person.PERSON_ROLE_NAME = reader.GetValue(i++).ToString();
                            person.FIRST_POSITION_NAME = reader.GetValue(i++).ToString();

                            return person;
                        }
                    }
                }
            }
            return null;
        }
        
        public static int GetLeaveRequiredCountByCommander(string citizenID) {
            OracleConnection.ClearAllPools();
            using (OracleConnection con = new OracleConnection(CONNECTION_STRING)) {
                con.Open();
                using (OracleCommand com = new OracleCommand("SELECT COUNT(LEV_BOSS_DATA.LEAVE_BOSS_ID) FROM LEV_DATA, LEV_BOSS_DATA WHERE LEAVE_STATUS_ID IN(1,4) AND LEV_DATA.LEAVE_ID = LEV_BOSS_DATA.LEAVE_ID AND LEV_DATA.BOSS_STATE = LEV_BOSS_DATA.STATE AND LEV_BOSS_DATA.CITIZEN_ID = '" + citizenID + "'", con)) {
                    using (OracleDataReader reader = com.ExecuteReader()) {
                        while (reader.Read()) {
                            int count = int.Parse(reader.GetInt32(0).ToString());
                            return count;
                        }
                    }
                }
            }
            return -1;
        }
        public static List<DateTime> GetLeaveDateTimeFromToDate(string citizenID) {
            OracleConnection.ClearAllPools();
            List<DateTime> list = new List<DateTime>();
            using (OracleConnection con = new OracleConnection(CONNECTION_STRING)) {
                con.Open();
                using (OracleCommand com = new OracleCommand("SELECT FROM_DATE, TO_DATE FROM LEV_DATA WHERE PS_ID = '" + citizenID + "'", con)) {
                    using (OracleDataReader reader = com.ExecuteReader()) {
                        while (reader.Read()) {
                            DateTime start = reader.GetDateTime(0);
                            DateTime to = reader.GetDateTime(1);
                            while (true) {
                                if(!list.Contains(start)) {
                                    list.Add(start);
                                }
                                start = start.AddDays(1);
                                if((to - start).TotalDays < 0) {
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            return list;
        }
        public static string รหัสหัวหน้าฝ่าย(string DVID) {
            OracleConnection.ClearAllPools();
            string citizenID = "-1";
            using (OracleConnection con = new OracleConnection(CONNECTION_STRING)) {
                con.Open();
                using (OracleCommand com = new OracleCommand("SELECT PS_CITIZEN_ID FROM PS_PERSON WHERE PS_ADMIN_POS_ID = 4 AND PS_WORK_DIVISION_ID = " + DVID, con)) {
                    using (OracleDataReader reader = com.ExecuteReader()) {
                        while (reader.Read()) {
                            citizenID = reader.GetString(0);
                        }
                    }
                }
            }
            return citizenID;
        }
        public static string รหัสหัวหน้าภาควิชา(string DVID) {
            OracleConnection.ClearAllPools();
            string citizenID = "-1";
            using (OracleConnection con = new OracleConnection(CONNECTION_STRING)) {
                con.Open();
                using (OracleCommand com = new OracleCommand("SELECT PS_CITIZEN_ID FROM PS_PERSON WHERE PS_ADMIN_POS_ID = 7 AND PS_DIVISION_ID = " + DVID, con)) {
                    using (OracleDataReader reader = com.ExecuteReader()) {
                        while (reader.Read()) {
                            citizenID = reader.GetString(0);
                        }
                    }
                }
            }
            return citizenID;
        }
        public static string รหัสคณบดี(string FID) {
            OracleConnection.ClearAllPools();
            string citizenID = "-1";
            using (OracleConnection con = new OracleConnection(CONNECTION_STRING)) {
                con.Open();
                using (OracleCommand com = new OracleCommand("SELECT PS_CITIZEN_ID FROM PS_PERSON WHERE PS_ADMIN_POS_ID = 3 AND PS_FACULTY_ID = " + FID, con)) {
                    using (OracleDataReader reader = com.ExecuteReader()) {
                        while (reader.Read()) {
                            citizenID = reader.GetString(0);
                        }
                    }
                }
            }
            return citizenID;
        }
        public static string รหัสอธิการบดี(string CID) {
            OracleConnection.ClearAllPools();
            string citizenID = "-1";
            using (OracleConnection con = new OracleConnection(CONNECTION_STRING)) {
                con.Open();
                using (OracleCommand com = new OracleCommand("SELECT PS_CITIZEN_ID FROM PS_PERSON WHERE PS_ADMIN_POS_ID = 1", con)) {
                    using (OracleDataReader reader = com.ExecuteReader()) {
                        while (reader.Read()) {
                            citizenID = reader.GetString(0);
                        }
                    }
                }
            }
            return citizenID;
        }
        public static List<Person> รหัสหัวหน้า(string citizenID) {
            List<Person> bossList = new List<Person>();
            OracleConnection.ClearAllPools();
            using (OracleConnection con = new OracleConnection(CONNECTION_STRING)) {
                con.Open();
                int workPositionID = -1;
                int adminPositionPower = 0;
                int facultyID = 0;
                int divisionID = 0;
                int workDivisionID = 0;
                int adminPositionID = 0;
                int campusID = 0;
                bool no_me = false;
                bool cancel = false;
                using (OracleCommand com = new OracleCommand("SELECT PS_WORK_POS_ID, PS_FACULTY_ID, PS_DIVISION_ID, PS_WORK_DIVISION_ID, PS_ADMIN_POS_ID, PS_CAMPUS_ID FROM PS_PERSON WHERE PS_CITIZEN_ID = '" + citizenID + "'", con)) {
                    using (OracleDataReader reader = com.ExecuteReader()) {
                        while (reader.Read()) {
                            workPositionID = reader.GetInt32(0);
                            facultyID = reader.GetInt32(1);
                            divisionID = reader.GetInt32(2);
                            
                            if(reader.IsDBNull(3)) {
                                workDivisionID = -1;
                            } else {
                                workDivisionID = reader.GetInt32(3);
                            }
                            adminPositionID = reader.GetInt32(4);
                            campusID = reader.GetInt32(5);

                        }
                    }
                }
                using (OracleCommand com = new OracleCommand("SELECT ADMIN_POSITION_POWER FROM PS_PERSON, TB_ADMIN_POSITION WHERE PS_CITIZEN_ID = '" + citizenID + "' AND PS_ADMIN_POS_ID = ADMIN_POSITION_ID", con)) {
                    using (OracleDataReader reader = com.ExecuteReader()) {
                        while (reader.Read()) {
                            adminPositionPower = reader.GetInt32(0);
                        }
                    }
                }
                int bossNodeType = -1;
                int bossNodeTypeID = -1;

                /*if ((workPositionID == 10075)) { //บุคคลธรรมดา
                    if (adminPositionPower == 0) {
                        bossNodeType = 3;
                        bossNodeTypeID = divisionID;
                    } else if (adminPositionPower == 3) {
                        bossNodeType = 3;
                        bossNodeTypeID = divisionID;
                        no_me = true;
                    }
                }
                else */if ((workPositionID == 10108 || workPositionID == 10077) && (adminPositionPower >= 3 || adminPositionPower == 0)) { //อาจารย์
                    if(adminPositionPower == 0) {
                        bossNodeType = 3;
                        bossNodeTypeID = divisionID;
                    }
                    else if(adminPositionPower == 3) {
                        bossNodeType = 3;
                        bossNodeTypeID = divisionID;
                        no_me = true;
                    }
                } else {
                    if(adminPositionPower == 2 && adminPositionID != 4) {
                        bossNodeType = 2;
                        bossNodeTypeID = facultyID;
                    } else if (adminPositionID == 4) { //คณบดี
                        bossNodeType = 1;
                        bossNodeTypeID = campusID;
                    } else if (adminPositionID == 2) { //รองอธิการ
                        bossNodeType = -1;
                        bossNodeTypeID = -1;
                    } else if(adminPositionID == 1) { //อธิการ
                        cancel = true;
                    } else {
                        if(workDivisionID == -1) {
                            bossNodeType = 3;
                            bossNodeTypeID = divisionID;
                        } else {
                            bossNodeType = 4;
                            bossNodeTypeID = workDivisionID;
                        }
                        
                    }
                }
                if(cancel) {
                    return null;
                }

                int? nextNodeID = null;
                while (true) {
                    string sql = "SELECT * FROM TB_BOSS_NODE WHERE BOSS_NODE_TYPE = " + bossNodeType + " AND BOSS_NODE_TYPE_ID = '" + bossNodeTypeID + "'";
                    if(bossNodeType == -1) {
                        sql = "SELECT * FROM TB_BOSS_NODE WHERE BOSS_NODE_ID = 1";
                    }
                    if(nextNodeID != null) {
                        sql = "SELECT * FROM TB_BOSS_NODE WHERE BOSS_NODE_ID = " + nextNodeID;
                    }
                    using (OracleCommand com = new OracleCommand(sql, con)) {
                        using (OracleDataReader reader = com.ExecuteReader()) {
                            while (reader.Read()) {
                                if (reader.IsDBNull(3)) {
                                    nextNodeID = null;
                                } else {
                                    nextNodeID = reader.GetInt32(3);   
                                }

                                string bossID = reader.GetString(6);
                                Person p = GetPerson(bossID);
                                if(no_me) {
                                    no_me = false;
                                } else {
                                    bossList.Add(p);
                                }
                                

                            }
                        }
                    }
                    if (nextNodeID == null)
                        break;
                }



               
            }
            return bossList;
        }
        /*public static string รหัสเลขาธิการคณะกรรมการ() {
            OracleConnection.ClearAllPools();
            string citizenID = "-1";
            using (OracleConnection con = new OracleConnection(CONNECTION_STRING)) {
                con.Open();
                using (OracleCommand com = new OracleCommand("SELECT PS_CITIZEN_ID FROM PS_PERSON WHERE PS_ADMIN_POS_ID = 10022", con)) {
                    using (OracleDataReader reader = com.ExecuteReader()) {
                        while (reader.Read()) {
                            citizenID = reader.GetString(0);
                        }
                    }
                }
            }
            return citizenID;
        }
        public static string รหัสรัฐมนตรีเจ้าสังกัด() {
            OracleConnection.ClearAllPools();
            string citizenID = "-1";
            using (OracleConnection con = new OracleConnection(CONNECTION_STRING)) {
                con.Open();
                using (OracleCommand com = new OracleCommand("SELECT PS_CITIZEN_ID FROM PS_PERSON WHERE PS_ADMIN_POS_ID = 10021", con)) {
                    using (OracleDataReader reader = com.ExecuteReader()) {
                        while (reader.Read()) {
                            citizenID = reader.GetString(0);
                        }
                    }
                }
            }
            return citizenID;
        }*/
        public static void AddCounter() {
            ExecuteNonQuery("UPDATE TB_WEB SET COUNTER = COUNTER+1 WHERE ID = 1");
        }
        public static int GetCounter() {
            return ExecuteInt("SELECT COUNTER FROM TB_WEB WHERE ID = 1");
        }
        public static string GetPersonImageFileName(string citizenID) {
            OracleConnection.ClearAllPools();
            string fileName = "";
            using (OracleConnection con = new OracleConnection(CONNECTION_STRING)) {
                con.Open();
                using (OracleCommand com = new OracleCommand("SELECT URL FROM PS_PERSON_IMAGE WHERE CITIZEN_ID = '" + citizenID + "' AND PRESENT = 1", con)) {
                    using (OracleDataReader reader = com.ExecuteReader()) {
                        while (reader.Read()) {
                            fileName = reader.GetValue(0).ToString();
                        }
                    }
                }
            }
            return fileName;
        }
        public static string[] GetPersonImageFileNames(string citizenID) {
            OracleConnection.ClearAllPools();
            List<string> fileNameList = new List<string>();
            using (OracleConnection con = new OracleConnection(CONNECTION_STRING)) {
                con.Open();
                using (OracleCommand com = new OracleCommand("SELECT URL FROM PS_PERSON_IMAGE WHERE CITIZEN_ID = '" + citizenID + "'", con)) {
                    using (OracleDataReader reader = com.ExecuteReader()) {
                        while (reader.Read()) {
                            fileNameList.Add(reader.GetValue(0).ToString());
                        }
                    }
                }
            }
            return fileNameList.ToArray();
        }

        public static bool ValidatePasswordFirst(string personID, DateTime password)
        {
            OracleConnection.ClearAllPools();
            using (OracleConnection con = new OracleConnection(CONNECTION_STRING))
            {
                con.Open();
                using (OracleCommand com = new OracleCommand("SELECT PS_CITIZEN_ID, PS_BIRTHDAY_DATE FROM PS_PERSON", con))
                {
                    using (OracleDataReader reader = com.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader.GetString(0) == personID && reader.GetDateTime(1) == password)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }


    }
}