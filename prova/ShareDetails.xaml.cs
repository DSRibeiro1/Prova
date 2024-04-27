using Dominio.Entidades;
using Integracao;
using Newtonsoft.Json;
namespace prova;

public partial class ShareDetails : ContentPage
{

    // Declara��o de vari�veis privadas
    private string _shareSymbol; // Armazena o s�mbolo da a��o

    private readonly BaseClient _client = new BaseClient(); // Inst�ncia de um cliente para intera��o com a API

    // Construtor da classe ShareDetails
    public ShareDetails(string shareSymbol)
    {
        InitializeComponent(); // Inicializa os componentes visuais da p�gina (deve vir primeiro)

        _shareSymbol = shareSymbol; // Atribui o s�mbolo da a��o fornecido ao campo privado _shareSymbol

        ShowShareDetails(_shareSymbol); // Chama o m�todo para exibir os detalhes da a��o
    }

    // M�todo ass�ncrono para exibir os detalhes da a��o
    public async Task ShowShareDetails(string shareSymbol)
    {
        try
        {
            // Faz uma solicita��o ass�ncrona � API para obter os detalhes da a��o
            HttpResponseMessage respostaAPI = await _client.GetShare(shareSymbol);

            string conteudo = await respostaAPI.Content.ReadAsStringAsync();

            // Desserializa o conte�do JSON recebido em um objeto do tipo Acao
            Acao acao = JsonConvert.DeserializeObject<Acao>(conteudo);

            


            // Atualiza a UI na thread principal com os detalhes da a��o
            MainThread.BeginInvokeOnMainThread(() =>
            {
                ShortName.Text = $"A��o = {acao.ShortName}";
                LongName.Text = $"Empresa = {acao.LongName}";
                Marketcap.Text = $"Valor de capitaliza��o = R${acao.MarketCap}";
                Value.Text = $"Valor atual = R${(acao.RegularMarketPrice)}";
                RegularMarketChange.Text = $"Varia��o do dia = R${(acao.RegularMarketChange)}";
                RegularMarketOpen.Text = $"Abertura do dia no mercado= R${(acao.RegularMarketOpen)}";

            });
        }
        catch (Exception ex)
        {

            // Trata exce��es (por exemplo, exibindo uma mensagem de erro para o usu�rio)
            MainThread.BeginInvokeOnMainThread(() =>
            {
                // Atualiza a UI para refletir o erro (por exemplo, exibindo uma mensagem de erro na tela)
            });
        }
    }
}





    
