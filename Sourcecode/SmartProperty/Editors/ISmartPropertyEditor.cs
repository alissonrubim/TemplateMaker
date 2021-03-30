namespace SmartProperty.Editors
{
    public interface ISmartPropertyEditor
    {
        void SetValue(object value);
        object GetValue();
    }
}
