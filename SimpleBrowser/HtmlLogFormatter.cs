namespace SimpleBrowser
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using RazorEngine;
    using SimpleBrowser.Properties;

    public class HtmlLogFormatter
    {
        public class RazorModel
        {
            public DateTime CaptureDate { get; set; }

            public string Title { get; set; }

            public TimeSpan TotalDuration { get; set; }

            public List<LogItem> Logs { get; set; }

            public int RequestsCount { get; set; }
        }

        public string Render(List<LogItem> logs, string title)
        {
            return Razor.Parse(
                Resources.HtmlLogTemplate,
                new RazorModel
                    {
                        CaptureDate = DateTime.UtcNow,
                        TotalDuration =
                            logs.Count == 0 ? TimeSpan.MinValue : logs.Last().ServerTime - logs.First().ServerTime,
                        Title = title,
                        Logs = logs,
                        RequestsCount = logs.Count(l => l is HttpRequestLog)
                    });
        }
    }
}
