using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EducationalPractice.Properties;

// Класс для работы с КЕШем программы
namespace EducationalPractice
{
    internal class CacheWork
    {
        public void AddUserID(ulong userID)
        {
            Settings.Default["userId"] = userID;
            Settings.Default.Save();
        }
        // Возвращение Активного ID пользователя
        public ulong ReturnUserId()
        {
            if (!(Settings.Default["userId"].ToString() == ""))
                return Convert.ToUInt64(Settings.Default["userId"]);
            new Instruments().createInformationMessageBox("Сheck if you have an account!");
            return 0;


        }


        public void AddUserRole(string userRole)
        {
            Settings.Default["userRole"] = userRole;
            Settings.Default.Save();
        }

        // Возвращение роли пользователя
        public string ReturnUserRole() 
        {
            if (!(Settings.Default["userRole"].ToString() == ""))
                return Settings.Default["userRole"].ToString();
            new Instruments().createInformationMessageBox("Сheck if you have an account!");
            return "null";
            
            
        }


        // Добавление оборудования
        public void AddNewEquipment(string equipment)
        {
            Settings.Default["Equipment"] = equipment;
            Settings.Default.Save();
        }

        public string ReturnEquipment() => Settings.Default["Equipment"].ToString();

        public void AddNewFault(string fault)
        {
            Settings.Default["Fault"] = fault;
            Settings.Default.Save();
        }

        public string ReturnFault() => Settings.Default["Fault"].ToString();


        // Запись описания проблемы
        public  void AddNewProblemDiscription(string problemD)
        {
            Settings.Default["ProblemD"] = problemD;
            Settings.Default.Save();
        }

        public string ReturnProblemD() => Settings.Default["ProblemD"].ToString();


        public void AddNewExecutorId(string executorId)
        {
            Settings.Default["ExecutorId"] = executorId;
            Settings.Default.Save();
        }

        public string ReturnExecutorId() => Settings.Default["ExecutorId"].ToString();
    }
}
