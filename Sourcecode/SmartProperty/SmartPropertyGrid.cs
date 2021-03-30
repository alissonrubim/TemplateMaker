using SmartProperty.EditorFactory;
using SmartProperty.Editors.Editor;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SmartProperty
{
    public delegate void SmartPropertyGridOnPropertyValueChangeHandler(IProperty property);
    public partial class SmartPropertyGrid : UserControl
    {
        private IList<IProperty> Properties;
        public event SmartPropertyGridOnPropertyValueChangeHandler PropertyValueChanged;


        public SmartPropertyGrid()
        {
            InitializeComponent();
        }

        public void LoadProperties(IList<IProperty> properties)
        {
            Properties = properties;
            dataGridView.Rows.Clear();
            foreach (IProperty property in Properties) {
                int index = dataGridView.Rows.Add();
                dataGridView.Rows[index].Cells["ColumnPropertyName"].Value = property.GetName();
                dataGridView.Rows[index].Cells["ColumnPropertyValue"].Value = property.GetDisplayValue();
            }
            SetPropertyInfo(null);        
        }

        private void SetPropertyInfo(IProperty property)
        {
            if (property != null)
            {
                labelType.Text = property.GetValueType().ToString();
                labelDescription.Text = property.GetDescription();
            }
            else
            {
                labelType.Text = string.Empty;
                labelDescription.Text = string.Empty;
            }
        }

        private void dataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            IProperty property = Properties[e.RowIndex];
            if (property.GetValueType() == typeof(string))
                property.SetValue(dataGridView.Rows[e.RowIndex].Cells["ColumnPropertyValue"].Value);
            PropertyValueChanged?.Invoke(property);
        }

        private void dataGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            IProperty property = Properties[e.RowIndex];

            if (property.GetIsCollection() || property.GetIsParameterObject() || property.GetCustomEditorType() != null)
            {
                e.Cancel = true;

                Type editorType;
                if (property.GetIsParameterObject())
                    editorType = typeof(SmartPropertyObjectEditor);
                else
                    editorType = property.GetCustomEditorType();

                object value = SmartPropetyEditorFactory.OpenEditor(editorType, property.GetValue(), property.GetIsCollection());
                property.SetValue(value);
                dataGridView.Rows[Properties.IndexOf(property)].Cells["ColumnPropertyValue"].Value = property.GetDisplayValue();
                PropertyValueChanged?.Invoke(property);
            }
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SetPropertyInfo(e.RowIndex > -1 ? Properties[e.RowIndex] : null);
        }
    }
}
