using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slingshot
{
    public class Input
    {
        KeyboardState _previousState;

        public void Initialize()
        {
            _previousState = Keyboard.GetState();
        }

        public IEnumerable<Keys> ProcessInput()
        {
            KeyboardState state = Keyboard.GetState();
            
            List<Keys> keys = new List<Keys>(state.GetPressedKeys());

            foreach(var key in _previousState.GetPressedKeys())
            {
                keys.Remove(key);
            }

            return keys;        
        }
    }
}
