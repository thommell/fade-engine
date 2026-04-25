using Microsoft.Xna.Framework;

namespace FadeCore.Runtime;

public sealed class FBoxCollider : FCollider {
    private Rectangle hitbox;
    private FSpriteRenderer renderer;

    public Rectangle Hitbox => hitbox;
    public override void Initialize() {
        renderer = GetComponent<FSpriteRenderer>();
        base.Initialize();
    }

    protected override void UpdateCollider(MoveEvent moveEvent) =>
        hitbox = new Rectangle((int)moveEvent.NewPosition.X - (int)Transform.Origin.X,
            (int)moveEvent.NewPosition.Y - (int)Transform.Origin.Y,
            renderer.Texture.Width,
            renderer.Texture.Height);

    public override bool Intersects(FCollider other) => other.IntersectsBox(this);
    public override bool IntersectsBox(FBoxCollider box) => hitbox.Intersects(box.Hitbox);
    public override bool IntersectsCircle(FCircleCollider circle) => false;
}