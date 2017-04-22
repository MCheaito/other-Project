using System;
using System.ComponentModel;
using System.Collections.Specialized;
using System.Collections.Generic;

namespace XmlDefinitionManager
{
    public enum PropertyTypes { Text, List };

	//The class to show in the propertygrid must inherit CustomClass
	//test Class with three public properties
	public class PropertyClass: ICustomClass
	{
		string id;
		string name;
        List<string> container;
        PropertyTypes type;

		PropertyList publicproperties=new PropertyList();

		public string Id
		{

			get
			{
				return id;
			}
			set
			{
				id=value;
			}
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
        public PropertyTypes Type
		{
			get
			{
				return type;
			}
			set
			{
				type=value;
			}
		}
        public List<string> Container
        {
            get
            {
                return container;
            }
            set
            {
                container = value;
            }


        }


        public PropertyClass(string id, string name, PropertyTypes type)
        {
            this.id = id;
            this.name = name;
            this.type = type;
            this.container = new List<string>();

            this.PublicProperties.Add(new myProperty("Id","Properties"));
            this.PublicProperties.Add(new myProperty("Name","Properties"));
            this.PublicProperties.Add(new myProperty("Type", "Properties"));
            this.publicproperties.Add(new myProperty("Container", "Misc"));

        }
		//ICustomClass implementation
		public PropertyList PublicProperties
		{
			get
			{
				return publicproperties;
			}
			set
			{
				publicproperties=value;
			}
		}		
	}
}
