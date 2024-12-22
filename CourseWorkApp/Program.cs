using System.Reflection.Metadata;

namespace CourseWorkApp
{
    internal class Program
    {
        const string AmbulanceCallsFilePath = "ambulance-calls.txt";
        const string SpecializationsFilePath = "specializations-of-brigades.txt";
        const string ActionsFilePath = "actions-of-brigades.txt";
        static void Main(string[] args)
        {
            MainMenu();
        }

        static List<Call> LoadAmbulanceCalls()
        {
            var allCallsLines = File.ReadAllLines(AmbulanceCallsFilePath);
            List<Call> calls = new List<Call>();

            foreach (var callLine in allCallsLines)
            {
                var splitCallLine = callLine.Split(";");

                Call call;
                call.BrigadeNumber = int.Parse(splitCallLine[0]);
                call.BrigadeSpecialization = splitCallLine[1];
                call.CallDate = DateTime.Parse(splitCallLine[2]);
                call.CallTime = TimeSpan.Parse(splitCallLine[3]);
                call.Address = splitCallLine[4];
                call.PatientFullName = splitCallLine[5];
                call.PatientBirthDate = DateTime.Parse(splitCallLine[6]);
                call.PatientSex = splitCallLine[7];
                call.BrigadeActions = splitCallLine[8];

                calls.Add(call);
            }

            return calls;
        }

        static List<string> LoadSpecializations()
        {
            var specializations = File.ReadAllLines(SpecializationsFilePath).ToList();

            return specializations;
        }

        static List<string> LoadActions()
        {
            var actions = File.ReadAllLines(ActionsFilePath).ToList();

            return actions;
        }

        static void SaveSpecializationsOfBrigades(List<string> specializations)
        {
            using (StreamWriter sw = new StreamWriter(SpecializationsFilePath))
            {
                foreach (var specialization in specializations)
                {
                    sw.WriteLine(specialization);
                }
            }
        }

        static void SaveActionsOfBrigades(List<string> actions)
        {
            using (StreamWriter sw = new StreamWriter(SpecializationsFilePath))
            {
                foreach (var action in actions)
                {
                    sw.WriteLine(action); // ?
                }
            }
        }

        static void SortAmbulanceCallsMenu(List<Call> calls)
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
            }
        }

        static void CallsMenu(List<Call> calls)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1) Просмотреть вызовы");
                Console.WriteLine("2) Добавить вызов");
                Console.WriteLine("3) Удалить вызов");
                Console.WriteLine("4) Сортировка вызовов");
                Console.WriteLine("5) Поиск вызовов");
                Console.WriteLine("0) Вернуться назад");
                Console.Write(">>> ");

                var option = Console.ReadKey().Key;
                var stop = false;

                switch (option)
                {
                    case ConsoleKey.D1:
                        break;
                    case ConsoleKey.D2:
                        break;
                    case ConsoleKey.D3:
                        break;
                    case ConsoleKey.D4:
                        SortAmbulanceCallsMenu(calls);
                        break;
                    case ConsoleKey.D5:
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

        static void DirectoriesMenu(List<string> specializations, List<string> actions)
        {
            var specializationsName = "Специализации врачей бригад скорой помощи";
            var actionsName = "Действия бригад скорой помощи";

            while (true)
            {
                Console.Clear();
                Console.WriteLine($"1) Справочник \"{specializationsName}\"");
                Console.WriteLine($"2) Справочник \"{actionsName}\"");
                Console.WriteLine("0) Вернуться назад");
                Console.Write(">>> ");

                var option = Console.ReadKey().Key;
                var stop = false;

                switch (option)
                {
                    case ConsoleKey.D1:
                        DirectoryMenu(specializations, specializationsName);
                        break;
                    case ConsoleKey.D2:
                        DirectoryMenu(actions, actionsName);
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

        static void DirectoryMenu(List<string> directory, string directoryName)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Справочник \"{directoryName}\"");
                Console.WriteLine();
                PrintDirectory(directory);
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1) Добавить запись");
                Console.WriteLine("2) Изменить запись");
                Console.WriteLine("3) Удалить запись");
                Console.WriteLine("0) Вернуться назад");
                Console.Write(">>> ");

                var option = Console.ReadKey().Key;
                var stop = false;

                switch (option)
                {
                    case ConsoleKey.D1:
                        break;
                    case ConsoleKey.D2:
                        break;
                    case ConsoleKey.D3:
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
        void AddEntryToDirectoryMenu(List<string> directory)
        {
            Console.Clear();
        }
        void AddEntryToDirectory(List<string> directory, string entry)
        {
            directory.Add(entry);
        }
        void DeleteEntryFromDirectory(List<string> directory, int index)
        {
            directory.RemoveAt(index);
        }
        void ModifyEntryInDirectory(List<string> directory, int index, string entry)
        {
            directory[index] = entry;
        }
        static void DocumentsMenu()
        {

        }

        static void PrintDirectory(List<string> directory)
        {
            for (var i = 0; i < directory.Count; i++)
            {
                Console.WriteLine($"{i + 1}) {directory[i]}");
            }
        }

        static void MainMenu()
        {
            var calls = new List<Call>();
            var specializations = new List<string>();
            var actions = new List<string>();
            /*var calls = LoadAmbulanceCalls();
            var specializaions = LoadSpecializations();
            var actions = LoadActions();*/

            while (true)
            {
                Console.Clear();
                Console.WriteLine("1) Работа с вызовами");
                Console.WriteLine("2) Работа со справочниками");
                Console.WriteLine("3) Создание документов");
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
        public TimeSpan CallTime;
        public string Address;
        public string PatientFullName;
        public DateTime PatientBirthDate;
        public string PatientSex;
        public string BrigadeActions;
    }
}
