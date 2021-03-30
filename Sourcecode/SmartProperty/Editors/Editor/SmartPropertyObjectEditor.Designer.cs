
namespace SmartProperty.Editors.Editor
{
    partial class SmartPropertyObjectEditor
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.smartPropertyGrid = new SmartProperty.SmartPropertyGrid();
            this.SuspendLayout();
            // 
            // smartPropertyGrid
            // 
            this.smartPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartPropertyGrid.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.smartPropertyGrid.Location = new System.Drawing.Point(0, 0);
            this.smartPropertyGrid.Name = "smartPropertyGrid";
            this.smartPropertyGrid.Size = new System.Drawing.Size(547, 301);
            this.smartPropertyGrid.TabIndex = 0;
            // 
            // SmartPropertyObjectEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.smartPropertyGrid);
            this.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "SmartPropertyObjectEditor";
            this.Size = new System.Drawing.Size(547, 301);
            this.ResumeLayout(false);

        }

        #endregion

        private SmartPropertyGrid smartPropertyGrid;
    }
}
