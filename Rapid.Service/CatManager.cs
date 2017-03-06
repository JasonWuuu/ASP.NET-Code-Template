using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rapid.Service
{
    public class CatManager : AnimalBaseManager, IAnimalManager
    {
        public override string Eat()
        {
            return "Cat eat fish.";
        }
    }
}
