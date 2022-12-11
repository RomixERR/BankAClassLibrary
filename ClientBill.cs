using System;
using System.Collections.Generic;
using System.Linq;


namespace HW12_6_BankA
{
    public abstract class ClientBill
    {
       
        public List<Bill> bills { get; set; }
        
        //public int clientID { get; set; }

        public ClientBill()
        {
            bills = new List<Bill>();
        }

        public ClientBill(int clientID)
        {
            bills = new List<Bill>();
            //this.clientID = clientID;
        }

        public bool OpenBill(Type typeBill, int clientID)
        {
            foreach (var bill in bills)
            {
                if (bill == null) continue;
                if (bill.GetType() == typeBill) return false; //если вдруг такой счёт уже есть то нельзя открыть такой-же
            }
            if (typeBill == typeof(BillDeposit))
            {
                bills.Add(new BillDeposit(clientID));
                LoggerHub.Log(
                    this,
                    $"Открытие {bills.Last<Bill>().GetTypeBillString()}, {bills.Last<Bill>().ID} для клиента ID:{bills.Last<Bill>().ClientID}",
                    LoggerHub.LogEventType.DisplayOnForm);
                return true;
            }
            else if (typeBill == typeof(BillCredit))
            {
                bills.Add(new BillCredit(clientID));
                LoggerHub.Log(
                    this,
                    $"Открытие {bills.Last<Bill>().GetTypeBillString()}, {bills.Last<Bill>().ID} для клиента ID:{bills.Last<Bill>().ClientID}",
                    LoggerHub.LogEventType.DisplayOnForm);
                return true;
            }
            else return false;
        }

        public bool CloseBill(Bill bill, int clientID)
        {
            if (bill.Money == 0)
            {
                bills.Remove(bill);
                LoggerHub.Log(
                    this,
                    $"Закрытие {bill.GetTypeBillString()}, {bill.ID} для клиента ID:{bill.ClientID}",
                    LoggerHub.LogEventType.DisplayOnForm);
                return true;
            }
            else return false;
        }

        public T GetBillByType<T>()
            where T:Bill
        {
            if (typeof(T) == typeof(BillDeposit))
            {
                return (T)(Bill)GetBillDeposit();
            } else if (typeof(T) == typeof(BillCredit))
            {
                return (T)(Bill)GetBillCredit();
            } else
            {
                throw new Exception("Тип для метода GetBillByType не корректен. Поддерживается BillDeposit или BillCredit");
            }
        }



        public BillDeposit GetBillDeposit()
        {
            foreach (var item in bills)
            {
                if (item == null) continue;
                if (item.GetType() == typeof(BillDeposit)) return (BillDeposit)item;
            }
            return null;
        }

        public BillCredit GetBillCredit()
        {
            foreach (var item in bills)
            {
                if (item == null) continue;
                if (item.GetType() == typeof(BillCredit)) return (BillCredit)item;
            }
            return null;
        }



        public override string ToString()
        {
            string S = "BILLS: \n";
            foreach (var item in bills)
            {
                S += $"{item.GetType().Name} \t{item}\n";
            }

            return S;
        }



        public abstract class Bill
        {
            public decimal Money { get; set; }
            public string ID { get; set; }
            public int nativeID { get; set; }
            public int ClientID { get; set; }

            protected Bill(int clientID, int nativeID)
            {
                this.ID = $"BILL{nativeID}#{clientID}";
                this.nativeID = nativeID;
                this.ClientID = clientID;
            }
            /// <summary>
            /// Оператор сумма счётов, выдаёт сумму денежных средств
            /// </summary>
            public static decimal operator +(Bill A, Bill B)
            {
                if ((A == null) && (B == null)) return 0;
                if (A == null) return B.Money;
                if (B == null) return A.Money;
                return A.Money + B.Money;
            }

            public static decimal operator +(Bill A, decimal B)
            {
                if (A == null) return B;
                return A.Money + B;
            }

            public static decimal operator +(decimal A, Bill B)
            {
                if (B == null) return A;
                return A + B.Money;
            }

            public override string ToString()
            {
                return $"# ID = {ID,10}; {GetTypeBillString()} \tMoney = {Money,10}";
            }
            /// <summary>
            /// Положить (перевести) на этот счет деньги
            /// </summary>
            /// <param name="billFromTake"></param>
            /// <param name="amount"></param>
            /// <returns></returns>
            public bool Put(Bill billFromTake, decimal amount)
            {
                if (billFromTake == null)
                {
                    LoggerHub.Log(this, $"Счёт для снятия {billFromTake.ID} не найден", LoggerHub.LogEventType.dontDisplayOnForm);
                    return false;
                }
                if (amount <= billFromTake.Money)
                {
                    billFromTake.Money -= amount;
                    Money += amount;
                    LoggerHub.Log(
                        this,
                        $"На счёт переведена сумма {amount}. Счёт:{ID} для клиента ID:{ClientID}, со счёта {billFromTake.ID}, ID клиента:{billFromTake.ClientID}, PUT(Bill billFromTake, decimal amount)",
                        LoggerHub.LogEventType.DisplayOnForm);
                    return true;
                }
                else return false;
            }
            /// <summary>
            /// Положить на этот счет деньги
            /// </summary>
            /// <param name="amount"></param>
            /// <returns></returns>
            public bool Put(decimal amount)
            {
                Money += amount;
                LoggerHub.Log(
                    this,
                    $"На счёт положенна сумма {amount}. Счёт:{ID} для клиента ID:{ClientID} PUT(decimal amount)",
                    LoggerHub.LogEventType.DisplayOnForm);
                return true;
            }
            /// <summary>
            /// Снять с этого счёта деньги (перевести на другой счёт)
            /// </summary>
            /// <param name="billForPut"></param>
            /// <param name="amount"></param>
            /// <returns></returns>
            public abstract bool Take(Bill billForPut, decimal amount);

            /// <summary>
            /// Снять с этого счёта деньги
            /// </summary>
            /// <param name="amount"></param>
            /// <returns></returns>
            public abstract bool Take(decimal amount);

            public Type GetTypeBill()
            {
                if (this.nativeID == 1) { return typeof(BillDeposit); }
                else if (this.nativeID == 2) { return typeof(BillCredit); }
                else return null;
            }
            public string GetTypeBillString()
            {
                //return GetTypeBill().Name;
                if (this.nativeID == 1) { return "депозитный счёт"; }
                else if (this.nativeID == 2) { return "кредитный счёт"; }
                else return $"!!! счёт неизвестного типа, nativeID={this.nativeID}";
            }


        }

        public class BillDeposit : Bill
        {
            public BillDeposit(int clientID) : base(clientID, 1) { }
            /// <summary>
            /// Снять с этого счёта деньги (перевести на другой счёт)
            /// </summary>
            /// <param name="billForPut"></param>
            /// <param name="amount"></param>
            /// <returns></returns>
            public override bool Take(Bill billForPut, decimal amount)
            {
                if (billForPut == null)
                {
                    LoggerHub.Log(this, $"Счёт для передачи {billForPut.ID} не найден", LoggerHub.LogEventType.dontDisplayOnForm);
                    return false;
                }
                if (Money >= amount)
                {
                    Money -= amount;
                    billForPut.Money += amount;
                    LoggerHub.Log(
                        this,
                        $"Со счёта переведена сумма {amount}. Счёт:{ID} клиента ID:{ClientID}, на счёт {billForPut.ID}, клиенту ID:{billForPut.ClientID}, Take(Bill billForPut, decimal amount), BillDeposit",
                        LoggerHub.LogEventType.DisplayOnForm);
                    return true;
                }
                else return false;
            }
            /// <summary>
            /// Снять с этого счёта деньги
            /// </summary>
            /// <param name="amount"></param>
            /// <returns></returns>
            public override bool Take(decimal amount)
            {
                if (Money >= amount)
                {
                    Money -= amount;
                    LoggerHub.Log(
                        this,
                        $"Со счёта снята сумма {amount}. Счёт:{ID} клиента ID:{ClientID}, Take(decimal amount), BillDeposit",
                        LoggerHub.LogEventType.DisplayOnForm);
                    return true;
                }
                else return false;
            }
        }

        public class BillCredit : Bill
        {
            public BillCredit(int clientID) : base(clientID, 2) { }
            /// <summary>
            /// Снять с этого счёта деньги (перевести на другой счёт)
            /// </summary>
            /// <param name="billForPut"></param>
            /// <param name="amount"></param>
            /// <returns></returns>
            public override bool Take(Bill billForPut, decimal amount)
            {
                if (billForPut == null)
                {
                    LoggerHub.Log(this, $"Счёт для передачи {billForPut.ID} не найден", LoggerHub.LogEventType.dontDisplayOnForm);
                    return false;
                }
                Money -= amount;
                billForPut.Money += amount;
                LoggerHub.Log(
                        this,
                        $"Со счёта переведена сумма {amount}. Счёт:{ID} клиента ID:{ClientID}, на счёт {billForPut.ID}, клиенту ID:{billForPut.ClientID}, Take(Bill billForPut, decimal amount), BillCredit",
                        LoggerHub.LogEventType.DisplayOnForm);
                return true;
            }
            /// <summary>
            /// Снять с этого счёта деньги
            /// </summary>
            /// <param name="amount"></param>
            /// <returns></returns>
            public override bool Take(decimal amount)
            {
                Money -= amount;
                LoggerHub.Log(
                    this,
                    $"Со счёта снята сумма {amount}. Счёт:{ID} клиента ID:{ClientID}, Take(decimal amount), BillCredit",
                    LoggerHub.LogEventType.DisplayOnForm);
                return true;
            }
        }
    }
}
