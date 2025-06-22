using NucleusWPF.MVVM;

namespace TestApp
{
    [TestClass]
    public class AsyncRelayCommandTest
    {
        [TestMethod]
        public void CanExecuteTest()
        {
            var canExecute = true;
            var command = new AsyncRelayCommand(() => Task.Run(() => { }), () => canExecute);
            var command2 = new AsyncRelayCommand(() => Task.Run(() => { }));

            Assert.IsTrue(command.CanExecute(null));
            Assert.IsTrue(command2.CanExecute(null), "CanExecute did not default to True");

            canExecute = false;
            Assert.IsFalse(command.CanExecute(null), "CanExecute should be false when canExecute is false");
        }

        [TestMethod]
        public async Task ExecuteTest()
        {
            var executed = false;
            var canExecute = true;
            var command = new AsyncRelayCommand(() => Task.Run(() => executed = true), () => canExecute);
            await command.ExecuteAsync();
            Assert.IsTrue(executed);

            executed = false; // Reset for next test
            canExecute = false;
            await command.ExecuteAsync();
            Assert.IsFalse(executed, "Command executed when it should not have");
        }

        [TestMethod]
        public void RaiseCanExecuteChangedTest()
        {
            var eventRaised = false;
            var command = new AsyncRelayCommand(() => Task.Run(() => { }));
            command.CanExecuteChanged += (s, e) => eventRaised = true;
            command.RaiseCanExecuteChanged();
            Assert.IsTrue(eventRaised, "RaiseCanExecuteChanged did not raise the event as expected.");
        }
    }
}
