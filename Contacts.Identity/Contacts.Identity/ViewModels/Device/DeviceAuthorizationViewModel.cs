// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Contacts.Identity.ViewModels.Consent;

namespace Contacts.Identity.ViewModels.Device
{
    public class DeviceAuthorizationViewModel : ConsentViewModel
    {
        public string UserCode { get; set; } = string.Empty;
        public bool ConfirmUserCode { get; set; }
    }
}