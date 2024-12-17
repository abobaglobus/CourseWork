namespace CourseWorkApp
{
    internal class Program
    {
        const string AmbulanceCallsFilePath = "ambulance-calls.txt";
        const string SpecializationsOfBrigadesFilePath = "specializations-of-brigades.txt";
        const string ActionsOfBrigadesFilePath = "actions-of-brigades.txt";
        static void Main(string[] args)
        {
            MainMenu();
        }

        static List<AmbulanceCall> LoadAmbulanceCalls()
        {
            var allAmbulanceCallsLines = File.ReadAllLines(AmbulanceCallsFilePath);
            List<AmbulanceCall> ambulanceCalls = new List<AmbulanceCall>();

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

                ambulanceCalls.Add(ambulanceCall);
            }

            return ambulanceCalls;
        }

        static List<string> LoadSpecializationsOfBrigades()
        {
            var specializationsOfBrigades = File.ReadAllLines(SpecializationsOfBrigadesFilePath).ToList();

            return specializationsOfBrigades;
        }

        static List<string> LoadActionsOfBrigades()
        {
            var actionsOfBrigades = File.ReadAllLines(ActionsOfBrigadesFilePath).ToList();

            return actionsOfBrigades;
        }

        static void SaveSpecializationsOfBrigades(List<string> specializationsOfBrigades)
        {
            using (StreamWriter sw = new StreamWriter(SpecializationsOfBrigadesFilePath))
            {
                foreach (var specializationOfBrigades in specializationsOfBrigades)
                {
                    sw.WriteLine(specializationOfBrigades);
                }
            }
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
