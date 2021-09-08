using System;
using System.IO;

namespace example2
{
    class Program
    {
        static void Main()
        {
            string text = "";
            string selection = "";
            while (selection != "0")
            {
                while (text == "")
                    text = SecondMenu(text);

                Console.Write("\nMenu:\nCommands:\n f - find word\n r - replace word\n " +
                    "d - delete word\n s - second menu\n 0 - exit\n");
                Console.Write("\nEnter the command: ");
                selection = Console.ReadLine();

                Console.Write("Enter the word from text for command: ");
                string word = Console.ReadLine();

                text = text.ToLower();
                word = word.ToLower();

                switch (selection)
                {
                    case "f":
                        FindWord(word, text);
                        break;
                    case "r":
                        text = ReplaceWord(word, text);
                        ChangeOpenFileHelper(text);
                        break;
                    case "d":
                        text = DeleteWord(word, text);
                        ChangeOpenFileHelper(text);
                        break;
                    case "s":
                        text = SecondMenu(text);
                        break;
                    case "0":
                        Console.WriteLine("Exit.");
                        Environment.Exit(1);
                        break;
                    default:
                        Console.WriteLine("Error. Not correct symbol was entered.");
                        break;
                }
            }
        }
        static string SecondMenu(string text)
        {
            Console.Write("Open file? Enter 'y' or 'n' or '0' for exit: ");
            string changeOpenFile = Console.ReadLine();
            switch (changeOpenFile)
            {
                case "y":
                    text = EnterFilePath(text, "start");
                    if (text == "" || text == null)//D:\Projects\example2\text.txt //D:\Projects\example2\File.txt";
                    {
                        Console.WriteLine("No text for work. Please enter path for file.");
                        break;
                    }
                    ChangeOpenFileHelper(text);
                    break;
                case "n":
                    if (text == "")
                        Console.WriteLine("No text for work.");
                    else
                        Console.WriteLine("Continue work with information.");
                    ChangeOpenFileHelper(text);
                    break;
                case "0":
                    Console.WriteLine("Exit.");
                    Environment.Exit(1);
                    break;
                default:
                    Console.WriteLine("Error. Not correct symbol was entered. Please enter path for file.");
                    break;
            }
            return text;
        }
        static void FindWord(string word, string text)
        {
            Console.WriteLine("\nFind word.");
            string[] words = text.Split(' ', ',', '.');
            int wordCount = 0;
            int index1 = text.IndexOf(word);

            for (int i = 0; i < words.Length; i++)
                if (words[i] == word && words[i].Length == word.Length)
                    wordCount++;

            if (wordCount >= 1)
            {
                Console.WriteLine("Word is find");
                Console.WriteLine($"The count of repetitions of the word '{word}' in the text: {wordCount}");
                Console.WriteLine($"First value Index of {word} is " + index1);
            }
            else
                Console.WriteLine("Word is not find.");
        }
        static string ReplaceWord(string word, string text)
        {
            Console.WriteLine("\nReplace word.");
            Console.Write("Enter the word for replace: ");
            string replaceWord = Console.ReadLine();

            if (word == null || replaceWord == null || word == "" || replaceWord == "")
                Console.WriteLine("Please write correct word.");
            else
                text = CheckFunction(word, replaceWord, text);
            return text;
        }
        static string DeleteWord(string word, string text)
        {
            Console.WriteLine("\nDelete word.");

            if (word == null || word == "")
                Console.WriteLine("Please, write correct word.");
            else
                text = CheckFunction(word, "", text);
            return text;
        }
        static string CheckFunction(string firstWord, string secondWord, string text)
        {
            string[] words = text.Split(' ', ',', '.');
            int temp = 0, checkErorr = 0;
            do
            {
                for (int i = 0; i < words.Length; i++)
                {
                    if (words[i] == firstWord && words[i].Length == firstWord.Length)
                    {
                        text = text.Replace(firstWord, secondWord);
                        checkErorr++;
                    }
                    else if (words.Length - 1 == i && checkErorr > 0)//выскакивает=<
                    {
                        Console.WriteLine("Not correct start word was entered.");
                        break;
                    }
                }
                temp++;
            } while (temp < 1);
            return text;
        }
        static string FilePath1(string text, string path)//переименовать
        {
            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    text = reader.ReadToEnd();
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine("Not correct path was entered.");
            }
            Console.WriteLine(text);
            return text;
        }
        static string EnterFilePath(string text, string path)
        {
            string check = path;
            Console.Write("Enter the path to *.txt file (only english text): ");
            path = Console.ReadLine();
            text = FilePath1(text, path);

            if (check == "start")
                return text;
            else
                return path;
        }
        static void ChangeOpenFileHelper(string text)
        {
            Console.Write("Do you want /work with file/(y/n): ");
            string changeWorkWithFile = Console.ReadLine();
            if (changeWorkWithFile == "y")
                FileHelper(text, "");
            else if (changeWorkWithFile == "n")
                Console.WriteLine("No.");
            else
                Console.WriteLine("Error. Not correct symbol was entered.");
        }
        static void FileHelper(string text, string path)
        {
            string selection = "";
            while (selection != "q")
            {
                Console.Write("\nMenu for file work:\nCommands:\n v - view text from file\n r - record text in file\n " +
               "rw - rerecord text in file\n d - delete text from file or file\n c - create new file" +
               "\n q - exit from menu\n 0 - exit from program\n");
                Console.Write("Enter the command: ");
                selection = Console.ReadLine();
                switch (selection)
                {
                    case "v":
                        Console.WriteLine("View text.");
                        if (text == "" || text == null)
                            Console.WriteLine("No text for work. Please enter path for file.");
                        else
                            Console.WriteLine(text);
                        break;

                    case "r":
                        Console.Write("Record NEW TEXT (not replaced/deleted word at text) in end file(1) or record all file(2)?\nHow do you want record? (1/2): ");
                        string changeRecordMenu = Console.ReadLine();
                        path = EnterFilePath(text, "FileHelper");

                        if (changeRecordMenu == "1")
                        {
                            Console.Write("In end file.\nEnter text: ");
                            string textForFile = Console.ReadLine();

                            File.AppendAllText(path, $"{textForFile} ");
                        }
                        else if (changeRecordMenu == "2")
                        {
                            Console.Write("All file.\nEnter text: ");
                            string textForFile = Console.ReadLine();
                            File.WriteAllText(path, $"{textForFile} ");
                        }
                        else
                            Console.WriteLine("Not correct symbol was entered.");
                        break;

                    case "rw":
                        Console.WriteLine("Rerecord text from file.");
                        path = EnterFilePath(text, "FileHelper");

                        if (text == "" || text == null)
                            Console.WriteLine("No text for work. Please enter path for file.");
                        else
                            File.WriteAllText(path, $"{text} ");
                        break;

                    case "d":
                        Console.Write("Delete text from file(1) or file(2)?\nWhat do you want delete? (1/2): ");
                        string changeDeleteMenu = Console.ReadLine();
                        path = EnterFilePath(text, "FileHelper");

                        if (changeDeleteMenu == "1")
                            File.WriteAllText(path, "");
                        else if (changeDeleteMenu == "2")
                            File.Delete(path);
                        break;

                    case "c":
                        Console.Write("Create new file.\nEnter file name: ");
                        string nameFile = Console.ReadLine();
                        nameFile += ".txt";
                        nameFile = "..\\..\\..\\" + nameFile;
                        File.Create(nameFile);
                        break;

                    case "0":
                        Console.WriteLine("Exit.");
                        Environment.Exit(1);
                        break;
                    case "q":
                        Console.WriteLine("Exit from menu for file work.");
                        break;

                    default:
                        Console.WriteLine("Error. Not correct symbol was entered.");
                        break;
                }
            }
        }
    }
}
