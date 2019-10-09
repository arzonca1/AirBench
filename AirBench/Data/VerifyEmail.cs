using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace AirBench.Data
{
    public class VerifyEmail
    {
        public string Email { get; set; }
        public VerifyEmail(string email)
        {
            Email = email;
        }
        public bool IsValid()
        {
            try
            {
                MailAddress m = new MailAddress(Email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}