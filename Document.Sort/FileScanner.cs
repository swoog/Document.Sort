namespace Document.Sort
{
    using System.IO;

    public class FileScanner : IFileScanner
    {
        private readonly IUiInteraction uiInteraction;

        private readonly IFileMover fileMover;

        private readonly string directorydestination;

        public FileScanner(IUiInteraction uiInteraction, IFileMover fileMover, string directorydestination)
        {
            this.uiInteraction = uiInteraction;
            this.fileMover = fileMover;
            this.directorydestination = directorydestination;
        }

        public void Sort(string file)
        {
            if (file.EndsWith(".pdf"))
            {
                bool notAction;
                do
                {
                    notAction = false;
                    var fileName = Path.GetFileName(file);
                    var category = this.uiInteraction.Question($"Choose category for file {fileName}\no : Open file {fileName}\n:");

                    if (!string.IsNullOrEmpty(category))
                    {
                        if (category == "o")
                        {
                            this.fileMover.OpenPdf(file);
                            notAction = true;
                        }
                        else
                        {
                            this.fileMover.Move(file, Path.Combine(this.directorydestination, category, fileName));
                        }
                    }
                }
                while (notAction);
            }
        }
    }
}