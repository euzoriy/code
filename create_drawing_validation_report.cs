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
        try
        {
            String dirName = new FileInfo(displayPart.FullPath).Directory.FullName;
            String fileName = Path.GetFileNameWithoutExtension(displayPart.FullPath);
            String sourceFile = @"L:\IevgenZ\template\PM_validation_template.docx";
            String destFile = dirName + "\\" + fileName + ".docx";

            //MessageBox.Show(destFile + " - exists: " + System.IO.Directory.Exists(destFile).ToString());

            //Boolean DirectoryExists = new DirectoryInfo(destFile).Exists;
            Boolean DirectoryExists = System.IO.File.Exists(destFile);
            //MessageBox.Show(destFile + " - exists: " + DirectoryExists.ToString());
 
            if (!DirectoryExists)
            {
                System.IO.File.Copy(sourceFile, destFile, false); // Copy, do not overwrite
            }
            System.Diagnostics.Process.Start(String.Format(@"{0}", destFile)); // Open file
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.ToString(), "Error...", MessageBoxButtons.OK);
        }
    }
    public static int GetUnloadOption(string dummy) { return (int)NXOpen.Session.LibraryUnloadOption.Immediately; }
}


