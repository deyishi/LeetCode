using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.System_Design.Design_Parking_Lot
{
    public abstract class ParkingSpot
    {
        public int id { get; set; }
        public CarType type { get; set; }
        public bool occupied { get; set; }

        public ParkingSpot(CarType type)
        {
            this.type = type;
        }

        public void assignVehicle()
        {
            occupied = true;
        }

        public void removeVehicle()
        {
            occupied = false;
        }
    }

    public class XLargeSpot : ParkingSpot
    {
        public XLargeSpot() : base(CarType.xLarge)
        {
        }
    }

    public class LargeSpot : ParkingSpot
    {
        public LargeSpot() : base(CarType.large)
        {
        }
    }

    public class MediumSpot : ParkingSpot
    {
        public MediumSpot() : base(CarType.medium)
        {
        }
    }
    public class SmallSpot : ParkingSpot
    {
        public SmallSpot() : base(CarType.small)
        {
        }
    }
}
