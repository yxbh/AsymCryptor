using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsymCryptor.Models
{
    public class AppModel : INotifyPropertyChanged
    {
        private string _CryptoKeyInfoFilePath;
        public string CryptoKeyInfoFilePath
        {
            get { return this._CryptoKeyInfoFilePath; }
            set
            {
                this._CryptoKeyInfoFilePath = value;
                NotifyPropertyChanged("CryptoKeyInfoFilePath");
            }
        }

        private string _CryptoDataInput;
        public string CryptoDataInput
        {
            get { return this._CryptoDataInput; }
            set
            {
                this._CryptoDataInput = value;
                NotifyPropertyChanged("CryptoDataInput");
            }
        }

        private string _CryptoDataOutput;
        public string CryptoDataOutput
        {
            get { return this._CryptoDataOutput; }
            set
            {
                this._CryptoDataOutput = value;
                NotifyPropertyChanged("CryptoDataOutput");
            }
        }

        public bool IsUsingOaepPadding { get; set; }
        public Int32 CryptoKeySize { get; set; }

        public AppModel()
        {
            this.IsUsingOaepPadding = false;
            this.CryptoKeySize = 512;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName = "")
        {
            Debug.WriteLine("AppModel property [" + propertyName + "] changed.");
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}

// reference for data binding: http://xamlnative.com/2015/09/27/xaml-101-more-data-binding/
