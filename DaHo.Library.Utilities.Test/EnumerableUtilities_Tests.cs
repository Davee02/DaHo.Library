using System.Collections.Generic;
using System.Collections.ObjectModel;
using DaHo.Library.Utilities;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void ToObservableCollection_Returns_Correct_Collection()
        {
            var myList = new List<string> { "value1", "value2", "value3" };

            var returnedCollection = myList.ToObservableCollection();

            Assert.AreEqual(myList.Count, returnedCollection.Count);
            Assert.AreEqual(myList[2], returnedCollection[2]);
            Assert.AreEqual(typeof(ObservableCollection<string>), returnedCollection.GetType());
        }
    }
}