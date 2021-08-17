using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateTesting.ElderLima.Models
{
    public class MinhaCdnModel
    {
        public string ResponseSize { get; set; }

        public string StatusCode { get; set; }

        public string CacheStatus { get; set; }
        
        public string HttpMethod { get; set; }

        public string UriPath { get; set; }

        public string HTTPVersion { get; set; }

        public int TimeTaken { get; set; }
    }
}
