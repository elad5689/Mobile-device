using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task6
{
    class Social :AppSystem, IApp
    {
        int appRating;
        bool forOrganization;

        public int AppRating 
        { 
            get
            {
                return appRating;
            }
            set
            {
                if(value < 1 || value > 5)
                {
                    throw new ArgumentException("Application can be between 1 to 5");
                }
                appRating = value;
            }
        }

        public bool ForOrganization 
        { 
            get => forOrganization; 
            set => forOrganization = value; 
        }

        public Social(string appName, float appPrice, int appRating, bool forOrganization) 
            :base (appName, appPrice)
        {
            AppRating = appRating;
            ForOrganization = forOrganization;
        }

        public void AddVAT(float vatPercent)
        {
            appPrice += appPrice * vatPercent / 100;
        }

        public override string ToString()
        {
            return base.ToString() + "  rating: " + AppRating + "  is for organization: " + forOrganization.ToString();
        }

        public override string AppSystemPurpose()
        {
            return "Far away and talking close";
        }
    }
}
