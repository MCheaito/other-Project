using System;
using System.IO;

namespace enncoded
{
	/// <summary>
	/// 
	/// </summary>
	public class ProcessDecodeFile
	{
		const int LINE_LEN = 195;
		public string filePathName = null;
		public ProcessDecodeFile()
		{
			// 
			// TODO: Add constructor logic here
			//
		}
		public ProcessDecodeFile(string inFilePathName)
		{
			filePathName = inFilePathName;

		}

		public string DoDecoding()
		{
			StreamReader nFileID = new  StreamReader(filePathName);

			if (nFileID == null ) 
			{
				return null;
			}

			// Lire la premiere line 
			string sLine  = nFileID.ReadLine();
			if (sLine == null) 
			{
				return null;
			}

			if (! sLine.StartsWith("begin "))
			{
				return null;
			}
			
			int pos = sLine.IndexOf(" ");
			string sOutFile  = sLine.Substring(sLine.IndexOf(" ",pos + 1));
			sOutFile = sOutFile.Trim();
			
			// StreamWriter  nOutFileID  = new StreamWriter(sOutFile);	

			FileStream nOutFileID = new FileStream(sOutFile, FileMode.Create);
			// Create the writer for data.
			BinaryWriter w = new BinaryWriter(nOutFileID);

			//DecodeLine(nFileID, nOutFileID);
			DecodeLine(nFileID, w);

			nFileID.Close(); 
			w.Close();
			nOutFileID.Close();

			return sOutFile;
		}

		private void DecodeLine(StreamReader nIn ,BinaryWriter  nOut )
		{
			string sLine ;
			int nLen ;
			int nRet;
			while (	(sLine = nIn.ReadLine())!=null)
			{
				nLen = sLine.Length + 1;   //add for the CRLF
				sLine = sLine+ (Convert.ToChar(13) + Convert.ToChar(10) + Convert.ToChar(0));

				nRet = Decode (ExtTypes.Asc(sLine.Substring(0,1)));

				if ((nRet <= 0)||(sLine.Substring(0,1).ToCharArray(0,1)[0]== Convert.ToChar(13)))
				{
					return ;
				}
				int nExpected = ((nRet + 2)/3) * 4;

				if( nExpected >= (nLen - 1))
				{
					sLine = sLine.Substring(0, sLine.Length - 3) + Space(nExpected - nLen - 1);
				}
				 int bp = 1;
				while ( nRet > 0)
				{
					Decode_OutDec(sLine.Substring(bp),nOut,nRet);
					bp = bp+ 4;
					nRet = nRet-3;
				}
			}
		}

		private int Decode(int nChar )
		{
			return (nChar - 32) & 63;
		}
		public static string Space(int n) 
		{ 
			string s = ""; 
			for (int i = 0; i < n; i++) s += ""; 
			return s; 
		} 

		private void Decode_OutDec(string sStr, BinaryWriter nOut ,int  n )
			{
			int nOne ;
			int nTwo ; 
			int nThree ; 
			int nFour ;
			int n1,n2,n3 ;

			nOne  = ExtTypes.Asc(sStr);
			nTwo  = ExtTypes.Asc(sStr.Substring(1));
			nThree= ExtTypes.Asc(sStr.Substring(2));
			nFour = ExtTypes.Asc(sStr.Substring(3));

			//limit number to 255
			n1 = ((Decode(nOne)* 4 | Decode(nTwo)/ 16) & 255);
			n2 = ((Decode(nTwo)* 16  | Decode(nThree)/ 4) & 255);
			n3 = ((Decode(nThree)* 64 | Decode(nFour)) & 255);

			//			if (n >= 1 ) nOut.Write (Convert.ToChar(n1));
			//			if (n >= 2 ) nOut.Write (Convert.ToChar(n2));
			//			if (n >= 3 ) nOut.Write (Convert.ToChar(n3));

						if (n >= 1 ) nOut.Write (Convert.ToByte(n1));
						if (n >= 2 ) nOut.Write (Convert.ToByte(n2));
						if (n >= 3 ) nOut.Write (Convert.ToByte(n3));

		}
	}
}
