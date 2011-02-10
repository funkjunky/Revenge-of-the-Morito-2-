using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Morito.Screens;

namespace Morito
{
    class FloatingWeapon : FullyPhysicalObject
    {
        Weapons _weapon;

        public FloatingWeapon(GameScreen gameScreen, Weapons weapon)
            : base(gameScreen)
        {
            _weapon = weapon;
        }

        public override void Draw()
        {
            base.Draw();
        }

        public override void Collide(isCollidable fellowCollider)
        {
            
        }
    }
}
