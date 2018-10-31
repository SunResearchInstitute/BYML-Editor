using System;
using System.IO;
using System.Windows.Forms;
using The4Dimension;

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

        private void OpenYAMLDisplayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConvertBYML(false);
        }

        private void OpenXMLDisplayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConvertBYML(true);
        }

        private void CreateYAMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox.Text = "";
            textBox.ReadOnly = true;
            IsXML = false;
        }

        private void CreateXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox.Text = "";
            textBox.ReadOnly = false;
            IsXML = true;
        }

        private void SaveLittleEndianToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save(false);
        }

        private void SaveBigEndianToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save(true);
        }

        private void ClearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox.Text = "";
            textBox.ReadOnly = true;
        }

        private void ConvertBYML(bool wantXML)
        {
            Directory.CreateDirectory(tempPath.FullName);

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileInfo selected = new FileInfo(openFileDialog.FileName);

                if (wantXML == false)
                {
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

                    textBox.Text = File.ReadAllText(yamlPath.FullName);
                    IsXML = false;
                }
                else
                {
                    textBox.Text = BymlConverter.GetXml(selected.FullName);
                    IsXML = true;
                }
                openFileDialog.FileName = "";
                textBox.ReadOnly = false;
            } 
        }

        private void Save(bool isBigEndian)
        {
            saveFileDialog.ShowDialog();
            if (saveFileDialog.FileName != "")
            {
                FileInfo savePath = new FileInfo(saveFileDialog.FileName);
                if (IsXML == true)
                {
                    if (savePath.Exists) savePath.Delete();
                    File.WriteAllBytes(savePath.FullName, BymlConverter.GetByml(textBox.Text));
                }
                else
                {
                    if (yamlPath.Exists) yamlPath.Delete();
                    File.WriteAllText(yamlPath.FullName, textBox.Text);
                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                    System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo
                    {
                        WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden,
                        FileName = "cmd.exe"
                    };
                    if (isBigEndian == true) startInfo.Arguments = $"/C yml_to_byml.exe \"{yamlPath.FullName}\" \"{savePath.FullName}\" -b";
                    else startInfo.Arguments = $"/C yml_to_byml.exe \"{yamlPath.FullName}\" \"{savePath.FullName}\"";
                    process.StartInfo = startInfo;
                    //todo: catch errors somehow
                    process.Start();
                    process.WaitForExit();
                }
                saveFileDialog.FileName = "";
            }
        }
    }
}
