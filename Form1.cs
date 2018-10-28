using System;
using System.IO;
using System.Windows.Forms;

namespace BYML_Editor
{

    public partial class Editor : Form
    {
        static DirectoryInfo tempPath = new DirectoryInfo($"{Path.GetTempPath()}/BYML");
        static FileInfo ymlPath = new FileInfo($"{Path.GetTempPath()}/BYML/temp.yml");
        static FileInfo savePath = new FileInfo($"{Path.GetTempPath()}/BYML/save");

        public Editor()
        {
            InitializeComponent();
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Directory.CreateDirectory(tempPath.FullName);

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileInfo selected = new FileInfo(openFileDialog.FileName);
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                if (ymlPath.Exists) ymlPath.Delete();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo
                {
                    WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden,
                    FileName = "cmd.exe",
                    Arguments = $"/C byml_to_yml.exe \"{selected.FullName}\" \"{ymlPath.FullName}\""
                };
                process.StartInfo = startInfo;
                process.Start();
                process.WaitForExit();

                ymlBox.Text = File.ReadAllText(ymlPath.FullName);
                ymlBox.ReadOnly = false;
            }
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ymlPath.Exists) ymlPath.Delete();
            if (savePath.Exists) savePath.Delete();
            File.WriteAllText(ymlPath.FullName, ymlBox.Text);
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo
            {
                WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden,
                FileName = "cmd.exe",
                Arguments = $"/C yml_to_byml.exe \"{ymlPath.FullName}\" \"{savePath.FullName}\""
            };
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();
            saveFileDialog.ShowDialog();
            MoveWithReplace(savePath.FullName, saveFileDialog.FileName);
        }

        public static void MoveWithReplace(string sourceFileName, string destFileName)
        {

            //first, delete target file if exists, as File.Move() does not support overwrite
            if (File.Exists(destFileName))
            {
                File.Delete(destFileName);
            }

            File.Move(sourceFileName, destFileName);
        }

        private void ClearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ymlBox.Text = "";
            ymlBox.ReadOnly = true;
        }

        private void CreateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ymlBox.ReadOnly = false;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
