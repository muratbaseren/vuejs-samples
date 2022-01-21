namespace BlogVue.WebAPI.Common
{
    public static class StringExtensions
    {
        public static MongoDB.Bson.ObjectId ToObjectId(this string s)
        {
            return MongoDB.Bson.ObjectId.Parse(s);
        }
    }
}
