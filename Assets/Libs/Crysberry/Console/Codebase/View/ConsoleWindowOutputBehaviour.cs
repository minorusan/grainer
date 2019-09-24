using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Crysberry.Console
{
	public enum EConsoleTextType
	{
		Result, Command
	}
	
	public class ConsoleWindowOutputBehaviour : MonoBehaviour
	{
		public ScrollRect ScrollRect;
		public GameObject OutputWindow;
		public GameObject CommandNameTextPrefab;
		public GameObject CommandResultTextPrefab;

		public void LogStringToConsoleWindow(string message, EConsoleTextType type)
		{
			var prefab = type == EConsoleTextType.Command ? CommandNameTextPrefab : CommandResultTextPrefab;
			var instance = Instantiate(prefab, OutputWindow.transform);
			instance.GetComponentInChildren<Text>().text = message;
			Invoke("ScrollToBottom", 0.1f);
		}

		public void ClearWindow()
		{
			var childCount = OutputWindow.transform.childCount;
			for (int i = 0; i < childCount; i++)
			{
				Destroy(OutputWindow.transform.GetChild(i).gameObject);
			}
		}

		private void ScrollToBottom()
		{
			ScrollRect.normalizedPosition = Vector2.zero;
		}
	}
}

