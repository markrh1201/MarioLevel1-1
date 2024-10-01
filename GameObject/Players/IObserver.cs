using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint4BeanTeam
{
    public interface IObserver
    {
        void Notify(Player player);
    }
}
