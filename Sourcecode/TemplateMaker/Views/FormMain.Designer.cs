
namespace TemplateMaker.Viewer
{
    partial class FormMain
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
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxTemplate = new System.Windows.Forms.ComboBox();
            this.richTextBoxDescription = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.richTextBoxParametersJson = new System.Windows.Forms.RichTextBox();
            this.buttonExecute = new System.Windows.Forms.Button();
            this.smartPropertyGrid = new SmartProperty.SmartPropertyGrid();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Template:";
            // 
            // comboBoxTemplate
            // 
            this.comboBoxTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxTemplate.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxTemplate.FormattingEnabled = true;
            this.comboBoxTemplate.Location = new System.Drawing.Point(15, 26);
            this.comboBoxTemplate.Margin = new System.Windows.Forms.Padding(6);
            this.comboBoxTemplate.Name = "comboBoxTemplate";
            this.comboBoxTemplate.Size = new System.Drawing.Size(825, 22);
            this.comboBoxTemplate.TabIndex = 1;
            this.comboBoxTemplate.SelectedIndexChanged += new System.EventHandler(this.comboBoxTemplate_SelectedIndexChanged);
            // 
            // richTextBoxDescription
            // 
            this.richTextBoxDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.richTextBoxDescription.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxDescription.Location = new System.Drawing.Point(15, 73);
            this.richTextBoxDescription.Margin = new System.Windows.Forms.Padding(6);
            this.richTextBoxDescription.Name = "richTextBoxDescription";
            this.richTextBoxDescription.Size = new System.Drawing.Size(825, 57);
            this.richTextBoxDescription.TabIndex = 4;
            this.richTextBoxDescription.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 59);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 14);
            this.label3.TabIndex = 5;
            this.label3.Text = "Description:";
            // 
            // richTextBoxParametersJson
            // 
            this.richTextBoxParametersJson.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxParametersJson.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.richTextBoxParametersJson.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxParametersJson.Location = new System.Drawing.Point(431, 144);
            this.richTextBoxParametersJson.Margin = new System.Windows.Forms.Padding(6);
            this.richTextBoxParametersJson.Name = "richTextBoxParametersJson";
            this.richTextBoxParametersJson.Size = new System.Drawing.Size(409, 329);
            this.richTextBoxParametersJson.TabIndex = 7;
            this.richTextBoxParametersJson.Text = "";
            // 
            // buttonExecute
            // 
            this.buttonExecute.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonExecute.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonExecute.Location = new System.Drawing.Point(777, 483);
            this.buttonExecute.Margin = new System.Windows.Forms.Padding(4);
            this.buttonExecute.Name = "buttonExecute";
            this.buttonExecute.Size = new System.Drawing.Size(65, 23);
            this.buttonExecute.TabIndex = 8;
            this.buttonExecute.Text = "Execute";
            this.buttonExecute.UseVisualStyleBackColor = true;
            this.buttonExecute.Click += new System.EventHandler(this.buttonExecute_Click);
            // 
            // smartPropertyGrid
            // 
            this.smartPropertyGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.smartPropertyGrid.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.smartPropertyGrid.Location = new System.Drawing.Point(15, 144);
            this.smartPropertyGrid.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.smartPropertyGrid.Name = "smartPropertyGrid";
            this.smartPropertyGrid.Size = new System.Drawing.Size(408, 362);
            this.smartPropertyGrid.TabIndex = 9;
            this.smartPropertyGrid.PropertyValueChanged += new SmartProperty.SmartPropertyGridOnPropertyValueChangeHandler(this.smartPropertyGrid_PropertyValueChanged);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(855, 519);
            this.Controls.Add(this.smartPropertyGrid);
            this.Controls.Add(this.buttonExecute);
            this.Controls.Add(this.richTextBoxParametersJson);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.richTextBoxDescription);
            this.Controls.Add(this.comboBoxTemplate);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "FormMain";
            this.Text = "Template Maker";
            this.Shown += new System.EventHandler(this.FormMain_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxTemplate;
        private System.Windows.Forms.RichTextBox richTextBoxDescription;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox richTextBoxParametersJson;
        private System.Windows.Forms.Button buttonExecute;
        private SmartProperty.SmartPropertyGrid smartPropertyGrid;
    }
}