using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using NucleusWPF.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
    [TestClass]
    public class AsyncRelayCommandOfTTest
    {
        [TestMethod]
        public void CanExecuteTest()
        {
            bool canExecute = true;
            Func<bool, Task> t = (_) => Task.Run(() => { });
            var command = new AsyncRelayCommand<bool>(t, (bool b) => b);
            var command2 = new AsyncRelayCommand<bool>(t, () => canExecute);
            var command3 = new AsyncRelayCommand<bool>(t);

            Assert.IsTrue(command.CanExecute(true));
            Assert.IsTrue(command2.CanExecute(null), "Parameterless CanExecute returned false");
            Assert.IsTrue(command3.CanExecute(null), "CanExecute did not default to true");

            canExecute = false;
            Assert.IsFalse(command.CanExecute(false), "CanExecute did not return false when expected");
            Assert.IsFalse(command2.CanExecute(null), "Parameterless CanExecute did not return false when expected");
        }

        [TestMethod]
        public async Task ExecuteTest()
        {
            var executed = false;
            var canExecute = true;
            var command = new AsyncRelayCommand<bool>((bool b) => Task.Run(() => executed = b), () => canExecute);
            await command.ExecuteAsync(true);
            Assert.IsTrue(executed, "Command did not execute when expected");

            canExecute = false;
            await command.ExecuteAsync(false);
            Assert.IsTrue(executed, "Command executed when it should not have");
        }

        [TestMethod]
        public void RaiseCanExecuteChangedTest()
        {
            var eventRaised = false;
            var command = new AsyncRelayCommand<bool>((_) => Task.Run(() => { }));
            command.CanExecuteChanged += (s, e) => eventRaised = true;
            command.RaiseCanExecuteChanged();
            Assert.IsTrue(eventRaised, "CanExecuteChanged event was not raised when expected");
        }
    }
}
