using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessing
{
    public class FunctionTimeChecker
    {
        public long ElapsedMs { get; private set; } = 0;
        public void CheckFunctionTime(Action action)
        {
            ElapsedMs = 0;
            var watch = System.Diagnostics.Stopwatch.StartNew();
            action();
            watch.Stop();
            ElapsedMs = watch.ElapsedMilliseconds;
        }
        public async void GetFunctionTimeAsync(Func<Task> action)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            await action();
            watch.Stop();
            ElapsedMs = watch.ElapsedMilliseconds;
        }
    }
}
