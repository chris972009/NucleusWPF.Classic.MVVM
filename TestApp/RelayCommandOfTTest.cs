using NucleusWPF.Classic.MVVM;

namespace TestApp
{
    [TestClass]
    public class RelayCommandOfTTest
    {
        [TestMethod]
        public void CanExecuteTest()
        {
            bool canExecute = true;
            var command = new RelayCommand<bool>((_) => { }, (b) => b);
            var command2 = new RelayCommand<bool>((_) => { }, () => canExecute);
            var command3 = new RelayCommand<bool>((_) => { });

            Assert.IsTrue(command.CanExecute(true));
            Assert.IsTrue(command2.CanExecute(null), "Parameterless CanExecute did not return true");
            Assert.IsTrue(command3.CanExecute(null), "CanExecute did not default to true");

            canExecute = false;
            Assert.IsFalse(command.CanExecute(false), "Can Execute did not return false when expected");
            Assert.IsFalse(command2.CanExecute(null), "Parameterless CanExecute did not return false when expected");
        }

        [TestMethod]
        public void ExecuteTest()
        {
            bool executed = false;
            bool canExecute = true;
            var command = new RelayCommand<bool>((b) => executed = b, () => canExecute);
            command.Execute(true);
            Assert.IsTrue(executed);

            canExecute = false;
            command.Execute(false);
            Assert.IsTrue(executed, "Command executed when it should not have been allowed to execute.");
        }

        [TestMethod]
        public void RaiseCanExecuteChangedTest()
        {
            bool eventRaised = false;
            var command = new RelayCommand<bool>((_) => { });
            command.CanExecuteChanged += (_, _) => eventRaised = true;
            command.RaiseCanExecuteChanged();
            Assert.IsTrue(eventRaised, "CanExecuteChanged event was not raised when expected.");
        }
    }
}
