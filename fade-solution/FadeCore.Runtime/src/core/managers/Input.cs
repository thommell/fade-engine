using Microsoft.Xna.Framework.Input;

namespace FadeCore.Runtime;

public static class Input {
    private static MouseState currentMouseState;
    private static KeyboardState currentKbState;
    private static MouseState previousMouseState;
    private static KeyboardState previousKbState;
    
    public static void Update() {
        previousKbState = currentKbState;
        currentKbState = Keyboard.GetState();
        
        previousMouseState = currentMouseState;
        currentMouseState = Mouse.GetState();
    }

    public static bool IsKeyUp(Keys key) => currentKbState.IsKeyUp(key);
    public static bool IsKeyDown(Keys key) => currentKbState.IsKeyDown(key);
    public static bool IsKeyPressed(Keys key) => currentKbState.IsKeyDown(key) && previousKbState.IsKeyUp(key);

    public static bool IsMouseLeftDown() => currentMouseState.LeftButton == ButtonState.Pressed;
    public static bool IsMouseRightDown() => currentMouseState.RightButton == ButtonState.Pressed;
    public static bool IsMouseMiddleDown() => currentMouseState.MiddleButton == ButtonState.Pressed;
    public static bool WasMouseLeftClicked() => IsMouseLeftDown() && previousMouseState.LeftButton == ButtonState.Released;
    public static bool WasMouseRightClicked() => IsMouseRightDown() && previousMouseState.RightButton == ButtonState.Released;
    public static bool WasMouseMiddleClick() => IsMouseMiddleDown() && previousMouseState.MiddleButton == ButtonState.Released;
}