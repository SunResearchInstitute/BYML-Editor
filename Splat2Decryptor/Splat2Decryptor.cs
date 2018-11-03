/*
MIT License

Copyright (c) 2018 Somebody Whoisbored

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;


namespace Splat2Decryptor
{
    class Program
    {
        [STAThread]
        public static void NisDecrypt()
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (!File.Exists("./nisasyst.py"))
                {
                    new WebClient().DownloadFile("https://gist.githubusercontent.com/SciresM/dba70bc2ee7eca11e1bd777ecb58ff16/raw/5bbca5d062d3d5fdb2b8507fe6d323b318be2d20/nisasyst.py", "nisasyst.py");
                }
                if (!IsPythonInstalled())
                {
                    MessageBox.Show("You don't have Python installed! Go install the latest Python 2.");
                    Environment.Exit(1);
                }
                InstallPyCrypto();
                string path = dialog.SelectedPath;
                Directory.CreateDirectory($"./dec/");
                string result;
                foreach (string file in FindFilesRecursively(path))
                {
                    string relativefile = file.Replace(path, "").Replace('\\', '/').Substring(1);
                    string output = $"./dec/{relativefile}";
                    Directory.CreateDirectory(new FileInfo(output).Directory.FullName);
                    result = RunPythonCommand($"nisasyst.py \"{file}\" \"{relativefile}\" \"{output}\"");
                    if (result.Contains("Traceback"))
                    {
                        File.WriteAllText("./error.txt", result);
                        MessageBox.Show("Unknown error occured! Saved log to error.txt");
                        Environment.Exit(1);
                    }
                    else MessageBox.Show($"Decrypted {relativefile}");
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
            ProcessStartInfo info = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = "/C " + command,
                UseShellExecute = false,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };


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
