using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OracleClient;
using System.Globalization;

namespace WEB_PERSONAL.Class {

    public class Person {

        public string AdminPositionPower;
        public string PS_CITIZEN_ID;
        public string PS_ID;
        public string PS_TITLE_ID;
        public string PS_TITLE_NAME;
        public string PS_FN_TH;
        public string PS_LN_TH;
        public string PS_GENDER_ID;
        public string PS_GENDER_NAME;
        public DateTime? PS_BIRTHDAY_DATE;
        public string PS_EMAIL;
        public string PS_CAMPUS_ID;
        public string PS_CAMPUS_NAME;
        public string PS_FACULTY_ID;
        public string PS_FACULTY_NAME;
        public string PS_DIVISION_ID;
        public string PS_DIVISION_NAME;
        public string PS_WORK_DIVISION_ID;
        public string PS_WORK_DIVISION_NAME;
        public string PS_ADMIN_POS_ID;
        public string PS_ADMIN_POS_NAME;
        public string PS_WORK_POS_ID;
        public string PS_WORK_POS_NAME;
        public DateTime? PS_INWORK_DATE;
        public string PS_PASSWORD;
        public string PS_POSITION_ID;
        public string PS_POSITION_NAME;
        public string PS_STAFFTYPE_ID;
        public string PS_STAFFTYPE_NAME;
        public string ST_LOGIN_ID;
        public string ST_LOGIN_NAME;
        public string PS_YEAR;
        public string PS_HOMEADD;
        public string PS_MOO;
        public string PS_STREET;
        public string PS_DISTRICT_ID;
        public string PS_DISTRICT_NAME;
        public string PS_AMPHUR_ID;
        public string PS_AMPHUR_NAME;
        public string PS_PROVINCE_ID;
        public string PS_PROVINCE_NAME;
        public string PS_TELEPHONE;
        public string PS_ZIPCODE;
        public string PS_NATION_ID;
        public string PS_NATION_NAME;
        public string PS_TIME_CONTACT_ID;
        public string PS_TIME_CONTACT_NAME;
        public string PS_BUDGET_ID;
        public string PS_BUDGET_NAME;
        public string PS_SUBSTAFFTYPE_ID;
        public string PS_SUBSTAFFTYPE_NAME;
        public string PS_POSITION_WORK;
        public DateTime? PS_DATE_START_THIS_U;
        public string PS_SPECIAL_NAME;
        public string PS_TEACH_ISCED_ID;
        public string PS_TEACH_ISCED_NAME;
        public string PS_GRAD_LEV_ID;
        public string PS_GRAD_LEV_NAME;
        public string PS_GRAD_CURR;
        public string PS_GRAD_ISCED_ID;
        public string PS_GRAD_ISCED_NAME;
        public string PS_GRAD_PROG_ID;
        public string PS_GRAD_PROG_NAME;
        public string PS_GRAD_UNIV;
        public string PS_GRAD_COUNTRY_ID;
        public string PS_GRAD_COUNTRY_NAME;
        public string PS_DEFORM_ID;
        public string PS_DEFORM_NAME;
        public string PS_SIT_NO;
        public string PS_RELIGION_ID;
        public string PS_RELIGION_NAME;
        public string PS_MOVEMENT_TYPE_ID;
        public string PS_MOVEMENT_TYPE_NAME;
        public DateTime? PS_MOVEMENT_DATE;
        public string PERSON_ROLE_ID;
        public string PERSON_ROLE_NAME;

        public string FullName {
            get { return PS_TITLE_NAME + PS_FN_TH + " " + PS_LN_TH; }
        }
        public string FirstNameAndLastName {
            get { return PS_FN_TH + " " + PS_LN_TH; }
        }

        public bool IsTeacher() {
            return PS_WORK_POS_ID == "10108" || PS_WORK_POS_ID == "10077" ? true : false;
        }
        public string AdminPositionNameExtra() {
            if(PS_ADMIN_POS_ID == "1") {
                return "มหาวิทยาลัยเทคโนโลยีราชมงคลตะวันออก";
            } else if (PS_ADMIN_POS_ID == "2") {
                return PS_CAMPUS_NAME;
            } else if (PS_ADMIN_POS_ID == "4") {
                return PS_FACULTY_NAME;
            } else if (PS_ADMIN_POS_ID == "5") {
                return PS_WORK_DIVISION_NAME;
            } else if (PS_ADMIN_POS_ID == "10") {
                return PS_DIVISION_NAME;
            }
            return PS_ADMIN_POS_NAME;
        }
        public bool IsMale() {
            return PS_GENDER_ID == "1";
        }
        public bool IsFemale() {
            return PS_GENDER_ID == "2";
        }
    }

}