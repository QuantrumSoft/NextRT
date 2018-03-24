using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextRT.Data
{
    public class List <TYPE>
    {
        public List<TYPE> Items = new List<TYPE>();
        public void Add(TYPE item)
        {
            Items.Add(item);
        }
        public int Count
        {
            get
            {
                return Items.Count;
            }
        }
        public void Clear()
        {
            Items.Clear();
        }
    }
}
