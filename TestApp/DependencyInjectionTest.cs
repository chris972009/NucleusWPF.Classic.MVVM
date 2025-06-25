using NucleusWPF.Classic.MVVM;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
    [TestClass]
    public class DependencyInjectionTest
    {
        [TestInitialize]
        public void Initialize()
        {
            DependencyInjection.Instance.Clear();

            DependencyInjection.Instance.Register<ITransientService, TransientService>();
            DependencyInjection.Instance.RegisterSingleton<ISingletonService, SingletonService>();
        }

        [TestMethod]
        public void ResolveTransientTest()
        {
            var service = DependencyInjection.Instance.Resolve<IMessageService>();
            Assert.IsInstanceOfType(service, typeof(IMessageService), "Resolved type is not of expected type IMessageService.");
            var service2 = DependencyInjection.Instance.Resolve<IMessageService>();
            Assert.AreNotSame(service, service2, "Transient service instances should not be the same.");
        }

        [TestMethod]
        public void ResolveSingletonTest()
        {
            var service = DependencyInjection.Instance.Resolve<IWindowService>();
            Assert.IsInstanceOfType(service, typeof(IWindowService), "Resolved type is not of expected type IMessageService.");
            var service2 = DependencyInjection.Instance.Resolve<IWindowService>();
            Assert.AreSame(service, service2, "Singleton service instances should be the same.");
        }

        [TestMethod]
        public void ResolveViewModelTest()
        {
            var viewModel = DependencyInjection.Instance.Resolve<TestViewModel>();
            Assert.IsInstanceOfType(viewModel, typeof(TestViewModel), "Resolved type is not of expected type TestViewModel.");
            Assert.IsInstanceOfType(viewModel.Service, typeof(IMessageService), "ViewModel's service is not of expected type IMessageService.");
        }

        [TestMethod]
        public void RegisterTransientTest()
        {
            var service = DependencyInjection.Instance.Resolve<ITransientService>();
            Assert.IsInstanceOfType(service, typeof(TransientService), "Resolved type is not of expected type TransientService.");
            var service2 = DependencyInjection.Instance.Resolve<ITransientService>();
            Assert.AreNotSame(service, service2, "Transient service instances should not be the same.");
        }

        [TestMethod]
        public void RegisterSingletonTest()
        {
            var service = DependencyInjection.Instance.Resolve<ISingletonService>();
            Assert.IsInstanceOfType(service, typeof(SingletonService), "Resolved type is not of expected type SingletonService.");
            var service2 = DependencyInjection.Instance.Resolve<ISingletonService>();
            Assert.AreSame(service, service2, "Singleton service instances should be the same.");
        }

        public class TestViewModel
        {
            public TestViewModel(IMessageService messageService)
            {
                Service = messageService;
            }

            public readonly IMessageService Service;
        }

        public interface ITransientService { }

        public class TransientService : ITransientService { }

        public interface ISingletonService { }

        public class SingletonService : ISingletonService { }
    }
}
