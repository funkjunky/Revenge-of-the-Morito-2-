using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Morito.Screens;

namespace Morito
{
    enum Weapons
    {
        TestGun            = 0,
        Fan                = 1,
        RemoteMineLauncher = 2
    }


    class WeaponGenerator
    {
        public static FloatingWeapon generateFloatingWeapon(Weapons weapon, GameScreen gameScreen)
        {
            AttachedWeapon tempWeapon = generateAttachedWeapon(weapon, gameScreen, null);

            return new FloatingWeapon(gameScreen, weapon)
                       {
                           ObjectModel = tempWeapon.ObjectModel,
                           BSphere = tempWeapon.ObjectModel.Meshes[0].BoundingSphere
                       };
        }

        public static AttachedWeapon generateAttachedWeapon(Weapons weapon, GameScreen gameScreen, Ship owner)
        {
            AttachedWeapon aWeapon;
            switch (weapon)
            {
                case Weapons.TestGun:
                    aWeapon = new TestGun(gameScreen, owner);
                    break;
                
                case Weapons.Fan:
                    aWeapon = new Fan(gameScreen, owner);
                    break;

                case Weapons.RemoteMineLauncher:
                    aWeapon = new RemoteMineLauncher(gameScreen, owner);
                    break;

                default:
                    aWeapon = null;
                    break;
            }

            return aWeapon;
        }
    }
}
