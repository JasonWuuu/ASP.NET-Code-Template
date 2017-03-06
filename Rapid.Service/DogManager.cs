using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rapid.Service
{
    public class DogManager : AnimalBaseManager, IAnimalManager
    {
        public override string Eat()
        {
            return "Dog eat bone.";
        }
    }
}
