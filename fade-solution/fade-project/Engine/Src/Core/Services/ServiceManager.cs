
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace fade_project.Core.Services;

public sealed class ServiceManager {
    private Dictionary<Type, Service> _services = [];
    private static ServiceManager _instance;
    
    public Dictionary<Type, Service> Services => _services;
    public static ServiceManager Instance => _instance ??= new ServiceManager();

    public void Initialize() {
        foreach (KeyValuePair<Type, Service> service in _services) {
            service.Value.Initialize();
        }
    }
    public void Load(SpriteBatch spriteBatch) {
        foreach (KeyValuePair<Type, Service> service in _services) {
            service.Value.Load(spriteBatch);
        }
    }
    public void Update(GameTime gameTime) {
        foreach (KeyValuePair<Type, Service> service in _services) {
            service.Value.Update(gameTime);
        }
    }

    public void Draw(SpriteBatch spriteBatch) {
        foreach (KeyValuePair<Type, Service> service in _services) {
            service.Value.Draw(spriteBatch);
        }
    }
    
    // Doesn't handle adding services during run-time!
    internal void AddService(Service service) {
        if (service == null) return;
        _services.Add(service.GetType(), service);
    } 
    internal T GetService<T>() where T : Service => (T)_services[typeof(T)];
}
