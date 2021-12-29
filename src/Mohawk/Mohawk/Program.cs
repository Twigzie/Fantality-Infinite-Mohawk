using System;
using Mohawk.Classes.Commands;
using static Mohawk.Classes.CommandHelpers;
using static Mohawk.Classes.ConsoleHelpers;

namespace Mohawk {

    public class Program {

        public static void Main(string[] args) {

            try {

                Init();

                while (true) {

                    var command = new Command(Console.ReadLine());
                    if (command.Valid == false)
                        continue;
                    else {
                        switch (command.Name.ToLower()) {
                            case "help": 
                                HelpCommand(); 
                                continue;
                            case "clear":
                                ClearCommand(); 
                                continue;
                            case "about":
                                AboutCommand(); 
                                continue;
                            case "update":
                                UpdateCommand();
                                continue;
                            case "github":
                                GithubCommand();
                                continue;
                            case "browse":
                                BrowseCommand(); 
                                continue;
                            case "extract":
                                ExtractCommand(command);
                                continue;
                            default:
                                Oops(command.Name);
                                continue;
                        }
                    }

                }

            }
            catch (Exception ex) {
                Error(ex);
            }
            finally {
                Close();
            }

        }

        private static void Init() {

            Console.Title = $"Mohawk | {Environment.UserName}";

            ClearCommand();

        }

    }

}