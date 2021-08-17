using CandidateTesting.ElderLima.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateTesting.ElderLima.Services
{
    public static class ExecutorDeComandos
    {
        public static void ExecutarComando(string command)
        {
            if (command.Contains("convert"))
            { 
                var arraySplitEspaco = command.Split(" ");
                var source = new Source();
                source.BaixarConteudo(arraySplitEspaco[1]);
                var agora = new AgoraModel(source.ConverterTextoBrutoParaObjetoMinhaCdn());
                GravarArquivoDeLog(agora.ToString(), arraySplitEspaco[2]);
            }

        }

        private static void GravarArquivoDeLog(string conteudo, string path)
        {
            var pathList = path.Split("/").ToList();
            var nomeArquivo = pathList.LastOrDefault();
            pathList.Remove(nomeArquivo);
            DirectoryInfo di = Directory.CreateDirectory(string.Join("/", pathList));
            if (!File.Exists(path))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine(conteudo);
                }
                Console.WriteLine("Arquivo criado com Sucesso!!");
            }
        }

    }
}
