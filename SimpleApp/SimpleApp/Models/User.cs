using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleApp.Models {
    public sealed class User {

        private static readonly User instance = new User();
        public string Username { get; set; }
        public string Name { get; set; }

        private User()
        {
        }
        public static User Instance
        {
            get
            {
                return instance;
            }
        }
    }
}
