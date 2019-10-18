using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Services.Abstract
{
    public interface IVerification
    {
        void VerificationCodeSender(string phoneNumber);
        void VerificationChecker(string verificationCode);
    }
}
