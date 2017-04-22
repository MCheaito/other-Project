using System;
using System.IO;

namespace enncoded
{
	/// <summary>
	/// 
	/// </summary>
	public class ProcessEncodeFile
	{
		public string sourceFilename; 
		public string targetFilename;
		private FileStream fr;
		private BinaryReader nFileID;
		private System.IO.StreamWriter nOutFileID; 

		public ProcessEncodeFile(string inFile, string outFile)
		{
			sourceFilename = inFile; 
			targetFilename = outFile;
		}
		public ProcessEncodeFile(string inFile )
		{
			sourceFilename = inFile; 
			targetFilename = sourceFilename.Substring(0,sourceFilename.IndexOf("."))+".txt";
		}
		public void doEncoding()
		{
			fr = new FileStream(sourceFilename,FileMode.Open,FileAccess.Read);

			nFileID = new BinaryReader(fr);
			nOutFileID = new StreamWriter(targetFilename);    //open in write text mode
			if(nOutFileID == null)
			{
				return; 
			}
			// extract the output file name only
			string sInFile = sourceFilename.Substring(sourceFilename.LastIndexOf("\\")+1);
			//output the first line to text file
			//'640 is hardcoded here(need to fix this)
			nOutFileID.WriteLine("begin 640 " + sInFile);

			//begin encoding line by line
			EncodeLine ();

			//print the last end line
			nOutFileID.WriteLine( "end");
    
			nOutFileID.Close();
			fr.Close();
			nFileID.Close();
		}
		private void EncodeLine()
		{
			int arrayLength = 45;
			byte[] bytLine;
			string sLine;
			

			while (true)
			{
				bytLine = nFileID.ReadBytes(arrayLength);
				// get 45 bytes
				Byte[] bytLine1 = new Byte[bytLine.Length + 3];

				// 
				bytLine.CopyTo(bytLine1,0);

				// Add 3 null at the end
				bytLine1.SetValue(Convert.ToByte(0),bytLine1.Length-1);
				bytLine1.SetValue(Convert.ToByte(0),bytLine1.Length-2);
				bytLine1.SetValue(Convert.ToByte(0),bytLine1.Length-3);

				// Get size/ Copy size
				int nLen = bytLine1.Length - 3 ; 
                nOutFileID.Write(Convert.ToChar(Encode(nLen)));

				string EL = ""; 
				for (int i = 0; i < nLen; i+=3)
				{
					Byte[] byt = new Byte[3]; 
					//
					byt[0] = bytLine1[i];
					byt[1] = bytLine1[i+1];
					byt[2] = bytLine1[i+2];


					EL = EL +  Encode_OutDec(byt);
				}
				nOutFileID.WriteLine(EL);
				if (bytLine.Length ==0)
				{
					break;
				}

			}

			
		}

		private string  Encode_OutDec(Byte[] sStr) 
			{
			int nOne, nTwo , nThree ;
			string retValue; 

			nOne = Convert.ToInt32(sStr[0]);              // 'this is my *p
			nTwo = Convert.ToInt32(sStr[1]);    //  'this is my p[1]
			nThree = Convert.ToInt32(sStr[2]);    //'this is my p[2]

			retValue =  Convert.ToChar(Encode(nOne / 4)).ToString() + 
				Convert.ToChar(Encode(((nOne * 16) & 48)|((nTwo / 16) & 15))).ToString() + 
				Convert.ToChar(Encode(((nTwo *  4) & 60)|((nThree / 64) & 3))).ToString() + 
				Convert.ToChar(Encode(nThree & 63)).ToString();
			return retValue;
		}

		private int Encode(int nChar )
		{
			//((c) ? ((c) & 077) + ' ': '`')
			if (nChar != 0 )
			{
				return (nChar & 63) + 32;
			}
			return 96; // ie. Asc("`")
		}
	}
}

