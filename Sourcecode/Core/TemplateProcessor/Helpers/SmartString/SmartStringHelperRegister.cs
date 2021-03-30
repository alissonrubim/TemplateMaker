using HandlebarsDotNet;

namespace TemplateProcessor.Helpers.SmartString
{
    public static class SmartStringHelperRegister
    {
        public static void Register()
        {
            Handlebars.RegisterHelper("PascalCase", (output, context, arguments) => output.Write(new SmartString(arguments[0].ToString()).PascalCase));
            Handlebars.RegisterHelper("CamelCase", (output, context, arguments) => output.Write(new SmartString(arguments[0].ToString()).CamelCase));
            Handlebars.RegisterHelper("SnakeCase", (output, context, arguments) => output.Write(new SmartString(arguments[0].ToString()).SnakeCase));
            Handlebars.RegisterHelper("LowerCase", (output, context, arguments) => output.Write(new SmartString(arguments[0].ToString()).LowerCase));
            Handlebars.RegisterHelper("UpperCase", (output, context, arguments) => output.Write(new SmartString(arguments[0].ToString()).UpperCase));
        }
    }
}
