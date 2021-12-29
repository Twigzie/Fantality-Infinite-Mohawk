using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Mohawk.Classes.Commands;
using Mohawk.Classes.Updates;

namespace Mohawk.Classes {

    internal static class FileHelpers {

        public static List<string> GetBitmapsFromPath(string path) {
            try {
                return Directory.GetFiles(path).Where(n => Path.GetExtension(n) == ".bitmap").ToList();
            }
            catch (Exception) {
                throw;
            }
        }
        public static List<string> GetChunksFromBitmapName(string path, string name) {
            try {
                return Directory.GetFiles(path).Where(n => n.Contains(Path.GetFileName(name)) && n.Contains("bitmap_resource_handle") == false && n.Contains("bitmap_resource_handle.chunk")).ToList();
            }
            catch (Exception) {
                throw;
            }
        }
        public static List<string> GetHandlesFromBitmapName(string path, string name) {
            try {
                return Directory.GetFiles(path).Where(n => n.Contains(Path.GetFileName(name)) && n.Contains("bitmap_resource_handle") && n.Contains("bitmap_resource_handle.chunk") == false).ToList();
            }
            catch (Exception) {
                throw;
            }
        }
        public static List<string> GetChunksFromHandleName(string path, string name) {
            try {
                return Directory.GetFiles(path).Where(n => n.Contains(Path.GetFileName(name)) && n.Contains("bitmap_resource_handle") && n.Contains("bitmap_resource_handle.chunk")).ToList();
            }
            catch (Exception) {
                throw;
            }
        }

    }
    internal static class CommandHelpers {

        public static void HelpCommand() {
            ConsoleHelpers.Title();
            Console.WriteLine($"\t> Help");
            Console.WriteLine();
            Console.WriteLine($"\t\t> Command: help");
            Console.WriteLine($"\t\t> Description: Shows this message");
            Console.WriteLine();
            Console.WriteLine($"\t> Clear");
            Console.WriteLine();
            Console.WriteLine($"\t\t> Command: clear");
            Console.WriteLine($"\t\t> Description: Clears this console.");
            Console.WriteLine();
            Console.WriteLine($"\t> About:");
            Console.WriteLine();
            Console.WriteLine($"\t\t> Command: about");
            Console.WriteLine($"\t\t> Description: Shows application credits");
            Console.WriteLine();
            Console.WriteLine($"\t> Github:");
            Console.WriteLine();
            Console.WriteLine($"\t\t> Command: github");
            Console.WriteLine($"\t\t> Description: Opens a link to the application repo on https://github.com/");
            Console.WriteLine();
            Console.WriteLine($"\t> Updates");
            Console.WriteLine();
            Console.WriteLine($"\t\t> Command: update");
            Console.WriteLine($"\t\t> Description: Checks for updates via 'https://github.com/Twigzie/Fantality-Infinite-Mohawk/releases'");
            Console.WriteLine();
            Console.WriteLine($"\t> Browse:");
            Console.WriteLine();
            Console.WriteLine($"\t\t> Command: browse");
            Console.WriteLine($"\t\t> Description: If an existing export folder if found, opens it. Otherwise, opens the applications directory.");
            Console.WriteLine();
            Console.WriteLine($"\t> Extract:");
            Console.WriteLine();
            Console.WriteLine($"\t\t> Command: extract");
            Console.WriteLine($"\t\t> Optional Arguments: file | directory");
            Console.WriteLine($"\t\t\t> File: The file to extract");
            Console.WriteLine($"\t\t\t> Directory: The directory of files to extract");
            Console.WriteLine($"\t\t> Description: Extracts textures from a given file or directory.");
            Console.WriteLine();
        }
        public static void AboutCommand() {
            ConsoleHelpers.Title();
            Console.WriteLine($"\t> Made with love by: Twigzie IRL");
            Console.WriteLine($"\t> Twitter: https://twitter.com/TwigzieIRL");
            Console.WriteLine($"\t> Github: https://github.com/Twigzie/Fantality-Infinite-Mohawk");
            Console.WriteLine($"\t> Version: {Assembly.GetExecutingAssembly().GetName().Version}");
            Console.WriteLine($"\t> Disclaimer:");
            Console.WriteLine($"\t\t> Mohawk is provided 'as is' and I will not be responsible for any corrupt or deleted game assets.");
            Console.WriteLine($"\t\t> It works with wpf_font files, not module files.");
            Console.WriteLine($"\t\t> For information about extracting module files, please visit https://github.com/MontagueM/HaloInfiniteModuleUnpacker");
            Console.WriteLine();
        }
        public static void ClearCommand() {
            ConsoleHelpers.Title();
            Console.WriteLine($"Version: {Assembly.GetExecutingAssembly().GetName().Version}");
            Console.WriteLine($"Directory: {Directory.GetParent(Assembly.GetExecutingAssembly().Location).Name}");
            Console.WriteLine();
            Console.WriteLine("--------------------------------------------------------------------------------------------------");
            Console.WriteLine();
            ConsoleHelpers.Message("Welcome!");
            ConsoleHelpers.Message("Enter a command or type 'help' for a list of available ones.");
            Console.WriteLine();
        }
        public static void GithubCommand() {
            Console.WriteLine();
            ConsoleHelpers.Info($"Opening 'https://github.com/Twigzie/Fantality-Infinite-Mohawk'...");
            Console.WriteLine();
            Process.Start("https://github.com/Twigzie/Fantality-Infinite-Mohawk");
        }
        public static void BrowseCommand() {
            try {

                Console.WriteLine();

                var root = Directory.GetParent(Assembly.GetExecutingAssembly().Location);
                var path = Path.Combine(root.FullName, "export");
                if (Directory.Exists(path)) {
                    ConsoleHelpers.Info($"Existing directory was found, browsing.");
                    Console.WriteLine($"\t> '{path}'");
                    Process.Start(path);
                }
                else {
                    ConsoleHelpers.Info($"No export directory was found, browsing application directory.");
                    Console.WriteLine($"\t> '{root.FullName}'");
                    Process.Start(root.FullName);
                }

                Console.WriteLine();

            }
            catch (Exception ex) {
                ConsoleHelpers.Error(ex);
            }
        }

        //Async
        public static async void UpdateCommand() {
            await UpdateCommandAsync();
        }
        private static async Task UpdateCommandAsync() {
            try {

                ConsoleHelpers.Title();
                ConsoleHelpers.Info($"Checking for updates...");
                Console.WriteLine();

                var update = await Update.GetUpdates();
                if (update.IsAvailable) {

                    Console.WriteLine($"\t> Available: {update.Available}");
                    Console.WriteLine($"\t> Current: {update.Current}");
                    Console.WriteLine();
                    Console.WriteLine($"\t\t> An update is available.");
                    Console.WriteLine();
                    Console.WriteLine($"\t\t> Details: {update.Details}");
                    Console.WriteLine($"\t\t> Download: {update.Url}");
                    Console.WriteLine();
                    ConsoleHelpers.Info($"Checking for updates...Done!");
                    Console.WriteLine();

                }
                else {

                    Console.WriteLine($"\t> Available: {update.Available}");
                    Console.WriteLine($"\t> Current: {update.Current}");
                    Console.WriteLine();
                    Console.WriteLine($"\t> No updates available.");
                    Console.WriteLine();
                    ConsoleHelpers.Info($"Checking for updates...Done!");
                    Console.WriteLine();

                }

            }
            catch {
                Console.WriteLine($"\t> An error occurred while checking for updates.");
                Console.WriteLine();
                ConsoleHelpers.Info($"Checking for updates...Failed!");
                Console.WriteLine();
            }
        }
        public static async void ExtractCommand(Command command) {
            await ExtractCommandAsync(command);
        }
        private static async Task ExtractCommandAsync(Command command) {
            try {

                ConsoleHelpers.Title();
                ConsoleHelpers.Info($"Extracting Textures...");
                Console.WriteLine();

            }
            catch (Exception ex) {
                ConsoleHelpers.Error(ex);
                ConsoleHelpers.Info($"Extracting Textures...Failed!");
                Console.WriteLine();
            }
        }

    }
    internal static class ConsoleHelpers {

        private static string[] Insults = new string[] {
            "If laughter is the best medicine, your face must be curing the world.",
            "You're so ugly, you scared the crap out of the toilet.",
            "No I'm not insulting you, I'm describing you.",
            "It's better to let someone think you are an Idiot than to open your mouth and prove it.",
            "If I had a face like yours, I'd sue my parents.",
            "Your birth certificate is an apology letter from the condom factory.",
            "I guess you prove that even god makes mistakes sometimes.",
            "The only way you'll ever get laid is if you crawl up a chicken's ass and wait.",
            "You're so fake, Barbie is jealous.",
            "I’m jealous of people that don’t know you!",
            "My psychiatrist told me I was crazy and I said I want a second opinion. He said okay, you're ugly too.",
            "You're so ugly, when your mom dropped you off at school she got a fine for littering.",
            "If I wanted to kill myself I'd climb your ego and jump to your IQ.",
            "You must have been born on a highway because that's where most accidents happen.",
            "Brains aren't everything. In your case they're nothing.",
            "I don't know what makes you so stupid, but it really works.",
            "Your family tree must be a cactus because everybody on it is a prick.",
            "I can explain it to you, but I can’t understand it for you.",
            "Roses are red violets are blue, God made me pretty, what happened to you?",
            "Behind every fat woman there is a beautiful woman. No seriously, your in the way.",
            "Calling you an idiot would be an insult to all the stupid people.",
            "You, sir, are an oxygen thief!",
            "Some babies were dropped on their heads but you were clearly thrown at a wall.",
            "Why don't you go play in traffic.",
            "Please shut your mouth when you’re talking to me.",
            "I'd slap you, but that would be animal abuse.",
            "They say opposites attract. I hope you meet someone who is good-looking, intelligent, and cultured.",
            "Stop trying to be a smart ass, you're just an ass.",
            "The last time I saw something like you, I flushed it.",
            "'m busy now. Can I ignore you some other time?",
            "You have Diarrhea of the mouth; constipation of the ideas.",
            "If ugly were a crime, you'd get a life sentence.",
            "Your mind is on vacation but your mouth is working overtime.",
            "I can lose weight, but you’ll always be ugly.",
            "Why don't you slip into something more comfortable... like a coma.",
            "Shock me, say something intelligent.",
            "If your gonna be two faced, honey at least make one of them pretty.",
            "Keep rolling your eyes, perhaps you'll find a brain back there.",
            "You are not as bad as people say, you are much, much worse.",
            "Don't like my sarcasm, well I don't like your stupid.",
            "I don't know what your problem is, but I'll bet it's hard to pronounce.",
            "You get ten times more girls than me? ten times zero is zero...",
            "There is no vaccine against stupidity.",
            "You're the reason the gene pool needs a lifeguard.",
            "Sure, I've seen people like you before - but I had to pay an admission.",
            "How old are you? - Wait I shouldn't ask, you can't count that high.",
            "Have you been shopping lately? They're selling lives, you should go get one.",
            "You're like Monday mornings, nobody likes you.",
            "Of course I talk like an idiot, how else would you understand me?",
            "All day I thought of you... I was at the zoo.",
            "To make you laugh on Saturday, I need to you joke on Wednesday.",
            "You're so fat, you could sell shade.",
            "I'd like to see things from your point of view but I can't seem to get my head that far up my ass.",
            "Don't you need a license to be that ugly?",
            "My friend thinks he is smart. He told me an onion is the only food that makes you cry, so I threw a coconut at his face.",
            "Your house is so dirty you have to wipe your feet before you go outside.",
            "If you really spoke your mind, you'd be speechless.",
            "Stupidity is not a crime so you are free to go.",
            "You are so old, when you were a kid rainbows were black and white.",
            "If I told you that I have a piece of dirt in my eye, would you move?",
            "You so dumb, you think Cheerios are doughnut seeds.",
            "So, a thought crossed your mind? Must have been a long and lonely journey.",
            "You are so old, your birth-certificate expired.",
            "Every time I'm next to you, I get a fierce desire to be alone.",
            "You're so dumb that you got hit by a parked car.",
            "Keep talking, someday you'll say something intelligent!",
            "Insult about saying something intelligent",
            "You're so fat, you leave footprints in concrete.",
            "How did you get here? Did someone leave your cage open?",
            "Pardon me, but you've obviously mistaken me for someone who gives a damn.",
            "Wipe your mouth, there's still a tiny bit of bullshit around your lips.",
            "Don't you have a terribly empty feeling - in your skull?",
            "As an outsider, what do you think of the human race?",
            "Just because you have one doesn't mean you have to act like one.",
            "We can always tell when you are lying. Your lips move.",
            "Are you always this stupid or is today a special occasion?"
        };

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
        public static void Oops(string command) {
            Console.WriteLine();
            Info($"Oops... {GetInsult()}");
            Info($"But really though, the command '{command}' is not valid.");
            Info($"For a list of available commands, type 'help'.");
            Console.WriteLine();
        }
        public static void Error(Exception source) {
            Title();
            Console.WriteLine($"[{DateTime.Now.ToLongTimeString()}] Exception: {source.Message}");
            Console.WriteLine($"[{DateTime.Now.ToLongTimeString()}] Details:");
            Console.WriteLine(source.StackTrace);
        }

        public static string GetInsult() {
            return Insults[new Random(Guid.NewGuid().GetHashCode()).Next(0, Insults.Length - 1)];
        }
        public static bool GetResponse(string value) {
            return Console.ReadLine().ToLower() == value.ToLower();
        }

    }

}