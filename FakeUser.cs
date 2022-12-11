using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeUsersLite
{
    public class FakeUser : FakeUserDatasetRUS
    {
        public enum Egender
        { 
            Male,
            Female,
            Horse
        }
        Random r = new Random();
        public Egender Gender { get; set; }
        public FakeUser(Egender gender) { Gender = gender;}
        public FakeUser() : this(Egender.Male)  {}
        private string Cutter(string S, int minLen, int maxLen)
        {
            int A = r.Next(minLen, maxLen);
            if (A >= S.Length) A = S.Length / 2;
            int B = r.Next(0, S.Length - A - 1);
            return S.Substring(B, A);
        }
        private string ArrayRandomDataGetter(string[] S)
        {
            return S[r.Next(0, S.Length)];
        }
        public string GetFName()
        {
            string S,S2;
            if (Gender == Egender.Male)
            {
                S = Cutter(NamesFMale,4, 7);
            }else
            {
                S = Cutter(NamesFFemale, 4, 7);
            }
            S2 = S.Substring(0, 1).ToUpper();
            return S.Insert(0, S2).Remove(1,1);
        }
        public string GetLName()
        {
            string S, S2,S3 ,S1;
            
            S = Cutter(NamesL,5, 11);
            S2 = S.Substring(0, 1).ToUpper();
            S3 = S.Insert(0, S2).Remove(1, 1);

            if (Gender == Egender.Male)
            {
                S1 = ArrayRandomDataGetter(NamesLEndsMale);
            }
            else
            {
                S1 = ArrayRandomDataGetter(NamesLEndsFeMale);
            }


            return S3+S1;
        }
        public string GetMName()
        {
            string S, S2, S3, S1;

            S = Cutter(NamesL, 4, 7);
            S2 = S.Substring(0, 1).ToUpper();
            S3 = S.Insert(0, S2).Remove(1, 1);

            if (Gender == Egender.Male)
            {
                S1 = ArrayRandomDataGetter(NamesMEndsMale);
            }
            else
            {
                S1 = ArrayRandomDataGetter(NamesMEndsFeMale);
            }


            return S3 + S1;
        }
        public string GetFullName()
        {
            return GetLName() + " " + GetFName() + " " + GetMName();
        }
        public string GetPhone()
        {
            return "+7-(" + GetNums(3) + ")-"+ GetNums(3) + "-" + GetNums(3);
        }
        public int GetPhoneInt()
        {
            return int.Parse(8 + GetNums(9));
        }
        public string GetPasport()
        {
            return GetNums(4) + " " + GetNums(7);
        }
        public int GetPasportInt()
        {
            return int.Parse(GetNums(11));
        }
        private string GetNums(int kolNums)
        {
            string S = string.Empty;
            for (int i = 0; i < kolNums; i++)
            {
                S += r.Next(0, 10);
            }
            return S;
        }

 


    }
}
