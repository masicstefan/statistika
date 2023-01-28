using APIStatistikaApp.DataAccess.Model;


namespace APIStatistikaApp.DataAccess.Data
{
    public interface IStatistikaData
    {
        Task<IEnumerable<StatistikaModel>> VrniVsePodatke();
        Task<StatistikaModelCounter> NaloziNajpogostejeKlicanEP();
        Task<StatistikaModel> NaloziZadnjeKlicanEP();
        Task<IEnumerable<StatistikaModelCounter>> SteviloEPKlicev();
        Task ShraniNoviKlicEP(StatistikaModel parameter);
    }
}
