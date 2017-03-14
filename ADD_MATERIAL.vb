' Created by I. Zorii 03/10/2017 to add parent part material to the title block
' NX 11.0.0.33
' Journal created by ievgenz on Fri Mar 10 14:36:43 2017 Eastern Standard Time
'
Imports System
Imports NXOpen
Imports NXOpen.UF
Imports NXOpen.Assemblies
Module NXJournal
	Dim theSession As NXOpen.Session = NXOpen.Session.GetSession()
	Dim ufs As NXOpen.UF.UFSession = NXOpen.UF.UFSession.GetUFSession()
	Dim workPart As NXOpen.Part = theSession.Parts.Work
	Dim displayPart As NXOpen.Part = theSession.Parts.Display
	Dim lw As ListingWindow = theSession.ListingWindow
	Sub Main (ByVal args() As String) 	
		Dim markId1 As NXOpen.Session.UndoMarkId = Nothing
		markId1 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Start")		
		lw.Open()		
		if false then
			For Each comp As Component In theSession.Parts.Work.ComponentAssembly.RootComponent.GetChildren 
				lw.writeline("component name: " & comp.Name)
				''comp.SetName(comp.Name & "_rev1")
				lw.writeline("new component name: " & comp.Name)
				lw.writeline("ref set name: " & comp.referenceset)
				lw.writeline("component part name: " & comp.Prototype.OwningPart.fullpath)
				lw.writeline("component file name: " & IO.Path.GetFileName(comp.Prototype.OwningPart.fullpath))			
				lw.writeline("") 
			Next
		end if
		try
			Dim comp1 as Component = theSession.Parts.Work.ComponentAssembly.RootComponent.GetChildren(0)
			lw.writeline("Parent component name: " & comp1.Name)
			addMat(comp1.Name)
		catch ex as Exception
			lw.writeline("Unable to add material info.")
		end try
	End Sub
''**************************************************************************************************
	Sub addMat (modelName As String)
	Const attributeName As String = "MATERIAL"
    Dim value As String = ""
''**************************************************************************************************
Dim thisType as Integer = 0
Dim thisSubType as Integer = 0
''Type(text): NXOpen.Annotations.TitleBlock   
Const whatType as Integer = 25
Const whatSubType as Integer = 42
''**************************************************************************************************
		Try
			For Each obj As DisplayableObject In theSession.Parts.Work.Views.WorkView.AskVisibleObjects  
				ufs.Obj.AskTypeAndSubtype(obj.Tag, thisType, thisSubType)  
				If whatType = thisType Then  
					if  whatSubType = thisSubType then
						Dim markId1 As NXOpen.Session.UndoMarkId = Nothing
						markId1 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Start")
						Dim titleblocks1(0) As NXOpen.Annotations.TitleBlock
						titleblocks1(0) = obj
						Dim editTitleBlockBuilder1 As NXOpen.Annotations.EditTitleBlockBuilder = Nothing
						editTitleBlockBuilder1 = workPart.DraftingManager.TitleBlocks.CreateEditTitleBlockBuilder(titleblocks1)
						Dim titleBlockCellBuilderList1 As NXOpen.Annotations.TitleBlockCellBuilderList = Nothing
						titleBlockCellBuilderList1 = editTitleBlockBuilder1.Cells
						theSession.SetUndoMarkName(markId1, "Populate Title Block Dialog")
						Dim taggedObject1 As NXOpen.TaggedObject = Nothing
						taggedObject1 = titleBlockCellBuilderList1.FindItem(0)
						Dim titleBlockCellBuilder1 As NXOpen.Annotations.TitleBlockCellBuilder = CType(taggedObject1, NXOpen.Annotations.TitleBlockCellBuilder)
						Dim markId5 As NXOpen.Session.UndoMarkId = Nothing
						markId5 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Populate Title Block")
						Dim part1 As NXOpen.Part = CType(theSession.Parts.FindObject(modelName), NXOpen.Part) ''modelname
						Dim expression1 As NXOpen.Expression = CType(part1.Expressions.FindObject("MATL_EXP"), NXOpen.Expression)
						Dim rhsName1 As String = Nothing
						rhsName1 = workPart.Expressions.AskInterpartRhsName(expression1)
						titleBlockCellBuilder1.EditableText = "<X0.2@MATL_EXP>"
						editTitleBlockBuilder1.SetCellValueForLabel("MATERIAL", "<X0.2@MATL_EXP>")
						theSession.SetUndoMarkName(markId5, "Populate Title Block")
						theSession.SetUndoMarkVisibility(markId5, Nothing, NXOpen.Session.MarkVisibility.Visible)
						theSession.SetUndoMarkVisibility(markId1, Nothing, NXOpen.Session.MarkVisibility.Invisible)
						Dim nXObject1 As NXOpen.NXObject = Nothing
						nXObject1 = editTitleBlockBuilder1.Commit()
						Dim objects1() As NXOpen.NXObject
						objects1 = editTitleBlockBuilder1.GetCommittedObjects()
						theSession.SetUndoMarkName(markId1, "Populate Title Block")
						editTitleBlockBuilder1.Destroy()
						theSession.SetUndoMarkVisibility(markId1, Nothing, NXOpen.Session.MarkVisibility.Visible)
						theSession.DeleteUndoMark(markId5, Nothing)
					End If  
				end if  
			Next	
		Catch ex As ApplicationException
			lw.WriteLine("## Error ###############################")
			lw.WriteLine(ex.ToString)
			lw.WriteLine("########################################")
							
			lw.WriteLine("Check if material assigned to the parent component")	
		End Try	
	End Sub
''**************************************************************************************************
End Module