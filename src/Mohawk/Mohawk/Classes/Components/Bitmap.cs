namespace Mohawk.Classes.Components {

    internal class Bitmap {
        public int Width { get; set; }
        public int Height { get; set; }
        public int Format { get; set; }
        public byte[] Data { get; set; }
        public uint Length { get; set; }
        public string Filename { get; set; }
        public Bitmap(string filename) {
            Filename = filename;
        }
        public override string ToString() {
            return $"Width: {Width}, Height: {Height}, Format: {Format}, Length: {Length}";
        }
    }

}