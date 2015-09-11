using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnatoliaLibrary.anatoliaclient
{
    public abstract class AnatoliaWebClient
    {
        // Sending Web requests using Xamarin RestSharp library
        public void SendPostRequest()
        {

        }
        public void SendGetRequest()
        {

        }
        public abstract bool IsOnline();
    }
}
