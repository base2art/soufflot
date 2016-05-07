namespace Base2art.Soufflot.Http.Owin
{
    using Base2art.Collections;

    public class HttpUser : IHttpUser
    {
        private readonly string userName;

        private readonly IReadOnlyArrayList<string> groupNames;

        public HttpUser(string userName)
            : this(userName, new string[0])
        {
        }

        public HttpUser(string userName, string[] groupNames)
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
            get { return !string.IsNullOrWhiteSpace(this.userName); }
        }

        public string UserName
        {
            get { return this.userName; }
        }

        public IReadOnlyArrayList<string> GroupNames
        {
            get { return this.groupNames; }
        }
    }
}
