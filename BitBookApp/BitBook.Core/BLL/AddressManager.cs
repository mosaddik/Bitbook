using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BitBookApp.BitBook.Core.DAL;
using BitBookApp.Models;

namespace BitBookApp.BitBook.Core.BLL
{
    public class AddressManager
    {
        AddressGetway addressGetway =  new AddressGetway();

        public bool SaveAddress(Address address,User currentUser)
        {
            addressGetway.SaveAddress(address);
            var user = new User();
            user.Address = addressGetway.GetRecentAddedData();
            user.Id = currentUser.Id;
            addressGetway.SetRelationShipToUser(user);
            return true;          
        }

        public bool UpdateAddress(Address address)
        {
            return addressGetway.UpdateAddress(address);

          
        }

        public bool MakeADisitionUpdateOrSave(Address address,User sessonUser)
        {

            address.Id = addressGetway.GetAddressId(sessonUser);


            if (address.Id == 0)
            {
                SaveAddress(address, sessonUser);
            }
            else
            {
                UpdateAddress(address);
            }

            return true;
        }

        public Address GetAddressByUserId(User currentUser)
        {
            currentUser.Address.Id = addressGetway.GetAddressId(currentUser);
            return  addressGetway.GetAddressById(currentUser);
        }

    }
}