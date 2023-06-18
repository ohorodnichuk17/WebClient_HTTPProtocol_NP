using System;
using System.ComponentModel;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;

namespace WpfApp_Practise_NP
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string width = txtBoxWidth.Text;
            string height = txtBoxHeight.Text;
            string category = txtBoxCategory.Text;

            string url = $"https://source.unsplash.com/random/{width}x{height}/?{category}&1";

            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Image Files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;
                await DownloadImageAsync(url, filePath);
            }
        }

        private async Task DownloadImageAsync(string url, string filePath)
        {
            using (var client = new WebClient())
            {
                try
                {
                    await client.DownloadFileTaskAsync(new Uri(url), filePath);
                    MessageBox.Show("Image downloaded successfully!");
                    DisplayImage(filePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error downloading image: {ex.Message}");
                }
            }
        }

        private void DisplayImage(string filePath)
        {
            try
            {
                var bitmap = new System.Windows.Media.Imaging.BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(filePath);
                bitmap.EndInit();
                image.Source = bitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error displaying image: {ex.Message}");
            }
        }
    }
}
