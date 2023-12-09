using static RestAssured.Dsl;
namespace ServiceAutomation
{
    public class Tests
    {
        private const string BaseUri = "https://api.trello.com/";
        private const string Key = "72d970fcc7a6a4d219b47228a8144a4b"; // Key
        private const string Token = "ATTA437047983d5dc0d4f19eb2f2d6a2d734d8f151c66e028d264179479a3bdb927bFB8CB03E"; // Token
        [SetUp]
        public void Setup()
        {

        }

        private static object? _boardId;
        [Test]
        public void TrelloBoardCreate()
        {
            const string name = "TestBoard";
            _boardId = Given()
                .Post($"{BaseUri}1/boards/?name={name}&key={Key}&token={Token}")
                .Extract()
                .Body("$.id");

            Assert.That(_boardId, Is.Not.Null);
        }
        private static object? _listId;
        private static object? _cardOne;
        private static object? _cardTwo;
        [Test]
        public void TrelloCardCreate()
        {
            const string name = "TestList";
            //Kartlar için liste oluþturulmasý gerekiyor
            _listId = Given().Post($"{BaseUri}1/lists/?name={name}&idBoard={_boardId}&key={Key}&token={Token}").Extract().Body("$.id");

            const string cardOneName = "CardOne";
            _cardOne = Given()
                .Post($"{BaseUri}1/cards/?idList={_listId}&name={cardOneName}&key={Key}&token={Token}")
                .Extract()
                .Body("$.id");

            const string cardTwoName = "CardTwo";
            _cardTwo = Given()
                .Post($"{BaseUri}1/cards/?idList={_listId}&name={cardTwoName}&key={Key}&token={Token}")
                .Extract()
                .Body("$.id");

            Assert.That(_cardOne, Is.Not.Null);
            Assert.That(_cardTwo, Is.Not.Null);
        }

        [Test]
        public void TrelloCardUpdate()
        {
            int randomCard = new Random().Next(2);
            if (randomCard == 0)
            {
                Given()
                    .QueryParam("name", "updatedCardOne")
                    .Put($"{BaseUri}1/cards/{_cardOne}?key={Key}&token={Token}").StatusCode(200);
            }
            else
            {
                Given()
                    .QueryParam("name", "updatedCardTwo")
                    .Put($"{BaseUri}1/cards/{_cardTwo}?key={Key}&token={Token}").StatusCode(200);
            }
            Assert.Pass();
        }

        [Test]
        public void TrelloCardYDelete()
        {
            Given()
                .Delete($"{BaseUri}1/cards/{_cardOne}?key={Key}&token={Token}").StatusCode(200);
            Given()
                .Delete($"{BaseUri}1/cards/{_cardTwo}?key={Key}&token={Token}").StatusCode(200);
        }

        [Test]
        public void TrelloYBoardDelete()
        {
            Given()
                .Delete($"{BaseUri}1/boards/{_boardId}?key={Key}&token={Token}").StatusCode(200);
        }
    }
}