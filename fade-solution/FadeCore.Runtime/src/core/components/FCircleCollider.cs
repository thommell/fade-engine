using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FadeCore.Runtime;

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
        float distanceSquared = Vector2.DistanceSquared(center, circle.center);
        float radiusSum = radius + circle.radius;

        return distanceSquared <= radiusSum * radiusSum;
    }
        
    public override bool IntersectsBox(FBoxCollider box) {
        Vector2 closestPoint = Vector2.Clamp(center, Transform.Position, Transform.Position * Transform.Scale);
        float distanceSquared = Vector2.DistanceSquared(center, closestPoint);

        return distanceSquared <= radius * radius;
    }

    public void Draw(SpriteBatch spriteBatch) {
        throw new NotImplementedException();
    }
}