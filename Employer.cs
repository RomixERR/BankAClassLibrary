using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW12_6_BankA
{
    public class Employer 
    {
        public string Name { get; private set; }
        public string Role { get; private set; }
        public Permission Permission { get; private set; }

        public Employer(string name, string role, Permission permission)
        {
            this.Name = name;
            this.Role = role;
            this.Permission = permission;
        }


        public override string ToString()
        {
            return $"Role: {Role}, Name: {Name}";
        }
    }
}
