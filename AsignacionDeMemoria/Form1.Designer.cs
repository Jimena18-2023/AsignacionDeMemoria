namespace AsignacionDeMemoria
{
    partial class Form1
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

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txtInitialMemory = new System.Windows.Forms.TextBox();
            this.txtProcessName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtProcessSize = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnInitMemory = new System.Windows.Forms.Button();
            this.btnAllocate = new System.Windows.Forms.Button();
            this.btnDeallocate = new System.Windows.Forms.Button();
            this.dgvMemory = new System.Windows.Forms.DataGridView();
            this.dgvProcesos = new System.Windows.Forms.DataGridView();
            this.cmbEstrategia = new System.Windows.Forms.ComboBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.txtBloques = new System.Windows.Forms.TextBox();
            this.lblBloques = new System.Windows.Forms.Label();
            this.pnlGrafica = new System.Windows.Forms.Panel();
            this.btnModificar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMemory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProcesos)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(21, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Memoria Total:";
            // 
            // txtInitialMemory
            // 
            this.txtInitialMemory.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInitialMemory.Location = new System.Drawing.Point(167, 21);
            this.txtInitialMemory.Name = "txtInitialMemory";
            this.txtInitialMemory.Size = new System.Drawing.Size(100, 31);
            this.txtInitialMemory.TabIndex = 1;
            // 
            // txtProcessName
            // 
            this.txtProcessName.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProcessName.Location = new System.Drawing.Point(614, 22);
            this.txtProcessName.Name = "txtProcessName";
            this.txtProcessName.Size = new System.Drawing.Size(100, 31);
            this.txtProcessName.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(529, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "Proceso:";
            // 
            // txtProcessSize
            // 
            this.txtProcessSize.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProcessSize.Location = new System.Drawing.Point(831, 21);
            this.txtProcessSize.Name = "txtProcessSize";
            this.txtProcessSize.Size = new System.Drawing.Size(100, 31);
            this.txtProcessSize.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(747, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 25);
            this.label3.TabIndex = 4;
            this.label3.Text = "Tamaño:";
            // 
            // btnInitMemory
            // 
            this.btnInitMemory.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInitMemory.Location = new System.Drawing.Point(26, 73);
            this.btnInitMemory.Name = "btnInitMemory";
            this.btnInitMemory.Size = new System.Drawing.Size(165, 35);
            this.btnInitMemory.TabIndex = 6;
            this.btnInitMemory.Text = "Iniciar Memoria";
            this.btnInitMemory.UseVisualStyleBackColor = true;
            this.btnInitMemory.Click += new System.EventHandler(this.btnInitMemory_Click);
            // 
            // btnAllocate
            // 
            this.btnAllocate.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAllocate.Location = new System.Drawing.Point(271, 73);
            this.btnAllocate.Name = "btnAllocate";
            this.btnAllocate.Size = new System.Drawing.Size(165, 35);
            this.btnAllocate.TabIndex = 7;
            this.btnAllocate.Text = "Asignar Proceso";
            this.btnAllocate.UseVisualStyleBackColor = true;
            this.btnAllocate.Click += new System.EventHandler(this.btnAllocate_Click);
            // 
            // btnDeallocate
            // 
            this.btnDeallocate.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeallocate.Location = new System.Drawing.Point(515, 73);
            this.btnDeallocate.Name = "btnDeallocate";
            this.btnDeallocate.Size = new System.Drawing.Size(165, 35);
            this.btnDeallocate.TabIndex = 8;
            this.btnDeallocate.Text = "Liberar Proceso";
            this.btnDeallocate.UseVisualStyleBackColor = true;
            this.btnDeallocate.Click += new System.EventHandler(this.btnDeallocate_Click);
            // 
            // dgvMemory
            // 
            this.dgvMemory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMemory.Location = new System.Drawing.Point(26, 142);
            this.dgvMemory.Name = "dgvMemory";
            this.dgvMemory.RowHeadersWidth = 51;
            this.dgvMemory.RowTemplate.Height = 24;
            this.dgvMemory.Size = new System.Drawing.Size(703, 350);
            this.dgvMemory.TabIndex = 9;
            this.dgvMemory.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // dgvProcesos
            // 
            this.dgvProcesos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProcesos.Location = new System.Drawing.Point(763, 142);
            this.dgvProcesos.Name = "dgvProcesos";
            this.dgvProcesos.RowHeadersWidth = 51;
            this.dgvProcesos.RowTemplate.Height = 24;
            this.dgvProcesos.Size = new System.Drawing.Size(263, 350);
            this.dgvProcesos.TabIndex = 10;
            this.dgvProcesos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProcesses_CellContentClick);
            // 
            // cmbEstrategia
            // 
            this.cmbEstrategia.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbEstrategia.FormattingEnabled = true;
            this.cmbEstrategia.Location = new System.Drawing.Point(720, 75);
            this.cmbEstrategia.Name = "cmbEstrategia";
            this.cmbEstrategia.Size = new System.Drawing.Size(257, 33);
            this.cmbEstrategia.TabIndex = 11;
            this.cmbEstrategia.SelectedIndexChanged += new System.EventHandler(this.cmbEstrategia_SelectedIndexChanged);
            // 
            // btnReset
            // 
            this.btnReset.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.Location = new System.Drawing.Point(1004, 528);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(127, 35);
            this.btnReset.TabIndex = 12;
            this.btnReset.Text = "Reiniciar";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // txtBloques
            // 
            this.txtBloques.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBloques.Location = new System.Drawing.Point(393, 21);
            this.txtBloques.Name = "txtBloques";
            this.txtBloques.Size = new System.Drawing.Size(100, 31);
            this.txtBloques.TabIndex = 14;
            // 
            // lblBloques
            // 
            this.lblBloques.AutoSize = true;
            this.lblBloques.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBloques.Location = new System.Drawing.Point(308, 24);
            this.lblBloques.Name = "lblBloques";
            this.lblBloques.Size = new System.Drawing.Size(79, 25);
            this.lblBloques.TabIndex = 13;
            this.lblBloques.Text = "Bloques:";
            // 
            // pnlGrafica
            // 
            this.pnlGrafica.Location = new System.Drawing.Point(26, 528);
            this.pnlGrafica.Name = "pnlGrafica";
            this.pnlGrafica.Size = new System.Drawing.Size(951, 110);
            this.pnlGrafica.TabIndex = 15;
            // 
            // btnModificar
            // 
            this.btnModificar.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModificar.Location = new System.Drawing.Point(1004, 593);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(127, 35);
            this.btnModificar.TabIndex = 17;
            this.btnModificar.Text = "Modificar";
            this.btnModificar.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1165, 662);
            this.Controls.Add(this.btnModificar);
            this.Controls.Add(this.pnlGrafica);
            this.Controls.Add(this.txtBloques);
            this.Controls.Add(this.lblBloques);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.cmbEstrategia);
            this.Controls.Add(this.dgvProcesos);
            this.Controls.Add(this.dgvMemory);
            this.Controls.Add(this.btnDeallocate);
            this.Controls.Add(this.btnAllocate);
            this.Controls.Add(this.btnInitMemory);
            this.Controls.Add(this.txtProcessSize);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtProcessName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtInitialMemory);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dgvMemory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProcesos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtInitialMemory;
        private System.Windows.Forms.TextBox txtProcessName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtProcessSize;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnInitMemory;
        private System.Windows.Forms.Button btnAllocate;
        private System.Windows.Forms.Button btnDeallocate;
        private System.Windows.Forms.DataGridView dgvMemory;
        private System.Windows.Forms.DataGridView dgvProcesos;
        private System.Windows.Forms.ComboBox cmbEstrategia;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.TextBox txtBloques;
        private System.Windows.Forms.Label lblBloques;
        private System.Windows.Forms.Panel pnlGrafica;
        private System.Windows.Forms.Button btnModificar;
    }
}

