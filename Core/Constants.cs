using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    public enum UserGender { MALE, FEMALE, OTHER }
    public enum UserRole { ADMIN,SUPPLIER, CUSTOMERS}

    public enum AddressType { HOME, OFFICE, OTHER }

    public enum EmailStatus { PENDING, SENT, OTHER }
    public enum TransactionType { DEBIT, CREDIT }

    public enum MilkType {
        BUFFALO, COW, OTHER }
    
    public enum TimeBatch { MORNING , EVENING }
    
    public enum MilkCollectStatus { COLLECTED, DISTRIBUTED};

    public enum OrderStatus
    {
        Incart,
        Ordered,
        Approved,
        Placed,
        Packed,
        Shipped,
        Deliverered,
        Cancelled,
        Cancelapproved,
        Returned
    }




    public class UserInfo
    {
        public UserInfo(int UserGuid, UserRole role, string value3)
        {
            Id = UserGuid;
            Role = role;
            Name = value3;
        }
        
        public UserRole Role { get; set; }

        public int Id { get; set; }
        public string Name { get; set; }
    }
    class Constants
    {

    }
}
