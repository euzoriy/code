// NX 11.0.0.33
// Journal created by ievgenz on Mon Jan 23 09:54:26 2017 Eastern Standard Time
//
using System;
using System.IO;
using NXOpen;
using System.Diagnostics;

using System.Windows.Forms;

public class NXJournal
{
  public static void Main(string[] args)
  {
    NXOpen.Session theSession = NXOpen.Session.GetSession();
    NXOpen.Part workPart = theSession.Parts.Work;
    NXOpen.Part displayPart = theSession.Parts.Display;
    String currentPath = "";
	try{
		String dirName = new FileInfo(displayPart.FullPath).Directory.FullName;   
			Process myApp = new Process();
			myApp.StartInfo.FileName   = @"EXPLORER.exe";			
			//String fullPath = Path.GetFullPath(displayPart.FullPath);

			//MessageBox.Show(dirName, "File location", MessageBoxButtons.OK);
			//myApp.StartInfo.Arguments = String.Format("{0}" , displayPart.Name );
			myApp.StartInfo.Arguments = String.Format("{0}" , dirName );
			
			myApp.Start();
			}
	catch (Exception ex)
	{
			MessageBox.Show(ex.ToString(), "Error...", MessageBoxButtons.OK);		
	}
	
  }
  public static int GetUnloadOption(string dummy) { return (int)NXOpen.Session.LibraryUnloadOption.Immediately; }
}


