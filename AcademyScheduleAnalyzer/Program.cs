using System.Text;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Academy_Schedule_Analyzer_Assignment
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0 && args[0] == "--benchmark")
            {
                BenchmarkDotNet.Running.BenchmarkRunner.Run<Benchmarks.StringBenchmark>();
                return;
            }
            RunMenu();
        }
        static string[] sessionNames =
            {
            "C# Basics",
            "Arrays",
            "Functions",
            "Date and Time",
            "Exception Handling"
            };
        static DateTime[] sessionDates =
        {
            new DateTime(2026, 9, 10, 18, 0, 0),
            new DateTime(2026, 9, 13, 18, 0, 0),
            new DateTime(2026, 9, 17, 18, 0, 0),
            new DateTime(2026, 9, 20, 18, 0, 0),
            new DateTime(2026, 9, 27, 18, 0, 0)
            };
        static int[] sessionDurations =
        {
            180,
            240,
            180,
            240,
            180
            };

        static void DisplayMenu()
        {
            Console.WriteLine("===================================");
            Console.WriteLine("Academy Schedule Analyzer");
            Console.WriteLine("===================================\n");
            Console.WriteLine("1. Display all sessions");
            Console.WriteLine("2. Search for a session");
            Console.WriteLine("3. Sort session names");
            Console.WriteLine("4. Reverse session names");
            Console.WriteLine("5. Find session index");
            Console.WriteLine("6. Check if session exists");
            Console.WriteLine("7. Show duration statistics");
            Console.WriteLine("8. Show session date details");
            Console.WriteLine("9. Show past and upcoming sessions");
            Console.WriteLine("10. Find next session");
            Console.WriteLine("11. Compare two session dates");
            Console.WriteLine("12. Read and validate a custom date");
            Console.WriteLine("13. Select session by index");
            Console.WriteLine("14. Validate session duration");
            Console.WriteLine("15. Generate report using string");
            Console.WriteLine("16. Generate report using StringBuilder");
            Console.WriteLine("17. Demo: ref");
            Console.WriteLine("18. Demo: out");
            Console.WriteLine("19. Demo: array without ref");
            Console.WriteLine("20. Demo: params");
            Console.WriteLine("21. Show date formats");
            Console.WriteLine("22. Find a session (Array.Find)");
            Console.WriteLine("23. Find session index (Array.FindIndex)");
            Console.WriteLine("24. Copy session names (Array.Copy)");
            Console.WriteLine("0. Exit");


        }
        static void RunMenu()
        {
            while (true)
            {
                DisplayMenu();

                int choice = ReadMenuChoice();
                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Displaying all sessions...");
                        DisplayAllSessions(sessionNames, sessionDates, sessionDurations);
                        break;

                    case 2:
                        Console.WriteLine("Searching for a session...");
                        SearchSession();
                        break;
                    case 3:
                        Console.WriteLine("Sorting session names...");
                        SortSessionNames();
                        break;
                    case 4:
                        Console.WriteLine("Reversing session names...");
                        ReverseSessionNames();
                        break;
                    case 5:
                        Console.WriteLine("Finding session index...");
                        FindSessionIndex();
                        break;
                    case 6:
                        Console.WriteLine("Checking if session exists...");
                        CheckSessionExists();
                        break;
                    case 7:
                        Console.WriteLine("Showing duration statistics...");
                        DisplayDurationStatistics();
                        DisplaySortedDurations();
                        break;
                    case 8:
                        Console.WriteLine("Showing session date details...");
                        DisplaySessionDetails(sessionNames, sessionDates, sessionDurations);
                        break;
                    case 9:
                        Console.WriteLine("Showing past and upcoming sessions...");
                        DisplaySessionStatus(sessionNames, sessionDates);
                        break;
                    case 10:
                        Console.WriteLine("Finding next session...");
                        FindNextSession(sessionNames, sessionDates);
                        break;
                    case 11:
                        Console.WriteLine("Comparing two session dates...");
                        DateDifference();
                        break;
                    case 12:
                        Console.WriteLine("Reading and validating a custom date...");
                        DateTime customDate = ReadSessionDate();
                        Console.WriteLine($"Valid date: {customDate.ToString("yyyy-MM-dd HH:mm")}");
                        break;
                    case 13:
                        Console.WriteLine("Selecting session by index...");
                        SelectSessionByIndex();
                        break;
                    case 14:
                        Console.WriteLine("Validating session duration...");
                        TestValidateSessionDuration();
                        break;
                    case 15:
                        Console.WriteLine("Generating report using string...");
                        string report = BuildReportUsingString(sessionNames, sessionDates, sessionDurations);
                        Console.WriteLine(report);
                        break;
                    case 16:
                        Console.WriteLine("Generating report using StringBuilder...");
                        Console.WriteLine(BuildReportUsingStringBuilder(sessionNames, sessionDates, sessionDurations));
                        break;
                    case 17:
                        Console.WriteLine("Demonstrating ref...");
                        TestUpdateNumber();
                        break;
                    case 18:
                        Console.WriteLine("Demonstrating out...");
                        TestTryGetSessionInfo();
                        break;
                    case 19:
                        Console.WriteLine("Demonstrating array parameter without ref...");
                        TestChangeFirstElement();
                        break;
                    case 20:
                        Console.WriteLine("Demonstrating params...");
                        TestCalculateTotalDuration();
                        break;
                    case 21:
                        Console.WriteLine("Showing date formats...");
                        DisplayDateFormats();
                        break;
                    case 22:
                        Console.WriteLine("Finding a session...");
                        FindSession();
                        break;
                    case 23:
                        Console.WriteLine("Finding session index by condition...");
                        FindSessionIndexByCondition();
                        break;
                    case 24:
                        Console.WriteLine("Copying session names...");
                        CopySessionNames();
                        break;

                    case 0:
                        return;

                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }
        static int ReadMenuChoice()
        {
            while (true)
            {
                Console.WriteLine("Choose an option: ");
                try
                {
                    return int.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid menu option. Enter a number.");
                }
            }
        }
        static void DisplayAllSessions(string[] session_Names, DateTime[] session_Dates, int[] session_Durations)
        {
            // Implementation for displaying all sessions
            if (session_Names.Length == 0)
            {
                Console.WriteLine("No sessions available.\n");
                return;
            }
            for (int i = 0; i < session_Names.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {session_Names[i]}");
                Console.WriteLine($"Date: {session_Dates[i].ToString("dd MMMM yyyy")}");
                Console.WriteLine($"Start Time: {session_Dates[i].ToString("hh:mm tt")}");
                Console.WriteLine($"Duration: {session_Durations[i]} minutes\n");
            }
        }
        static void DisplaySessionDetails(string[] session_Names, DateTime[] session_Dates, int[] session_Durations)
        {
            Console.WriteLine("Enter session name: ");
            string sessionName = Console.ReadLine();
            int index = Array.IndexOf(session_Names, sessionName);
            if (index < 0)
            {
                Console.WriteLine("Session not found.");
                return;
            }
            DateTime start = session_Dates[index];
            DateTime end = GetSessionEndTime(start, session_Durations[index]);

            Console.WriteLine($"Session: {session_Names[index]}");
            Console.WriteLine($"Date: {start.ToString("dd MMMM yyyy")}");
            Console.WriteLine($"Day: {start.DayOfWeek}");
            Console.WriteLine($"Year: {start.Year}");
            Console.WriteLine($"Month: {start.Month}");
            Console.WriteLine($"Day Number: {start.Day}");
            Console.WriteLine($"Start Time: {start.ToString("hh:mm tt")}");
            Console.WriteLine($"Duration: {session_Durations[index]} minutes");
            Console.WriteLine($"End Time: {end.ToString("hh:mm tt")}");
        }
        static void SearchSession()
        {
            Console.WriteLine("Enter the session name to search: ");
            string searchName = Console.ReadLine();
            int index = Array.IndexOf(sessionNames, searchName);
            if (index >= 0)
            {
                Console.WriteLine($"Session found: {sessionNames[index]}");
                Console.WriteLine($"Date: {sessionDates[index]}");
                Console.WriteLine($"Start time: {sessionDates[index].TimeOfDay}");
                Console.WriteLine($"Duration: {sessionDurations[index]} minutes\n");
            }
            else
            {
                Console.WriteLine("Session not found.\n");
            }
        }
        static void SortSessionNames()
        {
            string[] sessionNamesCopy = new string[sessionNames.Length];

            Array.Copy(sessionNames, sessionNamesCopy, sessionNames.Length);
            Array.Sort(sessionNamesCopy);

            foreach (string session in sessionNamesCopy)
            {
                Console.WriteLine(session);
            }
        }

        static void ReverseSessionNames()
        {
            string[] sessionNamesCopy = new string[sessionNames.Length];
            Array.Copy(sessionNames, sessionNamesCopy, sessionNames.Length);
            Array.Reverse(sessionNamesCopy);
            foreach (string session in sessionNamesCopy)
            {
                Console.WriteLine(session);
            }
        }

        static void FindSessionIndex()
        {
            Console.WriteLine("Enter session name: ");
            string searchName = Console.ReadLine();
            int index = Array.IndexOf(sessionNames, searchName);
            if (index >= 0)
            {
                Console.WriteLine($"Index: {index}");
            }
            else
            {
                Console.WriteLine("Session not found.");
            }
        }

        static void CheckSessionExists()
        {
            Console.WriteLine("Enter the session name to check: ");
            string searchName = Console.ReadLine();
            bool nameExists = Array.Exists(sessionNames, name => name == searchName);
            Console.WriteLine(nameExists ? "Session exists." : "Session does not exist.");
        }

        static void FindSession()
        {
            Console.WriteLine("Enter the session name to find: ");
            string searchName = Console.ReadLine();
            string foundSession = Array.Find(sessionNames, name => name == searchName);
            Console.WriteLine(foundSession != null ? $"Session found: {foundSession}" : "Session not found.");
        }

        static void FindSessionIndexByCondition()
        {
            Console.WriteLine("Enter the session name to find its index: ");
            string searchName = Console.ReadLine();
            int index = Array.FindIndex(sessionNames, name => name == searchName);
            Console.WriteLine(index >= 0 ? $"Session index: {index}" : "Session not found.");
        }

        static void CopySessionNames()
        {
            // Array.Copy()
            // modify copy
            // display original and copy
            string[] sessionNamesCopy = new string[sessionNames.Length];
            Array.Copy(sessionNames, sessionNamesCopy, sessionNames.Length);
            Console.WriteLine("Original vs Copy before modification:");
            // Display original session names vs copy session names
            for (int i = 0; i < sessionNames.Length; i++)
            {
                Console.WriteLine($"Original: {sessionNames[i]}, Copy: {sessionNamesCopy[i]}");
            }
            sessionNamesCopy[0] = "OOP";
            Console.WriteLine("Original vs Copy after modification:");
            for (int i = 0; i < sessionNames.Length; i++)
            {
                Console.WriteLine($"Original: {sessionNames[i]}, Copy: {sessionNamesCopy[i]}");
            }
        }
        static int GetTotalDuration(int[] durations)
        {
            int total = 0;
            foreach (int duration in durations)
            {
                total += duration;
            }
            return total;
        }

        static double GetAverageDuration(int[] durations)
        {
            return (double)GetTotalDuration(durations) / durations.Length;
        }

        static int GetShortestDuration(int[] durations)
        {
            int shortest = int.MaxValue;
            foreach (int duration in durations)
            {
                if (duration < shortest)
                {
                    shortest = duration;
                }
            }
            return shortest;
        }

        static int GetLongestDuration(int[] durations)
        {
            int longest = int.MinValue;
            foreach (int duration in durations)
            {
                if (duration > longest)
                {
                    longest = duration;
                }
            }
            return longest;
        }

        static void DisplayDurationStatistics()
        {
            int totalDuration = GetTotalDuration(sessionDurations);
            double averageDuration = GetAverageDuration(sessionDurations);
            int shortestDuration = GetShortestDuration(sessionDurations);
            int longestDuration = GetLongestDuration(sessionDurations);

            Console.WriteLine($"Total Duration: {totalDuration} minutes");
            Console.WriteLine($"Average Duration: {averageDuration} minutes");
            Console.WriteLine($"Shortest Duration: {shortestDuration} minutes");
            Console.WriteLine($"Longest Duration: {longestDuration} minutes");
        }

        static void DisplaySortedDurations()
        {
            int[] sortedDurations = new int[sessionDurations.Length];
            Array.Copy(sessionDurations, sortedDurations, sessionDurations.Length);
            Array.Sort(sortedDurations);

            Console.WriteLine("\nSorted Durations (smallest to largest):");
            foreach (int duration in sortedDurations)
            {
                Console.WriteLine($"{duration} minutes");
            }
        }

        static DateTime GetSessionEndTime(DateTime startTime, int duration)
        {
            return startTime.AddMinutes(duration);
        }


        static DateTime ReadSessionDate()
        {
            while (true)
            {
                Console.WriteLine("Enter date (yyyy-MM-dd HH:mm): ");
                string input = Console.ReadLine();

                if (DateTime.TryParseExact(input, "yyyy-MM-dd HH:mm",
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None,
                    out DateTime result))
                {
                    return result;
                }

                Console.WriteLine("Invalid date. Use the format yyyy-MM-dd HH:mm.");
            }
        }

        static string GetSessionStatus(DateTime sessionDate, DateTime now)
        {
            return sessionDate < now ? "Past" : "Upcoming";
        }

        static void DisplaySessionStatus(string[] session_Names, DateTime[] session_Dates)
        {
            DateTime now = DateTime.Now;
            for (int i = 0; i < session_Names.Length; i++)
            {
                Console.WriteLine($"{session_Names[i]} {GetSessionStatus(session_Dates[i], now)}");
            }
            Console.WriteLine();
        }

        static string BuildReportUsingString(string[] session_Names, DateTime[] session_Dates, int[] session_Durations)
        {
            string result = "";
            for (int i = 0; i < session_Names.Length; i++)
            {
                result += session_Names[i] + " - "
                        + session_Dates[i].ToString("dd/MM/yyyy hh:mm tt") + " - "
                        + session_Durations[i] + " minutes\n";
            }
            return result;
        }
        static string BuildReportUsingStringBuilder(string[] session_Names, DateTime[] session_Dates, int[] session_Durations)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < session_Names.Length; i++)
            {
                sb.Append($"{session_Names[i]} - {session_Dates[i]:dd/MM/yyyy hh:mm tt} - {session_Durations[i]} minutes\n");
            }
            return sb.ToString();

        }

        static void DateDifference()
        {
            Console.WriteLine("Enter two session names to compare their dates:");
            Console.Write("First Session: ");
            string firstSession = Console.ReadLine();
            Console.Write("Second Session: ");
            string secondSession = Console.ReadLine();
            int firstIndex = Array.IndexOf(sessionNames, firstSession);
            int secondIndex = Array.IndexOf(sessionNames, secondSession);
            if (firstIndex < 0 || secondIndex < 0)
            {
                Console.WriteLine("Session not found.");
                return;
            }
            DateTime firstDate = sessionDates[firstIndex];
            DateTime secondDate = sessionDates[secondIndex];
            TimeSpan difference = firstDate - secondDate;
            Console.WriteLine($"Difference: ");
            Console.WriteLine($"{Math.Abs(difference.TotalDays)} days");
            Console.WriteLine($"{Math.Abs(difference.TotalHours)} hours");


        }

        static void FindNextSession(string[] session_names, DateTime[] session_dates)
        {
            DateTime now = DateTime.Now;
            int nextSessionIndex = -1;
            for (int i = 0; i < session_dates.Length; i++)
            {
                if (session_dates[i] <= now)
                    continue;
                if (nextSessionIndex == -1 || session_dates[i] < session_dates[nextSessionIndex])
                {
                    nextSessionIndex = i;
                }
            }
            if (nextSessionIndex == -1)
            {
                Console.WriteLine("No upcoming sessions.");
                return;
            }
            TimeSpan remainingTime = session_dates[nextSessionIndex] - now;
            Console.WriteLine($"Next session: \n");
            Console.WriteLine($"{session_names[nextSessionIndex]}");
            Console.WriteLine($"{session_dates[nextSessionIndex].ToString("dd MMMM yyyy")}");
            Console.WriteLine($"{session_dates[nextSessionIndex].ToString("hh:mm tt")}\n");
            Console.WriteLine($"Time remaining:");
            Console.WriteLine($"{remainingTime.Days} days");
            Console.WriteLine($"{remainingTime.Hours} hours");

        }

        static void UpdateNumber(ref int number)
        {
            number += 10;

        }
        static void TestUpdateNumber()
        {
            int myNumber = 5;
            Console.WriteLine($"Before Update: {myNumber}");
            UpdateNumber(ref myNumber);
            Console.WriteLine($"After Update: {myNumber}");
        }

        static bool TryGetSessionInfo(string sessionName, out int sessionIndex, out int sessionDuration)
        {
            sessionIndex = Array.IndexOf(sessionNames, sessionName);
            if (sessionIndex < 0)
            {
                sessionDuration = 0;
                return false;

            }
            sessionDuration = sessionDurations[sessionIndex];
            return true;
        }
        static void TestTryGetSessionInfo()
        {
            Console.WriteLine("Enter session name: ");
            string sessionName = Console.ReadLine();
            if (TryGetSessionInfo(sessionName, out int index, out int duration))
            {
                Console.WriteLine($"Index: {index}");
                Console.WriteLine($"Duration: {duration} minutes");
            }
            else
            {
                Console.WriteLine("Session not found.");
            }
        }

        static void ChangeFirstElement(string[] names)
        {
            if (names.Length > 0)
            {
                names[0] = "Updated Session Name";

            }
        }
        static void TestChangeFirstElement()
        {
            string[] copy = new string[sessionNames.Length];
            Array.Copy(sessionNames, copy, sessionNames.Length);
            Console.WriteLine($"Before Change: {string.Join(", ", copy)}");
            ChangeFirstElement(copy);
            Console.WriteLine($"After Change: {string.Join(", ", copy)}");
        }

        static int CalculateTotalDuration(params int[] durations)
        {
            int total = 0;
            foreach (int duration in durations)
            {
                total += duration;
            }
            return total;
        }

        static void TestCalculateTotalDuration()
        {
            Console.WriteLine($"Total (2 values): {CalculateTotalDuration(120, 180)}");
            Console.WriteLine($"Total (3 values): {CalculateTotalDuration(120, 180, 240)}");
            Console.WriteLine($"Total (5 values): {CalculateTotalDuration(60, 90, 120, 180, 240)}");
        }

        static void ValidateSessionDuration()
        {
            Console.WriteLine("Enter session duration in minutes: ");
            int duration = int.Parse(Console.ReadLine());
            if(duration > 0)
            {
                Console.WriteLine("Duration accepted.");
            }
            else
            {
                throw new ArgumentException("Duration must be greater than zero.");
            }
        }
        static void TestValidateSessionDuration()
        {
            try
            {
                ValidateSessionDuration();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. Enter a number.");
            }
            finally
            {
                Console.WriteLine("Input operation finished.");
            }
        }

        static void SelectSessionByIndex()
        {
            Console.WriteLine("Enter session index: ");
            try
            {
                int index = int.Parse(Console.ReadLine());
                Console.WriteLine($"Session: {sessionNames[index]}");
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. Enter a number.");
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("The selected session index is out of range.");
            }
        }

        static void DisplayDateFormats()
        {
            Console.WriteLine("Enter the session name: ");
            string name = Console.ReadLine();
            int index = Array.IndexOf(sessionNames, name);
            if (index < 0)
            {
                Console.WriteLine("Session not found.");
                return;
            }
            DateTime date = sessionDates[index];
            Console.WriteLine($"Session: {sessionNames[index]}");
            Console.WriteLine(date.ToString("yyyy-MM-dd"));
            Console.WriteLine(date.ToString("dd/MM/yyyy"));
            Console.WriteLine(date.ToString("dd MMMM yyyy"));
            Console.WriteLine(date.ToString("dddd, dd MMMM yyyy"));
            Console.WriteLine(date.ToString("hh:mm tt"));
        }
    }
}
