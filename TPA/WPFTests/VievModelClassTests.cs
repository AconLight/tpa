using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ViewModel.viewmodel;

namespace PresentationTests
{
    [TestClass]
    public class VievModelClassTests
    {
        private ViewModelClass viewModelClass;
        [TestInitialize]
        public void Initialize()
        {
            viewModelClass = new ViewModelClass(null);
        }
        [TestMethod]
        public void pathVariable_WhenModified_ValueSchouldChange()
        {
            string oldOne = "oldOne";
            string newOne = "newOne";

            viewModelClass.pathVariable = oldOne;
            Assert.AreEqual(oldOne, viewModelClass.pathVariable);

            viewModelClass.pathVariable = newOne;
            Assert.AreEqual(newOne, viewModelClass.pathVariable);

        }
        [TestMethod]
        public void load_WhenNotDllFileisChosen_ShouldBeOk()
        {
            viewModelClass.Load();
            Assert.IsNull(viewModelClass.root);
        }
        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void load_WhenWrongPathIsChosen_ShouldThrowExeption()
        {
            viewModelClass.pathVariable = "Wrong.dll";
            viewModelClass.Load();
        }
        
    }
}
