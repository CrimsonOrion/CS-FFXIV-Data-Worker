using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FFXIV_Data_Worker
{
    public static class RunExternalProgram
    {
        public static async Task<string> LaunchVGMStreamAsync(string vgmStreamPath, string scdPath)
        {
            await Task.Run(() => LaunchVGMStream(vgmStreamPath, scdPath));
            return $"{scdPath.Replace(".scd", ".wav")} created.\r\n\r\n";
        }


        public static void LaunchVGMStream(string vgmStreamPath, string scdPath)
        {
            using (Process process = new Process())
            {
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.FileName = vgmStreamPath;
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.StartInfo.Arguments = $" /c \"D:\\Users\\eimi_\\Documents\\Projects\\C# Projects\\CS-FFXIV Data Worker\\FFXIV Data Worker\\bin\\Debug\\vgmstream\\test\" -o {scdPath.Replace(".scd", ".wav")} {scdPath}";
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                
                //* Set your output and error (asynchronous) handlers
                process.OutputDataReceived += new DataReceivedEventHandler(OutputHandler);
                process.ErrorDataReceived += new DataReceivedEventHandler(OutputHandler);
                
                //* Start process and handlers
                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
                process.WaitForExit();                
            }
        }
        
        private static void OutputHandler(object sendingProcess, DataReceivedEventArgs outLine)
        {
            //* Do your stuff with the output (write to console/log/StringBuilder)
            Console.WriteLine(outLine.Data);
        }
    }
}
