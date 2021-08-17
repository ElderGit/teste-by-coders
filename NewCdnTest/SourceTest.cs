using CandidateTesting.ElderLima.Models;
using System;
using System.Linq;
using Xunit;

namespace CandidateTesting.ElderLima.Tests
{
    public class SourceTest
    {
        private readonly string URL_EXEMPLO = "https://s3.amazonaws.com/uux-itaas-static/minha-cdn-logs/input-01.txt";
        [Fact]
        public void Deve_Baixar_Couteudo_De_Url_De_Exemplo()
        {
            //Arrange
            var source = new Source();
            var expected = $@"312|200|HIT|""GET /robots.txt HTTP/1.1""|100.2
                            101|200|MISS|""POST /myImages HTTP/1.1""|319.4
                            199|404|MISS|""GET /not-found HTTP/1.1""|142.9
                            312|200|INVALIDATE|""GET /robots.txt HTTP/1.1""|245.1
                            ";
            //Act
          source.BaixarConteudo(URL_EXEMPLO);

            //Assert
            Assert.Equal(expected.Replace(" ", ""), source.ConteudoUrlEmTexto.Replace(" ", "")); // usando o Replace para poder deixar o contúdo esperado identado
        }

        [Fact]
        public void Deve_Gerar_Exception_Conteudo_Vazio()
        {
            //Arrange
            var source = new Source();

            //Act
            source.BaixarConteudo("https://google.com.br");
            //Assert
            Assert.Throws<Exception>(() => source.ConverterTextoBrutoParaObjetoMinhaCdn()); // usando o Replace para poder deixar o contúdo esperado identado
        }


        [Fact]
        public void Deve_Converter_Conteudo_Baixado_Em_Objeto_MinhaCdn()
        {
            //Arrange
            var source = new Source();
            source.BaixarConteudo(URL_EXEMPLO);

            //Act
            var listaMinhaCdn = source.ConverterTextoBrutoParaObjetoMinhaCdn();

            //Assert
            Assert.Equal(4, listaMinhaCdn.Count);
            Assert.Equal("312", listaMinhaCdn.FirstOrDefault().ResponseSize);
            Assert.Equal("HIT", listaMinhaCdn.FirstOrDefault().CacheStatus);
            Assert.Equal("GET", listaMinhaCdn.FirstOrDefault().HttpMethod);
            Assert.Equal("200", listaMinhaCdn.FirstOrDefault().StatusCode);
            Assert.Equal("/robots.txt", listaMinhaCdn.FirstOrDefault().UriPath);
            Assert.Equal("HTTP/1.1", listaMinhaCdn.FirstOrDefault().HTTPVersion);
            Assert.Equal(100, listaMinhaCdn.FirstOrDefault().TimeTaken);
        }
    }
}
