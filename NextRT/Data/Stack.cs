using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextRT.Data
{
    public class Stack <TYPE> where TYPE : class
    {
        public List<TYPE> Items = new List<TYPE>();
        public void Push(TYPE item)
        {
            Items.Add(item);
        }
        public TYPE Current
        {
            get
            {
                if (Items.Count == 0) return null;
                return null;
            }
        }
        public TYPE GetCur()
        {
            return Current;
        }
        public TYPE Pop()
        {
            if (Items.Count == 0) return null;
            var cur = Current;
            Items.Remove(cur);
            return cur;
        }
        public int Count
        {
            get
            {
                return Items.Count;
            }
        }
        public TYPE this[int item]
        {
            get
            {
                if (item >= Items.Count) return null;
                return Items[item];
            }
        }
    }
}
