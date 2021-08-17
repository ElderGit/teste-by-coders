using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateTesting.ElderLima.Models
{
    public class AgoraModel
    {
        public AgoraModel(List<MinhaCdnModel> ListaMinhaCdn)
        {
            this.ListaMinhaCdn = ListaMinhaCdn;
            DataCriacao = DateTime.UtcNow;
        }
        public List<MinhaCdnModel> ListaMinhaCdn { get; }

        public DateTime DataCriacao { get; }

        public override string ToString()
        {
            if (!ListaMinhaCdn.Any()) return null;

            var retorno = $@"#Version: 1.0
#Date: {DataCriacao:G}
#Fields: provider http-method status-code uri-path time-taken response-size cache-status" + Environment.NewLine;

            foreach (var item in ListaMinhaCdn)
                retorno += $"\"MINHA CDN\" {item.HttpMethod} {item.StatusCode} {item.UriPath} {item.TimeTaken} {item.ResponseSize} {item.CacheStatus}{Environment.NewLine}";

            return retorno;
        }
    }
}
