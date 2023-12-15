using Microsoft.IdentityModel.Tokens;
using RestaurantOpdracht_BL.Interfaces;
using RestaurantOpdracht_BL.Managers;
using RestaurantOpdracht_BL.Model;
using RestaurantOpdracht_EF.Model;
using RestaurantOpdracht_EF.Repositories;
using System;
using System.Runtime.ConstrainedExecution;

namespace RestaurantOpdracht_Console_App {
    internal class Program {
        static void Main(string[] args) {
            IRestaurantRepository restoRepo = new RestaurantRepository();
            IKlantRepository klantRepo = new KlantRepository();
            IReservatieRepository resRepo = new ReservatieRepository();
            RestaurantManager restaurantManager = new(restoRepo);
            KlantManager klantManager = new(klantRepo);
            ReservatieManager reservatieManager = new(resRepo, restoRepo);

            #region
            int keuze = 99;

            while (keuze != 0) {

                Console.WriteLine(
                    "1. DB herstarten & opvullen\n" +
                    "2. Reservaties naar DB schrijven\n");

                Console.Write("Maak een keuze (0 om af te sluiten): ");
                bool correctInput = int.TryParse(Console.ReadLine(), out keuze);
                if (correctInput) {
                    switch (keuze) {
                        case 1:
                            //1: DB resetten & opvullen
                            RestaurantContext ctx = new();
                            ctx.Database.EnsureDeleted();
                            ctx.Database.EnsureCreated();

                            Console.WriteLine("\nDB deleted & created");
                            List<Contactgegevens> contactgegevens = new List<Contactgegevens>();
                            List<List<Tafel>> tafels = new List<List<Tafel>>();
                            
                            Random random = new Random();
                            for (int i = 0; i < 20; i++) {
                                contactgegevens.Add(new Contactgegevens(
                                    tel: random.Next(100000000, 999999999),
                                    email: $"email{i}@example.com",
                                    postcode: random.Next(1000, 9999),
                                    gemeentenaam: $"Gemeente{i}",
                                    straatnaam: (i % 2 == 0) ? $"Straat{i}" : null, // Assigning null for even numbers
                                    huisNr: (i % 3 == 0) ? $"HuisNr{i}" : null   // Assigning null for numbers divisible by 3
                                ));
                            }
                            for (int i = 0; i < 10; i++) {
                                List<Tafel> newTafels = new List<Tafel>();
                                for (int j = 0; j < 10; j++) {
                                    newTafels.Add(new Tafel(tafelNr: j+1, aantalPlaatsen: random.Next(1, 7)));
                                }
                                tafels.Add(newTafels);
                            }
                            List<string> restoNamen = new List<string>() {
                                "Chez Nous",
                                "De Visser",
                                "'t Standbeeldje",
                                "Gelijk Bij Ons Moeder",
                                "Ter Land, Ter Zee en In De Lucht",
                                "Brasserie AGV",
                                "De Ronde Tafel",
                                "Pico Bello",
                                "De Manke Schot",
                                "De Duivelse Vork",
                            };
                            List<string> keukenNamen = new List<string>() {
                                "Italiaans",
                                "Vegetarisch",
                                "Oosters",
                                "Traditioneel Vlaams",
                                "Grieks",
                            };
                            for (int i = 0; i < 10; i++) {
                                restaurantManager.VoegRestaurantToe(
                                    restoNamen[i],
                                    keukenNamen[Convert.ToInt32(Math.Floor(i / 2d))],
                                    contactgegevens[i],
                                    tafels[i]
                                    );
                            }
                            List<string> klantenNamen = new List<string>() {
                                "Lucas Verbeek",
                                "Emma Janssen",
                                "Milan de Jong",
                                "Sophia Bakker",
                                "Levi van Dijk",
                                "Olivia van der Berg",
                                "Noah Hendriks",
                                "Tess van Leeuwen",
                                "Finn ",
                                "Nora Bos",
                            };
                            for (int i = 0; i < 10; i++) {
                                klantManager.RegistreerKlant(
                                    klantenNamen[i],
                                    contactgegevens[i + 10]
                                    );
                            }

                            Console.WriteLine("\nDB gevuld\n");
                            break;

                        case 2:
                            //2. Reservaties maken
                            Random rnd = new Random();
                            List<DateTime> data = new List<DateTime>();
                            List<Reservatie> reservaties = new List<Reservatie>();
                            for (int i = 0; i < 10; i++) {
                                DateTime randomDate = DateTime.Now.AddDays(rnd.Next(1, 365));

                                int minutes = rnd.Next(0, 2) * 30;
                                randomDate = new DateTime(randomDate.Year, randomDate.Month, randomDate.Day, randomDate.Hour, minutes, 0);

                                data.Add(randomDate);
                            }
                            for (int i = 0; i < 10; i++) {
                                Restaurant resto = restaurantManager.GeefRestaurant(i+1);
                                Klant klant = klantManager.GeefKlant(i + 1);
                                reservatieManager.MaakReservatie(
                                    klant,
                                    resto,
                                    rnd.Next(1, 7),
                                    data[i],
                                    resto.Tafels[rnd.Next(0,10)].TafelNr
                                    );
                            }
                            Console.WriteLine("\nReservaties weggeschreven\n");
                            break;
                    }
                } else {
                    Console.WriteLine("\n--Maak een geldige keuze--\n");
                }
            }
        }
        #endregion
    }
}
