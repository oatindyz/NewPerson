using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEB_PERSONAL.Class {

    public class LeaveBossData {

        public int LeaveBossID;
        public int LeaveID;
        public string CitizenID;
        public string Comment;
        public int? Allow;
        public int State;
        public DateTime? AllowDate;
        public string CancelComment;
        public int? CancelAllow;
        public DateTime? CancelAllowDate;
        public Person Person;

    }

}