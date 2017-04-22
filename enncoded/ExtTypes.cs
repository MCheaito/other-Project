using System;

namespace enncoded
{
	/// <summary>
	/// 
	/// </summary>
	public class ExtTypes
	{
		public ExtTypes()
		{
			// 
			// TODO: Add constructor logic here
			//
		}
		public static int Asc(char value) 
		{ 
			int i = value;
			return i; 
		} 
		public static int Asc(string value) 
		{ 
			char[] c = value.ToCharArray(0,1);
			return Asc(c[0]); 
		} 
		
	}
}
