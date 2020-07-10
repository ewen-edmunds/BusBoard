﻿using System.Globalization;
using System.Net;
using System.Threading;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace BusBoard.Web
{
  public class MvcApplication : System.Web.HttpApplication
  {
    protected void Application_Start()
    {
      ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
      AreaRegistration.RegisterAllAreas();
      FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
      RouteConfig.RegisterRoutes(RouteTable.Routes);
      BundleConfig.RegisterBundles(BundleTable.Bundles);
      
      Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-GB");
    }
  }
}
