using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using ImageMagick;
using Mohawk.Classes.Components;

namespace Mohawk.Classes.IO {

    internal class BitmapReader : BinaryReader {

        #region Private

        private string[] Formats { get; } = {
            "DXGI_FORMAT_UNKNOWN",
            "DXGI_FORMAT_R32G32B32A32_TYPELESS",
            "DXGI_FORMAT_R32G32B32A32_FLOAT",
            "DXGI_FORMAT_R32G32B32A32_UINT",
            "DXGI_FORMAT_R32G32B32A32_SINT",
            "DXGI_FORMAT_R32G32B32_TYPELESS",
            "DXGI_FORMAT_R32G32B32_FLOAT",
            "DXGI_FORMAT_R32G32B32_UINT",
            "DXGI_FORMAT_R32G32B32_SINT",
            "DXGI_FORMAT_R16G16B16A16_TYPELESS",
            "DXGI_FORMAT_R16G16B16A16_FLOAT",
            "DXGI_FORMAT_R16G16B16A16_UNORM",
            "DXGI_FORMAT_R16G16B16A16_UINT",
            "DXGI_FORMAT_R16G16B16A16_SNORM",
            "DXGI_FORMAT_R16G16B16A16_SINT",
            "DXGI_FORMAT_R32G32_TYPELESS",
            "DXGI_FORMAT_R32G32_FLOAT",
            "DXGI_FORMAT_R32G32_UINT",
            "DXGI_FORMAT_R32G32_SINT",
            "DXGI_FORMAT_R32G8X24_TYPELESS",
            "DXGI_FORMAT_D32_FLOAT_S8X24_UINT",
            "DXGI_FORMAT_R32_FLOAT_X8X24_TYPELESS",
            "DXGI_FORMAT_X32_TYPELESS_G8X24_UINT",
            "DXGI_FORMAT_R10G10B10A2_TYPELESS",
            "DXGI_FORMAT_R10G10B10A2_UNORM",
            "DXGI_FORMAT_R10G10B10A2_UINT",
            "DXGI_FORMAT_R11G11B10_FLOAT",
            "DXGI_FORMAT_R8G8B8A8_TYPELESS",
            "DXGI_FORMAT_R8G8B8A8_UNORM",
            "DXGI_FORMAT_R8G8B8A8_UNORM_SRGB",
            "DXGI_FORMAT_R8G8B8A8_UINT",
            "DXGI_FORMAT_R8G8B8A8_SNORM",
            "DXGI_FORMAT_R8G8B8A8_SINT",
            "DXGI_FORMAT_R16G16_TYPELESS",
            "DXGI_FORMAT_R16G16_FLOAT",
            "DXGI_FORMAT_R16G16_UNORM",
            "DXGI_FORMAT_R16G16_UINT",
            "DXGI_FORMAT_R16G16_SNORM",
            "DXGI_FORMAT_R16G16_SINT",
            "DXGI_FORMAT_R32_TYPELESS",
            "DXGI_FORMAT_D32_FLOAT",
            "DXGI_FORMAT_R32_FLOAT",
            "DXGI_FORMAT_R32_UINT",
            "DXGI_FORMAT_R32_SINT",
            "DXGI_FORMAT_R24G8_TYPELESS",
            "DXGI_FORMAT_D24_UNORM_S8_UINT",
            "DXGI_FORMAT_R24_UNORM_X8_TYPELESS",
            "DXGI_FORMAT_X24_TYPELESS_G8_UINT",
            "DXGI_FORMAT_R8G8_TYPELESS",
            "DXGI_FORMAT_R8G8_UNORM",
            "DXGI_FORMAT_R8G8_UINT",
            "DXGI_FORMAT_R8G8_SNORM",
            "DXGI_FORMAT_R8G8_SINT",
            "DXGI_FORMAT_R16_TYPELESS",
            "DXGI_FORMAT_R16_FLOAT",
            "DXGI_FORMAT_D16_UNORM",
            "DXGI_FORMAT_R16_UNORM",
            "DXGI_FORMAT_R16_UINT",
            "DXGI_FORMAT_R16_SNORM",
            "DXGI_FORMAT_R16_SINT",
            "DXGI_FORMAT_R8_TYPELESS",
            "DXGI_FORMAT_R8_UNORM",
            "DXGI_FORMAT_R8_UINT",
            "DXGI_FORMAT_R8_SNORM",
            "DXGI_FORMAT_R8_SINT",
            "DXGI_FORMAT_A8_UNORM",
            "DXGI_FORMAT_R1_UNORM",
            "DXGI_FORMAT_R9G9B9E5_SHAREDEXP",
            "DXGI_FORMAT_R8G8_B8G8_UNORM",
            "DXGI_FORMAT_G8R8_G8B8_UNORM",
            "DXGI_FORMAT_BC1_TYPELESS",
            "DXGI_FORMAT_BC1_UNORM",
            "DXGI_FORMAT_BC1_UNORM_SRGB",
            "DXGI_FORMAT_BC2_TYPELESS",
            "DXGI_FORMAT_BC2_UNORM",
            "DXGI_FORMAT_BC2_UNORM_SRGB",
            "DXGI_FORMAT_BC3_TYPELESS",
            "DXGI_FORMAT_BC3_UNORM",
            "DXGI_FORMAT_BC3_UNORM_SRGB",
            "DXGI_FORMAT_BC4_TYPELESS",
            "DXGI_FORMAT_BC4_UNORM",
            "DXGI_FORMAT_BC4_SNORM",
            "DXGI_FORMAT_BC5_TYPELESS",
            "DXGI_FORMAT_BC5_UNORM",
            "DXGI_FORMAT_BC5_SNORM",
            "DXGI_FORMAT_B5G6R5_UNORM",
            "DXGI_FORMAT_B5G5R5A1_UNORM",
            "DXGI_FORMAT_B8G8R8A8_UNORM",
            "DXGI_FORMAT_B8G8R8X8_UNORM",
            "DXGI_FORMAT_R10G10B10_XR_BIAS_A2_UNORM",
            "DXGI_FORMAT_B8G8R8A8_TYPELESS",
            "DXGI_FORMAT_B8G8R8A8_UNORM_SRGB",
            "DXGI_FORMAT_B8G8R8X8_TYPELESS",
            "DXGI_FORMAT_B8G8R8X8_UNORM_SRGB",
            "DXGI_FORMAT_BC6H_TYPELESS",
            "DXGI_FORMAT_BC6H_UF16",
            "DXGI_FORMAT_BC6H_SF16",
            "DXGI_FORMAT_BC7_TYPELESS",
            "DXGI_FORMAT_BC7_UNORM",
            "DXGI_FORMAT_BC7_UNORM_SRGB",
            "DXGI_FORMAT_AYUV",
            "DXGI_FORMAT_Y410",
            "DXGI_FORMAT_Y416",
            "DXGI_FORMAT_NV12",
            "DXGI_FORMAT_P010",
            "DXGI_FORMAT_P016",
            "DXGI_FORMAT_420_OPAQUE",
            "DXGI_FORMAT_YUY2",
            "DXGI_FORMAT_Y210",
            "DXGI_FORMAT_Y216",
            "DXGI_FORMAT_NV11",
            "DXGI_FORMAT_AI44",
            "DXGI_FORMAT_IA44",
            "DXGI_FORMAT_P8",
            "DXGI_FORMAT_A8P8",
            "DXGI_FORMAT_B4G4R4A4_UNORM",
            "DXGI_FORMAT_P208",
            "DXGI_FORMAT_V208",
            "DXGI_FORMAT_V408",
            "DXGI_FORMAT_SAMPLER_FEEDBACK_MIN_MIP_OPAQUE",
            "DXGI_FORMAT_SAMPLER_FEEDBACK_MIP_REGION_USED_OPAQUE",
            "DXGI_FORMAT_FORCE_UINT"
        };
        private readonly List<byte[]> Headers = new List<byte[]>
        {
            new byte[] {
                0x44, 0x44, 0x53, 0x20,
                0x7C, 0x00, 0x00, 0x00,
                0x07, 0x10, 0x0A, 0x00 },
            new byte[] {
                0x01, 0x00, 0x00, 0x00,
                0x01, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x20, 0x00, 0x00, 0x00,
                0x04, 0x00, 0x00, 0x00,
                0x44, 0x58, 0x31, 0x30,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x10, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00
            },
            new byte[] {
                0x03, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x01, 0x00, 0x00, 0x00,
                0x01, 0x00, 0x00, 0x00
            }
        };

        #endregion
        #region Properties

        public long Length => BaseStream.Length;
        public long Position => BaseStream.Position;
        public string Filename {
            get;
        }

        #endregion

        public BitmapReader(string filename) : base(new MemoryStream(File.ReadAllBytes(filename))) {
            Filename = filename;
        }

        public void Move(int offset) {
            BaseStream.Position = offset;
        }

        public async Task Extract(Mode mode) {
            try {

                Console.WriteLine();
                Console.WriteLine("\t\t\t> Reading Bitmap Data:");

                var bitmap = new Bitmap(Filename);

                Move(708);
                bitmap.Width = ReadUInt16();
                if (bitmap.Width <= 0)
                    throw new Exception($"The specified texture width is not a valid. '{bitmap.Width}'");
                Console.WriteLine($"\t\t\t\t> Width: {bitmap.Width}");

                Move(710);
                bitmap.Height = ReadUInt16();
                if (bitmap.Height <= 0)
                    throw new Exception($"The specified texture height is not a valid. '{bitmap.Height}'");
                Console.WriteLine($"\t\t\t\t> Height: {bitmap.Height}");

                Move(768);
                bitmap.Length = ReadUInt32();
                if (bitmap.Length <= 0)
                    throw new Exception($"The specified texture length is not a valid. '{bitmap.Length}'");
                Console.WriteLine($"\t\t\t\t> Length: {bitmap.Length}");

                Move(777);
                bitmap.Format = Convert.ToInt32(ReadSByte());
                if (bitmap.Format > Formats.Length)
                    throw new Exception($"The specified texture format is not a valid. '{bitmap.Format}'");
                Console.WriteLine($"\t\t\t\t> Format: {bitmap.Format} ({Formats[bitmap.Format]})");

                Move(820);
                if (Length != Position + bitmap.Length)
                    throw new Exception($"The specified texture data does not match its bitmap length. '{Filename}'");
                bitmap.Data = ReadBytes((int)bitmap.Length);
                Console.WriteLine($"\t\t\t\t> Length: {bitmap.Length}");

                await Write(bitmap, mode);

            }
            catch (Exception ex) {
                throw ex;
            }
        }
        public async Task ExtractChunks(List<string> chunks, Mode mode) {
            try {

                Console.WriteLine();
                Console.WriteLine("\t\t\t> Reading Bitmap Data:");

                Move(128);
                var chunkCount = ReadInt32() / 16;
                if (chunkCount <= 0)
                    throw new Exception($"The specified texture count is not a valid. '{chunkCount}'");
                Console.WriteLine($"\t\t\t\t> Count: {chunkCount}");

                Move(777);
                var chunkFormat = Convert.ToInt32(ReadSByte());
                if (chunkFormat > Formats.Length)
                    throw new Exception($"The specified texture format is not a valid. '{chunkFormat}'");
                Console.WriteLine($"\t\t\t\t> Format: {chunkFormat} ({Formats[chunkFormat]})");

                var chunkOffset = (int)(Length - (16 * chunkCount));
                for (var chunkId = 0; chunkId < chunkCount; chunkId++) {

                    Console.WriteLine();
                    Console.WriteLine($"\t\t\t> Reading Chunk Data: [{chunkId + 1} of {chunkCount}]");

                    if (Position + 16 > Length)
                        throw new Exception($"The specified chunk entry could not be read. '{Path.GetFileName(chunks[chunkId])}'");
                    Move(chunkOffset + (chunkId * 16));

                    try {

                        var asset = $"{Path.GetFileNameWithoutExtension(Filename)}.bitmap[{chunkId}_bitmap_resource_handle.chunk{chunkId}]";
                        if (chunkId > (chunks.Count - 1) || chunks[chunkId].Contains(asset) == false) {
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine($"\t\t\t\t> Chunk at index '{chunkId}' was not found. Skipping...");
                            Console.WriteLine($"\t\t\t\t\t> Chunk: '{Path.GetFileName(asset)}'");
                            Console.ResetColor();
                        }
                        else {

                            var bitmap = new Bitmap(chunks[chunkId]);
                            using (var chunkReader = new BitmapReader(chunks[chunkId])) {

                                bitmap.Format = chunkFormat;
                                Console.WriteLine($"\t\t\t\t> Format: {chunkFormat} ({Formats[chunkFormat]})");

                                ReadBytes(4);

                                bitmap.Length = ReadUInt32();
                                if (bitmap.Length <= 0 || chunkReader.Length != bitmap.Length)
                                    throw new Exception($"The specified texture length is not a valid. '{bitmap.Length}'");
                                bitmap.Data = chunkReader.ReadBytes((int)bitmap.Length);
                                Console.WriteLine($"\t\t\t\t> Length: {bitmap.Length}");

                                ReadBytes(4);

                                bitmap.Width = ReadUInt16();
                                if (bitmap.Width <= 0)
                                    throw new Exception($"The specified texture width is not a valid. '{bitmap.Width}'");
                                Console.WriteLine($"\t\t\t\t> Width: {bitmap.Width}");

                                bitmap.Height = ReadUInt16();
                                if (bitmap.Height <= 0)
                                    throw new Exception($"The specified texture height is not a valid. '{bitmap.Height}'");
                                Console.WriteLine($"\t\t\t\t> Height: {bitmap.Height}");

                                Console.WriteLine($"\t\t\t> Writing Texture Data: [{chunkId + 1} of {chunkCount}]");
                                Console.WriteLine($"\t\t\t\t> Mode: '{mode}'");

                                await Write(bitmap, mode);

                            }

                        }

                    }
                    catch (Exception ex) {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine($"\t\t\t\t> Failed: '{ex.Message}', skipping...");
                        Console.WriteLine($"\t\t\t\t> Chunk: '{Path.GetFileName(chunks[chunkId])}'");
                        Console.ResetColor();
                    }

                }

            }
            catch (Exception ex) {
                throw ex;
            }
        }
        public async Task ExtractHandles(List<string> handles, Mode mode) {
            try {

                Console.WriteLine();
                Console.WriteLine("\t\t\t> Reading Bitmap Data:");

                Move(28);
                var version = ReadInt32();

                Move((version == 2) ? 96 : 128);
                var handleCount = ReadInt32() / 40;
                if (handleCount <= 0)
                    throw new Exception($"The specified texture count is not a valid. '{handleCount}'");
                Console.WriteLine($"\t\t\t\t> Count: {handleCount}");

                var handleOffset = (int)(Length - (40 * handleCount));
                for (var handleId = 0; handleId < handleCount; handleId++) {

                    Console.WriteLine();
                    Console.WriteLine($"\t\t\t> Reading Handle Data: [{handleId + 1} of {handleCount}]");

                    if (Position + 40 > Length)
                        throw new Exception($"The specified chunk entry could not be read. '{Path.GetFileName(handles[handleId])}'");
                    Move(handleOffset + (handleId * 40));

                    try {

                        var handleAsset = $"{Path.GetFileNameWithoutExtension(Filename)}.bitmap[{handleId}_bitmap_resource_handle]";
                        if (handleId > (handles.Count - 1) || handles[handleId].Contains(handleAsset) == false) {
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine($"\t\t\t\t> Handle at index '{handleId}' was not found. Skipping...");
                            Console.WriteLine($"\t\t\t\t\t> Handle: '{Path.GetFileName(handleAsset)}'");
                            Console.ResetColor();
                        }
                        else {

                            using (var handleReader = new BitmapReader(handles[handleId])) {

                                var chunks = FileHelpers.GetChunksFromHandleName(Path.GetDirectoryName(handles[handleId]), Path.GetFileName(handles[handleId]));
                                if (chunks?.Count >= 1) {

                                    Console.WriteLine("\t\t\t\t> Handle has chunk resources, reading as chunked handle.");

                                    ReadBytes(40);

                                    //Chunk Count
                                    handleReader.Move(96);
                                    var chunkCount = handleReader.ReadInt32() / 16;
                                    if (chunkCount <= 0)
                                        throw new Exception($"The specified texture count is not a valid. '{chunkCount}'");
                                    Console.WriteLine($"\t\t\t\t> Count: {chunkCount}");

                                    //Chunk Format
                                    handleReader.Move(241);
                                    var chunkFormat = Convert.ToInt32(handleReader.ReadSByte());
                                    if (chunkFormat > Formats.Length)
                                        throw new Exception($"The specified texture format is not a valid. '{chunkFormat}'");
                                    Console.WriteLine($"\t\t\t\t> Format: {chunkFormat} ({Formats[chunkFormat]})");

                                    var chunkOffset = (int)(handleReader.Length - (16 * chunkCount));
                                    for (var chunkId = 0; chunkId < chunkCount; chunkId++) {

                                        Console.WriteLine();
                                        Console.WriteLine($"\t\t\t> Reading Chunk Data: [{chunkId + 1} of {chunkCount}]");

                                        if (handleReader.Position + 16 > handleReader.Length)
                                            throw new Exception($"The specified chunk entry could not be read. '{Path.GetFileName(chunks[chunkId])}'");
                                        handleReader.Move((chunkOffset + (chunkId * 16)));

                                        try {

                                            var chunkAsset = $"{Path.GetFileNameWithoutExtension(Filename)}.bitmap[{handleId}_bitmap_resource_handle][{chunkId}_bitmap_resource_handle.chunk{chunkId}]";
                                            if (chunkId > (chunks.Count - 1) || chunks[chunkId].Contains(chunkAsset) == false) {
                                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                                Console.WriteLine($"\t\t\t\t> Chunk at index '{chunkId}' was not found. Skipping...");
                                                Console.WriteLine($"\t\t\t\t\t> Chunk: '{Path.GetFileName(chunkAsset)}'");
                                                Console.ResetColor();
                                            }
                                            else {

                                                var bitmap = new Bitmap(chunks[chunkId]);
                                                using (var chunkReader = new BitmapReader(chunks[chunkId])) {

                                                    bitmap.Format = chunkFormat;
                                                    Console.WriteLine($"\t\t\t\t> Format: {chunkFormat} ({Formats[chunkFormat]})");

                                                    handleReader.ReadBytes(4);

                                                    bitmap.Length = handleReader.ReadUInt32();
                                                    if (bitmap.Length <= 0 || chunkReader.Length != bitmap.Length)
                                                        throw new Exception($"The specified texture length is not a valid. '{bitmap.Length}'");
                                                    bitmap.Data = chunkReader.ReadBytes((int)bitmap.Length);
                                                    Console.WriteLine($"\t\t\t\t> Length: {bitmap.Length}");

                                                    handleReader.ReadBytes(4);

                                                    bitmap.Width = handleReader.ReadUInt16();
                                                    if (bitmap.Width <= 0)
                                                        throw new Exception($"The specified texture width is not a valid. '{bitmap.Width}'");
                                                    Console.WriteLine($"\t\t\t\t> Width: {bitmap.Width}");

                                                    bitmap.Height = handleReader.ReadUInt16();
                                                    if (bitmap.Height <= 0)
                                                        throw new Exception($"The specified texture height is not a valid. '{bitmap.Height}'");
                                                    Console.WriteLine($"\t\t\t\t> Height: {bitmap.Height}");

                                                    Console.WriteLine($"\t\t\t> Writing Texture Data: [{chunkId + 1} of {chunkCount}]");
                                                    Console.WriteLine($"\t\t\t\t> Mode: '{mode}'");

                                                    await Write(bitmap, mode);

                                                }

                                            }

                                        }
                                        catch (Exception ex) {
                                            Console.ForegroundColor = ConsoleColor.DarkRed;
                                            Console.WriteLine($"\t\t\t\t> Failed: '{ex.Message}', skipping...");
                                            Console.WriteLine($"\t\t\t\t> Chunk: '{Path.GetFileName(chunks[chunkId])}'");
                                            Console.ResetColor();
                                        }

                                    }

                                }
                                else {

                                    Console.WriteLine("\t\t\t\t> Handle has no chunk resources, reading as single handle bitmap.");

                                    var bitmap = new Bitmap(handles[handleId]);

                                    bitmap.Width = ReadUInt16();
                                    if (bitmap.Width <= 0)
                                        throw new Exception($"The specified texture width is not a valid. '{bitmap.Width}'");
                                    Console.WriteLine($"\t\t\t\t> Width: {bitmap.Width}");

                                    bitmap.Height = ReadUInt16();
                                    if (bitmap.Height <= 0)
                                        throw new Exception($"The specified texture height is not a valid. '{bitmap.Height}'");
                                    Console.WriteLine($"\t\t\t\t> Height: {bitmap.Height}");

                                    ReadBytes(36);

                                    handleReader.Move(96);
                                    bitmap.Length = handleReader.ReadUInt32();
                                    if (bitmap.Length <= 0)
                                        throw new Exception($"The specified texture length is not a valid. '{bitmap.Length}'");

                                    handleReader.Move(241);
                                    bitmap.Format = Convert.ToInt32(handleReader.ReadSByte());
                                    if (bitmap.Format > Formats.Length)
                                        throw new Exception($"The specified texture format is not a valid. '{bitmap.Format}'");
                                    Console.WriteLine($"\t\t\t\t> Format: {bitmap.Format} ({Formats[bitmap.Format]})");

                                    handleReader.Move(284);
                                    if (handleReader.Position + bitmap.Length > handleReader.Length)
                                        throw new Exception($"The specified texture length data is not a valid. '{bitmap.Length}'");
                                    bitmap.Data = handleReader.ReadBytes((int)bitmap.Length);
                                    Console.WriteLine($"\t\t\t\t> Length: {bitmap.Length}");

                                    Console.WriteLine($"\t\t\t> Writing Texture Data: [{handleId + 1} of {handleCount}]");
                                    Console.WriteLine($"\t\t\t\t> Mode: '{mode}'");

                                    await Write(bitmap, mode);

                                }

                            }

                        }

                    }
                    catch (Exception ex) {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine($"\t\t\t\t> Failed: '{ex.Message}', skipping...");
                        Console.WriteLine($"\t\t\t\t> Handle: '{Path.GetFileName(handles[handleId])}'");
                        Console.ResetColor();
                    }

                }

            }
            catch (Exception ex) {
                throw ex;
            }
        }

        private async Task Write(Bitmap bitmap, Mode mode) {
            try {

                var root = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty, "export");
                if (string.IsNullOrEmpty(root))
                    throw new Exception("Unable to locate the specified directory.");

                if (Directory.Exists(root) == false) {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine($"\t\t\t\t> Creating Directory: '{root}'");
                    Directory.CreateDirectory(root);
                }

                var type = (mode == Mode.Image) ? "png" : "dds";
                var name = Path.GetFileName(bitmap.Filename);
                var file = Path.Combine(root, $"{name}_{bitmap.Width}x{bitmap.Height}.{type}");
                if (File.Exists(file)) {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine($"\t\t\t\t> File Found: '{Path.GetFileName(file)}', overwriting.");
                    Console.ResetColor();
                }

                Console.WriteLine("\t\t\t> Writing Image Data:");

                if (mode == Mode.Texture)
                    await WriteWriteTextureAsDds(bitmap, file);
                else
                    await WriteWriteTextureAsPng(bitmap, file);

            }
            catch (Exception ex) {
                throw ex;
            }
        }
        private async Task WriteWriteTextureAsDds(Bitmap bitmap, string file) {
            try {

                using (var writer = new FileStream(file, FileMode.OpenOrCreate, FileAccess.ReadWrite)) {

                    await writer.WriteAsync(Headers[0], 0, Headers[0].Length);
                    await writer.WriteAsync(BitConverter.GetBytes(bitmap.Height), 0, 4);
                    await writer.WriteAsync(BitConverter.GetBytes(bitmap.Width), 0, 4);
                    await writer.WriteAsync(BitConverter.GetBytes(bitmap.Width * bitmap.Height * 4), 0, 4);
                    await writer.WriteAsync(Headers[1], 0, Headers[1].Length);
                    await writer.WriteAsync(BitConverter.GetBytes(bitmap.Format), 0, 4);
                    await writer.WriteAsync(Headers[2], 0, Headers[2].Length);
                    await writer.WriteAsync(bitmap.Data, 0, bitmap.Data.Length);

                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine($"\t\t\t\t> Done! '{Path.GetFileName(file)}'");
                    Console.ResetColor();

                }

            }
            catch (Exception ex) {
                throw ex;
            }
        }
        private async Task WriteWriteTextureAsPng(Bitmap bitmap, string file) {
            try {

                await Task.Run(() => {

                    using (var stream = new MemoryStream())
                    using (var writer = new BinaryWriter(stream)) {

                        writer.Write(Headers[0]);
                        writer.Write(BitConverter.GetBytes(bitmap.Height), 0, 4);
                        writer.Write(BitConverter.GetBytes(bitmap.Width), 0, 4);
                        writer.Write(BitConverter.GetBytes(bitmap.Width * bitmap.Height * 4), 0, 4);
                        writer.Write(Headers[1]);
                        writer.Write(BitConverter.GetBytes(bitmap.Format), 0, 4);
                        writer.Write(Headers[2]);
                        writer.Write(bitmap.Data);
                        stream.Position = 0;
                        using (var magick = new MagickImage(stream, new MagickReadSettings {
                            Width = bitmap.Width,
                            Height = bitmap.Height
                        })) {

                            magick.Write(file, MagickFormat.Png);

                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Console.WriteLine($"\t\t\t\t> Done! '{Path.GetFileName(file)}'");
                            Console.ResetColor();

                        }

                    }

                });

            }
            catch (Exception ex) {
                throw ex;
            }
        }

    }

}