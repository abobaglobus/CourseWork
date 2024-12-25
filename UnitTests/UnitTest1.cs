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
                new Call { Address = "��. ����, �.2" },
                new Call { Address = "��. �����, �.1" },
                new Call { Address = "��. �����, �.3" }
            };
            Program.Calls = calls;
            Program.SortCallsByAddress();
            Assert.AreEqual("��. �����, �.1", Program.Calls[0].Address);
            Assert.AreEqual("��. ����, �.2", Program.Calls[1].Address);
            Assert.AreEqual("��. �����, �.3", Program.Calls[2].Address);
        }
        [TestMethod]
        public void TestAddCallMenu_AddsCallCorrectly()
        {
            var calls = new List<Call>();
            Program.Calls = calls;
            var newCall = new Call
            {
                BrigadeNumber = 1,
                BrigadeSpecialization = "�����������",
                CallDate = DateTime.ParseExact("01.01.2023", "dd.MM.yyyy", CultureInfo.InvariantCulture),
                CallTime = DateTime.ParseExact("12:30", "HH:mm", CultureInfo.InvariantCulture),
                Address = "��. �������, �.1",
                PatientFullName = "������ ���� ��������",
                PatientBirthDate = DateTime.ParseExact("01.01.1980", "dd.MM.yyyy", CultureInfo.InvariantCulture),
                PatientSex = "�",
                BrigadeActions = "������, ������������"
            };
            Program.Calls.Add(newCall);
            Assert.AreEqual(1, Program.Calls.Count);
            Assert.AreEqual("�����������", Program.Calls[0].BrigadeSpecialization);
        }

        [TestMethod]
        public void TestDeleteCallMenu_DeletesCallCorrectly()
        {
            var calls = new List<Call>
            {
                new Call { Address = "��. �������, �.1" },
                new Call { Address = "��. ������, �.5" }
            };
            Program.Calls = calls;
            Program.Calls.RemoveAt(0);
            Assert.AreEqual(1, Program.Calls.Count);
            Assert.AreEqual("��. ������, �.5", Program.Calls[0].Address);
        }
        [TestMethod]
        public void TestSortCallsByPatientFullName_SortsCorrectly()
        {
            var calls = new List<Call>
            {
                new Call { PatientFullName = "������� ����� ���������" },
                new Call { PatientFullName = "����������� ��������� �������������" },
                new Call { PatientFullName = "���������� �������� ������������" }
            };
            Program.Calls = calls;
            Program.SortCallsByPatientFullName();
            Assert.AreEqual("����������� ��������� �������������", Program.Calls[0].PatientFullName);
            Assert.AreEqual("������� ����� ���������", Program.Calls[1].PatientFullName);
            Assert.AreEqual("���������� �������� ������������", Program.Calls[2].PatientFullName);
        }
    }
}