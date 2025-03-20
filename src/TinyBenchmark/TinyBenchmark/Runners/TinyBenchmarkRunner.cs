using RkSoftware.TinyBenchmark.Attributes;
using RkSoftware.TinyBenchmark.Results;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace RkSoftware.TinyBenchmark.Runners
{
    public static class TinyBenchmarkRunner
    {
        static Type attribute = typeof(TinyBenchmarkAttribute);
        static int defaultCount = 10;
        static int defaultInnerCount = 1_000;

        public static IReadOnlyCollection<TinyBenchmarkSummary> Run<T>() => Run<T>(defaultCount);
        public static IReadOnlyCollection<TinyBenchmarkSummary> Run<T>(int count) => Run<T>(count, defaultInnerCount);

        public static IReadOnlyCollection<TinyBenchmarkSummary> Run<T>(int count, int innerCount)
        {
            Stopwatch sw = new();
            var type = typeof(T);
            var methods = type.GetMethods()
                .Where(m => m.GetCustomAttributes(attribute).FirstOrDefault() != null)
                .Where(m => m.GetParameters().Length == 0);
            var results = methods.Select(m => (m, Run(sw, m, count, defaultInnerCount)));
            var summaries = results.Select(r => new TinyBenchmarkSummary { Type = type, Method = r.m, Results = r.Item2 });
            var array = summaries.ToArray();
            return array;
        }

        static IReadOnlyCollection<long> Run(Stopwatch sw, MethodInfo method, int count, int innerCount)
        {
            // Console.WriteLine($"{method.DeclaringType?.Name} :: {method.Name}");
            var results = Enumerable.Range(0, count).Select(_ => Run(sw, method, innerCount));
            var array = results.ToArray();
            return array;
        }

        static long Run(Stopwatch sw, MethodInfo method, int innerCount)
        {
            sw.Reset();
            sw.Start();
            foreach (var _ in Enumerable.Range(0, innerCount)) Run(method);
            sw.Stop();
            var result = sw.ElapsedTicks;
            sw.Reset();
            return result;
        }

        static void Run(MethodInfo method) => method.Invoke(null, null);
    }
}
