using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateTesting.ElderLima.Models
{
    public class Source
    {
        public string ConteudoUrlEmTexto { get; private set; }
        public void BaixarConteudo(string url)
        {
            var wc = new System.Net.WebClient();
            ConteudoUrlEmTexto = wc.DownloadString(url);
        }

        public List<MinhaCdnModel> ConverterTextoBrutoParaObjetoMinhaCdn()
        {

            var conteudoLimpo = ConteudoUrlEmTexto
                .Replace("\r", "");
            var linhas = conteudoLimpo.Split("\n");
            if (!linhas.Any()) throw new Exception("Conteúdo de url vazio");

            var listaRetorno = new List<MinhaCdnModel>();

            for (int i = 0; i < linhas.Length; i++)
            {
                try // try catch adicionado para poder informar em qual linha ocorreu o problema
                {
                    CriarMinhaCdnAPartirDeColunas(listaRetorno, linhas[i]);

                }
                catch (Exception e)
                {
                    throw new Exception($"Erro linha {i} - " + e.Message);
                }
            }

            return listaRetorno;
        }

        private static void CriarMinhaCdnAPartirDeColunas(List<MinhaCdnModel> listaRetorno, string linha)
        {
            var colunas = linha.Split("|");

            if (ValidarLinhaMinhaCdn(linha, colunas))
            {

                var minhaCdn = new MinhaCdnModel
                {
                    ResponseSize = colunas[0],
                    StatusCode = colunas[1],
                    CacheStatus = colunas[2],
                    HttpMethod = colunas[3].Split(" ").FirstOrDefault().Replace("\"", ""),
                    UriPath = colunas[3].Split(" ").FirstOrDefault(c => c.Contains("/")),
                    HTTPVersion = colunas[3].Split(" ").LastOrDefault().Replace("\"", ""),
                    TimeTaken = (int)Convert.ToDecimal(colunas[4], new CultureInfo("en-US"))
                };
                listaRetorno.Add(minhaCdn);
            }
        }

        private static bool ValidarLinhaMinhaCdn(string linha, string[] colunas)
        {
            if (string.IsNullOrEmpty(linha)) return false;
            if (colunas.Length < 5)
                throw new Exception("linha inválida. Número de campos insuficientes.");

            return true;
        }
    }
}
