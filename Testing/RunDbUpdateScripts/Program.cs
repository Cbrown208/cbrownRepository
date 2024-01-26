// See https://aka.ms/new-console-template for more information
using RunDbUpdateScripts;

Console.WriteLine("Hello, World!");

RunSqlScriptsManager manager = new RunSqlScriptsManager();

manager.RunDbScripts();
