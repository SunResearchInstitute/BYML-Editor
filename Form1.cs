using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            Text = "BYML=Editor";
            bymltext.Text = "";
            bymltext.ReadOnly = false;
            isXML = false;
        }

        private void CreateXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Text = "BYML=Editor";
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
            Text = "BYML-Editor";
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
                    Process process = new Process();
                    ProcessStartInfo startInfo = new ProcessStartInfo
                    {
                        WindowStyle = ProcessWindowStyle.Hidden,
                        FileName = "cmd.exe",
                        Arguments = $"/C byml_to_yml.exe \"{selected.FullName}\" \"{YamlPath.FullName}\"",
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    };
                    process.StartInfo = startInfo;
                    process.Start();
                    process.WaitForExit();
                    //If byml-v2 worked there should be no output.
                    if (process.StandardOutput.ReadLine() != null) MessageBox.Show("Something went wrong, check that Python is installed and in your path with byml-v2 installed via PIP (pip install byml).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                this.Text = $"BYML-Editor | {selected.Name}";
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
                    Process process = new Process();
                    ProcessStartInfo startInfo = new ProcessStartInfo
                    {
                        WindowStyle = ProcessWindowStyle.Hidden,
                        FileName = "cmd.exe",
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    };
                    if (isBigEndian == true) startInfo.Arguments = $"/C yml_to_byml.exe \"{YamlPath.FullName}\" \"{savePath.FullName}\" -b";
                    else startInfo.Arguments = $"/C yml_to_byml.exe \"{YamlPath.FullName}\" \"{savePath.FullName}\"";
                    process.StartInfo = startInfo;
                    process.Start();
                    process.WaitForExit();
                    //If byml-v2 worked there should be no output.
                    if (process.StandardOutput.ReadLine() != null) MessageBox.Show("Something went wrong, check that Python is installed and in your path with byml-v2 installed via PIP (pip install byml).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                Text = $"BYML-Editor | {savePath.Name}";
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

        private void DeobfuscateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(bymltext.Text))
            {
                if (opendictDialog.ShowDialog() == DialogResult.OK)
                {
                    string deobf = bymltext.Text;
                    FileInfo dict = new FileInfo(opendictDialog.FileName);
                    string[] param = File.ReadAllLines(opendictDialog.FileName);
                    foreach (string replace in param)
                    {
                        //Are there any other Dictionaries that use diffrent formatting?
                        string[] parts = replace.Split('=', '	', ' ');
                        //add " " for workaround with replace because regex is a dark place.
                        deobf = deobf.Replace(parts[0], parts[1] + " ");
                    }
                    bymltext.Text = deobf;
                }
            }
            else
            {
                MessageBox.Show("BYML text box is empty.", "Empty", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ReobfuscateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(bymltext.Text))
            {
                if (opendictDialog.ShowDialog() == DialogResult.OK)
                {
                    string reobf = bymltext.Text;
                    FileInfo dict = new FileInfo(opendictDialog.FileName);
                    string[] param = File.ReadAllLines(opendictDialog.FileName);
                    foreach (string replace in param)
                    {
                        //Are there any other Dictionaries that use diffrent formatting?
                        string[] parts = replace.Split('=', '	', ' ');
                        //add " " for workaround with replace because regex is a dark place.
                        reobf = reobf.Replace(parts[1] + " ", parts[0]);
                    }
                    bymltext.Text = reobf;
                }
            }
            else
            {
                MessageBox.Show("BYML text box is empty.", "Empty", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveTextboxToFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
#if DEBUG
            saveFileDialog.Title = "Save BYML Text Box Contents";
            saveFileDialog.Filter = "All files(*.*) | *.* ";
            saveFileDialog.ShowDialog();
            if (saveFileDialog.FileName != "")
            {
                FileInfo save = new FileInfo(saveFileDialog.FileName);
                File.WriteAllText(save.FullName, bymltext.Text);
            }
            saveFileDialog.Title = "Save Your BYML File";
            saveFileDialog.Filter = "BYML Files|*.byml;*.bprm|All files (*.*)|*.*";
#endif
        }
    }
}
