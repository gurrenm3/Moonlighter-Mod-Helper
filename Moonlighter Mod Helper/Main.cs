using BepInEx;
using HarmonyLib;
using System;
using System.Reflection;

namespace Moonlighter_Mod_Helper
{
    [BepInPlugin("GurrenM4.Moonlighter_Mod_Helper", "Moonlighter Mod Helper", "1.0.0")]
    public class Main : BaseUnityPlugin
    {
        internal static Assembly modAssembly = Assembly.GetExecutingAssembly();
        internal static string modName = $"{modAssembly.GetName().Name}";
        internal static string modDir = $"{Environment.CurrentDirectory}\\BepInEx\\{modName}";

        void Awake()
        {
            new Harmony($"GurrenM4_{modName}").PatchAll(modAssembly);
            Logger.LogInfo($"{modName} has loaded");
        }
    }
}