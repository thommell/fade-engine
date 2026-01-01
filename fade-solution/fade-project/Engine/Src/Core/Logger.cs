using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using fade_project.Core.Services; // for Caller Info
using fade_project.Core.Services.Enums;

namespace fade_project.Core;

public static class LogExtender {
    public static void Log(this object sender, LogType type, string message,
        [CallerLineNumber] int line = 0,
        [CallerMemberName] string member = "")
    {
        Logger.Log(sender, message, type, line, member);
    }
}
    
public static class Logger {
    private static readonly Dictionary<LogType, ConsoleColor> EnumColors = new(){
        { LogType.FATAL, ConsoleColor.Red },
        { LogType.WARN , ConsoleColor.Yellow },
        { LogType.INFO , ConsoleColor.Blue },
        { LogType.DEBUG , ConsoleColor.Green },
    };

    public static void Log(object sender, string message, LogType type, int line = 0, string member = "")
    {
        string scriptName = sender.GetType().BaseType == null || sender.GetType().BaseType == typeof(object) || sender.GetType().BaseType == typeof(Service)
            ? sender.GetType().Name 
            : sender.GetType().BaseType?.Name;

        Console.BackgroundColor = EnumColors[type];
        Console.WriteLine($"[{scriptName}-{Enum.GetName(typeof(LogType), type)}] Line: {line}: {message}");
        Console.BackgroundColor = ConsoleColor.White;
    }
}