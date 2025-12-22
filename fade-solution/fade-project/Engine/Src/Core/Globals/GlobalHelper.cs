using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;

namespace fade_project.Core.Globals;

public static class GlobalHelper {
    private static ContentManager _content;
    
    public static ContentManager Content {
        get => _content ?? throw new InvalidOperationException("Content hasn't been added yet?");
        set {
            if (_content != null) throw new InvalidOperationException("Content may only be set once.");
            _content = value;
        }
    }

    
    

}