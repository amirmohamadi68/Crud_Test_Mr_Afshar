namespace Mc2.CrudTest.Domain.Customers;

    //for reviwer :
    // i would not impilmentin IEqutable interface for strongly type ids... there is better way to avoid overriding those methods...
    public record CustomerId(Guid CId);
    
    
