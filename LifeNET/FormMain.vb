'http://www.bitstorm.org/gameoflife/lexicon/

Imports System.Threading

Public Class FormMain
    Private mouseIsDown As Boolean
    Private mouseLocation As Point
    Private mousePen As Boolean
    Private ctrlIsDown As Boolean
    Private worldOffsetFrom As Point
    Private zoomLevel As Single = 1.0

    Private shapeName As String

    Private topPos As Integer

    Private cancelLoadLibrary As Boolean
    Private isLoadingLibrary As Boolean

    Private infoFont As Font

    Private lifeEngine As Life

    Private Sub FormMain_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        cancelLoadLibrary = True
        While isLoadingLibrary
            Application.DoEvents()
        End While
        lifeEngine.Stop()
    End Sub

    Private Sub FormMain_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
        Me.SetStyle(ControlStyles.ResizeRedraw, True)
        Me.SetStyle(ControlStyles.UserPaint, True)
        Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)

        infoFont = New Font("Consolas", 11, FontStyle.Bold)

        topPos = MenuMainFile.Height + 6
        worldOffsetFrom = New Point(0, 0)

        lifeEngine = New Life(New Size(100, 100), 12)
        lifeEngine.WorldOffset = New Point(0, topPos)
        AddHandler lifeEngine.Update, Sub() Me.Invalidate()

        Dim loadShapesThread As Thread = New Thread(AddressOf LoadShapesLibrary)
        loadShapesThread.Start()
    End Sub

    Private Sub FormMain_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        ctrlIsDown = e.Control

        Select Case e.KeyCode
            Case Keys.Space
                If lifeEngine.IsRunning Then
                    lifeEngine.IsRunning = False
                Else
                    lifeEngine.IsRunning = True
                    lifeEngine.StepForward()
                End If
            Case Keys.Left
                If Not lifeEngine.IsRunning Then lifeEngine.StepBack()
            Case Keys.Right
                If Not lifeEngine.IsRunning Then lifeEngine.StepForward()
            Case Keys.Tab
                If lifeEngine.IsInserting Then
                    lifeEngine.RotateShape()
                    If Not lifeEngine.IsRunning Then Me.Invalidate()
                End If
            Case Keys.M
                If lifeEngine.IsInserting Then
                    Dim values = [Enum].GetValues(GetType(Life.PaintCellModes))
                    Dim mode = CInt(lifeEngine.PaintCellMode)

                    mode += 1
                    mode = mode Mod values.Length

                    lifeEngine.PaintCellMode = CType(values.GetValue(mode), Life.PaintCellModes)
                    'lifeEngine.StartInserting(lifeEngine.ScreenToWorld(mouseLocation.X + 1, mouseLocation.Y + 1))

                    If Not lifeEngine.IsRunning Then Me.Invalidate()
                End If
            Case Keys.Escape
                If lifeEngine.IsInserting Then
                    lifeEngine.IsInserting = False
                    If Not lifeEngine.IsRunning Then Me.Invalidate()
                End If
            Case Keys.Add
                If ctrlIsDown Then
                    zoomLevel += 0.1!
                    If Not lifeEngine.IsRunning Then Me.Invalidate()
                End If
            Case Keys.Subtract
                If ctrlIsDown Then
                    zoomLevel -= 0.1!
                    If Not lifeEngine.IsRunning Then Me.Invalidate()
                End If
            Case Keys.NumPad0
                If ctrlIsDown Then
                    zoomLevel = 1.0!
                    If Not lifeEngine.IsRunning Then Me.Invalidate()
                End If
        End Select
    End Sub

    Private Sub FormMain_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp
        ctrlIsDown = e.Control
    End Sub

    Private Sub FormMain_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDown
        Dim p As Point = lifeEngine.ScreenToWorld(e.X, e.Y)
        If p.X < 0 OrElse p.Y < 0 Then Exit Sub

        Select Case e.Button
            Case MouseButtons.Left
                If lifeEngine.IsInserting Then
                    lifeEngine.InsertShape(p.X + 1, p.Y + 1)
                Else
                    mouseIsDown = True
                    lifeEngine.ToggleCell(p)
                    mousePen = lifeEngine.World(p.X)(p.Y)
                End If

                If Not lifeEngine.IsRunning Then Me.Invalidate()
            Case MouseButtons.Middle
                mouseIsDown = True
                Me.Cursor = Cursors.NoMove2D

                worldOffsetFrom.X = e.X - lifeEngine.WorldOffset.X
                worldOffsetFrom.Y = e.Y - lifeEngine.WorldOffset.Y
        End Select
    End Sub

    Private Sub FormMain_MouseMove(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseMove
        If e.X < lifeEngine.WorldOffset.X OrElse e.Y < lifeEngine.WorldOffset.Y Then Exit Sub

        Select Case e.Button
            Case Windows.Forms.MouseButtons.Middle
                'lifeEngine.WorldOffset = New Point(Math.Max(e.X - worldOffsetFrom.X, 0), Math.Max(e.Y - worldOffsetFrom.Y, topPost))
                lifeEngine.WorldOffset = New Point(e.X - worldOffsetFrom.X, e.Y - worldOffsetFrom.Y)

                If Not lifeEngine.IsRunning Then Me.Invalidate()
            Case Else
                Dim mp As Point = lifeEngine.ScreenToWorld(e.X, e.Y)
                mouseLocation = lifeEngine.WorldToScreen(mp.X, mp.Y)

                If mouseIsDown Then
                    lifeEngine.SetCell(mp, mousePen)
                    If Not lifeEngine.IsRunning Then Me.Invalidate()
                ElseIf lifeEngine.IsInserting Then
                    If lifeEngine.PaintCellMode <> Life.PaintCellModes.Copy Then
                        lifeEngine.StartInserting(New Point(mp.X + 1, mp.Y + 1))
                    Else
                        If Not lifeEngine.IsRunning Then Me.Invalidate()
                    End If
                End If
        End Select
    End Sub

    Private Sub FormMain_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseUp
        mouseIsDown = False
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub FormMain_MouseWheel(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseWheel
        If ctrlIsDown Then
            If e.Delta > 0 Then
                lifeEngine.CellSize += 1
            Else
                lifeEngine.CellSize = Math.Max(lifeEngine.CellSize - 1, 1)
            End If
        End If
    End Sub

    Private Sub FormMain_Paint(sender As Object, e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        Dim g As Graphics = e.Graphics

        g.ScaleTransform(zoomLevel, zoomLevel)

        ' Slooooooowwwww!!!
        g.DrawImageUnscaled(lifeEngine.MainBitmap, lifeEngine.WorldOffset.X, lifeEngine.WorldOffset.Y)

        If lifeEngine.IsInserting Then
            g.DrawImageUnscaled(lifeEngine.DragBitmap, mouseLocation)

            Dim p = mouseLocation
            p.Y += lifeEngine.DragBitmap.Height

            Dim infoText = shapeName + vbCrLf +
                           "[TAB]:  Rotate" + vbCrLf +
                           "[M]ode: " + lifeEngine.PaintCellMode.ToString() + vbCrLf +
                           "[ESC]:  Cancel"

            g.FillRectangle(Brushes.White, New Rectangle(p, g.MeasureString(infoText, infoFont).ToSize()))
            g.DrawString(infoText, infoFont, Brushes.Black, p)
        End If
    End Sub

    Private Sub MenuMainFileExit_Click(sender As System.Object, e As System.EventArgs) Handles MenuMainFileExit.Click
        Me.Close()
    End Sub

    Private Sub MenuMainFileOpen_Click(sender As System.Object, e As System.EventArgs) Handles MenuMainFileOpen.Click
        Using dlg As OpenFileDialog = New OpenFileDialog()
            dlg.AutoUpgradeEnabled = True
            dlg.CheckFileExists = True
            dlg.Filter = "Cell Files (*.cells)|*.cells|All Files (*.*)|*.*"
            dlg.Multiselect = False
            If dlg.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                If lifeEngine.IsRunning Then lifeEngine.IsRunning = False

                Try
                    lifeEngine.Shape = lifeEngine.LoadShapeFromFile(dlg.FileName)
                    lifeEngine.StartInserting()
                Catch ex As Exception
                    MsgBox(ex.Message, MsgBoxStyle.Information Or MsgBoxStyle.OkOnly, "Error Opening File")
                End Try
            End If
        End Using
    End Sub

    Private Sub MenuMainFileNew_Click(sender As System.Object, e As System.EventArgs) Handles MenuMainFileNew.Click
        lifeEngine.Reset()
    End Sub

    Private Sub LoadShapesLibrary()
        isLoadingLibrary = True

        Dim data As String
        Dim shapes() As IO.FileInfo = New IO.DirectoryInfo(GetShapesDirectory()).GetFiles("*.cells")

        For Each shapeFile As IO.FileInfo In shapes
            If cancelLoadLibrary Then Exit For

            data = IO.File.ReadAllText(shapeFile.FullName)
            If data.StartsWith("!") AndAlso data.Contains("O") AndAlso data.Contains("!Name:") Then
                Me.Invoke(New MethodInvoker(Sub() AddMenuItem(shapeFile.FullName, data)))
            End If
        Next

        isLoadingLibrary = False
    End Sub

    Private Sub AddMenuItem(shapeFile As String, data As String)
        Dim mSubMenu As ToolStripMenuItem = Nothing
        Dim subMenuExists As Boolean = False
        Dim name As String

        name = data.Split(New String() {"!Name:"}, StringSplitOptions.None)(1)
        name = name.Substring(0, name.IndexOf("!") - 1).TrimEnd(New Char() {CChar(vbLf), CChar(vbCr)}).Replace("_", " ")
        name = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(name).Trim()

        subMenuExists = False
        For Each mi As ToolStripMenuItem In MenuMainShapes.DropDownItems
            If mi.Text = name.Substring(0, 1) Then
                subMenuExists = True
                mSubMenu = mi
                Exit For
            End If
        Next

        If Not subMenuExists Then
            mSubMenu = CType(MenuMainShapes.DropDownItems.Add(name.Substring(0, 1)), ToolStripMenuItem)
            mSubMenu.Text = name.Substring(0, 1)
        End If

        mSubMenu.Enabled = False
        'With mSubMenu.DropDownItems.Add(String.Format("{0} ({1})", name, shapeFile.Name.Split("."c)(0)), Nothing, AddressOf LoadShapeFromMenu)
        With mSubMenu.DropDownItems.Add(name, Nothing, AddressOf InsertShapeFromMenu)
            .Tag = shapeFile

            .ImageScaling = ToolStripItemImageScaling.None
            .Image = lifeEngine.ShapeToBitmap(lifeEngine.LoadShapeFromFile(shapeFile), 2, Color.White)
        End With
        mSubMenu.Enabled = True
    End Sub

    Private Sub InsertShapeFromMenu(sender As Object, e As EventArgs)
        Dim mi As ToolStripMenuItem = CType(sender, ToolStripMenuItem)
        Dim file As String = CType(mi.Tag, String)

        shapeName = mi.Text
        lifeEngine.Shape = lifeEngine.LoadShapeFromFile(file)
        lifeEngine.StartInserting()
    End Sub

    Private Sub MenuMainFileSave_Click(sender As System.Object, e As System.EventArgs) Handles MenuMainFileSave.Click
        Dim wasRunning = lifeEngine.IsRunning
        If wasRunning Then lifeEngine.IsRunning = False

        Dim isValid As Boolean
        Dim shape()() = lifeEngine.ExtractShape()

        For c = 0 To shape.Length - 1
            For r = 0 To shape(c).Length - 1
                If shape(c)(r) Then
                    isValid = True
                    Exit For
                End If
            Next
            If isValid Then Exit For
        Next

        If isValid Then
            Try
                Using dlg As New FormSaveShape()
                    dlg.PictureBoxShapePreview.Image = lifeEngine.ShapeToBitmap(shape, lifeEngine.CellSize, Color.White)

                    If dlg.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                        Dim name As String = dlg.TextBoxShapeName.Text
                        Dim illegalChars() As Char = New Char() {"<"c, ">"c, ":"c, """"c, "/"c, "\"c, "|"c, "?"c, "*"c}
                        Dim fileName As String = name

                        For Each ic In illegalChars
                            fileName = fileName.Replace(ic, "_")
                        Next
                        While fileName.Contains("__")
                            fileName = fileName.Replace("__", "_")
                        End While
                        fileName = fileName.TrimStart("_"c).TrimEnd("_"c).Trim()
                        fileName = IO.Path.Combine(GetShapesDirectory(), fileName + ".cells")

                        lifeEngine.SaveShape(shape, name, fileName)

                        AddMenuItem(fileName, IO.File.ReadAllText(fileName))
                    End If
                End Using
            Catch ex As Exception
                MsgBox("Something went wrong while trying to save the shape:" + vbCrLf + vbCrLf + ex.Message, MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Invalid Shape")
            End Try
        Else
            MsgBox("LifeNET's world does not appear to contain any shapes...", MsgBoxStyle.Information Or MsgBoxStyle.OkOnly, "Invalid Shape")
        End If

        If wasRunning Then
            lifeEngine.IsRunning = True
            lifeEngine.StepForward()
        End If
    End Sub

    Private Function GetShapesDirectory() As String
        Return IO.Path.Combine(My.Application.Info.DirectoryPath, "Shapes")
    End Function
End Class
