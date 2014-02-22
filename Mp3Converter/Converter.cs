using System;
using System.Windows.Forms;
using Mp3Converter.Converter;
using Mp3Converter.MusicFiles;

namespace Mp3Converter
{
    public partial class Mp3Converter : Form
    {
        private MusicFileCollection musicFileCollection;

        public Mp3Converter()
        {
            InitializeComponent();
        }

        private void Mp3Converter_Load(object sender, EventArgs e)
        {
            lsvFiles.AllowDrop = true;
            lsvFiles.DragEnter += Files_DragEnter;
            lsvFiles.DragDrop += Files_DragDrop;
            lsvFiles.SmallImageList = imgProcessing;
            musicFileCollection = new MusicFileCollection();
        }

        private void Files_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void Files_DragDrop(object sender, DragEventArgs e)
        {
            var fileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            PopulateCollectionAndListview(fileList);
        }

        private void PopulateCollectionAndListview(string[] fileList)
        {
            Utils.ShowBusyMouse();

            RemoveCompletedItems();


            musicFileCollection.AddFiles(fileList);

            foreach (var file in musicFileCollection.Files)
            {
                var lsvItem = new ListViewItem
                {
                    Text = file.DisplayName,
                    Name = file.OriginalFile
                };

                lsvItem.SubItems.Add(file.FileExtention.ToUpper());

                if (!lsvFiles.Items.ContainsKey(file.OriginalFile))
                {
                    lsvFiles.Items.Add(lsvItem);
                }
            }

            Utils.ShowNormalMouse();
        }

        void RemoveCompletedItems()
        {
            for (var i = lsvFiles.Items.Count - 1; i >= 0; i--)
            {
                if (lsvFiles.Items[i].ImageIndex == 0)
                {
                    musicFileCollection.Remove(lsvFiles.Items[i].Name);
                    lsvFiles.Items.RemoveAt(i);
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var fileBrowse = new OpenFileDialog
            {
                Title = @"Browse for AIF, WAV or a WMA File",
                Filter = @"WAV Files|*.wav|AIF Files|*.aif|WMA Files|*.wma",
                CheckFileExists = true,
                Multiselect = true
            };

            var result = fileBrowse.ShowDialog();

            if (result != DialogResult.OK) return;

            PopulateCollectionAndListview(fileBrowse.FileNames);
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            Utils.ShowBusyMouse();

            var total = lsvFiles.Items.Count;
            var count = 1;

            foreach (var file in musicFileCollection.Files)
            {
                tspStatusLabel.Text = string.Format("Converting file {0} of {1}", count, total);
                var factory = new ConverterFactory(file.OriginalFile, Utils.GetMp3FileName(file.OriginalFile), "320");
                var converter = factory.GetConverter(file.FileExtention);
                converter.Convert();
                var index = lsvFiles.Items.IndexOfKey(file.OriginalFile);
                lsvFiles.Items[index].ImageIndex = 0;
                count++;
            }

            tspStatusLabel.Text = @"Finished";

            Utils.ShowNormalMouse();
        }
    }
}
