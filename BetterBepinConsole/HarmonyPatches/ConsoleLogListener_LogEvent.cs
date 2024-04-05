using System;
using System.Text;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace BetterBepinConsole.HarmonyPatches;

[HarmonyPatch(typeof(ConsoleLogListener))]
[HarmonyPatch(nameof(ConsoleLogListener.LogEvent))]
internal static class ConsoleLogListener_LogEvent
{
    private const LogLevel ErrorLevels = LogLevel.Fatal | LogLevel.Error;

    static ConsoleLogListener_LogEvent()
    {
        
    }
    
    private static bool Prefix(object sender, LogEventArgs eventArgs)
    {
        var builder = new StringBuilder();
        string timestamp = $"{DateTime.Now:HH:mm:ss.fff}";
        
        if ((ErrorLevels & eventArgs.Level) == eventArgs.Level)
        {
            // timestamp
            builder.Append("[".FillColourAs(TextColour.Red));
            builder.Append(timestamp);
            builder.Append("] ");

            // mod name
            builder.Append("[");
            builder.Append(eventArgs.Source.SourceName);
            builder.Append("] ");

            // log level
            builder.Append("[");
            builder.Append(eventArgs.Level);
            builder.Append("] ");

            // content
            builder.Append(eventArgs.Data);
            builder.Append("".FillColourAs(0));
            builder.Append(Environment.NewLine);
        }
        else if (eventArgs.Level == LogLevel.Warning)
        {
            // timestamp
            builder.Append("[".FillColourAs(TextColour.BrightYellow));
            builder.Append(timestamp);
            builder.Append("] ");

            // mod name
            builder.Append("[");
            builder.Append(eventArgs.Source.SourceName);
            builder.Append("] ");
            
            // log level
            builder.Append("[");
            builder.Append(eventArgs.Level);
            builder.Append("] ");

            // content
            builder.Append(eventArgs.Data);
            builder.Append("".FillColourAs(0));
            builder.Append(Environment.NewLine);
        }
        else
        {
            builder.Append("[".ColourAs(TextColour.White));
            builder.Append(timestamp.ColourAs(TextColour.Green));
            builder.Append("] ".ColourAs(TextColour.White));

            // mod name
            builder.Append("[".ColourAs(TextColour.White));
            builder.Append(eventArgs.Source.SourceName.ColourAs(TextColour.BrightCyan));
            builder.Append("] ".ColourAs(TextColour.White));
            
            // log level
            builder.Append("[");
            builder.Append(eventArgs.Level == LogLevel.Debug
                ? eventArgs.Level.ToString().ColourAs(TextColour.Blue)
                : eventArgs.Level.ToString().ColourAs(TextColour.Green));
            builder.Append("] ".ColourAs(TextColour.White));
            
            // content
            builder.Append(eventArgs.Data);
            builder.Append(Environment.NewLine);
        }
        
        ConsoleManager.ConsoleStream?.Write(builder.ToString());

        return false;
    }
}