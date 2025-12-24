using System;
using fade_project.Core.Entities.Abstract;

namespace fade_project.Core.Components.BaseAbstract.BaseAbstract;

public abstract class FadeComponent : Component {
    public virtual void OnCollisionEnter(GameObject other) {}
    public virtual void OnCollisionExit() {}
}