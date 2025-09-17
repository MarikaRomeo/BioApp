namespace BioApp
{
    internal class Program
    {
        const double TAXES = 0.06;
        const double DISCOUNT = 0.15;
        const string CURRENCY = "SEK";

        // Movies, times and prices
        static string[] movies = { "Mean Girls", "Harry Potter and the sorcerer´s stone", "Star Wars" };
        static string[] showTimes = { "18:00", "20:30", "22:00" };
        static double[] basePrices = { 120.0, 130.0, 150.0 };

        static int selectedMovieIndex = -1;
        static int selectedTimeIndex = -1;
        static int tickets = 0;
        static bool isStudent = false;

        static void Main(string[] args)
        {
            bool running = true;

            while (running)
            {
                ShowMenu();
                string input = Console.ReadLine();
                Console.WriteLine();

                switch (input)
                {
                    case "1":
                        ListMovies(movies, showTimes, basePrices);
                        break;

                    case "2":
                        ListMovies(movies, showTimes, basePrices);

                        Console.Write("Choose movie (1-3): ");
                        if (int.TryParse(Console.ReadLine(), out int movieChoice) &&
                            movieChoice >= 1 && movieChoice <= movies.Length)
                        {
                            selectedMovieIndex = movieChoice - 1;
                        }
                        else
                        {
                            Console.WriteLine("Invalid choice.");
                            break;
                        }

                        Console.Write("Choose time (1-3): ");
                        if (int.TryParse(Console.ReadLine(), out int timeChoice) &&
                            timeChoice >= 1 && timeChoice <= showTimes.Length)
                        {
                            selectedTimeIndex = timeChoice - 1;
                        }
                        else
                        {
                            Console.WriteLine("Invalid choice.");
                            break;
                        }

                        Console.Write("Enter number of tickets: ");
                        if (int.TryParse(Console.ReadLine(), out int ticketCount) && ticketCount > 0)
                        {
                            tickets = ticketCount;
                        }
                        else
                        {
                            Console.WriteLine("Invalid ticket number.");
                        }
                        break;

                    case "3":
                        isStudent = !isStudent;
                        Console.WriteLine($"Student discount is now: {(isStudent ? "ON" : "OFF")}");
                        break;

                    case "4":
                        if (selectedMovieIndex == -1 || selectedTimeIndex == -1 || tickets == 0)
                        {
                            Console.WriteLine("Please choose a movie, time, and tickets first!");
                            break;
                        }

                        double basePrice = basePrices[selectedMovieIndex];
                        double total = isStudent
                            ? CalculatePrice(tickets, basePrice, DISCOUNT)
                            : CalculatePrice(tickets, basePrice);

                        PrintReceipt(
                            movie: movies[selectedMovieIndex],
                            time: showTimes[selectedTimeIndex],
                            tickets: tickets,
                            total: total,
                            isStudent: isStudent
                        );
                        break;

                    case "5":
                        running = false;
                        Console.WriteLine("Goodbye!");
                        break;

                    default:
                        Console.WriteLine("Invalid option, try again.");
                        break;
                }
            }
        }

        static void ShowMenu()
        {
            Console.WriteLine("CINEMA BOOKING APP");
            Console.WriteLine("1. List movies");
            Console.WriteLine("2. Choose movie & time, enter tickets");
            Console.WriteLine("3. Toggle student discount (on/off)");
            Console.WriteLine("4. Print receipt");
            Console.WriteLine("5. Exit");
            Console.Write("Choose an option: ");
        }

        static void ListMovies(string[] movies, string[] times, double[] prices)
        {
            Console.WriteLine("\n--- Movies & Showtimes ---");
            for (int i = 0; i < movies.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {movies[i]}:");
                for (int j = 0; j < times.Length; j++)
                {
                    Console.WriteLine($"   - {times[j]} : {prices[j]} {CURRENCY}");
                }
            }
        }


        // Price calculation methods
        static double CalculatePrice(int tickets, double basePrice)
        {
            double subtotal = tickets * basePrice;
            return subtotal + (subtotal * TAXES);
        }

        static double CalculatePrice(int tickets, double basePrice, double discountPercent)
        {
            double subtotal = tickets * basePrice;
            subtotal -= subtotal * discountPercent;
            return subtotal + (subtotal * TAXES);
        }

        static void PrintReceipt(string movie, string time, int tickets, double total, bool isStudent)
        {
            Console.WriteLine("\nRECEIPT");
            Console.WriteLine($"Movie: {movie}");
            Console.WriteLine($"Time: {time}");
            Console.WriteLine($"Tickets: {tickets}");
            Console.WriteLine($"Student discount: {(isStudent ? "Yes" : "No")}");
            Console.WriteLine($"Total: {total:F2} {CURRENCY}");
        }
    }
}

