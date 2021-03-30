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
    public partial class TableInfoPropertyEditor : UserControl, ISmartPropertyEditor
    {
        private readonly SchemaProviderManager SchemaProviderManager;
        public TableInfoType Value { get; set; }
        public TableInfoPropertyEditor()
        {
            InitializeComponent();
            SchemaProviderManager = new SchemaProviderManager();
        }

        public void SetValue(object value)
        {
            Value = value as TableInfoType;
            LoadValue();
        }

        public object GetValue()
        {
            return Value;
        }

        private void LoadValue()
        {
            foreach (ISchemaProvider schemaProvider in SchemaProviderManager.GetProviders())
                comboBoxProvider.Items.Add(schemaProvider);
            if (comboBoxProvider.Items.Count > 0)
                comboBoxProvider.SelectedIndex = 0;

            if (Value == null)
                Value = new TableInfoType();
            textBoxFullName.Text = Value.FullName;
            textBoxName.Text = Value.Name;
            textBoxPrefix.Text = Value.Prefix;


            var souce = new BindingSource();
            souce.DataSource = Value.Columns;
            dataGridViewColumns.AutoGenerateColumns = false;
            dataGridViewColumns.DataSource = souce;

        }

        private ISchemaProvider GetSelectedSchemaProvider()
        {
            return comboBoxProvider.SelectedItem as ISchemaProvider;
        }

        private void buttonLoadTableInfo_Click(object sender, EventArgs e)
        {
            try
            {
                Value = SchemaProvider.GetTableInfo(GetSelectedSchemaProvider(), textBoxEntityName.Text);
                LoadValue();
            }
            catch(SchemaNotFoundException ex)
            {
                MessageBox.Show(ex.Message, "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
