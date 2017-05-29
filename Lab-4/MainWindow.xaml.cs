using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MetadataExtractor;
using MessageBox = System.Windows.MessageBox;
using System.Windows.Forms;

namespace Lab_4
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ConfigureApp();
        }

        public void ConfigureApp()
        {
            btnFolder.Click += async (sender, e) =>
            {
                FolderBrowserDialog folderDialog = new FolderBrowserDialog();
                btnFolder.IsEnabled = false;
                List<ImageInfo> imagesInfo = null;
                if (folderDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string[] files = await GetFileInFolderAsync(folderDialog.SelectedPath);
                    imagesInfo = await GetMetadata(files);
                }
                listFileInfo.SelectionChanged += ListItemSelected;
                listFileInfo.ItemsSource = imagesInfo;
                btnFolder.IsEnabled = true;
            };
        }

        private void ListItemSelected(object sender, SelectionChangedEventArgs e)
        {
            System.Windows.Controls.ListView listView = (System.Windows.Controls.ListView)e.Source;
            int index = listView.SelectedIndex;
            try
            {
                List<ImageInfo> imagesInfo = (List<ImageInfo>)listView.ItemsSource;
                textBoxAdditionalInfo.Text = imagesInfo[index].AdditionalData;
            }
            catch { }
        }
        public Task<List<ImageInfo>> GetMetadata(string[] fileNames)
        {
            return Task.Run(() =>
            {
                List<ImageInfo> imagesInfo = new List<ImageInfo>();
                foreach(var file in fileNames)
                {
                    try
                    {
                        var directories = ImageMetadataReader.ReadMetadata(file);
                        ImageInfo imageInfo = new ImageInfo();
                        StringBuilder additionalInfo = new StringBuilder();
                        foreach(var directory in directories)
                        {
                            foreach(var tag in directory.Tags)
                            {
                                switch (tag.Name)
                                {
                                    case "Compression Type":
                                        imageInfo.CmprssnType = tag.Description;
                                        break;
                                    case "Compression":
                                        imageInfo.CmprssnType = tag.Description;
                                        break;
                                    case "File Name":
                                        imageInfo.FileName = tag.Description;
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
            else if (imageInfo.PixelSize.Contains('x'))
            {
                return;
            }
            else if (tag.Name == "Image Width")
                imageInfo.PixelSize += $"x {value}";
            else
                imageInfo.PixelSize = $"{value} x {imageInfo.PixelSize}";
        }

        public Task<string[]> GetFileInFolderAsync(string folderPath)
        {
            return Task.Run(() =>
            {
                string[] files = System.IO.Directory.GetFiles(folderPath);
                return files;
            });
        }

        public class ImageInfo
        {
            public string FileName { get; set; }
            public string PixelSize { get; set; }
            public string Dimension { get; set; }
            public string CmprssnType { get; set; }
            public string AdditionalData { get; set; }

            public ImageInfo() { }
            public ImageInfo(string fileName, string pixelSize, string dimension, string cmprssnType, string additionalData = "")
            {
                FileName = fileName;
                PixelSize = pixelSize;
                Dimension = dimension;
                CmprssnType = cmprssnType;
                AdditionalData = additionalData;
            }
            public override string ToString()
            {
                return $"File Name: {FileName}\n Pixel Size: {PixelSize}\n Dimension: {Dimension}\n CompressionType:{CmprssnType}\n";
            }
        }

        private void btnFolder_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
