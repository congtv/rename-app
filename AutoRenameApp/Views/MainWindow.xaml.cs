using AutoRenameApp.Models;
using AutoRenameApp.ViewModels;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace AutoRenameApp.Views
{
    /// <summary>
    /// Ứng dụng dùng để dổi tên file , có một số chức năng sau
    ///     + Chọn nhiều file trong cùng hoặc khác thư mục (mở thư mục dùng OpenFileDialog)
    ///         - Có thể chọn định dạng mặc định khi mở lên ( cộng chuỗi vào property Filter của OpenFileDialog (Tên extension) | *.(định dang)
    ///     + Sau khi chọn thì có thể bỏ những file ko muốn đổi tên ( sử dụng checkbox theo mỗi tên file)
    ///         - Có nút Select all để chọn hoặc bỏ chọn tất cả checkbox
    ///         - Có thể kéo thả các item để sắp xếp thứ tự cũng như index của file ( chắc phải sử dụng library bên thứ 3)
    ///     + Đổi tên, có một số chú ý sau
    ///         - Có kí tự bắt đầu hoặc không
    ///         - Có số để thấy sự khác nhau giữa các file
    ///         - Có kí tự kết thúc hoặc không
    ///         - Chọn output đầu ra hoặc mặc định là thư mục chọn file đầu tiên hoặc một thư mục default nào đó
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            BaseViewModel baseViewModel = new BaseViewModel();
            this.DataContext = baseViewModel;
        }
    }
}