using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;

namespace ConsoleApp1
{
    class Program
    {
        public static void Main(string[] args)
        {
            new Program().Run().Wait();
        }

        private async Task Run()
        {
            // Since the dinosaur-facts repo no longer works, populate your own one with sample data
            // in "sample.json"
            var firebase = new FirebaseClient("https://trofimon-pap.firebaseio.com/");

            var receitas = await firebase
                     .Child("Receitas")
                     .Child("12")
                     .OrderByKey()
                     .OnceAsync<Receitas>();

            foreach (var receita in receitas)
            {
                Console.WriteLine($"{receita.Key} is {receita.Object.NomeReceita}m high.");
            }

            Console.WriteLine("ok");
            Console.ReadKey();
        }
    }
}