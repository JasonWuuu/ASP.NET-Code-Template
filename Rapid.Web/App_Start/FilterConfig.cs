using Rapid.Core.Common;
using System.Web;
using System.Web.Mvc;

namespace Rapid.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new RapidHandleErrorAttribute());
            filters.Add(new HandleErrorAttribute());
        }
    }
}