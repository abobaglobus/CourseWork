using System.Globalization;
using CourseWorkApp;
namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestSortCallsByAddress_SortsCorrectly()
        {
            var calls = new List<Call>
            {
                new Call { Address = "ул. Бета, д.2" },
                new Call { Address = "ул. Альфа, д.1" },
                new Call { Address = "ул. Гамма, д.3" }
            };
            Program.Calls = calls;
            Program.SortCallsByAddress();
            Assert.AreEqual("ул. Альфа, д.1", Program.Calls[0].Address);
            Assert.AreEqual("ул. Бета, д.2", Program.Calls[1].Address);
            Assert.AreEqual("ул. Гамма, д.3", Program.Calls[2].Address);
        }
        [TestMethod]
        public void TestAddCallMenu_AddsCallCorrectly()
        {
            var calls = new List<Call>();
            Program.Calls = calls;
            var newCall = new Call
            {
                BrigadeNumber = 1,
                BrigadeSpecialization = "Кардиология",
                CallDate = DateTime.ParseExact("01.01.2023", "dd.MM.yyyy", CultureInfo.InvariantCulture),
                CallTime = DateTime.ParseExact("12:30", "HH:mm", CultureInfo.InvariantCulture),
                Address = "ул. Пушкина, д.1",
                PatientFullName = "Иванов Иван Иванович",
                PatientBirthDate = DateTime.ParseExact("01.01.1980", "dd.MM.yyyy", CultureInfo.InvariantCulture),
                PatientSex = "М",
                BrigadeActions = "Осмотр, Рекомендации"
            };
            Program.Calls.Add(newCall);
            Assert.AreEqual(1, Program.Calls.Count);
            Assert.AreEqual("Кардиология", Program.Calls[0].BrigadeSpecialization);
        }

        [TestMethod]
        public void TestDeleteCallMenu_DeletesCallCorrectly()
        {
            var calls = new List<Call>
            {
                new Call { Address = "ул. Пушкина, д.1" },
                new Call { Address = "ул. Ленина, д.5" }
            };
            Program.Calls = calls;
            Program.Calls.RemoveAt(0);
            Assert.AreEqual(1, Program.Calls.Count);
            Assert.AreEqual("ул. Ленина, д.5", Program.Calls[0].Address);
        }
        [TestMethod]
        public void TestSortCallsByPatientFullName_SortsCorrectly()
        {
            var calls = new List<Call>
            {
                new Call { PatientFullName = "Борисов Борис Борисович" },
                new Call { PatientFullName = "Александров Александр Александрович" },
                new Call { PatientFullName = "Владимиров Владимир Владимирович" }
            };
            Program.Calls = calls;
            Program.SortCallsByPatientFullName();
            Assert.AreEqual("Александров Александр Александрович", Program.Calls[0].PatientFullName);
            Assert.AreEqual("Борисов Борис Борисович", Program.Calls[1].PatientFullName);
            Assert.AreEqual("Владимиров Владимир Владимирович", Program.Calls[2].PatientFullName);
        }
    }
}