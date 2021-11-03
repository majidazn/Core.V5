using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Framework.Api
{
    public class SkipApiResultAttribute : Attribute, IFilterMetadata
    {
    }
}
