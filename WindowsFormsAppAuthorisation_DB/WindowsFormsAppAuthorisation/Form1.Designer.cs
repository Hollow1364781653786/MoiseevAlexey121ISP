namespace WindowsFormsAppAuthorisation
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
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
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.loginenbox = new System.Windows.Forms.TextBox();
            this.passenbox = new System.Windows.Forms.TextBox();
            this.enterbutton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(176, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Вход";
            // 
            // loginenbox
            // 
            this.loginenbox.Location = new System.Drawing.Point(141, 100);
            this.loginenbox.Name = "loginenbox";
            this.loginenbox.Size = new System.Drawing.Size(100, 20);
            this.loginenbox.TabIndex = 3;
            this.loginenbox.TextChanged += new System.EventHandler(this.loginenbox_TextChanged);
            // 
            // passenbox
            // 
            this.passenbox.Location = new System.Drawing.Point(141, 167);
            this.passenbox.Name = "passenbox";
            this.passenbox.Size = new System.Drawing.Size(100, 20);
            this.passenbox.TabIndex = 5;
            this.passenbox.TextChanged += new System.EventHandler(this.passenbox_TextChanged);
            // 
            // enterbutton
            // 
            this.enterbutton.Location = new System.Drawing.Point(153, 215);
            this.enterbutton.Name = "enterbutton";
            this.enterbutton.Size = new System.Drawing.Size(75, 23);
            this.enterbutton.TabIndex = 6;
            this.enterbutton.Text = "button1";
            this.enterbutton.UseVisualStyleBackColor = true;
            this.enterbutton.Click += new System.EventHandler(this.enterbutton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 271);
            this.Controls.Add(this.enterbutton);
            this.Controls.Add(this.passenbox);
            this.Controls.Add(this.loginenbox);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox loginenbox;
        private System.Windows.Forms.TextBox passenbox;
        private System.Windows.Forms.Button enterbutton;
    }
}

