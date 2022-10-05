using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using WpfSandbox.ViewModels;

namespace WpfSandboxTests.ViewModels
{
    [TestClass]
    public class MainWindowViewModelTests
    {
        [TestMethod]
        public void ConstructorIsOK()
        {
            // Arrange
            // Act
            var target = new MainWindowViewModel(path);

            // Assert
            Assert.AreEqual(0, target.Workspaces.Count, "Workspaces is not empty");
            Assert.AreEqual(2, target.Commands.Count);
        }

        [TestMethod]
        public void ExecuteFirstCommand()
        {
            // Arrange
            var target = new MainWindowViewModel(path);
            var commandViewModel = target.Commands.First(m => (m as ViewModelBase).DisplayName == "View all customers");

            // Act
            commandViewModel.Command.Execute(null);

            // Assert
            Assert.AreEqual(1, target.Workspaces.Count, " Did not create viewmodel.");
        }

        private const string path = "../../WpfSandbox/Data/customers.xml";

    }
}
