<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormSaveShape
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
        Me.PictureBoxShapePreview = New System.Windows.Forms.PictureBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBoxShapeName = New System.Windows.Forms.TextBox()
        Me.ButtonOK = New System.Windows.Forms.Button()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        CType(Me.PictureBoxShapePreview, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PictureBoxShapePreview
        '
        Me.PictureBoxShapePreview.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBoxShapePreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PictureBoxShapePreview.Location = New System.Drawing.Point(12, 12)
        Me.PictureBoxShapePreview.Name = "PictureBoxShapePreview"
        Me.PictureBoxShapePreview.Size = New System.Drawing.Size(326, 274)
        Me.PictureBoxShapePreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBoxShapePreview.TabIndex = 0
        Me.PictureBoxShapePreview.TabStop = False
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(344, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(156, 15)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Type the name of this shape"
        '
        'TextBoxShapeName
        '
        Me.TextBoxShapeName.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxShapeName.Location = New System.Drawing.Point(347, 30)
        Me.TextBoxShapeName.Name = "TextBoxShapeName"
        Me.TextBoxShapeName.Size = New System.Drawing.Size(287, 23)
        Me.TextBoxShapeName.TabIndex = 2
        '
        'ButtonOK
        '
        Me.ButtonOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOK.Location = New System.Drawing.Point(478, 263)
        Me.ButtonOK.Name = "ButtonOK"
        Me.ButtonOK.Size = New System.Drawing.Size(75, 23)
        Me.ButtonOK.TabIndex = 3
        Me.ButtonOK.Text = "OK"
        Me.ButtonOK.UseVisualStyleBackColor = True
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.Location = New System.Drawing.Point(559, 263)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(75, 23)
        Me.ButtonCancel.TabIndex = 3
        Me.ButtonCancel.Text = "Cancel"
        Me.ButtonCancel.UseVisualStyleBackColor = True
        '
        'FormSaveShape
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(646, 298)
        Me.Controls.Add(Me.ButtonCancel)
        Me.Controls.Add(Me.ButtonOK)
        Me.Controls.Add(Me.TextBoxShapeName)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.PictureBoxShapePreview)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "FormSaveShape"
        Me.Text = "Save Shape"
        CType(Me.PictureBoxShapePreview, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PictureBoxShapePreview As System.Windows.Forms.PictureBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBoxShapeName As System.Windows.Forms.TextBox
    Friend WithEvents ButtonOK As System.Windows.Forms.Button
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
End Class
