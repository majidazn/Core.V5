using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Framework.BackgroundServiceConfigurations
{
    public class EnumHelper
    {
        public static string GetDisplayName(Enum enumValue)
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<DisplayAttribute>()
                            .GetName();
        }
    }
    public enum EnumLocalIntegrationEvent
    {
        Pending = 0,
        Ready = 1,
        Done = 2

    }

    public enum EnumTicketKind
    {
        Internal = 0,
        Peyvand = 1,
    }

    public enum EnumTicketStatus
    {
        Created = 500,
        InProgress = 501,
        Done = 502,
        AutoClosed = 503
    }

    public enum TicketGvariableTypes
    {
        /// <summary>
        /// نوع ارسال برای کاربران
        /// </summary>
        SendingType = 1
    }

    public enum SendingTypePersianString
    {
        [Display(Name = "داخلی")]
        Internal = 500,

        [Display(Name = "اطلاع به پیوند")]
        NotifyToEPD = 501,

        [Display(Name = "در حال بررسی ")]
        Pending = 502,

        [Display(Name = "ارجاع به مدیر پیوند")]
        ReferToEPDManager = 503,

        [Display(Name = "ارجاع مدیر مرکز ")]
        ReferToCenterManager = 504,

        [Display(Name = "ارجاع به واحد فروش")]
        ReferToSaleUnit = 505,

        [Display(Name = "ارجاع به واحد تولید")]
        ReferToProductUnit = 506,

        [Display(Name = "ارجاع به واحد تست")]
        ReferToTest = 507,

        [Display(Name = "رد شده")]
        DisApproved = 508,

        [Display(Name = "انجام شده")]
        Done = 509,

        [Display(Name = "در حال برسی مرکز")]
        CenterPending = 510,

        [Display(Name = "تایید شده مرکز")]
        CenterAccepted = 511,

        [Display(Name = "رد شده مرکز")]
        CenterDisApproved = 512,


    }

    //public enum EnumReferenceKind
    //{
    //    Internal = 0,
    //    Peyvand = 1,
    //}
}
