using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using SdHelper.Models;

namespace SdHelperTests.Models
{
    [TestFixture]
    public class TextWrapperTest
    {
        [Test]
        public void プロンプトをWordリストに変換するテスト()
        {
            var textWrapper = new TextWrapper
            {
                Text = "best quality, test1 ,test2 , test3 \n"
                       + "(line2), (line2 test:1.1), (line2, test:1.2) \n "
                       + "\n "
                       + "(((line4))), \n"
                       + "(((line5))), \n",
            };

            var words = textWrapper.Words;

            // それぞれのテキストプロパティに入っていることが期待される値
            var expectedWords = new List<string>
            {
                "best quality", "test1", "test2", "test3", "\n",
                "(line2)", "(line2 test:1.1)", "(line2", "test:1.2)", "\n",
                "\n",
                "(((line4)))", "\n",
                "(((line5)))", "\n",
            };

            // Check if words match expected values
            Assert.That(words.Select(w => w.Text).ToList(), Is.EqualTo(expectedWords));
        }

        [Test]
        public void TextWrapper_HandlesEmptyLines_Correctly()
        {
            var textWrapper = new TextWrapper { Text = "\n \n \n", };

            var words = textWrapper.Words;

            var expectedWords = new List<string> { "\n", "\n", "\n", };

            Assert.That(words.Select(w => w.Text).ToList(), Is.EqualTo(expectedWords));
        }
    }
}