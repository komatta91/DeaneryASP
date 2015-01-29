using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DeaneryASP.Models.Storage.Entity;

namespace DeaneryASP.Models.Storage.Impl
{
    public class LoginStorage : ILoginStorage
    {
        public static int ADMINISTRATOR { get { return 0; } }
        public static int STUDENT { get { return 1; } }
        public static int PROFESSOR { get { return 2; } }
        public static int NOT_RECOGNIZE { get { return -1; } }

        public int T_ADMINISTRATOR = 0;
        public int T_STUDENT = 1;
        public int T_PROFESSOR = 2;
        public int T_NOT_RECOGNIZE = -1;

        public int WhoLogins(string login, string password)
        {
            using (var db = new StorageContext())
            {
                var logedUser =
                    (from user in db.Users
                     join student in db.Students on user.UserID equals student.StudentID into us
                     from subUser in us.DefaultIfEmpty()
                     where user.UID == login
                     where user.PWD == password
                     select new { UserID = user.UserID, StudentID = (subUser == null ? 0 : subUser.StudentID), user.LastName }).Single();

                if (logedUser.StudentID != 0)
                {
                    return LoginStorage.STUDENT;
                }
                if (logedUser.LastName == "Administrator")
                {
                    return LoginStorage.ADMINISTRATOR;
                }
                return LoginStorage.PROFESSOR;
            }
        }
    }
}