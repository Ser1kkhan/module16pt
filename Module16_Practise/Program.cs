using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

class Program
{
    static void Main()
    {


        
        Console.WriteLine("Welcome to the directory watcher!");

        
        Console.WriteLine("Press 'Q' to quit the application.");

        string directoryPath = GetUserInput("Enter the path to the directory to watch: ");
        string logFilePath = GetUserInput("Enter the path to the log file: ");


        
        try
        {
            using (FileSystemWatcher watcher = new FileSystemWatcher(directoryPath))
            {

                
                watcher.IncludeSubdirectories = true;
                watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;

                
                watcher.Created += OnChanged;
                watcher.Deleted += OnChanged;
                watcher.Renamed += OnRenamed;
                watcher.EnableRaisingEvents = true;

                Console.WriteLine($"Watching for changes in the directory: {directoryPath}");
                while (Console.ReadKey().Key != ConsoleKey.Q)
                {
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }



    
    static void OnChanged(object sender, FileSystemEventArgs e)
    {
        LogChange($"[{DateTime.Now}] {e.ChangeType}: {e.FullPath}");
    }

    static void OnRenamed(object sender, RenamedEventArgs e)
    {
        LogChange($"[{DateTime.Now}] {e.ChangeType}: {e.OldFullPath} renamed to {e.FullPath}");
    }


    
    static void LogChange(string logMessage)
    {
        string logFilePath = "changes.log";

        try
        {
            File.AppendAllText(logFilePath, logMessage + Environment.NewLine);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error writing to log file: {ex.Message}");
        }
    }

    static string GetUserInput(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine();
    }
}
