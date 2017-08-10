namespace PruebaWebBrowser
{
    partial class Principal
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.reiniciarBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // reiniciarBtn
            // 
            this.reiniciarBtn.BackColor = System.Drawing.Color.CadetBlue;
            this.reiniciarBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.reiniciarBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reiniciarBtn.Location = new System.Drawing.Point(115, 103);
            this.reiniciarBtn.Margin = new System.Windows.Forms.Padding(200);
            this.reiniciarBtn.Name = "reiniciarBtn";
            this.reiniciarBtn.Padding = new System.Windows.Forms.Padding(200);
            this.reiniciarBtn.Size = new System.Drawing.Size(99, 43);
            this.reiniciarBtn.TabIndex = 0;
            this.reiniciarBtn.Text = "REINICIAR APP";
            this.reiniciarBtn.UseVisualStyleBackColor = false;
            this.reiniciarBtn.Visible = false;
            this.reiniciarBtn.Click += new System.EventHandler(this.reiniciarBtn_Click);
            // 
            // Principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.ControlBox = false;
            this.Controls.Add(this.reiniciarBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Principal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button reiniciarBtn;
    }
}

