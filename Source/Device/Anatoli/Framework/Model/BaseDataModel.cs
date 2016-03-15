﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.Framework.Model
{
    public abstract class BaseViewModel
    {
        public BaseViewModel()
        {

        }
        string _id;
        public string Id { get { return (_id != null) ? _id.ToUpper() : null; } set { _id = (value != null) ? value.ToUpper() : null; } }
        string _uniqueId;
        public string UniqueId { get { return (_uniqueId != null) ? _uniqueId.ToUpper() : null; } set { _uniqueId = (value != null) ? value.ToUpper() : null; } }
        public bool IsSaveRequired { get; set; }
        public bool ReadOnly { get { return false; } }
        public bool IsRemoved { get; set; }
        public bool IsValid { get { return (String.IsNullOrEmpty(message)) ? true : false; } private set { IsValid = value; } }
        public string message { get; set; }
        public string ModelStateString
        {
            get
            {
                string str = "";
                if (ModelState != null)
                {
                    foreach (var item in ModelState)
                    {
                        foreach (var item2 in item.Value)
                        {
                            str += item2 + Environment.NewLine;
                        }
                    }
                }
                return str;
            }
        }
        public Dictionary<string, string[]> ModelState { get; set; }

        public string PrivateOwnerId { get { return "3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"; } }
    }
}