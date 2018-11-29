using System.Windows.Forms;

namespace TrabajoTitulacion.IU
{
    partial class CtrluContenido
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CtrluContenido));
            this.pctboxTipoContenido = new System.Windows.Forms.PictureBox();
            this.lblModificado = new System.Windows.Forms.Label();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.lnklblDescargar = new System.Windows.Forms.LinkLabel();
            this.lnklblPropiedades = new System.Windows.Forms.LinkLabel();
            this.fldbrwsrSeleccionarPath = new System.Windows.Forms.FolderBrowserDialog();
            this.lnklblNombre = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pctboxTipoContenido)).BeginInit();
            this.SuspendLayout();
            // 
            // pctboxTipoContenido
            // 
            this.pctboxTipoContenido.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pctboxTipoContenido.Image = ((System.Drawing.Image)(resources.GetObject("pctboxTipoContenido.Image")));
            this.pctboxTipoContenido.Location = new System.Drawing.Point(33, 18);
            this.pctboxTipoContenido.Name = "pctboxTipoContenido";
            this.pctboxTipoContenido.Size = new System.Drawing.Size(71, 67);
            this.pctboxTipoContenido.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pctboxTipoContenido.TabIndex = 0;
            this.pctboxTipoContenido.TabStop = false;
            // 
            // lblModificado
            // 
            this.lblModificado.AutoSize = true;
            this.lblModificado.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblModificado.ForeColor = System.Drawing.Color.Gray;
            this.lblModificado.Location = new System.Drawing.Point(132, 35);
            this.lblModificado.Name = "lblModificado";
            this.lblModificado.Size = new System.Drawing.Size(82, 14);
            this.lblModificado.TabIndex = 2;
            this.lblModificado.Text = "Modificado por";
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.AutoSize = true;
            this.lblDescripcion.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescripcion.Location = new System.Drawing.Point(132, 59);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(69, 14);
            this.lblDescripcion.TabIndex = 3;
            this.lblDescripcion.Text = "Descripción:";
            // 
            // lnklblDescargar
            // 
            this.lnklblDescargar.ActiveLinkColor = System.Drawing.Color.Green;
            this.lnklblDescargar.AutoSize = true;
            this.lnklblDescargar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lnklblDescargar.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnklblDescargar.ForeColor = System.Drawing.Color.Lime;
            this.lnklblDescargar.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lnklblDescargar.Location = new System.Drawing.Point(651, 18);
            this.lnklblDescargar.Name = "lnklblDescargar";
            this.lnklblDescargar.Size = new System.Drawing.Size(62, 15);
            this.lnklblDescargar.TabIndex = 4;
            this.lnklblDescargar.TabStop = true;
            this.lnklblDescargar.Text = "Descargar";
            this.lnklblDescargar.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lnklblDescargar.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnklblDescargar_LinkClicked);
            // 
            // lnklblPropiedades
            // 
            this.lnklblPropiedades.ActiveLinkColor = System.Drawing.Color.Green;
            this.lnklblPropiedades.AutoSize = true;
            this.lnklblPropiedades.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnklblPropiedades.ForeColor = System.Drawing.Color.Lime;
            this.lnklblPropiedades.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lnklblPropiedades.Location = new System.Drawing.Point(651, 35);
            this.lnklblPropiedades.Name = "lnklblPropiedades";
            this.lnklblPropiedades.Size = new System.Drawing.Size(95, 15);
            this.lnklblPropiedades.TabIndex = 5;
            this.lnklblPropiedades.TabStop = true;
            this.lnklblPropiedades.Text = "Ver propiedades";
            this.lnklblPropiedades.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnklblPropiedades_LinkClicked);
            // 
            // lnklblNombre
            // 
            this.lnklblNombre.ActiveLinkColor = System.Drawing.Color.Green;
            this.lnklblNombre.AutoSize = true;
            this.lnklblNombre.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lnklblNombre.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnklblNombre.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lnklblNombre.Location = new System.Drawing.Point(132, 18);
            this.lnklblNombre.Name = "lnklblNombre";
            this.lnklblNombre.Size = new System.Drawing.Size(53, 17);
            this.lnklblNombre.TabIndex = 6;
            this.lnklblNombre.TabStop = true;
            this.lnklblNombre.Text = "Nombre";
            this.lnklblNombre.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lnklblNombre.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnklblNombre_LinkClicked);
            // 
            // CtrluContenido
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.lnklblNombre);
            this.Controls.Add(this.lnklblPropiedades);
            this.Controls.Add(this.lnklblDescargar);
            this.Controls.Add(this.lblDescripcion);
            this.Controls.Add(this.lblModificado);
            this.Controls.Add(this.pctboxTipoContenido);
            this.Name = "CtrluContenido";
            this.Size = new System.Drawing.Size(786, 106);
            this.Load += new System.EventHandler(this.CtrluContenido_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.CtrluContenido_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.CtrluContenido_DragEnter);
            ((System.ComponentModel.ISupportInitialize)(this.pctboxTipoContenido)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pctboxTipoContenido;
        private Label lblModificado;
        private Label lblDescripcion;
        private LinkLabel lnklblDescargar;
        private LinkLabel lnklblPropiedades;
        private FolderBrowserDialog fldbrwsrSeleccionarPath;
        private LinkLabel lnklblNombre;

        public LinkLabel LnklblNombre { get => lnklblNombre; set => lnklblNombre = value; }
        public Label LblModificado { get => lblModificado; set => lblModificado = value; }
        public Label LblDescripcion { get => lblDescripcion; set => lblDescripcion = value; }
        public LinkLabel LnklblDescargar { get => lnklblDescargar; set => lnklblDescargar = value; }
        public LinkLabel LnklblPropiedades { get => lnklblPropiedades; set => lnklblPropiedades = value; }
    }
}
