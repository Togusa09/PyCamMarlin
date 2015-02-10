using System;   
using System.Environment;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using PyCamMarlinLib;

namespace PyCamMarlinGUI
{
    public partial class Form1 : Form
    {
        private string _currentDirectory = string.Empty;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _currentDirectory = GetFolderPath(SpecialFolder.MyDocuments);
            //SourceFile.Text = docFolder;
            OutputFile.Text = @"output.gcode";

        }

        private void OpenFile_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = _currentDirectory;
            //openFileDialog1.InitialDirectory = "C:\\";

            if (openFileDialog1.ShowDialog() != DialogResult.OK) return;

            var fileName = openFileDialog1.FileName;
            //OutputFile.Text = Path.GetFileNameWithoutExtension(fileName) + "_updated" +
            //    Path.GetExtension(fileName);
            ImportFiles.Items.Add(fileName);
            _currentDirectory = Path.GetDirectoryName(openFileDialog1.FileName);
        }

        private void ProcessFile_Click(object sender, EventArgs e)
        {
            var processor = new PyCamGCodeProcessor();

            var path = Path.GetDirectoryName(openFileDialog1.FileName);
            if (path == null) return;
            var outputLocation = Path.Combine(path, OutputFile.Text);

            var files = ImportFiles.Items.OfType<string>().ToList();

            var content = processor.Process(files);
            File.WriteAllLines(outputLocation, content);

            //using (var stream = processor.Process(outputLocation, files))
            //{
            //    stream.Flush();
            //    using (var fileStream = new FileStream(outputLocation, FileMode.OpenOrCreate))
            //    {
            //        stream.CopyTo(fileStream);
            //        //fileStream.Flush();
            //    }
            //}
        }

        private void RemoveFile_Click(object sender, EventArgs e)
        {
            if (ImportFiles.SelectedItems.Count == 0) return;

                foreach (var i in ImportFiles.SelectedItems.OfType<string>().ToList())
                    ImportFiles.Items.Remove(i);
        }
    }
}
