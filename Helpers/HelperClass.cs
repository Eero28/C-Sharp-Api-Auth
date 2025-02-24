using System.Text.Json;
namespace UserAuth.Helpers
{
    public static class HelperFunc
    {
        public static string ToJson(object obj)
        {
            return JsonSerializer.Serialize(obj, new JsonSerializerOptions
            {
                WriteIndented = true 
            });
        }

        public static string PasswordHash(string password)
        {
           
            return BCrypt.Net.BCrypt.HashPassword(password);
        }


        public static bool PasswordVerify(string enteredPassword, string userPassword)
        {
            return BCrypt.Net.BCrypt.Verify(enteredPassword, userPassword);
        }
    }
}
