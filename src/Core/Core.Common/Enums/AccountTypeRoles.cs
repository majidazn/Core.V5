using System.ComponentModel.DataAnnotations;

namespace Core.Common.Enums
{
    public enum AccountTypeRoles
    {
        [Display(Name = "مدیر مرکز")]
        CenterAdmin = 1195,

        [Display(Name = "کاربر مرکز")]
        CenterUser = 1196,

        [Display(Name = "مدیر پیوند")]
        EPDAdmin = 1197,

        [Display(Name = "کاربر پیوند")]
        EPDUser = 1198,

        [Display(Name = "مدیر دانشگاه")]
        UniAdmin = 1199,

        [Display(Name = "مسئول مدیران مرکز ")]
        CentersAdminResponsible = 1203,

        [Display(Name = "مسئول منابع انسانی")]
        HumanResourceResponsible = 1192,

        [Display(Name = "مسئول منابع انسانی مرکز")]
        HumanResourceResponsibleInCenter = 1201,

        [Display(Name = "بیمار")]
        Patient = 153,

        Admin = 1,
    }
}