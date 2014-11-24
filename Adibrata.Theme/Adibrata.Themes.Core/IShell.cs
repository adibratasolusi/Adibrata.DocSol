using Adibrata.Themes.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adibrata.Themes.Prototype.WPF
{
    public interface IShell
    {
        bool IsLightVersion { get; }

        void ExitApplication();

        void ShowMessage(string message, string title);

        IWindow CreateWindow(object userControl, string title);
    }
}
