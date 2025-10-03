
using LibraryApp.Application.DTOs.RequestDTO.Address;

namespace LibraryApp.Mappers;

public static class AddressMethods
{

    
    public static AddressCreateDTO MapDomainEntityToDTO(this Address address)
    {
        return new AddressCreateDTO
        {
            id = address.id,
            number = address.number,
            street = address.street
        };
    }

   
     public static Address MapDTOToDomainEntity(this AddressCreateDTO address,City city)
    {
        return new Address
        {
            id = address.id,
            number = address.number,
            street = address.street,
            City = city,
            PostalCode=city.PostalCode                        
        };
    }


    

}