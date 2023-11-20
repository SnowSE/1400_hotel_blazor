using Hotel.Logic;
using Hotel.UI;
using Hotel.Data;

try
{
    CurrentData.DeserializeData();
}
catch (FormatException fileLineError)
{
    ConsoleMethods.SetUpScreen(header: "ERROR", headerColor: ConsoleColor.Red);
    Console.WriteLine($"{fileLineError.Message} Please change the line before proceeding with the program");
    return;
}

// "Clean Slate" option 
ConsoleMethods.SetUpScreen(header: "Clean-slate the .txt files", subtext: "Clearing every file except RoomPrices.txt");

Console.WriteLine("\nWould you like to clean-slate the files?");
Console.WriteLine("\n   y - clear every .txt file except RoomPrices.txt! (Every other file has an in-program option to add items to)");
Console.WriteLine("   n - nope! keep it what it is");
char clearSlateAnswer = RequestUserInput.GetUserChoice("\nPlease press the key that corresponds to your choice: ", new List<char> { 'y', 'n' }, out bool _, allowReturnToMenu: false);

if (clearSlateAnswer == 'y')
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"\nThis will erase potentially important data. If you are 100%, enter \"CleanSlate\" to proceed. Otherwise, this will be skipped");
    Console.ForegroundColor = ConsoleColor.Gray;
    string clearSlateConfirm = Console.ReadLine();

    if (clearSlateConfirm == "CleanSlate")
    {
        CurrentData.RoomsList.Clear();
        CurrentData.ReservationsList.Clear();
        CurrentData.CustomersList.Clear();
        CurrentData.RefundsList.Clear();
        CurrentData.CouponCodesList.Clear();
        CurrentData.CouponRedemptionList.Clear();

        FileManager.WriteUpRooms(CurrentData.SerializeData(CurrentData.RoomsList));
        FileManager.WriteUpReservations(CurrentData.SerializeData(CurrentData.ReservationsList));
        FileManager.WriteUpCustomers(CurrentData.SerializeData(CurrentData.CustomersList));
        FileManager.WriteUpRefundsList(CurrentData.SerializeData(CurrentData.RefundsList));
        FileManager.WriteUpCouponCodesList(CurrentData.SerializeData(CurrentData.CouponCodesList));
        FileManager.WriteUpCouponRedemptionList(CurrentData.SerializeData(CurrentData.CouponRedemptionList));
    }
}

while (true) // will loop forever until the user types the option to exit
{
    try
    {
        CurrentData.DeserializeData();
    }
    catch (FormatException fileLineError)
    {
        ConsoleMethods.SetUpScreen(header: "ERROR", headerColor: ConsoleColor.Red);
        Console.WriteLine($"{fileLineError.Message} Please change the line before proceeding with the program");
        return;
    }

        // Main menu: Display menu-style options
        ConsoleMethods.SetUpScreen("Main menu");
        ConsoleMethods.DisplayMainMenu();
        char userChoice = RequestUserInput.GetUserChoice("\nPlease press your desired task's corresponding key: ", new List<char> { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', '0' }, out bool _, allowReturnToMenu: false);

        // Action based on user input
        switch (userChoice)
        {
            case '0':
                return;
            case 'a':
                SubMenuBundles.NewRoom();
                break;
            case 'b':
                SubMenuBundles.NewCustomer();
                break;
            case 'c':
                SubMenuBundles.NewReservation();
                break;
            case 'd':
                SubMenuBundles.AvailableRoomSearch();
                break;
            case 'e':
                SubMenuBundles.ReservationReport();
                break;
            case 'f':
                SubMenuBundles.CustomerReservationReport();
                break;
            case 'g':
                SubMenuBundles.ChangeRoomPrice();
                break;
            case 'h':
                SubMenuBundles.RefundReserv();
                break;
            case 'i':
                SubMenuBundles.ApplyCoupon();
                break;
            case 'j':
                SubMenuBundles.CreateNewCoupon();
                break;
            case 'k':
                SubMenuBundles.CouponCodesReport();
                break;
            case 'l':
                SubMenuBundles.UtilizationReport();
                break;
        }
    }