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
                                bool basketIsExit = false;
                                action = Console.ReadLine();
                                switch (action)
                                {
                                    case "1":
                                        {
                                            int page = 1;
                                            while (!basketIsExit)
                                            {
                                                Console.Clear();
                                                Connection conect = new Connection();
                                                conect.ConnectToDb();
                                                WholeRepository repository = new WholeRepository(conect.providerName, conect.configurationRoot.GetConnectionString("DebugConnectionString"));
                                                try
                                                {
                                                    var result = repository.ShopBaskets.GetDataInDb(temporaryUser, page);
                                                    foreach (Item item in result)
                                                    {
                                                        Console.WriteLine("Категория товаров: " + item.Name + "\nЦена: " + item.Price);
                                                        Console.WriteLine("--------------------------------------------------------------------------");
                                                    }
                                                }
                                                catch (ArgumentOutOfRangeException excepton)
                                                {
                                                    Console.WriteLine("Ошибка, что продолжить просмотр листай страницу вперед");
                                                }
                                                Console.WriteLine("\n\n1. Следущая страница \n2. Предыдущая страница \n3. Оформить заказ \n0. Выйти");
                                                //Console.SetCursorPosition(30, 30);
                                                //Console.WriteLine("Страница " + page);
                                                string basketAction = Console.ReadLine();
                                                if (basketAction == "1")
                                                {
                                                    page++;
                                                }
                                                else if (basketAction == "2")
                                                {
                                                    page--;
                                                }
                                                else if (basketAction == "3")
                                                {
                                                    Console.WriteLine("Скоро появится :)");
                                                }
                                                else if (basketAction == "0")
                                                {
                                                    basketIsExit = true;
                                                }
                                            }
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
                            Console.Clear();
                            Console.WriteLine("Введите название товара для поиска");
                            string itemName = Console.ReadLine();
                            Connection conect = new Connection();
                            conect.ConnectToDb();
                            WholeRepository repository = new WholeRepository(conect.providerName, conect.configurationRoot.GetConnectionString("DebugConnectionString"));
                            bool isExit = false;
                            int page = 1;
                            while (!isExit)
                            {
                                Console.Clear();
                                try
                                {
                                    int i = 0;
                                    var result = repository.Items.Find(page, itemName);
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
                                Console.WriteLine("\n\n1. Следущая страница \n2. Предыдущая страница \n3. Просмотреть товар \n0. Выйти");
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
                                    items = repository.Items.Find(page, itemName);
                                    Item temporaryItem = new Item();
                                    try
                                    {
                                        temporaryItem = items.ElementAt(index);
                                        Console.Clear();
                                        Console.WriteLine("Наименование товара: " + temporaryItem.Name);
                                        Console.WriteLine("Описание товара: " + temporaryItem.Description);
                                        Console.WriteLine("Цена: " + temporaryItem.Price);

                                        var itemRatings = repository.Ratings.GetAll(temporaryItem);
                                        foreach (var itemRating in itemRatings)
                                        {
                                            Console.WriteLine("-------------------------------------------------");
                                            Console.WriteLine("Пользователь " + itemRating.UserName);
                                            Console.WriteLine("Рейтинг " + itemRating.Mark);
                                            Console.WriteLine("Написал: " + itemRating.Comentary);
                                            Console.WriteLine(itemRating.CreationDate);
                                            Console.WriteLine("-------------------------------------------------");
                                        }

                                        Console.WriteLine("\n\n 1. Добавить в корзину 2. Добавить отзыв 3. Вернуться назад");
                                        string menuAction = Console.ReadLine();
                                        if (temporaryUser.IsLogged)
                                        {
                                            switch (menuAction)
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
                                                        Rating rating = new Rating();
                                                        rating.UserName = temporaryUser.Login;
                                                        rating.ItemId = temporaryItem.Id;
                                                        Console.WriteLine("Оцените товар от 1 до 5");
                                                        string mark = Console.ReadLine();
                                                        int markToInt;
                                                        Int32.TryParse(mark, out markToInt);
                                                        rating.Mark = markToInt;
                                                        Console.WriteLine("Введите ваш коментарий");
                                                        string comment = Console.ReadLine();
                                                        rating.Comentary = comment;
                                                        rating.UserId = temporaryUser.Id;
                                                        repository.Ratings.Add(rating);
                                                    }
                                                    break;
                                                case "3":
                                                    {
                                                        return;
                                                    }
                                                    break;
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Чтобы добавлять товар в корзину и писать коментарии нужно войти в систему");
                                            Console.ReadKey();
                                        }
                                    }
                                    catch(ArgumentOutOfRangeException exception)
                                    {
                                        Console.WriteLine("Ошибка номер товара не существует");
                                    }
                                    
                                    
                                }
                                else if (action == "0")
                                {
                                    isExit = true;
                                }
                            }
                        }
                        
                        break;
                    case "4":
                        {
                            Console.Clear();
                            string comand;
                            Console.WriteLine("1. Просмотр категорий товаров \n2. Просмотр всего ассортимента товара");
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
                                Console.WriteLine("\n\n1. Следущая страница \n2. Предыдущая страница \n3. Просмотреть товар \n0. Выйти");
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
                                    try
                                    {
                                        temporaryItem = items.ElementAt(index);

                                        Console.Clear();
                                        Console.WriteLine("Наименование товара: " + temporaryItem.Name);
                                        Console.WriteLine("Описание товара: " + temporaryItem.Description);
                                        Console.WriteLine("Цена: " + temporaryItem.Price);

                                        var itemRatings = repository.Ratings.GetAll(temporaryItem);
                                        foreach (var itemRating in itemRatings)
                                        {
                                            Console.WriteLine("-------------------------------------------------");
                                            Console.WriteLine("Пользователь " + itemRating.UserName);
                                            Console.WriteLine("Рейтинг " + itemRating.Mark);
                                            Console.WriteLine("Написал: " + itemRating.Comentary);
                                            Console.WriteLine(itemRating.CreationDate);
                                            Console.WriteLine("-------------------------------------------------");
                                        }

                                        Console.WriteLine("\n\n 1. Добавить в корзину 2. Добавить отзыв 3. Вернуться назад");
                                        string menuAction = Console.ReadLine();
                                        if (temporaryUser.IsLogged)
                                        {
                                            switch (menuAction)
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
                                                        Rating rating = new Rating();
                                                        rating.UserName = temporaryUser.Login;
                                                        rating.ItemId = temporaryItem.Id;
                                                        Console.WriteLine("Оцените товар от 1 до 5");
                                                        string mark = Console.ReadLine();
                                                        int markToInt;
                                                        Int32.TryParse(mark, out markToInt);
                                                        rating.Mark = markToInt;
                                                        Console.WriteLine("Введите ваш коментарий");
                                                        string comment = Console.ReadLine();
                                                        rating.Comentary = comment;
                                                        rating.UserId = temporaryUser.Id;
                                                        repository.Ratings.Add(rating);
                                                    }
                                                    break;
                                                case "3":
                                                    {
                                                        Console.Clear();
                                                    }
                                                    break;
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Чтобы добавлять товар в корзину и писать коментарии нужно войти в систему");
                                            Console.ReadKey();
                                        }
                                    }
                                    catch(ArgumentOutOfRangeException exception)
                                    {
                                        Console.WriteLine("Ошибка номер товара не существует");
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