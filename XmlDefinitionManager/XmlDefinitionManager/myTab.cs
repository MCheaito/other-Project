using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms.Design;
using System.ComponentModel;
using System.Drawing;

namespace XmlDefinitionManager
{
    public class myTab : PropertyTab
    {
        public myTab()
        {
        }

        // get the properties of the selected component
        public override System.ComponentModel.PropertyDescriptorCollection GetProperties(object component, System.Attribute[] attributes)
        {
            PropertyDescriptorCollection properties;
            if (attributes != null)
                properties = TypeDescriptor.GetProperties(component, attributes);
            else
                properties = TypeDescriptor.GetProperties(component);

            //Componet must implement the ICUSTOMCLASS interface.
            ICustomClass bclass = (ICustomClass)component;

            //The new array of properties, based on the PublicProperties properties of "model"
            PropertyDescriptor[] arrProp = new PropertyDescriptor[bclass.PublicProperties.Count];

            for (int i = 0; i < bclass.PublicProperties.Count; i++)
            {
                //Find the properties in the array of the propertis which neme is in the PubliCProperties
                PropertyDescriptor prop = properties.Find(bclass.PublicProperties[i].Name, true);
                
                
                //Build a new properties
                arrProp[i] = TypeDescriptor.CreateProperty(prop.ComponentType, prop, new CategoryAttribute(bclass.PublicProperties[i].Category));
            }
            return new PropertyDescriptorCollection(arrProp);
        }
        public override System.ComponentModel.PropertyDescriptorCollection GetProperties(object component)
        {
            return this.GetProperties(component, null);
        }
        // PropertyTab Name
        public override string TabName
        {
            get
            {
                return "Properties";
            }
        }
        //Image of the property tab (return a blank 16x16 Bitmap)
        public override System.Drawing.Bitmap Bitmap
        {
            get
            {
                return new Bitmap(16, 16); ;
            }
        }
    }
}
