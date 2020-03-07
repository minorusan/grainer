using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Crysberry.Console
{
	[AttributeUsage(AttributeTargets.Method, Inherited = false)]
	public class CrysberryConsoleMemberAttribute : Attribute
	{
		private readonly string methodName;
		private readonly string methodDescription;

		public CrysberryConsoleMemberAttribute(string methodName, string methodDescription)
		{
			this.methodName = methodName;
			this.methodDescription = methodDescription;
		}

		public string MethodName
		{
			get { return methodName; }
		}

		public string MethodDescription
		{
			get { return methodDescription; }
		}
	}
	
	public static class ConsoleCommandsRegistry 
	{
		private static Dictionary<string, MethodInfo> _consoleCommands = new Dictionary<string, MethodInfo>();
		
		static ConsoleCommandsRegistry()
		{
			var methods = RetrieveMethods();
			InitializeConsoleCommands(methods);
		}

		private static MethodInfo[] RetrieveMethods()
		{
			var assembly = Assembly.GetCallingAssembly();
			var methods = assembly.GetTypes()
				.SelectMany(t => t.GetMethods(BindingFlags.NonPublic | BindingFlags.Static))
				.Where(m => m.GetCustomAttributes(typeof(CrysberryConsoleMemberAttribute), false).Length > 0)
				.ToArray();
				
			return methods;
		}

		private static void InitializeConsoleCommands(MethodInfo[] methods)
		{
			foreach (var methodInfo in methods)
			{
				var attribute = GetAttribute(methodInfo);
				_consoleCommands.Add(attribute.MethodName, methodInfo);
			}
		}

		public static string[] GetCommandsAndDescriptions()
		{
			var keyCollection = new List<string>(_consoleCommands.Count);
			foreach (var key in _consoleCommands.Keys)
			{
				keyCollection.Add(key);
			}
			
			keyCollection.Sort();
			
			var keysAndDescriptions = new List<string>(_consoleCommands.Count);
			foreach (var key in keyCollection)
			{
				var description = GetAttribute(_consoleCommands[key]).MethodDescription;
				keysAndDescriptions.Add(string.Format("{0} :: {1}", key, description));
			}

			return keysAndDescriptions.ToArray();
		}

		public static string InvokeCommand(string name, string[] args)
		{
			if (_consoleCommands.ContainsKey(name))
			{
				var result = _consoleCommands[name].Invoke(typeof(ConsoleCommandsStorage), new []{args});
				return (string)result;
			}

			return string.Format("CrysberyConsole::Command {0} is not defined.", name);
		}

		private static CrysberryConsoleMemberAttribute GetAttribute(MethodInfo info)
		{
			return info.GetCustomAttributes(typeof(CrysberryConsoleMemberAttribute), false)[0] as CrysberryConsoleMemberAttribute;
		}
	}
}