namespace Document.Sort
{
    using System.IO;

    public class DirectoryScanner
    {
        private readonly IFileScanner fileScanner;

        public DirectoryScanner(IFileScanner fileScanner)
        {
            this.fileScanner = fileScanner;
        }

        public void Scan(string directory)
        {
            foreach (var file in Directory.GetFiles(directory))
            {
                this.fileScanner.Sort(file);
            }

            foreach (var subDirectory in Directory.GetDirectories(directory))
            {
                this.Scan(subDirectory);
            }
        }
    }
}