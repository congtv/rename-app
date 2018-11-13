using AutoRenameApp.Shared;

namespace AutoRenameApp.Models
{
    public class FileRename : BaseNotifyPropertyChaged
    {
        private bool _isChecked = false;

        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                _isChecked = value;
                RaisePropertyChanged(() => IsChecked);
            }
        }

        public string FileName { get; set; }
        public string Extension { get; set; }
        public string Directory { get; set; }
        public string FullPath { get; set; }
    }
}