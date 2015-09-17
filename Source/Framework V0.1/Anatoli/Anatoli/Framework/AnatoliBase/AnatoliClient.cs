using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnatoliLibrary.Anatoliclient
{
    public class AnatoliClient
    {

        AnatoliWebClient _webClient;
        public AnatoliWebClient WebClient
        {
            get { return _webClient; }
        }
        AnatoliSQLite _sqlite;
        public AnatoliSQLite DbClient
        {
            get { return _sqlite; }
        }
        IFileIO _fileIO;
        public IFileIO FileIO
        {
            get { return _fileIO; }
        }

        private static AnatoliClient instance;

        public static AnatoliClient GetInstance(AnatoliWebClient webClient, AnatoliSQLite sqlite, IFileIO fileIO)
        {
            if(instance == null)
                instance = new AnatoliClient(webClient, sqlite, fileIO);

            return instance;

        }
        private AnatoliClient()
        {

        }
        private AnatoliClient(AnatoliWebClient webClient, AnatoliSQLite sqlite, IFileIO fileIO)
        {
            _webClient = webClient;
            _sqlite = sqlite;
            _fileIO = fileIO;
        }
    }
}
