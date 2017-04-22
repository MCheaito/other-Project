using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms.Design;
using System.ComponentModel;
using System.Drawing;

namespace XmlDefinitionManager
{
    public class myProperty
    {
        string name = string.Empty;
        string category = string.Empty;
        string description = string.Empty;

        public myProperty()
        {
        }

        public myProperty(string pname, string pcat)
        {
            name = pname;
            category = pcat;
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public string Category
        {
            get
            {
                return category;
            }
            set
            {
                category = value;
            }
        }

        public string Description  
       {
            get
            {
                return description;
            }
            set
            {
                description = value;
            }
        }

    }
}
