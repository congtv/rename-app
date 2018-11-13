using AutoRenameApp.Models;
using AutoRenameApp.Shared;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AutoRenameApp.ViewModels
{
    public class BaseViewModel : BaseNotifyPropertyChaged
    {
        private ObservableCollection<FileRename> listRename = new ObservableCollection<FileRename>();
        private string _outputFolder;
        private bool _isFixExtension;
        private string _startText;
        private string _endText;
        private string _startNumber;
        private string _extension;
        private bool _isProcess;

        public BaseViewModel()
        {
            ListRename.Add(new FileRename() { FileName = "aasdasd" });
        }

        #region Property
        public string StartText
        {
            get { return _startText ?? ""; }
            set
            {
                _startText = value;
                RaisePropertyChanged(() => _startText);
            }
        }
        public string EndText
        {
            get { return _endText ?? ""; }
            set
            {
                _endText = value;
                RaisePropertyChanged(() => _endText);
            }
        }
        public string StartNumber
        {
            get { return _startNumber ?? "0"; }
            set
            {
                _startNumber = value;
                RaisePropertyChanged(() => _startNumber);
            }
        }
        public string OutputFolder
        {
            get { return _outputFolder ?? "C:\\"; }
            set
            {
                _outputFolder = value;
                RaisePropertyChanged(() => OutputFolder);
            }
        }
        public string Extension
        {
            get { return _extension ?? ""; }
            set
            {
                _extension = value;
                RaisePropertyChanged(() => Extension);
            }
        }
        public bool IsFixExtension
        {
            get { return _isFixExtension; }
            set
            {
                _isFixExtension = value;
                RaisePropertyChanged(() => _isFixExtension);
            }
        }
        public bool IsProcess
        {
            get { return _isProcess; }
            set
            {
                _isProcess = value;
                RaisePropertyChanged(() => IsProcess);
            }
        }
        public ObservableCollection<FileRename> ListRename
        {
            get { return listRename; }
            set
            {
                listRename = value;
                RaisePropertyChanged(() => ListRename);
            }
        }

        public ICommand OpenDialogCommand
        {
            get
            {
                return new RelayCommand<object>(x => OpenDialog());
            }
        }
        public ICommand RenameCommand
        {
            get
            {
                return new RelayCommand<object>(x => Rename());
            }
        }
        public ICommand ChooseOutputCommand
        {
            get
            {
                return new RelayCommand<object>(x => ChooseOutput());
            }
        }
        public ICommand CheckAllCommand
        {
            get
            {
                return new RelayCommand<object>(x => CheckAll(x));
            }
        }
        #endregion

        #region Excute Command
        private void CheckAll(object param)
        {
            bool isChecked; 
            Boolean.TryParse(param.ToString(), out isChecked);
            foreach(var item in ListRename)
            {
                item.IsChecked = isChecked;
            }
            RaisePropertyChanged(() => ListRename);
        }
        private async void Rename()
        {
            var _number = CheckAndConvertStringToNumber(StartNumber);
            if (_number == null)
            {
                MessageBox.Show("Start number invalid !");
            }
            foreach (var item in ListRename)
            {
                if (item.IsChecked)
                {
                    /*
                     * Có thể xẩy một số trường hợp tên file có sẵn ở trong thư mục output, cần in ra thông báo
                     *
                     */
                    IsProcess = true;
                    RaisePropertyChanged(() => IsProcess);
                    await Task.Run(() => Process(item, ref _number));

                    IsProcess = false;
                    RaisePropertyChanged(() => IsProcess);
                    
                }
            }
        }

        void Process(FileRename fileRename, ref int? number)
        {
            try
            {
                var path = OutputFolder + "\\" + StartText + number + EndText + Extension;
                if(!File.Exists(path))
                {
                    File.Copy(fileRename.FullPath, (path), false);
                    number++;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private int? CheckAndConvertStringToNumber(string startNumber)
        {
            int? number;
            try
            {
                number = int.Parse(startNumber);
            }
            catch(Exception)
            {
                return null;
            }
            return number;
        }

        private void ChooseOutput()
        {
            var dlg = new CommonOpenFileDialog();
            dlg.Title = "Choose Folder";
            dlg.IsFolderPicker = true;
            dlg.AddToMostRecentlyUsedList = false;
            dlg.AllowNonFileSystemItems = false;
            dlg.EnsureFileExists = true;
            dlg.EnsurePathExists = true;
            dlg.EnsureReadOnly = false;
            dlg.EnsureValidNames = true;
            dlg.Multiselect = false;
            dlg.ShowPlacesList = true;

            if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
            {
                OutputFolder = dlg.FileName;
                // Do something with selected folder string
            }
        }
        private void OpenDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.InitialDirectory = "D:\\";
            if (IsFixExtension == true)
            {
                if (Extension != string.Empty)
                {
                    openFileDialog.Filter = "(" + _extension + ")" + "|" + "*" + _extension + "|Text files (*.txt)|*.txt|MP4 Files(*.mp4)|*.mp4|All files (*.*)|*.*";
                }
            }
            else
            {
                openFileDialog.Filter = "Text files (*.txt)|*.txt|MP4 Files(*.mp4)|*.mp4|All files (*.*)|*.*";
            }

            if (openFileDialog.ShowDialog() == true)
            {
                foreach (string item in openFileDialog.FileNames)
                {
                    string fileName, extension, directory, fileNameWithExtension;

                    directory = item.Substring(0, item.LastIndexOf(@"\") + 1);
                    fileNameWithExtension = item.Substring(item.LastIndexOf(@"\") + 1);
                    if (fileNameWithExtension.LastIndexOf('.') == -1)
                    {
                        fileName = fileNameWithExtension.Substring(0);
                        extension = null;
                    }
                    else
                    {
                        fileName = fileNameWithExtension.Substring(0, fileNameWithExtension.LastIndexOf('.'));
                        extension = fileNameWithExtension.Substring(fileNameWithExtension.LastIndexOf('.'));
                    }

                    var obj = new FileRename()
                    {
                        IsChecked = true,
                        FileName = fileName,
                        Extension = extension,
                        Directory = directory,
                        FullPath = item
                    };
                    listRename.Add(obj);
                }
                RaisePropertyChanged(() => ListRename);
            }
        }
        #endregion
    }
}
