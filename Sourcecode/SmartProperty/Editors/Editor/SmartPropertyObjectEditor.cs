using System.Collections.Generic;
using System.Windows.Forms;

namespace SmartProperty.Editors.Editor
{
    public partial class SmartPropertyObjectEditor : UserControl, ISmartPropertyEditor
    {
        private IList<IProperty> Properties;

        public SmartPropertyObjectEditor()
        {
            InitializeComponent();
        }

        public void SetValue(object value)
        {
            Properties = value as IList<IProperty>;
            smartPropertyGrid.LoadProperties(Properties);
        }

        public object GetValue()
        {
            return Properties;
        }
    }
}
