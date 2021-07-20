using System;

namespace Assignment2_KatrinaMehta
{
    class Program
    {

        //Vehicle
        static Vehicle vehicle;
        static string vehicleIdText;
        static string model;
        static string make;
        static string yearText;
        static string condition;
        const string newText = "NEW";
        const string oldText = "OLD";

        //Inventory
        static Inventory inventory;
        static string invIdText;
        static int vehicleId;
        static int numHand;
        static decimal price, cost;

        //Reapir
        static Repair repair;
        static int invID;
        static string repairIdText;
        static string whatToRepair;


        static void Main(string[] args)
        {
            vehicle = new Vehicle();
            inventory = new Inventory();
            repair = new Repair();
            MainMenu();
        }

        static void MainMenu()
        {
            Boolean tocontinue = true;
            while (tocontinue)
            {
                Console.WriteLine("Welcome, please choose a command:");
                Console.WriteLine("Press 1 to modify vehicles");
                Console.WriteLine("Press 2 to modify inventory");
                Console.WriteLine("Press 3 to modify repair");
                Console.WriteLine("Press 4 to exit program");
                char input = Console.ReadLine()[0];
                Console.WriteLine("\n");
                switch (input)
                {
                    case '1':   //  Vehicle Menu
                        VehicleMenu();
                        break;
                    case '2':   //  Inventory Menu
                        InventoryMenu();
                        break;
                    case '3':   //  Repair Menu
                        RepairMenu();
                        break;
                    case '4':   //Exit program                       
                        return;
                    default:
                        Console.WriteLine("Invalid data entered");
                        break;
                }
            }
        }

        static void VehicleMenu()
        {
            Boolean tocontinue = true;
            while (tocontinue)
            {
                Console.WriteLine("Press 1 to list all vehicles");
                Console.WriteLine("Press 2 to add a new vehicle ");
                Console.WriteLine("Press 3 to update vehicle ");
                Console.WriteLine("Press 4 to delete vehicle ");
                Console.WriteLine("Press 5 to return to main menu");
                char input = Console.ReadLine()[0];

                switch (input)
                {

                    case '1': // List vehicles
                        Console.WriteLine("\n");
                        vehicle.GetAllVehicles();
                        break;

                    case '2':   //  Add vehicle
                        try
                        {
                            ReadVehicle();
                            ushort year = 0;
                            bool canConvert = ushort.TryParse(yearText, out year);
                            if (canConvert == false)
                            {
                                throw new FormatException();
                            }
                            if (String.Equals(condition.ToUpper(), newText) || String.Equals(condition.ToUpper(), oldText))
                            {
                                vehicle.InsertVehicle(make, model, yearText, condition);

                            }
                            else
                            {
                                throw new ArgumentException();
                            }

                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("\nInvalid data entered. Please try again.\n");
                        }
                        catch (ArgumentException){
                            Console.WriteLine("\nCondition must be 'NEW' or 'USED. Please try again.\n");
                        }

                        break;

                    case '3':   //Update Vehicle
                        try
                        {
                            Console.WriteLine("Enter Vehicle Id");
                            vehicleIdText = Console.ReadLine();
                            int id = Int32.Parse(vehicleIdText);
                            ReadVehicle();
                            ushort year = 0;
                            bool canConvert = ushort.TryParse(yearText, out year);
                            if (canConvert == false)
                            {
                                throw new FormatException();
                            }
                            if (String.Equals(condition.ToUpper(), newText) || String.Equals(condition.ToUpper(), oldText))
                            {
                                vehicle.UpdateVehicle(id,make, model, yearText, condition);

                            }
                            else
                            {
                                throw new ArgumentException();
                            }


                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("\nInvalid data entered. Please try again.\n");

                        }
                        catch (ArgumentException)
                        {
                            Console.WriteLine("\nCondition must be 'NEW' or 'USED'. Please try again.\n");
                        }
                        break;

                    case '4':   //Delete Vehicle
                        try
                        {
                            Console.WriteLine("Enter Vehicle ID:");
                            vehicleIdText = Console.ReadLine();
                            int id = Int32.Parse(vehicleIdText);
                            vehicle.DeleteVehicle(id);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("\nInvalid Data entered. Please try again\n");
                        }
                        break;

                    case '5':   //  Return to main menu
                        return;
                    default:
                        Console.WriteLine("Invalid data entered. Please try again.\n");
                        break;

                }
            }

        }


        static void InventoryMenu()
        {
            Boolean tocontinue = true;
            while (tocontinue)
            {
                Console.WriteLine("Press 1 to insert new inventory");
                Console.WriteLine("Press 2 to view inventory");
                Console.WriteLine("Press 3 to update inventory ");
                Console.WriteLine("Press 4 to delete inventory ");
                Console.WriteLine("Press 5 to return to main menu");
                char input = Console.ReadLine()[0];

                switch (input)
                {
                    case '1':  //   Add new Inventory
                        try
                        {

                            ReadInventory();
                            inventory.InsertInventory(vehicleId, numHand, price, cost);

                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("\nInvalid Data entered. Please try again.\n");
                        }
                        break;

                    case '2':   //  View all Inventory
                        inventory.GetAllInventory();
                        break;

                    case '3':   //  Update Inventory
                        try
                        {
                            Console.WriteLine("Enter Inventory ID");
                            invIdText = Console.ReadLine();
                            int id = Int32.Parse(invIdText);
                            ReadInventory();
                            inventory.UpdateInventory(id, vehicleId, numHand, price, cost);
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Invalid data entered. Please try again.\n");
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("\nInvalid Data entered. Please try again\n");
                        }
                        break;
                    case '4':   //  Delete Inventory
                        try
                        {
                            Console.WriteLine("Enter Inventory ID");
                            invIdText = Console.ReadLine();
                            int id = Int32.Parse(invIdText);
                            inventory.DeleteInventory(id);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("\nInvalid Data entered. Please try again.\n");
                        }
                        break;
                    case '5':
                        return;
                    default:
                        Console.WriteLine("\nInvalid Data entered. Please try again.\n");
                        break;

                }
            }

        }
       
        static void RepairMenu()
        {
            Boolean tocontinue = true;
            while (tocontinue)
            {
                Console.WriteLine("Press 1 to view repairs");
                Console.WriteLine("Press 2 to add a repair ");
                Console.WriteLine("Press 3 to update a repair ");
                Console.WriteLine("Press 4 to delete a repair");
                Console.WriteLine("Press 5 to return to main menu"); 
                char input = Console.ReadLine()[0];

                switch (input)
                {
                    case '1':  //   View Vacation Reapir
                        repair.GetAllRepairs();
                        break;

                    case '2':   // Add Repair
                        try
                        {
                            ReadRepair();
                            repair.InsertRepair(invID, whatToRepair);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("\nInvalid Data entered.Please try again.\n");
                        }
                        break;

                    case '3':   //Update repair
                        try
                        {
                            Console.WriteLine("Enter Repair ID");
                            repairIdText = Console.ReadLine();
                            int id = Int32.Parse(repairIdText);
                            ReadRepair();
                            repair.UpdateRepair(id, invID, whatToRepair);

                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("\nInvalid Data entered. Please try again.\n");
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("\nInvalid Data entered. Please try again.\n");
                        }
                        break;

                    case '4':   //delete repair
                        try
                        {
                            Console.WriteLine("Enter Repair ID");
                            repairIdText = Console.ReadLine();
                            int id = Int32.Parse(repairIdText);
                            repair.DeleteRepair(id);
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("\nInvalid Data entered. Please try again.\n");
                        }
                        catch(Exception)
                        {
                            Console.WriteLine("\nInvalid Data entered. Please try again.\n");
                        }
                        break;
                    case '5':   //return to menu
                        return;

                    default:
                        Console.WriteLine("\nInvalid Data entered. Please try again.\n");
                        break;

                }
            }

        }

        private static void ReadVehicle()
        {
            Console.WriteLine("Enter make");
            make = Console.ReadLine();
            Console.WriteLine("Enter model");
            model = Console.ReadLine();
            Console.WriteLine("Enter year");
            yearText = Console.ReadLine();
            Console.WriteLine("Enter condition");
            condition = Console.ReadLine();
        }

        private static void ReadInventory()
        {
            Console.WriteLine("Enter Vehicle ID");
            vehicleIdText = Console.ReadLine();
            vehicleId = Int32.Parse(vehicleIdText);
            Console.WriteLine("Enter number on hand");
            numHand = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Enter price");
            price = decimal.Parse(Console.ReadLine());
            Console.WriteLine("Enter cost");
            cost = decimal.Parse(Console.ReadLine());
        }

        private static void ReadRepair()
        {
            Console.WriteLine("Enter Inventory ID");
            invIdText = Console.ReadLine();
            invID = Int32.Parse(invIdText);
            Console.WriteLine("Enter what to repair:");
            whatToRepair = Console.ReadLine();

        }
    }
}
