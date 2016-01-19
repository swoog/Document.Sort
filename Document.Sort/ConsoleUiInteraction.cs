namespace Document.Sort
{
    using System;

    internal class ConsoleUiInteraction : IUiInteraction
    {
        public string Question(string question)
        {
            Console.Write(question);
            var result = Console.ReadLine();
            return result;
        }
    }
}