using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace RkSoftware.TinyBenchmark.Results
{
    public record TinyBenchmarkSummary
    {
        public required Type Type { get; init; }
        public required MethodInfo Method { get; init; }
        public required IReadOnlyCollection<long> Results { get; init; }
        public long Average => (long)Results.Average();
    }
}
