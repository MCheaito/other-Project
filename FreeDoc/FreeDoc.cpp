// FreeDoc.cpp : Defines the entry point for the DLL application.
//
#include <windows.h>
#include <tchar.h>
#include <stdio.h>



#include "stdafx.h"
#include "PDFLib/PDFLib.hpp"

#pragma comment(lib, "PDFLib/PDFLib.lib")


#define BUFSIZE MAX_PATH

BOOL APIENTRY DllMain( HANDLE hModule, 
                       DWORD  ul_reason_for_call, 
                       LPVOID lpReserved
					 )
{
    return TRUE;
}



int _stdcall  ConvertToPDF(LPCSTR ImageFileName,LPSTR pdfFileName, LPCSTR Author, LPCSTR Creator,LPCSTR Title,LPCSTR Subject) 
{
	 DWORD dwRetVal;
	 DWORD dwBufSize=BUFSIZE;
	 TCHAR lpPathBuffer[BUFSIZE];
	 UINT uRetVal;
	 TCHAR szTempName[BUFSIZE]; 
	 TCHAR pdfTemp[BUFSIZE];

     // Get the temp path.
	 dwRetVal = GetTempPath(dwBufSize,   // length of the buffer
                           lpPathBuffer); // buffer for path 

    if (dwRetVal > dwBufSize || (dwRetVal == 0))    {  return 1;    }

    // Create a temporary file. 
		uRetVal = GetTempFileName(lpPathBuffer, // directory for tmp files
                              TEXT(""),  // temp file name prefix 
                              0,            // create unique name 
								szTempName);  // buffer for name *
	   if (uRetVal == 0)
	    {
			return (2);
		}


 		strcpy(pdfTemp,szTempName);
 		strcpy(pdfFileName,pdfTemp);


		//open the PDF file
		PDFlib pdf;
		pdf.set_parameter("compatibility", "1.4");	//Compatible for Acrobat 5
		
		if (pdf.open_file(pdfFileName)==-1)
		{
			return 3;
		}
		
		pdf.set_info("Creator", Creator);
		pdf.set_info("Author", Author);
		pdf.set_info("Title",Title);
		pdf.set_info("Subject",Subject);
		

		int image = pdf.load_image("auto", ImageFileName, "");
			
			if (image != -1) 
			{
				pdf.begin_page_ext(10, 10, "");
				pdf.fit_image(image, 0.0, 0.0, "adjustpage");
				pdf.close_image(image);
				pdf.end_page_ext("");
			}

		pdf.close();

		//if (!MoveFile(pdfTemp,pdfFileName)) return 4;


		return 0;
}




      void __stdcall DisplayStringByVal(LPCSTR pszString)
      {
         //pszString is a pointer to a string
         MessageBox(NULL, pszString, "Display String ByVal",
                   MB_OK | MB_ICONINFORMATION);
      }

      void __stdcall DisplayStringByRef(LPCSTR* ppszString)
      {
         //ppszSting is a pointer to a pointer to a string
         MessageBox(NULL, *ppszString, "Display String ByRef",
                   MB_OK | MB_ICONINFORMATION);
      }

         void __stdcall FillString(LPSTR pszString, LONG cSize)
      {
         // Create a temp buffer with our string
         char buffer[] = "Hello from the C DLL!";

         // Copy our temp string to pszString
         // but check the size to make sure we have a buffer
         // big enough to hold the entire string.
         if (cSize > strlen(buffer))
            strcpy(pszString, buffer);
      }

      int __stdcall InStrRev(LPCSTR pszString, short iChar)
      {
         // This function is similar to Visual Basic's InStr function
         // except that it searches for the given ASCII character from
         // right to left, returning the character position of the
         // last occurrence (rather than the first) of the character
         // in the string.
         char* pszTmp;
         int nRet = 0;

         // Scan for iChar in pszString backwards
         pszTmp = strrchr(pszString, (int)iChar);
         if(pszTmp != NULL)
            nRet = pszTmp - pszString + 1;

         return nRet;
      }

