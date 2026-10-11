using Jogo_da_velha.Modelos;

Tabuleiros tabuleiro = new Tabuleiros();

bool jogarNovamente;
do
{
    char simboloPlayer = EscolherSimbolo();
    char simboloBot = simboloPlayer == 'X' ? 'O' : 'X';

    JogarPartida(tabuleiro, simboloPlayer, simboloBot);

    jogarNovamente = PerguntarNovaPartida();
} while (jogarNovamente);

Console.WriteLine("Obrigado por jogar!");

static char EscolherSimbolo()
{
    while (true)
    {
        Console.Clear();
        Console.WriteLine("Bem vindo ao jogo da velha!");
        Console.WriteLine("Você joga contra o bot em um tabuleiro 3x3. O símbolo 'X' sempre começa.");
        Console.WriteLine();
        Console.WriteLine("Digite 1 para jogar com 'X' ou 2 para jogar com 'O':");

        if (int.TryParse(Console.ReadLine(), out int opcao) && (opcao == 1 || opcao == 2))
        {
            return opcao == 1 ? 'X' : 'O';
        }

        Console.WriteLine("Entrada inválida. Por favor, digite 1 ou 2.");
        Thread.Sleep(1500);
    }
}

static void JogarPartida(Tabuleiros tabuleiro, char simboloPlayer, char simboloBot)
{
    tabuleiro.Reiniciar();
    char atual = 'X';

    while (true)
    {
        tabuleiro.ExibirTabuleiro();

        if (atual == simboloPlayer)
        {
            Console.WriteLine($"Sua vez! Você é o '{simboloPlayer}'.");
            tabuleiro.JogadaDoPlayer(atual);
        }
        else
        {
            Console.WriteLine($"O bot ('{simboloBot}') está jogando...");
            Thread.Sleep(800);
            tabuleiro.JogadaDoBot(atual);
        }

        if (tabuleiro.Venceu(atual))
        {
            tabuleiro.ExibirTabuleiro();
            Console.WriteLine(atual == simboloPlayer ? "Você venceu! 🎉" : "O bot venceu!");
            return;
        }

        if (tabuleiro.Empate())
        {
            tabuleiro.ExibirTabuleiro();
            Console.WriteLine("Deu velha! Empate.");
            return;
        }

        atual = atual == 'X' ? 'O' : 'X';
    }
}

static bool PerguntarNovaPartida()
{
    while (true)
    {
        Console.WriteLine();
        Console.WriteLine("Deseja jogar novamente? (S/N):");
        string? resposta = Console.ReadLine()?.Trim().ToUpper();

        if (resposta == "S") return true;
        if (resposta == "N") return false;

        Console.WriteLine("Resposta inválida. Digite S ou N.");
    }
}