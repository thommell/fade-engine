using System;
using System.Collections.Generic;
using fade_project.Core.Components.BaseAbstract;

namespace fade_project.Core;

public enum LogType {
    Fatal,
    Warn, 
    Info,
    Debug
}

public enum LogSource {
    Engine,
    Transform,
    Unknown
}

public static class LogExtender {
    public static void Log(this object sender, LogType type, string message) {
        Logger.Log(sender, message, type);
    }
}
    
public static class Logger {
    private static readonly Dictionary<LogType, ConsoleColor> EnumColors = new() {
        { LogType.Fatal, ConsoleColor.Red },
        { LogType.Warn , ConsoleColor.Yellow },
        { LogType.Info , ConsoleColor.Blue },
        { LogType.Debug , ConsoleColor.Green },
    };

    public static void Log(object sender, string message, LogType type) {
        LogSource loggingSource = sender switch {
            FTransform => LogSource.Transform,
            FadeEngine => LogSource.Engine,
            _ => LogSource.Unknown
        };
        
        if (type == LogType.Fatal) {
            Console.BackgroundColor = EnumColors[type];
            Console.ForegroundColor = ConsoleColor.Black;
        }
        else
            Console.ForegroundColor = EnumColors[type];

        Console.WriteLine($"[{loggingSource.ToString().ToUpper()}-{Enum.GetName(typeof(LogType), type)}]: {message}");
        Console.ResetColor();
    }
}