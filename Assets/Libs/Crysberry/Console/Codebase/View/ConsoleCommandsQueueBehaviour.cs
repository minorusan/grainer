using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Crysberry.Console
{
	[RequireComponent((typeof(ConsoleWindowInputBehaviour)))]
	public class ConsoleCommandsQueueBehaviour : MonoBehaviour
	{
		private int currentCommandIndex;

		private int CurrentCommandIndexWrapper
		{
			get { return currentCommandIndex; }
			set
			{
				currentCommandIndex = value;
				if (currentCommandIndex <= 0)
				{
					currentCommandIndex = commandsQueue.Count;
				}
				
				if (currentCommandIndex >= commandsQueue.Count)
				{
					currentCommandIndex = 0;
				}
			}
		}
		
		private List<string> commandsQueue = new List<string>();
		private ConsoleWindowInputBehaviour input;

		public InputField InputField;

		private void Awake()
		{
			input = GetComponent<ConsoleWindowInputBehaviour>();
			input.DidEnterCommand += InputOnDidEnterCommand;
		}

		private void Update()
		{
			if (commandsQueue.Count > 0)
			{
				if (Input.GetKeyDown(KeyCode.UpArrow))
				{
					CurrentCommandIndexWrapper++;
					InputField.text = commandsQueue[CurrentCommandIndexWrapper];
				}

				if (Input.GetKeyDown(KeyCode.DownArrow))
				{
					CurrentCommandIndexWrapper--;
					InputField.text = commandsQueue[CurrentCommandIndexWrapper];
				}
			}
		}

		private void InputOnDidEnterCommand(string obj)
		{
			commandsQueue.Add(obj);
		}
	}
}

