﻿namespace WFPresentationLayer
{
    partial class FormLogin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLogin));
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtSenha = new System.Windows.Forms.TextBox();
            this.btnSignIn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Password = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(357, 147);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(140, 20);
            this.txtEmail.TabIndex = 0;
            // 
            // txtSenha
            // 
            this.txtSenha.Location = new System.Drawing.Point(357, 193);
            this.txtSenha.Name = "txtSenha";
            this.txtSenha.PasswordChar = '*';
            this.txtSenha.Size = new System.Drawing.Size(140, 20);
            this.txtSenha.TabIndex = 1;
            this.txtSenha.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSenha_KeyPress);
            // 
            // btnSignIn
            // 
            this.btnSignIn.Font = new System.Drawing.Font("Cambria", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSignIn.Location = new System.Drawing.Point(357, 266);
            this.btnSignIn.Name = "btnSignIn";
            this.btnSignIn.Size = new System.Drawing.Size(80, 23);
            this.btnSignIn.TabIndex = 2;
            this.btnSignIn.Text = "Sign in";
            this.btnSignIn.UseVisualStyleBackColor = true;
            this.btnSignIn.Click += new System.EventHandler(this.btnSignIn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe Script", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(303, 147);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Email";
            // 
            // Password
            // 
            this.Password.AutoSize = true;
            this.Password.Font = new System.Drawing.Font("Segoe Script", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Password.Location = new System.Drawing.Point(303, 193);
            this.Password.Name = "Password";
            this.Password.Size = new System.Drawing.Size(48, 20);
            this.Password.TabIndex = 4;
            this.Password.Text = "Senha";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe Script", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(325, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(132, 34);
            this.label2.TabIndex = 5;
            this.label2.Text = "Park Hotel";
            // 
            // FormLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Password);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSignIn);
            this.Controls.Add(this.txtSenha);
            this.Controls.Add(this.txtEmail);
            this.Name = "FormLogin";
            this.Text = "FormLogin";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtSenha;
        private System.Windows.Forms.Button btnSignIn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label Password;
        private System.Windows.Forms.Label label2;
    }
}