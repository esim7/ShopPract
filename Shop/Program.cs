/*
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

            //List<Category> categories = new List<Category>
            //{
            //    new Category
            //    {
            //        Name = "Процессоры",
            //        ImagePath = @"C:\DataImage"
            //    },
            //    new Category
            //    {
            //        Name = "Видеокарты",
            //        ImagePath = @"C:\DataImage"
            //    },
            //    new Category
            //    {
            //        Name = "Оперативная память",
            //        ImagePath = @"C:\DataImage"
            //    },
            //    new Category
            //    {
            //        Name = "Материнские платы",
            //        ImagePath = @"C:\DataImage"
            //    },
            //    new Category
            //    {
            //        Name = "Жесткие диски",
            //        ImagePath = @"C:\DataImage"
            //    },
            //    new Category
            //    {
            //        Name = "Твердотельные накопители",
            //        ImagePath = @"C:\DataImage"
            //    },
            //    new Category
            //    {
            //        Name = "Блоки питания",
            //        ImagePath = @"C:\DataImage"
            //    },
            //    new Category
            //    {
            //        Name = "Корпуса",
            //        ImagePath = @"C:\DataImage"
            //    },
            //    new Category
            //    {
            //        Name = "Мониторы",
            //        ImagePath = @"C:\DataImage"
            //    },
            //    new Category
            //    {
            //        Name = "Мобильные телефоны",
            //        ImagePath = @"C:\DataImage"
            //    },
            //    new Category
            //    {
            //        Name = "Планшеты",
            //        ImagePath = @"C:\DataImage"
            //    },
            //    new Category
            //    {
            //        Name = "Кулеры для процессора",
            //        ImagePath = @"C:\DataImage"
            //    },
            //     new Category
            //    {
            //        Name = "Принтеры",
            //        ImagePath = @"C:\DataImage"
            //    },
            //      new Category
            //    {
            //        Name = "Наушники и гарнитуры",
            //        ImagePath = @"C:\DataImage"
            //    },
            //       new Category
            //    {
            //        Name = "Флэш накопители и карты памяти",
            //        ImagePath = @"C:\DataImage"
            //    },
            //         new Category
            //    {
            //        Name = "Серверы",
            //        ImagePath = @"C:\DataImage"
            //    }
            //};
            //Connection conect = new Connection();
            //conect.ConnectToDb();
            //WholeRepository repository = new WholeRepository(conect.providerName, conect.configurationRoot.GetConnectionString("DebugConnectionString"));
            //for(int i = 0; i < categories.Count; i++)
            //{
            //    repository.Categories.Add(categories[i]);
            //}
            //repository.Dispose();



            //Pagination Categories
            //Connection conect = new Connection();
            //conect.ConnectToDb();
            //WholeRepository repository = new WholeRepository(conect.providerName, conect.configurationRoot.GetConnectionString("DebugConnectionString"));
            //bool isExit = false;
            //string key;
            //int page = 1;
            //while (!isExit)
            //{
            //    Console.Clear();
            //    var result = repository.Categories.GetDataInDb(page);
            //    foreach (Category item in result)
            //    {
            //        Console.WriteLine("Категория товаров: " + item.Name + "\nПуть к фотографии: " + item.ImagePath);

            //    }
            //    //Console.SetCursorPosition(0, 15);
            //    Console.WriteLine("1. Следущая страница \n2. Предыдущая страница \n0. Выйти");
            //    //Console.SetCursorPosition(30, 30);
            //    //Console.WriteLine("Страница " + page);
            //    key = Console.ReadLine();
            //    if (key == "1")
            //    {
            //        page++;
            //    }
            //    else if (key == "2")
            //    {
            //        page--;
            //    }

            //}


            //GetAll categories
            //Connection conect = new Connection();
            //conect.ConnectToDb();
            //WholeRepository repository = new WholeRepository(conect.providerName, conect.configurationRoot.GetConnectionString("DebugConnectionString"));
            //ICollection<Category> categories = new List<Category>();
            //categories = repository.Categories.GetAll();


            //foreach (Category category in categories)
            //{

            //    Console.WriteLine(category.Id + "\t" + category.Name);
            //}


            //repository.Dispose();
            User temporaryUser = new User();
            bool isActive = true;
            string key;
            do
            {
                Console.Clear();
                Console.WriteLine("1. Зарегистрировать пользователя \n2. Войти в личный кабинет \n3. Поиск \n4. Просмотреть весь каталог товаров \n5. Выход");
                key = Console.ReadLine();
                switch (key)
                {
                    case "1":
                        {
                            Console.Clear();
                            User user = new User();
                            IRegistrationAccount registration = new AccountRegistration();
                            registration.Registration(user);
                        }
                        break;
                    case "2":
                        {
                            Console.Clear();
                            LogIn login = new LogIn();
                            login.LogInChecker(temporaryUser);
                            if (temporaryUser.IsLogged == true)
                            {
                                Console.Clear();
                                Console.WriteLine("1. Корзина \n2. История покупок \n3. Выйти из аккаунта \n4. Назад в главное меню");
                                string action;
                                action = Console.ReadLine();
                                switch (action)
                                {
                                    case "1":
                                        {

                                        }
                                        break;
                                    case "2":
                                        {

                                        }
                                        break;
                                    case "3":
                                        {
                                            login.LogOut(temporaryUser);
                                        }
                                        break;
                                    case "4":
                                        {
                                            break;
                                        }
                                        break;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Для доступа в личный кабинет нужно авторизоваться в системе");
                                Console.ReadKey();
                            }
                        }
                        break;
                    case "3":
                        {

                        }
                        break;
                    case "4":
                        {
                            Console.Clear();
                            string comand;
                            Console.WriteLine("1. Просмотр категорий товаров 2. Просмотр всего ассортимента товара");
                            comand = Console.ReadLine();
                            Connection conect = new Connection();
                            conect.ConnectToDb();
                            WholeRepository repository = new WholeRepository(conect.providerName, conect.configurationRoot.GetConnectionString("DebugConnectionString"));
                            bool isExit = false;
                            string command;
                            int page = 1;
                            while (!isExit)
                            {
                                Console.Clear();
                                if (comand == "1")
                                {
                                    try
                                    {
                                        var result = repository.Categories.GetDataInDb(page);
                                        foreach (Category category in result)
                                        {
                                            Console.WriteLine("Категория товаров: " + category.Name + "\nПуть к фотографии: " + category.ImagePath);
                                            Console.WriteLine("--------------------------------------------------------------------------");
                                        }
                                    }
                                    catch (ArgumentOutOfRangeException excepton)
                                    {
                                        Console.WriteLine("Ошибка, что продолжить просмотр листай страницу вперед");
                                    }
                                }
                                else if (comand == "2")
                                {
                                    try
                                    {
                                        int i = 0;
                                        var result = repository.Items.GetDataInDb(page);
                                        foreach (Item item in result)
                                        {
                                            Console.WriteLine("Товар № " + i);
                                            Console.WriteLine("Наименование товара: " + item.Name);
                                            Console.WriteLine("--------------------------------------------------------------------------");
                                            i++;
                                        }
                                    }
                                    catch (ArgumentOutOfRangeException excepton)
                                    {
                                        Console.WriteLine("Ошибка, что продолжить просмотр листай страницу вперед");
                                    }
                                }
                                //Console.SetCursorPosition(0, 15);
                                Console.WriteLine("\n\n1. Следущая страница \n2. Предыдущая страница \n3. Просмотреть товар \n0. Выйти");
                                //Console.SetCursorPosition(30, 30);
                                //Console.WriteLine("Страница " + page);
                                string action = Console.ReadLine();
                                if (action == "1")
                                {
                                    page++;
                                }
                                else if (action == "2")
                                {
                                    page--;
                                }
                                else if (action == "3")
                                {
                                    Console.WriteLine("Введите номер интересующего Вас товара");
                                    string input = Console.ReadLine();
                                    int index;
                                    Int32.TryParse(input, out index);

                                    ICollection<Item> items = new List<Item>().ToList();
                                    items = repository.Items.GetDataInDb(page);
                                    Item temporaryItem = new Item();
                                    temporaryItem = items.ElementAt(index);

                                    Console.Clear();
                                    Console.WriteLine("Наименование товара: " + temporaryItem.Name);
                                    Console.WriteLine("Описание товара: " + temporaryItem.Description);
                                    Console.WriteLine("Цена: " + temporaryItem.Price);

                                    Console.WriteLine("\n\n 1. Добавить в корзину 2. Добавить отзыв 3. Вернуться назад");
                                    string menuAction = Console.ReadLine();

                                    switch(menuAction)
                                    {
                                        case "1":
                                            {
                                                ShopBasket basket = new ShopBasket();
                                                basket.UserId = temporaryUser.Id;
                                                basket.ItemId = temporaryItem.Id;

                                                repository.ShopBaskets.Add(basket);
                                                Console.WriteLine("Товар успешно добавлен в корзину");
                                                Console.ReadKey();
                                            }
                                            break;
                                        case "2":
                                            {

                                            }
                                            break;
                                        case "3":
                                            {

                                            }
                                            break;
                                    }
                                    
                                }
                                else if (action == "0")
                                {
                                    isExit = true;
                                }

                            }
                        }
                        break;
                    case "5":
                        {
                            isActive = false;
                        }
                        break;
                }

                Console.ReadLine();

            } while (isActive != false);



        }
    }
}