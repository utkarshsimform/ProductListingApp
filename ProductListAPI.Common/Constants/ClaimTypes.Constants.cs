namespace ProtoTypeAPI.Common.Constants
{
    public static partial class Constants
    {
        public const string OBJECT_ID = "http://schemas.microsoft.com/identity/claims/objectidentifier";
        public const string CLAIM_TYPE_TENANTID = "http://schemas.microsoft.com/identity/claims/tenantid";
        public const string CLAIM_TYPE_SCOPE = "http://schemas.microsoft.com/identity/claims/scope";
        public const string CLAIM_TYPE_USERNAME = "name";
        public const string INSTANCE_REGEX = @"(?<=instance:).+?(?=\s|$)";
        public const string INSTANCE_PREFIX = "instance:";
        public const string INSTANCE_ALL = "all";

        public const string MSG_ACCESS_FORBIDDEN = "Access forbidden as no tenant or instance specified";

    }
}
