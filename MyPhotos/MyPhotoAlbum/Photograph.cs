﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Manning.MyPhotoAlbum
{
    /// <summary>
    /// The Photograph class represents a 
    /// photographic image stored in the 
    /// file system.
    /// </summary>
    public class Photograph : IDisposable, IFormattable
    {
        private string _fileName;
        private Bitmap _bitmap;
        private string _caption = "";
        private string _photographer = "";
        private DateTime _dateTaken = DateTime.Now;
        private string _notes = "";
        private bool _hasChanged = true;

        public Photograph(string filename)
        {
            _fileName = filename;
            _bitmap = null;
            _caption = System.IO.Path.GetFileNameWithoutExtension(filename);
        }

        public string FileName
        {
            get { return _fileName; }
        }

        public Bitmap Image
        {
            get
            {
                if (_bitmap == null)
                {
                    _bitmap = new Bitmap(_fileName);
                }
                return _bitmap;
            }
        }

        public bool HasChanged
        {
            get { return _hasChanged; }
            internal set { _hasChanged = value; }
        }

        public string Caption
        {
            get { return _caption; }
            set
            {
                if (_caption != value)
                {
                    _caption = value;
                    HasChanged = true;
                }
            }
        }

        public string Photographer
        {
            get { return _photographer; }
            set
            {
                if (_photographer != value)
                {
                    _photographer = value;
                    HasChanged = true;
                } 
            }
        }

        public DateTime DateTaken
        {
            get { return _dateTaken; }
            set
            {
                if (_dateTaken != value)
                {
                    _dateTaken = value;
                    HasChanged = true;
                }
            }
        }

        public string Notes
        {
            get { return _notes; }
            set
            {
                if (_notes != value)
                {
                    _notes = value;
                    HasChanged = true;
                }
            }
        }

        public void Dispose()
        {
            ReleaseImage();
        }

        public void ReleaseImage()
        {
            if (_bitmap != null)
            {
                _bitmap.Dispose();
                _bitmap = null;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is Photograph)
            {
                Photograph p = (Photograph)obj;
                return (FileName.Equals(p.FileName, StringComparison.InvariantCultureIgnoreCase));
            }
            return false;
        }

        public override int GetHashCode()
        {
            return FileName.ToLowerInvariant().GetHashCode();
        }

        public override string ToString()
        {
            return FileName;
        }

        #region IFormattable Memebers
        public string ToString(string format, IFormatProvider fp)
        {
            if (String.IsNullOrEmpty(format))
                format = "f";

            char first = format.ToLower()[0];
            if (format.Length == 1)
            {
                switch (first)
                {
                    case 'c': return Caption;
                    case 'd': return DateTaken.ToShortDateString();
                    case 'f': return FileName;
                }
            }
            else if (first == 'd')
                return DateTaken.ToString(
                    format.Substring(1), fp);

            throw new FormatException();
        }

        public string ToString(string format)
        {
            return ToString(format, null);
        }

        public string ToString (IFormatProvider fp)
        {
            return ToString(null, fp);
        }
        #endregion

    }
}
