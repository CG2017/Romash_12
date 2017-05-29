using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using MetadataExtractor;
using MessageBox = System.Windows.MessageBox;

namespace lab4
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ConfigureApplication();
        }

        public void ConfigureApplication()
        {
            btnOpenFolder.Click += async (sender, e) =>
            {
                FolderBrowserDialog folderDialog = new FolderBrowserDialog();
                btnOpenFolder.IsEnabled = false;
                List<ImageInfo> imagesInfo = null;// = new List<ImageInfo>();
                //imagesInfo.Add(new ImageInfo("test.jpg", "300", "1366x1024", "Haffman"));
                if (folderDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string[] files = await GetFilesInFolderAsync(folderDialog.SelectedPath);
                    imagesInfo = await GetMetadata(files);
                }
                listFileInfo.SelectionChanged += ListItemSelected;
                listFileInfo.ItemsSource = imagesInfo;
                btnOpenFolder.IsEnabled = true;
            };
        }

        private void ListItemSelected(object sender, SelectionChangedEventArgs e)
        {
            System.Windows.Controls.ListView listView = (System.Windows.Controls.ListView)e.Source;
            int index = listView.SelectedIndex;
            try
            {
                List<ImageInfo> imagesInfo = (List<ImageInfo>)listView.ItemsSource;
                //imagesInfo[index].AdditionalData;
            }
            catch { }
        }

        public Task<List<ImageInfo>> GetMetadata(string[] filenames)
        {
            return Task.Run(() =>
            {
                List<ImageInfo> imagesInfo = new List<ImageInfo>();
                foreach (var file in filenames)
                {
                    try
                    {
                        var directories = ImageMetadataReader.ReadMetadata(file);
                        ImageInfo imageInfo = new ImageInfo();
                        StringBuilder additionalInfo = new StringBuilder();
                        foreach (var directory in directories)
                        {
                            foreach (var tag in directory.Tags)
                            {
                                switch (tag.Name)
                                {
                                    case "Compression Type":
                                        imageInfo.CompressionType = tag.Description;
                                        break;
                                    case "Compression":
                                        imageInfo.CompressionType = tag.Description;
                                        break;
                                    case "File Name":
                                        imageInfo.Filename = tag.Description;
                                        break;
                                    case "Image Height":
                                        AddPixelSize(imageInfo, tag);
                                        break;
                                    case "Image Width":
                                        AddPixelSize(imageInfo, tag);
                                        break;
                                    case "X Resolution":
                                        imageInfo.Dimension = tag.Description;
                                        break;
                                    default:
                                        additionalInfo.Append($"{tag.Name} = {tag.Description}\n");
                                        break;
                                }
                            }

                        }
                        imageInfo.AdditionalData = additionalInfo.ToString();
                        imagesInfo.Add(imageInfo);
                    }
                    catch
                    {
                        MessageBox.Show(file);
                    }
                }

                return imagesInfo;
            });
        }

        private void AddPixelSize(ImageInfo imageInfo, Tag tag)
        {
            int value = int.Parse(tag.Description.Split(' ')[0]);
            if (imageInfo.PixelSize == null)
                imageInfo.PixelSize = value.ToString();
            else if (imageInfo.PixelSize.Contains("x"))
            {
                return;
            }
            else if (tag.Name == "Image Width")
                imageInfo.PixelSize += $"x {value}";
            else
                imageInfo.PixelSize = $"{value} x {imageInfo.PixelSize}";
        }

        public Task<string[]> GetFilesInFolderAsync(string folderPath)
        {
            return Task.Run(() =>
            {
                string[] files = System.IO.Directory.GetFiles(folderPath);
                return files;
            });
        }

        public class ImageInfo
        {
            public string Filename { get; set; }
            public string PixelSize { get; set; }
            public string Dimension { get; set; }
            public string CompressionType { get; set; }
            public string AdditionalData { get; set; }

            public ImageInfo()
            {

            }

            public ImageInfo(string filename, string pixelSize, string dimension, string compressionType,
                string additionalData = "")
            {
                Filename = filename;
                PixelSize = pixelSize;
                Dimension = dimension;
                CompressionType = compressionType;
                AdditionalData = additionalData;
            }

            public override string ToString()
            {
                return $"Filename: {Filename}\n PixelSize:{PixelSize}\n " +
                    $"Dimension:{Dimension}\n CompressionType:{CompressionType}\n";
            }
        }

        private void btnOpenFolder_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
