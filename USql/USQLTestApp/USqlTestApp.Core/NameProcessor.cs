using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Analytics.Interfaces;

namespace USqlTestApp.Core
{
	[SqlUserDefinedProcessor]
	public class NameProcessor : IProcessor
	{
		public override IRow Process(IRow input, IUpdatableRow output)
		{
			string first_name = input.Get<string>("first_name");
			string last_name = input.Get<string>("last_name");
			string name = first_name.Substring(0, 1) + "." + last_name;
			output.Set<string>("name", name);
			return output.AsReadOnly();
		}
	}
}
