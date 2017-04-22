using System;
using System.IO;
using Dnd.Cda.Allies.Common.Compression.ZipLib.Zip;
using Dnd.Cda.Allies.Common.Compression.ZipLib.Checksums;

namespace PackagingDllRes
{
	/// <summary>
	/// Summary description for ZipManager.
	/// </summary>
	public class ZipManager
	{
		public ZipManager()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public static void CompressFile(String origFilePathname, String destFilePath_in, bool chkDontStripExtension)
		{
            string filename = Path.GetFileName(origFilePathname);
            string destFilePathname = destFilePath_in + "\\" + ((chkDontStripExtension) ? filename : (filename.Substring(0, filename.Length - 4))) + ".zip";
				
			Crc32 crc = new Crc32();
            ZipOutputStream s = new ZipOutputStream(File.Create(destFilePathname));
		
			s.SetLevel(9); // 0 - store only to 9 - means best compression

			FileStream fs = File.OpenRead(origFilePathname);
		
			byte[] buffer = new byte[fs.Length];
			fs.Read(buffer, 0, buffer.Length);

			string fileEntry = origFilePathname.Substring(origFilePathname.LastIndexOf('\\') +1);

			ZipEntry entry = new ZipEntry(fileEntry);		

			
			entry.DateTime = DateTime.Now;
		
			// set Size and the crc, because the information
			// about the size and crc should be stored in the header
			// if it is not set it is automatically written in the footer.
			// (in this case size == crc == -1 in the header)
			// Some ZIP programs have problems with zip files that don't store
			// the size and crc in the header.
			entry.Size = fs.Length;
			fs.Close();
		
			crc.Reset();
			crc.Update(buffer);
			entry.Crc  = crc.Value;
			s.PutNextEntry(entry);
			s.Write(buffer, 0, buffer.Length);
		
			s.Finish();
			s.Close();
		}
	}
}
