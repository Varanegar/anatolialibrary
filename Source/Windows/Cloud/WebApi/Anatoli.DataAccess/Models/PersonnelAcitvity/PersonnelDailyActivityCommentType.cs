﻿using Anatoli.DataAccess.Models.Route;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.DataAccess.Models.PersonnelAcitvity
{
    public class PersonnelDailyActivityCommentType : AnatoliBaseModel
    {
        public string Title { get; set; }
    }
}
