﻿using System;

namespace LibraryWebAPI.Dtos.AccountDto
{
    public class Account_GetDto
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
    }
}
