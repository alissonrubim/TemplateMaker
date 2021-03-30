using SchemaProcessor;
using SchemaProcessor.SchemaProviders;
using System;
using System.Windows.Forms;
using TemplateMaker.Viewer.Helpers.SchemaProvider;
using TemplateMaker.Viewer.Helpers.SchemaProvider.Exceptions;
using TemplateMaker.Viewer.Types;
using SmartProperty.Editors;

namespace TemplateMaker.Views.PropertyEditors
{
    public partial class ColumnInfoPropertyEditor : UserControl, ISmartPropertyEditor
    {
        public ColumnInfoType Value { get; set; }
        public ColumnInfoPropertyEditor()
        {
            InitializeComponent();
        }

        public void SetValue(object value)
        {
            Value = value as ColumnInfoType;
            LoadValue();
        }

        public object GetValue()
        {
            Value.Name = textBoxName.Text;
            Value.Type = textBoxType.Text;
            return Value;
        }

        private void LoadValue()
        {
            if (Value == null)
                Value = new ColumnInfoType();
            textBoxName.Text = Value.Name;
            textBoxType.Text = Value.Type;
        }
    }
}
