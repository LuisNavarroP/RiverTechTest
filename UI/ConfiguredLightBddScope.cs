using System;
using LightBDD.Core.Configuration;
using LightBDD.Framework.Configuration;
using LightBDD.Framework.Reporting.Formatters;
using LightBDD.NUnit3;
using NUnit.Framework;
using UI;
using UI.Helpers;

[assembly: Parallelizable(ParallelScope.Fixtures)]
[assembly: ConfiguredLightBddScope]

namespace UI
{
    internal class ConfiguredLightBddScopeAttribute : LightBddScopeAttribute
    {
        // Configuration for report generation
        protected override void OnConfigure(LightBddConfiguration configuration)
        {
            var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");

            configuration
                .ReportWritersConfiguration()
                .AddFileWriter<HtmlReportFormatter>($"Reports/LightBDD_Report_{timestamp}.html") 
                .AddFileWriter<PlainTextReportFormatter>($"Reports/LightBDD_Report_{timestamp}.txt") 
                .AddFileWriter<XmlReportFormatter>($"Reports/LightBDD_Report_{timestamp}.xml");
        }
        protected override void OnSetUp()
        {
            WebDriverManager.SetupTest();

        }
        protected override void OnTearDown()
        {
            WebDriverManager.TearDown();
        }
    }
}
