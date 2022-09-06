namespace Web.Epsas.Context.Model
{
    public class LoadingService
    {
        public bool vVisble { get; set; } = false;

        public Action CambiarEstado;

        public void Show()
        {
            this.vVisble = true;
            CambiarEstado?.Invoke();
        }

        public void Hide()
        {
            this.vVisble = false;
            CambiarEstado?.Invoke();
        }
    }
}
