using System;
using System.Runtime.InteropServices;
using BepInEx;
using HarmonyLib;

namespace BetterBepinConsole
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetStdHandle(int nStdHandle);
        
        [DllImport("kernel32.dll")]
        private static extern bool SetConsoleMode(IntPtr handle, uint dwMode);
        
        [DllImport("kernel32.dll")]
        private static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint lpMode);

        
        private void Awake()
        {
            IntPtr handle = GetStdHandle(-11);
            GetConsoleMode(handle, out uint mode);
            SetConsoleMode(handle, mode | 4);
            
            var harmony = new Harmony(PluginInfo.PLUGIN_GUID);
            harmony.PatchAll();
            
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
        }
    }
}