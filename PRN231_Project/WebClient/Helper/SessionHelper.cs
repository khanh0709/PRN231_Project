using WebAPI.Business.DTO;
using Newtonsoft.Json;

namespace WebClient.Helper
{
    public class SessionHelper
    {
        public static void SetObjectAsJson(ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
        public static T GetObjectFromJson<T>(ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
        public static void DeleteSession(ISession session, string key)
        {
            session.Remove(key);
        }
        public static UserDTO GetUser(ISession session)
        {
            return GetObjectFromJson<UserDTO>(session, "user");
        }
        public static void SetUser(ISession session, object value)
        {
            session.SetString("user", JsonConvert.SerializeObject(value));
        }
    }
}
