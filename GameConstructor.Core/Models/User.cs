using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GameConstructor.Core.Models
{
    public class User
    {
        public string Login { get; set; }
        public int Id { get; set; }
        public string Password { get; set; }
        [JsonIgnore]
        public List<Game> Games { get; set; }
        public static string GetHash(string password)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(
            password));
            return Convert.ToBase64String(hash);
        }
    }
}
