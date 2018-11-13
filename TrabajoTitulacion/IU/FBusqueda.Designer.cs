namespace TrabajoTitulacion.IU
{
    partial class FBusqueda
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FBusqueda));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tblpanelContenedor = new System.Windows.Forms.TableLayoutPanel();
            this.flwlypanelNodosHijos = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlFiltros = new System.Windows.Forms.Panel();
            this.dtpkrValor = new System.Windows.Forms.DateTimePicker();
            this.cmbxValor = new System.Windows.Forms.ComboBox();
            this.lblNotFound = new System.Windows.Forms.Label();
            this.chkboxValor = new System.Windows.Forms.CheckBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.radioBtnAspecto = new System.Windows.Forms.RadioButton();
            this.radioBtnTipo = new System.Windows.Forms.RadioButton();
            this.cmbxPropiedad = new System.Windows.Forms.ComboBox();
            this.cmbxTipoAspecto = new System.Windows.Forms.ComboBox();
            this.cmbxModelo = new System.Windows.Forms.ComboBox();
            this.txtValor = new System.Windows.Forms.TextBox();
            this.lblTipoDato = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbxOperacion = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtValor2 = new System.Windows.Forms.TextBox();
            this.dtpkrValor2 = new System.Windows.Forms.DateTimePicker();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.tblpanelContenedor.SuspendLayout();
            this.pnlFiltros.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.pictureBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tblpanelContenedor, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(50, 50, 10, 10);
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.63158F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 87.36842F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 450);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // tblpanelContenedor
            // 
            this.tblpanelContenedor.ColumnCount = 2;
            this.tblpanelContenedor.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblpanelContenedor.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblpanelContenedor.Controls.Add(this.flwlypanelNodosHijos, 1, 0);
            this.tblpanelContenedor.Controls.Add(this.pnlFiltros, 0, 0);
            this.tblpanelContenedor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblpanelContenedor.Location = new System.Drawing.Point(53, 102);
            this.tblpanelContenedor.Name = "tblpanelContenedor";
            this.tblpanelContenedor.RowCount = 1;
            this.tblpanelContenedor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblpanelContenedor.Size = new System.Drawing.Size(734, 335);
            this.tblpanelContenedor.TabIndex = 1;
            // 
            // flwlypanelNodosHijos
            // 
            this.flwlypanelNodosHijos.AutoScroll = true;
            this.flwlypanelNodosHijos.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.flwlypanelNodosHijos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flwlypanelNodosHijos.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flwlypanelNodosHijos.Location = new System.Drawing.Point(370, 4);
            this.flwlypanelNodosHijos.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.flwlypanelNodosHijos.Name = "flwlypanelNodosHijos";
            this.flwlypanelNodosHijos.Size = new System.Drawing.Size(361, 327);
            this.flwlypanelNodosHijos.TabIndex = 1;
            this.flwlypanelNodosHijos.WrapContents = false;
            // 
            // pnlFiltros
            // 
            this.pnlFiltros.Controls.Add(this.panel1);
            this.pnlFiltros.Controls.Add(this.lblNotFound);
            this.pnlFiltros.Controls.Add(this.btnBuscar);
            this.pnlFiltros.Controls.Add(this.radioBtnAspecto);
            this.pnlFiltros.Controls.Add(this.radioBtnTipo);
            this.pnlFiltros.Controls.Add(this.cmbxPropiedad);
            this.pnlFiltros.Controls.Add(this.cmbxTipoAspecto);
            this.pnlFiltros.Controls.Add(this.cmbxModelo);
            this.pnlFiltros.Controls.Add(this.label3);
            this.pnlFiltros.Controls.Add(this.label2);
            this.pnlFiltros.Controls.Add(this.label1);
            this.pnlFiltros.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFiltros.Location = new System.Drawing.Point(3, 3);
            this.pnlFiltros.Name = "pnlFiltros";
            this.pnlFiltros.Size = new System.Drawing.Size(361, 329);
            this.pnlFiltros.TabIndex = 2;
            // 
            // dtpkrValor
            // 
            this.dtpkrValor.Location = new System.Drawing.Point(126, 15);
            this.dtpkrValor.Name = "dtpkrValor";
            this.dtpkrValor.Size = new System.Drawing.Size(200, 20);
            this.dtpkrValor.TabIndex = 9;
            this.dtpkrValor.Visible = false;
            // 
            // cmbxValor
            // 
            this.cmbxValor.DisplayMember = "ss";
            this.cmbxValor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbxValor.FormattingEnabled = true;
            this.cmbxValor.Items.AddRange(new object[] {
            "True",
            "False"});
            this.cmbxValor.Location = new System.Drawing.Point(126, 13);
            this.cmbxValor.Name = "cmbxValor";
            this.cmbxValor.Size = new System.Drawing.Size(121, 21);
            this.cmbxValor.TabIndex = 8;
            this.cmbxValor.Visible = false;
            // 
            // lblNotFound
            // 
            this.lblNotFound.AutoSize = true;
            this.lblNotFound.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNotFound.ForeColor = System.Drawing.Color.Red;
            this.lblNotFound.Location = new System.Drawing.Point(51, 314);
            this.lblNotFound.Name = "lblNotFound";
            this.lblNotFound.Size = new System.Drawing.Size(305, 14);
            this.lblNotFound.TabIndex = 7;
            this.lblNotFound.Text = "No se ha encontrado Nodos con las especificaciones dadas";
            this.lblNotFound.Visible = false;
            // 
            // chkboxValor
            // 
            this.chkboxValor.AutoSize = true;
            this.chkboxValor.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkboxValor.Location = new System.Drawing.Point(6, 14);
            this.chkboxValor.Name = "chkboxValor";
            this.chkboxValor.Size = new System.Drawing.Size(98, 18);
            this.chkboxValor.TabIndex = 6;
            this.chkboxValor.Text = "Valor a buscar";
            this.chkboxValor.UseVisualStyleBackColor = true;
            this.chkboxValor.CheckedChanged += new System.EventHandler(this.chkboxValor_CheckedChanged);
            // 
            // btnBuscar
            // 
            this.btnBuscar.Enabled = false;
            this.btnBuscar.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.Location = new System.Drawing.Point(54, 292);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(75, 23);
            this.btnBuscar.TabIndex = 5;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // radioBtnAspecto
            // 
            this.radioBtnAspecto.AutoSize = true;
            this.radioBtnAspecto.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioBtnAspecto.Location = new System.Drawing.Point(175, 65);
            this.radioBtnAspecto.Name = "radioBtnAspecto";
            this.radioBtnAspecto.Size = new System.Drawing.Size(66, 18);
            this.radioBtnAspecto.TabIndex = 4;
            this.radioBtnAspecto.Text = "Aspecto";
            this.radioBtnAspecto.UseVisualStyleBackColor = true;
            this.radioBtnAspecto.CheckedChanged += new System.EventHandler(this.radioBtnAspecto_CheckedChanged);
            // 
            // radioBtnTipo
            // 
            this.radioBtnTipo.AutoSize = true;
            this.radioBtnTipo.Checked = true;
            this.radioBtnTipo.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioBtnTipo.Location = new System.Drawing.Point(54, 65);
            this.radioBtnTipo.Name = "radioBtnTipo";
            this.radioBtnTipo.Size = new System.Drawing.Size(47, 18);
            this.radioBtnTipo.TabIndex = 4;
            this.radioBtnTipo.TabStop = true;
            this.radioBtnTipo.Text = "Tipo";
            this.radioBtnTipo.UseVisualStyleBackColor = true;
            this.radioBtnTipo.CheckedChanged += new System.EventHandler(this.radioBtnTipo_CheckedChanged);
            // 
            // cmbxPropiedad
            // 
            this.cmbxPropiedad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbxPropiedad.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbxPropiedad.FormattingEnabled = true;
            this.cmbxPropiedad.Location = new System.Drawing.Point(139, 137);
            this.cmbxPropiedad.Name = "cmbxPropiedad";
            this.cmbxPropiedad.Size = new System.Drawing.Size(121, 22);
            this.cmbxPropiedad.TabIndex = 3;
            this.cmbxPropiedad.SelectedIndexChanged += new System.EventHandler(this.cmbxPropiedad_SelectedIndexChanged);
            // 
            // cmbxTipoAspecto
            // 
            this.cmbxTipoAspecto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbxTipoAspecto.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbxTipoAspecto.FormattingEnabled = true;
            this.cmbxTipoAspecto.Location = new System.Drawing.Point(139, 100);
            this.cmbxTipoAspecto.Name = "cmbxTipoAspecto";
            this.cmbxTipoAspecto.Size = new System.Drawing.Size(121, 22);
            this.cmbxTipoAspecto.TabIndex = 3;
            this.cmbxTipoAspecto.SelectedIndexChanged += new System.EventHandler(this.cmbxTipoAspecto_SelectedIndexChanged);
            // 
            // cmbxModelo
            // 
            this.cmbxModelo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbxModelo.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbxModelo.FormattingEnabled = true;
            this.cmbxModelo.Location = new System.Drawing.Point(139, 33);
            this.cmbxModelo.Name = "cmbxModelo";
            this.cmbxModelo.Size = new System.Drawing.Size(121, 22);
            this.cmbxModelo.TabIndex = 2;
            this.cmbxModelo.SelectedIndexChanged += new System.EventHandler(this.cmbxModelo_SelectedIndexChanged);
            // 
            // txtValor
            // 
            this.txtValor.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtValor.Location = new System.Drawing.Point(126, 14);
            this.txtValor.Name = "txtValor";
            this.txtValor.Size = new System.Drawing.Size(121, 21);
            this.txtValor.TabIndex = 1;
            this.txtValor.Visible = false;
            // 
            // lblTipoDato
            // 
            this.lblTipoDato.AutoSize = true;
            this.lblTipoDato.Font = new System.Drawing.Font("Roboto", 6.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTipoDato.Location = new System.Drawing.Point(283, 0);
            this.lblTipoDato.Name = "lblTipoDato";
            this.lblTipoDato.Size = new System.Drawing.Size(54, 11);
            this.lblTipoDato.TabIndex = 0;
            this.lblTipoDato.Text = "Tipo de dato";
            this.lblTipoDato.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(45, 137);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 14);
            this.label3.TabIndex = 0;
            this.label3.Text = "Propiedad";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(45, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 14);
            this.label2.TabIndex = 0;
            this.label2.Text = "Tipo/Aspecto";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(45, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Modelo";
            // 
            // cmbxOperacion
            // 
            this.cmbxOperacion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbxOperacion.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbxOperacion.FormattingEnabled = true;
            this.cmbxOperacion.Location = new System.Drawing.Point(123, 44);
            this.cmbxOperacion.Name = "cmbxOperacion";
            this.cmbxOperacion.Size = new System.Drawing.Size(121, 22);
            this.cmbxOperacion.TabIndex = 3;
            this.cmbxOperacion.Visible = false;
            this.cmbxOperacion.SelectedIndexChanged += new System.EventHandler(this.cmbxOperacion_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.dtpkrValor2);
            this.panel1.Controls.Add(this.dtpkrValor);
            this.panel1.Controls.Add(this.chkboxValor);
            this.panel1.Controls.Add(this.cmbxValor);
            this.panel1.Controls.Add(this.cmbxOperacion);
            this.panel1.Controls.Add(this.txtValor2);
            this.panel1.Controls.Add(this.lblTipoDato);
            this.panel1.Controls.Add(this.txtValor);
            this.panel1.Location = new System.Drawing.Point(16, 165);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(340, 109);
            this.panel1.TabIndex = 0;
            // 
            // txtValor2
            // 
            this.txtValor2.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtValor2.Location = new System.Drawing.Point(123, 77);
            this.txtValor2.Name = "txtValor2";
            this.txtValor2.Size = new System.Drawing.Size(121, 21);
            this.txtValor2.TabIndex = 1;
            this.txtValor2.Visible = false;
            // 
            // dtpkrValor2
            // 
            this.dtpkrValor2.Location = new System.Drawing.Point(123, 77);
            this.dtpkrValor2.Name = "dtpkrValor2";
            this.dtpkrValor2.Size = new System.Drawing.Size(200, 20);
            this.dtpkrValor2.TabIndex = 9;
            this.dtpkrValor2.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(53, 54);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Padding = new System.Windows.Forms.Padding(20, 10, 0, 0);
            this.pictureBox1.Size = new System.Drawing.Size(734, 41);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // FBusqueda
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FBusqueda";
            this.Load += new System.EventHandler(this.FBusqueda_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tblpanelContenedor.ResumeLayout(false);
            this.pnlFiltros.ResumeLayout(false);
            this.pnlFiltros.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tblpanelContenedor;
        private System.Windows.Forms.FlowLayoutPanel flwlypanelNodosHijos;
        private System.Windows.Forms.Panel pnlFiltros;
        private System.Windows.Forms.ComboBox cmbxPropiedad;
        private System.Windows.Forms.ComboBox cmbxTipoAspecto;
        private System.Windows.Forms.ComboBox cmbxModelo;
        private System.Windows.Forms.TextBox txtValor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radioBtnAspecto;
        private System.Windows.Forms.RadioButton radioBtnTipo;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Label lblTipoDato;
        private System.Windows.Forms.CheckBox chkboxValor;
        private System.Windows.Forms.Label lblNotFound;
        private System.Windows.Forms.ComboBox cmbxValor;
        private System.Windows.Forms.DateTimePicker dtpkrValor;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cmbxOperacion;
        private System.Windows.Forms.DateTimePicker dtpkrValor2;
        private System.Windows.Forms.TextBox txtValor2;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}