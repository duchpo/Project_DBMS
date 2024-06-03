namespace sql
{
    partial class fbangPhanCa
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
            this.gridV_BangPhanCaNV = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridV_BangPhanCaNV)).BeginInit();
            this.SuspendLayout();
            // 
            // gridV_BangPhanCaNV
            // 
            this.gridV_BangPhanCaNV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridV_BangPhanCaNV.Location = new System.Drawing.Point(12, 116);
            this.gridV_BangPhanCaNV.Name = "gridV_BangPhanCaNV";
            this.gridV_BangPhanCaNV.RowHeadersWidth = 51;
            this.gridV_BangPhanCaNV.RowTemplate.Height = 24;
            this.gridV_BangPhanCaNV.Size = new System.Drawing.Size(977, 194);
            this.gridV_BangPhanCaNV.TabIndex = 0;
            // 
            // fbangPhanCa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1064, 450);
            this.Controls.Add(this.gridV_BangPhanCaNV);
            this.Name = "fbangPhanCa";
            this.Text = "fbangPhanCa";
            this.Load += new System.EventHandler(this.fbangPhanCa_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridV_BangPhanCaNV)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView gridV_BangPhanCaNV;
    }
}