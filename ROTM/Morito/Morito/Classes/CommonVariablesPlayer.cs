using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;


namespace Morito
{
   public class CommonVariablesPlayer 
    {
       private int _health = 100;
       private int _shield = 100;

       public int Health 
       {
         get {return _health;}
       }
       public int Shield
       {
           get { return _shield; }
       }

       /// <summary>
       /// Reset all the Values!
       /// </summary>
       public void Reset()
       {
           _health = 100;
           _shield = 100;
       }

       /// <summary>
       /// Player was HIT!
       /// </summary>
       public void Hit()
       {
           if (_shield > 100)
           {
               _shield--;
           }
           else
           {
               _health--;
           }
       }

       /// <summary>
       /// Returns True if Alive and False if dead !
       /// </summary>
       public Boolean IsAlive()
       {
           if (_health > 0) return true;
           if (_health <= 0) return false;

           return false;
           
       }



       public CommonVariablesPlayer(Player player)
       {
       }

      
    }
}