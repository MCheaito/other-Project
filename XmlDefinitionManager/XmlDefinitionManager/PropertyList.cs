using System;
using System.Collections.Specialized;
using System.Windows.Forms.Design;
using System.ComponentModel;
using System.Drawing;

namespace XmlDefinitionManager
{
	//Interface for the class to show in propertygrid
	interface ICustomClass
	{
		PropertyList PublicProperties
		{
			get;
			set;
		}
	}


	public class PropertyList : NameObjectCollectionBase
	{		
		public void Add(Object value)  
		{
			//The key for the object is taken from the object to insert
			this.BaseAdd(((myProperty)value).Name, value );
		}

		public void Remove(String key)  
		{
			this.BaseRemove( key );
		}

		public void Remove(int index)  
		{
			this.BaseRemoveAt( index );
		}		

		public void Clear()  
		{
			this.BaseClear();
		}

		public myProperty this[ String key ]  
		{
			get  
			{
				return (myProperty)(this.BaseGet(key));
			}
			set  
			{
				this.BaseSet(key,value);
			}
		}

		public myProperty this[ int indice ]  
		{
			get  
			{
				return (myProperty)(this.BaseGet(indice));
			}
			set  
			{
				this.BaseSet(indice,value);
			}
		}

	}




}
