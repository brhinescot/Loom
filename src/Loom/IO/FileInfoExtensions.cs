#region Using Directives

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

#endregion

namespace Loom.IO
{
    public static class FileInfoExtensions
    {
        public static void Split(this FileInfo fileInfo, int numberOfFiles, bool compress = false)
        {
            using (FileStream fs = fileInfo.OpenRead())
            {
                int sizeOfEachFile = (int) Math.Ceiling((double) fs.Length / numberOfFiles);

                for (int i = 1; i <= numberOfFiles; i++)
                {
                    string baseFileName = Path.GetFileNameWithoutExtension(fileInfo.Name);
                    string extension = fileInfo.Extension;

                    using (FileStream outputFile = new FileStream(fileInfo.DirectoryName + "\\" + baseFileName + "." + i.ToString(CultureInfo.InvariantCulture).PadLeft(5, Convert.ToChar("0")) + extension + ".csd", FileMode.Create, FileAccess.Write))
                    {
                        byte[] buffer = new byte[sizeOfEachFile];
                        SaveSegment(fs, sizeOfEachFile, outputFile, buffer);
                    }
                }
            }
        }

        public static void Merge(this IEnumerable<string> files, string outputDirectory)
        {
            string outPath = outputDirectory;
            FileStream outputFile = null;

            if (!Directory.Exists(outPath))
                Directory.CreateDirectory(outPath);

            foreach (string tempFile in files)
            {
                if (outputFile == null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(tempFile);
                    if (fileName == null)
                        continue;

                    string baseFileName = fileName.Substring(0, fileName.IndexOf(Convert.ToChar(".")));
                    string extension = Path.GetExtension(fileName);
                    outputFile = new FileStream(outPath + baseFileName + extension, FileMode.OpenOrCreate, FileAccess.Write);
                }

                int bytesRead;
                byte[] buffer = new byte[1024];
                FileStream inputTempFile = new FileStream(tempFile, FileMode.OpenOrCreate, FileAccess.Read);

                while ((bytesRead = inputTempFile.Read(buffer, 0, 1024)) > 0)
                    outputFile.Write(buffer, 0, bytesRead);

                inputTempFile.Close();
                File.Delete(tempFile);
            }
            outputFile?.Close();
        }

        public static void Recycle(this FileInfo fileInfo)
        {
            FileHelper.Recycle(fileInfo.FullName);
        }

        private static void SaveSegment(FileStream fs, int sizeOfEachFile, FileStream outputFile, byte[] buffer)
        {
            int bytesRead;
            if ((bytesRead = fs.Read(buffer, 0, sizeOfEachFile)) > 0)
                outputFile.Write(buffer, 0, bytesRead);
        }
    }
}