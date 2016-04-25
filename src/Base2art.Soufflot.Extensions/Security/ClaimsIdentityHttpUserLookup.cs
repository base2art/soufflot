
namespace Base2art.PlayN.Security
{
    using System.Linq;
    using System.Security.Principal;
    using Base2art.Collections;
    using Base2art.PlayN.Http;
    
    public class ClaimsIdentityHttpUserLookup : IHttpUserLookup
    {
        public IHttpUser FindUser(IPrincipal user)
        {
            var claimsPrincipal= (ClaimsPrincipal)user;
            
            var roles = claimsPrincipal.Identities
                .SelectMany(y => y.Claims
                            .Where(x => x.Type == y.RoleClaimType)
                            .Select(x => x.Value))
                .ToArray();
            
            return new ClaimsBasedHttpUser(user.Identity.Name, roles);
        }

        public class ClaimsBasedHttpUser : IHttpUser
        {
            private readonly string userName;

            private readonly IReadOnlyArrayList<string> groupNames;

            public ClaimsBasedHttpUser(string userName)
                : this(userName, new string[0])
            {
            }

            public ClaimsBasedHttpUser(string userName, string[] groupNames)
            {
                this.userName = userName;
                this.groupNames = groupNames.AsReadOnlyArrayList();
            }

            public static string NullUserName
            {
                get { return string.Empty; }
            }
            
            public bool IsAuthenticated
            {
                get
                {
                    return !string.IsNullOrWhiteSpace(this.userName);
                }
            }

            public string UserName
            {
                get
                {
                    return this.userName;
                }
            }

            public IReadOnlyArrayList<string> GroupNames
            {
                get
                {
                    return this.groupNames;
                }
            }
        }
    }
}
