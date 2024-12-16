namespace CourseWorkApp
{
    internal class Program
    {
        static string AmbulanceCallsFilePath = "ambulance-calls.txt";
        static string SpecializationsOfBrigadesFilePath = "specializations-of-brigades.txt";
        static string ActionsOfBrigadesFilePath = "actions-of-brigades.txt";
        static List<AmbulanceCall> AmbulanceCalls = new List<AmbulanceCall>();
        static List<string> SpecializationsOfBrigades = new List<string>();
        static List<string> ActionsOfBrigades = new List<string>();
        static void Main(string[] args)
        {
            MainMenu();
        }

        static void LoadAmbulanceCalls()
        {
            var allAmbulanceCallsLines = File.ReadAllLines(AmbulanceCallsFilePath);

            foreach (var ambulanceCallLine in allAmbulanceCallsLines)
            {
                var splitAmbulanceCallLine = ambulanceCallLine.Split(";");

                AmbulanceCall ambulanceCall;
                ambulanceCall.BrigadeNumber = int.Parse(splitAmbulanceCallLine[0]);
                ambulanceCall.BrigadeSpecialization = splitAmbulanceCallLine[1];
                ambulanceCall.CallDate = DateTime.Parse(splitAmbulanceCallLine[2]);
                ambulanceCall.CallTime = TimeSpan.Parse(splitAmbulanceCallLine[3]);
                ambulanceCall.Address = splitAmbulanceCallLine[4];
                ambulanceCall.PatientFullName = splitAmbulanceCallLine[5];
                ambulanceCall.PatientBirthDate = DateTime.Parse(splitAmbulanceCallLine[6]);
                ambulanceCall.PatientSex = splitAmbulanceCallLine[7];
                ambulanceCall.BrigadeActions = splitAmbulanceCallLine[8];

                AmbulanceCalls.Add(ambulanceCall);
            }
        }

        static void LoadSpecializationsOfBrigades()
        {
            SpecializationsOfBrigades.AddRange(File.ReadAllLines(SpecializationsOfBrigadesFilePath));
        }

        static void LoadActionsOfBrigades()
        {
            ActionsOfBrigades.AddRange(File.ReadAllLines(ActionsOfBrigadesFilePath));
        }

        static void CallsMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. Просмотреть вызовы");
                Console.WriteLine("2. Добавление вызова");
                Console.WriteLine("3. Удаление вызова");
                Console.WriteLine("4. Сортировка вызовов");
                Console.WriteLine("5. Поиск вызовов");
                Console.WriteLine("0. Назад");
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

        static void DirectoriesMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. Действия бригады");
                Console.WriteLine("2. Специализация врачей бригады");
                Console.WriteLine("0. Назад");
                Console.Write(">>> ");

                var option = Console.ReadKey().Key;
                var stop = false;

                switch (option)
                {
                    case ConsoleKey.D1:
                        break;
                    case ConsoleKey.D2:
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

        static void PrintActionsOfBrigades()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. Вызовы");
                Console.WriteLine("2. Справочники");
                Console.WriteLine("3. Обработка данных");
                Console.WriteLine("0. Назад");
                Console.Write(">>> ");

                var option = Console.ReadKey().Key;
                var stop = false;

                switch (option)
                {
                    case ConsoleKey.D1:
                        CallsMenu();
                        break;
                    case ConsoleKey.D2:
                        DirectoriesMenu();
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

        static void MainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. Вызовы");
                Console.WriteLine("2. Справочники");
                Console.WriteLine("3. Обработка данных");
                Console.WriteLine("0. Выход");
                Console.Write(">>> ");

                var option = Console.ReadKey().Key;
                var stop = false;

                switch (option)
                {
                    case ConsoleKey.D1:
                        CallsMenu();
                        break;
                    case ConsoleKey.D2:
                        DirectoriesMenu();
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
    }

    struct AmbulanceCall
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
