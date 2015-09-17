using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AnatoliaLibrary.user
{
    public class MailAddress
    {
        string _address;
        public string Address
        {
            get { return _address; }
        }
        public MailAddress(string address)
        {
            try
            {
                bool validAddress = Regex.IsMatch(address,
                      @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                      @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                      RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
                _address = address;
            }
            catch (RegexMatchTimeoutException)
            {
                throw new InvalidEmailAddressException();
            }
        }
    }
    public class InvalidEmailAddressException : Exception
    {

    }
}
