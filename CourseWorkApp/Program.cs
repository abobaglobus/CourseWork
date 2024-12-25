using System.Globalization;
using System.IO;

namespace CourseWorkApp
{
    public class Program
    {
        public static string CallsFilePath = "calls.txt";
        public static string SpecializationsFilePath = "specializations.txt";
        public static string ActionsFilePath = "actions.txt";
        public static string BrigadeCallDateReportFilePath = "brigade-call_date-report.txt";
        public static string ActionCountReportFilePath = "action-count-report.txt";
        public static string AverageAgeReportFilePath = "average_age-report.txt";
        public static List<Call> Calls = new List<Call>();
        public static List<string> Specializations = new List<string>();
        public static List<string> Actions = new List<string>();
        public static string SpecializationsName = "Специализации бригад скорой помощи";
        public static string ActionsName = "Действия бригад скорой помощи";
        static void Main(string[] args)
        {
            MainMenu();
        }
        static void LoadCalls()
        {
            var lines = File.ReadAllLines(CallsFilePath);
            foreach (var line in lines)
            {
                var fields = line.Split(";");
                Call call = new Call
                {
                    BrigadeNumber = int.Parse(fields[0]),
                    BrigadeSpecialization = fields[1],
                    CallDate = DateTime.ParseExact(fields[2], "dd.MM.yyyy",
                    CultureInfo.InvariantCulture),
                    CallTime = DateTime.ParseExact(fields[3], "HH:mm",
                    CultureInfo.InvariantCulture),
                    Address = fields[4],
                    PatientFullName = fields[5],
                    PatientBirthDate = DateTime.ParseExact(fields[6], "dd.MM.yyyy",
                    CultureInfo.InvariantCulture),
                    PatientSex = fields[7],
                    BrigadeActions = fields[8]
                };
                Calls.Add(call);
            }
        }
        static void LoadSpecializations()
        {
            Specializations.AddRange(File.ReadAllLines(SpecializationsFilePath));
        }
        static void LoadActions()
        {
            Actions.AddRange(File.ReadAllLines(ActionsFilePath));
        }
        static void SaveCalls()
        {
            var lines = new List<string>();
            foreach (var call in Calls)
            {
                lines.Add(CallToLine(call));
            }
            File.WriteAllLines(CallsFilePath, lines);
        }
        static void SaveDirectory(List<string> directory, string directoryFilePath)
        {
            File.WriteAllLines(directoryFilePath, directory);
        }
        static void SortCallsMenu()
        {
            ClearConsole();
            Console.WriteLine("Сортировка вызовов");
            Console.WriteLine();
            PrintCalls(Calls);
            Console.WriteLine();
            Console.WriteLine("Введите через запятую номера полей для сортировки" +
                " (пример: 1,4,5). Убедитесь, что ввод содержит только цифры и " +
                "запятые, а между запятыми целые числа 1-5.");
            Console.WriteLine("1) Специализация бригады");
            Console.WriteLine("2) Адрес");
            Console.WriteLine("3) ФИО пациента");
            Console.WriteLine("4) Пол");
            Console.WriteLine("5) Возраст");
            Console.Write(">>> ");
            var sortingOptionsLine = Console.ReadLine();
            while (!sortingOptionsLine.Split(",").All(x => int.TryParse(x, out _) &&
                   int.Parse(x) >= 1 && int.Parse(x) <= 5))
            {
                Console.WriteLine("Неверный ввод, попробуйте заново!");
                Console.Write(">>> ");
                sortingOptionsLine = Console.ReadLine();
            }
            var sortingOptions = sortingOptionsLine.Split(",");
            foreach (var sortingOption in sortingOptions)
            {
                switch (sortingOption)
                {
                    case "1":
                        SortCallsByBrigadeSpecialization();
                        break;
                    case "2":
                        SortCallsByAddress();
                        break;
                    case "3":
                        SortCallsByPatientFullName();
                        break;
                    case "4":
                        SortCallsByPatientSex();
                        break;
                    case "5":
                        SortCallsByPatientBirthDate();
                        break;
                }
            }
            SaveCalls();
        }

        static void CallsMenu()
        {
            while (true)
            {
                ClearConsole();
                Console.WriteLine("Работа с вызовами");
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
                        ViewCallsMenu();
                        break;
                    case ConsoleKey.D2:
                        SortCallsMenu();
                        break;
                    case ConsoleKey.D3:
                        AddCallMenu();
                        break;
                    case ConsoleKey.D4:
                        ModifyCallMenu();
                        break;
                    case ConsoleKey.D5:
                        DeleteCallMenu();
                        break;
                    case ConsoleKey.D6:
                        SearchCallsMenu();
                        break;
                    case ConsoleKey.D0:
                        return;
                }
            }
        }
        static void ViewCallsMenu()
        {
            ClearConsole();
            Console.WriteLine("Просмотр вызовов\n");
            PrintCalls(Calls);
            Console.WriteLine("\nЧтобы закрыть список вызовов, нажмите любую клавишу.");
            Console.ReadKey();
        }
        static void AddCallMenu()
        {
            ClearConsole();
            Console.WriteLine("Добавление вызова\n");
            PrintCalls(Calls);
            Console.WriteLine();
            var call = InputCall();
            Calls.Add(call);
            SaveCalls();
        }
        static void ModifyCallMenu()
        {
            ClearConsole();
            Console.WriteLine("Изменение вызова\n");
            PrintCalls(Calls);
            Console.WriteLine();
            var index = InputIndex(Calls.Count);
            Console.WriteLine();
            PrintCall(Calls[index]);
            Console.WriteLine("\nДанные для изменения:");
            Console.WriteLine("1) Номер бригады");
            Console.WriteLine("2) Специализация бригады");
            Console.WriteLine("3) Дата вызова");
            Console.WriteLine("4) Время вызова");
            Console.WriteLine("5) Адрес");
            Console.WriteLine("6) ФИО пациента");
            Console.WriteLine("7) Дата рождения пациента");
            Console.WriteLine("8) Пол пациента");
            Console.WriteLine("9) Действия бригады");
            Console.Write(">>> ");
            var option = Console.ReadKey();
        }
        static void DeleteCallMenu()
        {
            ClearConsole();
            Console.WriteLine("Удаление вызова");
            Console.WriteLine();
            PrintCalls(Calls);
            Console.WriteLine();
            int index = InputIndex(Calls.Count);
            Calls.RemoveAt(index);
            SaveCalls();
        }
        static void SearchCallsMenu()
        {
            ClearConsole();
            Console.WriteLine("Поиск вызовов\n");
            PrintCalls(Calls);
            Console.WriteLine("\nВведите через запятую номера полей для поиска" +
                " (пример: 1,3). Убедитесь, что ввод содержит только цифры и " +
                "запятые, а между запятыми целые числа 1-3.");
            Console.WriteLine("1) Специализация бригады");
            Console.WriteLine("2) Номер бригады");
            Console.WriteLine("3) Дата и время вызова");
            Console.Write(">>> ");
            var searchOptionsLine = Console.ReadLine();
            while (!searchOptionsLine.Split(",").All(x => int.TryParse(x, out _) &&
                   int.Parse(x) >= 1 && int.Parse(x) <= 3))
            {
                Console.WriteLine("Неверный ввод, попробуйте заново!\n>>> ");
                searchOptionsLine = Console.ReadLine();
            }
            Console.WriteLine();
            var foundCalls = new List<Call>(Calls);
            var searchOptions = searchOptionsLine.Split(",");
            foreach (var searchOption in searchOptions)
            {
                switch (searchOption)
                {
                    case "1":
                        foundCalls = SearchCallsBySpecialization(foundCalls);
                        break;
                    case "2":
                        foundCalls = SearchCallsByBrigadeNumber(foundCalls);
                        break;
                    case "3":
                        foundCalls = SearchCallsByCallDateAndTime(foundCalls);
                        break;
                }
            }
            PrintCalls(foundCalls);
            Console.ReadKey();
        }
        static List<Call> SearchCallsBySpecialization(List<Call> calls)
        {
            var specialization = ChooseBrigadeSpecialization();
            return calls.FindAll(call => call.BrigadeSpecialization == specialization);
        }
        static List<Call> SearchCallsByBrigadeNumber(List<Call> calls)
        {
            var brigadeNumber = InputBrigadeNumber();
            return calls.FindAll(call => call.BrigadeNumber == brigadeNumber);
        }
        static List<Call> SearchCallsByCallDateAndTime(List<Call> calls)
        {
            Console.WriteLine("Введите начало периода поиска");
            var start = InputCallDate().Date.Add(InputCallTime().TimeOfDay);
            Console.WriteLine("Введите конец периода поиска");
            var end = InputCallDate().Date.Add(InputCallTime().TimeOfDay);
            return calls.FindAll(call => call.CallDate.Date.Add(call.CallTime.TimeOfDay)
                   >= start && call.CallDate.Date.Add(call.CallTime.TimeOfDay) <= end);
        }

        static void DirectoriesMenu()
        {
            while (true)
            {
                ClearConsole();
                Console.WriteLine("Работа со справочниками");
                Console.WriteLine($"1) Справочник \"{SpecializationsName}\"");
                Console.WriteLine($"2) Справочник \"{ActionsName}\"");
                Console.WriteLine("0) Вернуться назад");
                Console.Write(">>> ");
                var option = Console.ReadKey().Key;
                switch (option)
                {
                    case ConsoleKey.D1:
                        SpecializationsMenu();
                        break;
                    case ConsoleKey.D2:
                        ActionsMenu();
                        break;
                    case ConsoleKey.D0:
                        return;
                }
            }
        }

        static void SpecializationsMenu()
        {
            DirectoryMenu(Specializations, SpecializationsName, SpecializationsFilePath);
        }
        static void ActionsMenu()
        {
            DirectoryMenu(Actions, ActionsName, ActionsFilePath);
        }

        static void DirectoryMenu(List<string> directory, string directoryName, string directoryFilePath)
        {
            while (true)
            {
                ClearConsole();
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
                        AddEntryToDirectoryMenu(directory, directoryName, directoryFilePath);
                        break;
                    case ConsoleKey.D2:
                        ModifyEntryInDirectoryMenu(directory, directoryName, directoryFilePath);
                        break;
                    case ConsoleKey.D3:
                        DeleteEntryFromDirectoryMenu(directory, directoryName, directoryFilePath);
                        break;
                    case ConsoleKey.D0:
                        return;
                }
            }
        }
        static void PrintSpecializations()
        {
            PrintDirectory(Specializations, SpecializationsName);
        }
        static void PrintActions()
        {
            PrintDirectory(Actions, ActionsName);
        }
        static void AddEntryToDirectoryMenu(List<string> directory, string directoryName, string directoryFilePath)
        {
            ClearConsole();
            Console.WriteLine("Добавление записи\n");
            PrintDirectory(directory, directoryName);
            Console.Write("\nВведите запись\n>>> ");
            var entry = Console.ReadLine();
            directory.Add(entry);
            SaveDirectory(directory, directoryFilePath);
        }
        static void ModifyEntryInDirectoryMenu(List<string> directory, string directoryName, string directoryFilePath)
        {
            ClearConsole();
            Console.WriteLine("Изменение записи\n");  
            PrintDirectory(directory, directoryName);
            Console.Write("\nВведите номер записи\n>>> ");
            var index = InputIndex(directory.Count);
            Console.Write("Введите запись\n>>> ");
            var entry = Console.ReadLine();
            directory[index] = entry;
            SaveDirectory(directory, directoryFilePath);
        }
        static void DeleteEntryFromDirectoryMenu(List<string> directory, string directoryName, string directoryFilePath)
        {
            ClearConsole();
            Console.WriteLine("Удаление записи\n");
            PrintDirectory(directory, directoryName);
            Console.WriteLine();
            int index = InputIndex(directory.Count);
            directory.RemoveAt(index);
            SaveDirectory(directory, directoryFilePath);
        }
        static Call InputCall()
        {
            Call call = new Call();

            call.BrigadeNumber = InputBrigadeNumber();
            Console.WriteLine();
            call.BrigadeSpecialization = ChooseBrigadeSpecialization();
            Console.WriteLine();
            call.CallDate = InputCallDate();
            Console.WriteLine();
            call.CallTime = InputCallTime();
            Console.WriteLine();
            call.Address = InputAddress();
            Console.WriteLine();
            call.PatientFullName = InputPatientFullName();
            Console.WriteLine();
            call.PatientBirthDate = InputPatientBirthDate();
            Console.WriteLine();
            call.PatientSex = InputPatientSex();
            Console.WriteLine();
            call.BrigadeActions = InputBrigadeActions();

            return call;
        }
        static int InputSpecializationNumber()
        {
            int number;
            Console.Write("Введите номер специализации\n>>> ");
            while (!(int.TryParse(Console.ReadLine(), out number) && number > 0))
            {
                Console.Write("Неверный ввод, попробуйте заново!\n>>> ");
            }
            return number;
        }
        static int InputBrigadeNumber()
        {
            int number;
            Console.Write("Введите номер бригады\n>>> ");
            while (!(int.TryParse(Console.ReadLine(), out number) && number > 0))
            {
                Console.Write("Неверный ввод, попробуйте заново!\n>>> ");
            }
            return number;
        }
        static string ChooseBrigadeSpecialization()
        {
            Console.WriteLine("Выбор специализации бригады\n");
            PrintSpecializations();
            Console.WriteLine();
            var index = InputIndex(Specializations.Count);
            return Specializations[index];
        }
        static DateTime InputCallDate()
        {
            var minDate = DateTime.ParseExact($"01.01.{DateTime.Now.Year - 5}",
                "dd.MM.yyyy", CultureInfo.InvariantCulture).Date;
            var maxDate = DateTime.Now.Date;
            return InputDate("Введите дату вызова в формате ДД.ММ.ГГГГ", minDate, maxDate);
        }
        static DateTime InputCallTime()
        {
            DateTime callTime;

            Console.Write("Введите время вызова в формате ЧЧ:ММ\n>>> ");
            while (!DateTime.TryParseExact(Console.ReadLine(), "HH:mm",
                CultureInfo.InvariantCulture, DateTimeStyles.None, out callTime))
            {
                Console.Write("Неверный ввод, попробуйте заново!\n>>> ");
            }

            return callTime; 
        }
        static string InputAddress()
        {
            Console.Write("Введите адрес\n>>> ");
            return Console.ReadLine();
        }
        static string InputPatientFullName()
        {
            Console.Write("Введите ФИО пациента\n>>> ");
            return Console.ReadLine();
        }
        static DateTime InputPatientBirthDate()
        {
            var minDate = DateTime.ParseExact("01.01.1900",
                "dd.MM.yyyy", CultureInfo.InvariantCulture).Date;
            var maxDate = DateTime.Now.Date;
            return InputDate("Введите дату рождения пациента в формате" +
                "ДД.ММ.ГГГГ", minDate, maxDate);
        }
        static string InputPatientSex()
        {
            Console.Write("Введите пол пациента (М или Ж)\n>>> ");
            var patientSex = Console.ReadLine().ToUpper();
            while (patientSex != "М" && patientSex != "Ж")
            {
                Console.Write("Неверный ввод, попробуйте заново!\n>>> ");
                patientSex = Console.ReadLine().ToUpper();
            }
            return patientSex;
        }
        static string InputBrigadeActions()
        {
            Console.Write("Введите действия бригады через запятую и пробел\n>>> ");
            return Console.ReadLine();
        }
        static DateTime InputDate(string prompt, DateTime minDate, DateTime maxDate)
        {
            DateTime callDate;
            Console.Write($"{prompt}\n>>> ");
            while (!(DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy",
                CultureInfo.InvariantCulture, DateTimeStyles.None, out callDate) &&
                callDate >= minDate && callDate <= maxDate))
            {
                Console.Write("Неверный ввод, попробуйте заново!\n>>> ");
            }
            return callDate;
        }
        static int InputIndex(int max, string prompt)
        {
            int index;
            Console.Write("Введите номер\n>>> ");
            while (!(int.TryParse(Console.ReadLine(), out index) &&
                   index >= 1 && index <= max))
            {
                Console.Write("Неверный ввод, попробуйте заново!\n>>> ");
            }
            index--;
            return index;
        }
        static void SortCallsByBrigadeSpecialization()
        {
            Calls.Sort((firstCall, secondCall) => string.Compare(firstCall.
                BrigadeSpecialization, secondCall.BrigadeSpecialization));
        }
        public static void SortCallsByAddress()
        {
            Calls.Sort((firstCall, secondCall) => string.Compare(firstCall.
                Address, secondCall.Address));
        }
        public static void SortCallsByPatientFullName()
        {
            Calls.Sort((firstCall, secondCall) => string.Compare(firstCall.
                PatientFullName, secondCall.PatientFullName));
        }
        static void SortCallsByPatientSex()
        {
            Calls.Sort((firstCall, secondCall) => string.Compare(firstCall.
                PatientSex, secondCall.PatientSex));
        }
        static void SortCallsByPatientBirthDate()
        {
            Calls.Sort((firstCall, secondCall) => DateTime.Compare(firstCall.
                PatientBirthDate, secondCall.PatientBirthDate));
        }
        static void PrintCalls(List<Call> calls)
        {
            for (var i = 0; i < calls.Count - 1; i++)
            {
                Console.Write($"{i + 1}) ");
                PrintCall(calls[i]);
                Console.WriteLine();
            }
            Console.Write($"{calls.Count}) ");
            PrintCall(calls[calls.Count - 1]);
        }
        public static void PrintCall(Call call)
        {
            var totalWidth = 30;
            Console.WriteLine($"Бригада {call.BrigadeNumber}");
            Console.WriteLine($"{"Специализация бригады:".PadRight(totalWidth)}" +
                $"{call.BrigadeSpecialization}");
            Console.WriteLine($"{"Дата вызова:".PadRight(totalWidth)}" +
                $"{call.CallDate:dd.MM.yyyy}");
            Console.WriteLine($"{"Время вызова:".PadRight(totalWidth)}" +
                $"{call.CallTime:HH:mm}");
            Console.WriteLine($"{"Адрес:".PadRight(totalWidth)}{call.Address}");
            Console.WriteLine($"{"ФИО пациента:".PadRight(totalWidth)}" +
                $"{call.PatientFullName}");
            Console.WriteLine($"{"Дата рождения:".PadRight(totalWidth)}" +
                $"{call.PatientBirthDate:dd.MM.yyyy}");
            Console.WriteLine($"{"Пол:".PadRight(totalWidth)}" +
                $"{call.PatientSex}");
            Console.WriteLine($"{"Действия:".PadRight(totalWidth)}" +
                $"{string.Join(", ", call.BrigadeActions)}");
        }
        static string CallToLine(Call call)
        {
            return $"{call.BrigadeNumber};{call.BrigadeSpecialization};" +
                $"{call.CallDate:dd.MM.yyyy};{call.CallTime:HH:mm};{call.Address};" +
                $"{call.PatientFullName};{call.PatientBirthDate:dd.MM.yyyy};" +
                $"{call.PatientSex};{call.BrigadeActions}";
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
        static void ReportsMenu()
        {
            ClearConsole();
            Console.WriteLine("Работа с отчетами");
            Console.WriteLine("1) Сгенерировать отчет по бригадам и вызовам");
            Console.WriteLine("2) Сгенерировать отчет по действиям и количеству");
            Console.WriteLine("3) Сгенерировать отчет по среднему возрасту");
            Console.WriteLine("0) Вернуться назад");
            Console.Write(">>> ");

            var option = Console.ReadKey().Key;

            switch (option)
            {
                case ConsoleKey.D1:
                    GenerateBrigadeCallsReport();
                    break;
                case ConsoleKey.D2:
                    GenerateActionCountReportMenu();
                    break;
                case ConsoleKey.D3:
                    GenerateAverageAgeReport();
                    break;
                case ConsoleKey.D0:
                    return;
            }
        }

        static void GenerateBrigadeCallsReport()
        {
            var report = new Dictionary<(int, string), Dictionary<DateTime, int>>();

            foreach (var call in Calls)
            {
                var key = (call.BrigadeNumber, call.BrigadeSpecialization);
                if (!report.ContainsKey(key))
                    report[key] = new Dictionary<DateTime, int>();

                if (!report[key].ContainsKey(call.CallDate))
                    report[key][call.CallDate] = 0;

                report[key][call.CallDate]++;
            }

            foreach (var brigade in report)
            {
                Console.WriteLine($"Бригада №{brigade.Key.Item1}, Специализация: {brigade.Key.Item2}");
                int totalCalls = 0;

                foreach (var date in brigade.Value)
                {
                    Console.WriteLine($"Дата: {date.Key.ToShortDateString()}, Количество вызовов: {date.Value}");
                    totalCalls += date.Value;
                }

                Console.WriteLine($"Итого: {totalCalls}");
                Console.WriteLine(new string('-', 50));
            }

            Console.ReadKey();
        }
        static void GenerateActionCountReportMenu()
        {

        }
        static void GenerateAverageAgeReport()
        {

        }

        static void MainMenu()
        {
            LoadCalls();
            LoadSpecializations();
            LoadActions();
            while (true)
            {
                ClearConsole();
                Console.WriteLine("Главное меню");
                Console.WriteLine("1) Работа с вызовами");
                Console.WriteLine("2) Работа со справочниками");
                Console.WriteLine("3) Работа с отчетами");
                Console.WriteLine("0) Выйти из программы");
                Console.Write(">>> ");
                var option = Console.ReadKey().Key;
                switch (option)
                {
                    case ConsoleKey.D1:
                        CallsMenu();
                        break;
                    case ConsoleKey.D2:
                        DirectoriesMenu();
                        break;
                    case ConsoleKey.D3:
                        ReportsMenu();
                        break;
                    case ConsoleKey.D0:
                        return;
                }
            }
        }

        static void ClearConsole()
        {
            Console.Clear();
            Console.Write("\x1b[3J");
        }
    }

    public struct Call
    {
        public int BrigadeNumber;
        public string BrigadeSpecialization;
        public DateTime CallDate;
        public DateTime CallTime;
        public string Address;
        public string PatientFullName;
        public DateTime PatientBirthDate;
        public string PatientSex;
        public string BrigadeActions;
    }
}
