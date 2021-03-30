using System;

namespace SmartProperty.Editors
{
    interface IFormSmartPropertyEditor: ISmartPropertyEditor
    {
        void DefinePropertyEditor(Type editorType);
    }
}