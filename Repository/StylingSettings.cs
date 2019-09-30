using System;
using System.Collections.Generic;
using System.Text;
using Models;

namespace Repository
{
    public class StylingSettings
    {
        #region GLOBAL 
        //StylingModel StyleModel = new StylingModel();

        #endregion 

        public StylingModel StylingList(int indexNumber)
        {
            List<StylingModel> SM = new List<StylingModel>();

            // Light Mode = Index 0
            SM.Add(new StylingModel
            {
                // body

                // Label

                // textbox 

                // button

                // combobox 

            });

            // Dark Mode = Index 1
            SM.Add(new StylingModel
            {
                // body

                // Label

                // textbox 

                // button

                // combobox 

            });

            return SM[indexNumber];
        }
    }
}
