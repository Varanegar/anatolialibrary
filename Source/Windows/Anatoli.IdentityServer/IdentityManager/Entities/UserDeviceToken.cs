using System;
using IdentityServer3.EntityFramework.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Anatoli.IdentityServer.Entities
{
    public class UserDeviceToken
    {
        public Guid Id
        {
            get;
            set;
        }
        public string AppToken
        {
            get;
            set;
        }
        public string Platform
        {
            get;
            set;
        }
        public bool IsActive
        {
            get;
            set;
        }

        [ForeignKey("User")]
        public string UserId
        {
            get;
            set;
        }
        public virtual User User
        {
            get;
            set;
        }

        [ForeignKey("Client")]
        public int ClientId
        {
            get;
            set;
        }
        public virtual Client Client { get; set; }
    }
}