using System;
using API;
using LightBDD.Core.Configuration;
using LightBDD.Framework.Configuration;
using LightBDD.Framework.Reporting.Formatters;
using LightBDD.NUnit3;
using NUnit.Framework;

// setting the parallel execution of test fixtures and setting up the config for all the tests
[assembly: Parallelizable(ParallelScope.Fixtures)]
[assembly: ConfiguredLightBddScope]

namespace API
{
    internal class ConfiguredLightBddScopeAttribute : LightBddScopeAttribute
    {
        // Defining a configuration for the LightBDD reporting and getting the timestamp to make each report unique
        protected override void OnConfigure(LightBddConfiguration configuration)
        {
            {
                var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");

                configuration
                    .ReportWritersConfiguration()
                    .AddFileWriter<HtmlReportFormatter>($"Reports/LightBDD_Report_{timestamp}.html")
                    .AddFileWriter<PlainTextReportFormatter>($"Reports/LightBDD_Report_{timestamp}.txt")
                    .AddFileWriter<XmlReportFormatter>($"Reports/LightBDD_Report_{timestamp}.xml");
            }
        }
    }
}
