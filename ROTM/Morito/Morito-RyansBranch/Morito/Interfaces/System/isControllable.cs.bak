#region Using Declaration
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
#endregion Using Declaration

namespace Morito
{
    public interface isControllable
    {
        KeyboardState CurrentKeyboard { get; set; }
        GamePadState CurrentGamePad { get; set; }
        MouseState GameMouse { get; set; }

        //for lack of a better name. hmmm...
        void applyControlsGamePad(GameTime gameTime);

        void applyControlsKeyboard(GameTime gameTime);
    }

    public static class isControllableDefinitions
    {
        //default behaviour function. This is here for testing, not to be used regularly. 
        //This can work as filler for yet to be implemented movement code.
        public static isPhysicsable updateMovementFromKeyboard(this isControllable controller, isPhysicsable physicsableObject)
        {
            Vector2 newVelocity = physicsableObject.Velocity;
            if (controller.CurrentKeyboard.IsKeyDown(Keys.Left))
                newVelocity.X = -5;
            else if (controller.CurrentKeyboard.IsKeyDown(Keys.Right))
                newVelocity.X = 5;
            else
                newVelocity.X = 0;

            if (controller.CurrentKeyboard.IsKeyDown(Keys.Up))
                newVelocity.Y = -5;
            else if (controller.CurrentKeyboard.IsKeyDown(Keys.Down))
                newVelocity.Y = 5;
            else
                newVelocity.Y = 0;

            physicsableObject.Velocity = newVelocity;

            return physicsableObject;
        }
    }
}
