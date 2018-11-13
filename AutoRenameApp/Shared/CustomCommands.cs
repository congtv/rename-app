using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AutoRenameApp.Shared
{
    public static class CustomCommands
    {
        public static readonly RoutedUICommand OpenInput = new RoutedUICommand
                        (
                                "OpenInput",
                                "OpenInput",
                                typeof(CustomCommands),
                                new InputGestureCollection()
                                {
                                        new KeyGesture(Key.I, ModifierKeys.Control)
                                }
                        );
        public static readonly RoutedUICommand OpenOutput = new RoutedUICommand
                (
                        "OpenOutput",
                        "OpenOutput",
                        typeof(CustomCommands),
                        new InputGestureCollection()
                                {
                                        new KeyGesture(Key.O, ModifierKeys.Control)
                                }
                );
        //Define more commands here, just like the one above
    }
}
