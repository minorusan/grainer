using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Crysberry.Console
{
	[RequireComponent(typeof(ConsoleWindowOutputBehaviour))]
	public class ConsoleWindowInputBehaviour : MonoBehaviour
	{
		private bool canPerformInput = true;

		private ConsoleWindowOutputBehaviour output;
		
		public event Action<string> DidEnterCommand = delegate(string s) {  }; 
		public InputField CommandsInputField;

		private void Awake()
		{
			output = GetComponent<ConsoleWindowOutputBehaviour>();
			CommandsInputField.onEndEdit.AddListener(OnCommandEntered);
		}

		public void ManualCommandInput()
		{
			canPerformInput = false;
			OnCommandEntered(CommandsInputField.text);
			StartCoroutine(ResetInputState());
		}

		private void OnCommandEntered(string commandText)
		{
			if (!canPerformInput)
			{
				return;
			}
			var commandComponents = commandText.Split(' ');
			var command = commandComponents[0];
			
			var commandParameters = new List<string>();
			if (commandComponents.Length > 0)
			{
				for (int i = 1; i < commandComponents.Length; i++)
				{
					commandParameters.Add(commandComponents[i]);
				}
			}
			
			output.LogStringToConsoleWindow(commandText, EConsoleTextType.Command);
			var result = ConsoleCommandsRegistry.InvokeCommand(command, commandParameters.ToArray());
			output.LogStringToConsoleWindow(result, EConsoleTextType.Result);

			DidEnterCommand(commandText);

			CommandsInputField.text = "";
		}

		/// <summary>
		/// Required to prevent double command input, when submitting through button
		/// </summary>
		private IEnumerator ResetInputState()
		{
			yield return new WaitForEndOfFrame();
			canPerformInput = true;
		}
	}
}

