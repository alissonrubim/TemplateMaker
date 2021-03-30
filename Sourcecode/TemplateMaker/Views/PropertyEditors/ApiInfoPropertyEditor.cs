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
    public partial class ApiInfoPropertyEditor : UserControl, ISmartPropertyEditor
    {
        public ApiInfoType Value { get; set; }
        public ApiInfoPropertyEditor()
        {
            InitializeComponent();
        }

        public void SetValue(object value)
        {
            Value = value as ApiInfoType;
            LoadValue();
        }

        public object GetValue()
        {
            Value.Name = textBoxName.Text;
            return Value;
        }

        private void LoadValue()
        {
            if (Value == null)
                Value = new ApiInfoType();
            textBoxName.Text = Value.Name;
        }
    }
}
