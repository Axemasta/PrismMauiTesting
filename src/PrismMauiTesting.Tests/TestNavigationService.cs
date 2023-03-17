using FluentAssertions;
using Prism.Common;

namespace PrismMauiTesting.Tests
{
    public interface IMockNavigationService
    {
        void SetupNavigationResult(string destination, bool success);

        void SetupNavigationResult(string destination, INavigationParameters navigationParameters, bool success);
    }

    public class MoqNavigationService : Mock<INavigationService>, IMockNavigationService, INavigationService, IRegistryAware
	{
        private IViewRegistry viewRegistry;

		public MoqNavigationService()
		{
            viewRegistry = new MockViewRegistry();

            var mockResult = new Mock<INavigationResult>();

            mockResult.SetupGet(m => m.Success)
                .Returns(false);

            this.Setup(m => m.NavigateAsync(It.IsAny<Uri>(), It.IsAny<INavigationParameters>()))
                .ReturnsAsync(mockResult.Object);
        }

        #region IMockNavigationSetup

        public void SetupNavigationResult(string destination, bool success)
        {
            var mockResult = new Mock<INavigationResult>();

            mockResult.SetupGet(m => m.Success)
                .Returns(success);

            this.Setup(
                m => m.NavigateAsync(
                    It.Is<Uri>(u => u.Equals(destination)),
                    It.IsAny<INavigationParameters>()))
                .ReturnsAsync(mockResult.Object);
        }

        public void SetupNavigationResult(string destination, INavigationParameters navigationParameters, bool success)
        {
            var mockResult = new Mock<INavigationResult>();

            mockResult.SetupGet(m => m.Success)
                .Returns(success);

            this.Setup(
                m => m.NavigateAsync(
                    It.Is<Uri>(u => u.Equals(destination)),
                    It.Is<INavigationParameters>(n => Equivalent(n, navigationParameters))))
                .ReturnsAsync(mockResult.Object);
        }

        private bool Equivalent(object a, object b)
        {
            try
            {
                if (a.Equals(b))
                {
                    return true;
                }

                a.Should().BeEquivalentTo(b);

                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion IMockNavigationSetup

        #region INavigationService

        public Task<INavigationResult> GoBackAsync(INavigationParameters parameters)
        {
            return Object.GoBackAsync(parameters);
        }

        public Task<INavigationResult> GoBackToAsync(string name, INavigationParameters parameters)
        {
            return Object.GoBackToAsync(name, parameters);
        }

        public Task<INavigationResult> GoBackToRootAsync(INavigationParameters parameters)
        {
            return Object.GoBackToRootAsync(parameters);
        }

        public Task<INavigationResult> NavigateAsync(Uri uri, INavigationParameters parameters)
        {
            return Object.NavigateAsync(uri, parameters);
        }

        #endregion INavigationService

        #region IRegistryAware

        public IViewRegistry Registry => viewRegistry;

        #endregion IRegistryAware

        private class MockViewRegistry : IViewRegistry
        {
            public IEnumerable<ViewRegistration> Registrations { get; } = new List<ViewRegistration>();

            public object CreateView(IContainerProvider container, string name)
            {
                return name;
            }

            public string GetViewModelNavigationKey(Type viewModelType)
            {
                return viewModelType.Name;
            }

            public Type GetViewType(string name)
            {
                return name.GetType();
            }

            public bool IsRegistered(string name)
            {
                return true;
            }

            public IEnumerable<ViewRegistration> ViewsOfType(Type baseType)
            {
                return Registrations;
            }
        }
    }
}

