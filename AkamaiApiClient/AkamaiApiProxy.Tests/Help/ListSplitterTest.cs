using AkamaiApiProxy.Configuration;
using AkamaiApiProxy.Helper;
using AkamaiApiProxy.Instrumentation;
using Moq;
using NUnit.Framework;

namespace AkamaiApiProxy.Tests.Help
{
    [TestFixture]
    public class ListSplitterTest
    {
        private Logable _logger;
        private Chunkable _chunkable;

        [TestFixtureSetUp]
        public void ListSplitterSetup()
        {
            _logger = DoMockLogable();
            _chunkable = DoMockChunkable(50);
        }

        [Test]
        public void SplitList_CreateObject()
        {
            var splitter = new ListSplitter(_logger, _chunkable);
            Assert.IsNotNull(splitter);
        }

        [TestCase(1)]
        [TestCase(10)]
        [TestCase(33)]
        [TestCase(50)]
        [TestCase(100)]
        public void SplitList_ChuckSize_PropertySet(int chuncksize)
        {
            var splitter = new ListSplitter(_logger, DoMockChunkable(chuncksize));
            Assert.AreEqual(chuncksize, splitter.ChunckSize);
        }

        [TestCase(1, 50, 1)]
        [TestCase(10, 50, 1)]
        [TestCase(100, 50, 2)]
        [TestCase(199, 50, 4)]
        [TestCase(200, 50, 4)]
        [TestCase(201, 50, 5)]
        [TestCase(200, 35, 6)]
        public void SplitList_ValidateGivenChunckSize_CorrectSublistCountCreated(int listsize, int chuncksize, int listcount)
        {
            var list = GenerateList(listsize);
            var splitter = new ListSplitter(_logger, DoMockChunkable(chuncksize));
            var lists = splitter.Split(list);

            Assert.AreEqual(listcount, lists.Count);
        }

        private static Chunkable DoMockChunkable(int chuncksize)
        {
            var m = new Mock<Chunkable>();
            m.SetupGet(x => x.ChunkSize).Returns(chuncksize);
            return m.Object;
        }

        private static Logable DoMockLogable()
        {
            var m = new Mock<Logable>();
            m.Setup(x => x.Info(It.IsAny<string>()));
            m.Setup(x => x.Warn(It.IsAny<string>()));
            m.Setup(x => x.Debug(It.IsAny<string>()));
            m.Setup(x => x.Error(It.IsAny<string>()));
            m.Setup(x => x.Fatal(It.IsAny<string>()));
            return m.Object;
        }

        private static string[] GenerateList(int size)
        {
            var list = new string[size];
            for (var i = 0; i < size; i++)
            {
                list[i] = "Test " + i.ToString();
            }
            return list;
        }
    }
}
