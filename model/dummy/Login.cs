using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RagaliaHotelMS.model.dummy
{
    class Login
    {

        private List<User> userList = new List<User>();

        public Login()
        {
            User saad = new User("Saad", "1234");
            User faisal = new User("Faisal", "1234");

            userList.Add(saad);
            userList.Add(faisal);
        }

        /// <summary>
        /// This method is used to authen
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool authenticate( string name, string password )
        {

            if (isUser(new User(name, password)))
                return true;

            return false;
        }


        private Boolean isUser(User user) {
            foreach (User temp in userList)
            {

                if (user.Equals(temp))
                    return true;
            }
            return false;
        }

    }

    class User 
    {

        private string userName;
        private string password;

        public User() { }

        public User(string userName, string password) {
            this.userName = userName;
            this.password = password;
        }

        public string UserName
        {
            set { userName = value; }
            get { return userName; }
        }

        public string Password
        {
            set { password = value; }
            get { return password; }
        }

        public bool Equals(User user)
        {
            if (this.userName.Equals(user.UserName)
                && this.password.Equals(user.Password))
                return true;
            return false;
        }

    }

}
