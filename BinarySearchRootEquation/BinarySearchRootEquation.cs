namespace BinarySearchRootEquation
{
    public static class BinarySearchRootEquation
    {
        public delegate double Function(double x);

        public static double FindRoot(Function func, double left, double right, double precision = 0.0001)
        {
            while (right - left > precision)
            {
                double midX = left + (right - left) / 2;
                double midY = func.Invoke(midX);
                double sign = midY * func.Invoke(left);

                if (sign < 0)
                {
                    right = midX;
                }else if (sign > 0)
                {
                    left = midX;
                }else
                {
                    return (midY == 0 ? midX : left);
                }
            }
            
            return left;
        }
    }
}
