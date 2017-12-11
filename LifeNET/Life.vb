Imports System.Threading

' http://www.bitstorm.org/gameoflife/lexicon/

Public Class Life
    Public Enum PaintCellModes
        Copy = 0
        [OR] = 1
        [AND] = 2
        [XOR] = 3
        NAND = 4
        [NOT] = 5
    End Enum

    Private mWorldSize As Size
    Private mWorld()() As Boolean
    Private mCellSize As Integer
    Private mCell As Rectangle

    Private mCellBackColor As Color = Color.White
    Private mCellForeColor As Color = Color.Blue
    Private mGridColor As Color = Color.Gray

    Private mIsInserting As Boolean

    Private mPaintCellMode As PaintCellModes = PaintCellModes.Copy

    Private runThread As Thread
    Private runWaitEvent As AutoResetEvent
    Private mIsRunning As Boolean
    Private cancelThread As Boolean

    Private mainBPM As Bitmap
    Private bpmG As Graphics
    Private dragBPM As Bitmap

    Private mWorldOffset As Point

    Private mShape()() As Boolean

    Private stepBackup As List(Of Dictionary(Of Point, Boolean))

    Public Event Update()

    Public Sub New(worldSize As Size, cellSize As Integer)
        mCellSize = cellSize
        mWorldSize = worldSize
        mWorldOffset = New Point(0, 0)

        CreateWorld(True)

        runWaitEvent = New AutoResetEvent(False)
        runThread = New Thread(AddressOf ProcessSub)
        runThread.Start()
    End Sub

    Public Sub Reset()
        mIsRunning = False
        CreateWorld(True)
    End Sub

    Public Sub [Stop]()
        mIsRunning = False
        cancelThread = True
        runWaitEvent.Set()

        bpmG.Dispose()
        mainBPM.Dispose()
    End Sub

    Public Property WorldOffset() As Point
        Get
            Return mWorldOffset
        End Get
        Set(ByVal value As Point)
            mWorldOffset = value
        End Set
    End Property

    Public ReadOnly Property DragBitmap() As Bitmap
        Get
            Return dragBPM
        End Get
    End Property

    Public ReadOnly Property MainBitmap() As Bitmap
        Get
            Return mainBPM
        End Get
    End Property

    Public ReadOnly Property Iteration() As Integer
        Get
            Return stepBackup.Count
        End Get
    End Property

    Public Property PaintCellMode As PaintCellModes
        Get
            Return mPaintCellMode
        End Get
        Set(value As PaintCellModes)
            mPaintCellMode = value
        End Set
    End Property

    Public Property Shape() As Boolean()()
        Get
            Return mShape
        End Get
        Set(ByVal value As Boolean()())
            mShape = value
        End Set
    End Property

    Public Property IsInserting() As Boolean
        Get
            Return mIsInserting
        End Get
        Set(ByVal value As Boolean)
            mIsInserting = value
        End Set
    End Property

    Public Property CellSize() As Integer
        Get
            Return mCellSize
        End Get
        Set(ByVal value As Integer)
            mCellSize = value

            Dim wasRunning As Boolean = mIsRunning
            mIsInserting = False

            CreateWorld(False)

            mIsRunning = wasRunning
        End Set
    End Property

    Public Property IsRunning() As Boolean
        Get
            Return mIsRunning
        End Get
        Set(ByVal value As Boolean)
            mIsRunning = value
        End Set
    End Property

    Public ReadOnly Property World() As Boolean()()
        Get
            Return mWorld
        End Get
    End Property

    Private Sub CreateWorld(fullInit As Boolean)
        If fullInit Then stepBackup = New List(Of Dictionary(Of Point, Boolean))

        If mainBPM IsNot Nothing Then
            bpmG.Dispose()
            mainBPM.Dispose()
        End If
        mainBPM = New Bitmap(mWorldSize.Width * mCellSize + 1, mWorldSize.Height * mCellSize + 1, Imaging.PixelFormat.Format24bppRgb)
        bpmG = Graphics.FromImage(mainBPM)

        mCell = New Rectangle(0, 0, mCellSize, mCellSize)

        If fullInit Then
            InitArray(mWorld, mWorldSize.Width, mWorldSize.Height, True)
        Else
            RepaintWorld()
        End If

        If Not mIsRunning Then RaiseEvent Update()
    End Sub

    Private Sub ResetStats()

    End Sub

    Public Sub ToggleCell(p As Point)
        ToggleCell(p.X, p.Y)
    End Sub

    Public Sub ToggleCell(c As Integer, r As Integer)
        SetCell(c, r, Not mWorld(c)(r))
    End Sub

    Public Sub StepForward()
        runWaitEvent.Set()
    End Sub

    Public Sub StepBack()
        If stepBackup.Count = 0 Then Exit Sub

        For Each changedCells As KeyValuePair(Of Point, Boolean) In stepBackup.Last()
            SetCell(changedCells.Key, Not changedCells.Value)
        Next

        stepBackup.RemoveAt(stepBackup.Count - 1)

        If Not mIsRunning Then RaiseEvent Update()
    End Sub

    Public Sub SetCell(p As Point, value As Boolean, Optional ignoreEquals As Boolean = True)
        SetCell(p.X, p.Y, value, ignoreEquals)
    End Sub

    Public Sub SetCell(c As Integer, r As Integer, value As Boolean, Optional ignoreEquals As Boolean = True)
        If ignoreEquals AndAlso mWorld(c)(r) = value Then Exit Sub
        mWorld(c)(r) = value

        PaintCell(c, r, value)
    End Sub

    Private Sub PaintCell(c As Integer, r As Integer, value As Boolean)
        Try
            mCell.Location = New Point(c * mCellSize, r * mCellSize)
            Using b As New SolidBrush(If(value, mCellForeColor, mCellBackColor))
                bpmG.FillRectangle(b, mCell)
            End Using

            If mCellSize > 6 Then bpmG.DrawRectangle(Pens.Gray, mCell)
        Catch
        End Try
    End Sub

    Public Function ScreenToWorld(x As Integer, y As Integer) As Point
        x = CInt(Math.Min(Math.Floor((x - mWorldOffset.X) / mCellSize), mWorldSize.Width - 1))
        y = CInt(Math.Min(Math.Floor((y - mWorldOffset.Y) / mCellSize), mWorldSize.Height - 1))

        Return New Point(x, y)
    End Function

    Public Function WorldToScreen(c As Integer, r As Integer) As Point
        c = c * mCellSize + mWorldOffset.X
        r = r * mCellSize + mWorldOffset.Y

        Return New Point(c, r)
    End Function

    Private Function CountNeighbors(c As Integer, r As Integer) As Integer
        Dim w = mWorldSize.Width
        Dim h = mWorldSize.Height

        Dim n As Integer
        For x As Integer = c - 1 To c + 1
            For y As Integer = r - 1 To r + 1
                If x <> c OrElse y <> r Then
                    n += If(mWorld((x + w) Mod w)((y + h) Mod h), 1, 0)
                End If
            Next
        Next

        Return n
    End Function

    'Private Function CountNeighbors(c As Integer, r As Integer) As Integer
    '    Dim n As Integer
    '    For x As Integer = Math.Max(c - 1, 0) To Math.Min(c + 1, mWorldSize.Width - 1)
    '        For y As Integer = Math.Max(r - 1, 0) To Math.Min(r + 1, mWorldSize.Height - 1)
    '            If x <> c OrElse y <> r Then n += If(mWorld(x)(y), 1, 0)
    '        Next
    '    Next

    '    Return n
    'End Function
    Private Const k1 As Integer = 3
    Private Const k2 As Integer = k1 - 1

    Private Sub ProcessSub()
        Dim changedCells As New Dictionary(Of Point, Boolean)
        Dim t1 As TimeSpan
        Dim t2 As TimeSpan
        Dim td As TimeSpan = TimeSpan.FromMilliseconds(15)

        Do Until cancelThread
            runWaitEvent.WaitOne(Timeout.Infinite, False)
            If cancelThread Then Exit Sub

            Do
                t1 = New TimeSpan(My.Computer.Clock.TickCount)
                changedCells.Clear()

                Dim n As Integer
                For c As Integer = 0 To mWorldSize.Width - 1
                    For r As Integer = 0 To mWorldSize.Height - 1
                        n = CountNeighbors(c, r)

                        If mWorld(c)(r) Then
                            If Not (n = k2 OrElse n = k1) Then changedCells.Add(New Point(c, r), False)
                        Else
                            If n = k1 Then changedCells.Add(New Point(c, r), True)
                        End If
                    Next
                Next

                stepBackup.Add(New Dictionary(Of Point, Boolean)(changedCells))

                For Each changedCell As KeyValuePair(Of Point, Boolean) In changedCells
                    SetCell(changedCell.Key, changedCell.Value)
                Next

                RaiseEvent Update()

                ' Ensure a constant delay
                If mIsRunning Then
                    t2 = New TimeSpan(My.Computer.Clock.TickCount)
                    If t2 - t1 < td Then Thread.Sleep(CInt((td - (t2 - t1)).TotalMilliseconds))
                End If
            Loop While mIsRunning
        Loop
    End Sub

    Public Function LoadShapeFromFile(fileName As String) As Boolean()()
        Dim shape()() As Boolean = Nothing

        Dim data As String = IO.File.ReadAllText(fileName)
        If data.StartsWith("!") AndAlso data.Contains("O") Then
            Dim lines() As String
            Dim EOL As String = ""

            If data.Contains(vbCr + vbLf) Then
                EOL = vbCr + vbLf
            ElseIf data.Contains(vbLf + vbCr) Then
                EOL = vbLf + vbCr
            ElseIf data.Contains(vbLf) Then
                EOL = vbLf
            ElseIf data.Contains(vbCr) Then
                EOL = vbCr
            End If

            lines = data.Split(New String() {EOL}, StringSplitOptions.RemoveEmptyEntries)
            Dim numRows As Integer = 0
            Dim startingRow As Integer = -1
            For r As Integer = 0 To lines.Length - 1
                If lines(r).StartsWith(".") OrElse lines(r).StartsWith("O") Then
                    If startingRow = -1 Then startingRow = r
                    numRows += 1
                End If
            Next

            InitArray(shape, lines(startingRow).Length, numRows, False)
            For r As Integer = 0 To numRows - 1
                For c As Integer = 0 To shape.Length - 1
                    If c < lines(r + startingRow).Length Then
                        shape(c)(r) = If(lines(r + startingRow).Substring(c, 1) = "O", True, False)
                    Else
                        ' Assume false for the rest of the cells
                        Exit For
                    End If
                Next
            Next r
        Else
            Throw New Exception("Unsupported File Type")
        End If

        Return shape
    End Function

    Public Sub InsertShape(tc As Integer, tr As Integer)
        mIsInserting = False

        For c As Integer = 0 To mShape.Length - 1
            For r As Integer = 0 To mShape(c).Length - 1
                If c + tc >= 0 AndAlso c + tc <= mWorldSize.Width - 1 AndAlso
                    r + tr >= 0 AndAlso r + tr <= mWorldSize.Height - 1 Then

                    Select Case mPaintCellMode
                        Case PaintCellModes.Copy : SetCell(c + tc, r + tr, mShape(c)(r))
                        Case PaintCellModes.OR : SetCell(c + tc, r + tr, mShape(c)(r) Or mWorld(c + tc)(r + tr))
                        Case PaintCellModes.AND : SetCell(c + tc, r + tr, mShape(c)(r) And mWorld(c + tc)(r + tr))
                        Case PaintCellModes.XOR : SetCell(c + tc, r + tr, mShape(c)(r) Xor mWorld(c + tc)(r + tr))
                        Case PaintCellModes.NAND : SetCell(c + tc, r + tr, Not (mShape(c)(r) And mWorld(c + tc)(r + tr)))
                        Case PaintCellModes.NOT : SetCell(c + tc, r + tr, Not mShape(c)(r))
                    End Select
                End If
            Next
        Next

        If Not mIsRunning Then RaiseEvent Update()
    End Sub

    Public Function ShapeToBitmap(cellSize As Integer, backColor As Color, Optional p As Point = Nothing) As Bitmap
        Return ShapeToBitmap(mShape, cellSize, backColor, p)
    End Function

    Public Function ShapeToBitmap(shape()() As Boolean, cellSize As Integer, backColor As Color, Optional p As Point = Nothing) As Bitmap
        Dim b As Bitmap = New Bitmap(shape.Length * cellSize + 2 * cellSize + 1, shape(0).Length * cellSize + 2 * cellSize + 1)
        Dim g As Graphics = Graphics.FromImage(b)

        g.Clear(backColor)
        For r As Integer = 0 To shape(0).Length - 1
            For c As Integer = 0 To shape.Length - 1
                Dim cellRect = New Rectangle(c * cellSize + cellSize, r * cellSize + cellSize, cellSize, cellSize)

                Select Case mPaintCellMode
                    Case PaintCellModes.Copy : If shape(c)(r) Then g.FillRectangle(Brushes.Blue, cellRect)
                    Case PaintCellModes.OR : If shape(c)(r) Or mWorld(c + p.X)(r + p.Y) Then g.FillRectangle(Brushes.Blue, cellRect)
                    Case PaintCellModes.AND : If shape(c)(r) And mWorld(c + p.X)(r + p.Y) Then g.FillRectangle(Brushes.Blue, cellRect)
                    Case PaintCellModes.XOR : If shape(c)(r) Xor mWorld(c + p.X)(r + p.Y) Then g.FillRectangle(Brushes.Blue, cellRect)
                    Case PaintCellModes.NAND : If Not (shape(c)(r) And mWorld(c + p.X)(r + p.Y)) Then g.FillRectangle(Brushes.Blue, cellRect)
                    Case PaintCellModes.NOT : If Not shape(c)(r) Then g.FillRectangle(Brushes.Blue, cellRect)
                End Select
            Next
        Next
        g.DrawRectangle(Pens.Black, 0, 0, b.Width - 1, b.Height - 1)
        g.Dispose()

        Return b
    End Function

    Public Sub StartInserting(Optional p As Point = Nothing)
        mIsInserting = True
        If dragBPM IsNot Nothing Then dragBPM.Dispose()
        dragBPM = ShapeToBitmap(mCellSize, Color.FromArgb(220, Color.White), p)
        If Not mIsRunning Then RaiseEvent Update()
    End Sub

    Public Sub RotateShape()
        Dim newShape()() As Boolean = Nothing
        InitArray(newShape, mShape(0).Length, mShape.Length, False)

        For r As Integer = 0 To mShape(0).Length - 1
            For c As Integer = 0 To mShape.Length - 1
                newShape(mShape(0).Length - 1 - r)(c) = mShape(c)(r)
            Next
        Next

        mShape = newShape
        StartInserting()
    End Sub

    Private Sub InitArray(ByRef a()() As Boolean, ByVal cols As Integer, ByVal rows As Integer, ByVal updateWorldBitmap As Boolean)
        ReDim a(cols - 1)

        For c As Integer = 0 To cols - 1
            ReDim a(c)(rows - 1)

            If updateWorldBitmap Then For r As Integer = 0 To rows - 1 : SetCell(c, r, False, False) : Next
        Next
    End Sub

    Private Sub RepaintWorld()
        For c As Integer = 0 To mWorldSize.Width - 1
            For r As Integer = 0 To mWorldSize.Height - 1
                PaintCell(c, r, mWorld(c)(r))
            Next
        Next
    End Sub

    Public Function ExtractShape() As Boolean()()
        Dim wasRunning As Boolean = mIsRunning
        mIsRunning = False

        Dim firstCol As Integer = -1
        Dim lastCol As Integer = -1
        Dim firstRow As Integer = -1
        Dim lastRow As Integer = -1

        For c As Integer = 0 To mWorldSize.Width - 1
            For r As Integer = 0 To mWorldSize.Height - 1
                If mWorld(c)(r) Then
                    firstCol = If(firstCol = -1, c, Math.Min(c, firstCol))
                    lastCol = Math.Max(c, lastCol)

                    firstRow = If(firstRow = -1, r, Math.Min(r, firstRow))
                    lastRow = Math.Max(r, lastRow)
                End If
            Next
        Next

        Dim shape()() As Boolean = Nothing
        Dim cols As Integer = lastCol - firstCol
        Dim rows As Integer = lastRow - firstRow
        InitArray(shape, cols + 1, rows + 1, False)

        If cols <> 0 AndAlso rows <> 0 Then
            For c As Integer = 0 To cols
                For r As Integer = 0 To rows
                    shape(c)(r) = mWorld(c + firstCol)(r + firstRow)
                Next
            Next
        End If

        mIsInserting = wasRunning

        Return shape
    End Function

    Public Sub SaveShape(ByVal shape()() As Boolean, ByVal name As String, ByVal fileName As String)
        Dim data As String = ""
        Dim EOL As String = Environment.NewLine

        data += "!Name:" + name + EOL
        data += "!" + EOL
        For r As Integer = 0 To shape(0).Length - 1
            For c As Integer = 0 To shape.Length - 1
                data += If(shape(c)(r), "O", ".")
            Next
            data += EOL
        Next

        IO.File.WriteAllText(fileName, data)
    End Sub
End Class
