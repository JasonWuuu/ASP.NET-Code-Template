using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rapid.Service
{
    public static class Constants
    {
        public const string RAPID_DB_CONNECTION = "RapidDbContext";

        public static class ConfigAppSettings
        {
            public const string ANIMAL_PREFIX = "AnimalPrefix";
        }

        public static class AnimalType
        {
            public const string DOG = "DOG";
            public const string CAT = "CAT";
            public const string OTHER = "OTHER";
        }

    }
}
