using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace DirectorySync.ViewModels;

public abstract class ViewModelBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public virtual Task LoadAsync() => Task.CompletedTask;
}