namespace Hotel.Data;

public enum RoomType { Single, Double, Suite }

public static class FileManager
{
    public static List<(string customerName, long cardNumber, bool freqTravelerStatus)> ReadInCustomers(string fullFilePath)
    {
        var customersList = new List<(string customerName, long cardNumber, bool freqTravelerStatus)>();
        var linesFromFile = File.ReadAllLines(fullFilePath);

        for (int index = 0; index < linesFromFile.Length; index++)
        {
            var line = linesFromFile[index];

            if (string.IsNullOrWhiteSpace(line) || string.IsNullOrEmpty(line)) // just don't parse it at all; removes the extraneous empty lines for the next time WriteUp() methods are called too
            {
                continue;
            }

            var parts = line.Split(',');
            try
            {
                if (parts.Length != 3) // something is wrong with the comma separation in the .txt file
                {
                    throw new FormatException();
                }

                string customerName = parts[0];
                long cardNumber = long.Parse(parts[1]);
                bool freqTravelerStatus = bool.Parse(parts[2]);

                customersList.Add((customerName, cardNumber, freqTravelerStatus));
            }
            catch (FormatException) // proofs against weird format in the .txt file
            {
                throw new FormatException($"Line {index + 1} in Customers.txt is in an invalid format.");
            }
        }

        return customersList;
    }

    public static List<(string reservationNumber, DateOnly dateStart, DateOnly dateStop, int roomNumber, string customerName, string paymentConfirmation, decimal chargedFees)> ReadInReservations(string fullFilePath)
    {
        var reservationsList = new List<(string reservationNumber, DateOnly dateStart, DateOnly dateStop, int roomNumber, string customerName, string paymentConfirmation, decimal chargedFees)>();
        var linesFromFile = File.ReadAllLines(fullFilePath);

        for (int index = 0; index < linesFromFile.Length; index++)
        {
            var line = linesFromFile[index];

            if (string.IsNullOrWhiteSpace(line) || string.IsNullOrEmpty(line)) // just don't parse it at all; removes the extraneous empty lines for the next time WriteUp() methods are called too
            {
                continue;
            }

            var parts = line.Split(',');
            try
            {
                if (parts.Length != 7) // something is wrong with the comma separation in the .txt file
                {
                    throw new FormatException();
                }

                string reservationNumber = parts[0];
                DateOnly dateStart = DateOnly.Parse(parts[1]);
                DateOnly dateStop = DateOnly.Parse(parts[2]);
                int roomNumber = int.Parse(parts[3]);
                string customerName = parts[4];
                string paymentConfirmation = parts[5];
                decimal chargedFees = decimal.Parse(parts[6]);

                reservationsList.Add((reservationNumber, dateStart, dateStop, roomNumber, customerName, paymentConfirmation, chargedFees));
            }
            catch (FormatException)
            {
                throw new FormatException($"Line {index + 1} in Reservations.txt is in an invalid format.");
            }

        }

        return reservationsList;
    }

    public static List<(RoomType roomType, decimal dailyRate, decimal dailyCleaningCost)> ReadInRoomPrices(string fullFilePath)
    {
        var roomPricesList = new List<(RoomType roomType, decimal dailyRate, decimal dailyCleaningCost)>();
        var linesFromFile = File.ReadAllLines(fullFilePath);

        for (int index = 0; index < linesFromFile.Length; index++)
        {
            var line = linesFromFile[index];

            if (string.IsNullOrWhiteSpace(line) || string.IsNullOrEmpty(line)) // just don't parse it at all; removes the extraneous empty lines for the next time WriteUp() methods are called too
            {
                continue;
            }

            var parts = line.Split(',');
            try
            {
                if (parts.Length != 3) // something is wrong with the comma separation in the .txt file
                {
                    throw new FormatException();
                }

                RoomType roomType = Enum.Parse<RoomType>(parts[0]);
                decimal dailyRate = decimal.Parse(parts[1], System.Globalization.NumberStyles.Currency);
                decimal dailyCleaningCost = decimal.Parse(parts[2], System.Globalization.NumberStyles.Currency);


                roomPricesList.Add((roomType, dailyRate, dailyCleaningCost));
            }
            catch (FormatException)
            {
                throw new FormatException($"Line {index + 1} in RoomPrices.txt is in an invalid format.");
            }

        }

        if (roomPricesList.Count == 0) // default load into RoomPricesList, which will load into Roomprices.txt next time the main menu finishes a loop
        {
            roomPricesList.Add((RoomType.Single, 100, 30));
            roomPricesList.Add((RoomType.Double, 199, 40));
            roomPricesList.Add((RoomType.Suite, 490, 60));
        }

        return roomPricesList;
    }

    public static List<(int roomNumber, RoomType roomType)> ReadInRooms(string fullFilePath)
    {
        var roomsList = new List<(int roomNumber, RoomType roomType)>();
        var linesFromFile = File.ReadAllLines(fullFilePath);

        for (int index = 0; index < linesFromFile.Length; index++)
        {
            var line = linesFromFile[index];

            if (string.IsNullOrWhiteSpace(line) || string.IsNullOrEmpty(line)) // just don't parse it at all; removes the extraneous empty lines for the next time WriteUp() methods are called too
            {
                continue;
            }

            var parts = line.Split(',');
            try
            {
                if (parts.Length != 2) // something is wrong with the comma separation in the .txt file
                {
                    throw new FormatException();
                }

                int roomNumber = int.Parse(parts[0]);
                RoomType roomType = Enum.Parse<RoomType>(parts[1]);

                roomsList.Add((roomNumber, roomType));
            }
            catch (FormatException)
            {
                throw new FormatException($"Line {index + 1} in Rooms.txt is in an invalid format.");
            }

        }

        return roomsList;
    }

    public static List<(string reservationNumber, DateOnly dateStart, DateOnly dateStop, int roomNumber, string customerName, string paymentConfirmation, decimal chargedFees)> ReadInRefunds(string fullFilePath)
    {
        var refundsList = new List<(string reservationNumber, DateOnly dateStart, DateOnly dateStop, int roomNumber, string customerName, string paymentConfirmation, decimal chargedFees)>();
        var linesFromFile = File.ReadAllLines(fullFilePath);

        for (int index = 0; index < linesFromFile.Length; index++)
        {
            var line = linesFromFile[index];

            if (string.IsNullOrWhiteSpace(line) || string.IsNullOrEmpty(line)) // just don't parse it at all; removes the extraneous empty lines for the next time WriteUp() methods are called too
            {
                continue;
            }

            var parts = line.Split(',');
            try
            {
                if (parts.Length != 7) // something is wrong with the comma separation in the .txt file
                {
                    throw new FormatException();
                }

                string reservationNumber = parts[0];
                DateOnly dateStart = DateOnly.Parse(parts[1]);
                DateOnly dateStop = DateOnly.Parse(parts[2]);
                int roomNumber = int.Parse(parts[3]);
                string customerName = parts[4];
                string paymentConfirmation = parts[5];
                decimal chargedFees = decimal.Parse(parts[6]);

                refundsList.Add((reservationNumber, dateStart, dateStop, roomNumber, customerName, paymentConfirmation, chargedFees));
            }
            catch (FormatException)
            {
                throw new FormatException($"Line {index + 1} in Refunds.txt is in an invalid format.");
            }

        }

        return refundsList;
    }

    public static List<(string couponCode, decimal couponDiscountInPercentage)> ReadInCouponCodes(string fullFilePath)
    {
        var couponCodesList = new List<(string couponCode, decimal couponDiscountPercentage)>();
        var linesFromFile = File.ReadAllLines(fullFilePath);

        for (int index = 0; index < linesFromFile.Length; index++)
        {
            var line = linesFromFile[index];

            if (string.IsNullOrWhiteSpace(line) || string.IsNullOrEmpty(line)) // just don't parse it at all; removes the extraneous empty lines for the next time WriteUp() methods are called too
            {
                continue;
            }

            var parts = line.Split(',');
            try
            {
                if (parts.Length != 2) // something is wrong with the comma separation in the .txt file
                {
                    throw new FormatException();
                }

                string couponCode = parts[0];
                decimal couponDiscountInPercentage = decimal.Parse(parts[1]);

                couponCodesList.Add((couponCode, couponDiscountInPercentage));
            }
            catch (FormatException)
            {
                throw new FormatException($"Line {index + 1} in CouponCodes.txt is in an invalid format.");
            }

        }

        return couponCodesList;
    }

    public static List<(string reservationNumberApplied, string couponCodeUsed)> ReadInCouponRedemptions(string fullFilePath)
    {
        var couponRedemptionList = new List<(string reservationNumberApplied, string couponCodeUsed)>();
        var linesFromFile = File.ReadAllLines(fullFilePath);

        for (int index = 0; index < linesFromFile.Length; index++)
        {
            var line = linesFromFile[index];

            if (string.IsNullOrWhiteSpace(line) || string.IsNullOrEmpty(line)) // just don't parse it at all; removes the extraneous empty lines for the next time WriteUp() methods are called too
            {
                continue;
            }

            var parts = line.Split(',');
            try
            {
                if (parts.Length != 2) // something is wrong with the comma separation in the .txt file
                {
                    throw new FormatException();
                }

                string reservationNumberApplied = parts[0];
                string couponCodeUsed = parts[1];

                couponRedemptionList.Add((reservationNumberApplied, couponCodeUsed));
            }
            catch (FormatException)
            {
                throw new FormatException($"Line {index + 1} in CouponRedemption.txt is in an invalid format.");
            }

        }

        return couponRedemptionList;
    }

    public static void WriteUpRooms(string deserializedItems)
    {
        File.WriteAllText(contents: deserializedItems, path: FindFile("Rooms.txt"));
    }

    public static void WriteUpReservations(string deserializedItems)
    {
        File.WriteAllText(contents: deserializedItems, path: FindFile("Reservations.txt"));
    }

    public static void WriteUpCustomers(string deserializedItems)
    {
        File.WriteAllText(contents: deserializedItems, path: FindFile("Customers.txt"));
    }

    public static void WriteUpRoomPrices(string deserializedItems)
    {
        File.WriteAllText(contents: deserializedItems, path: FindFile("RoomPrices.txt"));
    }

    public static void WriteUpRefundsList(string deserializedItems)
    {
        File.WriteAllText(contents: deserializedItems, path: FindFile("Refunds.txt"));
    }

    public static void WriteUpCouponCodesList(string deserializedItems)
    {
        File.WriteAllText(contents: deserializedItems, path: FindFile("CouponCodes.txt"));
    }

    public static void WriteUpCouponRedemptionList(string deserializedItems)
    {
        File.WriteAllText(contents: deserializedItems, path: FindFile("CouponRedemption.txt"));
    }

    /// <summary>
    /// Look in the current directory and all parent directories for the given file.
    /// FileNotFoundException thrown will be caught by .Logic's DeserializeData()
    /// </summary>
    /// <param name="fileName">The name of the file you want</param>
    /// <returns>The full path to that file</returns>
    /// <example>
    /// string[] linesInCustomerFile = File.ReadAllLines(FindFile("Customers.txt"));
    /// </example>
    public static string FindFile(string fileName)
    {
        var directory = new DirectoryInfo(Directory.GetCurrentDirectory());
        while (true)
        {
            var testPath = Path.Combine(directory.FullName, fileName);
            if (File.Exists(testPath))
            {
                return testPath;
            }

            if (directory.FullName == directory.Root.FullName)
            {
                throw new FileNotFoundException($"I looked for {fileName} in every folder from {Directory.GetCurrentDirectory()} to {directory.Root.FullName} and couldn't find it.");
            }
            directory = directory.Parent;
        }
    }

    /// <summary>
    /// Creates a file of specified name in the root directory of this entire 4-folder project (AKA the parent directory of the folders)
    /// </summary>
    /// <param name="fileName"></param>
    public static void CreateFileAtRoot(string fileName)
    {
        // this will be called in Hotel.UI, so go to the current directory's parent and THEN create the file
        // this method assumes there is a parent directory, which is a reliable assumption given the structure of the project, so I won't proof against the scenario where the parent is null
        var childDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());
        var parentDirectory = childDirectory.Parent;

        var filePath = Path.Combine(parentDirectory.FullName, fileName);
        var createdFile = File.Create(filePath);
        createdFile.Close();

        if (fileName == "RoomPrices.txt") // by default, write the contents of RoomPrices
        {
            File.WriteAllLines(filePath, new string[] { "Single,100,30", "Double,199,40", "Suite,490,60" });
        }
    }
}