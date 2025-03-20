using RkSoftware.TinyBenchmark.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace RkSoftware.TinyBenchmark.Formatters
{
    public static class TinyBenchmarkFormatter
    {
        public static string Format(TinyBenchmarkSummary summary)
        {
            //var s = $"Average: {summary.Average,11:#,##0}, Max: {summary.Results.Max(),11:#,##0}, Min: {summary.Results.Min(),11:#,##0}, Type: {summary.Type.Name}, Method: {summary.Method.Name}";
            var s = $"Average: {summary.Average,11:#,##0}, Type: {summary.Type.Name}, Method: {summary.Method.Name}";
            return s;
        }
        public static string Write(TinyBenchmarkSummary summary, Action<string> act)
        {
            var s = Format(summary);
            act.Invoke(s);
            return s;
        }
        public static IReadOnlyCollection<string> Write(IEnumerable<TinyBenchmarkSummary> summaries, Action<string> act) => summaries.Select(s => Write(s, act)).ToArray();
    }
}
