using Microsoft.VisualBasic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;

namespace CourseWorkApp
{
    internal class Program
    {
        const string callsFilePath = "calls.txt";
        const string specializationsFilePath = "specializations.txt";
        const string actionsFilePath = "actions.txt";
        static void Main(string[] args)
        {
            MainMenu();
        }

        static List<Call> LoadCalls()
        {
            var allCallsLines = File.ReadAllLines(callsFilePath);
            List<Call> calls = new List<Call>();

            foreach (var callLine in allCallsLines)
            {
                var splitCallLine = callLine.Split(";");

                Call call;
                call.BrigadeNumber = int.Parse(splitCallLine[0]);
                call.BrigadeSpecialization = splitCallLine[1];
                call.CallDate = DateTime.ParseExact(splitCallLine[2], "dd.MM.yyyy",
                    CultureInfo.InvariantCulture);
                call.CallTime = DateTime.ParseExact(splitCallLine[3], "HH:mm",
                    CultureInfo.InvariantCulture);
                call.Address = splitCallLine[4];
                call.PatientFullName = splitCallLine[5];
                call.PatientBirthDate = DateTime.ParseExact(splitCallLine[6], "dd.MM.yyyy",
                    CultureInfo.InvariantCulture);
                call.PatientSex = splitCallLine[7];
                call.BrigadeActions = splitCallLine[8].Split(", ").ToList();

                calls.Add(call);
            }

            return calls;
        }
        static List<string> LoadDirectory(string directoryFilePath)
        {
            var directory = File.ReadAllLines(directoryFilePath).ToList();

            return directory;
        }

        static void SaveCalls(List<Call> calls)
        {
            using (StreamWriter sw = new StreamWriter(callsFilePath))
            {
                foreach (var call in calls)
                {
                    var callText = CallToLine(call);
                    sw.WriteLine(callText);
                }
            }
        }

        static void SaveDirectory(List<string> directory, string directoryFilePath)
        {
            using (StreamWriter sw = new StreamWriter(directoryFilePath))
            {
                foreach (var entry in directory)
                {
                    sw.WriteLine(entry);
                }
            }
        }

        static void SortCallsMenu(List<Call> calls)
        {
            Console.Clear();
            Console.WriteLine("Введите через запятую номера полей для сортировки" +
                " (пример: 1,4,5). Убедитесь, что ввод, отличный от 0, содержит " +
                "только цифры и запятые, а между запятыми целые числа 1-5.");
            Console.WriteLine("1) Специализация бригады");
            Console.WriteLine("2) Адрес");
            Console.WriteLine("3) ФИО пациента");
            Console.WriteLine("4) Пол");
            Console.WriteLine("5) Возраст");
            Console.WriteLine("0) Вернуться назад");
            Console.Write(">>> ");

            var sortingOptions = Console.ReadLine();

            if (sortingOptions == "0")
            {
                return;
            }

            while (!sortingOptions.Split(",").All(x => int.TryParse(x, out _) &&
                   int.Parse(x) >= 1 && int.Parse(x) <= 5))
            {
                Console.WriteLine("Неверный ввод, попробуйте заново!");
                Console.Write(">>> ");
                sortingOptions = Console.ReadLine();

                if (sortingOptions == "0")
                {
                    return;
                }
            }

            foreach (var sortingOption in sortingOptions.Split(","))
            {
                switch (sortingOption)
                {
                    case "1":
                        SortCallsBySpecialization(calls);
                        break;
                    case "2":
                        SortCallsByAddress(calls);
                        break;
                    case "3":
                        SortCallsByPatientFullName(calls);
                        break;
                    case "4":
                        SortCallsByPatientSex(calls);
                        break;
                    case "5":
                        SortCallsByPatientBirthDate(calls);
                        break;
                }
            }

            SaveCalls(calls);
        }

        static void CallsMenu(List<Call> calls)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1) Просмотреть вызовы");
                Console.WriteLine("2) Сортировать вызовы");
                Console.WriteLine("3) Добавить вызов");
                Console.WriteLine("4) Изменить вызов");
                Console.WriteLine("5) Удалить вызов");
                Console.WriteLine("6) Найти вызов");
                Console.WriteLine("0) Вернуться назад");
                Console.Write(">>> ");

                var option = Console.ReadKey().Key;

                switch (option)
                {
                    case ConsoleKey.D1:
                        Console.Clear();
                        Console.WriteLine("Список вызовов");
                        Console.WriteLine();
                        PrintCalls(calls);
                        Console.WriteLine();
                        Console.WriteLine("Чтобы закрыть список вызовов, нажмите любую клавишу.");
                        Console.ReadKey();
                        break;
                    case ConsoleKey.D2:
                        SortCallsMenu(calls);
                        break;
                    case ConsoleKey.D3:
                        break;
                    case ConsoleKey.D4:
                        break;
                    case ConsoleKey.D5:
                        
                        break;
                    case ConsoleKey.D6:
                        break;
                    case ConsoleKey.D0:
                        return;
                }
            }
        }

        static void DirectoriesMenu(List<string> specializations, List<string> actions)
        {
            var specializationsName = "Специализации врачей бригад скорой помощи";
            var actionsName = "Действия бригад скорой помощи";

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Работа со справочниками");
                Console.WriteLine($"1) Справочник \"{specializationsName}\"");
                Console.WriteLine($"2) Справочник \"{actionsName}\"");
                Console.WriteLine("0) Вернуться назад");
                Console.Write(">>> ");

                var option = Console.ReadKey().Key;

                switch (option)
                {
                    case ConsoleKey.D1:
                        DirectoryMenu(specializations, specializationsName, specializationsFilePath);
                        break;
                    case ConsoleKey.D2:
                        DirectoryMenu(actions, actionsName, actionsFilePath);
                        break;
                    case ConsoleKey.D0:
                        return;
                }
            }
        }

        static void DirectoryMenu(List<string> directory, string directoryName, string directoryFilePath)
        {
            while (true)
            {
                Console.Clear();
                PrintDirectory(directory, directoryName);
                Console.WriteLine();
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1) Добавить запись");
                Console.WriteLine("2) Изменить запись");
                Console.WriteLine("3) Удалить запись");
                Console.WriteLine("0) Вернуться назад");
                Console.Write(">>> ");

                var option = Console.ReadKey().Key;

                switch (option)
                {
                    case ConsoleKey.D1:
                        AddEntryToDirectory(directory, directoryName, directoryFilePath);
                        break;
                    case ConsoleKey.D2:
                        ModifyEntryInDirectory(directory, directoryName, directoryFilePath);
                        break;
                    case ConsoleKey.D3:
                        DeleteEntryFromDirectory(directory, directoryName, directoryFilePath);
                        break;
                    case ConsoleKey.D0:
                        return;
                }
            }
        }
        static void AddEntryToDirectory(List<string> directory, string directoryName, string directoryFilePath)
        {
            Console.Clear();
            Console.WriteLine("Добавление записи");
            Console.WriteLine();
            PrintDirectory(directory, directoryName);
            Console.WriteLine();
            Console.WriteLine("Введите запись (или 0, если нужно вернуться назад)");
            Console.Write(">>> ");
            var entry = Console.ReadLine();

            if (entry == "0")
            {
                return;
            }

            directory.Add(entry);
            SaveDirectory(directory, directoryFilePath);
        }
        static void ModifyEntryInDirectory(List<string> directory, string directoryName, string directoryFilePath)
        {
            Console.Clear();
            Console.WriteLine("Изменение записи");
            Console.WriteLine();            
            PrintDirectory(directory, directoryName);
            Console.WriteLine();
            Console.WriteLine("Введите номер записи (или 0, если нужно вернуться назад)");
            Console.Write(">>> ");

            int index = InputEntryIndex(directory.Count);

            if (index == -1)
            {
                return;
            }

            Console.WriteLine("Введите запись (или 0, если нужно вернуться назад)");
            Console.Write(">>> ");
            var entry = Console.ReadLine();

            if (entry == "0")
            {
                return;
            }

            directory[index] = entry;
            SaveDirectory(directory, directoryFilePath);
        }
        static void DeleteEntryFromDirectory(List<string> directory, string directoryName, string directoryFilePath)
        {
            Console.Clear();
            Console.WriteLine("Удаление записи");
            Console.WriteLine();
            PrintDirectory(directory, directoryName);
            Console.WriteLine();
            Console.WriteLine("Введите номер записи (или 0, если нужно вернуться назад)");
            Console.Write(">>> ");

            int index = InputEntryIndex(directory.Count);

            if (index == -1)
            {
                return;
            }

            directory.RemoveAt(index);
            SaveDirectory(directory, directoryFilePath);
        }
        static Call InputCall()
        {
            Call call = new Call();

            Console.Write("Введите номер бригады (или 0, если нужно вернуться назад)");
            call.BrigadeNumber = InputBrigadeNumber();

            Console.Write("Выберите специализацию бригады (или введите 0, если нужно вернуться назад)");
            call.BrigadeSpecialization = InputBrigadeSpecialization();

            Console.Write("Введите дату вызова в формате ДД.ММ.ГГГГ (или 0, если нужно вернуться назад)");
            call.CallDate = InputCallDate();

            Console.Write("Введите адрес (или 0, если нужно вернуться назад)");
            call.Address = Console.ReadLine();

            Console.Write("Введите ФИО пациента (или 0, если нужно вернуться назад)");
            call.PatientFullName = Console.ReadLine();

            Console.Write("Введите дату рождения пациента в формате ДД.ММ.ГГГГ (или 0, если нужно вернуться назад)");
            call.PatientBirthDate = DateTime.Parse(Console.ReadLine());

            Console.Write("Введите пол пациента: ");
            call.PatientSex = Console.ReadLine();

            Console.Write("Введите действия: ");
            call.BrigadeActions = new List<string>();

            return call;
        }
        static DateTime InputCallDate()
        {
            DateTime callDate;

            while (!DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", 
                CultureInfo.InvariantCulture, DateTimeStyles.None, out callDate))
            {
                Console.WriteLine("Неверный ввод, попробуйте заново!");
                Console.Write(">>> ");
            }

            return callDate;
        }
        static string InputBrigadeSpecialization()
        {
            return Console.ReadLine();
        }
        static int InputBrigadeNumber()
        {
            int number;

            while (!(int.TryParse(Console.ReadLine(), out number) && number >= 0))
            {
                Console.WriteLine("Неверный ввод, попробуйте заново!");
                Console.Write(">>> ");
            }

            return number;
        }
        static int InputEntryIndex(int max)
        {
            int index;
            while (!(int.TryParse(Console.ReadLine(), out index) &&
                   index >= 0 && index <= max))
            {
                Console.WriteLine("Неверный ввод, попробуйте заново!");
                Console.Write(">>> ");
            }
            index--;

            return index;
        }
        static void AddCall(List<Call> calls, Call call)
        {
            calls.Add(call);
        }
        static void ModifyCall(List<Call> calls, int index, Call call)
        {
            calls[index] = call;
        }
        static void DeleteCall(List<Call> calls, int index)
        {
            calls.RemoveAt(index);
        }
        static void SortCallsBySpecialization(List<Call> calls)
        {
            calls.Sort((firstCall, secondCall) => string.Compare(firstCall.
                BrigadeSpecialization, secondCall.BrigadeSpecialization));
        }
        static void SortCallsByAddress(List<Call> calls)
        {
            calls.Sort((firstCall, secondCall) => string.Compare(firstCall.
                Address, secondCall.Address));
        }
        static void SortCallsByPatientFullName(List<Call> calls)
        {
            calls.Sort((firstCall, secondCall) => string.Compare(firstCall.
                PatientFullName, secondCall.PatientFullName));
        }
        static void SortCallsByPatientSex(List<Call> calls)
        {
            calls.Sort((firstCall, secondCall) => string.Compare(firstCall.
                PatientSex, secondCall.PatientSex));
        }
        static void SortCallsByPatientBirthDate(List<Call> calls)
        {
            calls.Sort((firstCall, secondCall) => DateTime.Compare(firstCall.
                PatientBirthDate, secondCall.PatientBirthDate));
        }
        static void DocumentsMenu()
        {

        }
        static void PrintCalls(List<Call> calls)
        {
            foreach (Call call in calls)
            {
                PrintCall(call);
                Console.WriteLine();
            }
        }
        static void PrintCall(Call call)
        {
            string text = CallToLine(call);

            Console.WriteLine(text);
        }
        static string CallToLine(Call call)
        {
            return $"{call.BrigadeNumber};{call.BrigadeSpecialization};{call.CallDate:dd.MM.yyyy};{call.CallTime:HH:mm};{call.Address};{call.PatientFullName};{call.PatientBirthDate};{call.PatientSex};{string.Join(", ", call.BrigadeActions)}";
        }
        static void PrintDirectory(List<string> directory, string directoryName)
        {
            Console.WriteLine($"Справочник \"{directoryName}\"");
            Console.WriteLine();

            if (directory.Count == 0)
            {
                Console.WriteLine("Справочник пустой");
                return;
            }

            for (var i = 0; i < directory.Count; i++)
            {
                Console.WriteLine($"{i + 1}) {directory[i]}");
            }
        }

        static void MainMenu()
        {
            var calls = LoadCalls();
            var specializations = LoadDirectory(specializationsFilePath);
            var actions = LoadDirectory(actionsFilePath);

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Главное меню");
                Console.WriteLine("1) Работа с вызовами");
                Console.WriteLine("2) Работа со справочниками");
                Console.WriteLine("3) Создание отчетов");
                Console.WriteLine("0) Выйти из программы");
                Console.Write(">>> ");

                var option = Console.ReadKey().Key;
                var stop = false;

                switch (option)
                {
                    case ConsoleKey.D1:
                        CallsMenu(calls);
                        break;
                    case ConsoleKey.D2:
                        DirectoriesMenu(specializations, actions);
                        break;
                    case ConsoleKey.D3:
                        DocumentsMenu();
                        break;
                    case ConsoleKey.D0:
                        stop = true;
                        break;
                }

                if (stop)
                {
                    break;
                }
            }
        }
    }

    struct Call
    {
        public int BrigadeNumber;
        public string BrigadeSpecialization;
        public DateTime CallDate;
        public DateTime CallTime;
        public string Address;
        public string PatientFullName;
        public DateTime PatientBirthDate;
        public string PatientSex;
        public List<string> BrigadeActions;
    }
}
