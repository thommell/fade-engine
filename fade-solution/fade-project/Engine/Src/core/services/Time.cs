using fade_project.Core;
using Microsoft.Xna.Framework;

namespace fade_project.systems;

public static class Time {
    private const float FixedDelta = 1f / 50f;
    private const int MaxFixedSteps = 5;
    
    private static float fixedAccumulator = 0f;
    
    public static void Update(GameTime gameTime) {
        float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
        fixedAccumulator += dt;

        int fixedSteps = 0;

        while (fixedAccumulator >= FixedDelta) {
            FixedUpdate(FixedDelta);
            fixedAccumulator -= FixedDelta;
            fixedSteps++;
        }

        if (fixedSteps == MaxFixedSteps) {
            fixedAccumulator = 0;
        }
    }

    private static void FixedUpdate(float fixedDeltaTime) {
        SceneManager.FixedUpdate(fixedDeltaTime);
    }
}