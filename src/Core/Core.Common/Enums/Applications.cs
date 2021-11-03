using System.ComponentModel.DataAnnotations;

namespace Core.Common.Enums
{
    public enum Applications
    {
        [Display(Name = "متغیرهای سیستم")]
        SystemVariables = 296,

        [Display(Name = "کارکنان")]
        Personnel = 297,

        [Display(Name = "امنیت")]
        Security = 295,

        [Display(Name = "نوبت دهی")]
        Appointment = 248,

        [Display(Name = "ثبت عملکرد")]
        WorkHour = 289,

        [Display(Name = "مراکز")]
        Center = 293,

        [Display(Name = "درخواست خدمت")]
        TicketCenter = 570,

        [Display(Name = "کنسول پزشک")]
        DoctorConsole = 356,

        [Display(Name = "صندوق")]
        Finance = 360,

        [Display(Name = "CIS")]
        CISApp = 351,

        [Display(Name = "حسابداری")]
        Accounting = 374
    }
}