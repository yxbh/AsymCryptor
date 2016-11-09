using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
using System.Security.Cryptography;

using AsymCryptor.Logics;
using AsymCryptor.Models;

namespace AsymCryptor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public AppModel Model { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            this.Model = new AppModel();

            this.Model.CryptoKeyInfoFilePath = "";
            this.Model.CryptoDataInput = "";
            this.Model.CryptoDataOutput = "";

            this.DataContext = this;
        }

        private void button_encrypt_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Encrypt button pressed.");
            
            if (this.Model.CryptoDataInput.Length == 0)
            {
                var result = MessageBox.Show("There is no input data.");
                return;
            }

            var fileInfo = new FileInfo(this.Model.CryptoKeyInfoFilePath);
            if (!Directory.Exists(fileInfo.Directory.FullName))
            {
                Directory.CreateDirectory(fileInfo.Directory.FullName);
            }
            
            var key = "";
            if (!File.Exists(fileInfo.FullName))
            {
                var crytoServiceProvider = new RSACryptoServiceProvider(this.Model.CryptoKeySize);
                key = crytoServiceProvider.ToXmlString(true);
                File.WriteAllText(fileInfo.FullName, key);
            }
            else
            {
                key = File.ReadAllText(fileInfo.FullName);
            }

            this.Model.CryptoDataOutput = AsymCryptoHelper.EncryptToBase64String(this.Model.CryptoDataInput, key);
        }

        private void button_decrypt_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Decrypt button pressed.");

            if (this.Model.CryptoKeyInfoFilePath.Length == 0)
            {
                var result = MessageBox.Show("A crypto info file path is needed.");
                return;
            }

            if (this.Model.CryptoDataInput.Length == 0)
            {
                var result = MessageBox.Show("There is no input data.");
                return;
            }

            var fileInfo = new FileInfo(this.Model.CryptoKeyInfoFilePath);
            if (!File.Exists(fileInfo.FullName))
            {
                var result = MessageBox.Show("The specified key info file does not exist.");
                return;
            }
            var key = File.ReadAllText(fileInfo.FullName);


            this.Model.CryptoDataOutput = AsymCryptoHelper.DecryptToString(this.Model.CryptoDataInput, key);
        }
    }
}
