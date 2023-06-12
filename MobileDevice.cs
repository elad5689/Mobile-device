using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Threading;


namespace Task6
{
    class MobileDevice
    {
        string userName;
        string password;
        bool online;
        int connectionTry;
        AppSystem[] apps;
        int appcount;

        public string UserName 
        { 
            get
            {
                return userName;
            }
            set
            {
                if(!Regex.IsMatch(value, @"^[a-zA-Z]+$"))
                {
                    throw new ArgumentException("User name can contain only letters");
                }
                userName = value;
            }
        }

        public string Password { get => password; set => password = value; }
        public bool Online { get => online; set => online = value; }
        public int ConnectionTry { get => connectionTry; set => connectionTry = value; }
        public int Appcount { get => appcount; set => appcount = value; }
        public AppSystem[] Apps { get => apps;}

        public MobileDevice(string userName, string password)
        {
            UserName = userName;
            Password = password;
            apps = new AppSystem[0];
            appcount = 0;
        }

        public void AddApp(AppSystem appToAdd)
        {
            foreach (AppSystem app in Apps)
            {
                if (app.AppName == appToAdd.AppName)
                {
                    throw new ArgumentException($"The application: {appToAdd} is already exist on your device");
                }
            }
            Array.Resize(ref apps, Apps.Length + 1);
            Apps[appcount] = appToAdd;
            appcount++;
            
        }

        public void ShowListAppNavigation()
        {
            foreach (AppSystem app in Apps)
            {
                if(app is Navigation)
                {
                    Console.WriteLine("Number: " + app.AppNum + " name: " + app.AppName);
                }
            }
        }

        public override string ToString()
        {
            string result;

            result = "User name: " + userName + " password: " + password + " online: " + online.ToString() + " try to connect: " + connectionTry;
            result += $"\nYou have {appcount} application in your mobile device";
            foreach (AppSystem app in Apps)
            {
               result += ("\nNumber: " + app.AppNum + " name: " + app.AppName);
            }

            return result;
        }

        public Navigation PopularNavigationApp()
        {
            int i = 0, maxlocation = 0, maxPopular = 0;
            Navigation tmp;
            
            for (i = 0; i < Apps.Length; i++)
            {
                if((tmp = Apps[i] as Navigation) != null)
                {
                    if(tmp.NavManager.LocationCount > maxlocation)
                    {
                        maxlocation = tmp.NavManager.LocationCount;
                        maxPopular = i;
                    }
                }
            }
            if(maxlocation > 0)
            {
                return (Navigation)Apps[maxPopular];
            }
            throw new Exception("\nNo navigation app Or no popular one");
        }

        public bool Login(string userName, string password)
        {
            if (connectionTry == 9)
            {
                throw new Exception("You tried to login more than 9 times, the device is locked");
            }

            connectionTry++;

            if(this.userName == userName && this.password == password)
            {
                return true;
            }
            else
            {
                Console.WriteLine("\nWrong details, try again...");
                if (connectionTry > 3)
                {
                    Console.WriteLine("Wait 15 second");
                    Thread.Sleep(15000);
                }
                return false;
            }
        }
    }
}
