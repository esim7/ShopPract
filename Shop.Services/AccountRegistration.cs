using Shop.DataAccess;
using Shop.Domain;
using Shop.Services.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Shop.Services
{
    public class AccountRegistration : IRegistrationAccount
    {
        public SmsVerification Verification { get; set; }
        public WholeRepository Repository { get; set; }
        public Connection Connect { get; set; }
         
        public AccountRegistration()
        {
            Verification = new SmsVerification();
            Connect = new Connection();
            Connect.ConnectToDb();
            Repository = new WholeRepository(Connect.providerName, Connect.configurationRoot.GetConnectionString("DebugConnectionString"));
        }
        public void Registration(User user)
        {
            Console.WriteLine("Введите логин");
            user.Login = Console.ReadLine();
            Console.WriteLine("Введите Ваш номер телефона в формате +7**********");
            user.PhoneNumber = Console.ReadLine();
            Console.WriteLine("Введите пароль");
            user.Password = Console.ReadLine();
            Console.WriteLine("Введите email");
            user.Email = Console.ReadLine();
            Console.WriteLine("Введите адресс");
            user.Address = Console.ReadLine();

            Verification.VerificationCodeSender(user.PhoneNumber);
            Console.WriteLine("Введите верификационный код который был отправлен на ваш номер телефона");
            string verificationCode = Console.ReadLine();
            Verification.VerificationChecker(verificationCode);

            if(Verification.isVerificated)
            {
                user.VerificationCode = Verification.VerificationCode;
                Repository.Users.Add(user);
                Repository.Dispose();
            }
        }
    }
}
