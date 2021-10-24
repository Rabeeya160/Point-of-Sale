using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stock
{
    class ClassDL
    {
        public ClassDL()
        {

        }

        public string category_name;


        public string AName
        {
            get
            {
                return category_name;
            }
            set
            {
                category_name = value;
            }

        }

    }
}
