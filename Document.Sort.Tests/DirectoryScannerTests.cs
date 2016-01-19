namespace Document.Sort.Tests
{
    using NSubstitute;

    using Xunit;

    public class DirectoryScannerTests
    {

        [Fact]
        public void Should_sort_file_when_found_a_file()
        {
            IFileScanner fileScanner = Substitute.For<IFileScanner>();

            var directoryScanner = new DirectoryScanner(fileScanner);

            directoryScanner.Scan("DirectoryScanner/");

            fileScanner.Received(1).Sort(
                Arg.Is<string>(s => s.EndsWith("DirectoryScanner/Test.txt")));
        }

        [Fact]
        public void Should_sort_file_when_found_a_file_in_sub_directory()
        {
            IFileScanner fileScanner = Substitute.For<IFileScanner>();

            var directoryScanner = new DirectoryScanner(fileScanner);

            directoryScanner.Scan(@"DirectoryScanner\");

            fileScanner.Received(1).Sort(
                Arg.Is<string>(s => s.EndsWith(@"DirectoryScanner\SubDirectory\Test.txt")));
        }
    }
}
