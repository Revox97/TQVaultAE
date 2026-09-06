using System;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using TQVaultAE.ViewModels;

namespace TQVaultAE
{
    public class ViewLocator : IDataTemplate
    {

        public Control? Build(object? param)
        {
            if (param is null)
                return null;

            string name = param.GetType().FullName!.Replace("ViewModel", "View", StringComparison.Ordinal);
            Type? type = Type.GetType(name);

            return type is not null
                ? (Control)Activator.CreateInstance(type)!
                : new TextBlock { Text = "Not Found: " + name };
        }

        public bool Match(object? data) => data is ViewModelBase;
    }
}
