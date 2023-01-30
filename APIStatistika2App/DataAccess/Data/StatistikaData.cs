using APIStatistikaApp.DataAccess.DbAccess;
using APIStatistikaApp.DataAccess.Model;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace APIStatistikaApp.DataAccess.Data
{
    public class StatistikaData : IStatistikaData
    {
        private readonly ISQLDataAccess _db;

        public StatistikaData(ISQLDataAccess db)
        {
            _db = db;
        }

        // Vrni vse podatke.
        public async Task<IEnumerable<StatistikaModel>> LogVrniVseKlice()
        {
            string sqlString = "SELECT * FROM tabStatistika";
            
            //var result = await _db.LoadOne<StatistikaModel, dynamic>(sqlString, new { });
            var result = await _db.LoadOne<StatistikaModel, dynamic>(sqlString, new { });
            //return result.FirstOrDefault();
            return result;
        }

        // Vrni enega od zadnjih LOG-ov (MAX DatumVpisa).
        public async Task<StatistikaModel> LogVrniZadnjiKlic()
        {
            string sqlString = "SELECT * " + 
                               "  FROM tabStatistika " + 
                               " WHERE DatumVpisa = (SELECT MAX(DatumVpisa) FROM tabStatistika)";
            
            var result = await _db.LoadOne<StatistikaModel, dynamic>(sqlString, new { });
            return result.FirstOrDefault();
        }

        // Grupiraj vse klice in izpisi njihovo stevilo za top 5 klicov.
        public async Task<IEnumerable<StatistikaModelCounter>> LogVsotaTop5Klicev()
        {
            string sqlString = "SELECT TOP 5 ImeKlicaneStoritve, SteviloKlicev " +
                               "  FROM (SELECT ImeKlicaneStoritve, Count(*) AS SteviloKlicev " +
                                       " FROM tabStatistika GROUP BY ImeKlicaneStoritve) AS tabTemp " +
                                       " WHERE ImeKlicaneStoritve > '' " +
                                       " ORDER BY SteviloKlicev DESC ";

            //var result = await _db.LoadOne<StatistikaModelCounter, dynamic>(sqlString, new { });
            //return result.FirstOrDefault();  // <--- vrne samo ENEGA, TO NI V REDU.
            var result = await _db.LoadAll<StatistikaModelCounter>(sqlString);
            return result;
        }

        // Seznam vseh klicev in njihovo stevilo.
        public async Task<IEnumerable<StatistikaModelCounter>> LogVsotaKlicev()
        {
            string sqlString = "SELECT ImeKlicaneStoritve, COUNT(*) AS SteviloKlicev " +
                               "  FROM tabStatistika " +
                               " GROUP BY ImeKlicaneStoritve";
            
            var result = await _db.LoadAll<StatistikaModelCounter>(sqlString);
            return result;
        }

        // Vnos novega zapisa v LOG (za potrebe testa).
        public async Task LogVnosTestnegaKlica(StatistikaModel parameter)
        {
            string sqlString = "INSERT INTO tabStatistika (DatumVpisa, ImeKlicaneStoritve) " +
                               " VALUES(@DatumVpisa, @ImeKlicaneStoritve)";

            var _parameter = new DynamicParameters();
            // _parameter.Add("IdStatistike", parameter.IdStatistike, DbType.Int32);
            _parameter.Add("DatumVpisa", parameter.DatumVpisa, DbType.Date);
            _parameter.Add("ImeKlicaneStoritve", parameter.ImeKlicaneStoritve, DbType.String);

            await _db.SaveOne<DynamicParameters>(sqlString, _parameter);
        }
    }
}
