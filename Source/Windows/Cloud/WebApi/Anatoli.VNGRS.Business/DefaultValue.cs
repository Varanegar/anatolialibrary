
using TrackingMap.Service.BL;
using TrackingMap.Common.ViewModel;
using TrackingMap.Common.Enum;

namespace TrackingMap.Service
{
   	public class DefaultValue {

        private const string USERDATA = "/User_Data/";
        private const string IMAGESPATH = USERDATA + "Images/";
        private const string LOGPATH = "/Log/";

        public const string ERROR_TAG = "Error";

        public const string WEDDING_GROUP = "WEDDING";
        
   	    public const int WIDTH = 200;
        public const int HEIGHT = 200;

        public const int DATA_PAGE_SIZE = 2;

        public const int NEXTPAGECODE = -1000;


   	    private static ConfigView _ConfigView { get; set; }

        #region  path
        private static string  _ServerPath { get; set; }

        public static void SetServerPath(string path)
        {
            _ServerPath = path;
        }

        public static string GetUserDataPath()
        {
            return _ServerPath + USERDATA;
        }

        public static string GetAppDataPath()
        {
            return _ServerPath + "/App_Data/";
        }

        public static string GetImagePath()
        {
            return _ServerPath + IMAGESPATH;
        }

        public static string GetImageFolder()
        {
            return IMAGESPATH;
        }
        #endregion
     
        #region log
   
        public static void SetLogLevel(ELogLevel level)
        {
            _ConfigView.LogLevel = level;
        }

        public static string GetLogPath()
        {
            return GetUserDataPath() + LOGPATH;
        }

        public static ELogLevel GetLogLevel()
        {
            return _ConfigView.LogLevel;
        }
        #endregion

        public static string GetConfigFile()
        {
            return GetAppDataPath() + "/config.xml";
        }

        public static void SetConfig(ConfigView config)
        {
            _ConfigView = config;
        }
        public static string GetContentPath()
        {
            return _ServerPath + "/Content";
        }
        
        
    }


}
