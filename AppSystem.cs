using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task6
{
    abstract class AppSystem :IComparable
    {
        protected int appNum;
        protected string appName;
        protected float appPrice;
        protected DateTime addAppDate;
        static int numOfAppCreated;

        public int AppNum
        {
            get
            {
                return appNum;
            }
        }

        public string AppName 
        {
            get
            {
                return appName;
            }
            set
            {
                if(value == null || value == "")
                {
                    throw new ArgumentNullException("Application name can't be empty");
                }
                appName = value;
            }
        }

        public float AppPrice 
        { 
            get
            {
                return appPrice;
            }
            set
            {
                if(value < 0)
                {
                    throw new ArgumentException("Application price can't be negative");
                }
                appPrice = value;
            }
        }

        public DateTime AddAppDate 
        { 
            get
            {
                return addAppDate;
            }
        }

        public AppSystem(string appName, float appPrice)
        {
            AppName = appName;
            AppPrice = appPrice;
            addAppDate = DateTime.Now;
            appNum = ++numOfAppCreated;
        }

        public override string ToString()
        {
            return "Number: " + appNum + "  name: " + appName + "  price: " + appPrice + "  download on: " + AddAppDate.ToShortDateString();
        }

        public abstract string AppSystemPurpose();

        public int CompareTo(object obj)
        {
            if(!(obj is AppSystem))
            {
                throw new Exception("This is not AppSystem, cant copmare it");
            }
            return AppName.CompareTo(((AppSystem)obj).AppName);
        }
        
    }
}
