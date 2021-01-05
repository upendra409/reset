using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tasks.backend.initservice.Models
{
    public class Character
    {
        public string Identifier { get; set; }

        public string Name { get; set; }
        public string NickName { get; set; }
        public string CreatedBy { get; set; }
        public string Universe { get; set; }
        public string Popularity { get; set; }
        public Rate Rate { get; set; }
    }
    public class Rate
    {
        public string Identifier { get; set; }
        public Dictionary<string, double> Rates { get; set; }
        public string Base { get; set; }
    }
}
