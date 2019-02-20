﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manning.MyPhotoAlbum
{
    public class PhotoAlbum : Collection<Photograph>, IDisposable
    {
        public enum DescriptorOption
        { FileName, Caption, DateTaken }
        private string _title;
        private DescriptorOption _descriptor;

        private void ClearSettings()
        {
            _title = null;
            _descriptor = DescriptorOption.Caption;
        }

        public PhotoAlbum()
        {
            ClearSettings();
        }
        public void Dispose()
        {
            ClearSettings();
            foreach (Photograph p in this)
                p.Dispose();
        }

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                HasChanged = true;
            }
        }

        public DescriptorOption PhotoDesctriptor
        {
            get { return _descriptor; }
            set { _descriptor = value;
                HasChanged = true;
            }
        }

        public string GetDescription(Photograph photo)
        {
            switch (PhotoDesctriptor)
            {
                case DescriptorOption.Caption:
                    return photo.Caption;
                case DescriptorOption.DateTaken:
                    return photo.DateTaken.ToShortDateString();
                case DescriptorOption.FileName:
                    return photo.FileName;
            }
            throw new ArgumentException(
                    "Unrecognized photo descriptor option.");
        }

        public string GetDescription(int index)
        {
            return GetDescription(this[index]);
        }

        private string _pwd;
        public string Password
        {
            get { return _pwd; }
            set { _pwd = value; }
        }

        private bool _hasChanged = false;
        public bool HasChanged
        {
            get
            {
                if (_hasChanged) return true;
                foreach (Photograph p in this)
                    if (p.HasChanged) return true;
                return false;
            }

            internal set
            {
                _hasChanged = value;
                if (value == false)
                {
                    foreach (Photograph p in this)
                    {
                        p.HasChanged = false;
                    }
                }
            }
        }

        public Photograph Add(string fileName)
        {
            Photograph p = new Photograph(fileName);
            base.Add(p);
            return p;
        }

        protected override void ClearItems()
        {
            if (Count > 0)
            {
                Dispose();
                base.ClearItems();
                HasChanged = true;
            }
        }

        protected override void InsertItem(int index, Photograph item)
        {
            base.InsertItem(index, item);
            HasChanged = true;
        }

        protected override void RemoveItem(int index)
        {
            Items[index].Dispose();
            base.RemoveItem(index);
            HasChanged = true;
        }

        protected override void SetItem(int index, Photograph item)
        {
            base.SetItem(index, item);
            HasChanged = true;
        }

    }
}