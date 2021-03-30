using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using TemplateProcessor.Models;
using TemplateMaker.Viewer.Models;
using TemplateProcessor;
using TemplateMaker.Viewer.Views;
using TemplateProcessor.Helpers.SmartString.Exceptions;
using SmartProperty;

namespace TemplateMaker.Viewer
{
    public partial class FormMain : Form
    {
        private readonly TemplateManager TemplateManager;
        private List<IProperty> CurrentProperties;
        private Template CurrentTemplate;
        public FormMain()
        {
            InitializeComponent();
            TemplateManager = new TemplateManager();
        }

        private void LoadTemplates()
        {
            TemplateManager.Load(@"C:\Dev\DotNet\#Mine\TemplateMaker\Sourcecode\Templates");
            foreach (Template template in TemplateManager.GetTemplates())
                comboBoxTemplate.Items.Add(template);
            if(comboBoxTemplate.Items.Count > 0)
                comboBoxTemplate.SelectedIndex = 0;
        }

        private void LoadTemplate(Template template)
        {
            try
            {
                CurrentTemplate = template;
                richTextBoxDescription.Text = CurrentTemplate.Description;
                CurrentProperties = ConvertParameterToProperty(CurrentTemplate.Parameters);
                smartPropertyGrid.LoadProperties(CurrentProperties);

                ShowTemplateParameters();
            }
            catch (Exception e)
            {
                MessageBox.Show($"An error ocurred when loading the template `{CurrentTemplate.Name}`: {e.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private List<IProperty> ConvertParameterToProperty(IEnumerable<TemplateParameter> parameters)
        {
            List<IProperty> properties = new List<IProperty>();
            foreach (TemplateParameter parameter in parameters)
                properties.Add(new Property(parameter));
            return properties;
        }

        private void ShowTemplateParameters()
        {
            dynamic templateParameters = TemplateParameterProcessor.Process(CurrentTemplate.Parameters);
            richTextBoxParametersJson.Text = JsonConvert.SerializeObject(templateParameters, Formatting.Indented);
        }

        /*private void ThreatInvalidDictionaryEntryException(MissingDictonaryEntryException ex)
        {
            FormDictionaryEntryEditor formDictionaryEntryEditor = new FormDictionaryEntryEditor(ex.Word);
            formDictionaryEntryEditor.ShowDialog();
            if (formDictionaryEntryEditor.Continue)
                ShowTemplateParameters();
        }*/


        private void FormMain_Shown(object sender, EventArgs e)
        {
            LoadTemplates();
        }

        private void comboBoxTemplate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBoxTemplate.SelectedItem != null)
                LoadTemplate(comboBoxTemplate.SelectedItem as Template);
        }

        private void buttonExecute_Click(object sender, EventArgs e)
        {
            var outputPath = Path.Combine(@"C:\Dev\DotNet\#Mine\TemplateMaker\Sourcecode\Output", CurrentTemplate.Name);
            if (Directory.Exists(outputPath))
                Directory.Delete(outputPath, true);

            TemplateProcessor.TemplateProcessor processor = new TemplateProcessor.TemplateProcessor(CurrentTemplate);
            processor.OnProcessFile += (string filePath, byte[] fileContents) =>
            {
                var outputFilePath = Path.Combine(outputPath, filePath);
                if (!Directory.Exists(Path.GetDirectoryName(outputFilePath)))
                    Directory.CreateDirectory(Path.GetDirectoryName(outputFilePath));
                File.WriteAllBytes(outputFilePath, fileContents);
            };
            processor.OnProcessFileError += (string file, Exception exception) =>
            {
                MessageBox.Show($"An error ocurred when processing the template at file `{file}`: {exception.Message}", "Atention", MessageBoxButtons.OK, MessageBoxIcon.Error);
            };
            processor.Process(CurrentTemplate.Parameters);
        }

        private void smartPropertyGrid_PropertyValueChanged(IProperty property)
        {
            ShowTemplateParameters();
        }
    }
}
