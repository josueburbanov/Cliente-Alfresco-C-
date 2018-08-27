using System.Windows.Forms;

namespace TrabajoTitulacion.IU
{
    partial class CtrluModelo
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

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbAccionesModelo = new System.Windows.Forms.ComboBox();
            this.btnNombreModelo = new System.Windows.Forms.Button();
            this.lblEstadoModelo = new System.Windows.Forms.Label();
            this.lblEspacioModelo = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.label4, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cmbAccionesModelo, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnNombreModelo, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblEstadoModelo, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblEspacioModelo, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 44F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 56F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(950, 51);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(714, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(233, 22);
            this.label4.TabIndex = 3;
            this.label4.Text = "Acciones";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(477, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(231, 22);
            this.label3.TabIndex = 2;
            this.label3.Text = "Estado";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(240, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(231, 22);
            this.label2.TabIndex = 1;
            this.label2.Text = "Espacio de nombres";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(231, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nombre";
            // 
            // cmbAccionesModelo
            // 
            this.cmbAccionesModelo.BackColor = System.Drawing.Color.White;
            this.cmbAccionesModelo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbAccionesModelo.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbAccionesModelo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAccionesModelo.FormattingEnabled = true;
            this.cmbAccionesModelo.Items.AddRange(new object[] {
            "Activar",
            "Editar",
            "Eliminar"});
            this.cmbAccionesModelo.Location = new System.Drawing.Point(714, 25);
            this.cmbAccionesModelo.Name = "cmbAccionesModelo";
            this.cmbAccionesModelo.Size = new System.Drawing.Size(233, 22);
            this.cmbAccionesModelo.TabIndex = 8;
            // 
            // btnNombreModelo
            // 
            this.btnNombreModelo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnNombreModelo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnNombreModelo.Location = new System.Drawing.Point(3, 25);
            this.btnNombreModelo.Name = "btnNombreModelo";
            this.btnNombreModelo.Size = new System.Drawing.Size(231, 23);
            this.btnNombreModelo.TabIndex = 9;
            this.btnNombreModelo.Text = "Nombre Modelo";
            this.btnNombreModelo.UseVisualStyleBackColor = true;
            // 
            // lblEstadoModelo
            // 
            this.lblEstadoModelo.AutoSize = true;
            this.lblEstadoModelo.Location = new System.Drawing.Point(477, 22);
            this.lblEstadoModelo.Name = "lblEstadoModelo";
            this.lblEstadoModelo.Padding = new System.Windows.Forms.Padding(5, 15, 0, 0);
            this.lblEstadoModelo.Size = new System.Drawing.Size(86, 29);
            this.lblEstadoModelo.TabIndex = 5;
            this.lblEstadoModelo.Text = "Estado Modelo";
            // 
            // lblEspacioModelo
            // 
            this.lblEspacioModelo.AutoSize = true;
            this.lblEspacioModelo.Location = new System.Drawing.Point(240, 22);
            this.lblEspacioModelo.Name = "lblEspacioModelo";
            this.lblEspacioModelo.Padding = new System.Windows.Forms.Padding(5, 15, 0, 0);
            this.lblEspacioModelo.Size = new System.Drawing.Size(91, 29);
            this.lblEspacioModelo.TabIndex = 7;
            this.lblEspacioModelo.Text = "Espacio Modelo";
            // 
            // CtrluModelo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "CtrluModelo";
            this.Size = new System.Drawing.Size(950, 51);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblEstadoModelo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblEspacioModelo;
        private System.Windows.Forms.ComboBox cmbAccionesModelo;
        private System.Windows.Forms.Button btnNombreModelo;
    }
}
