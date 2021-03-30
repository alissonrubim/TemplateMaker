using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Dynamic;
using TemplateProcessor.Models;

namespace TemplateProcessor
{
    public class TemplateParameterProcessor
    {
        public static dynamic Process(IEnumerable<TemplateParameter> parameters)
        {
            IDictionary<string, object> obj = new ExpandoObject();
            ProcessProperties(obj, parameters);
            return obj;
        }

        private static void ProcessProperties(IDictionary<string, object> dictonary, IEnumerable<TemplateParameter> parameters)
        {
            foreach (TemplateParameter parameter in parameters)
                ProcessProperty(dictonary, parameter);
        }

        private static void ProcessProperty(IDictionary<string, object> dictonary, TemplateParameter parameter)
        {
            if (parameter.IsParameterObject)
            {
                IDictionary<string, object> obj = new ExpandoObject();
                ProcessProperties(obj, parameter.Value as IEnumerable<TemplateParameter>);
                dictonary.Add(parameter.Name, obj);
            }
            else
                dictonary.Add(parameter.Name, parameter.Value);
        }
    }
}
