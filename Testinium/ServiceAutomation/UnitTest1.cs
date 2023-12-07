using RestAssured.Request.Builders;
using RestAssured.Request.Logging;
using RestAssured.Response.Logging;
using NUnit.Framework;
using System.Net;
using static RestAssured.Dsl;
namespace ServiceAutomation
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void TrelloBoardCreate()
        {
            Given()
        .Body("")
        .When()
        .Post("https://api.trello.com/1/boards/?name=tBoard&key=72d970fcc7a6a4d219b47228a8144a4b&token=ATTA437047983d5dc0d4f19eb2f2d6a2d734d8f151c66e028d264179479a3bdb927bFB8CB03E&prefs_permissionLevel=public")
        .Then()
        .StatusCode(200);


            Assert.Pass();
        }
        [Test]
        public void TrelloCardCreate()
        {
            Given()
       .Body("")
       .When()
       .Post("https://api.trello.com/1/cards?idList=65721e5c9805547da736c93e&key=72d970fcc7a6a4d219b47228a8144a4b&token=ATTA437047983d5dc0d4f19eb2f2d6a2d734d8f151c66e028d264179479a3bdb927bFB8CB03E&name=Taha&desc=123456")
       .Then()
       .StatusCode(200);


            Assert.Pass();
        }

    }
}