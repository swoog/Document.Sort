namespace Document.Sort
{
    public interface IFileMover
    {
        void Move(string source, string destination);

        void OpenPdf(string file);
    }
}