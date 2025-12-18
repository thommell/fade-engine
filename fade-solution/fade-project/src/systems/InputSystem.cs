using fade_project.containers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace fade_project.systems;

public sealed class InputSystem : SubSystem {
    private MouseState _mouseState;
    private KeyboardState _kbState;
    
    private MouseState _previousMouseState;
    private KeyboardState _previousKbState;
    
    public override void Update(GameTime gameTime) {
        // KB
        _previousKbState = _kbState;
        _kbState = Keyboard.GetState();
        
        // MOUSE
        _previousMouseState = _mouseState;
        _mouseState = Mouse.GetState();
    }

    public bool IsKeyUp(Keys key) => _kbState.IsKeyUp(key);
    public bool IsKeyDown(Keys key) => _kbState.IsKeyDown(key);
    public bool IsKeyPressed(Keys key) => _kbState.IsKeyDown(key) && _previousKbState.IsKeyUp(key);

    public bool IsMouseLeftDown() => _mouseState.LeftButton == ButtonState.Pressed;
    public bool IsMouseRightDown() => _mouseState.RightButton == ButtonState.Pressed;
    public bool IsMouseMiddleDown() => _mouseState.MiddleButton == ButtonState.Pressed;
    public bool IsMouseLeftClicked() => IsMouseLeftDown() && _previousMouseState.LeftButton == ButtonState.Released;
    public bool IsMouseRightClicked() => IsMouseRightDown() && _previousMouseState.RightButton == ButtonState.Released;
    public bool IsMouseMiddleClick() => IsMouseMiddleDown() && _previousMouseState.MiddleButton == ButtonState.Released;
}