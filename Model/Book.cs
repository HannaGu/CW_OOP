using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Media.Imaging;

namespace CW_WPF.Model
{
    public class Book : INotifyPropertyChanged
    {
        public string isbn;
        public string title;
        public string author;
        public string original_language;
        public int year_of_release;
        public string description;
        public int rate;
        public byte[] image;
        public bool is_custom;
        public string ganre;
      
        #region Properties       
        public string Isbn
        {
            get { return isbn; }
            set
            {
                isbn = value;
                OnPropertyChanged("Isbn");
            }
        }

        public bool Is_custom
        {
            get
            {
                return is_custom;
            }
            set
            {
                is_custom = value;
                OnPropertyChanged(nameof(Is_custom));
            }
        }

        public string Ganre
        {
            get { return ganre; }
            set
            {
                isbn = value;
                OnPropertyChanged("Ganre");
            }
        }

        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged("Title");
            }
        }

        public string Author
        {
            get { return author; }
            set
            {
                author = value;
                OnPropertyChanged("Author");
            }
        }

        public string Original_language
        {
            get { return original_language; }
            set
            {
                original_language = value;
                OnPropertyChanged("Original_language");
            }
        }

        public int Year_of_release
        {
            get { return year_of_release; }
            set
            {
                year_of_release = value;
                OnPropertyChanged("Year_of_release");
            }
        }


        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                OnPropertyChanged("Description");
            }
        }

        public int Rate
        {
            get { return rate; }
            set
            {
                rate = value;
                OnPropertyChanged("Rate");
            }
        }

        //public BitmapImage GetImage
        //{
        //    get
        //    {
        //        MemoryStream ms = new MemoryStream(image);
        //        var img = new BitmapImage();
        //        img.BeginInit();
        //        img.StreamSource = ms;
        //        img.CacheOption = BitmapCacheOption.OnLoad;
        //        img.EndInit();
        //        return img;
        //    }
        //}

        //public Image GetImage
        //{
        //    get
        //    {
        //        MemoryStream ms = new MemoryStream(image);
        //        Image img = System.Drawing.Image.FromStream(ms);
        //        return img;
        //    }
        //    set {
        //        OnPropertyChanged("GetImage");
        //    }

        //}

        public byte[] Image
        {
            get
            {
               return image;
            }
            set
            {
                image = value;
                OnPropertyChanged("Image");
            }
        }
        public bool Is_Custom
        {
            get
            {
                return is_custom;
            }
            set
            {
                is_custom = value;
                OnPropertyChanged("Is_Custom");
            }
        }

        #endregion
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        public Book() { }
        public Book(string isbn, string title, string author, string original_language, int year_of_release, string description, int rate, byte[] image, string ganre)
        {
            this.isbn = isbn;
            this.title = title;
            this.author= author;
            this.original_language = original_language;
            this.description = description;
            this.year_of_release = year_of_release;
            this.rate = rate;
            this.image = image;
            this.ganre = ganre;
        
        }
       
        //private static BitmapImage LoadImage(byte[] imageData)
        //{
        //    if (imageData == null || imageData.Length == 0) return null;
        //    var image = new BitmapImage();
        //    using (var mem = new MemoryStream(imageData))
        //    {
        //        mem.Position = 0;
        //        image.BeginInit();
        //        image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
        //        image.CacheOption = BitmapCacheOption.OnLoad;
        //        image.UriSource = null;
        //        image.StreamSource = mem;
        //        image.EndInit();
        //    }
        //    image.Freeze();
        //    return image;
        //}
    }
}
