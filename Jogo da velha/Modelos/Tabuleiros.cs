using System;
using System.Collections.Generic;
using System.Linq;

namespace Jogo_da_velha.Modelos
{
    public class Tabuleiros
    {
        private readonly Random random = new Random();
        private char[] casas = CriarCasas();

        private static readonly int[][] Combinacoes =
        {
            new[] { 0, 1, 2 }, new[] { 3, 4, 5 }, new[] { 6, 7, 8 }, // linhas
            new[] { 0, 3, 6 }, new[] { 1, 4, 7 }, new[] { 2, 5, 8 }, // colunas
            new[] { 0, 4, 8 }, new[] { 2, 4, 6 }                     // diagonais
        };

        private static char[] CriarCasas() =>
            new[] { '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        public void Reiniciar() => casas = CriarCasas();

        public void ExibirTabuleiro()
        {
            Console.Clear();
            Console.WriteLine("Tabuleiro:");
            Console.WriteLine($" {casas[0]} | {casas[1]} | {casas[2]} ");
            Console.WriteLine("---+---+---");
            Console.WriteLine($" {casas[3]} | {casas[4]} | {casas[5]} ");
            Console.WriteLine("---+---+---");
            Console.WriteLine($" {casas[6]} | {casas[7]} | {casas[8]} ");
            Console.WriteLine();
        }

        private bool CasaLivre(int posicao) =>
            casas[posicao] != 'X' && casas[posicao] != 'O';

        public void JogadaDoBot(char simboloBot)
        {
            List<int> livres = Enumerable.Range(0, 9).Where(CasaLivre).ToList();
            if (livres.Count == 0) return;

            int escolhida = livres[random.Next(livres.Count)];
            casas[escolhida] = simboloBot;
        }

        public void JogadaDoPlayer(char simboloPlayer)
        {
            while (true)
            {
                Console.WriteLine("Digite a posição que deseja preencher (1-9):");

                if (int.TryParse(Console.ReadLine(), out int posicao))
                {
                    posicao--;
                    if (posicao >= 0 && posicao <= 8 && CasaLivre(posicao))
                    {
                        casas[posicao] = simboloPlayer;
                        return;
                    }
                }

                Console.WriteLine("Posição inválida. Tente novamente.");
            }
        }

        public bool Venceu(char simbolo) =>
            Combinacoes.Any(c => casas[c[0]] == simbolo
                              && casas[c[1]] == simbolo
                              && casas[c[2]] == simbolo);

        public bool Empate() =>
            casas.All(c => c == 'X' || c == 'O');
    }
}