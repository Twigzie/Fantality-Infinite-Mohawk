using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mohawk.Classes.Components;
using Mohawk.Classes.IO;
using Mohawk.Classes.Updates;

namespace Mohawk.Classes {

    internal static class FileHelpers {

        public static List<string> GetBitmapsFromPath(string path) {
            return Directory.GetFiles(path).Where(n => Path.GetExtension(n) == ".bitmap").ToList();
        }
        public static List<string> GetChunksFromBitmapName(string path, string name) {
            return Directory.GetFiles(path).Where(n => n.Contains(Path.GetFileName(name)) && n.ContainsStringCount("bitmap_resource_handle", 1) && n.Contains("bitmap_resource_handle.chunk")).OrderBy(s => s, new BitmapComparer()).ToList();
        }
        public static List<string> GetHandlesFromBitmapName(string path, string name) {
            return Directory.GetFiles(path).Where(n => n.Contains(Path.GetFileName(name)) && n.ContainsStringCount("bitmap_resource_handle", 1) && n.Contains("bitmap_resource_handle.chunk") == false).OrderBy(s => s, new BitmapComparer()).ToList();
        }
        public static List<string> GetChunksFromHandleName(string path, string name) {
            return Directory.GetFiles(path).Where(n => n.Contains(Path.GetFileName(name)) && n.ContainsStringCount("bitmap_resource_handle", 2) && n.Contains("bitmap_resource_handle.chunk")).OrderBy(s => s, new BitmapComparer()).ToList();
        }

    }
    internal static class CommandHelpers {

        public static void HelpCommand() {
            ConsoleHelpers.Title();
            Console.WriteLine("\t> Help");
            Console.WriteLine();
            Console.WriteLine("\t\t> Command: help");
            Console.WriteLine("\t\t> Description: Shows this message");
            Console.WriteLine();
            Console.WriteLine("\t> Clear");
            Console.WriteLine();
            Console.WriteLine("\t\t> Command: clear");
            Console.WriteLine("\t\t> Description: Clears this console.");
            Console.WriteLine();
            Console.WriteLine("\t> About:");
            Console.WriteLine();
            Console.WriteLine("\t\t> Command: about");
            Console.WriteLine("\t\t> Description: Shows application credits");
            Console.WriteLine();
            Console.WriteLine("\t> Github:");
            Console.WriteLine();
            Console.WriteLine("\t\t> Command: github");
            Console.WriteLine("\t\t> Description: Opens a link to the application repo on https://github.com/");
            Console.WriteLine();
            Console.WriteLine("\t> Updates");
            Console.WriteLine();
            Console.WriteLine("\t\t> Command: update");
            Console.WriteLine("\t\t> Description: Checks for updates via 'https://github.com/Twigzie/Fantality-Infinite-Mohawk/releases'");
            Console.WriteLine();
            Console.WriteLine("\t> Browse:");
            Console.WriteLine();
            Console.WriteLine("\t\t> Command: browse");
            Console.WriteLine("\t\t> Description: If an existing export folder if found, opens it. Otherwise, opens the applications directory.");
            Console.WriteLine();
            Console.WriteLine("\t> Extract:");
            Console.WriteLine();
            Console.WriteLine("\t\t> Command: extract p, d");
            Console.WriteLine("\t\t> Optional Arguments: p | d");
            Console.WriteLine("\t\t\t> p: A png image will be created");
            Console.WriteLine("\t\t\t> d: A dds image will be created");
            Console.WriteLine("\t\t> Description: Extracts a texture from a given bitmap. Note, if no argument is given, a dds will be created by default.");
            Console.WriteLine();
        }
        public static void AboutCommand() {
            ConsoleHelpers.Title();
            Console.WriteLine("\t> Made with love by: Twigzie IRL");
            Console.WriteLine("\t> Twitter: https://twitter.com/TwigzieIRL");
            Console.WriteLine("\t> Github: https://github.com/Twigzie/Fantality-Infinite-Mohawk");
            Console.WriteLine($"\t> Version: {Assembly.GetExecutingAssembly().GetName().Version}");
            Console.WriteLine("\t> Honorable Mentions:");
            Console.WriteLine("\t\t> Lord-Zedd: https://github.com/Lord-Zedd/H5Tags");
            Console.WriteLine("\t\t> Lord-Zedd: https://github.com/Lord-Zedd/MCCTexturePackDumper");
            Console.WriteLine("\t\t> MontagueM: https://github.com/MontagueM/HaloInfiniteModelExtractor");
            Console.WriteLine("\t\t> MontagueM: https://github.com/MontagueM/HaloInfiniteModuleUnpacker");
            Console.WriteLine("\t> Disclaimer:");
            Console.WriteLine("\t\t> Mohawk is provided 'as is' and I will not be responsible for any corrupt or deleted game assets.");
            Console.WriteLine("\t\t> It works with bitmap files, not module files.");
            Console.WriteLine("\t\t> For information about extracting module files, please visit https://github.com/MontagueM/HaloInfiniteModuleUnpacker");
            Console.WriteLine();
        }
        public static void ClearCommand() {
            ConsoleHelpers.Title();
            Console.WriteLine($"Version: {Assembly.GetExecutingAssembly().GetName().Version}");
            Console.WriteLine($"Directory: {Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}");
            Console.WriteLine();
            Console.WriteLine("--------------------------------------------------------------------------------------------------");
            Console.WriteLine();
            ConsoleHelpers.Message("Welcome!");
            ConsoleHelpers.Message("Enter a command or type 'help' for a list of available ones.");
            Console.WriteLine();
        }
        public static void GithubCommand() {
            Console.WriteLine();
            ConsoleHelpers.Info("Opening 'https://github.com/Twigzie/Fantality-Infinite-Mohawk'...");
            Console.WriteLine();
            Process.Start("https://github.com/Twigzie/Fantality-Infinite-Mohawk");
        }
        public static void BrowseCommand() {
            try {

                Console.WriteLine();

                var root = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                if (string.IsNullOrEmpty(root))
                    throw new Exception("Unable to open the specified directory.");

                var path = Path.Combine(root, "export");
                if (Directory.Exists(path)) {
                    ConsoleHelpers.Info("Existing directory was found, browsing.");
                    Console.WriteLine($"\t> '{path}'");
                    Process.Start(path);
                }
                else {
                    ConsoleHelpers.Info("No export directory was found, browsing application directory.");
                    Console.WriteLine($"\t> '{root}'");
                    Process.Start(root);
                }

                Console.WriteLine();

            }
            catch (Exception ex) {
                ConsoleHelpers.Error(ex);
            }
        }

        public static async void UpdateCommand() {
            await UpdateCommandAsync();
        }
        private static async Task UpdateCommandAsync() {
            try {

                ConsoleHelpers.Title();
                ConsoleHelpers.Info("Checking for updates...");
                Console.WriteLine();

                var update = await Update.GetUpdates();
                if (update.IsAvailable) {

                    Console.WriteLine($"\t> Available: {update.Available}");
                    Console.WriteLine($"\t> Current: {update.Current}");
                    Console.WriteLine();
                    Console.WriteLine("\t\t> An update is available.");
                    Console.WriteLine();
                    Console.WriteLine($"\t\t> Details: {update.Details}");
                    Console.WriteLine($"\t\t> Download: {update.Url}");
                    Console.WriteLine();
                    ConsoleHelpers.Info("Checking for updates...Done!");
                    Console.WriteLine();

                }
                else {

                    Console.WriteLine($"\t> Available: {update.Available}");
                    Console.WriteLine($"\t> Current: {update.Current}");
                    Console.WriteLine();
                    Console.WriteLine("\t> No updates available.");
                    Console.WriteLine();
                    ConsoleHelpers.Info("Checking for updates...Done!");
                    Console.WriteLine();

                }

            }
            catch {
                Console.WriteLine("\t> An error occurred while checking for updates.");
                Console.WriteLine();
                ConsoleHelpers.Info("Checking for updates...Failed!");
                Console.WriteLine();
            }
        }
        public static async void ExtractCommand(Command command) {
            try {

                ConsoleHelpers.Title();
                ConsoleHelpers.Info("Extracting Texture(s)...");
                Console.WriteLine();

                using (var ofd = new OpenFileDialog()) {

                    ofd.Title = "Select a bitmap for extraction.";
                    ofd.Filter = "Bitmap Files | *.bitmap";
                    ofd.Multiselect = false;
                    if (ofd.ShowDialog() != DialogResult.OK) {
                        Console.WriteLine("\t> Canceled!");
                        Console.WriteLine();
                        ConsoleHelpers.Info("Extracting Texture(s)...Canceled!");
                        Console.WriteLine();
                    }
                    else {

                        await ExtractFileAsync(ofd.FileName, command);

                        Console.WriteLine();
                        ConsoleHelpers.Info("Extracting Texture(s)...Done!");
                        Console.WriteLine();

                    }

                }

            }
            catch (Exception ex) {
                ConsoleHelpers.Error(ex);
                ConsoleHelpers.Info("Extracting Texture(s)...Failed!");
                Console.WriteLine();
            }
        }
        private static async Task ExtractFileAsync(string filename, Command command) {
            try {

                Console.WriteLine($"\t> Processing '{Path.GetFileName(filename)}'...");
                Console.WriteLine();

                var root = Path.GetDirectoryName(filename);
                var chunks = FileHelpers.GetChunksFromBitmapName(root, Path.GetFileName(filename));
                var handles = FileHelpers.GetHandlesFromBitmapName(root, Path.GetFileName(filename));
                var mode =
                    command.Value.ToLower() == "d"
                        ? Mode.Texture
                        : command.Value.ToLower() == "p"
                            ? Mode.Image
                            : Mode.Texture;

                Console.WriteLine($"\t\t> Chunks '{chunks?.Count}'");
                Console.WriteLine($"\t\t> Handles '{handles?.Count}'");
                Console.WriteLine($"\t\t> Mode: '{mode}'");
                Console.WriteLine();

                using (var reader = new BitmapReader(filename)) {

                    if (chunks?.Count <= 0 && handles?.Count <= 0) {
                        Console.WriteLine("\t\t> File has no linked resources, reading as single resource bitmap.");
                        await reader.Extract(mode);
                    }
                    else {

                        if (chunks?.Count >= 1) {
                            Console.WriteLine($"\t\t> Found '{chunks.Count}' linked resource chunks, reading as chunked resource bitmap.");
                            await reader.ExtractChunks(chunks, mode);
                        }
                        else {
                            Console.WriteLine($"\t\t> Found '{handles?.Count}' linked resource handles, reading as handle resource bitmap.");
                            await reader.ExtractHandles(handles, mode);
                        }

                    }

                }

                Console.WriteLine();
                Console.WriteLine($"\t> Processing '{Path.GetFileName(filename)}'...Done!");

            }
            catch (Exception ex) {
                Console.WriteLine();
                Console.WriteLine($"\t> Failed! '{ex.Message}'");
            }

        }

    }
    internal static class ConsoleHelpers {

        public static void Title() {
            Console.Clear();
            Console.WriteLine(@"--------------------------------------------------------------------------------------------------");
            Console.WriteLine(@"            ___           ___           ___           ___           ___           ___             ");
            Console.WriteLine(@"           /  /\         /  /\         /  /\         /  /\         /  /\         /  /\            ");
            Console.WriteLine(@"          /  /::|       /  /::\       /  /:/        /  /::\       /  /:/_       /  /:/            ");
            Console.WriteLine(@"         /  /:|:|      /  /:/\:\     /  /:/        /  /:/\:\     /  /:/ /\     /  /:/             ");
            Console.WriteLine(@"        /  /:/|:|__   /  /:/  \:\   /  /::\ ___   /  /::\ \:\   /  /:/ /:/_   /  /::\____         ");
            Console.WriteLine(@"       /__/:/_|::::\ /__/:/ \__\:\ /__/:/\:\  /\ /__/:/\:\_\:\ /__/:/ /:/ /\ /__/:/\:::::\        ");
            Console.WriteLine(@"       \__\/  /~~/:/ \  \:\ /  /:/ \__\/  \:\/:/ \__\/  \:\/:/ \  \:\/:/ /:/ \__\/~|:|~~~~        ");
            Console.WriteLine(@"             /  /:/   \  \:\  /:/       \__\::/       \__\::/   \  \::/ /:/     |  |:|            ");
            Console.WriteLine(@"            /  /:/     \  \:\/:/        /  /:/        /  /:/     \  \:\/:/      |  |:|            ");
            Console.WriteLine(@"           /__/:/       \  \::/        /__/:/        /__/:/       \  \::/       |__|:|            ");
            Console.WriteLine(@"           \__\/         \__\/         \__\/         \__\/         \__\/         \__\|            ");
            Console.WriteLine(@"                                                                                                  ");
            Console.WriteLine(@"                           -- A texture extractor for Halo Infinite --                            ");
            Console.WriteLine(@"");
            Console.WriteLine(@"--------------------------------------------------------------------------------------------------");
            Console.WriteLine();
        }
        public static void Close() {
            Message("Termination imminent! Exiting...");
            Console.ReadKey();
        }

        public static void Message(string message) {
            Console.WriteLine($"[{DateTime.Now.ToLongTimeString()}] Mohawk: {message}");
        }
        public static void Info(string message) {
            Console.WriteLine($"[{DateTime.Now.ToLongTimeString()}] Info: {message}");
        }
        public static void Warning(string message) {
            Console.WriteLine($"[{DateTime.Now.ToLongTimeString()}] Warning: {message}");
        }
        public static void Error(Exception source) {
            Title();
            Console.WriteLine($"[{DateTime.Now.ToLongTimeString()}] Exception: {source.Message}");
            Console.WriteLine($"[{DateTime.Now.ToLongTimeString()}] Details:");
            Console.WriteLine(source.StackTrace);
        }

    }

}