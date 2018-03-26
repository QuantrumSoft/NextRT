using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextRT.Data
{
    public class DataBase <TYPE,VALUE> where VALUE : class 
    {

        public Dictionary<TYPE,VALUE> Items = new Dictionary<TYPE, VALUE>();

        public void Add(TYPE id,VALUE item)
        {
            Items.Add(id, item);
        }

        public void Remove(TYPE id)
        {
            if (Items.ContainsKey(id))
            {
                Items.Remove(id);
            }
        }

        public VALUE this[TYPE id]
        {
            get
            {
                if (Items.ContainsKey(id))
                {
                    return Items[id];
                }
                return null;
            }
        }


    }
}
