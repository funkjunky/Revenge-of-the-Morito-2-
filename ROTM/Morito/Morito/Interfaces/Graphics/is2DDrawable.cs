using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Morito
{
    interface is2DDrawable
    {
        Texture2D Texture { get; set; }
        Vector2 Position { get; set; }
        float Scale { get; set; }
        float RotationAngle { get; set; }
        Vector2 Origin { get; set; }
    }
}
