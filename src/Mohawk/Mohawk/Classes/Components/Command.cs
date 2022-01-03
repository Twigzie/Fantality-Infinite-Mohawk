namespace Mohawk.Classes.Components {

    public class Command {
        public string Name { get; set; }
        public string Value { get; set; }
        public bool Valid {
            get {
                switch (Name.ToLower()) {
                    case "help":
                    case "clear":
                    case "about":
                    case "github":
                    case "updates":
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