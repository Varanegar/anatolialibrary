using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnatoliaLibrary.anatoliaclient
{
    public class AnatoliaClient
    {
        AnatoliaWebClient _webClient;
        public AnatoliaWebClient WebClient
        {
            get { return _webClient; }
        }
        AnatoliaSQLite _sqlite;
        public AnatoliaSQLite DbClient
        {
            get { return _sqlite; }
        }
        IFileIO _fileIO;
        public IFileIO FileIO
        {
            get { return _fileIO; }
        }
        public AnatoliaClient(AnatoliaWebClient webClient, AnatoliaSQLite sqlite, IFileIO fileIO)
        {
            _webClient = webClient;
            _sqlite = sqlite;
            _fileIO = fileIO;
        }
    }
}
