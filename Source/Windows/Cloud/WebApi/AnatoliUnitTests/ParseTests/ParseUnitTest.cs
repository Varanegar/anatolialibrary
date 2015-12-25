using Parse;
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace AnatoliUnitTests.ParseTests
{
    [TestClass]
    public class ParseUnitTest
    {
        [TestMethod]
        public async Task ParsTestMethod()
        {
            await frs();

            Assert.IsTrue(true);
        }

        private async Task frs()
        {
            try
            {
                //ParseClient.Initialize("APPLICATION ID", ".NET KEY");
                ParseClient.Initialize("wUAgTsRuLdin0EvsBhPniG40O24i2nEGVFl8R5OI", "G7guVuyx35bb4fBOwo7BVhlG2L2E2qKLQI0sLAe0");

                //var gameScore = new ParseObject("GameScore");

                //gameScore["score"] = 1337;
                //gameScore["playerName"] = "Sean Plott";

                //await gameScore.SaveAsync();

                //await gameScore.FetchAsync();

                //gameScore = null;

                //var query = ParseObject.GetQuery("GameScore");
                //gameScore = await query.GetAsync("xWMyZ4YEGZ");
                //int score = gameScore.Get<int>("score");
                //string playerName = gameScore.Get<string>("playerName");
                //bool cheatMode = gameScore.Get<bool>("cheatMode");

                var query = from gs in ParseObject.GetQuery("GameScore")
                            where gs.Get<string>("playerName") == "Sean Plott"
                            select gs;

                IEnumerable<ParseObject> results = await query.FindAsync();
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
