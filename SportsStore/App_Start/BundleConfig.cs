using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace SportsStore
{
    public class BundleConfig
    {
        // Remember to call the below RegisterBundles in the Global.asax.cs
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/validation").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery.validate.js",
                "~/Scripts/jquery.validate.unobtrusive.js"));
            // The bundle feature can find the version by typing {version} - now we don't have to change the files evere time a new version is downloaded
        }
    }
}