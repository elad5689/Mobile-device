using System;

namespace Task6
{
    class Program
    {
        const int NAVTAX = 12;
        const int SOCIALTAX = 13;

        static void Main(string[] args)
        {
            bool login = false;
            char choice;
            MobileDevice nokia = new MobileDevice("nokia", "1234");


            NavigationManager m1 = new NavigationManager("bat", (CarType)1);
            NavigationManager m2 = new NavigationManager("ruppin", (CarType)3);

            AppSystem n1 = new Navigation("waze", 10, m1);
            nokia.AddApp(n1);

            AppSystem n2 = new Navigation("maps", 12, m2);
            nokia.AddApp(n2);



            while (!login)
            {
                Console.WriteLine("\n***Login***");
                Login(ref nokia, ref login);
                nokia.Online = login;
            }

            while(nokia.Online)
            {
                try
                {
                    Console.WriteLine("\n1 - Download application");
                    Console.WriteLine("2 - Show most popular navigation app");
                    Console.WriteLine("3 - Navigate");
                    Console.WriteLine("4 - Print device details");
                    Console.WriteLine("5 - Sort appliation");
                    Console.WriteLine("6 - Close device");
                    Console.Write("\nEnter youe choice: ");

                    choice = char.Parse(Console.ReadLine());

                    switch(choice)
                    {
                        case '1':
                            AddApp(ref nokia);
                            break;

                        case '2':
                            Console.WriteLine(nokia.PopularNavigationApp().ToString()); 
                            break;

                        case '3':
                            Console.WriteLine("\nChoose navigation app by name"); 
                            nokia.ShowListAppNavigation();
                            Console.Write("\nEnter the apllication name you want to navigate with: ");
                            string name = Console.ReadLine();
                            Navigation tmp = SearchNavigation(ref nokia, name);
                            Console.WriteLine("\nCurrent location: " + tmp.NavManager.CurrentLocation);
                            tmp.NavManager.ShowRecentLocations();
                            Console.Write("\nEnter the adress you want to navigate to: ");
                            tmp.NavManager.AddAdress(Console.ReadLine());
                            Console.WriteLine("Have a safe and pleasent ride");
                            break;

                        case '4':
                            Console.WriteLine();
                            Console.WriteLine(nokia.ToString()); 
                            break;

                        case '5':
                            Array.Sort(nokia.Apps);
                            break;

                        case '6':
                            Console.WriteLine("\n**byebye**");
                            nokia.Online = false;
                            break;
                    }
                }
                catch(ArgumentNullException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        //This method get MobileDevice object and bool object
        //ask from the user the user name and password and try to connect  to mobile device
        //if try 9 times without success will get exception and close the program
        static void Login(ref MobileDevice device, ref bool login)
        {
            string userName;
            try
            {
                Console.Write("Enter user name: ");
                userName = Console.ReadLine();
                Console.Write("Enter paswword: ");
                login = device.Login(userName, Console.ReadLine());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Environment.Exit(1);
            }
        }

        //This method get MobileDevice object
        //ask the user what kind of application he want to download to the device
        static void AddApp(ref MobileDevice device)
        {
            try
            {
                Console.WriteLine("\nPress - 1 for navigation app\nPress - 2 for social app");
                Console.Write("\nEnter your choice: ");
                int x = int.Parse(Console.ReadLine());
                if (x != 1 && x != 2)
                {
                    throw new ArgumentException("\nArgumentException - I told you to choose 1 or 2");
                }
                if (x == 1)
                {
                    device.AddApp(AddNavigationApp());
                    Console.WriteLine("Application added" + "\n" + device.Apps[device.Appcount - 1].ToString());
                }
                else
                {
                    device.AddApp(AddSocialApp());
                    Console.WriteLine("Application added" + "\n" + device.Apps[device.Appcount - 1].ToString());
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("\nFormatException - I told you to choose 1 or 2");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //This method get from the user the details of navigation application download 
        static AppSystem AddNavigationApp()
        {
            Console.Write("Enter application name: ");
            string name = Console.ReadLine();
            Console.Write("Enter application price: ");
            float price = float.Parse(Console.ReadLine());
            Console.Write("Enter your current location: ");
            string location = Console.ReadLine();
            NavigationManager nv = new NavigationManager(location, CarType());
            AppSystem tmp = new Navigation(name, price, nv);
            ((Navigation)tmp).AddVAT(NAVTAX);
            
            return tmp;
        }

        //This method get from the user the details of social application download
        static AppSystem AddSocialApp()
        {
            Console.Write("Enter application name: ");
            string name = Console.ReadLine();
            Console.Write("Enter application price: ");
            float price = float.Parse(Console.ReadLine());
            Console.Write("Enter application rating value between 1-5: ");
            int rating = int.Parse(Console.ReadLine());
            AppSystem tmp = new Social(name, price, rating, ForOrganization());
            ((Social)tmp).AddVAT(SOCIALTAX);

            return tmp;
        }


        //this method ask the user for his kind of car when download navigation application
        static CarType CarType()
        {
            try
            {
                Console.WriteLine("\nChoose your car type");
                Console.WriteLine("\nPress - 1 for car\nPress - 2 for bike\nPress - 3 for taxi");
                Console.Write("\nEnter your choice: ");
                int x = int.Parse(Console.ReadLine());
                if (x != 1 && x != 2 && x != 3)
                {
                    throw new ArgumentException("\nArgumentException - I told you to choose 1 or 2");
                }
                return (CarType)x;
            }
            catch (FormatException)
            {
                Console.WriteLine("\nFormatException - I told you to choose 1 or 2");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return 0;
        }

        //this method ask the user for the rating of application when download social application
        static bool ForOrganization()
        {
            Console.WriteLine("This application is for organization?");
            Console.WriteLine("press 1 - Yes\npress 2 - No");
            Console.Write("\nEnter your choice: ");
            int x = int.Parse(Console.ReadLine());
            if (x != 1 && x != 2)
            {
                throw new ArgumentException("\nArgumentException - I told you to choose 1 or 2");
            }
            if(x == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        //This method get MobileDevice object and string
        //searching if the name of the app is on the device nd its navigation app
        //if found return Navigation object
        static Navigation SearchNavigation(ref MobileDevice device, string name)
        {
            foreach(AppSystem app in device.Apps)
            {
                if(app is Navigation && app.AppName == name)
                {
                    return (Navigation)app;
                }
            }
            throw new Exception("The application you want to navigate with does not exist");
        }
    }
}
