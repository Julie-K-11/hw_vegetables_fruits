using Microsoft.Data.SqlClient;

namespace hw_vegetables_fruits
{
    class Program
    {
        static void Main(string[] args)
        {
            string connStr = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Products;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

            using (var connection = new SqlConnection(connStr))
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("Successfully connected to the database\n");

                    while (true)
                    {
                        Console.WriteLine("----------------items----------------");
                        Console.WriteLine("1. Show all items");
                        Console.WriteLine("2. Show all names");
                        Console.WriteLine("3. Show all colors");
                        Console.WriteLine("4. Show max calories");
                        Console.WriteLine("5. Show min calories");
                        Console.WriteLine("6. Show average calories");
                        Console.WriteLine("7. Count vegetables");
                        Console.WriteLine("8. Count fruits");
                        Console.WriteLine("9. Count items by color");
                        Console.WriteLine("10. Count items of each color");
                        Console.WriteLine("11. Items below given calories");
                        Console.WriteLine("12. Items above given calories");
                        Console.WriteLine("13. Items within calorie range");
                        Console.WriteLine("14. Yellow or Red items");
                        Console.WriteLine("15. Exit");
                        Console.Write("\nSelect an option: ");

                        string userInput = Console.ReadLine();
                        Console.WriteLine();

                        if (userInput == "15") break;

                        switch (userInput)
                        {
                            case "1":
                                DisplayAllStudents(connection); 
                                break;
                            case "2":
                                DisplayAllNames(connection); 
                                break;
                            case "3":
                                DisplayAllColores(connection); 
                                break;
                            case "4":
                                DisplayMaxCalories(connection); 
                                break;
                            case "5":
                                DisplayMinCalories(connection); 
                                break;
                            case "6":
                                DisplayAvgCalories(connection); 
                                break;
                            case "7":
                                DisplayVegetableCount(connection); 
                                break;
                            case "8":
                                DisplayFruitCount(connection); 
                                break;
                            case "9":
                                Console.Write("Enter color: ");
                                DisplayCountByColor(connection, Console.ReadLine());
                                break;
                            case "10":
                                DisplayCountForEachColor(connection); 
                                break;
                            case "11":
                                Console.Write("Enter maximum calories: ");
                                if (int.TryParse(Console.ReadLine(), out int maxCal))
                                {
                                    DisplayCaloriesBelow(connection, maxCal);
                                }
                                else Console.WriteLine("Error input");
                                break;
                            case "12":
                                Console.Write("Enter minimum calories: ");
                                if (int.TryParse(Console.ReadLine(), out int minCal))
                                {
                                    DisplayCaloriesAbove(connection, minCal);
                                }
                                else Console.WriteLine("Error input");
                                break;
                            case "13":
                                Console.Write("Min: ");
                                if (!int.TryParse(Console.ReadLine(), out int minRange))
                                {
                                    Console.WriteLine("Invalid input");
                                    break;
                                }
                                Console.Write("Max: ");
                                if (!int.TryParse(Console.ReadLine(), out int maxRange))
                                {
                                    Console.WriteLine("Invalid input");
                                    break;
                                }
                                DisplayCaloriesInRange(connection, minRange, maxRange);
                                break;
                            case "14":
                                DisplayYellowAndRed(connection); 
                                break;
                            default:
                                Console.WriteLine("Error Input");
                                break;
                        }
                    }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message} ");
                }
            }
        }

        static void DisplayAllStudents(SqlConnection connection)
        {
            var query = @"SELECT * FROM ShopProducts";
            using (var cmd = new SqlCommand(query, connection))
            using (var reader = cmd.ExecuteReader())
            {
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"Name: {reader["Name"]} \nType: {reader["Type"]} \nColor: {reader["Color"]} \nCalories: {reader["Calories"]}\n");
                    }
                }
            }
        }

        static void DisplayAllNames(SqlConnection connection)
        {
            var query = "SELECT Name FROM ShopProducts";
            using (var cmd = new SqlCommand(query, connection))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"Name: {reader["Name"]}");
                }
                Console.WriteLine();
            }
        }

        static void DisplayAllColores(SqlConnection connection)
        {
            var query = "SELECT Color FROM ShopProducts";
            using (var cmd = new SqlCommand(query, connection))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"Color: {reader["Color"]}");
                }
                Console.WriteLine();
            }
        }

        static void DisplayMaxCalories(SqlConnection connection)
        {
            var query = "SELECT MAX(Calories) FROM ShopProducts";
            using (var cmd = new SqlCommand(query, connection))
            {
                var result = cmd.ExecuteScalar();
                Console.WriteLine($"MAX Calories: {result}");
            }
        }

        static void DisplayMinCalories(SqlConnection connection)
        {
            var query = "SELECT MIN(Calories) FROM ShopProducts";
            using (var cmd = new SqlCommand(query, connection))
            {
                var result = cmd.ExecuteScalar();
                Console.WriteLine($"MIN Calories: {result}");
            }
        }

        static void DisplayAvgCalories(SqlConnection connection)
        {
            var query = "SELECT AVG(Calories) FROM ShopProducts";
            using (var cmd = new SqlCommand(query, connection))
            {
                var result = cmd.ExecuteScalar();
                Console.WriteLine($"AVG Calories: {result}");
            }
        }

        static void DisplayVegetableCount(SqlConnection connection)
        {
            var query = "SELECT COUNT(*) FROM ShopProducts WHERE Type = 'Vegetable'";
            using (var cmd = new SqlCommand(query, connection))
            {
                var result = cmd.ExecuteScalar();
                Console.WriteLine($"Vegetables: {result}");
            }
        }

        static void DisplayFruitCount(SqlConnection connection)
        {
            var query = "SELECT COUNT(*) FROM ShopProducts WHERE Type = 'Fruit'";
            using (var cmd = new SqlCommand(query, connection))
            {
                var result = cmd.ExecuteScalar();
                Console.WriteLine($"Fruits: {result}");
            }
        }
        static void DisplayCountByColor(SqlConnection connection, string color)
        {
            if (string.IsNullOrWhiteSpace(color))
            {
                Console.WriteLine("Enter color!");
                return;
            }
            var query = "SELECT COUNT(*) FROM ShopProducts WHERE Color = @Color";
            using (var cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@Color", color);
                var result = cmd.ExecuteScalar();
                Console.WriteLine($"Color '{color}': {result}");
            }
        }

        static void DisplayCountForEachColor(SqlConnection connection)
        {
            var query = "SELECT Color, COUNT(*) AS Amount FROM ShopProducts GROUP BY Color";
            using (var cmd = new SqlCommand(query, connection))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"Color: {reader["Color"]}, Count: {reader["Amount"]}");
                    }
                }
            }

        }

        static void DisplayCaloriesBelow(SqlConnection connection, int maxCal)
        {
            var query = "SELECT Name, Type, Color, Calories FROM ShopProducts WHERE Calories < @MaxCal";
            using (var cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@MaxCal", maxCal);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"Name: {reader["Name"]}, Type: {reader["Type"]}, Color: {reader["Color"]}, Calories: {reader["Calories"]}");
                    }
                }
            }
        }

        static void DisplayCaloriesAbove(SqlConnection connection, int minCal)
        {
            var query = "SELECT Name, Type, Color, Calories FROM ShopProducts WHERE Calories > @MinCal";
            using (var cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@MinCal", minCal);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"Name: {reader["Name"]}, Type: {reader["Type"]}, Color: {reader["Color"]}, Calories: {reader["Calories"]}");
                    }
                }
            }
        }

        static void DisplayCaloriesInRange(SqlConnection connection, int minRange, int maxRange)
        {
            var query = "SELECT Name, Type, Color, Calories FROM ShopProducts WHERE Calories BETWEEN @Min AND @Max";
            using (var cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@Min", minRange);
                cmd.Parameters.AddWithValue("@Max", maxRange);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"Name: {reader["Name"]}, Type: {reader["Type"]}, Color: {reader["Color"]}, Calories: {reader["Calories"]}");
                    }
                }
            }
        }

        static void DisplayYellowAndRed(SqlConnection connection)
        {
            var query = "SELECT Name, Type, Color, Calories FROM ShopProducts WHERE Color IN ('Yellow', 'Red')";
            using (var cmd = new SqlCommand(query, connection))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"Name: {reader["Name"]}, Type: {reader["Type"]}, Color: {reader["Color"]}, Calories: {reader["Calories"]}");
                    }
                }
            }
        }
    }
}
