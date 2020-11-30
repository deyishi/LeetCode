using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.System_Design.Design_Parking_Lot
{
    public abstract class Vehicle
    {
        public string licensePlate { get; set; }
        public CarType type { get;set; }
    }

    public class Car : Vehicle { 
    }

    public enum CarType
    { 
        small,
        medium,
        large,
        xLarge
    }
}
