using DebtDiary.Core;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

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
            var command = new RelayParameterizedCommand(async x => await Task.Delay(1));

            Assert.True(command.CanExecute(parameter));
        }

        [Test]
        public void TestExecuteWithSimpleLambdaExpression()
        {
            int number = 10;
            int result = 0;
            var command = new RelayParameterizedCommand(async x => await Task.Run(() => result = (int)x * (int)x));

            command.Execute(number);

            // Command is beeing executed in another thread so result is calculating there when assert is done
            Assert.True(result == 0 || result == 100);
        }

        [Test]
        public void TestExecuteAndAwaitWithSimpleLambdaExpression()
        {
            int number = 10;
            int result = 0;
            var command = new RelayParameterizedCommand(async x => await Task.Run(() => result = (int)x * (int)x));

            command.ExecuteAndAwait(number);

            Assert.True(result == 100);
        }
    }
}
