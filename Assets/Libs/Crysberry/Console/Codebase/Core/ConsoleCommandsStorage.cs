using UnityEngine;

namespace Crysberry.Console
{
    public static class ConsoleCommandsStorage
    {
        [CrysberryConsoleMember("clrprfs", "Clears PlayerPrefs. Usage: clrprfs <key1 key2 key3> - removes specified keys; no parameter - removes all ")]
        private static string ConsoleClearPlayerPrefs(string[] args)
        {
            if (args.Length > 0)
            {
                var keysString = "[";
                foreach (var arg in args)
                {
                    PlayerPrefs.DeleteKey(arg);
                    keysString += arg + " ";
                }

                keysString += "]";
                
                return "Cleared PlayerPrefs keys: " + keysString;
            }

            PlayerPrefs.DeleteAll();
           
            return "Player prefs cleared";
        }
        
        [CrysberryConsoleMember("help", "Get help about console usage")]
        private static string ConsoleHelp(string[] args)
        {
            var allCommands = ConsoleCommandsRegistry.GetCommandsAndDescriptions();
            string resultString = "\nAll commands list.\n";
            foreach (var command in allCommands)
            {
                resultString += command + "\n";
            }

            resultString += "\n Please, add custom commands in ConsoleCommandsStorage.cs";
            return resultString;
        }
        
        [CrysberryConsoleMember("cls", "Clear console window")]
        private static string ConsoleClear(string[] args)
        {
            GameObject.FindObjectOfType<ConsoleWindowOutputBehaviour>().ClearWindow();
            return "Window cleared";
        }
    }
}