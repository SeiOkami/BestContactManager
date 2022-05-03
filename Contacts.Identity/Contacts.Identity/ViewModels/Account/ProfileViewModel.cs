// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System.ComponentModel.DataAnnotations;

namespace Contacts.Identity.ViewModels.Account
{
    public class ProfileViewModel
    {
        public string UserName { get; set; } = string.Empty;
        
        public string Email { get; set; } = string.Empty;

        public bool ReadOnly { get; set; } = true;
    }
}