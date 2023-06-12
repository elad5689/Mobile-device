using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task6
{
    public enum CarType { Car = 1 , Bike, Taxi};

    class NavigationManager
    {
        string currentLocation;
        string[] lastLocations;
        CarType carType;
        int locationCount;

        public string CurrentLocation 
        { 
            get
            {
                return currentLocation;
            }
            set
            {
                if (value == null || value == "")
                {
                    throw new ArgumentNullException("Current location can't be empty");
                }
                currentLocation = value;
            }
        }

        public CarType CarType 
        { 
            get => carType; 
            set => carType = value; 
        }

        public string[] LastLocations 
        {
            get => lastLocations;
        }

        public int LocationCount 
        { 
            get => locationCount;
        }

        public NavigationManager(string currentLocation, CarType carType)
        {
            CurrentLocation = currentLocation;
            CarType = carType;
            lastLocations = new string[0];
            locationCount = 0;
        }

        public override string ToString()
        {
            string result;

            result = "Current Location: " + currentLocation + "  car Type: " + carType + "  number of visited locations: " + LocationCount;
            result += $"\nList of the last locations you visit";

            if(lastLocations.Length > 0)
            {
                for (int i = 0; i < lastLocations.Length; i++)
                {
                    result += ($"\n{i + 1}: " + lastLocations[i]);
                }
            }
            else
            {
                result += "\nNo recent locations yet";
            }
            

            return result;
        }

        public void ShowRecentLocations()
        {
            if(LocationCount == 0)
            {
                Console.WriteLine("No recent locations yet");
                return;
            }
            Console.WriteLine("Last locations you visit: ");
            for(int i = 0; i < LastLocations.Length; i++)
            {
                Console.Write($"\nLocation {i + 1}: ");
                Console.WriteLine(LastLocations[i]);
            }
        }

        public void AddAdress(string adress)
        {
            foreach(string location in LastLocations)
            {
                if (location.CompareTo(adress) == 0)
                    return;
            }
            Array.Resize(ref lastLocations, lastLocations.Length + 1);
            LastLocations[LocationCount] = adress;
            locationCount++;
        }
    }
}
