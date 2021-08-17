using CandidateTesting.ElderLima.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CandidateTesting.ElderLima.Tests
{
    public class AgoraTest
    {
        private readonly string URL_EXEMPLO = "https://s3.amazonaws.com/uux-itaas-static/minha-cdn-logs/input-01.txt";
        
        [Fact]
        public void Deve_Converter_Couteudo_ListaMInhaCdnParaFOrmatoAgora()
        {
            //Arrange
            //Act  Necessário fazer o Arrange e Act juntos pois foi necessário buscar a hora exata em que a conversão foi feita
            var source = new Source();
            source.BaixarConteudo(URL_EXEMPLO);
            var agora = new AgoraModel(source.ConverterTextoBrutoParaObjetoMinhaCdn());

            var expected = $@"#Version: 1.0
#Date: {agora.DataCriacao:G}
#Fields: provider http-method status-code uri-path time-taken response-size cache-status
""MINHA CDN"" GET 200 /robots.txt 100 312 HIT
""MINHA CDN"" POST 200 /myImages 319 101 MISS
""MINHA CDN"" GET 404 /not-found 142 199 MISS
""MINHA CDN"" GET 200 /robots.txt 245 312 INVALIDATE" + Environment.NewLine; 
           
            
            //Assert
            Assert.Equal(expected, agora.ToString()); // usando o Replace para poder deixar o contúdo esperado identado
        }
    }
}
