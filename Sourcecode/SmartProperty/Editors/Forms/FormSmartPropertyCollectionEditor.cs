using SmartProperty.EditorFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SmartProperty.Editors.Forms
{
    public partial class FormSmartPropertyCollectionEditor : Form, IFormSmartPropertyEditor
    {
        private Type EditorType;
        private ICollection<object> Collection;
        public FormSmartPropertyCollectionEditor()
        {
            InitializeComponent(); 
        }

        public void LoadCollection(ICollection<object> collection)
        {
            if (collection == null)
                collection = new List<object>();
            Collection = collection as ICollection<object>;

            dataGridView.Rows.Clear();
            foreach (object item in collection)
            {
                int index = dataGridView.Rows.Add();
                dataGridView.Rows[index].Cells["ColumnItemIndex"].Value = index;
                dataGridView.Rows[index].Cells["ColumnItemValue"].Value = item;
            }
        }

        public void DefinePropertyEditor(Type editorType)
        {
            EditorType = editorType;
        }

        public object GetValue()
        {
            return Collection;
        }

        public void SetValue(object value)
        {
            LoadCollection(value as ICollection<object>);
        }
        
        private void SetItemToCollection(int index, object value)
        {
            if (index < Collection.Count())
                (Collection as IList<object>)[index] = value;
            else
                (Collection as IList<object>).Add(value);
        }

        private void dataGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            dataGridView.Rows[e.RowIndex].Cells["ColumnItemIndex"].Value = e.RowIndex;
            if (EditorType != null)
            {
                e.Cancel = true;
                object value = null;
                if (e.RowIndex < Collection.Count())
                    value = Collection.ElementAt(e.RowIndex);
                value = SmartPropetyEditorFactory.OpenEditor(EditorType, value, false);
                dataGridView.Rows[e.RowIndex].Cells["ColumnItemValue"].Value = value;
                SetItemToCollection(e.RowIndex, value);
                LoadCollection(Collection);
            }
        }

        private void dataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            object value = dataGridView.Rows[e.RowIndex].Cells["ColumnItemValue"].Value;
            dataGridView.Rows[e.RowIndex].Cells["ColumnItemIndex"].Value = e.RowIndex;
            SetItemToCollection(e.RowIndex, value);
        }
    }
}
