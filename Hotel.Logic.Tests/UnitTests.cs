using Hotel.Data;
using static Hotel.Logic.CurrentData;
using static Hotel.Logic.HelperFunctions;

namespace Hotel.Logic.Tests;

public class UnitTests // Unit tests for any logic not related to working with the screen or working with files.
{
    [Fact]
    public void SerializeRoomsList1()
    {
        List<(int roomNumber, RoomType roomType)> roomsList = new() { (101, RoomType.Single), (102, RoomType.Double), (103, RoomType.Suite) };

        Assert.Equal($"101,Single{Environment.NewLine}102,Double{Environment.NewLine}103,Suite{Environment.NewLine}", SerializeData(roomsList));
    }

    [Fact]
    public void SerializeRoomsList2()
    {
        List<(int roomNumber, RoomType roomType)> roomsList = new() { (101, RoomType.Double), (102, RoomType.Suite), (106, RoomType.Suite) };

        Assert.Equal($"101,Double{Environment.NewLine}102,Suite{Environment.NewLine}106,Suite{Environment.NewLine}", SerializeData(roomsList));
    }

    [Fact]
    public void SerializeReservationsList()
    {
        List<(string reservationNumber, DateOnly dateStart, DateOnly dateStop, int roomNumber, string customerName, string paymentConfirmation, decimal chargedFees)> reservationsList = new()
        {
            ("5d18bf57-7089-437a-9f48-148b5893f8a8", DateOnly.Parse("1/2/2024"), DateOnly.Parse("1/2/2024"), 101, "John Doe", "123456711012345678901254567890", 150m) ,
            ("2148af82-c7cb-4315-a38d-cefc29281e99", DateOnly.Parse("1/3/2024"), DateOnly.Parse("1/5/2024"), 105, "John Dee", "012345678901234567890123456789", 150m)
        };

        Assert.Equal
        (
            $"5d18bf57-7089-437a-9f48-148b5893f8a8,1/2/2024,1/2/2024,101,John Doe,123456711012345678901254567890,150{Environment.NewLine}2148af82-c7cb-4315-a38d-cefc29281e99,1/3/2024,1/5/2024,105,John Dee,012345678901234567890123456789,150{Environment.NewLine}"
            , SerializeData(reservationsList));
    }

    [Fact]
    public void SerializeRefundsList()
    {
        List<(string reservationNumber, DateOnly dateStart, DateOnly dateStop, int roomNumber, string customerName, string paymentConfirmation, decimal chargedFees)> refundsList = new()
        {
            ("f53ce5c2-b873-47a1-b2bc-2004127a4b92", DateOnly.Parse("1/6/2024"), DateOnly.Parse("1/10/2024"), 105, "John Daa", "123456789012345678901234567890", 50m) ,
            ("dc27b5f3-01ae-47ea-9363-7f70db872152", DateOnly.Parse("1/9/2024"), DateOnly.Parse("1/11/2024"), 110 , "John Average", "123456789012345678901234567890", 50m) ,
            ("6f845a31-0fae-4581-aba7-66285cf0cb3d", DateOnly.Parse("1/10/2024"), DateOnly.Parse("1/15/2024"), 201 , "John Excellent", "123456789012345678901234567890", 50m)
        };

        Assert.Equal
        (
            $"f53ce5c2-b873-47a1-b2bc-2004127a4b92,1/6/2024,1/10/2024,105,John Daa,123456789012345678901234567890,50{Environment.NewLine}dc27b5f3-01ae-47ea-9363-7f70db872152,1/9/2024,1/11/2024,110,John Average,123456789012345678901234567890,50{Environment.NewLine}6f845a31-0fae-4581-aba7-66285cf0cb3d,1/10/2024,1/15/2024,201,John Excellent,123456789012345678901234567890,50{Environment.NewLine}"
            , SerializeData(refundsList)
        );
    }

    [Fact]
    public void SerializeCustomersList1()
    {
        List<(string customerName, long cardNumber, bool freqTravelerStatus)> customersList = new()
        {
            ("jonny",123123123000123,false),
            ("jane dey",1111221111311111,true)

        };

        Assert.Equal
        (
            $"jonny,123123123000123,False{Environment.NewLine}jane dey,1111221111311111,True{Environment.NewLine}"
            , SerializeData(customersList)
        );
    }

    [Fact]
    public void SerializeCustomersList2()
    {
        List<(string customerName, long cardNumber, bool freqTravelerStatus)> customersList = new()
        {
            ("papa john",9155491554915541,true),
            ("mama john",9155445519915541,false)
        };

        Assert.Equal
        (
            $"papa john,9155491554915541,True{Environment.NewLine}mama john,9155445519915541,False{Environment.NewLine}"
            , SerializeData(customersList)
        );
    }

    [Fact]
    public void SerializeRoomPricesList1()
    {
        List<(RoomType roomType, decimal dailyRate, decimal dailyCleaningCost)> roomPricesList = new()
        {
            (RoomType.Double, 200, 10) ,
            (RoomType.Single, 100, 10) ,
            (RoomType.Suite, 400, 20)
        };

        Assert.Equal
        (
            $"Double,200,10{Environment.NewLine}Single,100,10{Environment.NewLine}Suite,400,20{Environment.NewLine}"
            , SerializeData(roomPricesList)
        );
    }

    [Fact]
    public void SerializeRoomPricesList2()
    {
        List<(RoomType roomType, decimal dailyRate, decimal dailyCleaningCost)> roomPricesList = new()
        {
            (RoomType.Suite, 400, 10) ,
            (RoomType.Double, 200, 10) ,
            (RoomType.Single, 100, 20)
        };

        Assert.Equal
        (
          $"Suite,400,10{Environment.NewLine}Double,200,10{Environment.NewLine}Single,100,20{Environment.NewLine}"
          , SerializeData(roomPricesList)
        );
    }

    [Fact]
    public void SerializeCouponCodesList1()
    {
        List<(string couponCode, decimal couponDiscountPercentage)> couponCodesList = new()
        {
            ("iAmaCoupon", 10) ,
            ("CoupOn", 50) ,
            ("iAmaCoupUnder", 10)
        };

        string expectedString = $"iAmaCoupon,10{Environment.NewLine}CoupOn,50{Environment.NewLine}iAmaCoupUnder,10{Environment.NewLine}";
        string actualString = SerializeData(couponCodesList);

        Assert.Equal(expectedString, actualString);
    }

    [Fact]
    public void SerializeCouponCodesList2()
    {
        List<(string couponCode, decimal couponDiscountPercentage)> couponCodesList = new()
        {
            ("iAmaCoupon", 10) ,
            ("CoupUnder", 20) ,
            ("iAmaCoupUnder", 10)
        };

        string expectedString = $"iAmaCoupon,10{Environment.NewLine}CoupUnder,20{Environment.NewLine}iAmaCoupUnder,10{Environment.NewLine}";
        string actualString = SerializeData(couponCodesList);

        Assert.Equal(expectedString, actualString);
    }

    [Fact]
    public void SerializeCouponRedemptionList1()
    {
        List<(string reservationNumberApplied, string couponCodeUsed)> couponRedemptionList = new()
        {
            ("iAmAReserv", "iAmACoupOn") ,
            ("uniqueNum", "uniqueCode") ,
            ("iAmAReCook", "iAmACoupUnder")
        };

        string expectedString = $"iAmAReserv,iAmACoupOn{Environment.NewLine}uniqueNum,uniqueCode{Environment.NewLine}iAmAReCook,iAmACoupUnder{Environment.NewLine}";
        string actualString = SerializeData(couponRedemptionList);

        Assert.Equal(expectedString, actualString);
    }

    [Fact]
    public void SerializeCouponRedemptionList2()
    {
        List<(string reservationNumberApplied, string couponCodeUsed)> couponRedemptionList = new()
        {
            ("iAmAReserv", "iAmACoupOn") ,
            ("unUniqueNum", "unUniqueCode") ,
            ("iAmAReCook", "iAmACoupUnder")
        };

        string expectedString = $"iAmAReserv,iAmACoupOn{Environment.NewLine}unUniqueNum,unUniqueCode{Environment.NewLine}iAmAReCook,iAmACoupUnder{Environment.NewLine}";
        string actualString = SerializeData(couponRedemptionList);

        Assert.Equal(expectedString, actualString);
    }

    [Fact]
    public void MoveReservationToRefundsList1()
    {
        ReservationsList.Clear();
        RefundsList.Clear();

        ReservationsList.Add(("reservNum1", new DateOnly(2000, 1, 1), new DateOnly(2000, 1, 10), 100, "John Doe", "paymentConf1", 700));
        ReservationsList.Add(("reservNum2", new DateOnly(2000, 2, 1), new DateOnly(2000, 2, 10), 101, "John Do", "paymentConf2", 600));
        ReservationsList.Add(("reservNum3", new DateOnly(2000, 3, 1), new DateOnly(2000, 3, 10), 102, "John D", "paymentConf3", 500));

        MoveReservationToRefundsList(0);
        var expectedFinalReservList = new List<(string reservationNumber, DateOnly dateStart, DateOnly dateStop, int roomNumber, string customerName, string paymentConfirmation, decimal chargedFees)>()
        {
            ("reservNum2", new DateOnly(2000, 2, 1), new DateOnly(2000, 2, 10), 101, "John Do", "paymentConf2", 600) ,
            ("reservNum3", new DateOnly(2000, 3, 1), new DateOnly(2000, 3, 10), 102, "John D", "paymentConf3", 500)
        };
        var actualFinalReservList = ReservationsList;
        var expectedFinalRefundsList = new List<(string reservationNumber, DateOnly dateStart, DateOnly dateStop, int roomNumber, string customerName, string paymentConfirmation, decimal chargedFees)>()
        {
            ("reservNum1", new DateOnly(2000, 1, 1), new DateOnly(2000, 1, 10), 100, "John Doe", "paymentConf1", 700)
        };
        var actualFinalRefundsList = RefundsList;

        Assert.Equal(expectedFinalReservList, actualFinalReservList);
        Assert.Equal(expectedFinalRefundsList, actualFinalRefundsList);
    }

    [Fact]
    public void MoveReservationToRefundsList2()
    {
        ReservationsList.Clear();
        RefundsList.Clear();

        ReservationsList.Add(("reservNum1", new DateOnly(2000, 1, 1), new DateOnly(2000, 1, 10), 100, "John Doe", "paymentConf1", 700));
        ReservationsList.Add(("reservNum2", new DateOnly(2000, 2, 1), new DateOnly(2000, 2, 10), 101, "John Do", "paymentConf2", 600));
        ReservationsList.Add(("reservNum3", new DateOnly(2000, 3, 1), new DateOnly(2000, 3, 10), 102, "John D", "paymentConf3", 500));

        MoveReservationToRefundsList(1);
        var expectedFinalReservList = new List<(string reservationNumber, DateOnly dateStart, DateOnly dateStop, int roomNumber, string customerName, string paymentConfirmation, decimal chargedFees)>()
        {
            ("reservNum1", new DateOnly(2000, 1, 1), new DateOnly(2000, 1, 10), 100, "John Doe", "paymentConf1", 700) ,
            ("reservNum3", new DateOnly(2000, 3, 1), new DateOnly(2000, 3, 10), 102, "John D", "paymentConf3", 500)
        };
        var actualFinalReservList = ReservationsList;
        var expectedFinalRefundsList = new List<(string reservationNumber, DateOnly dateStart, DateOnly dateStop, int roomNumber, string customerName, string paymentConfirmation, decimal chargedFees)>()
        {
            ("reservNum2", new DateOnly(2000, 2, 1), new DateOnly(2000, 2, 10), 101, "John Do", "paymentConf2", 600)
        };
        var actualFinalRefundsList = RefundsList;

        Assert.Equal(expectedFinalReservList, actualFinalReservList);
        Assert.Equal(expectedFinalRefundsList, actualFinalRefundsList);
    }

    [Fact]
    public void MoveReservationToRefundsList3()
    {
        ReservationsList.Clear();
        RefundsList.Clear();

        ReservationsList.Add(("reservNum1", new DateOnly(2000, 1, 1), new DateOnly(2000, 1, 10), 100, "John Doe", "paymentConf1", 700));
        ReservationsList.Add(("reservNum2", new DateOnly(2000, 2, 1), new DateOnly(2000, 2, 10), 101, "John Do", "paymentConf2", 600));
        ReservationsList.Add(("reservNum3", new DateOnly(2000, 3, 1), new DateOnly(2000, 3, 10), 102, "John D", "paymentConf3", 500));

        try
        {
            MoveReservationToRefundsList(17);
            Assert.Fail("This should fail");
        }
        catch (IndexOutOfRangeException indexError)
        {
            Assert.Equal("ReservationsList contains less than 18 items", indexError.Message);
        }
    }

    [Fact]
    public void RoomNumDoesAlreadyExist()
    {
        RoomsList.Clear();
        RoomsList.Add((101, RoomType.Single));
        RoomsList.Add((105, RoomType.Double));
        RoomsList.Add((103, RoomType.Suite));

        int roomNumber = 105;

        Assert.True(AlreadyExists(roomNumber));
    }

    [Fact]
    public void RoomNumDoesNOTAlreadyExist()
    {
        RoomsList.Clear();
        RoomsList.Add((101, RoomType.Single));
        RoomsList.Add((105, RoomType.Double));
        RoomsList.Add((103, RoomType.Suite));

        int roomNumber = 102;

        Assert.False(AlreadyExists(roomNumber));
    }

    [Fact]
    public void RoomTypeDoesAlreadyExist()
    {
        RoomPricesList.Clear();
        RoomPricesList.Add((RoomType.Single, 999, 1));
        RoomPricesList.Add((RoomType.Double, 9999, 10));
        RoomPricesList.Add((RoomType.Suite, 99999, 100));

        RoomType roomType = RoomType.Single;

        Assert.True(AlreadyExists(roomType));
    }

    [Fact]
    public void RoomTypeDoesNOTAlreadyExist()
    {
        RoomPricesList.Clear();
        RoomPricesList.Add((RoomType.Single, 999, 1));
        RoomPricesList.Add((RoomType.Double, 9999, 10));
        RoomPricesList.Add((RoomType.Suite, 99999, 100));

        RoomType roomType = Enum.Parse<RoomType>("5");

        Assert.False(AlreadyExists(roomType));
    }

    [Fact]
    public void CustomerNameDoesAlreadyExist()
    {
        CustomersList.Clear();
        CustomersList.Add(("brother john", 9155491554915541, true));
        CustomersList.Add(("sister john", 9155445519915541, false));

        string customerName = "brother john";

        Assert.True(AlreadyExists(customerName));
    }

    [Fact]
    public void CustomerNameDoesNOTAlreadyExist()
    {
        CustomersList.Clear();
        CustomersList.Add(("aunt john", 9155491554915541, false));
        CustomersList.Add(("uncle john", 9155445519915541, true));

        string customerName = "neighbor john";

        Assert.False(AlreadyExists(customerName));
    }

    [Fact]
    public void CouponCodeDoesAlreadyExist()
    {
        CouponCodesList.Clear();
        CouponCodesList.Add(("secondCoupUnder", 100));
        CouponCodesList.Add(("GenkneeInfluence", 1));
        CouponCodesList.Add(("thirdCoupUnder", 15));

        string testCouponCode = "GenkneeInfluence";
        bool actualBool = CouponCodeAlreadyExists(testCouponCode, out int actualIndex);
        int expectedIndex = 1;

        Assert.True(actualBool);
        Assert.Equal(expectedIndex, actualIndex);
    }

    [Fact]
    public void CouponCodeDoesNOTAlreadyExist()
    {
        CouponCodesList.Clear();
        CouponCodesList.Add(("GenkneeInfluence", 100));
        CouponCodesList.Add(("secondCoupUnder", 1));
        CouponCodesList.Add(("thirdCoupUnder", 15));

        string testCouponCode = "GenshinImpact";
        bool actualBool = CouponCodeAlreadyExists(testCouponCode, out int actualIndex);
        int expectedIndex = -1;

        Assert.False(actualBool);
        Assert.Equal(expectedIndex, actualIndex);
    }

    [Fact]
    public void ThisReservationHASUsedThisCode()
    {
        CouponRedemptionList.Clear();
        CouponRedemptionList.Add(("reservNum1", "coup1"));
        CouponRedemptionList.Add(("reservNum2", "coup2"));
        CouponRedemptionList.Add(("reservNum3", "coup3"));

        string testReservNum = "reservNum2";
        string testTargetCoupCode = "coup2";

        Assert.True(ThisReservationHasUsedThisCode(testReservNum, testTargetCoupCode));
    }

    [Fact]
    public void ThisReservationHasNOTUsedThisCode()
    {
        CouponRedemptionList.Clear();
        CouponRedemptionList.Add(("reservNum1", "coup1"));
        CouponRedemptionList.Add(("reservNum2", "coup2"));
        CouponRedemptionList.Add(("reservNum3", "coup3"));

        string testReservNum = "reservNum2";
        string testTargetCoupCode = "coup3";

        Assert.False(ThisReservationHasUsedThisCode(testReservNum, testTargetCoupCode));
    }

    [Fact]
    public void DateRangesDoOverlap()
    {
        DateOnly dateStart1 = DateOnly.Parse("1/1/2000");
        DateOnly dateStop1 = DateOnly.Parse("2/1/2000");

        DateOnly dateStart2 = DateOnly.Parse("12/31/1999");
        DateOnly dateStop2 = DateOnly.Parse("1/10/2000");

        Assert.True(DateRangesOverlap(dateStart1, dateStop1, dateStart2, dateStop2));
    }

    [Fact]
    public void DateRangesDoNOTOverlap()
    {
        DateOnly dateStart1 = DateOnly.Parse("1/1/2000");
        DateOnly dateStop1 = DateOnly.Parse("2/1/2000");

        DateOnly dateStart2 = DateOnly.Parse("1/1/2005");
        DateOnly dateStop2 = DateOnly.Parse("2/1/2005");

        Assert.False(DateRangesOverlap(dateStart1, dateStop1, dateStart2, dateStop2));
    }

    [Fact]
    public void DoesOverlapWithExistingReservation()
    {
        ReservationsList.Clear();
        ReservationsList.Add(("f53ce5c2-b873-47a1-b2bc-2004127a4b92", DateOnly.Parse("1/6/2024"), DateOnly.Parse("1/10/2024"), 105, "John Daa", "123456789012345678901234567890", 1000));
        ReservationsList.Add(("dc27b5f3-01ae-47ea-9363-7f70db872152", DateOnly.Parse("1/9/2024"), DateOnly.Parse("1/11/2024"), 110, "John Average", "123456789012345678901234567890", 100));
        ReservationsList.Add(("6f845a31-0fae-4581-aba7-66285cf0cb3d", DateOnly.Parse("1/10/2024"), DateOnly.Parse("1/15/2024"), 201, "John Excellent", "123456789012345678901234567890", 10));

        DateOnly dateStart1 = DateOnly.Parse("1/1/2024");
        DateOnly dateStop1 = DateOnly.Parse("2/1/2024");
        int roomNumber = 105;

        Assert.True(OverlapWithExistingReservation(dateStart1, dateStop1, roomNumber));
    }

    [Fact]
    public void DoesNOTOverlapWithExistingReservation()
    {
        ReservationsList.Clear();
        ReservationsList.Add(("f53ce5c2-b873-47a1-b2bc-2004127a4b92", DateOnly.Parse("1/6/2024"), DateOnly.Parse("1/10/2024"), 105, "John Daa", "123456789012345678901234567890", 1000));
        ReservationsList.Add(("dc27b5f3-01ae-47ea-9363-7f70db872152", DateOnly.Parse("1/9/2024"), DateOnly.Parse("1/11/2024"), 110, "John Average", "123456789012345678901234567890", 100));
        ReservationsList.Add(("6f845a31-0fae-4581-aba7-66285cf0cb3d", DateOnly.Parse("1/10/2024"), DateOnly.Parse("1/15/2024"), 201, "John Excellent", "123456789012345678901234567890", 10));

        DateOnly dateStart1 = DateOnly.Parse("1/1/2000");
        DateOnly dateStop1 = DateOnly.Parse("2/1/2000");
        int roomNumber = 105;

        Assert.False(OverlapWithExistingReservation(dateStart1, dateStop1, roomNumber));
    }

    [Fact]
    public void ReservationsOfCustomer1() // for future reservations
    {
        ReservationsList.Clear();
        ReservationsList.Add(("5d18bf57-7089-437a-9f48-148b5893f8a8", DateOnly.Parse("1/2/2024"), DateOnly.Parse("1/2/2024"), 101, "John Doe", "123456711012345678901254567890", 999));
        ReservationsList.Add(("2148af82-c7cb-4315-a38d-cefc29281e99", DateOnly.Parse("1/3/2024"), DateOnly.Parse("1/5/2024"), 105, "John Doe", "012345678901234567890123456789", 999));
        ReservationsList.Add(("f53ce5c2-b873-47a1-b2bc-2004127a4b92", DateOnly.Parse("1/6/2020"), DateOnly.Parse("1/10/2020"), 105, "John Doe", "123456789012345678901234567890", 999));
        ReservationsList.Add(("dc27b5f3-01ae-47ea-9363-7f70db872152", DateOnly.Parse("1/9/2024"), DateOnly.Parse("1/11/2024"), 110, "John Average", "123456789012345678901234567890", 999));
        ReservationsList.Add(("6f845a31-0fae-4581-aba7-66285cf0cb3d", DateOnly.Parse("1/10/2024"), DateOnly.Parse("1/15/2024"), 201, "John Excellent", "123456789012345678901234567890", 999));

        string customerName = "John Doe";

        List<(string reservationNumber, DateOnly dateStart, DateOnly dateStop, int roomNumber, string customerName, string paymentConfirmation, decimal chargedFees)> actualReservationsOfCustomer = new()
        {
            ("5d18bf57-7089-437a-9f48-148b5893f8a8", DateOnly.Parse("1/2/2024"), DateOnly.Parse("1/2/2024"), 101, "John Doe", "123456711012345678901254567890", 999) ,
            ("2148af82-c7cb-4315-a38d-cefc29281e99", DateOnly.Parse("1/3/2024"), DateOnly.Parse("1/5/2024"), 105, "John Doe", "012345678901234567890123456789", 999) ,
        };

        bool reservationIsInFuture = true;

        Assert.Equal(actualReservationsOfCustomer, ReservationsOfCustomer(customerName, reservationIsInFuture));
    }

    [Fact]
    public void ReservationsOfCustomer2() // for prior reservations
    {
        ReservationsList.Clear();
        ReservationsList.Add(("5d18bf57-7089-437a-9f48-148b5893f8a8", DateOnly.Parse("1/2/2024"), DateOnly.Parse("1/2/2024"), 101, "John Doe", "123456711012345678901254567890", 999));
        ReservationsList.Add(("2148af82-c7cb-4315-a38d-cefc29281e99", DateOnly.Parse("1/3/2024"), DateOnly.Parse("1/5/2024"), 105, "John Doe", "012345678901234567890123456789", 999));
        ReservationsList.Add(("f53ce5c2-b873-47a1-b2bc-2004127a4b92", DateOnly.Parse("1/6/2020"), DateOnly.Parse("1/10/2020"), 105, "John Doe", "123456789012345678901234567890", 999));
        ReservationsList.Add(("dc27b5f3-01ae-47ea-9363-7f70db872152", DateOnly.Parse("1/9/2024"), DateOnly.Parse("1/11/2024"), 110, "John Average", "123456789012345678901234567890", 999));
        ReservationsList.Add(("6f845a31-0fae-4581-aba7-66285cf0cb3d", DateOnly.Parse("1/10/2024"), DateOnly.Parse("1/15/2024"), 201, "John Excellent", "123456789012345678901234567890", 999));

        string customerName = "John Doe";

        List<(string reservationNumber, DateOnly dateStart, DateOnly dateStop, int roomNumber, string customerName, string paymentConfirmation, decimal chargedFees)> actualReservationsOfCustomer = new()
        {
            ("f53ce5c2-b873-47a1-b2bc-2004127a4b92", DateOnly.Parse("1/6/2020"), DateOnly.Parse("1/10/2020"), 105, "John Doe", "123456789012345678901234567890", 999)
        };

        bool reservationIsInFuture = false;

        Assert.Equal(actualReservationsOfCustomer, ReservationsOfCustomer(customerName, reservationIsInFuture));
    }

    [Fact]
    public void ReservationsDuring1()
    {
        ReservationsList.Clear();
        ReservationsList.Add(("5d18bf57-7089-437a-9f48-148b5893f8a8", DateOnly.Parse("1/2/2024"), DateOnly.Parse("1/2/2024"), 101, "John Doe", "123456711012345678901254567890", 111));
        ReservationsList.Add(("2148af82-c7cb-4315-a38d-cefc29281e99", DateOnly.Parse("1/3/2024"), DateOnly.Parse("1/5/2024"), 105, "John Doe", "012345678901234567890123456789", 111));
        ReservationsList.Add(("f53ce5c2-b873-47a1-b2bc-2004127a4b92", DateOnly.Parse("1/6/2024"), DateOnly.Parse("1/10/2024"), 105, "John Doe", "123456789012345678901234567890", 111));
        ReservationsList.Add(("dc27b5f3-01ae-47ea-9363-7f70db872152", DateOnly.Parse("1/9/2024"), DateOnly.Parse("1/11/2024"), 110, "John Average", "123456789012345678901234567890", 111));
        ReservationsList.Add(("6f845a31-0fae-4581-aba7-66285cf0cb3d", DateOnly.Parse("1/10/2024"), DateOnly.Parse("1/15/2024"), 201, "John Excellent", "123456789012345678901234567890", 111));

        DateOnly dateStart = DateOnly.Parse("1/1/2024");
        DateOnly dateStop = DateOnly.Parse("1/6/2024");

        List<(string reservationNumber, DateOnly dateStart, DateOnly dateStop, int roomNumber, string customerName, string paymentConfirmation, decimal chargedFees)> actualReservationsDuring = new()
        {
            ("5d18bf57-7089-437a-9f48-148b5893f8a8", DateOnly.Parse("1/2/2024"), DateOnly.Parse("1/2/2024"), 101, "John Doe", "123456711012345678901254567890", 111) ,
            ("2148af82-c7cb-4315-a38d-cefc29281e99", DateOnly.Parse("1/3/2024"), DateOnly.Parse("1/5/2024"), 105, "John Doe", "012345678901234567890123456789", 111) ,
            ("f53ce5c2-b873-47a1-b2bc-2004127a4b92", DateOnly.Parse("1/6/2024"), DateOnly.Parse("1/10/2024"), 105, "John Doe", "123456789012345678901234567890", 111)
        };

        Assert.Equal(actualReservationsDuring, ReservationsDuring(dateStart, dateStop));
    }

    [Fact]
    public void ReservationsDuring2()
    {
        ReservationsList.Clear();
        ReservationsList.Add(("5d18bf57-7089-437a-9f48-148b5893f8a8", DateOnly.Parse("1/2/2024"), DateOnly.Parse("1/2/2024"), 101, "John Doe", "123456711012345678901254567890", 222));
        ReservationsList.Add(("2148af82-c7cb-4315-a38d-cefc29281e99", DateOnly.Parse("1/3/2024"), DateOnly.Parse("1/5/2024"), 105, "John Doe", "012345678901234567890123456789", 222));
        ReservationsList.Add(("f53ce5c2-b873-47a1-b2bc-2004127a4b92", DateOnly.Parse("1/6/2024"), DateOnly.Parse("1/10/2024"), 105, "John Doe", "123456789012345678901234567890", 222));
        ReservationsList.Add(("dc27b5f3-01ae-47ea-9363-7f70db872152", DateOnly.Parse("1/9/2024"), DateOnly.Parse("1/11/2024"), 110, "John Average", "123456789012345678901234567890", 222));
        ReservationsList.Add(("6f845a31-0fae-4581-aba7-66285cf0cb3d", DateOnly.Parse("1/10/2024"), DateOnly.Parse("1/15/2024"), 201, "John Excellent", "123456789012345678901234567890", 222));

        DateOnly dateStart = DateOnly.Parse("1/9/2024");
        DateOnly dateStop = DateOnly.Parse("1/10/2024");

        List<(string reservationNumber, DateOnly dateStart, DateOnly dateStop, int roomNumber, string customerName, string paymentConfirmation, decimal chargedFees)> actualReservationsDuring = new()
        {
            ("f53ce5c2-b873-47a1-b2bc-2004127a4b92", DateOnly.Parse("1/6/2024"), DateOnly.Parse("1/10/2024"), 105, "John Doe", "123456789012345678901234567890", 222) ,
            ("dc27b5f3-01ae-47ea-9363-7f70db872152", DateOnly.Parse("1/9/2024"), DateOnly.Parse("1/11/2024"), 110 , "John Average", "123456789012345678901234567890", 222) ,
            ("6f845a31-0fae-4581-aba7-66285cf0cb3d", DateOnly.Parse("1/10/2024"), DateOnly.Parse("1/15/2024"), 201 , "John Excellent", "123456789012345678901234567890", 222)
        };

        Assert.Equal(actualReservationsDuring, ReservationsDuring(dateStart, dateStop));
    }

    [Fact]
    public void UnreservedRoomNumbers1()
    {
        RoomsList.Clear();
        RoomsList.Add((101, RoomType.Double));
        RoomsList.Add((102, RoomType.Suite));
        RoomsList.Add((106, RoomType.Suite));
        RoomsList.Add((105, RoomType.Double));
        RoomsList.Add((110, RoomType.Double));
        RoomsList.Add((201, RoomType.Double));

        List<(string reservationNumber, DateOnly dateStart, DateOnly dateStop, int roomNumber, string customerName, string paymentConfirmation, decimal chargedFees)> reservedRooms = new()
        {
            ("f53ce5c2-b873-47a1-b2bc-2004127a4b92", DateOnly.Parse("1/6/2024"), DateOnly.Parse("1/10/2024"), 105, "John Daa", "123456789012345678901234567890", 20) ,
            ("dc27b5f3-01ae-47ea-9363-7f70db872152", DateOnly.Parse("1/9/2024"), DateOnly.Parse("1/11/2024"), 110 , "John Average", "123456789012345678901234567890", 20) ,
        };

        List<int> actualUnreservedRoomNumbers = new() { 101, 102, 106, 201 };

        Assert.Equal(actualUnreservedRoomNumbers, UnreservedRoomNumbers(reservedRooms));
    }

    [Fact]
    public void UnreservedRoomNumbers2()
    {
        RoomsList.Clear();
        RoomsList.Add((101, RoomType.Double));
        RoomsList.Add((102, RoomType.Suite));
        RoomsList.Add((106, RoomType.Suite));
        RoomsList.Add((105, RoomType.Double));
        RoomsList.Add((110, RoomType.Double));
        RoomsList.Add((201, RoomType.Double));

        List<(string reservationNumber, DateOnly dateStart, DateOnly dateStop, int roomNumber, string customerName, string paymentConfirmation, decimal chargedFees)> reservedRooms = new()
        {
            ("6f845a31-0fae-4581-aba7-66285cf0cb3d", DateOnly.Parse("1/10/2024"), DateOnly.Parse("1/15/2024"), 201 , "John Excellent", "123456789012345678901234567890", 15)
        };

        List<int> actualUnreservedRoomNumbers = new() { 101, 102, 106, 105, 110 };

        Assert.Equal(actualUnreservedRoomNumbers, UnreservedRoomNumbers(reservedRooms));
    }

    [Fact]
    public void CustomerIsFreqTraveler()
    {
        CustomersList.Clear();
        CustomersList.Add(("customer1", 1234, false));
        CustomersList.Add(("customer2", 12345, true));
        CustomersList.Add(("customer3", 123456, true));

        string testCustomerName = "customer2";

        Assert.True(IsFreqTraveler(testCustomerName));
    }

    [Fact]
    public void CustomerIsNOTFreqTraveler()
    {
        CustomersList.Clear();
        CustomersList.Add(("customer1", 1234, false));
        CustomersList.Add(("customer2", 12345, true));
        CustomersList.Add(("customer3", 123456, true));

        string testCustomerName = "customer1";

        Assert.False(IsFreqTraveler(testCustomerName));
    }

    [Fact]
    public void ListOfEachDateReport1()
    {
        RoomsList.Clear();
        RoomsList.Add((100, RoomType.Single));
        RoomsList.Add((101, RoomType.Double));

        RoomPricesList.Clear();
        RoomPricesList.Add((RoomType.Single, 500, 10));
        RoomPricesList.Add((RoomType.Double, 200, 20));

        ReservationsList.Clear();
        ReservationsList.Add(("resNum", new DateOnly(2000, 1, 1), new DateOnly(2000, 1, 10), 100, "Bob", "payConf", 5000));

        DateOnly testDateStart = new(2000, 1, 1);
        DateOnly testDateStop = new(2000, 1, 1);

        var actualList = ListOfEachDateReport(testDateStart, testDateStop, out decimal actualTotalRevenue, out decimal actualTotalExpenses);
        var expectedList = new List<(DateOnly date, List<(int roomNumber, RoomType roomType, bool isOccupied, decimal dailyRentalFee, decimal dailyCleaningCost)> eachRoomReportOfThisDate, double occupancyPercentageOfThisDate, decimal revenueOfThisDate, decimal cleaningCostsOfThisDate)>()
        {
            (date: new DateOnly(2000, 1, 1), eachRoomReportOfThisDate: new() {(100, RoomType.Single, true, 500, 10), (101, RoomType.Double, false, 0, 0)}, occupancyPercentageOfThisDate: 50, revenueOfThisDate: 500, cleaningCostsOfThisDate: 10)
        };
        decimal expectedTotalRevenue = 500;
        decimal expectedTotalExpenses = 10;

        Assert.Equal(expectedList, actualList);
        Assert.Equal(expectedTotalRevenue, actualTotalRevenue);
        Assert.Equal(expectedTotalExpenses, actualTotalExpenses);
    }

    [Fact]
    public void ListOfEachDateReport2()
    {
        RoomsList.Clear();
        RoomsList.Add((100, RoomType.Single));
        RoomsList.Add((101, RoomType.Double));

        RoomPricesList.Clear();
        RoomPricesList.Add((RoomType.Single, 500, 10));
        RoomPricesList.Add((RoomType.Double, 200, 20));

        ReservationsList.Clear();
        ReservationsList.Add(("resNum", new DateOnly(2000, 1, 1), new DateOnly(2000, 1, 2), 100, "Bob", "payConf", 5000));

        DateOnly testDateStart = new(2000, 1, 1);
        DateOnly testDateStop = new(2000, 1, 2);

        var actualList = ListOfEachDateReport(testDateStart, testDateStop, out decimal actualTotalRevenue, out decimal actualTotalExpenses);
        var expectedList = new List<(DateOnly date, List<(int roomNumber, RoomType roomType, bool isOccupied, decimal dailyRentalFee, decimal dailyCleaningCost)> eachRoomReportOfThisDate, double occupancyPercentageOfThisDate, decimal revenueOfThisDate, decimal cleaningCostsOfThisDate)>()
        {
            (date: new DateOnly(2000, 1, 1), eachRoomReportOfThisDate: new() {(100, RoomType.Single, true, 2500, 10), (101, RoomType.Double, false, 0, 0)}, occupancyPercentageOfThisDate: 50, revenueOfThisDate: 2500, cleaningCostsOfThisDate: 10) ,
            (date: new DateOnly(2000, 1, 2), eachRoomReportOfThisDate: new() {(100, RoomType.Single, true, 2500, 10), (101, RoomType.Double, false, 0, 0)}, occupancyPercentageOfThisDate: 50, revenueOfThisDate: 2500, cleaningCostsOfThisDate: 10)
        };
        decimal expectedTotalRevenue = 5000;
        decimal expectedTotalExpenses = 20;

        Assert.Equal(expectedList, actualList);
        Assert.Equal(expectedTotalRevenue, actualTotalRevenue);
        Assert.Equal(expectedTotalExpenses, actualTotalExpenses);
    }

    [Fact]
    public void RandomStringGenerator1() // because it is random and unpredictable, i will measure the performance by the lengths being what is specified
    {
        int length = 30;
        Assert.Equal(length, RandomStringGenerator(length).Length);
    }

    [Fact]
    public void RandomStringGenerator2() // because it is random and unpredictable, i will measure the performance by the lengths being what is specified
    {
        int length = 10;
        Assert.Equal(length, RandomStringGenerator(length).Length);
    }

    [Fact]
    public void RoomTypeOfRoomNumber1()
    {
        RoomsList.Clear();
        RoomsList.Add((101, RoomType.Double));
        RoomsList.Add((102, RoomType.Suite));
        RoomsList.Add((106, RoomType.Suite));
        RoomsList.Add((105, RoomType.Double));
        RoomsList.Add((110, RoomType.Double));
        RoomsList.Add((201, RoomType.Double));

        int roomNumber = 106;
        RoomType expectedRoomType = RoomType.Suite;

        Assert.Equal(expectedRoomType, RoomTypeOfRoomNumber(roomNumber));
    }

    [Fact]
    public void RoomTypeOfRoomNumber2()
    {
        RoomsList.Clear();
        RoomsList.Add((101, RoomType.Double));
        RoomsList.Add((102, RoomType.Suite));
        RoomsList.Add((106, RoomType.Suite));
        RoomsList.Add((105, RoomType.Double));
        RoomsList.Add((110, RoomType.Double));
        RoomsList.Add((201, RoomType.Double));

        int roomNumber = 101;
        RoomType expectedRoomType = RoomType.Double;

        Assert.Equal(expectedRoomType, RoomTypeOfRoomNumber(roomNumber));
    }

    [Fact]
    public void DailyRateOfRoomType1()
    {
        RoomPricesList.Clear();
        RoomPricesList.Add((RoomType.Single, 100, 10));
        RoomPricesList.Add((RoomType.Double, 200, 20));

        RoomType testRoomType = RoomType.Single;

        decimal actualDailyRate = DailyRateOfRoomType(testRoomType);
        decimal expectedDailyRate = 100;

        Assert.Equal(expectedDailyRate, actualDailyRate);
    }

    [Fact]
    public void DailyRateOfRoomType2()
    {
        RoomPricesList.Clear();
        RoomPricesList.Add((RoomType.Single, 100, 10));
        RoomPricesList.Add((RoomType.Double, 200, 20));

        RoomType testRoomType = RoomType.Double;

        decimal actualDailyRate = DailyRateOfRoomType(testRoomType);
        decimal expectedDailyRate = 200;

        Assert.Equal(expectedDailyRate, actualDailyRate);
    }

    [Fact]
    public void IndexOfRoomType1()
    {
        RoomPricesList.Clear();
        RoomPricesList.Add((RoomType.Suite, 400, 100));
        RoomPricesList.Add((RoomType.Double, 200, 100));
        RoomPricesList.Add((RoomType.Single, 100, 100));

        RoomType roomType = RoomType.Suite;
        int expectedIndex = 0;
        Assert.Equal(expectedIndex, IndexOfRoomType(roomType));
    }

    [Fact]
    public void IndexOfRoomType2()
    {
        RoomPricesList.Clear();
        RoomPricesList.Add((RoomType.Double, 200, 20));
        RoomPricesList.Add((RoomType.Single, 100, 20));
        RoomPricesList.Add((RoomType.Suite, 400, 20));

        RoomType roomType = RoomType.Suite;
        int expectedIndex = 2;
        Assert.Equal(expectedIndex, IndexOfRoomType(roomType));
    }

    [Fact]
    public void IndexOfReserv1() // ignore reservation number
    {
        ReservationsList.Clear();
        ReservationsList.Add(("reservNum1", new DateOnly(2000, 1, 2), new DateOnly(2000, 1, 2), 101, "customer1", "payConf1", 200));
        ReservationsList.Add(("reservNum2", new DateOnly(2000, 1, 2), new DateOnly(2000, 1, 2), 102, "customer2", "payConf2", 200));

        string testName = "customer1";
        string testReservNum = "-1";
        int actualIndex = IndexOfReserv(testName, testReservNum);
        int expectedIndex = 0;

        Assert.Equal(expectedIndex, actualIndex);
    }

    [Fact]
    public void IndexOfReserv2() // take into consideration reservation number
    {
        ReservationsList.Clear();
        ReservationsList.Add(("reservNum1", new DateOnly(2000, 1, 2), new DateOnly(2000, 1, 2), 101, "customer1", "payConf1", 200));
        ReservationsList.Add(("legitReservNum123", new DateOnly(2000, 1, 2), new DateOnly(2000, 1, 2), 102, "customer1", "payConf1", 200));

        string testName = "customer1";
        string testReservNum = "legitReservNum123";
        int actualIndex = IndexOfReserv(testName, testReservNum);
        int expectedIndex = 1;

        Assert.Equal(expectedIndex, actualIndex);
    }

    [Fact]
    public void ReplaceRoomPriceTuple1()
    {
        RoomPricesList.Clear();
        RoomPricesList.Add((RoomType.Suite, 400, 20));
        RoomPricesList.Add((RoomType.Double, 200, 20));
        RoomPricesList.Add((RoomType.Single, 100, 20));

        var expectedUpdatedRoomPriceTuple = (RoomType.Single, 99999, 20);

        ReplaceRoomPriceTuple(2, (RoomType.Single, 99999, 20));

        Assert.Equal(expectedUpdatedRoomPriceTuple, RoomPricesList[2]);
    }

    [Fact]
    public void ReplaceRoomPriceTuple2()
    {
        RoomPricesList.Clear();
        RoomPricesList.Add((RoomType.Suite, 400, 20));
        RoomPricesList.Add((RoomType.Double, 200, 20));
        RoomPricesList.Add((RoomType.Single, 100, 20));

        var expectedUpdatedRoomPriceTuple = (RoomType.Suite, 99999, 20);

        ReplaceRoomPriceTuple(0, (RoomType.Suite, 99999, 20));

        Assert.Equal(expectedUpdatedRoomPriceTuple, RoomPricesList[0]);
    }

    [Fact]
    public void ReplaceRoomPriceTuple3()
    {
        RoomPricesList.Clear();
        RoomPricesList.Add((RoomType.Suite, 400, 20));
        RoomPricesList.Add((RoomType.Double, 200, 20));
        RoomPricesList.Add((RoomType.Single, 100, 20));

        try
        {
            ReplaceRoomPriceTuple(3, (RoomType.Suite, 99999, 20));
            Assert.Fail("This should fail");
        }
        catch (IndexOutOfRangeException indexError)
        {
            Assert.Equal($"RoomPricesList contains less than 4 items", indexError.Message);
        }
    }

    [Fact]
    public void ReplaceReservationChargedFees1()
    {
        ReservationsList.Clear();
        ReservationsList.Add(("reservationNum1", new DateOnly(2000, 1, 1), new DateOnly(2000, 1, 2), 101, "customerName1", "payConf1", 100m));
        ReservationsList.Add(("reservationNum2", new DateOnly(2000, 1, 1), new DateOnly(2000, 1, 3), 102, "customerName2", "payConf2", 500m));
        ReservationsList.Add(("reservationNum3", new DateOnly(2000, 1, 1), new DateOnly(2000, 1, 4), 103, "customerName3", "payConf3", 300m));

        ReplaceReservationChargedFees(1, 200);
        var expectedUpdatedReservation = ("reservationNum2", new DateOnly(2000, 1, 1), new DateOnly(2000, 1, 3), 102, "customerName2", "payConf2", 200m);
        var actualUpdatedReservation = ReservationsList[1];

        Assert.Equal(expectedUpdatedReservation, actualUpdatedReservation);
    }

    [Fact]
    public void ReplaceReservationChargedFees2()
    {
        ReservationsList.Clear();
        ReservationsList.Add(("reservationNum1", new DateOnly(2000, 1, 1), new DateOnly(2000, 1, 2), 101, "customerName1", "payConf1", 100m));
        ReservationsList.Add(("reservationNum2", new DateOnly(2000, 1, 1), new DateOnly(2000, 1, 3), 102, "customerName2", "payConf2", 500m));
        ReservationsList.Add(("reservationNum3", new DateOnly(2000, 1, 1), new DateOnly(2000, 1, 4), 103, "customerName3", "payConf3", 300m));

        ReplaceReservationChargedFees(2, 900);
        var expectedUpdatedReservation = ("reservationNum3", new DateOnly(2000, 1, 1), new DateOnly(2000, 1, 4), 103, "customerName3", "payConf3", 900m);
        var actualUpdatedReservation = ReservationsList[2];

        Assert.Equal(expectedUpdatedReservation, actualUpdatedReservation);
    }

    [Fact]
    public void ReplaceReservationChargedFees3()
    {
        ReservationsList.Clear();
        ReservationsList.Add(("reservationNum1", new DateOnly(2000, 1, 1), new DateOnly(2000, 1, 2), 101, "customerName1", "payConf1", 100m));
        ReservationsList.Add(("reservationNum2", new DateOnly(2000, 1, 1), new DateOnly(2000, 1, 3), 102, "customerName2", "payConf2", 500m));
        ReservationsList.Add(("reservationNum3", new DateOnly(2000, 1, 1), new DateOnly(2000, 1, 4), 103, "customerName3", "payConf3", 300m));

        try
        {
            ReplaceReservationChargedFees(100, 900);
            Assert.Fail("This should fail");
        }
        catch (IndexOutOfRangeException indexError)
        {
            Assert.Equal("ReservationsList contains less than 101 items", indexError.Message);
        }
    }


    [Fact]
    public void ListOfCustomersWithName1()
    {
        CustomersList.Clear();
        CustomersList.Add(("customerName", 1234, true));
        CustomersList.Add(("customerName", 12345, false));
        CustomersList.Add(("customerName", 123456, true));
        CustomersList.Add(("NameCustomer", 1234567, true));
        CustomersList.Add(("NameCustomer", 12345678, false));

        string testName = "NameCustomer";
        var actualList = ListOfCustomersWithName(testName);
        var expectedList = new List<(string customerName, long cardNumber, bool freqTravelerStatus)>()
        {
        ("NameCustomer", 1234567, true) ,
        ("NameCustomer", 12345678, false)
        };

        Assert.Equal(expectedList, actualList);
    }

    [Fact]
    public void ListOfCustomersWithName2()
    {
        CustomersList.Clear();
        CustomersList.Add(("customerName", 1234, true));
        CustomersList.Add(("customerName", 12345, false));
        CustomersList.Add(("customerName", 123456, true));
        CustomersList.Add(("NameCustomer", 1234567, true));
        CustomersList.Add(("NameCustomer", 12345678, false));

        string testName = "customerName";
        var actualList = ListOfCustomersWithName(testName);
        var expectedList = new List<(string customerName, long cardNumber, bool freqTravelerStatus)>()
        {
        ("customerName", 1234, true) ,
        ("customerName", 12345, false) ,
        ("customerName", 123456, true)
        };

        Assert.Equal(expectedList, actualList);
    }

    [Fact]
    public void ListOfReservationsUnderName1()
    {
        ReservationsList.Clear();
        ReservationsList.Add(("reservationNum1", new DateOnly(2000, 1, 1), new DateOnly(2000, 1, 2), 101, "NameCustomer", "payConf1", 100m));
        ReservationsList.Add(("reservationNum2", new DateOnly(2000, 1, 1), new DateOnly(2000, 1, 3), 102, "NameCustomer", "payConf2", 500m));
        ReservationsList.Add(("reservationNum3", new DateOnly(2000, 1, 1), new DateOnly(2000, 1, 4), 103, "customerName3", "payConf3", 300m));
        ReservationsList.Add(("reservationNum4", new DateOnly(2000, 1, 1), new DateOnly(2000, 1, 5), 104, "customerName3", "payConf4", 700m));

        string testName = "customerName3";
        var actualList = ListOfReservationsUnderName(testName);
        var expectedList = new List<(string, DateOnly, DateOnly, int, string, string, decimal)>()
        {
            ("reservationNum3", new DateOnly(2000, 1, 1), new DateOnly(2000, 1, 4), 103, "customerName3", "payConf3", 300m) ,
            ("reservationNum4", new DateOnly(2000, 1, 1), new DateOnly(2000, 1, 5), 104, "customerName3", "payConf4", 700m)
        };

        Assert.Equal(expectedList, actualList);
    }

    [Fact]
    public void ListOfReservationsUnderName2()
    {
        ReservationsList.Clear();
        ReservationsList.Add(("reservationNum1", new DateOnly(2000, 1, 1), new DateOnly(2000, 1, 2), 101, "NameCustomer", "payConf1", 100m));
        ReservationsList.Add(("reservationNum2", new DateOnly(2000, 1, 1), new DateOnly(2000, 1, 3), 102, "NameCustomer", "payConf2", 500m));
        ReservationsList.Add(("reservationNum3", new DateOnly(2000, 1, 1), new DateOnly(2000, 1, 4), 103, "customerName3", "payConf3", 300m));
        ReservationsList.Add(("reservationNum4", new DateOnly(2000, 1, 1), new DateOnly(2000, 1, 5), 104, "customerName3", "payConf4", 700m));

        string testName = "NameCustomer";
        var actualList = ListOfReservationsUnderName(testName);
        var expectedList = new List<(string, DateOnly, DateOnly, int, string, string, decimal)>()
        {
            ("reservationNum1", new DateOnly(2000, 1, 1), new DateOnly(2000, 1, 2), 101, "NameCustomer", "payConf1", 100m) ,
            ("reservationNum2", new DateOnly(2000, 1, 1), new DateOnly(2000, 1, 3), 102, "NameCustomer", "payConf2", 500m)
        };

        Assert.Equal(expectedList, actualList);
    }

    [Fact]
    public void CardNumberISInHere()
    {
        var listOfCustomersWithSameName = new List<(string, long, bool)>()
        {
            ("customerName", 1234, true) ,
            ("customerName", 12345, false) ,
            ("customerName", 123456, true)
        };

        long testDesiredCardNumber = 123456;

        Assert.True(CardNumberIsInHere(testDesiredCardNumber, listOfCustomersWithSameName));
    }

    [Fact]
    public void CardNumberIsNOTInHere()
    {
        var listOfCustomersWithSameName = new List<(string, long, bool)>()
        {
            ("customerName", 1234, true) ,
            ("customerName", 12345, false) ,
            ("customerName", 123456, true)
        };

        long testDesiredCardNumber = 1234567;

        Assert.False(CardNumberIsInHere(testDesiredCardNumber, listOfCustomersWithSameName));
    }

    [Fact]
    public void ApplyFreqTravelerDiscountOf15Percent() // this will fail if the const DisCountPercentageForFreqTravelers gets changed
    {
        RoomPricesList.Clear();
        RoomPricesList.Add((RoomType.Single, 200, 20));
        RoomPricesList.Add((RoomType.Double, 300, 30));
        RoomPricesList.Add((RoomType.Suite, 400, 40));

        RoomsList.Clear();
        RoomsList.Add((100, RoomType.Single));
        RoomsList.Add((101, RoomType.Double));
        RoomsList.Add((102, RoomType.Suite));

        // test customer & reservation info:
        bool freqTravelerStatus = true;
        var dateStart = new DateOnly(2000, 1, 1);
        var dateStop = new DateOnly(2000, 1, 3);
        int roomNumber = 102;

        decimal expectedFeesAfterDiscount = 1020;

        decimal actualFees = ApplyDiscount(freqTravelerStatus, dateStart, dateStop, roomNumber);
        Assert.Equal(expectedFeesAfterDiscount, actualFees);
    }

    [Fact]
    public void ApplyNONFreqTravelerDiscountOf15Percent() // this will fail if the const DisCountPercentageForFreqTravelers gets changed
    {
        RoomPricesList.Clear();
        RoomPricesList.Add((RoomType.Single, 200, 20));
        RoomPricesList.Add((RoomType.Double, 300, 30));
        RoomPricesList.Add((RoomType.Suite, 400, 40));

        RoomsList.Clear();
        RoomsList.Add((100, RoomType.Single));
        RoomsList.Add((101, RoomType.Double));
        RoomsList.Add((102, RoomType.Suite));

        // test customer & reservation info:
        bool freqTravelerStatus = false;
        var dateStart = new DateOnly(2000, 1, 1);
        var dateStop = new DateOnly(2000, 1, 3);
        int roomNumber = 100;

        decimal expectedFeesAfterDiscount = 600;

        decimal actualFees = ApplyDiscount(freqTravelerStatus, dateStart, dateStop, roomNumber);
        Assert.Equal(expectedFeesAfterDiscount, actualFees);
    }

    [Fact]
    public void ApplyCouponDiscount1()
    {
        CouponCodesList.Clear();
        CouponCodesList.Add(("couponCode1", 10));
        CouponCodesList.Add(("couponCode2", 20));
        CouponCodesList.Add(("BestCoupon", 100));

        decimal initialChargedFees = 1000;
        int indexOfApplyingCouponCode = 2;
        decimal expectedFeesAfterCoupon = 0;

        var actualFees = ApplyDiscount(initialChargedFees, indexOfApplyingCouponCode);

        Assert.Equal(expectedFeesAfterCoupon, actualFees);
    }

    [Fact]
    public void ApplyCouponDiscount2()
    {
        CouponCodesList.Clear();
        CouponCodesList.Add(("couponCode1", 10));
        CouponCodesList.Add(("couponCode2", 20));
        CouponCodesList.Add(("BestCoupon", 100));

        decimal initialChargedFees = 2000;
        int indexOfApplyingCouponCode = 0;
        decimal expectedFeesAfterCoupon = 1800;

        var actualFees = ApplyDiscount(initialChargedFees, indexOfApplyingCouponCode);

        Assert.Equal(expectedFeesAfterCoupon, actualFees);
    }

    [Fact]
    public void ConvertDecimalToPercentage1()
    {
        decimal inDecimalForm = 0.50m;
        decimal expectedPercentage = 50m;

        var actual = ConvertDecimalToPercentage(inDecimalForm);

        Assert.Equal(expectedPercentage, actual);
    }

    [Fact]
    public void ConvertDecimalToPercentage2()
    {
        decimal inDecimalForm = 1m;
        decimal expectedPercentage = 100m;

        var actual = ConvertDecimalToPercentage(inDecimalForm);

        Assert.Equal(expectedPercentage, actual);
    }

    [Fact]
    public void ConvertPercentageToDecimal1()
    {
        decimal inPercentage = 25m;
        decimal expectedDecimal = 0.25m;

        var actual = ConvertPercentageToDecimal(inPercentage);

        Assert.Equal(expectedDecimal, actual);
    }

    [Fact]
    public void ConvertPercentageToDecimal2()
    {
        decimal inPercentage = 100m;
        decimal expectedDecimal = 1m;

        var actual = ConvertPercentageToDecimal(inPercentage);

        Assert.Equal(expectedDecimal, actual);
    }

    [Fact]
    public void ReservISInThePast()
    {
        ReservationsList.Clear();
        ReservationsList.Add(("reservNum1", new DateOnly(2001, 1, 1), new DateOnly(2001, 1, 10), 100, "John Doe", "paymentConf1", 700));
        ReservationsList.Add(("reservNum2", new DateOnly(2000, 2, 1), new DateOnly(2000, 2, 10), 101, "John Do", "paymentConf2", 600));
        ReservationsList.Add(("reservNum3", new DateOnly(2024, 3, 1), new DateOnly(2024, 3, 10), 102, "John D", "paymentConf3", 500));

        int testIndexOfReservation = 0;

        Assert.True(ReservIsInThePast(testIndexOfReservation));
    }
    [Fact]
    public void ReservIsNOTInThePast()
    {
        ReservationsList.Clear();
        ReservationsList.Add(("reservNum1", new DateOnly(2001, 1, 1), new DateOnly(2001, 1, 10), 100, "John Doe", "paymentConf1", 700));
        ReservationsList.Add(("reservNum2", new DateOnly(2000, 2, 1), new DateOnly(2000, 2, 10), 101, "John Do", "paymentConf2", 600));
        ReservationsList.Add(("reservNum3", new DateOnly(2024, 3, 1), new DateOnly(2024, 3, 10), 102, "John D", "paymentConf3", 500));

        int testIndexOfReservation = 2;

        Assert.False(ReservIsInThePast(testIndexOfReservation));
    }

    [Fact]
    public void ListOfDaysBetween1()
    {
        DateOnly dateStart = new(2000, 1, 1);
        DateOnly dateStop = new(2000, 1, 1);
        List<DateOnly> expectedList = new()
        {
            new DateOnly(2000, 1, 1)
        };

        var actualList = ListOfDaysBetween(dateStart, dateStop);

        Assert.Equal(expectedList, actualList);
    }

    [Fact]
    public void ListOfDaysBetween2()
    {
        DateOnly dateStart = new(2000, 1, 1);
        DateOnly dateStop = new(2000, 1, 5);
        List<DateOnly> expectedList = new()
        {
            new DateOnly(2000, 1, 1) ,
            new DateOnly(2000, 1, 2) ,
            new DateOnly(2000, 1, 3) ,
            new DateOnly(2000, 1, 4) ,
            new DateOnly(2000, 1, 5)
        };

        var actualList = ListOfDaysBetween(dateStart, dateStop);

        Assert.Equal(expectedList, actualList);
    }

    [Fact]
    public void RoomISOccupiedThisDate()
    {
        RoomPricesList.Clear();
        RoomPricesList.Add((RoomType.Single, 100, 1));
        RoomPricesList.Add((RoomType.Double, 200, 10));
        RoomPricesList.Add((RoomType.Suite, 300, 100));

        RoomsList.Clear();
        RoomsList.Add((100, RoomType.Single));
        RoomsList.Add((101, RoomType.Single));
        RoomsList.Add((102, RoomType.Single));
        RoomsList.Add((105, RoomType.Double));
        RoomsList.Add((103, RoomType.Suite));

        ReservationsList.Clear();
        ReservationsList.Add(("reservNum1", new DateOnly(2000, 1, 1), new DateOnly(2000, 1, 10), 100, "John Doe", "paymentConf1", 700));
        ReservationsList.Add(("reservNum2", new DateOnly(2000, 2, 1), new DateOnly(2000, 2, 10), 101, "John Do", "paymentConf2", 600));
        ReservationsList.Add(("reservNum3", new DateOnly(2000, 3, 1), new DateOnly(2000, 3, 10), 102, "John D", "paymentConf3", 900));

        var actualBool = RoomIsOccupiedThisDate(102, new DateOnly(2000, 3, 5), out decimal actualDailyRevenue, out decimal actualDailyCleaningCost);
        decimal expectedDailyRevenue = 90m;
        decimal expectedDailyCleaningCost = 1;

        Assert.True(actualBool);
        Assert.Equal(expectedDailyRevenue, actualDailyRevenue);
        Assert.Equal(expectedDailyCleaningCost, actualDailyCleaningCost);
    }
    [Fact]
    public void RoomIsNOTOccupiedThisDate()
    {
        RoomPricesList.Clear();
        RoomPricesList.Add((RoomType.Single, 999, 1));
        RoomPricesList.Add((RoomType.Double, 9999, 10));
        RoomPricesList.Add((RoomType.Suite, 99999, 100));

        RoomsList.Clear();
        RoomsList.Add((100, RoomType.Single));
        RoomsList.Add((101, RoomType.Suite));
        RoomsList.Add((102, RoomType.Single));
        RoomsList.Add((105, RoomType.Double));
        RoomsList.Add((103, RoomType.Suite));

        ReservationsList.Clear();
        ReservationsList.Add(("reservNum1", new DateOnly(2000, 1, 1), new DateOnly(2000, 1, 10), 100, "John Doe", "paymentConf1", 700));
        ReservationsList.Add(("reservNum2", new DateOnly(2000, 2, 1), new DateOnly(2000, 2, 10), 101, "John Do", "paymentConf2", 600));
        ReservationsList.Add(("reservNum3", new DateOnly(2000, 3, 1), new DateOnly(2000, 3, 10), 102, "John D", "paymentConf3", 500));

        var actualBool = RoomIsOccupiedThisDate(105, new DateOnly(2000, 3, 5), out decimal actualDailyRevenue, out decimal actualDailyCleaningCost);
        decimal expectedDailyRevenue = 0m;
        decimal expectedDailyCleaningCost = 0m;

        Assert.False(actualBool);
        Assert.Equal(expectedDailyRevenue, actualDailyRevenue);
        Assert.Equal(expectedDailyCleaningCost, actualDailyCleaningCost);
    }

}