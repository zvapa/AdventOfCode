namespace Utils;

public static class Helpers
{
    public static IEnumerable<int> Range(int start, int stop, int step = 1)
    {
        if (step == 0)
            throw new ArgumentException("Step cannot be zero.", nameof(step));

        if (stop <= start && step > 0)
            throw new ArgumentException("Step direction is inconsistent with Start and Stop positions", nameof(step));

        return RangeIterator(start, stop, step);

        static IEnumerable<int> RangeIterator(int start, int stop, int step)
        {
            int x = start;

            while ((step > 0 || x > stop) && (step < 0 || x < stop))
            {
                yield return x;
                x += step;
            }
        }
    }
}