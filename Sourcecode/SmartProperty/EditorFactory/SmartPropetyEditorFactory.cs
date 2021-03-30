using SmartProperty.Editors;
using SmartProperty.Editors.Forms;
using System;
using System.Windows.Forms;

namespace SmartProperty.EditorFactory
{
    public delegate void SmartPropetyEditorFactoryOnEndEditHanlder(IProperty property, object value);
    public class SmartPropetyEditorFactory
    {
        public static object OpenEditor(Type editorType, object value, bool isCollection)
        {
            IFormSmartPropertyEditor formEditor;
            if (isCollection)
                formEditor = new FormSmartPropertyCollectionEditor();
            else
                formEditor = new FormSmartPropertyEditor();

            formEditor.DefinePropertyEditor(editorType);
            formEditor.SetValue(value);
            if ((formEditor as Form).ShowDialog() == DialogResult.OK)
                value = formEditor.GetValue();
            return value;
        }
    }
}
