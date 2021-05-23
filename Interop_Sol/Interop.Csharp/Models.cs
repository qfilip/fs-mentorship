using System;

namespace Interop.Csharp
{
    public enum eVesselType
    {
        Rocket, Spaceplane, Satelite
    }

    public class Vessel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public eVesselType Type { get; set; }
    }
}
