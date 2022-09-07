using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using TrackingSystem.Persistence.DataAccess;
using TrackingSystem.Shared.Abstract;

namespace TrackingSystem.Api.Controllers.Status
{
    [Route("")]
    public class StatusInfoController : BaseApiController
    {
        private readonly TrackingSystemDbContext _dbContext;
        private IConfiguration _config;
        public StatusInfoController(TrackingSystemDbContext dbContext, IConfiguration config)
        {
            _dbContext = dbContext;
            _config = config;
        }

        [HttpGet("")]
        [AllowAnonymous]
        public ContentResult Index()
        {
            int pendingMigrations;
            try
            {
                pendingMigrations = _dbContext.Database.GetPendingMigrations().Count();
            }
            catch
            {
                pendingMigrations = 0;
            }
            string lastExecutedMigration = null;

            try
            {
                var appliedMigrations = _dbContext.Database.GetAppliedMigrations();

                if (appliedMigrations.Count() > 0)
                {
                    lastExecutedMigration = appliedMigrations.Last().Substring(0, 14);
                }
            }
            catch
            {

            }
            string appVersion = "1.0.0";
            string applicationUrl = _config.GetValue<string>("Urls:web");

            var machineName = "";
            try
            {
                machineName = Environment.GetEnvironmentVariable("COMPUTERNAME") ?? Environment.GetEnvironmentVariable("HOSTNAME");
            }
            catch (Exception e)
            {
                machineName = "";
            }
            string htmlContent = @"
                <!doctype html>
                <html class=""no-js"" lang="""">
                    <head>
                        <meta charset=""utf-8"">
                        <meta http-equiv=""X-UA-Compatible"" content=""IE=edge,chrome=1"">
                        <title></title>
                        <meta name=""description"" content="""">
                        <meta name=""viewport"" content=""width=device-width, initial-scale=1"">
                        <link rel=""stylesheet"" href=""/Html/css/normalize.min.css"">
                        <link rel=""stylesheet"" href=""/Html/css/main.css"">
                    </head>
                    <body>
                        <div class=""wrapper"">
                            <div class=""box"">
                                <aside class=""sidebar"">
                                    <div class=""logo""><img src="""" alt=""""></div>
                                    <div class=""miod filter-multiply""><img src=""/Html/img/api-top.png"" alt=""api top""></div>
                                    <div class=""ludzie filter-multiply""><img src=""/Html/img/api-bottom.jpg"" alt=""api bottom""></div>
                                </aside>
                                <main>
                                    <header>
                                        <h1>TruckerFinder API</h1>
                                    </header>
                                    <div id=""info"">
                                    </div>
                                    <footer id=""activate"">
                                    </footer>
                                </main>
                            </div>
                        </div>
                    </body>
                </html>
                </html>
                <script>
                    var STATUS = {  license: " + "true" + @", 
                                    api: true, 
                                    mainConn: " + CheckConnectionStrings(_config.GetConnectionString("DefaultConnection")) + @", 
                                    tmConn: " + CheckConnectionStrings(_config.GetConnectionString("Hangfire")) + @",
                                    machineName: '" + "WebService" + @"',
                                    applicationUrl: '" + applicationUrl + @"',
                                    appVersion: '" + appVersion + @"',
                                    pendingMigrations: " + pendingMigrations + @",
                                    lastExecutedMigration: '" + lastExecutedMigration + @"'
                                };              
                </script>
                <script src=""/Html/js/vendor/jquery.min.js""></script>
                <script src=""/Html/js/vendor/qrcode.min.js""></script>
                <script src=""/Html/js/vendor/vue.min.js""> </script>
                <script src=""/Html/js/main.js""></script>
            ";

            return new ContentResult
            {
                ContentType = "text/html",
                StatusCode = (int)System.Net.HttpStatusCode.OK,
                Content = htmlContent
            };
        }

        private static string CheckConnectionStrings(string connectionString)
        {
            bool isValid = false;
            string decrptedConnectionString = connectionString;

            var sqlConnection = new MySqlConnection(decrptedConnectionString);
            try
            {
                sqlConnection.Open();

                if (sqlConnection.State == System.Data.ConnectionState.Open)
                {
                    isValid = true;
                    sqlConnection.Close();
                }
            }
            catch (Exception e)
            {
                return isValid.ToString().ToLower();
            }

            return isValid.ToString().ToLower();
        }
    }
}
