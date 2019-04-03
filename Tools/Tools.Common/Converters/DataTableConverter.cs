﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

namespace Tools.Common.Converters
{
	public class DataTableConverter
	{
		public DataTable ConvertToDatatable<T>(List<T> data)
		{
			PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
			var table = new DataTable();
			for (var i = 0; i < props.Count; i++)
			{
				PropertyDescriptor prop = props[i];
				if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
					table.Columns.Add(prop.Name, prop.PropertyType.GetGenericArguments()[0]);
				else
					table.Columns.Add(prop.Name, prop.PropertyType);
			}

			var values = new object[props.Count];
			foreach (T item in data)
			{
				for (var i = 0; i < values.Length; i++)
				{
					values[i] = props[i].GetValue(item);
				}
				table.Rows.Add(values);
			}
			return table;
		}

		public List<T> ConvertDatatableToList<T>(DataTable dt)
		{
			PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
			List<T> lst = new List<T>();

			foreach (DataRow dr in dt.Rows)
			{
				// Create the object of T
				var ob = Activator.CreateInstance<T>();

				for (var i = 0; i < props.Count; i++)
				{
					PropertyDescriptor prop = props[i];
					foreach (DataColumn dc in dt.Columns)
					{
						// Matching the columns with fields
						if (prop.Name == dc.ColumnName)
						{
							// Get the value from the datatable cell
							object value = dr[dc.ColumnName];
							if (string.IsNullOrWhiteSpace(value.ToString()) && prop.PropertyType == typeof(string))
							{
								value = "";
							}

							if (prop.PropertyType == typeof(int))
							{
								value = int.Parse(value.ToString());
							}
							if (prop.PropertyType == typeof(Guid))
							{
								value = new Guid(value.ToString());
							}
							if (prop.PropertyType == typeof(decimal))
							{
								value = decimal.Parse(value.ToString());
							}
							if (prop.PropertyType == typeof(bool))
							{
								value = bool.Parse(value.ToString());
							}
							if (prop.PropertyType == typeof(DateTime))
							{
								value = DateTime.Parse(value.ToString());
							}

							// Set the value into the object
							prop.SetValue(ob, value);
							break;
						}
					}
				}
				lst.Add(ob);
			}
			return lst;
		}
	}
}
