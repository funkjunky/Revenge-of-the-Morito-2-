using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Morito
{
    interface is3DDrawable
    {
        Model ObjectModel { get; set; }
        Vector3 Position { get; set; }

        Camera ReferenceCamera { get; set; }

        void DrawModel();
    }
}