using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SES.Core
{
    public interface ILab : ISpace
    {
        void EndLab(IClassroom classroom);
    }
}
