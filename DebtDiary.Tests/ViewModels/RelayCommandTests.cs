using DebtDiary.Core;
using NUnit.Framework;
using System;

namespace DebtDiary.Tests.ViewModels
{
    [TestFixture]
    public class RelayCommandTests
    {
        [Test]
        public void TestConstructorPassingNullThrowsException() => Assert.Throws<ArgumentNullException>(() => new RelayCommand(null));

        [TestCase(null)]
        [TestCase("")]
        [TestCase("asdsad")]
        public void TestCanExecuteReturnsTrue(object parameter)
        {
            var command = new RelayCommand(() => { });

            Assert.True(command.CanExecute(parameter));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("asdsad")]
        public void TestExecuteAlwaysExecuteSimpleLambdaExpression(object parameter)
        {
            int number = 10;
            var command = new RelayCommand(() => number = number * number);

            command.Execute(parameter);

            Assert.True(number == 100);
        }
    }
}
