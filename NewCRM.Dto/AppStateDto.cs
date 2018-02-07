﻿using System;
using System.ComponentModel.DataAnnotations;

namespace NewCRM.Dto
{
    public sealed class AppStateDto : BaseDto
    {
        public String Name { get; set; }

        public String Type { get; set; }
    }
}
