using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitBookApp.BitBook.Core.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BitBookApp.Models;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using FriendRequest = BitBookApp.Models.FriendRequest;

namespace BitBook.Test
{
    [TestClass]
    class DalTest
    {

    [TestMethod]
        public void ShuldReturnTrue()
        {
            //arrenege
            FriendRequestGetway friendRequest = new FriendRequestGetway();
            //act 
            FriendRequest friendReqeust1 = new FriendRequest()
            {
                Id = 1, User1 = new User()
                {
                    Id = 1
                },
                User2 = new User()
                {
                    Id = 2
                }

            };
           bool result =  friendRequest.Add(friendReqeust1);
            //Assert
          Assert.AreEqual(result,true);

        }

  
        


    }
}
