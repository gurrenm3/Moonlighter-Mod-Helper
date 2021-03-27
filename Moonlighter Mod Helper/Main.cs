using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using Moonlighter_Mod_Helper.Api.Web;
using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace Moonlighter_Mod_Helper
{
    [BepInPlugin("GurrenM4.Moonlighter_Mod_Helper", "Moonlighter Mod Helper", "1.0.0")]
    public class Main : BaseUnityPlugin
    {
        internal static Main instance;
        internal static Assembly modAssembly = Assembly.GetExecutingAssembly();
        internal static string modName = $"{modAssembly.GetName().Name}";
        internal static string modDir = $"{Environment.CurrentDirectory}\\BepInEx\\{modName}";

        void Awake()
        {
            instance = this;
            new Harmony($"GurrenM4_{modName}").PatchAll(modAssembly);
            Logger.LogInfo($"{modName} has loaded");
        }

        void Update()
        {
            
        }

        internal static void LogMessage(string message)
        {
            instance.Logger.LogMessage(message);
        }

        internal static void LogError(string message)
        {
            instance.Logger.LogError(message);
        }

        internal static void Log(LogLevel level, string message)
        {
            instance.Logger.Log(level, message);
        }
    }
}