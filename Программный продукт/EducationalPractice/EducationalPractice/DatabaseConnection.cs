using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Controls;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Data;
using System.Diagnostics;
using System.Xml.Linq;
using System.Diagnostics.Contracts;
using System.Collections.Specialized;
using System.IO;

namespace EducationalPractice
{
    internal class DatabaseConnection
    {
        private const string url = "data source=.\\MSSQLSERVER2022;" +
            "Database=RepairService;" +
            "User Id=sa;" +
            "Password=123;" +
            "TrustServerCertificate=True;";
        

        // Возвращает активный ID в пользовательской таблице
        private ulong takeActiveId(string tableName)
        {
            ulong ActiveId = 1;
            using (SqlConnection conn = new SqlConnection(url))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = $@"
                    Use RepairService
                    SELECT *
                    FROM {tableName};
                    ";

                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ActiveId++;
                    }
                    dr.Close();
                }
                conn.Close();
            }
            return ActiveId;
        }

        // Возвращает информацию про статьи
        public Dictionary<string, string> returnApplicationInf()
        {
            Dictionary<string, string> applicationInf = new Dictionary<string, string>();
            using (SqlConnection conn = new SqlConnection(url))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = $@"
                    Use RepairService
                    SELECT [ID Заявки], [Дата добавления], Статус
                    FROM Заявка
                    WHERE [ID Заказчика] = {new CacheWork().ReturnUserId()};
                    ";

                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        applicationInf[("" + dr["ID Заявки"]).Trim()+" "+("" + dr["Дата добавления"]).Trim().Split(' ')[0]] = ("" + dr["Статус"]).Trim();
                        

                    }
                    dr.Close();
                }
                conn.Close();
            }
            
            
            return applicationInf;
        }


        // Проверка что данные не совпадают с другими аккаунтами
        private bool checkSimilarDataInDB(string[] userInputData, string role)
        {
            string  login = userInputData[2],
                    password = userInputData[3],
                    phone =userInputData[0];
            using (SqlConnection conn = new SqlConnection(url))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = $@"
                    Use RepairService
                    SELECT Логин, Пароль, Телефон
                    FROM {role};
                    ";

                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        if (login.Trim() == ("" + dr["Логин"]).Trim() ||
                            password.Trim() == ("" + dr["Пароль"]).Trim() ||
                            phone.Trim() == ("" + dr["Телефон"]).Trim())
                            return false;
                    }
                    dr.Close();
                }
                conn.Close();
            }
            return true;
        }

        // Регистрация нового аккаунта
        public bool addNewAccount(string[] userData, string userRole)
        {
            // Проверка на дубликаты
            if (!checkSimilarDataInDB(userData, userRole))
                return false;
            ulong id = takeActiveId(userRole);
            Console.WriteLine(userRole);
            using (SqlConnection connection = new SqlConnection(url))
            {

                connection.Open();
                SqlCommand commandToAddInformationFromTable = new SqlCommand($@"
                Use RepairService
                INSERT INTO {userRole}
                VALUES({id}, N'{userData[0]}', N'{userData[1]}', N'{userData[2]}', N'{userData[3]}', N'{userData[4]}')");
                commandToAddInformationFromTable.Connection = connection;
                commandToAddInformationFromTable.ExecuteNonQuery();
            }
            CacheWork cacheWork = new CacheWork();
            // Регистрация пользователя в КЕШ
            cacheWork.AddUserID(id);
            cacheWork.AddUserRole(userRole);
            return true;
        }

        // Авторизация пользователя
        public bool signInAccount(string[] userData, string userRole, string idName)
        {
            string login = userData[0],
                    password = userData[1];
            using (SqlConnection conn = new SqlConnection(url))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = $@"
                    Use RepairService
                    SELECT [{idName}], Логин, Пароль
                    FROM {userRole};
                    ";

                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        if (login.Trim() == ("" + dr["Логин"]).Trim() &&
                            password.Trim() == ("" + dr["Пароль"]).Trim())
                        {
                            CacheWork cacheWork = new CacheWork();
                            // Регистрация пользователя в КЕШ
                            cacheWork.AddUserID(Convert.ToUInt64(("" + dr[idName]).Trim()));
                            cacheWork.AddUserRole(userRole);
                            return true;
                        }
                            
                    }
                    dr.Close();
                }
                conn.Close();
            }
            
            return false;
        }


        // Возврат информации для аккаунта пользователя
        public string[] returnUserAccountInformation()
        {
            CacheWork cacheWork = new CacheWork();
            string id =  $"ID {cacheWork.ReturnUserRole()}а";
            string[] informationArray = new string[3];
            using (SqlConnection conn = new SqlConnection(url))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = $@"
                    Use RepairService
                    SELECT [{id}], ФИО, Телефон
                    FROM {cacheWork.ReturnUserRole()}
                    where [{id}] = {cacheWork.ReturnUserId()};
                    ";

                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        informationArray[0] = ("" + dr["ФИО"]).Trim();
                        informationArray[1] = ("" + dr["Телефон"]).Trim();
                        informationArray[2] = cacheWork.ReturnUserRole();

                    }
                    dr.Close();
                }
                conn.Close();
            }
            return informationArray;
        }


        // Регистрация заявки
        public bool RepairReg(Dictionary<string, string> information)
        {
            ulong id = takeActiveId("Заявка");
            foreach(var item in information)
            {
                Console.WriteLine( item.Key + " : " + item.Value);
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(url))
                {

                    connection.Open();
                    SqlCommand commandToAddInformationFromTable = new SqlCommand($@"
                Use RepairService
                INSERT INTO Заявка
                VALUES({id}, N'{information["Дата добавления"]}', N'{information["Описание"]}', N'{information["Статус"]}',
N'{information["Комментарий"]}', N'{information["ID Заказчика"]}', N'{information["ID Исполнителя"]}',
N'{information["Тип неисправности"]}', N'{information["Набор инструментов"]}')");
                    commandToAddInformationFromTable.Connection = connection;
                    commandToAddInformationFromTable.ExecuteNonQuery();
                }
                new Instruments().createInformationMessageBox(
                    $"The application has been registered, the application number is: {id}.\nDate: {information["Дата добавления"]}") ;
                return true;
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
            
            
        }
    }
}
