using Shop.Services.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Shop.Services
{
    public class SmsVerification : IVerification
    {
        public bool isVerificated { get; set; }
        public string VerificationCode { get; set; }

        public SmsVerification()
        {
            isVerificated = false;
        }
        /// <summary>
        /// Метод принимающий номер телефона абонента и отправляющий на него коде верификации
        /// </summary>
        /// <param name="phoneNumber">в качестве параметра передается действующий номер</param>
        public void VerificationCodeSender(string phoneNumber)
        {
            string AccountSid = "AC8342d8d4276f3b549f9f2ca00a2c0d0a";
            string AuthToken = "1b0da09f5ad619addd9a972e0f9cede3";
            string Message = string.Empty;
            string VerificationCode = string.Empty;

            this.VerificationCode = Path.GetRandomFileName().ToUpper().Remove(6); //генерация рандомного 6-ти значного кода верификации
            this.VerificationCode = this.VerificationCode.Replace(".", "");
            TwilioClient.Init(AccountSid, AuthToken);

            var to = new Twilio.Types.PhoneNumber(phoneNumber);
            var from = new Twilio.Types.PhoneNumber("+14842355664");
            var message = MessageResource.Create(
                to: to,
                from: from,
                body: this.VerificationCode);
        }
        /// <summary>
        /// метод проверяет код верификации отправленный на номер телефона. Если код введен верно дает разрешение на регистрацию аккаунта в БД
        /// </summary>
        /// <param name="verificationCode"></param>
        public void VerificationChecker(string verificationCode)
        {
            if(this.VerificationCode == verificationCode)
            {
                Console.WriteLine("Регистрация аккаунта успешно подтверждена");
                isVerificated = true;
            }
            else if(this.VerificationCode != verificationCode)
            {
                Console.WriteLine("Введен неверный код верификации");
            }
        }
    }
}
