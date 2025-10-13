using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testcontainers.MsSql;
using Xunit;

namespace MyWardrobe.IntegrationTests
{
    public class MsSqlContainerFixture : IAsyncLifetime
    {
        public MsSqlContainer? Container { get; private set; }
        public string ConnectionString => Container!.GetConnectionString(); //  ! innebär att Container inte är null. InitializeAsync kör innan testen startar.
                                                                            //  Så länge ConnectionString används först i testen är Conatiner garanterat inte null.
        public async Task InitializeAsync()
        {
            var password = "p@SswOrd789" + Guid.NewGuid().ToString("N"); // Gör bättre!

            Container = new MsSqlBuilder()
                .WithImage("mcr.microsoft.com/mssql/server:2019-latest") // Är det här rätt version? 
                .WithPassword(password) 
                .WithCleanUp(true)
                .Build();

            await Container.StartAsync();
        }

        public async Task DisposeAsync()                
        {                                               
            if (Container is not null)                  
            {
                try
                {
                    await Container.StopAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"StopAsync misslyckades:  {ex.GetType().Name}: { ex.Message}"); // Använd Logger istället.
                }

                try
                {
                    await Container.DisposeAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"DisposeAsync misslyckades:  {ex.GetType().Name}: {ex.Message}"); // Använd Logger istället.
                }
                finally
                {
                    Container = null;
                }
                         
            }
        }
    }
}
