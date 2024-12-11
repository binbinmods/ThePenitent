using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using static Obeliskial_Essentials.Essentials;
using Obeliskial_Essentials;
using System.IO;
using UnityEngine;
using System;

namespace ThePenitent
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [BepInDependency("com.stiffmeds.obeliskialessentials")]
    [BepInDependency("com.stiffmeds.obeliskialcontent")]
    [BepInProcess("AcrossTheObelisk.exe")]
    public class Plugin : BaseUnityPlugin
    {
        internal int ModDate = int.Parse(DateTime.Today.ToString("yyyyMMdd"));
        private readonly Harmony harmony = new(PluginInfo.PLUGIN_GUID);
        internal static ManualLogSource Log;

        public static string debugBase = "Binbin - Testing " + subclassName + " ";
        
        public static string characterName = "Cain";

        public static string subclassName = "The Penitent"; // needs caps

        private void Awake()
        {
            Log = Logger;
            Log.LogInfo($"{PluginInfo.PLUGIN_GUID} {PluginInfo.PLUGIN_VERSION} has loaded!");
            // register with Obeliskial Essentials
            RegisterMod(
                _name: PluginInfo.PLUGIN_NAME,
                _author: "binbin",
                _description: "Cain, The Penitent.",
                _version: PluginInfo.PLUGIN_VERSION,
                _date: ModDate,
                _link: @"https://github.com/binbinmods/ThePenitent",
                _contentFolder: "ThePenitent",
                _type: ["content", "hero", "trait"]
            );

            LogDebug("Magnus Pet Text - " + Texts.Instance.GetText("magnusPet1"));
            // apply patches
            harmony.PatchAll();
        }    

        internal static void LogDebug(string msg)
        {
            Log.LogDebug(debugBase + msg);
        }
        internal static void LogInfo(string msg)
        {
            Log.LogInfo(debugBase + msg);
        }
        internal static void LogError(string msg)
        {
            Log.LogError(debugBase + msg);
        }
    }
}
