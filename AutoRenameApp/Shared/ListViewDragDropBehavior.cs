using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Interactivity;
using AutoRenameApp.ViewModels;
using System.Collections.ObjectModel;
using AutoRenameApp.Models;

namespace AutoRenameApp.Shared
{
    public class ListViewDragDropBehavior : Behavior<ListView>
    {
        private ListViewDragDropManager<FileRename> dragManager;
        protected override void OnAttached()
        {
            base.OnAttached();
            dragManager = new ListViewDragDropManager<FileRename>(this.AssociatedObject);
            this.AssociatedObject.DragEnter += OnDragEnter;
            this.AssociatedObject.Drop += OnDrop;
            this.AssociatedObject.KeyUp += AssociatedObject_KeyUp;
            this.AssociatedObject.MouseLeftButtonDown += AssociatedObject_MouseLeftButtonDown;
        }

        private void AssociatedObject_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            (sender as ListView).Focus();
        }

        private void AssociatedObject_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(e.Key == System.Windows.Input.Key.Delete)
            {
                var listView = (sender as ListView);
                var currentIndex = listView.SelectedIndex;
                var itemSource = ((BaseViewModel)listView.DataContext).ListRename;
                List<FileRename> selectedItems = new List<FileRename>();
                foreach (FileRename item in listView.SelectedItems)
                {
                    selectedItems.Add(item);
                }
                foreach (FileRename item in selectedItems)
                {
                    itemSource.Remove(item);
                    listView.SelectedIndex = currentIndex;
                    if (listView.SelectedItems.Count == 0) return;
                }
            }
        }

        private void OnDrop(object sender, DragEventArgs e)
        {

        }

        private void OnDragEnter(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Move;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
        }
    }
}
