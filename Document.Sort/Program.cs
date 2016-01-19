using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document.Sort
{
    using System.Diagnostics;
    using System.IO;

    class Program
    {
        static void Main(string[] args)
        {
            var sourceDirectory = args[0];
            var destDirectory = args[1];

            var consoleUiInteraction = new ConsoleUiInteraction();
            var fileMover = new FileMover();
            var fileScanner = new FileScanner(consoleUiInteraction, fileMover, destDirectory);
            var scanner = new DirectoryScanner(fileScanner);

            scanner.Scan(sourceDirectory);
        }
    }

    internal class FileBase
    {
        public void OpenPdf(string file)
        {
            var arguments = $"\"{file}\"";
            var processStartInfo = new ProcessStartInfo(@"C:\Program Files (x86)\Adobe\Reader 11.0\Reader\AcroRd32.exe", arguments);

            var process = new Process();

            process.StartInfo = processStartInfo;

            process.Start();
            process.WaitForExit();
         }
    }

    internal class FileCopy : FileBase, IFileMover
    {
        public void Move(string source, string destination)
        {
            var directory = Path.GetDirectoryName(destination);

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            File.Copy(source, destination);
        }
    }

    internal class FileMover : FileBase, IFileMover
    {
        public void Move(string source, string destination)
        {
            var directory = Path.GetDirectoryName(destination);

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            File.Move(source, destination);
        }
    }
}
