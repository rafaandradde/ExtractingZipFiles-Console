using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace CriandoExtraindo_ZIP
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var localizacaoArquivoZip = @"Your path";

            if (File.Exists(localizacaoArquivoZip))
            {
                using (FileStream zipFileStream = File.OpenRead(localizacaoArquivoZip))
                {
                    using (ZipArchive archive = new ZipArchive(zipFileStream, ZipArchiveMode.Read))
                    {
                        List<byte[]> extractedFiles = new List<byte[]>();
                        List<string> extractedFileNames = new List<string>();

                        foreach (ZipArchiveEntry entry in archive.Entries)
                        {
                            if (entry.FullName.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
                            {
                                using (Stream entryStream = entry.Open())
                                {
                                    using (MemoryStream memoryStream = new MemoryStream())
                                    {
                                        entryStream.CopyTo(memoryStream);
                                        extractedFiles.Add(memoryStream.ToArray());
                                        extractedFileNames.Add(entry.Name);
                                    }
                                }
                            }
                        }

                        for (int i = 0; i < extractedFiles.Count; i++)
                        {
                            Console.WriteLine($"NOME DO ARQUIVO: {extractedFileNames[i]}");
                            Console.WriteLine();
                            Console.WriteLine($"CONTEUDO DO ARQUIVO:\n{Encoding.UTF8.GetString(extractedFiles[i])}");
                            Console.WriteLine("===================================================================================");
                            Console.WriteLine();

                        }
                    }
                }
            }
            else
            {
                throw new FileNotFoundException("O Arquivo Zip não foi localizado");
            }
        }
    }
}
