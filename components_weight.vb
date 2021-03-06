''Vit,
''If you want to do this programmatically you need to use the command "ufs.weight.askprops(cur_part_tag,units,weight)"
''If you look in the docs under uf_weight it's there.
''here's an example that will return the current weight of the work part.
''The weight has to be updated either manually or set to update on save under file->proporties under the weight tab.

''==================================================================

option strict off
Imports System
Imports NXOpen
Imports NXOpen.Assemblies
imports nxopen.uf
imports system.windows.forms

Module NXJournal

    Dim s As Session = Session.GetSession()
	Dim workPart As Part = s.Parts.Work
    Dim dispPart As Part = s.Parts.Display
	Dim ufs as UFSession = UFSession.GetUFSession()
	Dim theUI As UI = UI.GetUI()
	dim cur_part_tag as tag
	dim weight as ufweight.properties
	dim units as ufweight.unitstype
	Dim lw As ListingWindow = s.ListingWindow
	
	sub Main

			lw.Open()
			Try
				Dim c As ComponentAssembly = dispPart.ComponentAssembly
				If Not IsNothing(c.RootComponent) Then
					getWeight(c.RootComponent)
				End If
			Catch e As Exception
				s.ListingWindow.WriteLine("Failed: " & e.ToString)
			End Try
			lw.Close()

	end sub

    Sub getWeight(ByVal comp As Component) 
		
 
        For Each child As Component In comp.GetChildren()
            '*** insert code to process component or subassembly
            ''If LoadComponent(child) Then
                lw.WriteLine("component name: " & child.Name)
                REM lw.WriteLine("file name: " & child.Prototype.OwningPart.Leaf)
                REM lw.WriteLine("")
				
				' Add a row for each point
				''ufs.Tabnot.CreateRow(1, row)
				''ufs.Tabnot.AddRow(tabnote, row, UFConstants.UF_TABNOT_APPEND)
				''ufs.Tabnot.AskCellAtRowCol(row, columns(0), cell)
				''ufs.Tabnot.SetCellText(cell, child.Name.ToString())
				ufs.weight.askprops(child.tag,units,weight)
				msgbox(weight.mass)
            ''Else
                'component could not be loaded
            ''End If
            '*** end of code to process component or subassembly
            getWeight(child)
        Next		
		
		
    End Sub

	Sub notRun
		Dim comp As ComponentAssembly = s.parts.work.ComponentAssembly
		dim c as component = comp.rootcomponent
		dim children as component() = c.getchildren
		dim child as component
		For Each child In children
			dim prototype as part
			if typeof child.prototype is part then	
				''cur_part_tag = child.prototype.tag
				''prototype = CTYPE(child.prototype,part)
				'messagebox.show(prototype.leaf)
				''ufs.weight.askprops(cur_part_tag,units,weight)
				''msgbox(weight.mass)
			end if
		next	
	End sub
	
	
	
End Module



''crap!!!!!!
