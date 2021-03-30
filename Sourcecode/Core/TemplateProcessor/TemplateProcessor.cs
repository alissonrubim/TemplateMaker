using HandlebarsDotNet;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Text;
using TemplateProcessor.Helpers.SmartString;
using TemplateProcessor.Helpers.SmartType;
using TemplateProcessor.Models;

namespace TemplateProcessor
{
    public delegate void TemplateProcessorOnProcessFileHandler(string filePath, byte[] fileContents);
    public delegate void TemplateProcessorOnProcessFileErrorHandler(string filePath, Exception exception);
    public class TemplateProcessor
    {
        public event TemplateProcessorOnProcessFileHandler OnProcessFile;
        public event TemplateProcessorOnProcessFileErrorHandler OnProcessFileError;
        private readonly Template Template;
        public TemplateProcessor(Template template)
        {
            Template = template;
            
            SetupHandlebars();
        }

        private void SetupHandlebars()
        {
            Handlebars.Configuration.ThrowOnUnresolvedBindingExpression = true;
            SmartStringHelperRegister.Register();
            SmartTypeHelperRegister.Register();
        }

        public void Process(IEnumerable<TemplateParameter> parameters)
        {
            ProcessDirectory(Template.SearchDirectory, TemplateParameterProcessor.Process(parameters));
        }

        private void ProcessDirectory(string directoryName, dynamic parameters)
        {
            foreach (var file in Directory.GetFiles(directoryName))
                if (!Path.GetFileName(file).Equals(TemplateManager.TemplateFileName))
                    ProcessFile(file, parameters);

            foreach (string directory in Directory.GetDirectories(directoryName))
                ProcessDirectory(directory, parameters);
        }
        private void ProcessFile(string filePath, dynamic parameters)
        {
            try
            {
                //Process the file name
                HandlebarsTemplate<object, object> templateFileName = Handlebars.Compile(filePath.Replace("\\", "//"));
                string processedFileName = templateFileName(parameters);
                processedFileName = processedFileName.Replace("//", "\\");

                if ((Template.SearchFileExtensions as IList<string>).IndexOf(Path.GetExtension(filePath)) >= 0){
                    string fileContents = File.ReadAllText(filePath);
                    //Process the file contents
                    HandlebarsTemplate<object, object> templateFileContents = Handlebars.Compile(fileContents);
                    string processedFileContents = templateFileContents(parameters);
                    OnProcessFile?.Invoke(GetTruncateFilePath(processedFileName, Template.SearchDirectory), Encoding.ASCII.GetBytes(processedFileContents));
                }
                else
                    OnProcessFile?.Invoke(GetTruncateFilePath(processedFileName, Template.SearchDirectory), File.ReadAllBytes(filePath));
            }
            catch (Exception e)
            {
                OnProcessFileError?.Invoke(GetTruncateFilePath(filePath, Template.SearchDirectory), e);
            }
        }

        private string GetTruncateFilePath(string filePath, string root)
        {
            filePath = filePath.Replace(root, string.Empty);
            if (Path.GetPathRoot(filePath) != string.Empty)
                filePath = filePath.Substring(Path.GetPathRoot(filePath).Length);
            return filePath;
        }
    }
}
