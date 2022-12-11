using System;

namespace HW12_6_BankA
{
    /// <summary>
    /// Атрибуты изменения или создания данных (например клиента)
    /// </summary>
    public class DataChangeAtributes : IDataChangeAtributes
    {
        public DateTime dateTime { get; set; }
        public string WhatDataIsChange { get; set; }
        public string TypeDataChange { get; set; }
        public string WhoIsChange { get; set; }

        public IDataChangeAtributes GetDataChangeAtributes()
        {
            return (IDataChangeAtributes)this;
        }

        public void SetDataChangeAtributes(IDataChangeAtributes dataChangeAtributes)
        {
            this.dateTime = dataChangeAtributes.dateTime;
            this.WhatDataIsChange = dataChangeAtributes.WhatDataIsChange;
            this.TypeDataChange = dataChangeAtributes.TypeDataChange;
            this.WhoIsChange = dataChangeAtributes.WhoIsChange;
        }
        /// <summary>
        /// Используется в конструкторах, для нового, вновь добавляемого объекта
        /// </summary>
        /// <param name="employer">работник, который добавляет объект</param>
        /// <returns></returns>
        public static DataChangeAtributes NewChangeAtributes(Employer employer)
        {
            DataChangeAtributes dataChangeAtributes = new DataChangeAtributes()
            {
                dateTime = DateTime.Now,
                WhoIsChange = employer.ToString(),
                WhatDataIsChange = "All",
                TypeDataChange = "New Data"
            };
            return dataChangeAtributes;
        }
    }
}
