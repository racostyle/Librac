﻿using System;
using System.IO;
using System.Reflection;

namespace Librac.FileHandlingLib
{
    internal class FileHandlerMethods
    {
        #region READONLY METHODS
        public void RemoveReadOnlyFromDirectory(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                Console.WriteLine("Directory does not exist.");
                return;
            }

            try
            {
                // Remove the read-only attribute from all files in the directory
                foreach (string filePath in Directory.GetFiles(directoryPath, "*", SearchOption.AllDirectories))
                {
                    var file = new FileInfo(filePath);
                    // Check if the file is read-only and remove the attribute
                    if ((file.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                    {
                        file.Attributes &= ~FileAttributes.ReadOnly;
                    }
                }
                Console.WriteLine("Read-only attribute removed from all files successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public void ApplyReadOnlyToDirectory(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                Console.WriteLine("Directory does not exist.");
                return;
            }

            try
            {
                // Set the read-only attribute to all files in the directory
                foreach (string filePath in Directory.GetFiles(directoryPath, "*", SearchOption.AllDirectories))
                {
                    var file = new FileInfo(filePath);
                    file.Attributes |= FileAttributes.ReadOnly;
                }
                Console.WriteLine("Read-only attribute applied to all files successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
        #endregion

        #region MODE AND COPY
        public void MoveFileTo(string fileFullName, string destinationFullName, bool overwrite)
        {
            if (!File.Exists(fileFullName))
                return;
            if (overwrite)
            {
                if (!File.Exists(destinationFullName))
                    File.Delete(destinationFullName);
            }
            try
            {
                File.Move(fileFullName, destinationFullName);
                Console.WriteLine("File copied successfully.");
            }
            catch (IOException e)
            {
                Console.WriteLine($"An error occurred: {e.Message}");
            }
        }

        public void CopyFileTo(string fileFullName, string destinationFullName, bool overwrite)
        {
            if (!File.Exists(fileFullName))
                return;
            try
            {
                File.Copy(fileFullName, destinationFullName, overwrite);
                Console.WriteLine("File copied successfully.");
            }
            catch (IOException e)
            {
                Console.WriteLine($"An error occurred: {e.Message}");
            }
        }

        #endregion

        #region SEARCH FOR FILE AND COPY INTO WORKING DIRECTORY
        internal string GetAssemblyDirectory(string currentWorkingDirectory, string assemblyName)
        {
            var index = currentWorkingDirectory.IndexOf(assemblyName) + assemblyName.Length;
            return currentWorkingDirectory.Substring(0, index);
        }

        internal void FindAndCopyFileToWorkingDirectory(string path, string fileName, string assemblyName = "", bool overwrite = false)
        {
            if (!overwrite)
            {
                if (File.Exists(Path.Combine(Directory.GetCurrentDirectory(), fileName)))
                    return;
            }
            FindAndCopy(path, fileName, assemblyName);
        }

        private void FindAndCopy(string path, string fileName, string assemblyName)
        {
            if (path == Directory.GetCurrentDirectory()) //safetycheck
            {
                path = Directory.GetParent(path).FullName;
                FindAndCopy(path, fileName, assemblyName);
                return;
            }
            var file = Path.Combine(path, fileName);
            try
            {
                if (!File.Exists(file))
                {
                    if (!string.IsNullOrEmpty(assemblyName))
                    {
                        var cd = new DirectoryInfo(path).Name;
                        if (cd == assemblyName)
                        {
                            Console.WriteLine($"File: {fileName} not found. Search stopped in folder {cd}");
                            return;
                        }
                    }
                    path = Directory.GetParent(path).FullName;
                    FindAndCopy(path, fileName, assemblyName);
                }
                else
                {
                    var targetFile = Path.Combine(Directory.GetCurrentDirectory(), fileName);
                    if (File.Exists(targetFile))
                        File.Delete(targetFile);
                    File.Copy(file, Path.Combine(Directory.GetCurrentDirectory(), fileName));
                }
            }
            catch
            {
                Console.WriteLine($"File: {fileName} not found. Search stopped in {path}");
            };
        }
        #endregion
    }
}
