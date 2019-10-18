
namespace Shop.Domain
{
    public class User : Entity
    {
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        public string VerificationCode { get; set; }

        // покупки, комментарии, рейтинги, и так далее
        //public User()
        //{
        //    PhoneNumber = string.Empty;
        //    Email = string.Empty;
        //    Address = string.Empty;
        //    Password = string.Empty;
        //    VerificationCode = string.Empty;
        //}
    }
}
