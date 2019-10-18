using Shop.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Services.Abstract
{
    public interface IRegistrationAccount
    {
        void Registration(User user);
    }
}
