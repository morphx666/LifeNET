<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormWorldSettings
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBoxColumns = New System.Windows.Forms.TextBox()
        Me.TextBoxRows = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.CheckBoxWrapAround = New System.Windows.Forms.CheckBox()
        Me.ButtonSave = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(10, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(53, 18)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Columns"
        '
        'TextBoxColumns
        '
        Me.TextBoxColumns.Location = New System.Drawing.Point(69, 12)
        Me.TextBoxColumns.Name = "TextBoxColumns"
        Me.TextBoxColumns.Size = New System.Drawing.Size(61, 21)
        Me.TextBoxColumns.TabIndex = 1
        '
        'TextBoxRows
        '
        Me.TextBoxRows.Location = New System.Drawing.Point(69, 39)
        Me.TextBoxRows.Name = "TextBoxRows"
        Me.TextBoxRows.Size = New System.Drawing.Size(61, 21)
        Me.TextBoxRows.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(10, 40)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(36, 18)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Rows"
        '
        'CheckBoxWrapAround
        '
        Me.CheckBoxWrapAround.AutoSize = True
        Me.CheckBoxWrapAround.Location = New System.Drawing.Point(13, 66)
        Me.CheckBoxWrapAround.Name = "CheckBoxWrapAround"
        Me.CheckBoxWrapAround.Size = New System.Drawing.Size(97, 22)
        Me.CheckBoxWrapAround.TabIndex = 4
        Me.CheckBoxWrapAround.Text = "Wrap Around"
        Me.CheckBoxWrapAround.UseVisualStyleBackColor = True
        '
        'ButtonSave
        '
        Me.ButtonSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonSave.Location = New System.Drawing.Point(192, 116)
        Me.ButtonSave.Name = "ButtonSave"
        Me.ButtonSave.Size = New System.Drawing.Size(75, 25)
        Me.ButtonSave.TabIndex = 5
        Me.ButtonSave.Text = "Save"
        Me.ButtonSave.UseVisualStyleBackColor = True
        '
        'FormWorldSettings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(279, 153)
        Me.ControlBox = False
        Me.Controls.Add(Me.ButtonSave)
        Me.Controls.Add(Me.CheckBoxWrapAround)
        Me.Controls.Add(Me.TextBoxRows)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TextBoxColumns)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("Trebuchet MS", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "FormWorldSettings"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "World Settings"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents TextBoxColumns As TextBox
    Friend WithEvents TextBoxRows As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents CheckBoxWrapAround As CheckBox
    Friend WithEvents ButtonSave As Button
End Class
