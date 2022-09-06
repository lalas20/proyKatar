using MudBlazor;
using Web.Epsas.Context.Abstract;
using Web.Epsas.Context.Model;
using Web.Epsas.Dtos;

namespace Web.Epsas.Pages.Auth
{
    public partial class Login
    {
        private const InputType password = InputType.Password;
        private AuthModel _tokenModel = new();
        bool PasswordVisibility;
        InputType PasswordInput = password;
        string PasswordInputIcon = Icons.Material.Filled.VisibilityOff;


        async void SubmitAsync()
        {
            _Loading.Show();
            var result = await _Rest.PostLoginAsync<LoginResponse>("Login", _tokenModel);
            if (result.State != State.Success)
            {
                _MessageShow(result.Message, result.State);
                _DialogShow(result.Message, result.State);
                _Loading.Hide();
                return;
            }
            else
            {
                await _LoginService.LoginAsync(result);
                await _storage.SetValue<LoginResponse>("USER", result.Data);
                _navMgr.NavigateTo("/Home", true);
            }
        }

        void TogglePasswordVisibility()
        {
            if (PasswordVisibility)
            {
                PasswordVisibility = false;
                PasswordInputIcon = Icons.Material.Filled.VisibilityOff;
                PasswordInput = password;
            }
            else
            {
                PasswordVisibility = true;
                PasswordInputIcon = Icons.Material.Filled.Visibility;
                PasswordInput = InputType.Text;
            }
        }


        protected override async Task OnInitializedAsync()
        {
            await _LoginService.LogoutnAsync();
            _Loading.Hide();
        }

    }
}
