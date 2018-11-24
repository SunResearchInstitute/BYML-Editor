using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using The4Dimension;
using Yaz0Enc;

namespace BYML_Editor
{
    public partial class Editor : Form
    {
        static DirectoryInfo TempPath = new DirectoryInfo($"{Path.GetTempPath()}/BYML-Editor");
        static FileInfo YamlPath = new FileInfo($"{Path.GetTempPath()}/BYML-Editor/temp.yaml");
        private bool isXML;
#if DEBUG
        private static bool deletetemp = true;
#endif

        public Editor()
        {
            InitializeComponent();
#if DEBUG
            debugToolStripMenuItem.Visible = true;
#endif
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);
        }

        private void DisableDeletingTempFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
#if DEBUG
            if (deletetemp)
            {
                deletetemp = false;
                disableDeletingTempFolderToolStripMenuItem.Text = "Enable deleting temporary folder";
            }
            else
            {
                deletetemp = true;
                disableDeletingTempFolderToolStripMenuItem.Text = "Disable deleting temporary folder";
            }
#endif
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
            bymltext.Text = "";
            bymltext.ReadOnly = false;
            isXML = false;
        }

        private void CreateXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bymltext.Text = "";
            bymltext.ReadOnly = false;
            isXML = true;
        }

        private void SaveLittleEndianToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(bymltext.Text)) MessageBox.Show("The text box is blank.", "No BYML file", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else Save(false);
        }

        private void SaveBigEndianToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(bymltext.Text)) MessageBox.Show("The text box is blank.", "No BYML file", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else Save(true);
        }

        private void ClearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bymltext.Text = "";
            bymltext.ReadOnly = true;
        }

        private void Yaz0CompressLittleEndianToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openxmlFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileInfo file = new FileInfo(openyamlFileDialog.FileName);

                saveyaz0FileDialog.ShowDialog();
                if (saveyaz0FileDialog.FileName != "")
                {
                    FileInfo selected = new FileInfo(saveFileDialog.FileName);

                    FileStream byml = file.OpenRead();
                    File.WriteAllBytes(selected.FullName, Yaz0.Encode(byml));
                    byml.Close();
                }
            }
        }

        private void ConvertBYML(bool wantXML)
        {
            Directory.CreateDirectory(TempPath.FullName);

            OpenFileDialog bymlselect;
            if (wantXML == true) bymlselect = openxmlFileDialog;
            else bymlselect = openyamlFileDialog;

            if (bymlselect.ShowDialog() == DialogResult.OK)
            {
                FileInfo selected = new FileInfo(bymlselect.FileName);

                if (wantXML == false)
                {
                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                    System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo
                    {
                        WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden,
                        FileName = "cmd.exe",
                        Arguments = $"/C byml_to_yml.exe \"{selected.FullName}\" \"{YamlPath.FullName}\""
                    };
                    process.StartInfo = startInfo;
                    process.Start();
                    process.WaitForExit();

                    bymltext.Text = File.ReadAllText(YamlPath.FullName);
                    isXML = false;
                }
                else
                {
                    bymltext.Text = BymlConverter.GetXml(selected.FullName);
                    isXML = true;
                }
                bymlselect.FileName = "";
                bymltext.ReadOnly = false;
            }
        }

        private void Save(bool isBigEndian)
        {
            saveFileDialog.ShowDialog();
            if (saveFileDialog.FileName != "")
            {
                FileInfo savePath = new FileInfo(saveFileDialog.FileName);
                if (isXML == true)
                {
                    if (isBigEndian == true) bymltext.Text.Replace($"isBigEndian Value=\"False\"", $"isBigEndian Value=\"True\"");
                    else bymltext.Text.Replace($"isBigEndian Value=\"True\"", $"isBigEndian Value=\"False\"");
                    File.WriteAllBytes(savePath.FullName, BymlConverter.GetByml(bymltext.Text));
                }
                else
                {
                    File.WriteAllText(YamlPath.FullName, bymltext.Text);
                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                    System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo
                    {
                        WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden,
                        FileName = "cmd.exe"
                    };
                    if (isBigEndian == true) startInfo.Arguments = $"/C yml_to_byml.exe \"{YamlPath.FullName}\" \"{savePath.FullName}\" -b";
                    else startInfo.Arguments = $"/C yml_to_byml.exe \"{YamlPath.FullName}\" \"{savePath.FullName}\"";
                    process.StartInfo = startInfo;
                    //todo: catch errors somehow
                    process.Start();
                    process.WaitForExit();
                }
                saveFileDialog.FileName = "";
            }
        }

        private void DecryptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openxmlFileDialog.Title = "Select your BYML inside the Splatoon 2 romfs";
            if (openxmlFileDialog.ShowDialog() == DialogResult.OK)
            {
                DialogResult result = gamefolderBrowserDialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    string relativefile = openxmlFileDialog.FileName.Replace(gamefolderBrowserDialog.SelectedPath, "").Replace('\\', '/').Substring(1);
                    List<string> args = new List<string>(new string[] { relativefile, openxmlFileDialog.FileName });
                    NisasystSharp.NisasystSharp.Decrypt(args.ToArray());
                }
            }
            openxmlFileDialog.FileName = "";
            openxmlFileDialog.Title = "";
        }

        static void OnProcessExit(object sender, EventArgs e)
        {
#if DEBUG
            if (deletetemp)
#endif
                if (TempPath.Exists)
                    TempPath.Delete(true);
        }
    }
}
