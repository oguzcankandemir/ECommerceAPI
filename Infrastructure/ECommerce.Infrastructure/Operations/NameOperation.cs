using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.Operations
{
    public static class NameOperation
    {
        public static string CharacterRegulatory(string name)
        {
            string source = @"ığüşöçĞÜŞİÖÇâß\!'^+%&/()=?_@€¨~,;:<>|. ";
            string destination = @"igusocGUSIOC                          --";

            for (int i = 0; i < source.Length; i++)
            {
                name = name.Replace(source[i], destination[i]);
            }
            return name;
        }
    }
}