using CandidateTesting.ElderLima.Models;
using CandidateTesting.ElderLima.Services;
using System;
using System.IO;
using System.Linq;

namespace CandidateTesting.ElderLima
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Digite o comando desejado: ");
                var command = Console.ReadLine();
                ExecutorDeComandos.ExecutarComando(command);
            }
            catch (Exception e)
            {
                Console.WriteLine("Ocorreu algum erro ao tentar executar o comando :(  - " + e.Message);
            }
        }
    }
}

