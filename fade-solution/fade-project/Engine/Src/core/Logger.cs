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
    SceneManager,
    Unknown
}
    
public static class Logger {
    private static readonly Dictionary<LogType, ConsoleColor> enumColors = new() {
        { LogType.Fatal, ConsoleColor.Red },
        { LogType.Warn , ConsoleColor.Yellow },
        { LogType.Info , ConsoleColor.Blue },
        { LogType.Debug , ConsoleColor.Green },
    };

    public static void Log(object sender, string message, LogType type) {
        // what the fuck
        LogSource loggingSource = sender switch {
            Type t when t == typeof(FTransform) => LogSource.Transform,
            Type t when t == typeof(FadeEngine) => LogSource.Engine,
            Type t when t == typeof(SceneManager) => LogSource.SceneManager,
            _ => LogSource.Unknown
        };
        
        if (type == LogType.Fatal) {
            Console.BackgroundColor = enumColors[type];
            Console.ForegroundColor = ConsoleColor.Black;
        }
        else
            Console.ForegroundColor = enumColors[type];

        Console.WriteLine($"[{loggingSource.ToString().ToUpper()}-{Enum.GetName(typeof(LogType), type)}]: {message}");
        Console.ResetColor();
    }
}