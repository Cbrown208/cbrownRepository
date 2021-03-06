﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web.Mvc;

namespace CustomException
{
	internal class Program
	{
		private static void Main()
		{

			var names = new List<string>();

			names.Add("Bob");
			names.Add("Joe");
			names.Add("Three");

			PrintList(names);

			Console.WriteLine(names.Count);
			if (names.Count > 2)
			{
				//throw new CustomException(names.ToArray());
				//throw new CustomException1("Exception with parameter value '{0}'", names.ToString());
			}

			try
			{
				if (names.Count > 2)
				{
					throw new CustomException1("CustomException");
				}
			}
			catch (ArgumentException argEx)
			{

			}
			catch (CustomException1 sqlEx)
			{
				Console.WriteLine(sqlEx.Message);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}

			Console.ReadLine();
		}

		private static void PrintList(List<string> names)
		{
			foreach (string value in names)
			{
				Console.WriteLine(value);
			}
		}
	}

	public class CustomException : ApplicationException
	{
		public CustomException(string[] message)
			: base(string.Join("\n", message.ToArray()))
		{
		}
	}

	[Serializable]
	public class CustomException1 : ApplicationException
	{
		public CustomException1()
			: base()
		{
		}

		public CustomException1(string message)
			: base(message)
		{
		}

		public CustomException1(string format, params object[] args)
			: base(string.Format(format, args))
		{
		}

		public CustomException1(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		public CustomException1(string format, Exception innerException, params object[] args)
			: base(string.Format(format, args), innerException)
		{
		}

		protected CustomException1(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
	public class ShieldingHandleErrorAttribute : HandleErrorAttribute
	{
		public override void OnException(ExceptionContext filterContext)
		{
			if (filterContext.ExceptionHandled)
			{
				return;
			}
			if (ExceptionType.IsInstanceOfType(filterContext.Exception) == false)
			{
				return;
			}

			ActionResult result;
			if (filterContext.Exception.GetType() == typeof(ApplicationException))
			{

				Console.ReadLine();
				//result = GetCustomException(filterContext);
				//filterContext.HttpContext.Response.StatusCode = 400;
				//var id = GenerateNewGuid();
				//LogException(filterContext.Exception, id);
			}

			else
			{
			}
		}
	}
}
