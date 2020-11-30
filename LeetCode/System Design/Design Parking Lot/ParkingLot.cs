using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.System_Design.Design_Parking_Lot
{
   
    public class ParkingLot
    {
        //Lot[] Lots; // we don't want to loop through the lots to find a free spot, we can use a stack
        private string name;
        private string location;
        private double ratePerHour;
        
        private int xlCount;
        private int lCount;
        private int mCount;
        private int sCount;
        private int xlLimit;
        private int lLimit;
        private int mLimit;
        private int sLimit;


        private Dictionary<string, ParkingTicket> tickets;
        private Dictionary<string, ParkingLot> lots;


        public ParkingLot() {
            // load name, location, rate and limit either from memory or from database 
            name = "David Parking Lot";
            location = "Wisconsin";
            ratePerHour = 20;

            xlCount = 0;
            lCount = 0;
            mCount = 0;
            sCount = 0;

            xlLimit = 250;
            lLimit = 250;
            mLimit = 250;
            sLimit = 250;

            tickets = new Dictionary<string, ParkingTicket>();
        }  
           

        public ParkingTicket Enter(Vehicle vehicle) {
            // check vehicle type and put it into correct lot 
            

            var ticket =  GenerateTicket(vehicle);
            tickets.Add(ticket.ticketNumber, ticket);
            return ticket;
        }

        public void Exit(ParkingTicket ticket) {

            var vehicle = ticket.vehicle;
            // free lot
            if (vehicle.type == CarType.large)
            {
                lCount--;
            }

            if (vehicle.type == CarType.xLarge)
            {
                xlCount--;
            }

            if (vehicle.type == CarType.medium)
            {
                mCount--;
            }

            if (vehicle.type == CarType.small)
            {
                sCount--;
            }
            tickets.Remove(ticket.ticketNumber);
            // ask user for payment

        }

        public int GetAvailableLot(Vehicle vehicle) {
            if (vehicle.type == CarType.large && lCount == lLimit ||
                vehicle.type == CarType.xLarge || xlCount == xlLimit ||
                vehicle.type == CarType.medium || mCount == mLimit ||
                vehicle.type == CarType.small || sCount == sLimit)
            {
                return -1;
            }

            if (vehicle.type == CarType.large)
            {
                lCount++;
                return lCount;
            }

            if (vehicle.type == CarType.xLarge)
            {
                xlCount++;
                return xlCount;
            }

            if (vehicle.type == CarType.medium)
            {
                mCount++;
                return mCount;
            }

            if (vehicle.type == CarType.small)
            {
                sCount++;
                return sCount;
            }

            return 0;
        }
        public ParkingTicket GenerateTicket(Vehicle vehicle) {
            string ticketNumber = Guid.NewGuid().ToString();
            while(tickets.ContainsKey(ticketNumber)) {
                ticketNumber = Guid.NewGuid().ToString();
            }

            
            return new ParkingTicket {
                ticketNumber = ticketNumber,
                liscensePlate = vehicle.licensePlate,
                createdOn = DateTime.Now,
                vehicle = vehicle
            };
        }

    }
}
