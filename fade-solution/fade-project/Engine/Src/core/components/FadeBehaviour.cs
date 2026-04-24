using fade_project.Core.Event.Types;

namespace fade_project.Core.Components.BaseAbstract.BaseAbstract;

public abstract class FadeBehaviour : FComponent {
    public override void Load() {
        Owner.Events.Listen<CollisionEnterEvent>(OnCollisionEnter);
        Owner.Events.Listen<CollisionStayEvent>(OnCollisionStay);
        Owner.Events.Listen<CollisionExitEvent>(OnCollisionExit);
        base.Load();
    }

    protected virtual void OnCollisionEnter(CollisionEnterEvent data) {}
    protected virtual void OnCollisionStay(CollisionStayEvent data) { }
    protected virtual void OnCollisionExit(CollisionExitEvent data) {}
}