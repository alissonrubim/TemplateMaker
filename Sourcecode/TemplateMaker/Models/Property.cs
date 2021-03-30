using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SmartProperty;
using System;
using System.Collections.Generic;
using System.Linq;
using TemplateMaker.Viewer.Types;
using TemplateMaker.Views.PropertyEditors;
using TemplateProcessor;
using TemplateProcessor.Models;

namespace TemplateMaker.Viewer.Models
{
    public class Property : IProperty
    {
        private TemplateParameter TemplateParameter;

        public Property(TemplateParameter templateParameter)
        {
            TemplateParameter = templateParameter;
            ValidateConfiguration();
        }

        private void ValidateConfiguration()
        {
            if (TemplateParameter.IsParameterObject && TemplateParameter.Value == null)
                throw new Exception($"Parameter {TemplateParameter.Name} is defined as Object but don't have the parameters setted at the Value field.");
            if (TemplateParameter.IsCollection && TemplateParameter.IsParameterObject)
                throw new Exception($"Parameter {TemplateParameter.Name} is defined as Collection and as Object at the same time, this is not allowed.");

            //When loading, the Value is an JArray object, so this code will tranform it in a propert IEnumrable<TemplateParameter>
            if (TemplateParameter.IsParameterObject)
                TemplateParameter.Value = JArrayToTemplateParameterCollection(TemplateParameter);
            else if (TemplateParameter.IsCollection && TemplateParameter.Value != null)
                TemplateParameter.Value = JArrayToCollection(TemplateParameter);
            else if (GetValueType() != typeof(string) && TemplateParameter.Value != null)
                TemplateParameter.Value = JArrayToCustomType(TemplateParameter);

            ValidateDefaultValue();
        }

        private void ValidateDefaultValue()
        {
            bool valid = true;
            if (TemplateParameter.Value != null)
            {
                if (TemplateParameter.IsParameterObject)
                {
                    /*if (TemplateParameter.Value.GetType() != GetValueType())
                        throw new Exception($"The default Value on parameter {TemplateParameter.Name} it's not a valid {GetValueType()}");*/
                }
                else if (TemplateParameter.IsCollection)
                {
                    //valid = TemplateParameter.Value.GetType().IsAssignableFrom(GetValueType());
                }
                else
                    valid = TemplateParameter.Value.GetType() == GetValueType();    
            }

            if(!valid)
                throw new Exception($"The default Value on parameter {TemplateParameter.Name} it's not a valid {GetValueType()}");
        }

        private object JArrayToCustomType(TemplateParameter parameter)
        {
            if (parameter.Value.GetType().IsAssignableFrom(typeof(JObject)))
                return JsonConvert.DeserializeObject(parameter.Value.ToString(), GetValueType());
            return Convert.ChangeType(parameter.Value, GetValueType());
        }

        private ICollection<object> JArrayToCollection(TemplateParameter parameter)
        {
            if (parameter.Value.GetType().IsAssignableFrom(typeof(JArray)))
                return JsonConvert.DeserializeObject<ICollection<object>>(parameter.Value.ToString());
            return parameter.Value as ICollection<object>;
        }

        private IEnumerable<TemplateParameter> JArrayToTemplateParameterCollection(TemplateParameter parameter)
        {
            if(parameter.Value.GetType().IsAssignableFrom(typeof(JArray)))
                return JsonConvert.DeserializeObject<IEnumerable<TemplateParameter>>(parameter.Value.ToString());
            return parameter.Value as IEnumerable<TemplateParameter>;
        }

        public string GetDescription()
        {
            return TemplateParameter.Name;
        }

        public string GetName()
        {
            return TemplateParameter.Name;
        }

        public object GetValue()
        {
            if (GetIsParameterObject())
            {
                List<IProperty> proprs = new List<IProperty>();
                foreach (TemplateParameter templateParameter in TemplateParameter.Value as IEnumerable<TemplateParameter>)
                    proprs.Add(new Property(templateParameter));
                return proprs;
            }
            else
                return TemplateParameter.Value;
        }

        public void SetValue(object value)
        {
            if (!GetIsParameterObject())
                TemplateParameter.Value = value;
        }

        public Type GetValueType()
        {
            if (GetIsCollection())
            {
                if (TemplateParameter.Type == ETemplateParameterType.String)
                    return typeof(ICollection<string>);
                else if (TemplateParameter.Type == ETemplateParameterType.Object)
                    return typeof(ICollection<TemplateParameter>);
                else if (TemplateParameter.Type == ETemplateParameterType.TableInfo)
                    return typeof(ICollection<TableInfoType>);
                else if (TemplateParameter.Type == ETemplateParameterType.ColumnInfo)
                    return typeof(ICollection<ColumnInfoType>);
                else if (TemplateParameter.Type == ETemplateParameterType.ApiInfo)
                    return typeof(ICollection<ApiInfoType>);
            }
            else
            {
                if (TemplateParameter.Type == ETemplateParameterType.String)
                    return typeof(string);
                else if (TemplateParameter.Type == ETemplateParameterType.Object)
                    return typeof(IEnumerable<TemplateParameter>);
                else if (TemplateParameter.Type == ETemplateParameterType.TableInfo)
                    return typeof(TableInfoType);
                else if (TemplateParameter.Type == ETemplateParameterType.ColumnInfo)
                    return typeof(ColumnInfoType);
                else if (TemplateParameter.Type == ETemplateParameterType.ApiInfo)
                    return typeof(ApiInfoType);
            }
            return null;
        }

        public string GetDisplayValue()
        {
            if (GetValue() != null)
            {
                if (GetIsCollection())
                    return $"[Collection::{TemplateParameter.Type}][{(GetValue() as ICollection<object>).Count()}]";
                else
                {
                    if (TemplateParameter.Type == ETemplateParameterType.String)
                        return GetValue() as string;
                    else 
                        return $"[Object::{TemplateParameter.Type}]";
                }
            }
            return null;
        }

        public Type GetCustomEditorType()
        {
            if (TemplateParameter.Type == ETemplateParameterType.TableInfo)
                return typeof(TableInfoPropertyEditor);
            else if (TemplateParameter.Type == ETemplateParameterType.ColumnInfo)
                return typeof(ColumnInfoPropertyEditor);
            else if (TemplateParameter.Type == ETemplateParameterType.ApiInfo)
                return typeof(ApiInfoPropertyEditor);
            return null;
        }

        public bool GetIsCollection()
        {
            return TemplateParameter.IsCollection;
        }

        public bool GetIsParameterObject()
        {
            return TemplateParameter.IsParameterObject;
        }
    }
}
