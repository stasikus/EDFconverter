using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter path to edf2ascii.exe...");
            string edf2asciiPath = Console.ReadLine();// "D:\\diplom"; //
            Console.WriteLine("Enter path to parent dir...");
            string path = Console.ReadLine(); //"D:\\diplom\\data";


            string[] folders = System.IO.Directory.GetDirectories(path, "*", System.IO.SearchOption.AllDirectories);

            for (int i = 0; i < folders.Length; i++)
            {
                path = folders[i];
                string[] files = System.IO.Directory.GetFiles(path);

                for (int j = 0; j < files.Length; j++)
                {
                    string nameFolder = Path.GetFileName(files[j]).Substring(0, 7);
                    string newFolderPath = Path.GetDirectoryName(files[j]);
                    string nameCopiedFile = newFolderPath + "\\" + nameFolder + "\\" + nameFolder + ".edf";

                    if (Directory.Exists(nameFolder))
                    {
                        File.Copy(files[j], nameCopiedFile);
                    }
                    else
                    {
                        System.IO.Directory.CreateDirectory(newFolderPath + "\\" + nameFolder);
                        File.Copy(files[j], nameCopiedFile);
                    }

                    string strCmdText = "" + edf2asciiPath + "\\edf2ascii.exe " + nameCopiedFile + "";

                    var startInfo = new ProcessStartInfo
                    {
                        FileName = "cmd.exe",
                        RedirectStandardInput = true,
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    };

                    var process = new Process { StartInfo = startInfo };

                    process.Start();
                    process.StandardInput.WriteLine(strCmdText);
                    process.StandardInput.WriteLine("exit");

                    process.WaitForExit();

                    if (File.Exists(nameCopiedFile))
                    {
                        File.Delete(nameCopiedFile);
                    }

                    Console.WriteLine(files[j].ToString() + " - Done");
                }
            }
            Console.WriteLine("Finished");
            Console.ReadLine();
        }
    }
}
