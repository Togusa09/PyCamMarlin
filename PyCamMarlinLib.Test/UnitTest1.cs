using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace PyCamMarlinLib.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var test = new PyCamGCodeProcessor();

            var stream = new MemoryStream();
            using (var streamWriter = new StreamWriter(stream))
            {
                streamWriter.WriteLine("G40");
                streamWriter.WriteLine("G49");
                //streamWriter.WriteLine("G61");
                //streamWriter.WriteLine("F200.00000");
                //streamWriter.WriteLine("S1000.00000");
                //streamWriter.WriteLine("G64");
                streamWriter.WriteLine("G1 Z10.0000");
                streamWriter.WriteLine("X68.7987");
                streamWriter.WriteLine("X28.6957 Y-35.1923");
                streamWriter.WriteLine("G0 Z25.0000");
                streamWriter.WriteLine("X70.5823 Y-38.1538");

                streamWriter.Flush();
                stream.Seek(0, SeekOrigin.Begin);

                var outStream = test.ProcessStream(stream);
                using (var streamReader = new StreamReader(outStream))
                {
                    var testString = streamReader.ReadToEnd();
                }
            }
        }
    }
}
