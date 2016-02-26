using System;
using System.Linq;
using System.Collections.Generic;

namespace Anatoli.ViewModels.Order
{
    public class PurchaseOrderStatusHistoryViewModel : BaseViewModel
    {
        private string actionTimeString = "";
        public static string PreInvoice = "5C0D43FC-6822-4D39-AB40-363B885BE464";
        public static string WaitForDelivery = "EA5961AB-792A-4D20-8A52-5501F01F034A";
        public static string Cleared = "D12DEEE1-DA5B-44F6-937A-7B282789908F";
        public static string Delivered = "3AA22CED-A45E-4E58-B992-A4B1F838B19B";
        public static string Request = "A591658A-E46B-440D-9ADB-E3E5B01B7489";
        public static string InQueue = "A0418ABD-941C-4826-8063-E82B4A5D48FE";
        public Guid StatusId { get; set; }
        public DateTime ActionDate { get; set; }
        public string ActionPDate { get; set; }
        public TimeSpan ActionTime { get; set; }
        public string ActionTimeString
        {
            get { return actionTimeString; }
            set
            {
                actionTimeString = value;
                try
                {
                    if (value != null)
                        ActionTime = TimeSpan.Parse(value);
                }
                catch (Exception)
                {

                }
            }
        }
        public string Comment { get; set; }
        public Guid PurchaseOrderId { get; set; }
    }
}
