using APIStatistikaApp.DataAccess.Model;


namespace APIStatistikaApp.DataAccess.Data
{
    public interface IStatistikaData
    {
        Task<IEnumerable<StatistikaModel>> LogVrniVseKlice();
        Task<StatistikaModel> LogVrniZadnjiKlic();
        Task<IEnumerable<StatistikaModelCounter>> LogVsotaTop5Klicev();
        Task<IEnumerable<StatistikaModelCounter>> LogVsotaKlicev();
        Task LogVnosTestnegaKlica(StatistikaModel parameter);
    }
}
