using Aantoli.Common.Entity.App.Basket;
using Anatoli.Common.Gateway.Business.Basket;
using Anatoli.Framework.Busieness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.Cloud.Gateway.Business.Basket
{
    public class BasketInfoCloudHandler : BasketInfoHandler
    {
        #region Singleton
        private static BasketInfoCloudHandler Instance;
        public static BasketInfoCloudHandler GetInstance()
        {
            if (Instance == null)
                Instance = new BasketInfoCloudHandler();

            return Instance;
        }
        private BasketInfoCloudHandler()
            : base()
        {

        }
        #endregion

        public BasketInfoListEntity GetSampleData()
        {
            BasketInfoListEntity returnDataList = new BasketInfoListEntity();
            BasketInfoEntity returnData = new BasketInfoEntity();
            returnData.BasketName = "سبد روزانه";
            returnData.BasketComment = "سبد روزانه خودم";
            returnData.ChangeDate = DateTime.Now;
            returnData.RequestSourceId = Aantoli.Common.Entity.Common.SOURCE_TYPE.App;
            returnData.DeviceIMEI = "EMNBSIY2461M8S8";
            returnData.ID = 1;

            BasketItemInfoListEntity infoList = new BasketItemInfoListEntity();
            BasketItemInfoEntity info = new BasketItemInfoEntity();
            info.ID = 1;
            info.ProductId = Guid.NewGuid();
            info.Qty = 10;
            infoList.Add(info);
            info = new BasketItemInfoEntity();
            info.ID = 2;
            info.ProductId = Guid.NewGuid();
            info.Qty = 20;
            infoList.Add(info);
            returnData.ItemInfoList = infoList;
            BasketNoteInfoListEntity infoList2 = new BasketNoteInfoListEntity();
            BasketNoteInfoEntity info2 = new BasketNoteInfoEntity();
            info2.Note = "شامپو مخصوص خوذم";
            infoList2.Add(info2);
            returnData.NoteInfoList = infoList2;
            returnDataList.Add(returnData);

            infoList = new BasketItemInfoListEntity();
            info = new BasketItemInfoEntity();
            info.ID = 3;
            info.ProductId = Guid.NewGuid();
            info.Qty = 15;
            infoList.Add(info);
            info = new BasketItemInfoEntity();
            info.ID = 4;
            info.ProductId = Guid.NewGuid();
            info.Qty = 20;
            infoList.Add(info);
            returnData.ItemInfoList = infoList;
            infoList2 = new BasketNoteInfoListEntity();
            info2 = new BasketNoteInfoEntity();
            info2.Note = "نمک آشپزخانه";
            infoList2.Add(info2);
            returnData.NoteInfoList = infoList2;
            returnDataList.Add(returnData);

            return returnDataList;
        }

    }
}
