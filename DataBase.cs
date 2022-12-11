
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeUsersLite;

namespace HW12_6_BankA
{
    public class DataBase
    {
        public List<Client> clients;
        public List<Departament> departaments;
        private FakeUser fu;
        private Random r = new Random();
        /// <summary>
        /// Конструктор по умолчанию. Применяется, если база данных пустая и надо создать БД по умолчанию
        /// </summary>
        /// <param name="AddFakeUsersForTest">Добавить фейковых пользователей и департаменты для теста? Укажите количество или 0</param>
        public DataBase(int AddFakeUsersForTest=0, int AddFakeDepartForTest=0)
        {
            fu = new FakeUser();
            clients = new List<Client>();
            departaments = new List<Departament>();

            if (AddFakeUsersForTest > 0)
            {
                AddFakeDeps(AddFakeDepartForTest);
                AddFakeClients(AddFakeUsersForTest);
            }else if (AddFakeDepartForTest > 0)
            {
                AddFakeDeps(AddFakeDepartForTest);
            }


        }

        private void AddFakeClients(int amount)
        {
            Permission permission = new Permission(Permission.EDataMode.All, Permission.EDataMode.All, Permission.EDataMode.All, Permission.EDataMode.All);
            Employer fakeEmployer = new Employer("fakeEmployer", "My Gode", permission);
            for (int i = 0; i < amount; i++)
            {
                clients.Add(GetFakeClient(fakeEmployer));
            }
        }
        private void AddFakeDeps(int amount)
        {
            Permission permission = new Permission(Permission.EDataMode.All, Permission.EDataMode.All, Permission.EDataMode.All, Permission.EDataMode.All);
            Employer fakeEmployer = new Employer("fakeEmployer", "My Gode", permission);
            for (int i = 0; i < amount; i++)
            {
                departaments.Add(GetFakeDepartament(fakeEmployer));
            }
        }
        private Client GetFakeClient(Employer employer)
        {
            return new Client(new FIO(fu.GetFName(), fu.GetLName(), fu.GetMName()), fu.GetPhone(), fu.GetPasport(), employer, departaments[r.Next(3)]);
        }

        private Departament GetFakeDepartament(Employer employer)
        {
            int ID = LastIdMonitor.DepartamentsIDCount;
            return new Departament($"Департамент {ID}", employer);
        }

        public void OldIDsClientsFix()
        {
            Client client;
            int count=0;
            for (int i = 0; i < clients.Count; i++)
            {
                client = clients[i];
                if (client.ClientBill.GetBillCredit()?.ClientID == 0)  {
                    client.ClientBill.GetBillCredit().ClientID = client.ID;
                    count++;
                }
                if (client.ClientBill.GetBillDeposit()?.ClientID == 0) {
                    client.ClientBill.GetBillDeposit().ClientID = client.ID;
                    count++;
                }
            }
            LoggerHub.Log(this, "OldIDsClientsFix() Выполненно. Кол-во замен ID - " + count,LoggerHub.LogEventType.dontDisplayOnForm);

        }
    }
}
