namespace PI_Simuladores
{
    partial class Form1
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
            this.txtTarjeta = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbTarjetas = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.btnServico = new System.Windows.Forms.Button();
            this.grpServicio = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSaldo = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tb = new System.Windows.Forms.GroupBox();
            this.btnGenerarUnidades = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtCantidadUnidades = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.grpServicio.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tb.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtTarjeta
            // 
            this.txtTarjeta.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtTarjeta.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtTarjeta.Location = new System.Drawing.Point(69, 20);
            this.txtTarjeta.MaxLength = 12;
            this.txtTarjeta.Name = "txtTarjeta";
            this.txtTarjeta.Size = new System.Drawing.Size(151, 22);
            this.txtTarjeta.TabIndex = 0;
            this.txtTarjeta.TextChanged += new System.EventHandler(this.cuandoTarjetaCambia);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Tarjeta:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // cmbTarjetas
            // 
            this.cmbTarjetas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTarjetas.FormattingEnabled = true;
            this.cmbTarjetas.Location = new System.Drawing.Point(9, 48);
            this.cmbTarjetas.Name = "cmbTarjetas";
            this.cmbTarjetas.Size = new System.Drawing.Size(211, 24);
            this.cmbTarjetas.TabIndex = 2;
            this.cmbTarjetas.SelectedValueChanged += new System.EventHandler(this.cuandoValorCambia);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Fecha:";
            // 
            // dtpFecha
            // 
            this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFecha.Location = new System.Drawing.Point(63, 75);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(157, 22);
            this.dtpFecha.TabIndex = 4;
            // 
            // btnServico
            // 
            this.btnServico.Location = new System.Drawing.Point(9, 101);
            this.btnServico.Name = "btnServico";
            this.btnServico.Size = new System.Drawing.Size(211, 23);
            this.btnServico.TabIndex = 5;
            this.btnServico.Text = "Servicio!";
            this.btnServico.UseVisualStyleBackColor = true;
            // 
            // grpServicio
            // 
            this.grpServicio.Controls.Add(this.label1);
            this.grpServicio.Controls.Add(this.btnServico);
            this.grpServicio.Controls.Add(this.txtTarjeta);
            this.grpServicio.Controls.Add(this.dtpFecha);
            this.grpServicio.Controls.Add(this.cmbTarjetas);
            this.grpServicio.Controls.Add(this.label2);
            this.grpServicio.Location = new System.Drawing.Point(12, 12);
            this.grpServicio.Name = "grpServicio";
            this.grpServicio.Size = new System.Drawing.Size(231, 134);
            this.grpServicio.TabIndex = 6;
            this.grpServicio.TabStop = false;
            this.grpServicio.Text = "Servicio";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 17);
            this.label3.TabIndex = 7;
            this.label3.Text = "Nombre:";
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(74, 20);
            this.txtNombre.MaxLength = 12;
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(192, 22);
            this.txtNombre.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 52);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 17);
            this.label5.TabIndex = 10;
            this.label5.Text = "Saldo:";
            // 
            // txtSaldo
            // 
            this.txtSaldo.Location = new System.Drawing.Point(74, 49);
            this.txtSaldo.MaxLength = 12;
            this.txtSaldo.Name = "txtSaldo";
            this.txtSaldo.Size = new System.Drawing.Size(192, 22);
            this.txtSaldo.TabIndex = 11;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtSaldo);
            this.groupBox2.Controls.Add(this.txtNombre);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(249, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(280, 82);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Usuario";
            // 
            // tb
            // 
            this.tb.Controls.Add(this.btnGenerarUnidades);
            this.tb.Controls.Add(this.label6);
            this.tb.Controls.Add(this.txtCantidadUnidades);
            this.tb.Location = new System.Drawing.Point(249, 100);
            this.tb.Name = "tb";
            this.tb.Size = new System.Drawing.Size(280, 46);
            this.tb.TabIndex = 14;
            this.tb.TabStop = false;
            this.tb.Text = "Unidades";
            // 
            // btnGenerarUnidades
            // 
            this.btnGenerarUnidades.Location = new System.Drawing.Point(161, 18);
            this.btnGenerarUnidades.Name = "btnGenerarUnidades";
            this.btnGenerarUnidades.Size = new System.Drawing.Size(105, 23);
            this.btnGenerarUnidades.TabIndex = 15;
            this.btnGenerarUnidades.Text = "Generar";
            this.btnGenerarUnidades.UseVisualStyleBackColor = true;
            this.btnGenerarUnidades.Click += new System.EventHandler(this.btnGenerarUnidades_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 17);
            this.label6.TabIndex = 13;
            this.label6.Text = "Cantidad:";
            // 
            // txtCantidadUnidades
            // 
            this.txtCantidadUnidades.Location = new System.Drawing.Point(80, 19);
            this.txtCantidadUnidades.MaxLength = 12;
            this.txtCantidadUnidades.Name = "txtCantidadUnidades";
            this.txtCantidadUnidades.Size = new System.Drawing.Size(75, 22);
            this.txtCantidadUnidades.TabIndex = 14;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 162);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(541, 25);
            this.statusStrip1.TabIndex = 15;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(151, 20);
            this.lblStatus.Text = "toolStripStatusLabel1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(541, 187);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tb);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.grpServicio);
            this.Name = "Form1";
            this.Text = "Simulador Autobuses";
            this.grpServicio.ResumeLayout(false);
            this.grpServicio.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tb.ResumeLayout(false);
            this.tb.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtTarjeta;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbTarjetas;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.Button btnServico;
        private System.Windows.Forms.GroupBox grpServicio;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSaldo;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox tb;
        private System.Windows.Forms.Button btnGenerarUnidades;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtCantidadUnidades;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
    }
}

