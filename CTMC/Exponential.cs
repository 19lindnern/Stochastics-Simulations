using System;

namespace CTMC
{
    public class Exponential
    {
        private double param;
        private Random U = new Random();
        public Exponential(double param)
        {
            this.param = param;
        }

        public double Sample()
        {
            double x = U.NextDouble();
            double y = -(1 / param) * Math.Log(x);
            return y;
        }

        public void SetParam(double param)
        {
            this.param = param;
        }
    }
}