namespace CovidTracker.Web.ViewModels;

public class ViewModelBase
{
    public Action StateHasChanged { get; set; } = () => { };

    public virtual Task OnInitializedAsync()
    {
        return Task.CompletedTask;
    }

    public virtual Task OnParametersSetAsync()
    {
        return Task.CompletedTask;
    }

    public virtual Task OnAfterRenderAsync(bool firstRender)
    {
        return Task.CompletedTask;
    }
}
