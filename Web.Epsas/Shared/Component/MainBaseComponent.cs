using Microsoft.AspNetCore.Components;
using MudBlazor;
using Web.Epsas.Context.Abstract;

namespace Web.Epsas.Shared.Component;

public class MainBaseComponent : ComponentBase
{
    [Inject] private ISnackbar Snackbars { get; set; }
    [Inject] private IDialogService DialogService { get; set; }

    public void _MessageShow(string Message, State state)
    {
        switch (state)
        {
            case State.Success:
                Snackbars.Add(Message, Severity.Success);
                break;
            case State.Warning:
                Snackbars.Add(Message, Severity.Warning);
                break;
            case State.Error:
                Snackbars.Add(Message, Severity.Error);
                break;
            case State.NoData:
                Snackbars.Add(Message, Severity.Info);
                break;
            default:
                Snackbars.Add(Message, Severity.Normal);
                break;
        }
    }

    public void _DialogShow(string Message, State state)
    {
        var parameters = new DialogParameters();
        parameters.Add("message", Message);
        parameters.Add("state", state);
        DialogService.Show<DialogShow>("", parameters);
    }

    public async Task _MessageConfirm(string Message, System.Action aceptable)
    {
        var parameters = new DialogParameters();
        parameters.Add("message", Message);
        var dialog = DialogService.Show<DialogConfirm>("", parameters);
        var result = await dialog.Result;

        if (!result.Cancelled)
        {
            aceptable();
        }
    }
}
