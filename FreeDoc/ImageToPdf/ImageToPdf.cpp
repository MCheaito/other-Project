// ImageToPdf.cpp : Defines the entry point for the DLL application.
//

#include "stdafx.h"
#include <windows.h>
#include <tchar.h>
#include <stdio.h>
#include "PDFLib/pdflib.hpp"


#pragma comment(lib, "PDFLib/PDFLib.lib")

#define BUFSIZE MAX_PATH

BOOL APIENTRY DllMain( HANDLE hModule, 
                       DWORD  ul_reason_for_call, 
                       LPVOID lpReserved
					 )
{
    return TRUE;
}


int _stdcall  ConvertToPDF(LPCSTR ImageFileName,		// Image filename
						   LPCSTR pdfFileName,			// Pdffilename
						   LPCSTR Author,				// Author name
						   LPCSTR Creator,				// Creator 
						   LPCSTR Title,				// Title
						   LPCSTR Subject)				// Subject
{
	 DWORD dwRetVal;
	 UINT uRetVal;
	 TCHAR lpPathBuffer[BUFSIZE];
	 TCHAR szTempName[BUFSIZE]; 
	 TCHAR pdfTemp[BUFSIZE];
 
     // Get the temp path.
	 dwRetVal = GetTempPath(BUFSIZE,   // length of the buffer
                           lpPathBuffer); // buffer for path 

    if (dwRetVal > BUFSIZE || (dwRetVal == 0))    
	{  
		return 1;    
	}

	// Create a temporary file. 
	uRetVal = GetTempFileName(	lpPathBuffer,	// directory for tmp files
								TEXT(""),		// temp file name prefix 
								0,				// create unique name 
								szTempName);	// buffer for name *
	if (uRetVal == 0)
	{
		return (2);
	}


 		strcpy(pdfTemp,szTempName);


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

		if (!MoveFile(pdfTemp,pdfFileName)) return 4;


		return 0;
}
