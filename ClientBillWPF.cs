using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HW12_6_BankA
{
    public class ClientBillWPF:ClientBill
    {
        public Visibility IsOpenDepositVisible { get; set; }
        public Visibility IsCloseDepositVisible { get; set; }
        public string DepositSum { get; set; }
        public string DepositID { get; set; }

        public Visibility IsOpenCreditVisible { get; set; }
        public Visibility IsCloseCreditVisible { get; set; }
        public string CreditSum { get; set; }
        public string CreditID { get; set; }

        private const string noBillString = "NO BILL";

        public ClientBillWPF(int clientID) : base(clientID)
        {
        }

        public ClientBillWPF() : base()
        {
        }

        /// <summary>
        /// Обновление элементов формы
        /// </summary>
        public void Refresh(Repository rep)
        {
            rep.SaveCurrentClient(true);
            BillCredit billCredit = GetBillCredit();
            if (billCredit == null)
            {
                IsOpenCreditVisible = Visibility.Hidden;
                IsCloseCreditVisible =Visibility.Visible;
                CreditSum = noBillString;
                CreditID = noBillString;
            }
            else
            {
                IsOpenCreditVisible = Visibility.Visible;
                IsCloseCreditVisible = Visibility.Hidden;
                CreditSum = billCredit.Money.ToString();
                CreditID = billCredit.ID;
            }

            BillDeposit billDeposit = GetBillDeposit();
            if (billDeposit == null)
            {
                IsOpenDepositVisible = Visibility.Hidden;
                IsCloseDepositVisible = Visibility.Visible;
                DepositSum = noBillString;
                DepositID = noBillString;
            }
            else
            {
                IsOpenDepositVisible = Visibility.Visible;
                IsCloseDepositVisible = Visibility.Hidden;
                DepositSum = billDeposit.Money.ToString();
                DepositID = billDeposit.ID;
            }
            rep.OnPropertyChanged("CurrentClient");
        }
    }
}
