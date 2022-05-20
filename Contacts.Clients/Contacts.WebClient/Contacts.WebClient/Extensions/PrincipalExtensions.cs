using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Claims;
using System.Security.Principal;


namespace Contacts.WebClient.Extensions
{
    public static class PrincipalExtensions
    {       

        //
        // Summary:
        //     Gets the subject identifier.
        //
        // Parameters:
        //   principal:
        //     The principal.
        [DebuggerStepThrough]
        public static string GetSubjectId(this IPrincipal principal)
        {
            return principal?.Identity?.GetSubjectId() ?? "";
        }

        //
        // Summary:
        //     Gets the subject identifier.
        //
        // Parameters:
        //   identity:
        //     The identity.
        //
        // Exceptions:
        //   T:System.InvalidOperationException:
        //     sub claim is missing
        [DebuggerStepThrough]
        public static string GetSubjectId(this IIdentity identity)
        {
            return ((identity as ClaimsIdentity)?.FindFirst("sub") ?? throw new InvalidOperationException("sub claim is missing"))!.Value;
        }

        //
        // Summary:
        //     Gets the name.
        //
        // Parameters:
        //   principal:
        //     The principal.
        [DebuggerStepThrough]
        [Obsolete("This method will be removed in a future version. Use GetDisplayName instead.")]
        public static string GetName(this IPrincipal principal)
        {
            return principal?.Identity?.GetName() ?? "";
        }

        //
        // Summary:
        //     Gets the name.
        //
        // Parameters:
        //   principal:
        //     The principal.
        [DebuggerStepThrough]
        public static string GetDisplayName(this ClaimsPrincipal principal)
        {
            return principal?.Identity!.Name ?? "";
        }

        //
        // Summary:
        //     Gets the name.
        //
        // Parameters:
        //   identity:
        //     The identity.
        //
        // Exceptions:
        //   T:System.InvalidOperationException:
        //     name claim is missing
        [DebuggerStepThrough]
        [Obsolete("This method will be removed in a future version. Use GetDisplayName instead.")]
        public static string GetName(this IIdentity identity)
        {
            return ((identity as ClaimsIdentity)?.FindFirst("name") ?? throw new InvalidOperationException("name claim is missing"))!.Value;
        }


    }
}
