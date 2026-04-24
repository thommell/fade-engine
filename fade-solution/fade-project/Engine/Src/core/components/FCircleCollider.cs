using fade_project.Core.Components.BaseAbstract.BaseAbstract;
using fade_project.Core.Components.BaseAbstract.Interfaces;
using fade_project.Core.Event.Types;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace fade_project.Core.Components.BaseAbstract;

public sealed class FCircleCollider : FCollider, IDrawableComponent {
    private int radius;
    private Vector2 center;
    private FSpriteRenderer renderer;
    
    public int Radius => radius;
    public Vector2 Center => center;

    public FCircleCollider(int radius = 3) {
        this.radius = radius;
    }

    public override void Initialize() {
        renderer = GetComponent<FSpriteRenderer>();
        base.Initialize();
    }

    protected override void UpdateCollider(MoveEvent e) {
        center = Transform.Position;
    }

    public override bool Intersects(FCollider other) =>
        other.IntersectsCircle(this);

    public override bool IntersectsCircle(FCircleCollider circle) {
        return false;
    }
        
    public override bool IntersectsBox(FBoxCollider box) {
        return true;
    }

    public void Draw(SpriteBatch spriteBatch) {
        // spriteBatch.Draw();
    }
}