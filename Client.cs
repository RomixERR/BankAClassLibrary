using System;
using System.Collections.Generic;




namespace HW12_6_BankA
{
    public class Client : IEquatable<Client>
    {
        public int ID { get; set; }
        private FIO fio;
        private string phoneNum;
        private string pasportNum;
        private Departament departament;
        public DataChangeAtributes dataChangeAtributes;
        private ClientBillWPF clientBill;

        public ClientBillWPF ClientBill { get { return clientBill; } set { clientBill = value; } }
        public FIO Fio { get { return fio; } set { fio = value; } }
        public string PhoneNum { get { return phoneNum; } set { phoneNum = value; } }
        public string PasportNum { get { return pasportNum; } set { pasportNum = value; } }
        public Departament Departament { get { return departament; } set { departament = value;} }

        public Client(FIO name, string phoneNum, string pasportNum, Employer employer, Departament departament)
        {
            if (employer.Permission.SetClientsData != Permission.EDataMode.All)
            {
                throw new Exception("Нет привелегий");
            }
            
            this.fio = name;
            this.phoneNum = phoneNum;
            this.pasportNum = pasportNum;
            this.Departament = departament;
            dataChangeAtributes = DataChangeAtributes.NewChangeAtributes(employer);
            ID = LastIdMonitor.GenerateClientID();
            clientBill = new ClientBillWPF(ID);
        }

        /// <summary>
        /// Пустой клиент !!!
        /// </summary>
        public Client() 
        {
            this.fio = new FIO("", "", "");
            this.phoneNum = "";
            this.pasportNum = "";
            this.ID = -1;
            clientBill = new ClientBillWPF();
            this.Departament = new Departament(-1,"");
        }

        public Client(FIO name, string phoneNum, string pasportNum)
        {
            this.fio = name;
            this.phoneNum = phoneNum;
            this.pasportNum = pasportNum;
            ID = LastIdMonitor.GenerateClientID();
            clientBill = new ClientBillWPF(ID);
        }
        /// <summary>
        /// Дубликат клиента (клон)
        /// </summary>
        /// <param name="ForCopy"></param>
        public Client(Client ForCopy)
        {
            this.fio = new FIO(ForCopy.Fio.FirstName, ForCopy.Fio.LastName, ForCopy.Fio.MiddleName);
            this.phoneNum =ForCopy.PhoneNum;
            this.pasportNum = ForCopy.PasportNum;
            this.ID = ForCopy.ID;
            this.Departament = new Departament( ForCopy.Departament.ID, ForCopy.Departament.NameOfDepartament);
            this.clientBill = ForCopy.ClientBill;
        }

        public override string ToString()
        {
            return $"{Fio} {PhoneNum} {PasportNum} {Departament} СЧЕТА: {clientBill.GetBillDeposit()} {clientBill.GetBillCredit()}";
        }

        public bool Equals(Client other)
        {
            if (other.ID == this.ID) return true; else return false;
        }
        public static Client FindClientByID(List<Client> list, int ID)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].ID == ID) return list[i];
            }
            return null;
        }
        /// <summary>
        /// Проверка полноты данных
        /// </summary>
        /// <returns></returns>
        public (bool check, string errorMsg) Check()
        {
            string errorMsg = "";
            bool check = true;
            if (PhoneNum.Length < 6) { check = false; errorMsg += "PhoneNum.Length" + " "; }
            if (PasportNum.Length < 9) { check = false; errorMsg += "PasportNum.Length" + " "; }
            var K = this.Fio.Check();
            if (!K.check) { check = false; errorMsg += K.errorMsg + " "; }
            K = this.Departament.Check();
            if (!K.check) { check = false; errorMsg += K.errorMsg + " "; }
            return (check, errorMsg);
        }
    }
}
