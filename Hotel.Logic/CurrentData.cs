using System.Text;
using Hotel.Data;

namespace Hotel.Logic;

public static class CurrentData
{
    public static List<(int roomNumber, RoomType roomType)> RoomsList = new();
    public static List<(string reservationNumber, DateOnly dateStart, DateOnly dateStop, int roomNumber, string customerName, string paymentConfirmation, decimal chargedFees)> ReservationsList = new();
    public static List<(string customerName, long cardNumber, bool freqTravelerStatus)> CustomersList = new();
    public static List<(RoomType roomType, decimal dailyRate, decimal dailyCleaningCost)> RoomPricesList = new();
    public static List<(string reservationNumber, DateOnly dateStart, DateOnly dateStop, int roomNumber, string customerName, string paymentConfirmation, decimal chargedFees)> RefundsList = new();
    public static List<(string couponCode, decimal couponDiscountPercentage)> CouponCodesList = new();
    public static List<(string reservationNumberApplied, string couponCodeUsed)> CouponRedemptionList = new();

    public const decimal DisCountPercentageForFreqTravelers = 15m; // means 15%

    /// <summary>
    /// Loads in data in the 7 files into their corresponding lists
    /// </summary>
    public static void DeserializeData()
    {
        // checking/safeguarding stage:

        CatchFileNotFound("Rooms.txt");
        CatchFileNotFound("Reservations.txt");
        CatchFileNotFound("Customers.txt");
        CatchFileNotFound("RoomPrices.txt");
        CatchFileNotFound("Refunds.txt");
        CatchFileNotFound("CouponCodes.txt");
        CatchFileNotFound("CouponRedemption.txt");

        // code will only reach here once the files are where they need to be

        RoomsList = FileManager.ReadInRooms(FileManager.FindFile("Rooms.txt"));
        ReservationsList = FileManager.ReadInReservations(FileManager.FindFile("Reservations.txt"));
        CustomersList = FileManager.ReadInCustomers(FileManager.FindFile("Customers.txt"));
        RoomPricesList = FileManager.ReadInRoomPrices(FileManager.FindFile("RoomPrices.txt"));
        RefundsList = FileManager.ReadInRefunds(FileManager.FindFile("Refunds.txt"));
        CouponCodesList = FileManager.ReadInCouponCodes(FileManager.FindFile("CouponCodes.txt"));
        CouponRedemptionList = FileManager.ReadInCouponRedemptions(FileManager.FindFile("CouponRedemption.txt"));
    }

    /// <summary>
    /// Calls FileManager as necessary to take steps in fixing issue addressed by FileNotFoundException.
    /// Could've all just been in DeserializeData(), but it'll look really ugly
    /// </summary>
    /// <param name="fileName">Name of file you want to safeguard</param>
    public static void CatchFileNotFound(string fileName)
    {
        try
        {
            FileManager.FindFile(fileName);
        }
        catch (FileNotFoundException)
        {
            FileManager.CreateFileAtRoot(fileName);
        }
    }

    /// <summary>
    /// Unlike some other methods that deal with the 7 lists, this one is not hard-coded to use Current.Data's lists
    /// </summary>
    /// <param name="RoomsList"></param>
    /// <returns></returns>
    public static string SerializeData(List<(int roomNumber, RoomType roomType)> RoomsList)
    {
        StringBuilder buildingString = new();

        foreach ((int roomNumber, RoomType roomType) in RoomsList)
        {
            buildingString.AppendLine($"{roomNumber.ToString()},{roomType.ToString()}");
        }

        return buildingString.ToString();
    }

    /// <summary>
    /// Usable for both ReservationsList and RefundsList. Unlike some other methods that deal with the 7 lists, this one is not hard-coded to use Current.Data's lists
    /// </summary>
    /// <param name="ReservationsList"></param>
    /// <returns></returns>
    public static string SerializeData(List<(string reservationNumber, DateOnly dateStart, DateOnly dateStop, int roomNumber, string customerName, string paymentConfirmation, decimal chargedFees)> ReservationsORRefundsList)
    {
        StringBuilder buildingString = new();

        foreach ((string reservationNumber, DateOnly dateStart, DateOnly dateStop, int roomNumber, string customerName, string paymentConfirmation, decimal chargedFees) in ReservationsORRefundsList)
        {
            buildingString.AppendLine($"{reservationNumber.ToString()},{dateStart.ToString()},{dateStop.ToString()},{roomNumber.ToString()},{customerName.ToString()},{paymentConfirmation.ToString()},{chargedFees.ToString()}");
        }

        return buildingString.ToString();
    }

    /// <summary>
    /// Unlike some other methods that deal with the 7 lists, this one is not hard-coded to use Current.Data's lists
    /// </summary>
    /// <param name="CustomersList"></param>
    /// <returns></returns>
    public static string SerializeData(List<(string customerName, long cardNumber, bool freqTravelerStatus)> CustomersList)
    {
        StringBuilder buildingString = new();

        foreach ((string customerName, long cardNumber, bool freqTravelerStatus) in CustomersList)
        {
            buildingString.AppendLine($"{customerName.ToString()},{cardNumber.ToString()},{freqTravelerStatus.ToString()}");
        }

        return buildingString.ToString();
    }

    /// <summary>
    /// Unlike some other methods that deal with the 7 lists, this one is not hard-coded to use Current.Data's lists
    /// </summary>
    /// <param name="RoomPricesList"></param>
    /// <returns></returns>
    public static string SerializeData(List<(RoomType roomType, decimal dailyRate, decimal dailyCleaningCost)> RoomPricesList)
    {
        StringBuilder buildingString = new();

        foreach ((RoomType roomType, decimal dailyRate, decimal cleaningCost) in RoomPricesList)
        {
            buildingString.AppendLine($"{roomType.ToString()},{dailyRate.ToString()},{cleaningCost.ToString()}");
        }

        return buildingString.ToString();
    }

    /// <summary>
    /// Unlike some other methods that deal with the 7 lists, this one is not hard-coded to use Current.Data's lists
    /// </summary>
    /// <param name="CouponCodesList"></param>
    /// <returns></returns>
    public static string SerializeData(List<(string couponCode, decimal couponDiscountPercentage)> CouponCodesList)
    {
        StringBuilder buildingString = new();

        foreach ((string couponCode, decimal couponDiscountPercentage) in CouponCodesList)
        {
            buildingString.AppendLine($"{couponCode},{couponDiscountPercentage}");
        }

        return buildingString.ToString();
    }

    /// <summary>
    /// Unlike some other methods that deal with the 7 lists, this one is not hard-coded to use Current.Data's lists
    /// </summary>
    /// <param name="CouponRedemptionList"></param>
    /// <returns></returns>
    public static string SerializeData(List<(string reservationNumberApplied, string couponCodeUsed)> CouponRedemptionList)
    {
        StringBuilder buildingString = new();

        foreach ((string reservationNumberApplied, string couponCodeUsed) in CouponRedemptionList)
        {
            buildingString.AppendLine($"{reservationNumberApplied},{couponCodeUsed}");
        }

        return buildingString.ToString();
    }

    public static void MoveReservationToRefundsList(int indexOfReservation)
    {
        if (indexOfReservation >= ReservationsList.Count) // index is out of range
        {
            throw new IndexOutOfRangeException($"ReservationsList contains less than {indexOfReservation + 1} items");
            // shouldn't get run unless the user's interfering with the code
        }
        RefundsList.Add(ReservationsList[indexOfReservation]);
        ReservationsList.RemoveAt(indexOfReservation);
    }

    /// <summary>
    /// Checks if room number already exists on file.
    /// </summary>
    /// <param name="roomNumber"></param>
    /// <returns></returns>
    public static bool AlreadyExists(int roomNumber)
    {
        foreach (var room in RoomsList)
        {
            if (roomNumber == room.roomNumber)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Checks if room type already exists on file.
    /// </summary>
    /// <param name="roomType"></param>
    /// <returns></returns>
    public static bool AlreadyExists(RoomType roomType)
    {
        foreach (var roomPrice in RoomPricesList)
        {
            if (roomType == roomPrice.roomType)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Checks if customer name already exists on file.
    /// </summary>
    /// <param name="roomType"></param>
    /// <returns></returns>
    public static bool AlreadyExists(string customerName)
    {
        foreach (var customer in CustomersList)
        {
            if (customerName == customer.customerName)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Careful: returns index -1 if specified coupon code is not on file
    /// </summary>
    /// <param name="couponCode"></param>
    /// <param name="indexOfSpecifiedCode"></param>
    /// <returns></returns>
    public static bool CouponCodeAlreadyExists(string couponCode, out int indexOfSpecifiedCode)
    {
        for (int index = 0; index < CouponCodesList.Count; index++)
        {
            if (CouponCodesList[index].couponCode == couponCode)
            {
                indexOfSpecifiedCode = index;
                return true;
            }
        }

        indexOfSpecifiedCode = -1;
        return false;
    }

    public static bool ThisReservationHasUsedThisCode(string reservationNumber, string targetCouponCode)
    {
        foreach (var (reservationNumberApplied, couponCodeUsed) in CouponRedemptionList)
        {
            if (reservationNumberApplied == reservationNumber && couponCodeUsed == targetCouponCode) // "this usage shows the specified reservation has already used the specified targetCouponCode"
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Checks if 2 date ranges overlap
    /// </summary>
    /// <param name="dateStart1"></param>
    /// <param name="dateStop1"></param>
    /// <param name="dateStart2"></param>
    /// <param name="dateStop2"></param>
    /// <returns></returns>
    public static bool DateRangesOverlap(DateOnly dateStart1, DateOnly dateStop1, DateOnly dateStart2, DateOnly dateStop2)
    {
        if ((dateStart1 >= dateStart2) && dateStop1 <= dateStop2) { return true; }
        if ((dateStart1 <= dateStart2) && dateStop1 >= dateStop2) { return true; }
        if ((dateStart1 <= dateStart2) && dateStop1 <= dateStop2 && (dateStop1 >= dateStart2)) { return true; }
        if ((dateStart1 >= dateStart2) && dateStop1 >= dateStop2 && (dateStart1 <= dateStop2)) { return true; }

        return false;
    }

    public static bool OverlapWithExistingReservation(DateOnly dateStart1, DateOnly dateStop1, int roomNumber)
    {
        foreach (var reservation in ReservationsList)
        {
            if (reservation.roomNumber != roomNumber)
            {
                continue;
            }

            if (DateRangesOverlap(dateStart1, dateStop1, reservation.dateStart, reservation.dateStop))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Returns the list of the specified customer name's (prior/future) reservations. Currently ongoing reservations will never be listed. 
    /// </summary>
    /// <param name="customerName"></param>
    /// <param name="reservationIsInFuture">True if you want future reservations. False if you want prior reservations</param>
    /// <returns></returns>
    public static List<(string reservationNumber, DateOnly dateStart, DateOnly dateStop, int roomNumber, string customerName, string paymentConfirmation, decimal chargedFees)> ReservationsOfCustomer(string customerName, bool reservationIsInFuture)
    {
        List<(string reservationNumber, DateOnly dateStart, DateOnly dateStop, int roomNumber, string customerName, string paymentConfirmation, decimal chargedFees)> reservationsOfCustomer = new();
        DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);

        switch (reservationIsInFuture)
        {
            case true:
                foreach (var reservation in ReservationsList)
                {
                    if (reservation.customerName == customerName && reservation.dateStart > currentDate)
                    {
                        reservationsOfCustomer.Add(reservation);
                    }
                }
                break;
            case false:
                foreach (var reservation in ReservationsList)
                {
                    if (reservation.customerName == customerName && reservation.dateStop < currentDate)
                    {
                        reservationsOfCustomer.Add(reservation);
                    }
                }
                break;
        }

        return reservationsOfCustomer;
    }

    /// <summary>
    /// Returns a list of all reservations made during the specified date range (includes all reservations that are ongoing during this time; doesn't need to start or end exactly on the daterange endpoints)
    /// </summary>
    /// <param name="dateStart"></param>
    /// <param name="dateStop"></param>
    /// <returns></returns>
    public static List<(string reservationNumber, DateOnly dateStart, DateOnly dateStop, int roomNumber, string customerName, string paymentConfirmation, decimal chargedFees)> ReservationsDuring(DateOnly dateStart, DateOnly dateStop)
    {
        List<(string reservationNumber, DateOnly dateStart, DateOnly dateStop, int roomNumber, string customerName, string paymentConfirmation, decimal chargedFees)> reservationsOnTheDates = new();

        foreach (var reservation in ReservationsList)
        {
            if (DateRangesOverlap(reservation.dateStart, reservation.dateStop, dateStart, dateStop))
            {
                reservationsOnTheDates.Add(reservation);
            }
        }

        return reservationsOnTheDates;
    }

    /// <summary>
    /// Returns a list of room numbers that exist but are not part of the "reserved rooms" list
    /// </summary>
    /// <param name="reservedRoomNumbers"></param>
    /// <returns></returns>
    public static List<int> UnreservedRoomNumbers(List<(string reservationNumber, DateOnly dateStart, DateOnly dateStop, int roomNumber, string customerName, string paymentConfirmation, decimal chargedFees)> reservedRooms)
    {
        List<int> unreservedRoomNumbers = new();
        List<int> reservedRoomNumbers = new();

        foreach (var reservedRoom in reservedRooms)
        {
            reservedRoomNumbers.Add(reservedRoom.roomNumber);
        }

        foreach (var room in RoomsList)
        {
            if (!reservedRoomNumbers.Contains(room.roomNumber))
            {
                unreservedRoomNumbers.Add(room.roomNumber);
            }
        }

        return unreservedRoomNumbers;
    }

    /// <summary>
    /// Reports whether customer of specified name is a Frequent Traveler. Takes the first customer with that name-- DOES NOT DIFFERENTIATE between >1 customers of the same name!
    /// </summary>
    /// <returns></returns>
    public static bool IsFreqTraveler(string customerName)
    {
        foreach (var customer in CustomersList)
        {
            if (customer.customerName == customerName)
            {
                return customer.freqTravelerStatus;
            }
        }

        throw new Exception(message: $"Customer {customerName} does not exist.");
        // this line should never be run assuming the customerName parameter is sure to exist in CustomersList before calling this method. 
        // but the compiler won't be happy if I don't return something or an exception outside the for loop
    }

    /// <summary>
    /// Returns a list of each date's (in the specified date range, inclusive) full-package report (take notice of the named return value's elements). Referencing ReservationsList and RoomsList
    /// </summary>
    /// <param name="dateStart"></param>
    /// <param name="dateStop"></param>
    /// <returns></returns>
    public static List<(DateOnly date, List<(int roomNumber, RoomType roomType, bool isOccupied, decimal dailyRentalFee, decimal dailyCleaningCost)> eachRoomReportOfThisDate, double occupancyPercentageOfThisDate, decimal revenueOfThisDate, decimal cleaningCostsOfThisDate)> ListOfEachDateReport(DateOnly dateStart, DateOnly dateStop, out decimal totalRevenueOfDateRange, out decimal totalCleaningCostsOfDateRange)
    {
        List<DateOnly> listOfDatesToReport = HelperFunctions.ListOfDaysBetween(dateStart, dateStop);
        List<(DateOnly date, List<(int roomNumber, RoomType roomType, bool isOccupied, decimal dailyRentalFee, decimal dailyCleaningCost)> eachRoomReportOfThisDate, double occupancyPercentageOfThisDate, decimal revenueOfThisDate, decimal cleaningCostsOfThisDate)> dateReports = new(); // return this

        // hierarchy: the list "dateReports" has multiple of "date report", a "dateReport" has multiple of "room reports", a "room reports" has multiple "room report", "room report" is a tuple

        foreach (var date in listOfDatesToReport) // for each date, produce a date report to pile up into "dateReports"
        {
            double totalRooms = 0;
            double usedRoomsThisDate = 0;
            decimal revenueOfThisDate = 0m;
            decimal cleaningCostsOfThisDate = 0m;

            List<(int roomNumber, RoomType roomType, bool isOccupied, decimal dailyRentalFee, decimal dailyCleaningCost)> roomReports = new();

            foreach (var (roomNumber, roomType) in RoomsList) // for each room, produce a roomReport. Pile up into "roomReports"
            {

                bool isOccupied = HelperFunctions.RoomIsOccupiedThisDate(roomNumber, date, out decimal dailyRentalFee, out decimal dailyCleaningCost);

                if (isOccupied)
                {
                    usedRoomsThisDate++;
                    revenueOfThisDate += dailyRentalFee;
                    cleaningCostsOfThisDate += dailyCleaningCost;
                }
                totalRooms++;

                (int roomNumber, RoomType roomType, bool isOccupied, decimal dailyRentalFee, decimal dailyCleaningCost) roomReport = (roomNumber, roomType, isOccupied, dailyRentalFee, dailyCleaningCost);
                roomReports.Add(roomReport);
            }

            double occupancyPercentage = usedRoomsThisDate / totalRooms * 100; // PERCENTAGE not decimal. When representing this, include "%"

            dateReports.Add((date, roomReports, occupancyPercentage, revenueOfThisDate, cleaningCostsOfThisDate)); // produce dateReports: pile up the date reports <= pile up the room reports
        } // when this ends, every date has added its "date report" into the "dateReports" list

        totalRevenueOfDateRange = 0m;
        totalCleaningCostsOfDateRange = 0m;
        foreach (var dateReport in dateReports)
        {
            totalRevenueOfDateRange += dateReport.revenueOfThisDate;
            totalCleaningCostsOfDateRange += dateReport.cleaningCostsOfThisDate;
        }
        return dateReports;
    }
}

public static class HelperFunctions
{
    /// <summary>
    /// String of random chars of specified length
    /// </summary>
    /// <param name="length"></param>
    /// <returns></returns>
    public static string RandomStringGenerator(int length)
    {
        string selectableChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*";
        // I'm picking only numbers, some special characters, and lowercase/uppercase english alphabet to be safe

        StringBuilder buildingString = new();

        for (int i = 0; i < length; i++)
        {
            int charNumber = Random.Shared.Next(selectableChars.Length);

            buildingString.Append(selectableChars[charNumber]);
        }

        return buildingString.ToString();
    }

    /// <summary>
    /// Returns a string of the RoomType enum that the specified room number is. Assumes specified room number exists on file
    /// </summary>
    /// <returns></returns>
    public static RoomType RoomTypeOfRoomNumber(int roomNumber)
    {
        foreach (var room in CurrentData.RoomsList)
        {
            if (room.roomNumber == roomNumber)
            {
                return room.roomType;
            }
        }

        throw new Exception(message: $"Room {roomNumber} does not exist.");
        // this line should never be run assuming the roomNumber parameter is checked to exist in RoomsList before calling this method. 
        // but the compiler won't be happy if I don't return something or an exception outside the foreach
    }

    public static decimal DailyRateOfRoomType(RoomType roomType)
    {
        foreach (var roomPriceTuple in CurrentData.RoomPricesList)
        {
            if (roomPriceTuple.roomType == roomType)
            {
                return roomPriceTuple.dailyRate;
            }
        }

        throw new Exception(message: $"Room type {roomType} does not exist.");
        // this line should never be run assuming the roomType parameter is checked to exist in RoomPricesList before calling this method. 
        // but the compiler won't be happy if I don't return something or an exception outside the for loop
    }

    /// <summary>
    /// Returns the index of the item in RoomPricesList that contains the specified room type
    /// </summary>
    /// <param name="roomType"></param>
    /// <returns></returns>
    public static int IndexOfRoomType(RoomType roomType)
    {
        for (int index = 0; index < CurrentData.RoomPricesList.Count; index++)
        {
            if (CurrentData.RoomPricesList[index].roomType == roomType)
            {
                return index;
            }
        }

        throw new Exception(message: $"Room type {roomType} does not exist.");
        // this line should never be run assuming the roomType parameter is checked to exist in RoomPricesList before calling this method. 
        // but the compiler won't be happy if I don't return something or an exception outside the for loop
    }

    /// <summary>
    /// Returns the index of the item in ReservationsList that contains the specified customer name AND specified reservationNumber (if it is not "-1")
    /// </summary>
    /// <param name="customerName"></param>
    /// <param name="reservationNumber">Pass in "-1" if the reservation number is not needed to accurately identify the target reservation</param>
    /// <returns></returns>
    public static int IndexOfReserv(string customerName, string reservationNumber)
    {
        for (int index = 0; index < CurrentData.ReservationsList.Count; index++)
        {
            switch (reservationNumber)
            {
                case "-1":
                    if (CurrentData.ReservationsList[index].customerName == customerName)
                    {
                        return index;
                    }
                    break;
                default: // take reservation num into consideration
                    if (CurrentData.ReservationsList[index].customerName == customerName && CurrentData.ReservationsList[index].reservationNumber == reservationNumber)
                    {
                        return index;
                    }
                    break;
            }
        }

        throw new Exception(message: $"Invalid customer name and/or reservation number for ReservationsList.");
        // this line should never be run assuming the parameter is checked to exist in ReservationsList before calling this method. 
        // but the compiler won't be happy if I don't return something or an exception outside the for loop
    }

    /// <summary>
    /// Directly changes RoomPricesList to update a room price change. Purpose: update the room price
    /// </summary>
    /// <param name="indexOfTuple"></param>
    /// <param name="targetDailyRate"></param>
    public static void ReplaceRoomPriceTuple(int indexOfTuple, (RoomType targetRoomType, decimal targetDailyRate, decimal preservedCleaningCost) targetItem)
    {
        if (indexOfTuple >= CurrentData.RoomPricesList.Count) // index is out of range
        {
            throw new IndexOutOfRangeException($"RoomPricesList contains less than {indexOfTuple + 1} items");
            // shouldn't get run unless the user's interfering with the code
        }
        CurrentData.RoomPricesList.RemoveAt(indexOfTuple);
        CurrentData.RoomPricesList.Insert(indexOfTuple, targetItem);
    }

    /// <summary>
    /// Directly changes ReservationsList to update a specific reservation number's charged fees
    /// </summary>
    /// <param name="indexOfReservation">Current index of the item in ReservationsList</param>
    /// <param name="newChargedFees"></param>
    public static void ReplaceReservationChargedFees(int indexOfReservation, decimal newChargedFees)
    {
        if (indexOfReservation >= CurrentData.ReservationsList.Count) // index is out of range
        {
            throw new IndexOutOfRangeException($"ReservationsList contains less than {indexOfReservation + 1} items");
            // shouldn't get run unless the user's interfering with the code
        }

        var (reservationNumber, dateStart, dateStop, roomNumber, customerName, paymentConfirmation, _) = CurrentData.ReservationsList[indexOfReservation];

        CurrentData.ReservationsList.RemoveAt(indexOfReservation);
        CurrentData.ReservationsList.Insert(indexOfReservation, (reservationNumber, dateStart, dateStop, roomNumber, customerName, paymentConfirmation, newChargedFees)); // everything is the same as before except the updated chargedFees
    }

    /// <summary>
    /// Typically used with other methods to accurately identify a customer that shares name with another
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static List<(string customerName, long cardNumber, bool freqTravelerStatus)> ListOfCustomersWithName(string customerName)
    {
        List<(string customerName, long cardNumber, bool freqTravelerStatus)> customersWithName = new();

        foreach (var customer in CurrentData.CustomersList)
        {
            if (customer.customerName == customerName)
            {
                customersWithName.Add(customer);
            }
        }

        return customersWithName;

    }

    /// <summary>
    /// Typically used with other methods to accurately differentiate between multiple reservations made by customer(s) of the same name
    /// </summary>
    public static List<(string reservationNumber, DateOnly dateStart, DateOnly dateStop, int roomNumber, string customerName, string paymentConfirmation, decimal chargedFees)> ListOfReservationsUnderName(string customerName)
    {
        List<(string reservationNumber, DateOnly dateStart, DateOnly dateStop, int roomNumber, string customerName, string paymentConfirmation, decimal chargedFees)> reservationsUnderName = new();

        foreach (var reservation in CurrentData.ReservationsList)
        {
            if (reservation.customerName == customerName)
            {
                reservationsUnderName.Add(reservation);
            }
        }

        return reservationsUnderName;
    }

    /// <summary>
    /// Detects if a specified card number already exists in a specified list. Used with ListOfCustomersWithName() to prohibit customers with same name and card number from getting added into Customers.txt
    /// </summary>
    /// <param name="cardNumber"></param>
    /// <param name="listOfInterest"></param>
    /// <returns></returns>
    public static bool CardNumberIsInHere(long cardNumber, List<(string customerName, long cardNumber, bool freqTravelerStatus)> listOfInterest)
    {
        foreach (var customer in listOfInterest)
        {
            if (customer.cardNumber == cardNumber)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// OG fees = days * daily rate. Apply discount after calculating OG fees. Considers status of customer in calculation
    /// </summary>
    /// <param name="freqTravelerStatus"></param>
    /// <param name="dateStart"></param>
    /// <param name="dateStop"></param>
    /// <param name="roomNumber"></param>
    /// <returns></returns>
    public static decimal ApplyDiscount(bool freqTravelerStatus, DateOnly dateStart, DateOnly dateStop, int roomNumber)
    {
        decimal originalFees = ListOfDaysBetween(dateStart, dateStop).Count * DailyRateOfRoomType(RoomTypeOfRoomNumber(roomNumber));
        decimal discountToApply = freqTravelerStatus switch
        {
            true => ConvertPercentageToDecimal(CurrentData.DisCountPercentageForFreqTravelers),
            false => 0m
        };

        return originalFees * (1 - discountToApply);
    }

    /// <summary>
    /// Overload for coupon discount
    /// </summary>
    /// <param name="initialChargedFees"></param>
    /// <param name="indexOfApplyingCouponCode"></param>
    /// <returns></returns>
    public static decimal ApplyDiscount(decimal initialChargedFees, int indexOfApplyingCouponCode)
    {
        decimal discountRateInDecimal = ConvertPercentageToDecimal(CurrentData.CouponCodesList[indexOfApplyingCouponCode].couponDiscountPercentage);
        return initialChargedFees * (1 - discountRateInDecimal);
    }

    /// <summary>
    /// Just used to display discount rate nicely. Ex: input = 0.25m, output = 25m (0.25 --> 25%)
    /// </summary>
    /// <param name="inDecimal"></param>
    /// <returns></returns>
    public static decimal ConvertDecimalToPercentage(decimal inDecimalForm)
    {
        return inDecimalForm * 100;
    }

    /// <summary>
    /// Aids calculation. Ex: input = 25m, output = 0.25m (25% --> 0.25)
    /// </summary>
    /// <param name="inPercentageForm"></param>
    /// <returns></returns>
    public static decimal ConvertPercentageToDecimal(decimal inPercentageForm)
    {
        return inPercentageForm / 100;
    }

    public static bool ReservIsInThePast(int indexOfReservation)
    {
        return CurrentData.ReservationsList[indexOfReservation].dateStop < DateOnly.FromDateTime(DateTime.Now);
    }

    /// <summary>
    /// Returns the list of dates between 2 dates, inclusive
    /// </summary>
    /// <param name="dateStart"></param>
    /// <param name="dateStop"></param>
    /// <returns></returns>
    public static List<DateOnly> ListOfDaysBetween(DateOnly dateStart, DateOnly dateStop)
    {
        List<DateOnly> listOfDaysBetween = new();

        // DateOnly does not support subtraction operation, so I'm converting them to their DateTime equivalents, and the arbritrary "Time" is set to midnight
        // Sole purpose of calculating how many times to add the DateOnly's dateStart:
        DateTime dayStart = dateStart.ToDateTime(TimeOnly.MinValue);
        DateTime dayStop = dateStop.ToDateTime(TimeOnly.MinValue);
        int inclusiveNumberOfDaysBetween = (int)(dayStop - dayStart).TotalDays + 1; // never gonna have decimal anyways-- they're all DateTimes with "midnight" Time, so casting isn't losing crucial info

        for (int dayNumber = 1; dayNumber <= inclusiveNumberOfDaysBetween; dayNumber++)
        {
            listOfDaysBetween.Add(dateStart.AddDays(dayNumber - 1));
        }

        return listOfDaysBetween;
    }

    /// <summary>
    /// Checks whether the room is occupied during the specified date. Also outputs the averaged-out "daily fee" of the reservation that's occupying the room, and the roomType-dependent "daily cleaning cost". Helps with daily utilization report
    /// </summary>
    /// <param name="roomNumberOfInterest"></param>
    /// <param name="thisDate"></param>
    /// <param name="dailyRevenue"></param>
    /// <returns></returns>
    public static bool RoomIsOccupiedThisDate(int roomNumberOfInterest, DateOnly thisDate, out decimal dailyRevenue, out decimal dailyCleaningCost)
    {
        foreach (var (reservationNumber, dateStart, dateStop, roomNumber, customerName, paymentConfirmation, chargedFees) in CurrentData.ReservationsList)
        {
            if (roomNumber != roomNumberOfInterest) // immediately ignore if it's not the room num we want to know about
            {
                continue;
            }

            if (ListOfDaysBetween(dateStart, dateStop).Contains(thisDate)) // thisDate is in "days of this reservation"
            {
                dailyRevenue = chargedFees / ListOfDaysBetween(dateStart, dateStop).Count;
                dailyCleaningCost = CurrentData.RoomPricesList[IndexOfRoomType(RoomTypeOfRoomNumber(roomNumber))].dailyCleaningCost; // find the room type by the room number. then find the room type's index in RoomPricesList. then, at that index, find the .dailyCleaningCost
                return true;
            }
        }

        // if it gets to here, it means the specified room number is not occupied during the specified date
        dailyRevenue = 0m;
        dailyCleaningCost = 0m;
        return false;
    }

}