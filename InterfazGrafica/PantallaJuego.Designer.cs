namespace InterfazGrafica
{
    partial class PantallaJuego
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
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            panelInferior = new Panel();
            txtAnuncios = new TextBox();
            txtDadosD = new TextBox();
            txtDadosA = new TextBox();
            lblDadosD = new Label();
            lblDadosA = new Label();
            lblFase = new Label();
            txtFase = new TextBox();
            btnEjecutar = new Button();
            lblCantidad = new Label();
            lblIdDestino = new Label();
            lblIdOrigen = new Label();
            txtCantidad = new TextBox();
            txtIdDestino = new TextBox();
            txtIdOrigen = new TextBox();
            txtTurno = new TextBox();
            lblTurno = new Label();
            txtTropasJ3 = new TextBox();
            txtTropasJ2 = new TextBox();
            txtTropasJ1 = new TextBox();
            lblTropasJ3 = new Label();
            lblTropasJ2 = new Label();
            lblTropasJ1 = new Label();
            panelInferior.SuspendLayout();
            SuspendLayout();
            // 
            // panelInferior
            // 
            panelInferior.BackColor = SystemColors.InactiveCaption;
            panelInferior.Controls.Add(txtAnuncios);
            panelInferior.Controls.Add(txtDadosD);
            panelInferior.Controls.Add(txtDadosA);
            panelInferior.Controls.Add(lblDadosD);
            panelInferior.Controls.Add(lblDadosA);
            panelInferior.Controls.Add(lblFase);
            panelInferior.Controls.Add(txtFase);
            panelInferior.Controls.Add(btnEjecutar);
            panelInferior.Controls.Add(lblCantidad);
            panelInferior.Controls.Add(lblIdDestino);
            panelInferior.Controls.Add(lblIdOrigen);
            panelInferior.Controls.Add(txtCantidad);
            panelInferior.Controls.Add(txtIdDestino);
            panelInferior.Controls.Add(txtIdOrigen);
            panelInferior.Controls.Add(txtTurno);
            panelInferior.Controls.Add(lblTurno);
            panelInferior.Controls.Add(txtTropasJ3);
            panelInferior.Controls.Add(txtTropasJ2);
            panelInferior.Controls.Add(txtTropasJ1);
            panelInferior.Controls.Add(lblTropasJ3);
            panelInferior.Controls.Add(lblTropasJ2);
            panelInferior.Controls.Add(lblTropasJ1);
            panelInferior.Location = new Point(-13, 470);
            panelInferior.Name = "panelInferior";
            panelInferior.Size = new Size(1082, 150);
            panelInferior.TabIndex = 0;
            // 
            // txtAnuncios
            // 
            txtAnuncios.Location = new Point(372, 14);
            txtAnuncios.Name = "txtAnuncios";
            txtAnuncios.ReadOnly = true;
            txtAnuncios.Size = new Size(368, 23);
            txtAnuncios.TabIndex = 21;
            // 
            // txtDadosD
            // 
            txtDadosD.Location = new Point(790, 73);
            txtDadosD.Name = "txtDadosD";
            txtDadosD.Size = new Size(100, 23);
            txtDadosD.TabIndex = 20;
            // 
            // txtDadosA
            // 
            txtDadosA.Location = new Point(790, 29);
            txtDadosA.Name = "txtDadosA";
            txtDadosA.Size = new Size(100, 23);
            txtDadosA.TabIndex = 19;
            // 
            // lblDadosD
            // 
            lblDadosD.AutoSize = true;
            lblDadosD.Location = new Point(789, 55);
            lblDadosD.Name = "lblDadosD";
            lblDadosD.Size = new Size(89, 15);
            lblDadosD.TabIndex = 18;
            lblDadosD.Text = "Dados defensor";
            // 
            // lblDadosA
            // 
            lblDadosA.AutoSize = true;
            lblDadosA.Location = new Point(790, 14);
            lblDadosA.Name = "lblDadosA";
            lblDadosA.Size = new Size(88, 15);
            lblDadosA.TabIndex = 17;
            lblDadosA.Text = "Dados atacante";
            // 
            // lblFase
            // 
            lblFase.AutoSize = true;
            lblFase.Location = new Point(213, 50);
            lblFase.Name = "lblFase";
            lblFase.Size = new Size(33, 15);
            lblFase.TabIndex = 16;
            lblFase.Text = "Fase:";
            // 
            // txtFase
            // 
            txtFase.Location = new Point(261, 45);
            txtFase.Name = "txtFase";
            txtFase.ReadOnly = true;
            txtFase.Size = new Size(100, 23);
            txtFase.TabIndex = 15;
            // 
            // btnEjecutar
            // 
            btnEjecutar.Location = new Point(917, 46);
            btnEjecutar.Name = "btnEjecutar";
            btnEjecutar.Size = new Size(121, 23);
            btnEjecutar.TabIndex = 14;
            btnEjecutar.Text = "Ejecutar acción";
            btnEjecutar.UseVisualStyleBackColor = true;
            btnEjecutar.Click += btnEjecutar_Click;
            // 
            // lblCantidad
            // 
            lblCantidad.AutoSize = true;
            lblCantidad.Location = new Point(640, 50);
            lblCantidad.Name = "lblCantidad";
            lblCantidad.Size = new Size(107, 15);
            lblCantidad.TabIndex = 13;
            lblCantidad.Text = "Cantidad de tropas";
            // 
            // lblIdDestino
            // 
            lblIdDestino.AutoSize = true;
            lblIdDestino.Location = new Point(534, 50);
            lblIdDestino.Name = "lblIdDestino";
            lblIdDestino.Size = new Size(79, 15);
            lblIdDestino.TabIndex = 12;
            lblIdDestino.Text = "ID del destino";
            // 
            // lblIdOrigen
            // 
            lblIdOrigen.AutoSize = true;
            lblIdOrigen.Location = new Point(400, 50);
            lblIdOrigen.Name = "lblIdOrigen";
            lblIdOrigen.Size = new Size(71, 15);
            lblIdOrigen.TabIndex = 11;
            lblIdOrigen.Text = "ID de origen";
            // 
            // txtCantidad
            // 
            txtCantidad.Location = new Point(640, 75);
            txtCantidad.Name = "txtCantidad";
            txtCantidad.Size = new Size(100, 23);
            txtCantidad.TabIndex = 10;
            // 
            // txtIdDestino
            // 
            txtIdDestino.Location = new Point(523, 75);
            txtIdDestino.Name = "txtIdDestino";
            txtIdDestino.Size = new Size(100, 23);
            txtIdDestino.TabIndex = 9;
            // 
            // txtIdOrigen
            // 
            txtIdOrigen.Location = new Point(385, 75);
            txtIdOrigen.Name = "txtIdOrigen";
            txtIdOrigen.Size = new Size(100, 23);
            txtIdOrigen.TabIndex = 8;
            // 
            // txtTurno
            // 
            txtTurno.Location = new Point(261, 75);
            txtTurno.Name = "txtTurno";
            txtTurno.ReadOnly = true;
            txtTurno.Size = new Size(100, 23);
            txtTurno.TabIndex = 7;
            // 
            // lblTurno
            // 
            lblTurno.AutoSize = true;
            lblTurno.Location = new Point(197, 78);
            lblTurno.Name = "lblTurno";
            lblTurno.Size = new Size(58, 15);
            lblTurno.TabIndex = 6;
            lblTurno.Text = "Turno de:";
            // 
            // txtTropasJ3
            // 
            txtTropasJ3.BackColor = Color.DarkGreen;
            txtTropasJ3.Location = new Point(81, 72);
            txtTropasJ3.Name = "txtTropasJ3";
            txtTropasJ3.ReadOnly = true;
            txtTropasJ3.Size = new Size(100, 23);
            txtTropasJ3.TabIndex = 5;
            // 
            // txtTropasJ2
            // 
            txtTropasJ2.BackColor = Color.Blue;
            txtTropasJ2.Location = new Point(81, 43);
            txtTropasJ2.Name = "txtTropasJ2";
            txtTropasJ2.ReadOnly = true;
            txtTropasJ2.Size = new Size(100, 23);
            txtTropasJ2.TabIndex = 4;
            // 
            // txtTropasJ1
            // 
            txtTropasJ1.BackColor = Color.Red;
            txtTropasJ1.Location = new Point(81, 14);
            txtTropasJ1.Name = "txtTropasJ1";
            txtTropasJ1.ReadOnly = true;
            txtTropasJ1.Size = new Size(100, 23);
            txtTropasJ1.TabIndex = 3;
            // 
            // lblTropasJ3
            // 
            lblTropasJ3.AutoSize = true;
            lblTropasJ3.Location = new Point(25, 75);
            lblTropasJ3.Name = "lblTropasJ3";
            lblTropasJ3.Size = new Size(55, 15);
            lblTropasJ3.TabIndex = 2;
            lblTropasJ3.Text = "Tropas J3";
            // 
            // lblTropasJ2
            // 
            lblTropasJ2.AutoSize = true;
            lblTropasJ2.Location = new Point(25, 46);
            lblTropasJ2.Name = "lblTropasJ2";
            lblTropasJ2.Size = new Size(55, 15);
            lblTropasJ2.TabIndex = 1;
            lblTropasJ2.Text = "Tropas J2";
            // 
            // lblTropasJ1
            // 
            lblTropasJ1.AutoSize = true;
            lblTropasJ1.Location = new Point(25, 17);
            lblTropasJ1.Name = "lblTropasJ1";
            lblTropasJ1.Size = new Size(55, 15);
            lblTropasJ1.TabIndex = 0;
            lblTropasJ1.Text = "Tropas J1";
            // 
            // PantallaJuego
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.Mapa;
            ClientSize = new Size(1046, 609);
            Controls.Add(panelInferior);
            Name = "PantallaJuego";
            Text = "PantallaJuego";
            panelInferior.ResumeLayout(false);
            panelInferior.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Panel panelInferior;
        private Label lblTropasJ3;
        private Label lblTropasJ2;
        private Label lblTropasJ1;
        private TextBox txtTurno;
        private Label lblTurno;
        private TextBox txtTropasJ3;
        private TextBox txtTropasJ2;
        private TextBox txtTropasJ1;
        private Label lblCantidad;
        private Label lblIdDestino;
        private Label lblIdOrigen;
        private TextBox txtCantidad;
        private TextBox txtIdDestino;
        private TextBox txtIdOrigen;
        private Button btnEjecutar;
        private Label lblDadosD;
        private Label lblDadosA;
        private Label lblFase;
        private TextBox txtFase;
        private TextBox txtDadosD;
        private TextBox txtDadosA;
        private TextBox txtAnuncios;
    }
}