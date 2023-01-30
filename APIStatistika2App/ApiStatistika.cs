﻿using APIStatistikaApp.DataAccess.Data;
using APIStatistikaApp.DataAccess.Model;
using Microsoft.AspNetCore.Authorization;


namespace APIStatistikaApp
{
    public static class ApiStatistika
    {
        public static void ConfigureApi(this WebApplication app)
        {
            // Mapiranje metod.
            app.MapGet("/stat/logVsiKlici", LogVrniVseKlice);
            app.MapGet("/stat/logZadnjiKlic", LogVrniZadnjiKlic);
            app.MapGet("/stat/logTop5Klicov", LogVsotaTop5Klicov);
            app.MapGet("/stat/logVsotaKlicev", LogVsotaKlicev);
            app.MapPost("/stat/logVnosTestnegaKlica", LogVnosTestnegaKlica);
        }

        // Izpis celotnega LOG-a klicov storitev.
        private static async Task<IResult> LogVrniVseKlice(IStatistikaData data)
        {
            try
            {
                var result = Results.Ok(await data.LogVrniVseKlice());
                return result;
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        // Vrni zadnjo klicano storitev.
        private static async Task<IResult> LogVrniZadnjiKlic(IStatistikaData data)
        {
            try
            {
                var result = Results.Ok(await data.LogVrniZadnjiKlic());
                return result;
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        // Izpise najveckrat klicane storitve in stevilo klicov za top 5.
        private static async Task<IResult> LogVsotaTop5Klicov(IStatistikaData data)
        {
            try
            {
                //MyRabbitMq myRabbitMq = new MyRabbitMq("GET klic za izpis vseh zahtevkov ");
                var result = Results.Ok(await data.LogVsotaTop5Klicev());
                return result;
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        // Izpis stevila izvrsenih klicev posamezne storitve.
        private static async Task<IResult> LogVsotaKlicev(IStatistikaData data)
        {
            try
            {
                //MyRabbitMq myRabbitMq = new MyRabbitMq("GET klic za izpis vseh zahtevkov ");
                var result = Results.Ok(await data.LogVsotaKlicev());
                return result;
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        // LOG izvedene storitve.
        private static async Task<IResult> LogVnosTestnegaKlica(StatistikaModel statistikaModel, IStatistikaData data)
        {
            try
            {
                //MyRabbitMq myRabbitMq = new MyRabbitMq("POST klic za dodajanje zahtevka ");
                await data.LogVnosTestnegaKlica(statistikaModel);
                return Results.Ok();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }
    }
}
