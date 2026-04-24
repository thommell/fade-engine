using fade_project.Core.Event.Types;

namespace fade_project.Core.Components.BaseAbstract.BaseAbstract;

public abstract class FCollider : FComponent {
    public bool IsColliding { get; set; }
    public override void Initialize() {
        Owner.Events.Listen<MoveEvent>(UpdateCollider);
        base.Initialize();
    }
    public override void Load() {
        Owner.Events.Invoke(new MoveEvent(Owner, Owner.Transform.Position));
        base.Load();
    }
    protected abstract void UpdateCollider(MoveEvent e);
    public abstract bool Intersects(FCollider other);
    public abstract bool IntersectsCircle(FCircleCollider circle);
    public abstract bool IntersectsBox(FBoxCollider box);
}