﻿/*
 * 1. Регистрация и вход (смс-код / email код) - сделать до 11 октября
 * 2. История покупок
 * 3. Категории и товары (картинка в файловой системе)
 * 4. Покупка (корзина), оплата и доставка (PayPal/Qiwi/etc)
 * 5. Комментарии и рейтинги
 * 6. Поиск (пагинация)
 * 
 * Кто сделает 3 версии (Подключённый, автономный и EF) получит автомат на экзамене.
 */

using Microsoft.Extensions.Configuration;
using Shop.DataAccess;
using Shop.DataAccess.Abstract;
using Shop.Domain;
using Shop.Services;
using Shop.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace Shop.UI
{
    class Program
    {

        static void Main(string[] args)
        {
            //var builder = new ConfigurationBuilder()
            //    .SetBasePath(Directory.GetCurrentDirectory())
            //    .AddJsonFile("appsettings.json", false, true);

            //IConfigurationRoot configurationRoot = builder.Build();
            //string providerName = configurationRoot.GetSection("AppConfig").GetChildren().Single(/*item=>item.Key == "ProviderName"*/).Value;


            //DbProviderFactories.RegisterFactory(providerName, SqlClientFactory.Instance);

            //Category category = new Category
            //{
            //    Name = "Бытовая техника",
            //    ImagePath = "C:/data"
            //};

            //ICategoryRepository repository = new CategoryRepository(configurationRoot.GetConnectionString("DebugConnectionString"), providerName);
            //ICategoryRepository repository = new CategoryRepository(configurationRoot.GetConnectionString
            //repository.Add(category);
            //var result = repository.GetAll();

            //Registration User
            //User user = new User();
            //IRegistrationAccount registration = new AccountRegistration();
            //registration.Registration(user);

            //GetAll
            //Connection conect = new Connection();
            //conect.ConnectToDb();
            //WholeRepository repository = new WholeRepository(conect.providerName, conect.configurationRoot.GetConnectionString("DebugConnectionString"));
            //ICollection<User> users = new List<User>();
            //users = repository.Users.GetAll();
            //repository.Dispose();
            //foreach (User user in users)
            //{

            //    Console.WriteLine(user.Id + "\t" + user.Address + "\t" + user.CreationDate + "\t" + user.DeletedDate + "\t" + user.Email + "\t" + user.Password + "\t" + user.PhoneNumber + "\t" + user.VerificationCode);
            //}

            //Console.ReadKey();


            //Update User

            //Connection conect = new Connection();
            //conect.ConnectToDb();
            //WholeRepository repository = new WholeRepository(conect.providerName, conect.configurationRoot.GetConnectionString("DebugConnectionString"));




            //User user = new User();
            //ICollection<User> users = new List<User>().ToList();
            //users = repository.Users.GetAll();
            //user = users.ElementAt(4);
            //repository.Users.Update(user, "email", "dota2");
        }
    }
}