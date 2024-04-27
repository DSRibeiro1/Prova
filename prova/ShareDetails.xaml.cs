using Dominio.Entidades;
using Integracao;
using Newtonsoft.Json;
namespace prova;

public partial class ShareDetails : ContentPage
{

    // Declaração de variáveis privadas
    private string _shareSymbol; // Armazena o símbolo da ação

    private readonly BaseClient _client = new BaseClient(); // Instância de um cliente para interação com a API

    // Construtor da classe ShareDetails
    public ShareDetails(string shareSymbol)
    {
        InitializeComponent(); // Inicializa os componentes visuais da página (deve vir primeiro)

        _shareSymbol = shareSymbol; // Atribui o símbolo da ação fornecido ao campo privado _shareSymbol

        ShowShareDetails(_shareSymbol); // Chama o método para exibir os detalhes da ação
    }

    // Método assíncrono para exibir os detalhes da ação
    public async Task ShowShareDetails(string shareSymbol)
    {
        try
        {
            // Faz uma solicitação assíncrona à API para obter os detalhes da ação
            HttpResponseMessage respostaAPI = await _client.GetShare(shareSymbol);

            string conteudo = await respostaAPI.Content.ReadAsStringAsync();

            // Desserializa o conteúdo JSON recebido em um objeto do tipo Acao
            Acao acao = JsonConvert.DeserializeObject<Acao>(conteudo);

            


            // Atualiza a UI na thread principal com os detalhes da ação
            MainThread.BeginInvokeOnMainThread(() =>
            {
                ShortName.Text = $"Ação = {acao.ShortName}";
                LongName.Text = $"Empresa = {acao.LongName}";
                Marketcap.Text = $"Valor de capitalização = R${acao.MarketCap}";
                Value.Text = $"Valor atual = R${(acao.RegularMarketPrice)}";
                RegularMarketChange.Text = $"Variação do dia = R${(acao.RegularMarketChange)}";
                RegularMarketOpen.Text = $"Abertura do dia no mercado= R${(acao.RegularMarketOpen)}";

            });
        }
        catch (Exception ex)
        {

            // Trata exceções (por exemplo, exibindo uma mensagem de erro para o usuário)
            MainThread.BeginInvokeOnMainThread(() =>
            {
                // Atualiza a UI para refletir o erro (por exemplo, exibindo uma mensagem de erro na tela)
            });
        }
    }
}





    
