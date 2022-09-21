using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using BL;

namespace PL
{
    public class HebCalModel
    {
        IBL BL;

        public HebCalModel()
        {
            BL = new BLIMP();
        }
    }
}
