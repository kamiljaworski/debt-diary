using DebtDiary.Core;
using NUnit.Framework;
using System;

namespace DebtDiary.Tests.ViewModels
{
    [TestFixture]
    public class RelayParameterizedCommandTests
    {
        [Test]
        public void TestConstructorPassingNullThrowsException() => Assert.Throws<ArgumentNullException>(() => new RelayParameterizedCommand(null));

        [TestCase(null)]
        [TestCase("")]
        [TestCase("asdsad")]
        public void TestCanExecuteReturnsTrue(object parameter)
        {
            //var command = new RelayParameterizedCommand(x => { });

            // Assert.True(command.CanExecute(parameter));
            Assert.True(false);
        }

        [Test]
        public void TestExecuteWithSimpleLambdaExpression()
        {
            int number = 10;
            int result = 0;
            //var command = new RelayParameterizedCommand(x => result = (int)x * (int)x);

            //command.Execute(number);

            Assert.True(result == 100);
        }
    }
}
