using LunarLabs.Parser.XML;
using LunarLabs.Parser.YAML;
using System;
using System.IO;
using System.Windows.Forms;

namespace BYML_Editor
{

    public partial class Editor : Form
    {
        static DirectoryInfo tempPath = new DirectoryInfo($"{Path.GetTempPath()}/BYML-Editor");
        static FileInfo yamlPath = new FileInfo($"{Path.GetTempPath()}/BYML-Editor/temp.yaml");
        private bool IsXML;

        public Editor()
        {
            InitializeComponent();
        }
        
        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConvertBYML(false);

        }

        private void OpenXMLDisplayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConvertBYML(true);
        }

        private void CreateXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox.Text = "";
            textBox.ReadOnly = false;
            IsXML = true;
        }

        private void CreateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox.Text = "";
            textBox.ReadOnly = false;
            IsXML = false;
        }

        private void ClearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox.Text = "";
            textBox.ReadOnly = true;
            saveToolStripMenuItem.Enabled = false;
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog.ShowDialog();
            if (saveFileDialog.FileName != "")
            {
                if (IsXML == true)
                {
                    //todo: add YAML to XML
                }
                FileInfo savePath = new FileInfo(saveFileDialog.FileName);
                if (yamlPath.Exists) yamlPath.Delete();
                File.WriteAllText(yamlPath.FullName, textBox.Text);
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo
                {
                    WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden,
                    FileName = "cmd.exe",
                    Arguments = $"/C yml_to_byml.exe \"{yamlPath.FullName}\" \"{savePath.FullName}\""
                };
                process.StartInfo = startInfo;
                process.Start();
                process.WaitForExit();
            }
        }

        private void ConvertBYML(bool wantXML)
        {
            Directory.CreateDirectory(tempPath.FullName);

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileInfo selected = new FileInfo(openFileDialog.FileName);
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                if (yamlPath.Exists) yamlPath.Delete();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo
                {
                    WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden,
                    FileName = "cmd.exe",
                    Arguments = $"/C byml_to_yml.exe \"{selected.FullName}\" \"{yamlPath.FullName}\""
                };
                process.StartInfo = startInfo;
                process.Start();
                process.WaitForExit();

                if (wantXML == false)
                {
                    textBox.Text = File.ReadAllText(yamlPath.FullName);
                    IsXML = false;
                }
                else
                {
                    //fix YAML header and combine it with actual YAML
                    string yaml = "---\n";
                    yaml += File.ReadAllText(yamlPath.FullName);
                    yaml += "---\n ";
                    yaml = yaml.Replace("\r", "");
                    var root = YAMLReader.ReadFromString(yaml);
                    var xml = XMLWriter.WriteToString(root);
                    textBox.Text = File.ReadAllText(xml);
                    IsXML = true;
                }
                textBox.ReadOnly = false;
            } 
        }
    }
}
