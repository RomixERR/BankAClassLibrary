using System;


namespace HW12_6_BankA
{
    public class Departament: IComparable
    {
        public int ID { get; set; }
        public string nameOfDepartament { get; set; }
        public DataChangeAtributes dataChangeAtributes;

        public string NameOfDepartament { get { return nameOfDepartament; } set { nameOfDepartament = value; } }


        public Departament()
        {
 
        }

        //[JsonConstructor] ////////////////
        //public Departament(int Id, string nameOfDepartament, DataChangeAtributes dataChangeAtributes)
        //{
        //    ID = Id;
        //    NameOfDepartament = nameOfDepartament;
        //    this.dataChangeAtributes = dataChangeAtributes;
        //}


        public Departament(string NameOfDepartament, Employer employer)
        {
            if (employer.Permission.SetDepartamentsData != Permission.EDataMode.All)
            {
                throw new Exception("Нет привелегий");
            }
            dataChangeAtributes = DataChangeAtributes.NewChangeAtributes(employer);
            this.nameOfDepartament = NameOfDepartament;
            ID = LastIdMonitor.GenerateDepartamentsID();
        }
        
        public Departament(int Id, string nameOfDepartament)
        {
            ID = Id;
            NameOfDepartament = nameOfDepartament;
        }
        /// <summary>
        /// Проверка полноты данных
        /// </summary>
        /// <returns></returns>
        public (bool check, string errorMsg) Check()
        {
            string errorMsg = "";
            bool check = true;
            if (nameOfDepartament.Length<2) { check = false; errorMsg += "nameOfDepartament.Length"+" "; }
            return (check, errorMsg);
        }

        public override string ToString()
        {
            return nameOfDepartament;
        }

        public int CompareTo(object obj)
        {
            return nameOfDepartament.CompareTo(((Departament)obj).nameOfDepartament);
        }
    }
}
