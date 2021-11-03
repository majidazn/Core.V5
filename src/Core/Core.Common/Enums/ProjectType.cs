using System.ComponentModel.DataAnnotations;

namespace Core.Common.Enums
{
    public enum ProjectType : byte
    {
        [Display(Name = "ماهیت مرکز")]
        HISWeb = 23,

        [Display(Name = "ماهیت مرکز")]
        HISCloud = 24,

        [Display(Name = "برنامه های کمکی تحت Cloud")]
        UtilitiesUnderCloud = 25,

        [Display(Name = "HIS Windows Base")]
        HISWindowsBase = 26,

        [Display(Name = "MIS")]
        MIS = 27,

        [Display(Name = "Pacs")]
        Pacs = 28,
    }
}