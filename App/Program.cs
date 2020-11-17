﻿using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using App.Clients;
using App.Models;
using Microsoft.Extensions.Configuration;

namespace App
{
    class Program
    {
        public static IConfigurationRoot Configuration { get; set; }

        static async Task Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();

            Console.WriteLine("Bem-vindo ao Sistema de Monitoramento em Tempo Real de Exchanges para Evitar Violinadas\n");

            var option = 0;

            while (option != 5)
            {
                Console.WriteLine("\n1 - Cadastrar Credenciais");
                Console.WriteLine("2 - Cadastrar Stop-Limit");
                Console.WriteLine("3 - Remover Stop-Limit");
                Console.WriteLine("4 - Listar Stop-Limits cadastrados");
                Console.WriteLine("5 - Sair");

                option = Convert.ToInt16(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        Console.WriteLine("Cadastrando credenciais ...");
                        await CreateExchangeCredential();

                        break;
                    case 2:
                        Console.WriteLine("Cadastrando stop-limit ...");
                        break;
                    case 3:
                        Console.WriteLine("Removendo stop-limit ...");
                        break;
                    case 4:
                        Console.WriteLine("Carregando ...");
                        break;
                    case 5:
                        Console.WriteLine("Até breve!");
                        break;
                    default:
                        Console.WriteLine("Opção inválida, tente novamente por favor");
                        break;
                }
            }
        }

        private static async Task CreateExchangeCredential()
        {
            var exchangeCredentialClient = new ExchangeCredentialClient(
                new HttpClient(),
                Configuration.GetValue<string>("ExchangeApiUrl")
            );

            var exchangeCredential = new ExchangeCredentialModel();
            exchangeCredential.UserId = Configuration.GetValue<Guid>("UserId");

            Console.WriteLine("\nDigite o ApiKey: ");
            exchangeCredential.ApiKey = Console.ReadLine();

            Console.WriteLine("\nDigite o ApiSecret:");
            exchangeCredential.ApiSecret = Console.ReadLine();

            exchangeCredential.Name = "Binance";

            await exchangeCredentialClient.CreateCredential(exchangeCredential);

            Console.WriteLine("\n Credenciais criadas com sucesso!");
        }
    }
}
