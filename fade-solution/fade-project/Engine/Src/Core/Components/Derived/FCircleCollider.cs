using fade_project.Core.Components.BaseAbstract.BaseAbstract;
using fade_project.Core.Components.BaseAbstract.Interfaces;
using fade_project.Core.Event.Types;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace fade_project.Core.Components.BaseAbstract;

public sealed class FCircleCollider : FCollider, IDrawableComponent {
    private int _radius;
    private Vector2 _center;
    private FSpriteRenderer _renderer;
    
    public int Radius => _radius;
    public Vector2 Center => _center;

    public FCircleCollider(int radius = 3) {
        _radius = radius;
    }

    public override void Initialize() {
        _renderer = GetComponent<FSpriteRenderer>();
        base.Initialize();
    }

    protected override void UpdateCollider(MoveEvent e) {
        _center = Transform.Position;
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