﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Mohawk.Classes.ConsoleHelpers;

namespace Mohawk.Classes.Commands {

    public class Command {
        public string Name { get; set; }
        public string Value { get; set; }
        public bool Valid {
            get {
                switch (Name.ToLower()) {
                    case "help":
                    case "home":
                    case "clear":
                    case "about":
                    case "github":
                    case "browse":
                    case "extract": return true;
                    default:
                        return (string.IsNullOrEmpty(Name) == false);
                }
            }
        }
        public Command(string source) {

            var args = source.Split(' ');
            Name = args[0].ToLower();
            Value = (args.Length <= 1)
                ? ""
                : (args.Length >= 2)
                    ? source.Substring(Name.Length + 1)
                    : args[1];

        }
    }

}