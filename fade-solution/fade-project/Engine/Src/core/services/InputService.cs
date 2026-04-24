using fade_project.Core.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace fade_project.systems;

public sealed class InputService : Service {
    private MouseState _currentMouseState;
    private KeyboardState _currentKbState;
    private MouseState _previousMouseState;
    private KeyboardState _previousKbState;
    private SceneService _scene;
    
    public override void Update(GameTime gameTime) {
        _previousKbState = _currentKbState;
        _currentKbState = Keyboard.GetState();
        
        _previousMouseState = _currentMouseState;
        _currentMouseState = Mouse.GetState();
    }

    public bool IsKeyUp(Keys key) => _currentKbState.IsKeyUp(key);
    public bool IsKeyDown(Keys key) => _currentKbState.IsKeyDown(key);
    public bool IsKeyPressed(Keys key) => _currentKbState.IsKeyDown(key) && _previousKbState.IsKeyUp(key);

    public bool IsMouseLeftDown() => _currentMouseState.LeftButton == ButtonState.Pressed;
    public bool IsMouseRightDown() => _currentMouseState.RightButton == ButtonState.Pressed;
    public bool IsMouseMiddleDown() => _currentMouseState.MiddleButton == ButtonState.Pressed;
    public bool IsMouseLeftClicked() => IsMouseLeftDown() && _previousMouseState.LeftButton == ButtonState.Released;
    public bool IsMouseRightClicked() => IsMouseRightDown() && _previousMouseState.RightButton == ButtonState.Released;
    public bool IsMouseMiddleClick() => IsMouseMiddleDown() && _previousMouseState.MiddleButton == ButtonState.Released;
}