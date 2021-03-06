' NX 12.0.1.7
' Journal created by ievgenz on Tue Jul 10 10:32:09 2018 Eastern Daylight Time
'
Imports System
Imports NXOpen
Module NXJournal
    Sub Main(ByVal args() As String)

        Dim theSession As NXOpen.Session = NXOpen.Session.GetSession()
        Dim workPart As NXOpen.Part = theSession.Parts.Work
        Dim displayPart As NXOpen.Part = theSession.Parts.Display
        ' ----------------------------------------------
        '   Menu: Information->Part->Part History...
        ' ----------------------------------------------
        theSession.Information.DisplayPartHistory(workPart)
        ' ----------------------------------------------
        '   Menu: File->Properties
        ' ----------------------------------------------

        ' ----------------------------------------------
        '   Dialog Begin Customer Defaults
        ' ----------------------------------------------
        Dim changeList1 As NXOpen.Options.ChangeList = Nothing
        changeList1 = theSession.OptionsManager.NewOptionsChangeList(NXOpen.Options.LevelType.User, NXOpen.Options.LevelLockedByDefault.False)
        changeList1.SetValue("UG_AllowAttributeLockingInNXOpen", True)
        changeList1.Save()
        changeList1.Dispose()
        ' ----------------------------------------------
        '   Menu: Tools->Journal->Stop Recording
        ' ----------------------------------------------
        workPart.SetUserAttributeLock("AUTHOR", NXObject.AttributeType.Any, False)
        workPart.SetAttribute("AUTHOR", "IANP")'Environment.UserName.ToUpper)
        workPart.setAttribute("AUTHOR_DATE", DateTime.Now.ToString("MM/dd/yyyy"))
    End Sub
End Module

