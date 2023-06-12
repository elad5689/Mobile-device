using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task6
{
   
    class Navigation :AppSystem, IApp
    {

        NavigationManager navManager;

        public NavigationManager NavManager 
        { 
            get => navManager;
        }

        public Navigation(string appName, float appPrice, NavigationManager navManager)
            :base (appName, appPrice)
        {
            this.navManager = navManager;
        }

        public override string ToString()
        {
            return base.ToString() + "\n" + NavManager.ToString();
        }

        public void AddVAT(float vatPercent)
        {
            appPrice += appPrice * vatPercent / 100; 
        }

        public override string AppSystemPurpose()
        {
            return "Cath The Road-Choose The Best Way";
        }

    }
}
