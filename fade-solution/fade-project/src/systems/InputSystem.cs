using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace fade_project.systems;

public sealed class InputSystem {
    private MouseState _mouseState;
    private KeyboardState _kbState;
    
    //TODO: Later?
    // private MouseState _previousMouseState;
    // private KeyboardState _previousKbState;
    
    public void Update(GameTime gameTime) {
        _kbState = Keyboard.GetState();
        _mouseState = Mouse.GetState();
    }

    public bool IsKeyUp(Keys key) => _kbState.IsKeyUp(key);
    public bool IsKeyPressed(Keys key) => _kbState.IsKeyDown(key);

    public bool IsMouseLeftUp() => _mouseState.LeftButton == ButtonState.Pressed;
    public bool IsMouseRightUp() => _mouseState.RightButton == ButtonState.Pressed;
}