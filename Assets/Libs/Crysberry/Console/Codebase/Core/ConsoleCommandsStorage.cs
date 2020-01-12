using System;
#if UNITY_EDITOR
using UnityEditor; 
#endif

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

        [CrysberryConsoleMember("poschk", "Check position")]
        private static string CheckPosition(string[] args)
        {
            var position = new Vector3(Convert.ToSingle(args[0]),Convert.ToSingle(args[1]), Convert.ToSingle(args[2]));
            return $"Walkable status of position ({position}) is '{position.IsWalkable()})'";
        }
        
        [CrysberryConsoleMember("optinfo", "Get optimization level information")]
        private static string OptInfo(string[] args)
        {
            var reached = OptimizatonBehaviour.OptimizationLevelGoalReached ? "" : "not";
            var level = OptimizatonBehaviour.OptimizationLevel;
            var averageFps = Mathf.RoundToInt(FrameDataBehaviour.FreshFrameData);
            return $"Optimization goal {reached} reached.\n::Optimisation level is {level}.\n::Average frame rate:{averageFps}.\n::Device model:{SystemInfo.deviceModel}";
        }
        
        [CrysberryConsoleMember("optrst", "Reset optimizer")]
        private static string OptRst(string[] args)
        {
            OptimizatonBehaviour.ResetOptimizationSettings();
            return $"Optimizer reset";
        }
        
        [CrysberryConsoleMember("passall", "Pass all levels")]
        private static string PassAll(string[] args)
        {
            LevelsHistory.CurrentLevelID = 999;
            return $"All levels opened";
        }
        
        [CrysberryConsoleMember("rstlvl", "Reset level progress")]
        private static string RstLVL(string[] args)
        {
            
            LevelsHistory.ClearHistory();
            return $"All levels closed";
        }
        
        [CrysberryConsoleMember("win", "Win current level")]
        private static string WinLVL(string[] args)
        {
            GameObject.FindObjectOfType<GameOutcomeBehaviour>().ForceWin();
            return $"Win current level";
        }
    }
}