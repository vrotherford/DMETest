using Core;
using Data;
using Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DbUploader
{
    class UserData
    {
        public string gender { get; set; }
        public Name name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public Picture picture { get; set; }
        public Login login { get; set; }
        public Dob dob { get; set; }
    }

    public class Name
    {
        public string first { get; set; }
        public string last { get; set; }
    }

    public class Picture
    {
        public string large { get; set; }
        public string medium { get; set; }
        public string thumbnail { get; set; }
    }

    public class Login
    {
        public string password { get; set; }
        public Guid uuid { get; set; }
    }

    public class Dob
    {
        public DateTime date { get; set; }
    }

    public class BasicContext : DbContext
    {
        public BasicContext() : base("Default") { }
        public DbSet<User> Users { get; set; }

        private void FixEfProviderServicesProblem()
        {
            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            BasicContext basicContext = new BasicContext();
            var isExist = basicContext.Database.Exists();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://randomuser.me/api/?results=1000");
            request.Method = "Get";
            request.ContentType = "appication/json";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string myResponse = "";
            using (System.IO.StreamReader sr = new System.IO.StreamReader(response.GetResponseStream()))
            {
                myResponse = sr.ReadToEnd();
            }

            JObject usersResults = JObject.Parse(myResponse);
            IList<JToken> results = usersResults["results"].Children().ToList();
            IList<UserData> userDatas = new List<UserData>();
            foreach (JToken result in results)
            {
                UserData searchResult = result.ToObject<UserData>();
                User user = new User{ firstName = searchResult.name.first,
                    lastName = searchResult.name.last,
                    email = searchResult.email,
                    phone = searchResult.phone,
                    thumbnail = searchResult.picture.thumbnail,
                    largeSizePhoto = searchResult.picture.large,
                    password = searchResult.login.password,
                    birth = searchResult.dob.date,
                    id = searchResult.login.uuid
                };
                basicContext.Users.Add(user);
                userDatas.Add(searchResult);
            }
            basicContext.SaveChanges();

        }
    }
}
