using System;
using System.Collections.Generic;
using fade_project.Core.Services.Enums;

namespace fade_project.Core.Services.Derived;
    
public static class Logger {
    private static readonly Dictionary<LogType, ConsoleColor> EnumColors = new(){
        { LogType.FATAL, ConsoleColor.Red },
        { LogType.WARN , ConsoleColor.Yellow },
        { LogType.INFO , ConsoleColor.Blue },
        { LogType.DEBUG , ConsoleColor.Green },
    };
    
    public static void Log(LogType type, string message) {
        LogMessage(message, type);
    }

    private static void LogMessage(string message, LogType type) {
        Console.ForegroundColor = EnumColors[type];
        Console.WriteLine($"{Enum.GetName(typeof(LogType), type)}: " + message);
        Console.ForegroundColor = ConsoleColor.White;
    }
}