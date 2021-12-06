namespace Floward.OnlineStore.Core.Models
{
    // byte because it is tinyint in the DB to be used in DB mapping
    public enum ProductType : byte 
    {
        Flower = 1,
        Electronic = 2,
        Food = 3
    }

    public enum OrderStatus : byte
    {
        InCart = 1,
        WishList = 2,
        Paid = 10,
        Return = 11,
        Claim = 12
    }
}
