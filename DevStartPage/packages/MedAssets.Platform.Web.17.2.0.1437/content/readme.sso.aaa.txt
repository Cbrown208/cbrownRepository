This information is derived from the following docs:
http://twiki.broadlane.com/index.php?title=A4
http://rtwiki/lib/exe/fetch.php?media=rcmplatform:dev:devs:integrate_with_aaa_and_a4.docx
http://mac.medassets.com/sites/spendmanagement/perfanalytics/tech/SS/sscoll/Gateway Project Phase 1/Create Gateway (Phase 1)/Gateway_Application_Requirements_v1.doc

Here are the manual steps:
1. Have Global.asax.cs inherit from AaaWifWebApplication
	•	Note: If you are handling the Application_Start or Application_BeginRequest events, you will need to change the method signature as follows:
			protected override void OnApplicationStart()
			{
				// Your code goes here
			}
			protected override void OnApplicationBeginRequest()
			{
				// Your code goes here
			}

2. In web.config, search for ####### and add relevant values values 
