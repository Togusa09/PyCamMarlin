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

        private void FileUp_Click(object sender, EventArgs e)
        {
            // only if the first item isn't the current one
            if (ImportFiles.SelectedIndex > 0)
            {
                // add a duplicate item up in the listbox
                ImportFiles.Items.Insert(ImportFiles.SelectedIndex - 1, ImportFiles.Text);
                // make it the current item
                ImportFiles.SelectedIndex = (ImportFiles.SelectedIndex - 2);
                // delete the old occurrence of this item
                ImportFiles.Items.RemoveAt(ImportFiles.SelectedIndex + 2);
            }
        }

        private void FileDown_Click(object sender, EventArgs e)
        {
            if ((ImportFiles.SelectedIndex != -1) && (ImportFiles.SelectedIndex < ImportFiles.Items.Count - 1))
            {
                // add a duplicate item down in the listbox
                ImportFiles.Items.Insert(ImportFiles.SelectedIndex + 2, ImportFiles.Text);
                // make it the current item
                ImportFiles.SelectedIndex = ImportFiles.SelectedIndex + 2;
                // delete the old occurrence of this item
                ImportFiles.Items.RemoveAt(ImportFiles.SelectedIndex - 2);
            }
        }
    }
}
