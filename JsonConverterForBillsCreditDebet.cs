using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static HW12_6_BankA.ClientBill;

namespace HW12_6_BankA
{
    /// <summary>
    /// Вспомогательный конвертор для JSON NEWTONSOFT для загрузки БД из файлов. Определяет какой конкретно тип счёта 
    /// необходимо создать при десериализации Bill -> BillDeposit или Bill -> BillCredit
    /// </summary>
    internal class JsonConverterForBillsCreditDebet : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(Bill));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jo = JObject.Load(reader);
            if (jo["nativeID"].Value<int>() == 1)
                return jo.ToObject<BillDeposit>(serializer);

            if (jo["nativeID"].Value<int>() == 2)
                return jo.ToObject<BillCredit>(serializer);

            return null;
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }



    }
}
