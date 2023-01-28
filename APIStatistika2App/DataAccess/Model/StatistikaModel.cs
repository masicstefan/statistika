using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIStatistikaApp.DataAccess.Model
{
    public class StatistikaModel
    {
        public int IdStatistike { get; set; }
        public DateTime DatumVpisa { get; set; }
        public string ImeKlicaneStoritve { get; set; }
    }
}
