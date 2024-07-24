using SequoiaEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biosphere
{
    public enum DietType
    {
        Herbivore,
        Carnivore,
        Omnivore
    }

    public enum Sex
    { 
        Male,
        Female
    }

    public enum AnimalSpecies
    {
        Bunny,
        Wolf
    }

    public enum PlantSpecies
    {
        Grass,
        Bush,
        Tree,
    }


    public class Animal : Component
    {
        public string AnimalName;

        // What kind of food they eat
        public DietType Diet;

        public int NumOffspringInLiter;

        // How often they have offspring
        public float ReproductionRate;

        // Male or Female
        public Sex BiologicalSex;

        // Indicate what type of animal they are
        public AnimalSpecies AnimalSpecies;

        // What do they eat (of animals)
        public List<AnimalSpecies> Prey;

        // What eats them (of animals)
        public List<AnimalSpecies> Predators;

        // If something is territorial, it will attack it even if they aren't part of the predators and not part of the AnimalsInGroup.
        // TODO: Might add something that calculates its chance of survival in attacking and if too low, it avoids it instead.
        public bool IsTerritorial;

        // If they eat vegetation, what do they eat?
        public List<PlantSpecies> Plants;

        // I.E. These are the animals that it will want to stay around
        public List<GameObject> AnimalsInGroup;

        public Animal(string animalName,
            AnimalSpecies animalSpecies,
            DietType dietType = DietType.Carnivore,
            float reproductionRate = 10000f,
            Sex biologicalSex = Sex.Male,
            List<AnimalSpecies> prey = null,
            List<AnimalSpecies> predators = null, 
            List<PlantSpecies> plants = null,
            bool isTerritorial = false,
            List<GameObject> animalsInGroup = null
            )
        {
            this.AnimalName = animalName;
            this.AnimalSpecies = animalSpecies;
            this.Diet = dietType;
            this.ReproductionRate = reproductionRate;
            this.BiologicalSex = biologicalSex;
            this.Prey = prey ?? new List<AnimalSpecies>();
            this.Predators = predators ?? new List<AnimalSpecies>();
            this.Plants = plants ?? new List<PlantSpecies>();
            this.IsTerritorial = isTerritorial;
            this.AnimalsInGroup = animalsInGroup ?? new List<GameObject>();
        }


    }
}
