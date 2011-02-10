using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace serializerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Barn mybarn = new Barn
            {
                animal1 = new animal { name = "pig", age = 2 },
                animal2 = new animal { name = "cow", age = 4 },
                colour = "off-red",
                otherAnimals = new List<animal>()
            };

            mybarn.otherAnimals.Add(new animal { name = "coyote", age = 1 });
            mybarn.otherAnimals.Add(new animal { name = "dog", age = 1 });
            mybarn.otherAnimals.Add(new animal { name = "lamb", age = 1 });


            System.Console.Write(mybarn.serialize());
            System.Console.ReadKey();
        }
    }
}
