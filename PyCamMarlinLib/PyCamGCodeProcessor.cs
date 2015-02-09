using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PyCamMarlinLib
{
    public class PyCamGCodeProcessor
    {
        private readonly IList<ILogger> _loggers;

        private static readonly List<string> UnsupportedGCodes = new List<string>
        {
            "G40", "G49", "G54", "G80", "G20", "G61", "G21", "G04", "M2", "M5"
        };

        private static readonly List<string> SkipPrefix = new List<string>
        {
            "S", "T", "F"
        };

        private static readonly List<string> UseModalPrefix = new List<string>
        {
            "X", "Y", "Z"
        };

        public PyCamGCodeProcessor(IList<ILogger> loggers = null)
        {
            _loggers = loggers;
        }

        public List<string> Process(List<string> importFiles)
        {
            var fileContents = new List<string> {"G92 X0 Y0 Z0", "G90", "M3 S10000"};

            foreach (var file in importFiles)
            {
                using (var fileStream = File.OpenRead(file))
                {
                    fileContents.Add(";" + Path.GetFileName(file));
                    var fileContent = ProcessStream(fileStream);
                    fileContents.AddRange(fileContent);
                }
            }

            fileContents.Add("M5");
            return fileContents;

            //var outputStream = new MemoryStream();
            //WriteListToStream(fileContents, outputStream);
            //return outputStream;
        }

        public List<string> ProcessStream(Stream input)
        {
            using (var fileRead = new StreamReader(input))
            {
                var fileContents = new List<string>();

                while (!fileRead.EndOfStream)
                {
                    var readLine = fileRead.ReadLine();
                    if (IsLineEmpty(readLine) || readLine == null) continue;

                    var line = readLine.Trim();
                    fileContents.Add(line);
                }

                fileContents = AddMissingGCode(fileContents);
                fileContents = RemoveUnsupportedGCode(fileContents);

                // Turn off mill
                return fileContents;
            }
        }

        List<string> AddMissingGCode(IEnumerable<string> fileContents)
        {
            var returnList = new List<string>();
            var modalCommand = string.Empty;
            foreach(var line in fileContents)
            {
                var commandCode = line.Split(' ').First();

                // Should split into two passes. One to add missing codes, second to remove unknown codes
                var firstChar = commandCode.Substring(0, 1);

                if (commandCode.StartsWith("G"))
                {
                    modalCommand = commandCode;
                }

                if (UseModalPrefix.Contains(firstChar))
                {
                    returnList.Add(modalCommand + " " + line);
                }
                else
                {
                    returnList.Add(line);
                }
            }
            return returnList;
        }

        List<string> RemoveUnsupportedGCode(IEnumerable<string> fileContents, bool removeComments = false)
        {
            var returnList = new List<string>();
            foreach (var line in fileContents)
            {
                if (IsLineComment(line))
                {
                    if(!removeComments)
                        returnList.Add(line);
                    continue;
                }

                if (!IsValidGCode(line))
                {
                    continue;
                }
                // If no speed on spindle start
                if (line.Trim().StartsWith("M3") && !line.Contains("S")) 
                {
                    returnList.Add("M3 S10000");
                    continue;
                }
                returnList.Add(line);
            }
            return returnList;
        }

        //static void WriteListToStream(List<string> fileContents, Stream outStream)
        //{
        //    var streamWriter = new StreamWriter(outStream);
        //    fileContents.ForEach(streamWriter.WriteLine);
        //    outStream.Flush();
            
        //    File.WriteAllLines("test.txt",fileContents);
        //    outStream.Seek(0, SeekOrigin.Begin);
        //}

        private static bool IsLineEmpty(string line)
        {
            return string.IsNullOrWhiteSpace(line);
        }

        private static bool IsLineComment(string line)
        {
            return line.TrimStart().StartsWith(";");
        }

        private static bool IsValidGCode(string command)
        {
            if (string.IsNullOrWhiteSpace(command))
                return false;
            command = command.Trim();

            return !(UnsupportedGCodes.Contains(command) || SkipPrefix.Contains(command.Substring(0, 1)));
        }

        protected void Log(string message)
        {
            if (_loggers == null) return;
            foreach(var logger in _loggers)
            {
                logger.Write(message);
            }
        }
    }
}
