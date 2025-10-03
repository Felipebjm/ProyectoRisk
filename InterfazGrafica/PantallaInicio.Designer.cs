namespace InterfazGrafica
{
    partial class PantallaInicio
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblJ1 = new Label();
            lblJ2 = new Label();
            lblJ3 = new Label();
            txtJugador1 = new TextBox();
            txtJugador2 = new TextBox();
            txtJugador3 = new TextBox();
            btnIniciar = new Button();
            SuspendLayout();
            // 
            // lblJ1
            // 
            lblJ1.AutoSize = true;
            lblJ1.Location = new Point(284, 129);
            lblJ1.Name = "lblJ1";
            lblJ1.Size = new Size(106, 15);
            lblJ1.TabIndex = 0;
            lblJ1.Text = "Jugador 1 nombre:";
            // 
            // lblJ2
            // 
            lblJ2.AutoSize = true;
            lblJ2.Location = new Point(284, 198);
            lblJ2.Name = "lblJ2";
            lblJ2.Size = new Size(106, 15);
            lblJ2.TabIndex = 1;
            lblJ2.Text = "Jugador 2 nombre:";

            // 
            // lblJ3
            // 
            lblJ3.AutoSize = true;
            lblJ3.Location = new Point(284, 269);
            lblJ3.Name = "lblJ3";
            lblJ3.Size = new Size(106, 15);
            lblJ3.TabIndex = 2;
            lblJ3.Text = "Jugador 3 nombre:";
            // 
            // txtJugador1
            // 
            txtJugador1.Location = new Point(433, 129);
            txtJugador1.Name = "txtJugador1";
            txtJugador1.Size = new Size(100, 23);
            txtJugador1.TabIndex = 3;
            // 
            // txtJugador2
            // 
            txtJugador2.Location = new Point(433, 190);
            txtJugador2.Name = "txtJugador2";
            txtJugador2.Size = new Size(100, 23);
            txtJugador2.TabIndex = 4;
            // 
            // txtJugador3
            // 
            txtJugador3.Location = new Point(433, 261);
            txtJugador3.Name = "txtJugador3";
            txtJugador3.Size = new Size(100, 23);
            txtJugador3.TabIndex = 5;
            // 
            // btnIniciar
            // 
            btnIniciar.Location = new Point(198, 337);
            btnIniciar.Name = "btnIniciar";
            btnIniciar.Size = new Size(445, 23);
            btnIniciar.TabIndex = 8;
            btnIniciar.Text = "Iniciar Partida";
            btnIniciar.UseVisualStyleBackColor = true;
            btnIniciar.Click += btnIniciar_Click;
            // 
            // PantallaInicio
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnIniciar);
            Controls.Add(txtJugador3);
            Controls.Add(txtJugador2);
            Controls.Add(txtJugador1);
            Controls.Add(lblJ3);
            Controls.Add(lblJ2);
            Controls.Add(lblJ1);
            Name = "PantallaInicio";
            Text = "PantallaInicio";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblJ1;
        private Label lblJ2;
        private Label lblJ3;
        private TextBox txtJugador1;
        private TextBox txtJugador2;
        private TextBox txtJugador3;
        private Button btnIniciar;
    }
}
