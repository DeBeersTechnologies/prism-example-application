using Prism.Mvvm;
using Prism.Navigation;

namespace application.models;
public abstract class ViewModelBase : BindableBase, IDestructible
{
    protected ViewModelBase()
    {

    }

    public virtual void Destroy()
    {

    }
}
