﻿using StructureMap.Configuration.DSL;

namespace DevStartPage.Web.AngularCli.App_Config
{
    public class ObjectRegistry : Registry
    {
        public ObjectRegistry()
        {
            Scan(scan =>
            {
                scan.TheCallingAssembly();
                scan.WithDefaultConventions();
            });
        }
    }
}