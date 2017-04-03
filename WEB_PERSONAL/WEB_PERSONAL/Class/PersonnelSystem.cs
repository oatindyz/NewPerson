using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace WEB_PERSONAL.Class {

    public class PersonnelSystem {

        public Person LoginPerson;
        public string Redirect;


        public PersonnelSystem() {
                  
        }

        public static PersonnelSystem GetPersonnelSystem(Control control) {
            return ((PersonnelSystem)control.Page.Session["PersonnelSystem"]);
        }

    }

}