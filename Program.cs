using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DataBaseForColleagues
{
    class Program
    {
        static string PasswordAndLoginAdding(string Login, string Password)
        {
            string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=R:\c#\DataBaseForColleagues\DataBaseForColleagues\MainDB.mdf;Integrated Security=True";
            string SucessfulAdding = "Теперь вы можете авторизоваться";
            using (SqlConnection Connection = new SqlConnection(ConnectionString))
            {
                Connection.Open();
                string SQLLoginAndPasswordAdding = String.Format("INSERT INTO Users (Login, Password) VALUES ('{0}', '{1}') ", Login, Password);
                SqlCommand SQLCommandLoginAndPasswordAdding = new SqlCommand(SQLLoginAndPasswordAdding, Connection);
                int NumbersOfAddings = SQLCommandLoginAndPasswordAdding.ExecuteNonQuery();

                if (NumbersOfAddings == 1)
                {
                    Console.WriteLine("Вы успешно зарегистрировались");
                }
                Connection.Close();
            }
            return SucessfulAdding;
        }

        static string Test()
        {
            string a = "s";

            string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=R:\c#\DataBaseForColleagues\DataBaseForColleagues\MainDB.mdf;Integrated Security=True";

            using (SqlConnection Connection = new SqlConnection(ConnectionString))
            {
                Connection.Open();
                string SQLTEST = "SELECT Password FROM Users";
                SqlCommand SQLCOMMANDTEXT = new SqlCommand(SQLTEST, Connection);
                SqlDataReader READERSQLCOMMANDTEXT = SQLCOMMANDTEXT.ExecuteReader();
                while (READERSQLCOMMANDTEXT.Read())
                {
                    Console.WriteLine(READERSQLCOMMANDTEXT[0].ToString());
                }
                READERSQLCOMMANDTEXT.Close();
                Connection.Close();
            }
            return a;
        }

        static void Main(string[] args)
        {
            int AnswerInMenu = 0;

            try
            {
                Console.WriteLine("Здравствуйте, введите 1 - чтобы зарегистрироваться в мессенджере");
                AnswerInMenu = Convert.ToInt32(Console.ReadLine());
            }
            catch
            {
                Console.Clear();
                Console.WriteLine("Ой-ёй, что-то пошло не так, нам кажеться, что вы хотите зарегестроваться");
                AnswerInMenu = 1;
            }

            if (AnswerInMenu != 1)
            {
                Console.Clear();
                Console.WriteLine("Упссс-сс, что неверно. Нашему боту показалось, что вы хотели зарегестрироваться");
                AnswerInMenu = 1;
            }

            if (AnswerInMenu == 1)
            {
                Test();
                Console.WriteLine("Давайте не терять ни минуты, придумайте логин");
                string Login = Console.ReadLine();

                string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=R:\c#\DataBaseForColleagues\DataBaseForColleagues\MainDB.mdf;Integrated Security=True";
                bool ThatLoginUnique = false;

                using (SqlConnection Connection = new SqlConnection(ConnectionString))
                {
                    Connection.Open();
                    string SQLVerifyLogin = String.Format("SELECT Login FROM Users WHERE Login = '{0}'", Login);

                    SqlCommand CommandSQLVerifyLogin = new SqlCommand(SQLVerifyLogin, Connection);
                    SqlDataReader ReaderCommandSQLVerifyLogin = CommandSQLVerifyLogin.ExecuteReader();

                    if (ReaderCommandSQLVerifyLogin.Read())
                    {
                        do
                        {
                            Connection.Close();

                            Console.WriteLine("Ваш логин неуникален, попробуйте придумать что-нибудь пооригинальнее");
                            string UniqueLogin = Console.ReadLine();

                            string SQLVerifyUniqueLogin = String.Format("SELECT Login FROM Users WHERE Login = '{0}'", UniqueLogin);

                            SqlCommand CommandSQLVerifyUniqueLogin = new SqlCommand(SQLVerifyUniqueLogin, Connection);

                            Connection.Open();

                            SqlDataReader ReaderCommandSQLVerifyUniqueLogin = CommandSQLVerifyUniqueLogin.ExecuteReader();

                            if (!ReaderCommandSQLVerifyUniqueLogin.Read())
                            {
                                ThatLoginUnique = true;
                            }

                        }
                        while (ThatLoginUnique == false);
                    }
                    Connection.Close();
                }

                Console.WriteLine("Давайте не терять ни минуты, придумайте пароль");
                string Password = Console.ReadLine();

                if (Password.Length <= 7)
                {
                    bool PasswordIsAllowable = false;

                    do
                    {
                        Console.WriteLine("Ваш пароль ненадёжен, придумайте что-нибудь посложнее, минимальная длинна пароля - 8 символов");
                        Password = Console.ReadLine();

                        if (Password.Length > 7)
                        {
                            PasswordIsAllowable = true;
                        }
                    } while (PasswordIsAllowable == false);
                }

                Console.Clear();
                Console.WriteLine("Да-да, вот так, но всё-таки, введите пароль заново");
                string PasswordAgain = Console.ReadLine();

                if (Password != PasswordAgain)
                {
                    bool PasswordsAreSame = false;

                    do
                    {
                        Console.WriteLine("Пароли не совпадают, введите пароль ещё раз");
                        Password = Console.ReadLine();

                        Console.Clear();
                        Console.WriteLine("Хорошо-хорошо, так держать, введите пароль повторно");
                        PasswordAgain = Console.ReadLine();

                        if (Password == PasswordAgain)
                        {
                            PasswordsAreSame = true;
                        }

                    } while (PasswordsAreSame == false);
                }

                Console.Clear();
                Console.WriteLine(PasswordAndLoginAdding(Login, Password));
            }
            Console.ReadKey();
        }
    }
}
