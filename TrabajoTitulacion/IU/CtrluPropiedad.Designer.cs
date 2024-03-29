﻿using System.Windows.Forms;

namespace TrabajoTitulacion.IU
{
    partial class CtrluPropiedad
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
            this.lblNombrePropiedad = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.txtValorPropiedad = new System.Windows.Forms.TextBox();
            this.dtpValorPropiedad = new System.Windows.Forms.DateTimePicker();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 351F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.lblNombrePropiedad, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(484, 39);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lblNombrePropiedad
            // 
            this.lblNombrePropiedad.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNombrePropiedad.AutoSize = true;
            this.lblNombrePropiedad.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombrePropiedad.Location = new System.Drawing.Point(3, 0);
            this.lblNombrePropiedad.Name = "lblNombrePropiedad";
            this.lblNombrePropiedad.Size = new System.Drawing.Size(107, 14);
            this.lblNombrePropiedad.TabIndex = 0;
            this.lblNombrePropiedad.Text = "Nombre Propiedad:";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.txtValorPropiedad);
            this.flowLayoutPanel1.Controls.Add(this.dtpValorPropiedad);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(116, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(345, 33);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // txtValorPropiedad
            // 
            this.txtValorPropiedad.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtValorPropiedad.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtValorPropiedad.Enabled = false;
            this.txtValorPropiedad.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtValorPropiedad.Location = new System.Drawing.Point(3, 3);
            this.txtValorPropiedad.Name = "txtValorPropiedad";
            this.txtValorPropiedad.Size = new System.Drawing.Size(331, 21);
            this.txtValorPropiedad.TabIndex = 1;
            // 
            // dtpValorPropiedad
            // 
            this.dtpValorPropiedad.Enabled = false;
            this.dtpValorPropiedad.Location = new System.Drawing.Point(3, 30);
            this.dtpValorPropiedad.Name = "dtpValorPropiedad";
            this.dtpValorPropiedad.Size = new System.Drawing.Size(345, 20);
            this.dtpValorPropiedad.TabIndex = 2;
            this.dtpValorPropiedad.Visible = false;
            this.dtpValorPropiedad.ValueChanged += new System.EventHandler(this.dtpValorPropiedad_ValueChanged);
            // 
            // CtrluPropiedad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "CtrluPropiedad";
            this.Size = new System.Drawing.Size(484, 39);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblNombrePropiedad;
        private System.Windows.Forms.TextBox txtValorPropiedad;
        private FlowLayoutPanel flowLayoutPanel1;
        private DateTimePicker dtpValorPropiedad;

        public Label LblNombrePropiedad { get => lblNombrePropiedad; set => lblNombrePropiedad = value; }
        public TextBox TxtValorPropiedad { get => txtValorPropiedad; set => txtValorPropiedad = value; }
        public DateTimePicker DtpValorPropiedad { get => dtpValorPropiedad; set => dtpValorPropiedad = value; }
    }
}
