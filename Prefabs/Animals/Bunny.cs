using SequoiaEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Biosphere
{
    public static class Bunny
    {
        public static GameObject Create(Vector2 position, List<GameObject> animalsInGroup = null)
        {
            GameObject gameObject = new();

            Animal animal = new Animal("bunny", AnimalSpecies.Bunny, DietType.Herbivore, animalsInGroup: animalsInGroup);
            gameObject.Add(animal);

            


            return gameObject;
        }

    }
}
