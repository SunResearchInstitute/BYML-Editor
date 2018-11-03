using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Windows;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace Splat2Decryptor
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                if (!File.Exists("./nisasyst.py"))
                {
                    Console.WriteLine("Requesting SciresM's decryption script...");
                    new WebClient().DownloadFile("https://gist.githubusercontent.com/SciresM/dba70bc2ee7eca11e1bd777ecb58ff16/raw/5bbca5d062d3d5fdb2b8507fe6d323b318be2d20/nisasyst.py", "nisasyst.py");
                }
                if (!IsPythonInstalled())
                {
                    MessageBox.Show("You don't have Python installed! Go install the latest Python 2.");
                    Environment.Exit(1);
                }
                Console.WriteLine("Installing pycrypto");
                InstallPyCrypto();
                string path = dialog.FileName;
                Directory.CreateDirectory($"./dec/");
                string result;
                foreach (string file in FindFilesRecursively(path))
                {
                    string relativefile = file.Replace(path, "").Replace('\\', '/').Substring(1);
                    string output = $"./dec/{relativefile}";
                    Directory.CreateDirectory(new FileInfo(output).Directory.FullName);
                    result = RunPythonCommand($"nisasyst.py \"{file}\" \"{relativefile}\" \"{output}\"");
                    if (result.Contains("Error: Input appears not to be an encrypted Splatoon 2 archive!"))
                        Console.WriteLine($"Skipping {relativefile}...");
                    else if (result.Contains("Traceback"))
                    {
                        File.WriteAllText("./error.txt", result);
                        MessageBox.Show("Unknown error occured! Saved log to error.txt");
                        Environment.Exit(1);
                    } else
                        Console.WriteLine($"Decrypted {relativefile}");
                }
            }
        }

        static string RunPythonCommand(string command)
        {
            string result;
            if (!CanUsePythonLauncher())
            {
                if (GetPythonVersion().ToCharArray()[0] != '2')
                    MessageBox.Show("You don't have Python 2 installed! This probably won't work...");
                result = RunCommand("python " + command);
            }
            else
            {
                result = RunCommand("py -2 " + command);
                if (result.Contains("Requested Python version (2) not installed"))
                {
                    MessageBox.Show("You don't have Python 2 installed! Go install it.");
                    Environment.Exit(1);
                }
            }
            return result;
        }

        static string GetPythonVersion()
        {
            string result;
            result = RunCommand("py --version");
            if(result.Contains("not recognized"))
                result = RunCommand("python --version");
            if (result.Contains("not recognized"))
                return null;
            return result.Replace("Python ", "");
        }

        static bool CanUsePythonLauncher()
        {
            string result;
            result = RunCommand("py --version");
            if (result.Contains("not recognized"))
                return false;
            return true;
        }

        static bool IsPythonInstalled()
        {
            return GetPythonVersion() != null;
        }

        static void InstallPyCrypto()
        {
            string result;
            if (!CanUsePythonLauncher())
            {
                if(GetPythonVersion().ToCharArray()[0] == '3')
                    MessageBox.Show("You only have Python 3 installed! This probably won't work...");
                result = RunCommand("pip install pycrypto");
            } else
            {
                result = RunCommand("py -2 -m pip install pycrypto");
                if(result.Contains("Requested Python version (2) not installed"))
                {
                    MessageBox.Show("You don't have Python 2 installed! Go install it.");
                    Environment.Exit(1);
                }
            }
            if (result.Contains("error: Microsoft Visual C++ 9.0 is required."))
            {
                MessageBox.Show("You are missing Microsoft Visual C++ 9.0.\nAfter you press ok, the link to install will be put into your clipboard.\nRun this program again when you are done.");
                Clipboard.SetText("https://www.microsoft.com/en-us/download/details.aspx?id=44266");
                Environment.Exit(1);
            }
            if (result.Contains("error"))
            {
                File.WriteAllText("./error.txt", result);
                MessageBox.Show("Unknown error occured! Saved log to error.txt");
                Environment.Exit(1);
            }

        }

        static List<string> FindFilesRecursively(string path)
        {
            // much easier than Java
            List<string> list = new List<string>();
            foreach (string file in Directory.EnumerateFiles(path, "*.*", SearchOption.AllDirectories))        
                list.Add(file);
            return list;
        }

        static string RunCommand(string command)
        {
            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = "cmd.exe";
            info.Arguments = "/C " + command;
            info.UseShellExecute = false;
            info.RedirectStandardInput = true;
            info.RedirectStandardOutput = true;
            info.RedirectStandardError = true;
            info.CreateNoWindow = true;
            

            using (Process process = Process.Start(info))
            {
                StringBuilder builder = new StringBuilder();
                process.OutputDataReceived += (s, a) => builder.Append(a.Data);
                process.ErrorDataReceived += (s, a) => builder.Append(a.Data);
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
                process.WaitForExit();
                return builder.ToString();
            }
        }
    }
}
