using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW12_6_BankA
{
    /// <summary>
    /// У разных пользователей разная полнота доступа к базе данных
    /// </summary>
    public class Permission 
    {
        public bool FNameEnable { get; private set; }
        public bool LNameEnable { get; private set; }
        public bool MNameEnable { get; private set; }
        public bool PhoneNumberEnable { get; private set; }
        public bool PasportNumberEnable { get; private set; }
        public bool AddNewClientEnable { get; private set; }
        public bool DeleteClientEnable { get; private set; }
        public bool BillsClientEnable { get; private set; }

        public EDataMode GetDepartamentsData { get; private set; }
        public EDataMode SetDepartamentsData { get; private set; }
        public EDataMode GetClientsData { get; private set; }
        public EDataMode SetClientsData { get; private set; }

        public  Permission( EDataMode GetDepartamentsData,
                            EDataMode SetDepartamentsData,
                            EDataMode GetClientsData,
                            EDataMode SetClientsData)
        {
            this.GetDepartamentsData = GetDepartamentsData;
            this.SetDepartamentsData = SetDepartamentsData;
            this.GetClientsData = GetClientsData;
            this.SetClientsData = SetClientsData;
            SetEnableParam();
        }
        public enum EDataMode 
        {
            No,
            All,
            AllExclusivePasportNum,
            OnlyPhoneNumber
        }
        private void SetEnableParam()
        {
            if(SetClientsData == EDataMode.OnlyPhoneNumber)
            {
                PhoneNumberEnable = true;
            }
            else if(SetClientsData == EDataMode.All)
            {
                FNameEnable = true;
                LNameEnable = true;
                MNameEnable = true;
                PhoneNumberEnable = true;
                PasportNumberEnable = true;
                AddNewClientEnable = true;
                DeleteClientEnable = true;
                BillsClientEnable = true;
            }
        }

    }
}
