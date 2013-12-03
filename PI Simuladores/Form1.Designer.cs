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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.txtTarjeta = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbTarjetas = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.btnServico = new System.Windows.Forms.Button();
            this.grpServicio = new System.Windows.Forms.GroupBox();
            this.btnRandom = new System.Windows.Forms.Button();
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.grpServicio.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tb.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtTarjeta
            // 
            this.txtTarjeta.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtTarjeta.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtTarjeta.Location = new System.Drawing.Point(52, 16);
            this.txtTarjeta.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtTarjeta.MaxLength = 12;
            this.txtTarjeta.Name = "txtTarjeta";
            this.txtTarjeta.Size = new System.Drawing.Size(114, 20);
            this.txtTarjeta.TabIndex = 0;
            this.txtTarjeta.TextChanged += new System.EventHandler(this.cuandoTarjetaCambia);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 19);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Tarjeta:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // cmbTarjetas
            // 
            this.cmbTarjetas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTarjetas.FormattingEnabled = true;
            this.cmbTarjetas.Location = new System.Drawing.Point(7, 39);
            this.cmbTarjetas.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cmbTarjetas.Name = "cmbTarjetas";
            this.cmbTarjetas.Size = new System.Drawing.Size(159, 21);
            this.cmbTarjetas.TabIndex = 2;
            this.cmbTarjetas.SelectedValueChanged += new System.EventHandler(this.cuandoValorCambia);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 65);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Fecha:";
            // 
            // dtpFecha
            // 
            this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFecha.Location = new System.Drawing.Point(47, 61);
            this.dtpFecha.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(119, 20);
            this.dtpFecha.TabIndex = 4;
            // 
            // btnServico
            // 
            this.btnServico.Location = new System.Drawing.Point(7, 82);
            this.btnServico.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnServico.Name = "btnServico";
            this.btnServico.Size = new System.Drawing.Size(136, 19);
            this.btnServico.TabIndex = 5;
            this.btnServico.Text = "Servicio!";
            this.btnServico.UseVisualStyleBackColor = true;
            this.btnServico.Click += new System.EventHandler(this.btnServico_Click);
            // 
            // grpServicio
            // 
            this.grpServicio.Controls.Add(this.btnRandom);
            this.grpServicio.Controls.Add(this.label1);
            this.grpServicio.Controls.Add(this.btnServico);
            this.grpServicio.Controls.Add(this.txtTarjeta);
            this.grpServicio.Controls.Add(this.dtpFecha);
            this.grpServicio.Controls.Add(this.cmbTarjetas);
            this.grpServicio.Controls.Add(this.label2);
            this.grpServicio.Location = new System.Drawing.Point(9, 10);
            this.grpServicio.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.grpServicio.Name = "grpServicio";
            this.grpServicio.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.grpServicio.Size = new System.Drawing.Size(173, 108);
            this.grpServicio.TabIndex = 6;
            this.grpServicio.TabStop = false;
            this.grpServicio.Text = "Servicio";
            // 
            // btnRandom
            // 
            this.btnRandom.Location = new System.Drawing.Point(148, 82);
            this.btnRandom.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnRandom.Name = "btnRandom";
            this.btnRandom.Size = new System.Drawing.Size(17, 19);
            this.btnRandom.TabIndex = 6;
            this.btnRandom.Text = "?";
            this.btnRandom.UseVisualStyleBackColor = true;
            this.btnRandom.Click += new System.EventHandler(this.btnRandom_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 19);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Nombre:";
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(56, 16);
            this.txtNombre.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtNombre.MaxLength = 12;
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(145, 20);
            this.txtNombre.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 42);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Saldo:";
            // 
            // txtSaldo
            // 
            this.txtSaldo.Location = new System.Drawing.Point(56, 40);
            this.txtSaldo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtSaldo.MaxLength = 12;
            this.txtSaldo.Name = "txtSaldo";
            this.txtSaldo.Size = new System.Drawing.Size(145, 20);
            this.txtSaldo.TabIndex = 11;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtSaldo);
            this.groupBox2.Controls.Add(this.txtNombre);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(187, 10);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Size = new System.Drawing.Size(210, 67);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Usuario";
            // 
            // tb
            // 
            this.tb.Controls.Add(this.btnGenerarUnidades);
            this.tb.Controls.Add(this.label6);
            this.tb.Controls.Add(this.txtCantidadUnidades);
            this.tb.Location = new System.Drawing.Point(187, 122);
            this.tb.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tb.Name = "tb";
            this.tb.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tb.Size = new System.Drawing.Size(210, 37);
            this.tb.TabIndex = 14;
            this.tb.TabStop = false;
            this.tb.Text = "Unidades";
            // 
            // btnGenerarUnidades
            // 
            this.btnGenerarUnidades.Location = new System.Drawing.Point(121, 15);
            this.btnGenerarUnidades.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnGenerarUnidades.Name = "btnGenerarUnidades";
            this.btnGenerarUnidades.Size = new System.Drawing.Size(79, 19);
            this.btnGenerarUnidades.TabIndex = 15;
            this.btnGenerarUnidades.Text = "Generar";
            this.btnGenerarUnidades.UseVisualStyleBackColor = true;
            this.btnGenerarUnidades.Click += new System.EventHandler(this.btnGenerarUnidades_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 18);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Cantidad:";
            // 
            // txtCantidadUnidades
            // 
            this.txtCantidadUnidades.Location = new System.Drawing.Point(60, 15);
            this.txtCantidadUnidades.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtCantidadUnidades.MaxLength = 12;
            this.txtCantidadUnidades.Name = "txtCantidadUnidades";
            this.txtCantidadUnidades.Size = new System.Drawing.Size(57, 20);
            this.txtCantidadUnidades.TabIndex = 14;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 176);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 10, 0);
            this.statusStrip1.Size = new System.Drawing.Size(413, 22);
            this.statusStrip1.TabIndex = 15;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(118, 17);
            this.lblStatus.Text = "toolStripStatusLabel1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Location = new System.Drawing.Point(187, 81);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(210, 37);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Recibos";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(7, 15);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(192, 19);
            this.button1.TabIndex = 15;
            this.button1.Text = "Generar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(413, 198);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tb);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.grpServicio);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form1";
            this.Text = "Simulador Autobuses";
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.cuandoDobleClick);
            this.grpServicio.ResumeLayout(false);
            this.grpServicio.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tb.ResumeLayout(false);
            this.tb.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
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
        private System.Windows.Forms.Button btnRandom;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
    }
}

