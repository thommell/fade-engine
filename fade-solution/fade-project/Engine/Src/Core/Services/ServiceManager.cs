
using System;
using System.Collections.Generic;
using fade_project.Core.Services.Derived;
using fade_project.Core.Services.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace fade_project.Core.Services;

internal sealed class ServiceManager {
    private Dictionary<Type, Service> _services = [];
    private static ServiceManager _instance;

    public Dictionary<Type, Service> Services => _services;
    internal static ServiceManager Instance => _instance ??= new ServiceManager();

    public void Initialize(ContentManager content) {
        foreach (KeyValuePair<Type, Service> service in Services) {
            service.Value.Initialize(content);
        }
        Logger.Log(LogType.INFO, "Services have initialized.");
    }
    public void Load(SpriteBatch spriteBatch, ContentManager content) {
        foreach (KeyValuePair<Type, Service> service in Services) {
            service.Value.Load(spriteBatch, content);
        }
        Logger.Log(LogType.INFO, "Services have loaded.");            
    }

    public void LateLoad(SpriteBatch spriteBatch, ContentManager content) {
        foreach (KeyValuePair<Type, Service> service in Services) {
            service.Value.LateLoad(spriteBatch, content);
        }
    }
    
    public void Update(GameTime gameTime) {
        foreach (KeyValuePair<Type, Service> service in Services) {
            service.Value.Update(gameTime);
        }
    }

    public void Draw(SpriteBatch spriteBatch) {
        foreach (KeyValuePair<Type, Service> service in Services) {
            service.Value.Draw(spriteBatch);
        }
    }
    
    // Doesn't handle adding services during run-time!
    internal void AddService(Service service) {
        if (service == null) return;
        _services.Add(service.GetType(), service);
    }

    internal T GetService<T>() where T : Service {
    T service = (T)Services[typeof(T)];
    if (service == null) {
        Logger.Log(LogType.WARN, $"Requested service doesn't exist.");
        return null;
    }
    return service;
    }
}
