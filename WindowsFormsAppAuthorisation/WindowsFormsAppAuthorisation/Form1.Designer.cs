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
            this.label2 = new System.Windows.Forms.Label();
            this.loginregbox = new System.Windows.Forms.TextBox();
            this.loginenbox = new System.Windows.Forms.TextBox();
            this.passregbox = new System.Windows.Forms.TextBox();
            this.passenbox = new System.Windows.Forms.TextBox();
            this.enterbutton = new System.Windows.Forms.Button();
            this.registerbutton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(47, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Вход";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(281, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Регистрация";
            // 
            // loginregbox
            // 
            this.loginregbox.Location = new System.Drawing.Point(267, 100);
            this.loginregbox.Name = "loginregbox";
            this.loginregbox.Size = new System.Drawing.Size(100, 20);
            this.loginregbox.TabIndex = 2;
            // 
            // loginenbox
            // 
            this.loginenbox.Location = new System.Drawing.Point(12, 100);
            this.loginenbox.Name = "loginenbox";
            this.loginenbox.Size = new System.Drawing.Size(100, 20);
            this.loginenbox.TabIndex = 3;
            // 
            // passregbox
            // 
            this.passregbox.Location = new System.Drawing.Point(267, 167);
            this.passregbox.Name = "passregbox";
            this.passregbox.Size = new System.Drawing.Size(100, 20);
            this.passregbox.TabIndex = 4;
            // 
            // passenbox
            // 
            this.passenbox.Location = new System.Drawing.Point(12, 167);
            this.passenbox.Name = "passenbox";
            this.passenbox.Size = new System.Drawing.Size(100, 20);
            this.passenbox.TabIndex = 5;
            // 
            // enterbutton
            // 
            this.enterbutton.Location = new System.Drawing.Point(24, 215);
            this.enterbutton.Name = "enterbutton";
            this.enterbutton.Size = new System.Drawing.Size(75, 23);
            this.enterbutton.TabIndex = 6;
            this.enterbutton.Text = "button1";
            this.enterbutton.UseVisualStyleBackColor = true;
            this.enterbutton.Click += new System.EventHandler(this.enterbutton_Click);
            // 
            // registerbutton
            // 
            this.registerbutton.Location = new System.Drawing.Point(278, 215);
            this.registerbutton.Name = "registerbutton";
            this.registerbutton.Size = new System.Drawing.Size(75, 23);
            this.registerbutton.TabIndex = 7;
            this.registerbutton.Text = "button2";
            this.registerbutton.UseVisualStyleBackColor = true;
            this.registerbutton.Click += new System.EventHandler(this.registerbutton_Click_1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 271);
            this.Controls.Add(this.registerbutton);
            this.Controls.Add(this.enterbutton);
            this.Controls.Add(this.passenbox);
            this.Controls.Add(this.passregbox);
            this.Controls.Add(this.loginenbox);
            this.Controls.Add(this.loginregbox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox loginregbox;
        private System.Windows.Forms.TextBox loginenbox;
        private System.Windows.Forms.TextBox passregbox;
        private System.Windows.Forms.TextBox passenbox;
        private System.Windows.Forms.Button enterbutton;
        private System.Windows.Forms.Button registerbutton;
    }
}

