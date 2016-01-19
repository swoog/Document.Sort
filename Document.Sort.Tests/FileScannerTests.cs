namespace Document.Sort.Tests
{
    using NSubstitute;

    using Xunit;

    public class FileScannerTests
    {

        [Fact]
        public void Should_question_redirect_when_found_a_pdf()
        {
            var fileMover = Substitute.For<IFileMover>();
            var uiInteraction = Substitute.For<IUiInteraction>();
            var fileScanner = new FileScanner(uiInteraction, fileMover, "");

            fileScanner.Sort(@"MyDirectory\MyFile.pdf");

            uiInteraction.Received(1).Question("Choose category for file MyFile.pdf\no : Open file MyFile.pdf\n:");
        }

        [Fact]
        public void Should_move_to_directory_redirect_when_answer_the_category()
        {
            var fileMover = Substitute.For<IFileMover>();
            var uiInteraction = Substitute.For<IUiInteraction>();

            uiInteraction.Question(Arg.Any<string>()).Returns("MyCategory");

            var fileScanner = new FileScanner(uiInteraction, fileMover, "directoryDestination");

            fileScanner.Sort(@"MyDirectory\MyFile.pdf");

            fileMover.Received(1).Move(@"MyDirectory\MyFile.pdf", @"directoryDestination\MyCategory\MyFile.pdf");
        }

        [Fact]
        public void Should_do_not_move_file_when_answer_empty_for_the_category()
        {
            var fileMover = Substitute.For<IFileMover>();
            var uiInteraction = Substitute.For<IUiInteraction>();

            uiInteraction.Question(Arg.Any<string>()).Returns("");

            var fileScanner = new FileScanner(uiInteraction, fileMover, "directoryDestination");

            fileScanner.Sort(@"MyDirectory\MyFile.pdf");

            fileMover.DidNotReceive().Move(Arg.Any<string>(), Arg.Any<string>());
        }

        [Fact]
        public void Should_open_file_when_answer_o_for_the_category()
        {
            var fileMover = Substitute.For<IFileMover>();
            var uiInteraction = Substitute.For<IUiInteraction>();

            uiInteraction.Question(Arg.Any<string>()).Returns("o", "");

            var fileScanner = new FileScanner(uiInteraction, fileMover, "directoryDestination");

            fileScanner.Sort(@"MyDirectory\MyFile.pdf");

            fileMover.Received(1).OpenPdf(@"MyDirectory\MyFile.pdf");
        }

        [Fact]
        public void Should_did_not_move_file_when_answer_o_and_empty_for_the_category()
        {
            var fileMover = Substitute.For<IFileMover>();
            var uiInteraction = Substitute.For<IUiInteraction>();

            uiInteraction.Question(Arg.Any<string>()).Returns("o", "");

            var fileScanner = new FileScanner(uiInteraction, fileMover, "directoryDestination");

            fileScanner.Sort(@"MyDirectory\MyFile.pdf");

            fileMover.DidNotReceive().Move(Arg.Any<string>(), Arg.Any<string>());
        }

        [Fact]
        public void Should_call_question_again_when_answer_o_for_the_category()
        {
            var fileMover = Substitute.For<IFileMover>();
            var uiInteraction = Substitute.For<IUiInteraction>();

            uiInteraction.Question(Arg.Any<string>()).Returns("o", "");

            var fileScanner = new FileScanner(uiInteraction, fileMover, "directoryDestination");

            fileScanner.Sort(@"MyDirectory\MyFile.pdf");

            uiInteraction.Received(2).Question(Arg.Any<string>());
        }
    }
}
