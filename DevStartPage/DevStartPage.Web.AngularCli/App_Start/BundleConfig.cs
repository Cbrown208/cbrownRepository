using System.Web;
using System.Web.Optimization;

namespace DevStartPage.Web.AngularCli
{
	public class BundleConfig
	{
		public static void RegisterBundles(BundleCollection bundles)
		{
			//To debug against Prod settings you will need to use the else statement logic.
			//Or change the debug="true" -> "false" in the web.base.config 
			if (HttpContext.Current.IsDebuggingEnabled)
			{
				BundleTable.EnableOptimizations = false;
				bundles.Add(new ScriptBundle("~/mainApp/scriptBundles")
					.Include(
						"~/mainApp/runtime.*",
						"~/mainApp/polyfills.*",
						"~/mainApp/styles.*",
						"~/mainApp/vendor.*",
						"~/mainApp/main.*"
					));
			}

			else
			{
				BundleTable.EnableOptimizations = true;
				bundles.Add(new ScriptBundle("~/mainApp/scriptBundles")
					.Include(
						"~/mainApp/runtime.*",
						"~/mainApp/polyfills.*",
						"~/mainApp/main.*"
					));

				bundles.Add(new StyleBundle("~/mainApp/styleBundles").Include(
					"~/mainApp/styles.*"
				));
			}
		}
	}
}
