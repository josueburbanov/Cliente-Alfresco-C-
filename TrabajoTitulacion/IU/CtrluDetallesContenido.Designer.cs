namespace TrabajoTitulacion.IU
{
    partial class CtrluDetallesContenido
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CtrluDetallesContenido));
            this.pctboxTipoContenido = new System.Windows.Forms.PictureBox();
            this.lnklblNombre = new System.Windows.Forms.LinkLabel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.lblModificado = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.pctboxTipoContenido)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pctboxTipoContenido
            // 
            this.pctboxTipoContenido.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pctboxTipoContenido.Image = ((System.Drawing.Image)(resources.GetObject("pctboxTipoContenido.Image")));
            this.pctboxTipoContenido.Location = new System.Drawing.Point(3, 3);
            this.pctboxTipoContenido.Name = "pctboxTipoContenido";
            this.pctboxTipoContenido.Size = new System.Drawing.Size(27, 23);
            this.pctboxTipoContenido.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pctboxTipoContenido.TabIndex = 1;
            this.pctboxTipoContenido.TabStop = false;
            // 
            // lnklblNombre
            // 
            this.lnklblNombre.ActiveLinkColor = System.Drawing.Color.Green;
            this.lnklblNombre.AutoSize = true;
            this.lnklblNombre.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lnklblNombre.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnklblNombre.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lnklblNombre.Location = new System.Drawing.Point(36, 0);
            this.lnklblNombre.Name = "lnklblNombre";
            this.lnklblNombre.Size = new System.Drawing.Size(53, 17);
            this.lnklblNombre.TabIndex = 7;
            this.lnklblNombre.TabStop = true;
            this.lnklblNombre.Text = "Nombre";
            this.lnklblNombre.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.pctboxTipoContenido);
            this.flowLayoutPanel1.Controls.Add(this.lnklblNombre);
            this.flowLayoutPanel1.Controls.Add(this.lblModificado);
            this.flowLayoutPanel1.Controls.Add(this.lblVersion);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(660, 34);
            this.flowLayoutPanel1.TabIndex = 8;
            // 
            // lblModificado
            // 
            this.lblModificado.AutoSize = true;
            this.lblModificado.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblModificado.ForeColor = System.Drawing.Color.Gray;
            this.lblModificado.Location = new System.Drawing.Point(95, 0);
            this.lblModificado.Name = "lblModificado";
            this.lblModificado.Size = new System.Drawing.Size(82, 14);
            this.lblModificado.TabIndex = 9;
            this.lblModificado.Text = "Modificado por";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersion.ForeColor = System.Drawing.Color.Gray;
            this.lblVersion.Location = new System.Drawing.Point(183, 0);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(45, 14);
            this.lblVersion.TabIndex = 9;
            this.lblVersion.Text = "Versión";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel2, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(666, 403);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Gray;
            this.label1.Location = new System.Drawing.Point(3, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 14);
            this.label1.TabIndex = 9;
            this.label1.Text = "Propiedades";
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 83);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(660, 317);
            this.flowLayoutPanel2.TabIndex = 10;
            // 
            // CtrluDetallesContenido
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "CtrluDetallesContenido";
            this.Size = new System.Drawing.Size(666, 403);
            ((System.ComponentModel.ISupportInitialize)(this.pctboxTipoContenido)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pctboxTipoContenido;
        private System.Windows.Forms.LinkLabel lnklblNombre;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label lblModificado;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
    }
}
