namespace KinectMicrophone
{
    partial class mainForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.activateButton = new System.Windows.Forms.Button();
            this.logContainer = new System.Windows.Forms.TextBox();
            this.recTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // activateButton
            // 
            this.activateButton.Location = new System.Drawing.Point(12, 12);
            this.activateButton.Name = "activateButton";
            this.activateButton.Size = new System.Drawing.Size(75, 23);
            this.activateButton.TabIndex = 0;
            this.activateButton.Text = "Activate";
            this.activateButton.UseVisualStyleBackColor = true;
            this.activateButton.Click += new System.EventHandler(this.activateButton_Click);
            // 
            // logContainer
            // 
            this.logContainer.Location = new System.Drawing.Point(12, 67);
            this.logContainer.Multiline = true;
            this.logContainer.Name = "logContainer";
            this.logContainer.Size = new System.Drawing.Size(876, 333);
            this.logContainer.TabIndex = 1;
            // 
            // recTextBox
            // 
            this.recTextBox.Location = new System.Drawing.Point(12, 41);
            this.recTextBox.Name = "recTextBox";
            this.recTextBox.Size = new System.Drawing.Size(876, 20);
            this.recTextBox.TabIndex = 2;
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 412);
            this.Controls.Add(this.recTextBox);
            this.Controls.Add(this.logContainer);
            this.Controls.Add(this.activateButton);
            this.Name = "mainForm";
            this.Text = "Know How";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.formClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button activateButton;
        private System.Windows.Forms.TextBox logContainer;
        private System.Windows.Forms.TextBox recTextBox;
    }
}

