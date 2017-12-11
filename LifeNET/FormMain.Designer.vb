<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.MenuMain = New System.Windows.Forms.MenuStrip()
        Me.MenuMainFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuMainFileNew = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator()
        Me.MenuMainFileOpen = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuMainFileSave = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripSeparator()
        Me.MenuMainFileExit = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuMainShapes = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuMain
        '
        Me.MenuMain.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MenuMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MenuMainFile, Me.MenuMainShapes})
        Me.MenuMain.Location = New System.Drawing.Point(0, 0)
        Me.MenuMain.Name = "MenuMain"
        Me.MenuMain.Padding = New System.Windows.Forms.Padding(7, 2, 0, 2)
        Me.MenuMain.Size = New System.Drawing.Size(737, 24)
        Me.MenuMain.TabIndex = 0
        '
        'MenuMainFile
        '
        Me.MenuMainFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MenuMainFileNew, Me.ToolStripMenuItem1, Me.MenuMainFileOpen, Me.MenuMainFileSave, Me.ToolStripMenuItem2, Me.MenuMainFileExit})
        Me.MenuMainFile.Name = "MenuMainFile"
        Me.MenuMainFile.Size = New System.Drawing.Size(37, 20)
        Me.MenuMainFile.Text = "File"
        '
        'MenuMainFileNew
        '
        Me.MenuMainFileNew.Name = "MenuMainFileNew"
        Me.MenuMainFileNew.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N), System.Windows.Forms.Keys)
        Me.MenuMainFileNew.Size = New System.Drawing.Size(155, 22)
        Me.MenuMainFileNew.Text = "New"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(152, 6)
        '
        'MenuMainFileOpen
        '
        Me.MenuMainFileOpen.Name = "MenuMainFileOpen"
        Me.MenuMainFileOpen.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O), System.Windows.Forms.Keys)
        Me.MenuMainFileOpen.Size = New System.Drawing.Size(155, 22)
        Me.MenuMainFileOpen.Text = "Open..."
        '
        'MenuMainFileSave
        '
        Me.MenuMainFileSave.Name = "MenuMainFileSave"
        Me.MenuMainFileSave.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.MenuMainFileSave.Size = New System.Drawing.Size(155, 22)
        Me.MenuMainFileSave.Text = "Save"
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(152, 6)
        '
        'MenuMainFileExit
        '
        Me.MenuMainFileExit.Name = "MenuMainFileExit"
        Me.MenuMainFileExit.Size = New System.Drawing.Size(155, 22)
        Me.MenuMainFileExit.Text = "Exit"
        '
        'MenuMainShapes
        '
        Me.MenuMainShapes.Name = "MenuMainShapes"
        Me.MenuMainShapes.Size = New System.Drawing.Size(56, 20)
        Me.MenuMainShapes.Text = "Shapes"
        '
        'FormMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(737, 520)
        Me.Controls.Add(Me.MenuMain)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "FormMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "LifeNET"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.MenuMain.ResumeLayout(False)
        Me.MenuMain.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuMain As System.Windows.Forms.MenuStrip
    Friend WithEvents MenuMainFile As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuMainFileNew As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents MenuMainFileOpen As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuMainFileSave As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuMainShapes As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents MenuMainFileExit As System.Windows.Forms.ToolStripMenuItem

End Class
