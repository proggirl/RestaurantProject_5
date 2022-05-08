using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProject5
{
    public class Cook    
    {
        public bool InProccess;
    public void Process(TableRequests requests)
        {
            var chickens = requests.Get<Chicken>();
            foreach (var ch in chickens)
            {
                ch.CutUp();
                ch.Cook();                
            }

            var eggs = requests.Get<Egg>();
            foreach (var e in eggs)
            {
                e.Crack();
                e.DiscardShells();
                e.Cook();                
            }
        }
    }
}
