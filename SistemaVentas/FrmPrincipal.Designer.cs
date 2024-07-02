namespace SistemaVentas
{
    partial class FrmPrincipal
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
            this.btnCasa = new System.Windows.Forms.Button();
            this.btnBoleta = new System.Windows.Forms.Button();
            this.btnCliente = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblInicio = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCasa
            // 
            this.btnCasa.BackColor = System.Drawing.Color.Transparent;
            this.btnCasa.ForeColor = System.Drawing.Color.Transparent;
            this.btnCasa.Image = global::SistemaVentas.Properties.Resources.casa;
            this.btnCasa.Location = new System.Drawing.Point(199, 273);
            this.btnCasa.Name = "btnCasa";
            this.btnCasa.Size = new System.Drawing.Size(116, 111);
            this.btnCasa.TabIndex = 3;
            this.btnCasa.UseVisualStyleBackColor = false;
            this.btnCasa.Click += new System.EventHandler(this.btnCasa_Click);
            // 
            // btnBoleta
            // 
            this.btnBoleta.BackColor = System.Drawing.Color.Transparent;
            this.btnBoleta.Image = global::SistemaVentas.Properties.Resources.boleta1;
            this.btnBoleta.Location = new System.Drawing.Point(545, 273);
            this.btnBoleta.Name = "btnBoleta";
            this.btnBoleta.Size = new System.Drawing.Size(118, 112);
            this.btnBoleta.TabIndex = 2;
            this.btnBoleta.UseVisualStyleBackColor = false;
            this.btnBoleta.Click += new System.EventHandler(this.btnBoleta_Click);
            // 
            // btnCliente
            // 
            this.btnCliente.BackColor = System.Drawing.Color.Transparent;
            this.btnCliente.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnCliente.ForeColor = System.Drawing.Color.Transparent;
            this.btnCliente.Image = global::SistemaVentas.Properties.Resources.cliente;
            this.btnCliente.Location = new System.Drawing.Point(378, 274);
            this.btnCliente.Name = "btnCliente";
            this.btnCliente.Size = new System.Drawing.Size(111, 111);
            this.btnCliente.TabIndex = 1;
            this.btnCliente.UseVisualStyleBackColor = false;
            this.btnCliente.Click += new System.EventHandler(this.btnCliente_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pictureBox1.Image = global::SistemaVentas.Properties.Resources.fondo1;
            this.pictureBox1.Location = new System.Drawing.Point(1, 1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1302, 749);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // lblInicio
            // 
            this.lblInicio.BackColor = System.Drawing.Color.Transparent;
            this.lblInicio.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblInicio.Font = new System.Drawing.Font("Times New Roman", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInicio.ForeColor = System.Drawing.Color.Black;
            this.lblInicio.Location = new System.Drawing.Point(284, 48);
            this.lblInicio.Name = "lblInicio";
            this.lblInicio.Size = new System.Drawing.Size(314, 85);
            this.lblInicio.TabIndex = 4;
            this.lblInicio.Text = "SISTEMA DE VENTAS";
            this.lblInicio.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // FrmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(875, 453);
            this.Controls.Add(this.lblInicio);
            this.Controls.Add(this.btnCasa);
            this.Controls.Add(this.btnBoleta);
            this.Controls.Add(this.btnCliente);
            this.Controls.Add(this.pictureBox1);
            this.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.Name = "FrmPrincipal";
            this.Text = "FrmPrincipal";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnCliente;
        private System.Windows.Forms.Button btnBoleta;
        private System.Windows.Forms.Button btnCasa;
        private System.Windows.Forms.Label lblInicio;
    }
}