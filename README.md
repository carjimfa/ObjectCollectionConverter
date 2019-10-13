# ObjectCollectionConverter

Converts an object to different collection types.

Collection types supported:

-Dictionary

## Install

```powershell
Install-Package ObjectCollectionConverter -Version 0.0.1
```
## Overview

After install, we can convert any object-derived object into a Dictionary. 

```csharp
    //We create three different classes User, Address and ZipCode 
    //(they make no sense, but it's just for test purposes)

    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public Address Address { get; set; }
    }

    public class Address
    {
        public string City { get; set; }
        public string AddressLine { get; set; }
        public string AddressLine2 { get; set; }
        public ZipCode ZipCode { get; set; }
    }

    public class ZipCode
    {
        public int NumericValue { get; set; }
        public decimal DecimalValue { get; set; }
        public String StringValue { get; set; }
    }
```

Then we initialize an object User

```csharp
var user = new User
    {
        FirstName = "John",
        LastName = "Doe",
        Age = 23,
        Address = new Address
        {
            AddressLine = "False Street",
            AddressLine2 = "123",
            City = "USA",
            ZipCode = new ZipCode
            {
                DecimalValue=(decimal)10.0,
                NumericValue=2,
                StringValue="TestStringLALALA"
            }
        }
    };
```

And we can now convert the object to a <string,object> Dictionary or to a <string, string> Dictionary.

```csharp
    var dictionary = user.ToDictionary();
    var stringDictionary = user.ToStringDictionary();
```

The string Dictionary will be a Dictionary with string keys and string values where inner objects are created with ':' separator. The User Dictionary will be something like this JSON:

```json
{
    "FirstName":"John",
    "LastName":"Doe",
    ...
    "Address:AddressLine":"False Street",
    "Address:AddressLine2":"123",
    ...
    "Address:ZipCode:DecimalValue":"10.0",
    "Address:ZipCode:NumericValue":"2",
    "Address:ZipCode:StringValue":"TestStringLALALA"
}
```

## Change Log

2019-10-13 : Updates to Net Core 2.2