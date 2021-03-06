' NX 11.0.0.33
' Journal created by ievgenz on Fri Dec 09 14:02:03 2016 Eastern Standard Time
'
Imports System
Imports NXOpen
Imports NXOpen.UF

Module NXJournal
''**************************************************************************************************
	Dim theSession As NXOpen.Session = NXOpen.Session.GetSession()
	Dim workPart As NXOpen.Part = theSession.Parts.Work
	Dim displayPart As NXOpen.Part = theSession.Parts.Display
	Dim lw As ListingWindow = theSession.ListingWindow	
	Dim ufs As NXOpen.UF.UFSession = NXOpen.UF.UFSession.GetUFSession()
''**************************************************************************************************
	Sub Main (ByVal args() As String) 
		Dim thisType as Integer = 0
		Dim thisSubType as Integer = 0
'' Type: 60
'' Type(text): NXOpen.Drawings.DetailView
'' Subtype: 6  
		Const whatType as Integer = 60
		Const whatSubType as Integer = 6		
''**************************************************************************************************
		lw.Open()
		lw.WriteLine("Started")
		Try
			For Each obj As NXOpen.View In theSession.Parts.Work.Views  
				ufs.Obj.AskTypeAndSubtype(obj.Tag, thisType, thisSubType) 
				lw.WriteLine(" Type(text): " & obj.GetType().ToString())				
				lw.WriteLine(" Type: " & thisType.ToString())
				lw.WriteLine(" Subtype: " & thisSubType.ToString())
				lw.WriteLine("")					
				If whatType = thisType Then  
					if  whatSubType = thisSubType then
						RedrawLeader(obj)
					End If  
				end if  
			Next	
		Catch ex As ApplicationException
			lw.WriteLine("## Error ###############################")
			lw.WriteLine(ex.ToString)
			lw.WriteLine("########################################")
		End Try	
		lw.close
''**************************************************************************************************
	end sub ''main
	Sub RedrawLeader (obj As NXOpen.View) 
		Dim markId1 As NXOpen.Session.UndoMarkId = Nothing
		markId1 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Start")
		Dim views1(0) As NXOpen.View
		Dim detailView1 As NXOpen.Drawings.DetailView = obj ''CType(workPart.DraftingViews.FindObject("DETAIL@39"), NXOpen.Drawings.DetailView)
		views1(0) = detailView1
		Dim editViewSettingsBuilder1 As NXOpen.Drawings.EditViewSettingsBuilder = Nothing
		editViewSettingsBuilder1 = workPart.SettingsManager.CreateDrawingEditViewSettingsBuilder(views1)
		Dim origin1 As NXOpen.Point3d = New NXOpen.Point3d(0.0, 0.0, 0.0)
		Dim normal1 As NXOpen.Vector3d = New NXOpen.Vector3d(0.0, 0.0, 1.0)
		Dim plane1 As NXOpen.Plane = Nothing
		plane1 = workPart.Planes.CreatePlane(origin1, normal1, NXOpen.SmartObject.UpdateOption.WithinModeling)
		Dim unit1 As NXOpen.Unit = CType(workPart.UnitCollection.FindObject("Inch"), NXOpen.Unit)
		Dim expression1 As NXOpen.Expression = Nothing
		expression1 = workPart.Expressions.CreateSystemExpressionWithUnits("0", unit1)
		Dim expression2 As NXOpen.Expression = Nothing
		expression2 = workPart.Expressions.CreateSystemExpressionWithUnits("0", unit1)
		theSession.SetUndoMarkName(markId1, "Settings Dialog")
		Dim editsettingsbuilders1(0) As NXOpen.Drafting.BaseEditSettingsBuilder
		editsettingsbuilders1(0) = editViewSettingsBuilder1
		workPart.SettingsManager.ProcessForMultipleObjectsSettings(editsettingsbuilders1)
		editViewSettingsBuilder1.ViewDetailLabel.LabelParentDisplay = NXOpen.Drawings.ViewDetailLabelBuilder.LabelParentDisplayTypes.Note
		Dim markId2 As NXOpen.Session.UndoMarkId = Nothing
		markId2 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Settings")
		Dim nXObject1 As NXOpen.NXObject = Nothing
		nXObject1 = editViewSettingsBuilder1.Commit()
		theSession.DeleteUndoMark(markId2, Nothing)
		theSession.SetUndoMarkName(markId1, "Settings")
		editViewSettingsBuilder1.Destroy()
		Try
		  ' Expression is still in use.
		  workPart.Expressions.Delete(expression2)
		Catch ex As NXException
		  ex.AssertErrorCode(1050029)
		End Try
		Try
		  ' Expression is still in use.
		  workPart.Expressions.Delete(expression1)
		Catch ex As NXException
		  ex.AssertErrorCode(1050029)
		End Try
		plane1.DestroyPlane()
		Dim markId3 As NXOpen.Session.UndoMarkId = Nothing
		markId3 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Start")
		Dim views2(0) As NXOpen.View
		Dim detailView2 As NXOpen.Drawings.DetailView = obj ''CType(nXObject1, NXOpen.Drawings.DetailView)
		views2(0) = detailView2
		Dim editViewSettingsBuilder2 As NXOpen.Drawings.EditViewSettingsBuilder = Nothing
		editViewSettingsBuilder2 = workPart.SettingsManager.CreateDrawingEditViewSettingsBuilder(views2)
		Dim origin2 As NXOpen.Point3d = New NXOpen.Point3d(0.0, 0.0, 0.0)
		Dim normal2 As NXOpen.Vector3d = New NXOpen.Vector3d(0.0, 0.0, 1.0)
		Dim plane2 As NXOpen.Plane = Nothing
		plane2 = workPart.Planes.CreatePlane(origin2, normal2, NXOpen.SmartObject.UpdateOption.WithinModeling)
		Dim expression3 As NXOpen.Expression = Nothing
		expression3 = workPart.Expressions.CreateSystemExpressionWithUnits("0", unit1)
		Dim expression4 As NXOpen.Expression = Nothing
		expression4 = workPart.Expressions.CreateSystemExpressionWithUnits("0", unit1)
		theSession.SetUndoMarkName(markId3, "Settings Dialog")
		Dim editsettingsbuilders2(0) As NXOpen.Drafting.BaseEditSettingsBuilder
		editsettingsbuilders2(0) = editViewSettingsBuilder2
		workPart.SettingsManager.ProcessForMultipleObjectsSettings(editsettingsbuilders2)
		' ----------------------------------------------
		'   Dialog Begin Settings
		' ----------------------------------------------
		Dim markId4 As NXOpen.Session.UndoMarkId = Nothing
		markId4 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Settings")
		theSession.DeleteUndoMark(markId4, Nothing)
		Dim markId5 As NXOpen.Session.UndoMarkId = Nothing
		markId5 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Settings")
		Dim nXObject2 As NXOpen.NXObject = Nothing
		nXObject2 = editViewSettingsBuilder2.Commit()
		theSession.DeleteUndoMark(markId5, Nothing)
		theSession.SetUndoMarkName(markId3, "Settings")
		editViewSettingsBuilder2.Destroy()
		Try
		  ' Expression is still in use.
		  workPart.Expressions.Delete(expression4)
		Catch ex As NXException
		  ex.AssertErrorCode(1050029)
		End Try
		Try
		  ' Expression is still in use.
		  workPart.Expressions.Delete(expression3)
		Catch ex As NXException
		  ex.AssertErrorCode(1050029)
		End Try
		plane2.DestroyPlane()
		' ----------------------------------------------
		'   Menu: Edit->View->Settings...
		' ----------------------------------------------
		Dim markId6 As NXOpen.Session.UndoMarkId = Nothing
		markId6 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Start")
		Dim views3(0) As NXOpen.View
		Dim detailView3 As NXOpen.Drawings.DetailView = obj ''CType(nXObject2, NXOpen.Drawings.DetailView)
		views3(0) = detailView3
		Dim editViewSettingsBuilder3 As NXOpen.Drawings.EditViewSettingsBuilder = Nothing
		editViewSettingsBuilder3 = workPart.SettingsManager.CreateDrawingEditViewSettingsBuilder(views3)
		Dim origin3 As NXOpen.Point3d = New NXOpen.Point3d(0.0, 0.0, 0.0)
		Dim normal3 As NXOpen.Vector3d = New NXOpen.Vector3d(0.0, 0.0, 1.0)
		Dim plane3 As NXOpen.Plane = Nothing
		plane3 = workPart.Planes.CreatePlane(origin3, normal3, NXOpen.SmartObject.UpdateOption.WithinModeling)
		Dim expression5 As NXOpen.Expression = Nothing
		expression5 = workPart.Expressions.CreateSystemExpressionWithUnits("0", unit1)
		Dim expression6 As NXOpen.Expression = Nothing
		expression6 = workPart.Expressions.CreateSystemExpressionWithUnits("0", unit1)
		theSession.SetUndoMarkName(markId6, "Settings Dialog")
		Dim editsettingsbuilders3(0) As NXOpen.Drafting.BaseEditSettingsBuilder
		editsettingsbuilders3(0) = editViewSettingsBuilder3
		workPart.SettingsManager.ProcessForMultipleObjectsSettings(editsettingsbuilders3)
		editViewSettingsBuilder3.ViewDetailLabel.LabelParentDisplay = NXOpen.Drawings.ViewDetailLabelBuilder.LabelParentDisplayTypes.Label
		Dim markId7 As NXOpen.Session.UndoMarkId = Nothing
		markId7 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Settings")
		theSession.DeleteUndoMark(markId7, Nothing)
		Dim markId8 As NXOpen.Session.UndoMarkId = Nothing
		markId8 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Settings")
		Dim nXObject3 As NXOpen.NXObject = Nothing
		nXObject3 = editViewSettingsBuilder3.Commit()
		theSession.DeleteUndoMark(markId8, Nothing)
		theSession.SetUndoMarkName(markId6, "Settings")
		editViewSettingsBuilder3.Destroy()
		Try
		  ' Expression is still in use.
		  workPart.Expressions.Delete(expression6)
		Catch ex As NXException
		  ex.AssertErrorCode(1050029)
		End Try
		Try
		  ' Expression is still in use.
		  workPart.Expressions.Delete(expression5)
		Catch ex As NXException
		  ex.AssertErrorCode(1050029)
		End Try
		plane3.DestroyPlane()
		lw.writeLine("View leader updated")
	End Sub
''**************************************************************************************************	
End Module

