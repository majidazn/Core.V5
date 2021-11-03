using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Common.ViewModels.SMS
{
    public class SmsVisitCancellation
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CenterName { get; set; }
        public string ClinicName { get; set; }
        public string DoctorName { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Reason { get; set; }
        public string Sex { get; set; }
    }
}
