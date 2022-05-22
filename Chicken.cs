using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProject5
{
    public class Chicken : CookedFood
    {
        int slice = 0;
        int quantity = 1;
        public Chicken()
        {
        }

        public override void Obtain()
        {
            base.Obtain();
        }

        public void CutUp()
        {
            if (slice > quantity)
                throw new Exception("Enough CutUp");
            slice++;
        }

        public override void Cook()
        {
            if (slice < quantity)
                throw new Exception("Slice are not equal quantity");
        }

        public override void Serve()
        {
            base.Serve();
        }
    }
}

