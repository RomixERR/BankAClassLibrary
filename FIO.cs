using System;
namespace HW12_6_BankA
{   /// <summary>
    /// класс для хранения Фамилии Имени и Отчества
    /// </summary>
    public class FIO : IComparable
    {
        
        private string firstName;
        private string lastName;
        private string middleName;
        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get { return firstName; } set { firstName = value; } }
        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get { return lastName; } set { lastName = value;  } }
        /// <summary>
        /// Отчество
        /// </summary>
        public string MiddleName { get { return middleName; } set { middleName = value; } }
        public FIO(string FirstName, string LastName, string MiddleName)
        {
            this.firstName = FirstName;
            this.lastName = LastName;
            this.middleName = MiddleName;
        }

        public override string ToString()
        {
            //return $"Имя: {FirstName}, Фамилия: {LastName}, Отчество: {MiddleName}";
            return $"{LastName} {FirstName} {MiddleName}";
        }

        public int CompareTo(object obj)
        {
            FIO fio = (FIO)obj;
            return lastName.CompareTo(fio.lastName);
        }
        /// <summary>
        /// Проверка полноты данных
        /// Поля не должны быть Length < 2 символа
        /// </summary>
        /// <returns></returns>
        public (bool check, string errorMsg) Check()
        {
            string errorMsg = "";
            bool check = true;
            if (FirstName.Length < 2) { check = false; errorMsg += "FirstName.Length < 2" + " "; }
            if (LastName.Length < 2) { check = false; errorMsg += "LastName.Length < 2" + " "; }
            if (MiddleName.Length < 2) { check = false; errorMsg += "MiddleName.Length < 2" + " "; }
            return (check, errorMsg);
        }

    }
}
