using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextRT.Data
{
    public class Identity
    {
        public static long CodeIndex = 0;

        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
            }
        }
        public string _Name = "";

        public long Code
        {

            get
            {
                return _Code;
            }
            set
            {
                _Code = value;
            }
        }
        public long _Code = 0;

        public Identity(string name)
        {
            _Name = name;
            _Code = CodeIndex++;
            
        }

    }
}
