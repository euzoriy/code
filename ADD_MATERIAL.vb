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
		'lw.Open()		
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
			addMat(comp1.Name)
			lw.writeline("Material have been linked to parent component: " & comp1.Name)			
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
						Dim taggedObject1 As NXOpen.TaggedObject = Nothing
						taggedObject1 = titleBlockCellBuilderList1.FindItem(0)
						Dim titleBlockCellBuilder1 As NXOpen.Annotations.TitleBlockCellBuilder = CType(taggedObject1, NXOpen.Annotations.TitleBlockCellBuilder)
						Dim part1 As NXOpen.Part = CType(theSession.Parts.FindObject(modelName), NXOpen.Part) ''modelname
						
						Dim txtMaterial As String = "<X0.2@MATL_EXP>"
								Dim expression1 As NXOpen.Expression = Nothing
								Dim expression2 As NXOpen.Expression = Nothing
								
						try
								
							expression1 = CType(part1.Expressions.FindObject("MATL_EXP"), NXOpen.Expression)
							Dim rhsName1 As String = Nothing
							rhsName1 = workPart.Expressions.AskInterpartRhsName(expression1)
							'titleBlockCellBuilder1.EditableText = "<X0.2@MATL_EXP>"
							'txtMaterial = "<X0.2@MATL_EXP>"
						Catch ex1 as Exception
							Dim anotherWay As Boolean = FALSE
							Dim component1 As NXOpen.Assemblies.Component = CType(workPart.ComponentAssembly.RootComponent.FindObject("COMPONENT " & modelName & " 1"), NXOpen.Assemblies.Component)							
							If anotherWay = True Then
								Dim associativeText1 As NXOpen.Annotations.AssociativeText = Nothing
								associativeText1 = workPart.Annotations.CreateAssociativeText()
								txtMaterial = associativeText1.GetObjectAttributeText(component1, "NX_Material")	
								associativeText1.Dispose()
								editTitleBlockBuilder1.SetCellValueForLabel("MATERIAL", txtMaterial)'AddAttributeReference(component1, "NX_Material", False, 1, 1)
							Else

								expression1 = workPart.Expressions.GetAttributeExpression(component1, "NX_Material", NXOpen.NXObject.AttributeType.String, -1)
								try 
									expression2 = workPart.Expressions.FindObject("MATL_EXP")
									Catch ex As NXException
										'MsgBox(ex.Message)
										If ex.ErrorCode = 3520016 Then
											'no object found with this name
											'code to handle this error
											'lw.WriteLine("expression not found with name: " & expToFind)
											expression2 = workPart.Expressions.CreateExpression("String", "MATL_EXP=StringUpper(" & expression1.Name & " )")
										Else
											'code to handle other errors
											lw.WriteLine(ex.ErrorCode & ": " & ex.Message)
										End If
								End Try

								txtMaterial = "<X0.2@" & expression2.Name & ">"					

							End If

	
						
						end Try	
						'lw.WriteLine(txtMaterial)
						'titleBlockCellBuilder1.EditableText = txtMaterial
						editTitleBlockBuilder1.SetCellValueForLabel("MATERIAL", txtMaterial)
						
						theSession.SetUndoMarkVisibility(markId1, Nothing, NXOpen.Session.MarkVisibility.Invisible)
						Dim nXObject1 As NXOpen.NXObject = Nothing
						nXObject1 = editTitleBlockBuilder1.Commit()
						Dim objects1() As NXOpen.NXObject
						objects1 = editTitleBlockBuilder1.GetCommittedObjects()
						
						theSession.SetUndoMarkName(markId1, "Populate Title Block")
						editTitleBlockBuilder1.Destroy()
						theSession.SetUndoMarkVisibility(markId1, Nothing, NXOpen.Session.MarkVisibility.Visible)
						'msgBox ("Done")
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