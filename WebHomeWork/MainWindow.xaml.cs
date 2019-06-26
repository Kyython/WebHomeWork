using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WebHomeWork
{
    public partial class MainWindow : Window
    {
        private string _body;

        public MainWindow()
        {
            InitializeComponent();
        }

        private async Task ParseWebContent()
        {
            var request = WebRequest.Create(urlTextBox.Text);
            var response = await request.GetResponseAsync();

            using (var stream = new StreamReader(response.GetResponseStream()))
            {
                _body = $"{stream.ReadLine()}\n";
            }
        }

        private async void EnterButtonClick(object sender, RoutedEventArgs e)
        {
            await ParseWebContent();
            Dictionary<string, int> dictionary = new Dictionary<string, int>();

            foreach (string item in _body.Split(new char[] { ' ', ',', '.', '/', '<', '>', ';', ':', '\\', '"', '\'', '?', '!', '#', '$', '%', '^', '(', ')', '-', '_', '=', '*', '+', '@', '—' }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (dictionary.ContainsKey(item))
                {
                    dictionary[item]++;
                }
                else
                {
                    dictionary.Add(item, 1);
                }
            }

            foreach (KeyValuePair<string, int> pair in dictionary)
            {
                dictionaryTextBox.Text += $"Item = {pair.Key}; Count = {pair.Value}\n";
            }
        }
    }
}
