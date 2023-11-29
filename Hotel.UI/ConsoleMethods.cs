using Hotel.Data;
using Hotel.Logic;

namespace Hotel.UI;

public static class ConsoleMethods
{
    public static void DisplayMainMenu()
    {
        Console.WriteLine("Below are the options with their corresponding keys\n");
        Console.WriteLine(" a - enter a new room");
        Console.WriteLine(" b - enter a new customer");
        Console.WriteLine(" c - enter a new reservation");
        Console.WriteLine(" d - search for saved available rooms");
        Console.WriteLine(" e - print report of saved reservations");
        Console.WriteLine(" f - print report of a specific customer's saved reservations");
        Console.WriteLine(" g - change the rate of a room type");
        Console.WriteLine(" h - refund a reservation");
        Console.WriteLine(" i - apply coupon code");
        Console.WriteLine(" j - create a new coupon code");
        Console.WriteLine(" k - coupon code report");
        Console.WriteLine(" l - utilization report");
        Console.WriteLine(" 0 - exit");
    }

    /// <summary>
    /// Useful for quickly telling what each screen's code will do
    /// </summary>
    /// <param name="header"></param>
    /// <param name="subtext"></param>
    /// <param name="headerColor"></param>
    /// <param name="subTextColor"></param>
    public static void SetUpScreen(string header, string subtext = "", ConsoleColor headerColor = ConsoleColor.Green, ConsoleColor subTextColor = ConsoleColor.Cyan)
    {
        Console.Clear();
        Console.ForegroundColor = headerColor;
        Console.WriteLine(header);
        Console.ForegroundColor = subTextColor;
        Console.WriteLine($"\t{subtext}");

        Console.ForegroundColor = ConsoleColor.Gray;
    }

    /// <summary>
    /// Prints message at the end of doing a task in a sub-menu. Waits for a keypress before finishing method
    /// </summary>
    public static void EndingMessage(string message = "")
    {
        Console.WriteLine(message);
        Console.WriteLine("\nPlease press any key to return to main menu");
        Console.ReadKey(true);
    }

    /// <summary>
    /// Neatly displays the info of a single date report
    /// </summary>
    /// <param name="dateReportInfo"></param>
    /// <param name="highlightColor"></param>
    public static void DisplayDateReport((DateOnly date, List<(int roomNumber, RoomType roomType, bool isOccupied, decimal dailyRentalFee, decimal dailyCleaningCost)> eachRoomReportOfThisDate, double occupancyPercentageOfThisDate, decimal revenueOfThisDate, decimal cleaningCostsOfThisDate) dateReportInfo, ConsoleColor highlightColor = ConsoleColor.DarkYellow)
    {
        // deconstruct tuple for convenient usage
        (DateOnly date, List<(int roomNumber, RoomType roomType, bool isOccupied, decimal dailyRentalFee, decimal dailyCleaningCost)> eachRoomReportOfThisDate, double occupancyPercentageOfThisDate, decimal revenueOfThisDate, decimal cleaningCostsOfThisDate) = dateReportInfo;

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("----------------------------------------------------------------------------------------------------------------");

        Console.ForegroundColor = highlightColor;
        Console.WriteLine($"\nReport Date: {date}");
        Console.WriteLine($"\tTotal rental fees collected: ${revenueOfThisDate}     Total cleaning costs incurred: ${cleaningCostsOfThisDate}      Occupancy Rate: {occupancyPercentageOfThisDate: #.##}%");
        Console.ForegroundColor = ConsoleColor.Gray;

        Console.WriteLine($"\n{"Room Number",15} | {"Room Type",-10} | {"Occupancy Status",-17} | {"Daily Fee Collected",20} | {"Daily Cleaning Cost",20}");
        Console.WriteLine("----------------------------------------------------------------------------------------------------------------");
        foreach (var (roomNumber, roomType, isOccupied, dailyRentalFee, dailyCleaningCost) in eachRoomReportOfThisDate)
        {
            Console.WriteLine($"{roomNumber,15} | {roomType,-10} | {isOccupied,-17} | {"$" + dailyRentalFee,20} | {"$" + dailyCleaningCost,20}");
        }

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\n----------------------------------------------------------------------------------------------------------------");
        Console.ForegroundColor = ConsoleColor.Gray;
    }

    /// <summary>
    /// Console.ReadLine() with prompt, but built-in rejection of empty/whitespace inputs
    /// </summary>
    /// <returns></returns>
    public static string ReadLine(string prompt = "")
    {
        while (true)
        {
            Console.Write(prompt);

            string userInput = Console.ReadLine();

            if (string.IsNullOrEmpty(userInput) || string.IsNullOrWhiteSpace(userInput))
            {
                Console.WriteLine("Please enter something!");
            }
            else
            {
                return userInput;
            }
        }
    }

    /// <summary>
    /// Returns whether the user wants to break out of the sub-menu or not (yes if they pressed the left-arrow)
    /// </summary>
    /// <returns></returns>
    public static bool AllowGoingBackToMenu()
    {
        Console.Write("\nPress any key to continue ");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("(or press the left-arrow key to return to main menu)");
        Console.ForegroundColor = ConsoleColor.Gray;
        var userKey = Console.ReadKey(true).Key;

        return userKey == ConsoleKey.LeftArrow;
    }
}

public static class RequestUserInput
{
    /// <summary>
    /// Returns char with built-in bananaproof. Case-insensitive
    /// </summary>
    /// <param name="prompt"></param>
    /// <param name="listOfValidInputs"></param>
    /// <param name="userWantsToGoBack">Indicates whether to prematurely end the submenu</param>
    /// <param name="allowReturnToMenu">This method is also used in the main menu; wouldn't make sense if it always asks the user if they want to return to menu</param>
    /// <returns></returns>
    public static char GetUserChoice(string prompt, List<char> listOfValidInputs, out bool userWantsToGoBack, bool allowReturnToMenu = true)
    {
        userWantsToGoBack = false;

        while (true)
        {
            Console.Write($"{prompt}");

            char userInput = char.ToLower(Console.ReadKey().KeyChar);
            Console.WriteLine();

            foreach (var validInput in listOfValidInputs)
            {
                if (userInput == char.ToLower(validInput))
                {
                    return Convert.ToChar(userInput);
                }
            }

            Console.WriteLine("Please press one of the given keys!");
            if (allowReturnToMenu)
            {
                userWantsToGoBack = ConsoleMethods.AllowGoingBackToMenu();
                if (userWantsToGoBack)
                {
                    return '0';
                }
            }
        }
    }

    /// <summary>
    /// Returns int with built-in bananaproof
    /// </summary>
    /// <param name="prompt"></param>
    /// <param name="listOfValidInputs"></param>
    /// <param name="userWantsToGoBack">Indicates whether to prematurely end the submenu</param>
    /// <returns></returns>
    public static int GetUserChoice(string prompt, List<int> listOfValidInputs, out bool userWantsToGoBack)
    {
        userWantsToGoBack = false;

        while (true)
        {
            string userInput = ConsoleMethods.ReadLine(prompt);

            if (!int.TryParse(userInput, out int returnInt))
            {
                Console.WriteLine($"Please enter a valid int number!");
            }
            else if (!listOfValidInputs.Contains(returnInt))
            {
                Console.WriteLine($"Please only enter a number in the given list of numbered choices!");
            }
            else
            {
                return returnInt;
            }

            if (ConsoleMethods.AllowGoingBackToMenu())
            {
                userWantsToGoBack = true;
                return 0;
            }
        }
    }

    /// <summary>
    /// Returns room number, works for both if you want the room number to be a new one or an existing one on file
    /// </summary>
    /// <param name="prompt"></param>
    /// <param name="shouldExistOnFile"></param>
    /// <param name="userWantsToGoBack">Indicates whether to prematurely end the submenu</param>
    /// <returns></returns>
    public static int AskRoomNumber(string prompt, bool shouldExistOnFile, out bool userWantsToGoBack)
    {
        userWantsToGoBack = false;

        while (true)
        {
            string userInput = ConsoleMethods.ReadLine(prompt);

            if (!int.TryParse(userInput, out int roomNumber))
            {
                Console.WriteLine("Please enter a valid number!");
            }
            else if (CurrentData.AlreadyExists(roomNumber) == shouldExistOnFile) // "it exists/doesn't, as it should"
            {
                return roomNumber;
            }
            else if (shouldExistOnFile)
            {
                Console.WriteLine("Please enter a room number that already exists on the file!");
            }
            else
            {
                Console.WriteLine("Please enter a room number that doesn't yet exist on the file!");
            }

            if (ConsoleMethods.AllowGoingBackToMenu()) // runs when the user wishes to go back to the main menu
            {
                userWantsToGoBack = true;
                return 0;
            }
        }
    }

    /// <summary>
    /// Returns room type. Only lets through if the room type exists on file. Case-insensitive to user input
    /// </summary>
    /// <param name="prompt"></param>
    /// <param name="userWantsToGoBack">Indicates whether to prematurely end the submenu</param>
    /// <returns></returns>
    public static RoomType AskRoomType(string prompt, out bool userWantsToGoBack)
    {
        RoomType roomType;
        userWantsToGoBack = false;

        while (true)
        {
            string userInput = ConsoleMethods.ReadLine(prompt);
            userInput = $"{char.ToUpper(userInput[0])}{userInput[1..].ToLower()}"; // removes case-sensitivity

            if (int.TryParse(userInput, out _)) // ints can always parse into enum, but don't let them!
            {
                Console.WriteLine("Please only enter the actual names of each room type. No numbers!");
            }
            else if (Enum.TryParse(userInput, out roomType)) // not int, and valid enum
            {
                return roomType;
            }
            else // not int, but invalid enum
            {
                Console.WriteLine("Please enter a room type that already exists on the file!");
            }

            if (ConsoleMethods.AllowGoingBackToMenu())
            {
                userWantsToGoBack = true;
                return 0;
            }
        }
    }

    /// <summary>
    /// Returns customer name, lets both (non-empty) duplicates and unique names through, unless specified that customer must already exist on file
    /// </summary>
    /// <param name="prompt"></param>
    /// <param name="customerHasToBeOnFile"></param>
    /// <param name="userWantsToGoBack">Indicates whether to prematurely end the submenu</param>
    /// <returns></returns>
    public static string AskCustomerName(string prompt, out bool userWantsToGoBack, bool customerHasToBeOnFile = false)
    {
        string customerName;
        userWantsToGoBack = false;

        while (true)
        {
            customerName = ConsoleMethods.ReadLine(prompt);

            if (customerHasToBeOnFile && !CurrentData.AlreadyExists(customerName))
            {
                Console.WriteLine("The customer needs to first be on file!");
            }
            else
            {
                return customerName;
            }

            if (ConsoleMethods.AllowGoingBackToMenu())
            {
                userWantsToGoBack = true;
                return "";
            }
        }
    }

    /// <summary>
    /// Returns card numbers, lets both unique ones and duplicates through
    /// </summary>
    /// <param name="prompt"></param>
    /// <param name="userWantsToGoBack">Indicates whether to prematurely end the submenu</param>
    /// <returns></returns>
    public static long AskCardNumber(string prompt, out bool userWantsToGoBack)
    {
        string userInput;
        userWantsToGoBack = false;

        while (true)
        {
            userInput = ConsoleMethods.ReadLine(prompt);

            if (!long.TryParse(userInput, out long cardNumber)) // not a long
            {
                Console.WriteLine("Must be a valid number!");
            }
            else if (long.TryParse(userInput, out _) && userInput.Length != 16) // long but not 16-digit
            {
                Console.WriteLine("Card number must be 16 digits!");
            }
            else
            {
                return cardNumber;
            }

            if (ConsoleMethods.AllowGoingBackToMenu())
            {
                userWantsToGoBack = true;
                return 0;
            }
        }
    }

    public static DateOnly AskDate(string prompt, out bool userWantsToGoBack)
    {
        userWantsToGoBack = false;

        while (true)
        {
            string userInput = ConsoleMethods.ReadLine(prompt);

            if (!DateOnly.TryParse(userInput, out DateOnly date))
            {
                Console.WriteLine("Please enter the date in the valid format! Ex: MM/DD/YYYY");
            }
            else
            {
                return date;
            }

            if (ConsoleMethods.AllowGoingBackToMenu())
            {
                userWantsToGoBack = true;
                return new DateOnly();
            }
        }
    }

    public static decimal AskDailyRate(string prompt, out bool userWantsToGoBack)
    {
        userWantsToGoBack = false;

        while (true)
        {
            string userInput = ConsoleMethods.ReadLine(prompt);

            if (!decimal.TryParse(userInput, out decimal dailyRate))
            {
                Console.WriteLine("Please enter a valid number!");
            }
            else
            {
                return dailyRate;
            }

            if (ConsoleMethods.AllowGoingBackToMenu())
            {
                userWantsToGoBack = true;
                return 0;
            }
        }
    }

    /// <summary>
    /// Returns coupon code, works for both if you want it to be a new one (creating a new coupon code) or an existing one on file (applying an existing coupon code)
    /// </summary>
    /// <param name="prompt"></param>
    /// <param name="shouldExistOnFile"></param>
    /// <param name="indexOfCouponCode"></param>
    /// <param name="userWantsToGoBack">Indicates whether to prematurely end the submenu</param>
    /// <returns></returns>
    public static string AskCouponCode(string prompt, bool shouldExistOnFile, out int indexOfCouponCode, out bool userWantsToGoBack)
    {
        userWantsToGoBack = false;

        while (true)
        {
            string userInput = ConsoleMethods.ReadLine(prompt);

            if (CurrentData.CouponCodeAlreadyExists(userInput, out indexOfCouponCode) == shouldExistOnFile) // "coupon code exists/doesn't, as it should"
            {
                return userInput;
            }
            else if (shouldExistOnFile) // "it should exist, but it doesn't"
            {
                Console.WriteLine("Please enter a coupon code that already exists on file!");
            }
            else // "it shouldn't exist, but it does"
            {
                Console.WriteLine("Please enter a coupon code that doesn't yet exist on file!");
            }

            if (ConsoleMethods.AllowGoingBackToMenu())
            {
                userWantsToGoBack = true;
                return "";
            }
        }
    }

    /// <summary>
    /// Returns in percentage form. Ex: user enters "25" --> returns 25 (not 0.25). Clarifies that the user understands this
    /// </summary>
    /// <param name="prompt"></param>
    /// <param name="header"></param>
    /// <param name="subtext"></param>
    /// <param name="userWantsToGoBack">Indicates whether to prematurely end the submenu</param>
    /// <returns></returns>
    public static decimal AskPercentage(string prompt, out bool userWantsToGoBack, string header = "Enter a new coupon code", string subtext = "")
    {
        userWantsToGoBack = false;

        while (true)
        {
            string userInput = ConsoleMethods.ReadLine(prompt);

            if (!decimal.TryParse(userInput, out decimal returnPercentage))
            {
                Console.WriteLine("Please enter a valid number!");

                if (ConsoleMethods.AllowGoingBackToMenu()) // fail to enter valid number: offer to go back
                {
                    userWantsToGoBack = true;
                    return 0;
                }

                continue;
            }

            Console.WriteLine($"\nDo you mean to enter {returnPercentage}%?");
            Console.WriteLine("y - yeah!");
            Console.WriteLine($"n - no, I was thinking of the decimal version {returnPercentage / 100}!");
            char userAnswer = GetUserChoice("\nPlease press a key that corresponds to your answer: ", new List<char> { 'y', 'n' }, out userWantsToGoBack);
            if (userWantsToGoBack) { return 0; }

            if (userAnswer == 'n')
            {
                Console.WriteLine("Press any key to try again");
                Console.ReadKey(true);
                ConsoleMethods.SetUpScreen(header, subtext);
            }
            else
            {
                return returnPercentage;
            }

        }
    }

    /// <summary>
    /// Returns whether the identified customer is a frequent traveler or not: Asks for the card number to differentiate between customers with the same name. Will only let through once a valid card number (one of the customers has the entered card number) is entered
    /// </summary>
    /// <param name="prompt"></param>
    /// <param name="listOfCustomersWithName"></param>
    /// <param name="userWantsToGoBack">Indicates whether to prematurely end the submenu</param>
    /// <returns></returns>
    public static bool ClarifyStatusOfNonUniqueCustomer(string prompt, List<(string customerName, long cardNumber, bool freqTravelerStatus)> listOfCustomersWithName, out bool userWantsToGoBack)
    {
        userWantsToGoBack = false;
        List<long> listOfValidCardNumbers = new();

        Console.WriteLine($"Below is the list of customers who share the name {listOfCustomersWithName[0].customerName}");
        Console.WriteLine($"\n{"Customer Name",-20} | {"Card Number",20} | {"FreqTraveler Status",-20}");
        Console.WriteLine("----------------------------------------------------------------------");
        foreach (var (customerName, cardNumber, freqTravelerStatus) in listOfCustomersWithName)
        {
            Console.WriteLine($"{customerName,-20} | {cardNumber,20} | {freqTravelerStatus,-20}");
            listOfValidCardNumbers.Add(cardNumber);
        }

        List<int> listOfValidUserChoices = new();
        // this will allow user to type a number that corresponds to their target card number, instead of having to write out the entire thing perfectly
        // how it'll work: the typed number will correspond to the index of each valid cardNumber in listOfValidCardNumbers
        Console.WriteLine("\nPick the card number of your interest");
        for (int index = 0; index < listOfValidCardNumbers.Count; index++)
        {
            Console.WriteLine($"{index} - {listOfValidCardNumbers[index]}");
            listOfValidUserChoices.Add(index);
        }

        int indexOfClarifyingCardNumber = GetUserChoice("\nPlease enter the number that corresponds to the numbered choices: ", listOfValidUserChoices, out userWantsToGoBack);
        if (userWantsToGoBack)
        {
            return false;
        }

        return listOfCustomersWithName[indexOfClarifyingCardNumber].freqTravelerStatus;
    }

    public static bool ClarifyStatusOfNonUniqueCustomer(List<(string customerName, long cardNumber, bool freqTravelerStatus)> listOfCustomersWithName, long firstCardNumber)
    {
        List<long> listOfValidCardNumbers = new();

        
        foreach (var (_, cardNumber, _) in listOfCustomersWithName)
        {
            listOfValidCardNumbers.Add(cardNumber);
        }

        // this will allow user to type a number that corresponds to their target card number, instead of having to write out the entire thing perfectly
        // how it'll work: the typed number will correspond to the index of each valid cardNumber in listOfValidCardNumbers
        int index = 0;
        foreach (var cardNumber in listOfValidCardNumbers)
        {
            if(cardNumber == firstCardNumber)
            {
                break;
            }
            index++;
        }

        return listOfCustomersWithName[index].freqTravelerStatus;
    }

    /// <summary>
    /// Returns the "clarifying reservation number". Can help differentiate between reservations made under the same customer name. Will only let through once a valid reservation number (one of the reservations has that reservation number) is entered
    /// </summary>
    /// <param name="promptBeforeDisplaying"></param>
    /// <param name="listOfReservationsUnderName"></param>
    /// <param name="indexOfReservation"></param>
    /// <param name="userWantsToGoBack">Indicates whether to prematurely end the submenu</param>
    /// <returns></returns>
    public static string ClarifyReservName(string promptBeforeDisplaying, List<(string reservationNumber, DateOnly dateStart, DateOnly dateStop, int roomNumber, string customerName, string paymentConfirmation, decimal chargedFees)> listOfReservationsUnderName, out int indexOfReservation, out bool userWantsToGoBack)
    {
        userWantsToGoBack = false;
        List<string> listOfValidReservNumbers = new();

        Console.WriteLine(promptBeforeDisplaying);
        Console.WriteLine($"\n{"Reservation Number",40} | {"Date Start",15} | {"Date Stop",15} | {"Room Number",13} | {"Customer Name",-25} | {"PaymentConfirmation",-32} | {"Charged Fees",10}");
        Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
        foreach (var (reservationNumber, dateStart, dateStop, roomNumber, customerName, paymentConfirmation, chargedFees) in listOfReservationsUnderName)
        {
            Console.WriteLine($"{reservationNumber,40} | {dateStart,15} | {dateStop,15} | {roomNumber,13} | {customerName,-25} | {paymentConfirmation,-32} | {"$" + chargedFees,10}");
            listOfValidReservNumbers.Add(reservationNumber);
        }

        List<int> listOfValidUserChoices = new();
        // this will allow user to type a number that corresponds to their target reservation number, instead of having to write out the entire thing perfectly
        // how it'll work: the typed number will correspond to the index of each valid reservNumber in listOfValidReservNumbers
        Console.WriteLine("\nPick the reservation number of your interest");
        for (int index = 0; index < listOfValidReservNumbers.Count; index++)
        {
            Console.WriteLine($"  {index} - {listOfValidReservNumbers[index]}");
            listOfValidUserChoices.Add(index);
        }

        int indexOfClarifyingReservNumber = GetUserChoice("Please enter the number that corresponds to the numbered choices: ", listOfValidUserChoices, out userWantsToGoBack);
        if (userWantsToGoBack)
        {
            indexOfReservation = -1;
            return "";
        }

        indexOfReservation = indexOfClarifyingReservNumber;

        return listOfValidReservNumbers[indexOfClarifyingReservNumber];
    }
}

/// <summary>
/// Same code as if I was to put these directly in Main(), but supports the switch statements because it allows me to repeat identifier names this way
/// </summary>
public static class SubMenuBundles
{
    public static void NewRoom()
    {
        bool returnToMenuNow;

        ConsoleMethods.SetUpScreen("Entering a new room");
        int roomNumber = RequestUserInput.AskRoomNumber("\nPlease enter the new room's room number: ", false, out returnToMenuNow);
        if (returnToMenuNow) { return; }

        RoomType roomType = RequestUserInput.AskRoomType("\nPlease enter the new room's room type: ", out returnToMenuNow);
        if (returnToMenuNow) { return; }

        CurrentData.RoomsList.Add((roomNumber, roomType));
        FileManager.WriteUpRooms(CurrentData.SerializeData(CurrentData.RoomsList));

        ConsoleMethods.EndingMessage("\nRoom added!");
    }

    public static void NewCustomer()
    {
        bool returnToMenuNow;
        ConsoleMethods.SetUpScreen("Entering a new customer");
        string customerName;
        long cardNumber;
        while (true)
        {
            customerName = RequestUserInput.AskCustomerName("\nWhat is the customer's name? ", userWantsToGoBack: out returnToMenuNow);
            if (returnToMenuNow) { return; }

            cardNumber = RequestUserInput.AskCardNumber("\nWhat is the customer's 16-digit card number? ", out returnToMenuNow);
            if (returnToMenuNow) { return; }

            var listOfCustomersWithName = HelperFunctions.ListOfCustomersWithName(customerName);

            // do not accept if BOTH customerName and cardNumber are duplicate of the same customer in Customers.txt. Ruins uniqueness & causes funky things when it comes to looking up a customer
            if (listOfCustomersWithName.Count >= 1 && HelperFunctions.CardNumberIsInHere(cardNumber, listOfCustomersWithName))
            {
                Console.WriteLine($"There are {listOfCustomersWithName.Count} customers on file with the name {customerName}. One of them already has the card number {cardNumber}!");
            }
            else
            {
                break;
            }
        }

        Console.WriteLine("\nIs this customer a Frequent Traveler?");
        Console.WriteLine("\n\ty - yes, they are a frequent traveler (discounts!)");
        Console.WriteLine("\tn - no, they are not a frequent traveler (no discounts...)");
        char userAnswer = RequestUserInput.GetUserChoice("\nPlease press the letter that corresponds to your answer: ", new List<char> { 'y', 'n' }, out returnToMenuNow);
        if (returnToMenuNow) { return; }

        bool freqTravelerStatus = userAnswer switch
        {
            'y' => true,
            'n' => false
        };


        CurrentData.CustomersList.Add((customerName, cardNumber, freqTravelerStatus));
        FileManager.WriteUpCustomers(CurrentData.SerializeData(CurrentData.CustomersList));

        ConsoleMethods.EndingMessage("\nCustomer added to file!");
    }

    public static string NewCustomer(string customerName, long cardNumber, bool freqTravelerStatus)
    {
        bool returnToMenuNow;
        while (true)
        {
            var listOfCustomersWithName = HelperFunctions.ListOfCustomersWithName(customerName);

            // do not accept if BOTH customerName and cardNumber are duplicate of the same customer in Customers.txt. Ruins uniqueness & causes funky things when it comes to looking up a customer
            if (listOfCustomersWithName.Count >= 1 && HelperFunctions.CardNumberIsInHere(cardNumber, listOfCustomersWithName))
            {
                return $"There are {listOfCustomersWithName.Count} customers on file with the name {customerName}. One of them already has the card number {cardNumber}!";
            }
            else
            {
                break;
            }
        }


        CurrentData.CustomersList.Add((customerName, cardNumber, freqTravelerStatus));
        FileManager.WriteUpCustomers(CurrentData.SerializeData(CurrentData.CustomersList));

        return "Customer added to file!";
    }

    public static void NewReservation()
    {
        bool returnToMenuNow;
        ConsoleMethods.SetUpScreen(header: "Entering a new reservation", subtext: $"Reservation number and payment confirmation will be generated by the system.\n\tReminder: discount rate for Frequent Travelers is {CurrentData.DisCountPercentageForFreqTravelers}%");
        string reservationNumber = Guid.NewGuid().ToString();
        DateOnly dateStart;
        DateOnly dateStop;
        int roomNumber;
        decimal chargedFees;
        bool customerIsFreqTraveler;

        while (true)
        {
            dateStart = RequestUserInput.AskDate("\nPlease enter the desired start date: ", out returnToMenuNow);
            if (returnToMenuNow) { return; }

            dateStop = RequestUserInput.AskDate("\nPlease enter the desired stop date: ", out returnToMenuNow);
            if (returnToMenuNow) { return; }

            if (dateStart > dateStop)
            {
                Console.WriteLine("End date should not come before the starting date!");
                continue;
            }

            if (CurrentData.UnreservedRoomNumbers(CurrentData.ReservationsDuring(dateStart, dateStop)).Count == 0)
            {
                Console.WriteLine($"None of our rooms are available during {dateStart} - {dateStop}!");
                continue;
            }

            roomNumber = RequestUserInput.AskRoomNumber("\nPlease enter the desired room number: ", true, out returnToMenuNow);
            if (returnToMenuNow) { return; }

            if (!CurrentData.OverlapWithExistingReservation(dateStart, dateStop, roomNumber))
            {
                break;
            }
            else
            {
                Console.WriteLine("That room is already reserved during that time!");
            }
        }

        string customerName = RequestUserInput.AskCustomerName("\nPlease enter the customer's name: ", customerHasToBeOnFile: true, userWantsToGoBack: out returnToMenuNow);
        if (returnToMenuNow) { return; }

        customerIsFreqTraveler = CurrentData.IsFreqTraveler(customerName);

        var listOfCustomersWithName = HelperFunctions.ListOfCustomersWithName(customerName);

        if (listOfCustomersWithName.Count > 1) // duplicate: needs more info for accurate identification to determine chargedFees()
        {
            Console.WriteLine($"\nThere is more than 1 customer under the name {customerName}. A specific card number is required to identify the exact customer who is making this reservation");
            customerIsFreqTraveler = RequestUserInput.ClarifyStatusOfNonUniqueCustomer(prompt: "\nWhat is the target customer's card number? ", listOfCustomersWithName: HelperFunctions.ListOfCustomersWithName(customerName), out returnToMenuNow);
            if (returnToMenuNow) { return; }
        }

        chargedFees = HelperFunctions.ApplyDiscount(customerIsFreqTraveler, dateStart, dateStop, roomNumber);

        string paymentConfirmation = HelperFunctions.RandomStringGenerator(30);

        CurrentData.ReservationsList.Add((reservationNumber, dateStart, dateStop, roomNumber, customerName, paymentConfirmation, chargedFees));
        FileManager.WriteUpReservations(CurrentData.SerializeData(CurrentData.ReservationsList));

        ConsoleMethods.EndingMessage("\nReservation added to file!");
    }

    public static string NewReservation(DateOnly dateStart, DateOnly dateStop, int roomNumber, string customerName, long cardNumber)
    {
        decimal chargedFees;
        string reservationNumber = Guid.NewGuid().ToString();
        bool customerIsFreqTraveler;

        while (true)
        {

            if (dateStart > dateStop)
            {
                return "End date should not come before the starting date!";
            }

            if (CurrentData.UnreservedRoomNumbers(CurrentData.ReservationsDuring(dateStart, dateStop)).Count == 0)
            {
                return $"None of our rooms are available during {dateStart} - {dateStop}!";
            }

            if (!CurrentData.OverlapWithExistingReservation(dateStart, dateStop, roomNumber))
            {
                break;
            }
            else
            {
                return "That room is already reserved during that time!";
            }
        }

        
        customerIsFreqTraveler = CurrentData.IsFreqTraveler(customerName);

        var listOfCustomersWithName = HelperFunctions.ListOfCustomersWithName(customerName);

        if (listOfCustomersWithName.Count > 1) // duplicate: needs more info for accurate identification to determine chargedFees()
        {
            customerIsFreqTraveler = RequestUserInput.ClarifyStatusOfNonUniqueCustomer(listOfCustomersWithName, cardNumber);
        }

        chargedFees = HelperFunctions.ApplyDiscount(customerIsFreqTraveler, dateStart, dateStop, roomNumber);

        string paymentConfirmation = HelperFunctions.RandomStringGenerator(30);

        CurrentData.ReservationsList.Add((reservationNumber, dateStart, dateStop, roomNumber, customerName, paymentConfirmation, chargedFees));
        FileManager.WriteUpReservations(CurrentData.SerializeData(CurrentData.ReservationsList));
        return "Reservation added to file!";
    }

    public static void AvailableRoomSearch()
    {
        bool returnToMenuNow;
        ConsoleMethods.SetUpScreen(header: "Available Room Search", subtext: "Prints out rooms that are unreserved on the given date range");

        DateOnly dateStart;
        DateOnly dateStop;
        while (true)
        {
            dateStart = RequestUserInput.AskDate("\nPlease enter the desired start date: ", out returnToMenuNow);
            if (returnToMenuNow) { return; }

            dateStop = RequestUserInput.AskDate("\nPlease enter the desired stop date: ", out returnToMenuNow);
            if (returnToMenuNow) { return; }

            if (dateStart > dateStop)
            {
                Console.WriteLine("Starting date should not come before the end date!");
                continue;
            }
            break;
        }

        ConsoleMethods.SetUpScreen(header: "Available Room Search", subtext: $"Printed below are unreserved rooms during {dateStart} - {dateStop}\n");
        Console.WriteLine($"{"Room Number",13} | {"Room Type",-13}");
        Console.WriteLine("----------------------------------");
        foreach (var unreservedRoomNumber in CurrentData.UnreservedRoomNumbers(CurrentData.ReservationsDuring(dateStart, dateStop)))
        {
            Console.WriteLine($"{unreservedRoomNumber,13} | {HelperFunctions.RoomTypeOfRoomNumber(unreservedRoomNumber),-13}");
        }

        ConsoleMethods.EndingMessage();
    }

    public static void ReservationReport()
    {
        bool returnToMenuNow;
        ConsoleMethods.SetUpScreen(header: "Reservation Report", subtext: "Prints out rooms that are reserved on the given date range");

        DateOnly dateStart;
        DateOnly dateStop;
        while (true)
        {
            dateStart = RequestUserInput.AskDate("\nPlease enter the desired start date: ", out returnToMenuNow);
            if (returnToMenuNow) { return; }

            dateStop = RequestUserInput.AskDate("\nPlease enter the desired stop date: ", out returnToMenuNow);
            if (returnToMenuNow) { return; }

            if (dateStart > dateStop)
            {
                Console.WriteLine("Starting date should not come before the end date!");
                continue;
            }
            break;
        }

        ConsoleMethods.SetUpScreen(header: "Reservation Report", subtext: $"Printed below are reserved rooms during {dateStart} - {dateStop}");
        Console.WriteLine($"\n{"Reservation Number",38} | {"Date Start",12} | {"Date Stop",12} | {"Room",6} | {"Customer",-23} | {"Payment Confirmation",-32} | {"Charged Fees",12}");
        Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------------------------------");
        foreach (var reservedRoom in CurrentData.ReservationsDuring(dateStart, dateStop))
        {
            Console.WriteLine($"{reservedRoom.reservationNumber,38} | {reservedRoom.dateStart,12} | {reservedRoom.dateStop,12} | {reservedRoom.roomNumber,6} | {reservedRoom.customerName,-23} | {reservedRoom.paymentConfirmation,-32} | {"$" + reservedRoom.chargedFees,12}");
        }

        ConsoleMethods.EndingMessage();
    }

    public static void CustomerReservationReport()
    {
        bool returnToMenuNow;

        ConsoleMethods.SetUpScreen(header: "Customer Reservation Report", subtext: "Prints either past and future reservations made for the specified customer name");
        string customerName = RequestUserInput.AskCustomerName("\nPlease enter the name of the customer: ", customerHasToBeOnFile: true, userWantsToGoBack: out returnToMenuNow);
        if (returnToMenuNow) { return; }

        bool reservationIsInFuture;
        Console.WriteLine("\nBelow are your options: ");
        Console.WriteLine(" a - Prior Reservations");
        Console.WriteLine(" b - Future Reservations");

        char userAnswer = RequestUserInput.GetUserChoice("Please press the key that corresponds to the desired task: ", new List<char> { 'a', 'b' }, out returnToMenuNow);
        if (returnToMenuNow) { return; }

        reservationIsInFuture = userAnswer switch
        {
            'a' => false,
            'b' => true
        };

        string reservationMessage = userAnswer switch
        {
            'a' => "prior",
            'b' => "future"
        };

        ConsoleMethods.SetUpScreen(header: "Customer Reservation Report", subtext: $"Printed below are {reservationMessage} reservations for customer(s) with name \"{customerName}\"");
        Console.WriteLine($"\n{"Reservation Number",38} | {"Date Start",12} | {"Date Stop",12} | {"Room",6} | {"Customer",-22} | {"Payment Confirmation",-32} | {"Charged Fees",15}");
        Console.WriteLine("--------------------------------------------------------------------------------------------------------------------------------------------------------------");
        foreach (var reservation in CurrentData.ReservationsOfCustomer(customerName, reservationIsInFuture))
        {
            Console.WriteLine($"{reservation.reservationNumber,38} | {reservation.dateStart,12} | {reservation.dateStop,12} | {reservation.roomNumber,6} | {reservation.customerName,-22} | {reservation.paymentConfirmation,-32} | {"$" + reservation.chargedFees,15}");
        }

        ConsoleMethods.EndingMessage();
    }

    public static void ChangeRoomPrice()
    {
        bool returnToMenuNow;
        ConsoleMethods.SetUpScreen(header: "Changing the daily rate of a specified room type");

        RoomType roomType;
        decimal targetDailyRate;
        decimal cleaningCost;

        roomType = RequestUserInput.AskRoomType("\nWhich room type would you like to change the daily rate of? ", out returnToMenuNow);
        if (returnToMenuNow) { return; }

        int roomTypeIndex = HelperFunctions.IndexOfRoomType(roomType);
        cleaningCost = CurrentData.RoomPricesList[roomTypeIndex].dailyCleaningCost;

        while (true)
        {
            targetDailyRate = RequestUserInput.AskDailyRate($"\nWhat daily rate would you like to change the {roomType} room type to? ", out returnToMenuNow);
            if (returnToMenuNow) { return; }

            if (targetDailyRate == CurrentData.RoomPricesList[roomTypeIndex].dailyRate)
            {
                Console.WriteLine($"{roomType} already has a daily rate of {targetDailyRate}!");
            }
            else
            {
                break;
            }
        }

        HelperFunctions.ReplaceRoomPriceTuple(roomTypeIndex, (roomType, targetDailyRate, cleaningCost));
        FileManager.WriteUpRoomPrices(CurrentData.SerializeData(CurrentData.RoomPricesList));

        ConsoleMethods.EndingMessage("\nRoom price updated!");
    }

    public static void RefundReserv()
    {
        bool returnToMenuNow;
        ConsoleMethods.SetUpScreen
                (header: "Refund a reservation",
                subtext: "100% refunds and cancels the reservation of specified customer name. If there is more than 1 under the specified name, an identifying reservation number is needed"
                );

        string clarifyingReservNumber = "-1"; // default --> the IndexOfReserv() method won't care for the reservation number if it is passed in as -1
        string customerName;

        while (true)
        {
            customerName = RequestUserInput.AskCustomerName("\nPlease enter the name of the customer: ", customerHasToBeOnFile: true, userWantsToGoBack: out returnToMenuNow);
            if (returnToMenuNow) { return; }

            if (HelperFunctions.ListOfReservationsUnderName(customerName).Count == 0)
            {
                Console.WriteLine($"Customer \"{customerName}\" does not have any reservation on file!");
            }
            else if (HelperFunctions.ListOfReservationsUnderName(customerName).Count > 1)
            {
                ConsoleMethods.SetUpScreen
                (header: "Refund a reservation",
                subtext: "100% refunds and cancels the reservation of specified customer name. If there is more than 1 under the specified name, an identifying reservation number is needed"
                );

                Console.WriteLine($"\nThere is more than 1 reservation made under the name \"{customerName}\"!");
                clarifyingReservNumber = RequestUserInput.ClarifyReservName($"\nBelow is the list of reservations all under this name", HelperFunctions.ListOfReservationsUnderName(customerName), out int _, out returnToMenuNow);
                if (returnToMenuNow) { return; }

                break;
            }
            else
            {
                break;
            }
        }

        int indexOfReservation = HelperFunctions.IndexOfReserv(customerName, clarifyingReservNumber);
        CurrentData.MoveReservationToRefundsList(indexOfReservation);
        FileManager.WriteUpReservations(CurrentData.SerializeData(CurrentData.ReservationsList));
        FileManager.WriteUpRefundsList(CurrentData.SerializeData(CurrentData.RefundsList));

        ConsoleMethods.EndingMessage("\nReservation moved to refunds file!");
    }

    public static void ApplyCoupon()
    {
        bool returnToMenuNow;
        ConsoleMethods.SetUpScreen
                (header: "Apply coupon code",
                subtext: "Coupon code will discount the specified reservation's charged fees by a certain percentage.\n\tReservation is looked up by reservation number"
                );

        string reservNumber;
        int indexOfReservation;
        string applyingCouponCode;
        int indexOfApplyingCouponCode;

        if (CurrentData.ReservationsList.Count == 0)
        {
            Console.WriteLine("\nThere are no reservations to work with! Press any key to continue to the main menu");
            Console.ReadKey();
            return;
        }

        while (true)
        {
            ConsoleMethods.SetUpScreen
            (header: "Apply coupon code",
            subtext: "Coupon code will discount the specified reservation's charged fees by a certain percentage.\n\tReservation is looked up by customer name, but if there is more than 1 under the specified name, an identifying reservation number is needed"
            );

            while (true)
            {

                reservNumber = RequestUserInput.ClarifyReservName("\nBelow is a list of all reservations. Please pick a reservation that has not ended already!", CurrentData.ReservationsList, indexOfReservation: out indexOfReservation, userWantsToGoBack: out returnToMenuNow);
                if (returnToMenuNow) { return; }

                // do not let through if that specified reservation is a past reservation
                if (HelperFunctions.ReservIsInThePast(indexOfReservation))
                {
                    Console.WriteLine("We do not apply coupons to past reservations!");
                    Console.WriteLine("Press any key to try again");
                    Console.ReadKey(true);
                }
                else
                {
                    break;
                }
            }

            applyingCouponCode = RequestUserInput.AskCouponCode("\nWhat coupon code would they like to apply? ", true, indexOfCouponCode: out indexOfApplyingCouponCode, userWantsToGoBack: out returnToMenuNow);
            if (returnToMenuNow) { return; }

            if (!CurrentData.ThisReservationHasUsedThisCode(reservNumber, applyingCouponCode)) // only lets through if this reservation has not used the code before
            {
                break;
            }
            else
            {
                Console.WriteLine("A reservation cannot use the same coupon code more than once!");
                Console.WriteLine("\nPress any key to try again");
                Console.ReadKey(true);
            }
        }

        decimal newChargedFees = HelperFunctions.ApplyDiscount(initialChargedFees: CurrentData.ReservationsList[indexOfReservation].chargedFees, indexOfApplyingCouponCode);

        HelperFunctions.ReplaceReservationChargedFees(indexOfReservation, newChargedFees);
        FileManager.WriteUpReservations(CurrentData.SerializeData(CurrentData.ReservationsList));

        CurrentData.CouponRedemptionList.Add((reservNumber, applyingCouponCode));
        FileManager.WriteUpCouponRedemptionList(CurrentData.SerializeData(CurrentData.CouponRedemptionList));

        ConsoleMethods.EndingMessage("\nCoupon code applied!");
    }


    public static void CreateNewCoupon()
    {
        bool returnToMenuNow;
        ConsoleMethods.SetUpScreen("Enter a new coupon code");

        decimal couponDiscountInPercentage = RequestUserInput.AskPercentage("\nPlease enter the desired discount percentage for the new coupon code: ", userWantsToGoBack: out returnToMenuNow);
        if (returnToMenuNow) { return; }

        string couponCode = RequestUserInput.AskCouponCode("\nPlease enter the new coupon code: ", false, out int _, out returnToMenuNow);
        if (returnToMenuNow) { return; }


        CurrentData.CouponCodesList.Add((couponCode, couponDiscountInPercentage));
        FileManager.WriteUpCouponCodesList(CurrentData.SerializeData(CurrentData.CouponCodesList));

        ConsoleMethods.EndingMessage("\nCoupon code added!");
    }

    public static void CouponCodesReport()
    {
        bool returnToMenuNow;
        ConsoleMethods.SetUpScreen(header: "Coupon code report", subtext: "Prints out list of usable coupon codes / list of coupon code usage history");

        Console.WriteLine("\nWhich would you like to print?");
        Console.WriteLine("\na - usable coupon codes\nb - coupon code usage history");
        char userAnswer = RequestUserInput.GetUserChoice("\nPlease press a key that corresponds to your choice: ", new List<char> { 'a', 'b' }, out returnToMenuNow);
        if (returnToMenuNow) { return; }

        if (userAnswer == 'a')
        {
            ConsoleMethods.SetUpScreen(header: "Coupon code report", subtext: "Prints out list of usable coupon codes");

            Console.WriteLine("\nBelow is the list of all usable coupon codes:\n");
            Console.WriteLine($"{"Coupon Code",-45} | {"Discount",13}");
            Console.WriteLine("-----------------------------------------------------------------");
            foreach (var (couponCode, couponDiscountPercentage) in CurrentData.CouponCodesList)
            {
                Console.WriteLine($"{couponCode,-45} | {couponDiscountPercentage,12}%");
            }

            ConsoleMethods.EndingMessage();
        }
        else
        {
            ConsoleMethods.SetUpScreen(header: "Coupon code report", subtext: "Prints out list of coupon code usage history");

            Console.WriteLine("\nBelow is the list of coupon code usage history\n");
            Console.WriteLine($"{"Reservation Num",40} | {"Code Applied",-45}");
            Console.WriteLine("-------------------------------------------------------------------------------------");
            foreach (var (reservationNumberApplied, couponCodeUsed) in CurrentData.CouponRedemptionList)
            {
                Console.WriteLine($"{reservationNumberApplied,40} | {couponCodeUsed,-45}");
            }

            ConsoleMethods.EndingMessage();
        }
    }

    public static void UtilizationReport() // combines Util Report & Cost Tracking A-la-cart features together
    {
        bool returnToMenuNow;
        ConsoleMethods.SetUpScreen(header: "Utilization report", subtext: "Based on specified day(s), prints out whether each room is used or not with collected rental fees & cleaning costs & occupancy rate");

        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("\nNote: If you wish to view only a single day, simply enter that day as both start date and stop date");
        Console.ForegroundColor = ConsoleColor.Gray;
        DateOnly dateStart;
        DateOnly dateStop;

        while (true)
        {
            dateStart = RequestUserInput.AskDate("\nWhat would you like to be the start date? ", out returnToMenuNow);
            if (returnToMenuNow) { return; }

            dateStop = RequestUserInput.AskDate("\nWhat would you like to be the stop date? ", out returnToMenuNow);
            if (returnToMenuNow) { return; }

            if (dateStart > dateStop)
            {
                Console.WriteLine("Start date cannot come after stop date!");
            }
            else
            {
                break;
            }
        }

        var utilList = CurrentData.ListOfEachDateReport(dateStart, dateStop, out decimal totalRevenueOfDateRange, out decimal totalCleaningCostsOfDateRange);
        ConsoleMethods.SetUpScreen(header: "Utilization report", subtext: $"Total revenue incurred during date(s) {dateStart} - {dateStop}: ${totalRevenueOfDateRange}\n\tTotal cleaning costs incurred during date(s) {dateStart} - {dateStop}: ${totalCleaningCostsOfDateRange}\n");
        foreach (var dateReport in utilList) // separate each date report
        {
            ConsoleMethods.DisplayDateReport(dateReport);
        }

        ConsoleMethods.EndingMessage();
    }
}