using System;
using TanteBazar.WebApi.Client;

namespace TanteBazar.Example.Console
{
    class Program
    {
        public static Client ApiClient;

        static void Main(string[] args)
        {
            ApiClient = new Client(new Configuration("http://localhost:5000", "D32EB8F6-12FB-4BD0-BCFD-0607FD99AA97"));

            System.Console.WriteLine("Getting the list of items from the API using the client");
            var items = ApiClient.GetItemsAsync().Result;

            int id = 0;
            foreach(var item in items)
            {
                id++;

                System.Console.WriteLine($"ItemId: {id}, ItemName: {item.Name}, ItemDescription: {item.Description}, ItemUnitPrice:{item.UnitPrice}");
                System.Console.WriteLine();
            }

            System.Console.WriteLine("Getting this user's basket from the API using the client");
            var basket = ApiClient.GetBasketAsync().Result;

            System.Console.WriteLine($"Total item in basket: {basket.TotalItems}, TotalPrice: {basket.TotalPrice}");
            foreach(var basketItem in basket.Items)
            {
                System.Console.WriteLine($"{basketItem.Quantity} x {basketItem.ItemName} = {basketItem.UnitPrice}");
            }

            System.Console.ReadKey();
        }
    }
}
