﻿using SWLOR.Game.Server.Service.Legacy;

// ReSharper disable once CheckNamespace
namespace NWN.Scripts
{
#pragma warning disable IDE1006 // Naming Styles
    public class dialog_appears11
#pragma warning restore IDE1006 // Naming Styles
    {
        // ReSharper disable once UnusedMember.Local
        public static int Main()
        {
            return DialogService.OnAppearsWhen(2, 11) ? 1 : 0;
        }
    }
}