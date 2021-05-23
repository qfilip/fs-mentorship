using System;
using System.Collections.Generic;
using System.Linq;

namespace Interop.Csharp
{
    public class Functionality
    {
        private static List<Vessel> _vessels = new List<Vessel>()
        {
            new Vessel() { Id = 1, Name = "Dunester", Type = eVesselType.Rocket },
            new Vessel() { Id = 2, Name = "Joolooloo", Type = eVesselType.Spaceplane },
            new Vessel() { Id = 1, Name = "KerbSat", Type = eVesselType.Satelite },
        };

        public  Vessel GetVessel(int id)
        {
            return _vessels.FirstOrDefault(x => x.Id == id);
        }

    }
}
