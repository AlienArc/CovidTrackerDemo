using Microsoft.AspNetCore.Components;

namespace CovidTracker.Web.ViewModels;

public class ViewModelComponentBase<TViewModel> : ComponentBase where TViewModel : ViewModelBase
{
    [Inject]
    public TViewModel ViewModel { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        ViewModel.StateHasChanged = StateHasChanged;
        await ViewModel.OnInitializedAsync();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await ViewModel.OnAfterRenderAsync(firstRender);
    }

    protected override async Task OnParametersSetAsync()
    {
        await ViewModel.OnParametersSetAsync();
    }
}