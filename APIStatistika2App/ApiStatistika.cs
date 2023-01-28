using APIStatistikaApp.DataAccess.Data;
using APIStatistikaApp.DataAccess.Model;
using Microsoft.AspNetCore.Authorization;


namespace APIStatistikaApp
{
    public static class ApiStatistika
    {
        public static void ConfigureApi(this WebApplication app)
        {
            // Mapiranje metod.
            app.MapGet("/stat/vsiPodatki", VrniVsePodatke);
            app.MapGet("/stat/zadnjiKlicanEP", NaloziZadnjeKlicanEP);
            app.MapGet("/stat/najpogostejeKlicanEP", NaloziNajpogostejeKlicanEP);
            app.MapGet("/stat/steviloEPKlicev", SteviloEPKlicev);
            app.MapPost("/stat/noviKlicEP", NoviKlicEP);
        }

        private static async Task<IResult> VrniVsePodatke(IStatistikaData data)
        {
            try
            {
                var result = Results.Ok(await data.VrniVsePodatke());
                return result;
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        private static async Task<IResult> NaloziZadnjeKlicanEP(IStatistikaData data)
        {
            try
            {
                var result = Results.Ok(await data.NaloziZadnjeKlicanEP());
                return result;
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        // Izpise storitev, ki so klicana najveckrat.
        private static async Task<IResult> NaloziNajpogostejeKlicanEP(IStatistikaData data)
        {
            try
            {
                //MyRabbitMq myRabbitMq = new MyRabbitMq("GET klic za izpis vseh zahtevkov ");
                var result = Results.Ok(await data.NaloziNajpogostejeKlicanEP());
                return result;
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        // Izpis stevila izvrsenih klicev posamezne storitve.
        private static async Task<IResult> SteviloEPKlicev(IStatistikaData data)
        {
            try
            {
                //MyRabbitMq myRabbitMq = new MyRabbitMq("GET klic za izpis vseh zahtevkov ");
                var result = Results.Ok(await data.SteviloEPKlicev());
                return result;
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        // LOG izvedene storitve.
        private static async Task<IResult> NoviKlicEP(StatistikaModel statistikaModel, IStatistikaData data)
        {
            try
            {
                //MyRabbitMq myRabbitMq = new MyRabbitMq("POST klic za dodajanje zahtevka ");
                await data.ShraniNoviKlicEP(statistikaModel);
                return Results.Ok();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);

            }
        }
    }
}
