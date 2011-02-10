#region Using Declaration
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
#endregion Using Declaration

namespace Morito
{
    public interface isControllable
    {
        InputState Input { get; set; }
        //MouseState GameMouse { get; set; }

        //for lack of a better name. hmmm...
        //void applyControlsGamePad();

        //void applyControlsKeyboard();
    }
}
