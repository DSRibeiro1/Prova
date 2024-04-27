namespace prova
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void CliqueBuscarInformacoes(object sender, EventArgs e)
        {
            string simboloAcao = campoSimbolo.Text; // Obtém o texto inserido na entrada de texto chamada campoSimbolo
            ShareDetails shareDetails = new ShareDetails(simboloAcao); // Cria uma instância da classe ShareDetails com o símbolo da ação
            await Navigation.PushAsync(shareDetails); // Navega para a página de detalhes da ação
            SemanticScreenReader.Announce(BuscarInformacoes.Text); // Anuncia o texto do botão "Buscar Informações" para acessibilidade
        }
    }
}

