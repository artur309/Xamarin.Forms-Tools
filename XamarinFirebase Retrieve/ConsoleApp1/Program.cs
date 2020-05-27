using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using Models;

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
                Console.WriteLine($"{receita.Key} is {receita.Object.NomeReceita}m high.");

            Console.WriteLine("ok");
            Console.ReadKey();

            bool IsValidEmail(string email)
            {
                try
                {
                    var addr = new System.Net.Mail.MailAddress(email);
                    return addr.Address == email;
                }
                catch
                {
                    return false;
                }
            }

            Console.WriteLine("\n\nNome para o novo email: ");
            string entry = Console.ReadLine();

            if (IsValidEmail(entry))
                Console.WriteLine("TA FIXE :)");
            else
                Console.Write("Ta Horrivel");

            Console.WriteLine("---------------------------------------------------------");
            Console.ReadKey();

            int IDUser = 0;

            var idCount = await firebase
                     .Child("Users")
                     .OrderByKey()
                     .OnceAsync<Users>();

            foreach (var id in idCount)
            {
                //id automatico 
                IDUser++;
                Console.WriteLine(IDUser);
                if (id.Object.Id == IDUser)
                    IDUser++;

            }

            bool? accountAvailable = null;

            //verificaoca de Utilizador
            foreach (var id in idCount)
            {
                if (id.Object.Email != entry)
                    accountAvailable = true;
                else
                    accountAvailable = false;
            }

            //verificaoca de Utilizador
            if (accountAvailable == true)
            {
                Console.WriteLine("Erro");
                var user = await firebase
                .Child("Users")
                .PostAsync(new Users()
                {
                    Email = entry,
                    Id = IDUser,
                });
                Console.WriteLine($"{user.Object.Email} ---");
            }
            else
            {
                Console.WriteLine("Erro");
            }

            Console.ReadKey();
            Console.WriteLine("ok");
            Console.ReadKey();
        }

        public async Task<Users> GetPerson(string personId)
        {
            var firebase = new FirebaseClient("https://trofimon-pap.firebaseio.com/");
            var allPersons = await GetAllPersons();
            await firebase
              .Child("Users")
              .OnceAsync<Users>();
            return allPersons.Where(a => a.Email == personId).FirstOrDefault();
        }

        public async Task<List<Users>> GetAllPersons()
        {
            var firebase = new FirebaseClient("https://trofimon-pap.firebaseio.com/");

            return (await firebase
              .Child("Users")
              .OnceAsync<Users>()).Select(item => new Users
              {
                  Email = item.Object.Email,
                  Id = item.Object.Id,
              }).ToList();
        }
    }
}