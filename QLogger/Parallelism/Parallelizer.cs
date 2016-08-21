using System.Threading.Tasks;

namespace QLogger.Parallelism
{
    // This is the base class that provides the context for parallelizer to inherit
    // so it doesn't need to worry about incorporating the info in its arguments and
    // can have a conformant signature as its serial counterpart
    public abstract class Parallelizer
    {
        public Parallelizer(int maxDegreeOfParallelism = int.MaxValue)
        {
            ParallelOptions = new ParallelOptions { MaxDegreeOfParallelism = maxDegreeOfParallelism };
        }

        public Parallelizer(ParallelOptions options)
        {
            ParallelOptions = options;
        }

        public ParallelOptions ParallelOptions { get; }
    }
}
