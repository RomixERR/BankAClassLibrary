using System.Linq;

namespace HW12_6_BankA
{
    /// <summary>
    /// Статичный класс для IDшников. Новый пользователь всегда должен создаваться с ID = Last().ID + 1, затем этот ID становится последним
    /// </summary>
    public static class LastIdMonitor
    {
        public static int ClientsIDCount;
        public static int DepartamentsIDCount;
        static LastIdMonitor() { }
        public static void SetCounts(DataBase db)
        {
            ClientsIDCount = db.clients.Last().ID;
            DepartamentsIDCount = db.departaments.Last().ID;
        }
        
        public static int GenerateClientID()
        {
            ClientsIDCount++;
            return ClientsIDCount;
        }
        public static int GenerateDepartamentsID()
        {
            DepartamentsIDCount++;
            return DepartamentsIDCount;
        }
    }
}
