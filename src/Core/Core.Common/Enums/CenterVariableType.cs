using System.ComponentModel.DataAnnotations;

namespace Core.Common.Enums
{
    public enum CenterVariableType : byte
    {
        [Display(Name = "مورد استفاده در برنامه")]
        InternalUsage = 2,

        [Display(Name = "برنامه ها")]
        Applications = 10,

        [Display(Name = "گروه مراکز")]
        CenterGroup = 20,

        [Display(Name = "عنوان مراکز")]
        CenterTitle = 22,

        [Display(Name = "HIS Web")]
        HIS_Web = 23,

        [Display(Name = "HIS Cloud")]
        HIS_Cloud = 24,

        [Display(Name = "برنامه های کمکی تحت Cloud")]
        Under_Cloud = 25,

        [Display(Name = "HIS Windows Base")]
        Windows_Base = 26,

        [Display(Name = "MIS")]
        MIS = 27,

        [Display(Name = "PACS")]
        PACS = 28,
    }
}