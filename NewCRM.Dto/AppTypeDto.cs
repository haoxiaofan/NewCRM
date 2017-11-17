﻿using System;
using System.ComponentModel.DataAnnotations;

namespace NewCRM.Dto
{
    public sealed class AppTypeDto : BaseDto
    {
        [Required]
        public String Name { get; set; }
    }
}