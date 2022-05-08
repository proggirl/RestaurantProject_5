using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProject5
{
    public class Egg : CookedFood, IDisposable
        
    {
        int crackCount = 0;
        int DiscardCount = 0;
        public static int quality;
        public int quantity=1;

        public Egg()
        {
            Random rand = new Random();
            quality = rand.Next(101);
        }
        public override void Obtain()
        {
            base.Obtain();
        }

        public void Crack()
        {
            if (crackCount > quantity)
                throw new Exception("Stop to crack eggs!");
            crackCount++;
        }
        public override void Cook()
        {
            if (quality <= 25)
            {
                throw new Exception("This simulates a rotten egg quality is les than 25, quality: "+quality);
            }
        }
        public void DiscardShells()
        {
            if (DiscardCount > quantity)
                throw new Exception("Stop to DiscardShell !");
            if (crackCount < DiscardCount)
            {
                throw new Exception("You did't crack the eggs, that's why you couldn't throw the discardshell !");
            }
            Dispose();
        }
        public override void Serve()
        {
            base.Serve();
        }

        public void Dispose()
        {
            Console.WriteLine("Egg Disposed");
        }
    }
}
