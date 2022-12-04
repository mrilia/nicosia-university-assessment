using System.Net;
using BoDi;
using Ductus.FluentDocker.Builders;
using Ductus.FluentDocker.Services;
using Microsoft.Extensions.Configuration;
using Xunit.Sdk;

namespace Nicosia.Assessment.AcceptanceTests.Hooks
{
    [Binding]
    public class DockerControllerHooks
    {
        private static ICompositeService? _compositeService;
        private readonly IObjectContainer _objectContainer;

        public DockerControllerHooks(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
        }

        private static IConfiguration LoadConfiguration()
        {
            return new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
        }

        private static string GetDockerComposeLocation(string dockerComposeFileName)
        {
            var directory = Directory.GetCurrentDirectory();
            while (!Directory.EnumerateFiles(directory, "*.yml").Any(a => a.EndsWith(dockerComposeFileName)))
            {
                directory = directory.Substring(0, directory.LastIndexOf(Path.DirectorySeparatorChar));
            }

            return Path.Combine(directory, dockerComposeFileName);
        }

        [BeforeScenario]
        public void AddHttpClient()
        {
            var config = LoadConfiguration();
            var httpClient = new HttpClient()
            {
                BaseAddress = new Uri(config["Nicosia.Assessment.WebApi:BaseAddress"] ?? throw new NullException("Base Address Not Found!"))
            };

            _objectContainer.RegisterInstanceAs(httpClient);
        }

        [BeforeTestRun]
        public static void DockerComposeUp()
        {
            var config = LoadConfiguration();

            var dockerComposeFileName = config["DockerComposeFileName"] ?? throw new NullException("Docker Compose File Name Not Found!"); ;
            var dockerComposePath = GetDockerComposeLocation(dockerComposeFileName);

            var confirmationUrl = config["Nicosia.Assessment.WebApi:BaseAddress"];
            _compositeService = new Builder()
                .UseContainer()
                .UseCompose()
                .FromFile(dockerComposePath)
                .RemoveOrphans()
                .WaitForHttp("webapi", $"{confirmationUrl}/student/list",
                    continuation: (response, _) => response.Code != HttpStatusCode.OK ? 2000 : 0)
                .Build()
                .Start();
        }

        [AfterTestRun]
        public static void DockerComposeDown()
        {
            _compositeService!.Stop();
            _compositeService.Dispose();
        }
    }
}