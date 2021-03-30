using System;
using System.Drawing;
using System.Windows.Forms;

namespace SmartProperty.Editors.Forms
{
    public partial class FormSmartPropertyEditor : Form, IFormSmartPropertyEditor
    {
        private UserControl Editor;
        public FormSmartPropertyEditor()
        {
            InitializeComponent();
        }

        public void DefinePropertyEditor(Type editorType)
        {
            if (editorType.IsSubclassOf(typeof(UserControl)))
            {
                Editor = (UserControl)Activator.CreateInstance(editorType);
                this.ClientSize = new Size(Editor.Width, Editor.Height + panelBottom.Height);
                Editor.Parent = this.panelRenderEditor;
                Editor.Dock = DockStyle.Fill;
            }
        }

        public void SetValue(object value)
        {
            (Editor as ISmartPropertyEditor).SetValue(value);
        }

        public object GetValue()
        {
            return (Editor as ISmartPropertyEditor).GetValue();
        }
    }
}
