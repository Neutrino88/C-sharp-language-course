namespace BinarySearchRootEquation
{
    public static class BinarySearchRootEquation
    {
        public delegate double Function(double x);

        public static double FindRoot(double left, double right, double precision, Function func)
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

        public static double[] FindRoots(double left, double right, double precision, params Function[] funcs)
        {
            double[] results = new double[funcs.Length];
            
            System.Func<double, double, double, Function, double> solver = FindRoot;
            System.IAsyncResult[] asyncResults = new System.IAsyncResult[funcs.Length];

            int i = 0;
            foreach (var func in funcs)
            {
                asyncResults[i++] = solver.BeginInvoke(left, right, precision, func, null, null);
            }

            i = 0;
            foreach (var asyncResult in asyncResults)
            {
                results[i++] = solver.EndInvoke(asyncResult);
            }

            return results;
        }
    }
}
