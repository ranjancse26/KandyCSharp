using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;
using KandyCSharp.Kandy;
using KandyCSharp.Entities;
using KandyCSharp.Entities.Message;
using Message = KandyCSharp.Entities.Message.Message;
using System.Drawing;
using System.Drawing.Imaging;

namespace KandyCSharpConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var saraImageString = ReadImage("sara-arjun.jpg");
            var jsonObjectDumpper = new JsonObjectDumper();
            var deviceService = new DeviceService();
            var accountService = new AccountService();
            var accountAccessToken = accountService.GetAccountAccessToken();

            var userAccessTokenService = new UserAccessTokenService();
            var userAccessToken = userAccessTokenService.GetUserAccessToken("user2");

            var deviceId = "abc2fa752c3c4edf97de8b0a12f622f0";
            var textMessage = new MessageToSendRoot
            {
                MessageToSend = new MessageToSend
                {
                    Message = new Message
                    {
                        MimeType = "text/plain",
                        Text = "let's meet tonight"
                    },
                    UUID = deviceId,
                    ContentType = "text",
                    Destination = "user2@kandy.hypercat.gmail.com"
                }
            };

            // Example to send message to device
            SendMessageToDevice(userAccessToken, deviceId, textMessage);

            // Send Image as Message
            var imageMessage = new MessageToSendRoot
            {
                MessageToSend = new MessageToSend
                {
                    Message = new Message
                    {
                        MimeType = "image/jpeg",
                        Text = saraImageString
                    },
                    UUID = deviceId,
                    ContentType = "image",
                    Destination = "user2@kandy.hypercat.gmail.com"
                }
            };
            SendImageToDevice(userAccessToken, deviceId, imageMessage);

            // Fetch Device Addressbook Contacts
            var deviceAddressBookContacts = deviceService.GetDeviceAddressBookContacts(userAccessToken,
                deviceId);

            // Delete Handled Messages
            //deviceService.DeleteHandledMessages(userAccessToken,
            //    deviceId, new List<string> {
            //        "A149EBB9A69D4E66B62D26FBDEF440C5"});

            // Create a new user device
            var deviceEntity = new DeviceEntityCreate
            {
                NativeId = "353897050095214",
                Family = "Windows",
                Name = "Windows Phone 8",
                ClientSoftwareVersion = "0102001",
                OperatingSystemVersion = "8.0.10328.78"
            };

            // Fetch and Create new device
            var devices = deviceService.GetDevices(userAccessToken);
            Console.WriteLine(jsonObjectDumpper.WriteToString(devices));
            var newDeviceId = deviceService.Create(userAccessToken, deviceEntity);
          
            var domains = accountService.GetDomains(accountAccessToken);
            var newDomain = accountService.CreateDomain(accountAccessToken, "TestDomain", "Test");

            var domainService = new DomainService();
            var domainAccessToken = domainService.GetDomainAccessToken();
          
            // Fetch and create user contacts
            var userAddressBookService = new UserAddressBookService(userAccessToken);
            var contacts = userAddressBookService.GetContacts();
            Console.WriteLine(jsonObjectDumpper.WriteToString(contacts.ContactResultEntity.Contacts));

            var contactId = userAddressBookService.AddContact(new ContactEntity
            {
                FirstName = "Test",
                LastName = "Test 123",
                Email = "test@gmail.com",
                Nickname = "Testing",
                Name = "Test",
                MobileNumber = "12244199894"
            });
            Console.WriteLine("Contact Id: {0}", contactId);

            // Group related
            string groupId = "6f6db62a2d0c4900b7c8b70661442194";
            var groupService = new GroupService(userAccessToken);
            var groupInfo = groupService.FindGroupById(groupId);
            var allGroups = groupService.GetAllGroups();
            Console.WriteLine(jsonObjectDumpper.WriteToString(allGroups));

            // Add Group Members
            var groupMemeberCreationStatus = 
                groupService.AddGroupMembers(userAccessToken, groupId,
                    new List<string>
                    {
                        "member1@kandy.hypercat.gmail.com",
                        "member2@kandy.hypercat.gmail.com",
                    });

            var createdGroupId = groupService.CreateGroup(new GroupEntity
            {
                GroupName = "Test Group",
                GroupImage = string.Empty
            });
            Console.WriteLine("Newly Created Group Id: {0}", createdGroupId);

            // Send GroupMessage
            var groupMessage = new GroupMessageToSendRoot
            {
                MessageToSend = new GroupMessageToSend
                {
                    GroupId = "6f6db62a2d0c4900b7c8b70661442194",
                    Message = new Message
                    {
                        MimeType = "text/plain",
                        Text = "Sending test message to group"
                    },
                    UUID = deviceId,
                    ContentType = "text",
                    Destination = "user2@kandy.hypercat.gmail.com"
                }
            };
            groupService.SendGroupMessage(userAccessToken,
                JsonConvert.SerializeObject(groupMessage));

            Console.ReadLine();
        }
        
        private static string ReadImage(string imageFileName)
        {
            string appDirectory = Path.GetDirectoryName(Application.ExecutablePath);
            string path = $"{appDirectory}{"\\Images\\"}" + imageFileName;
            var image = new Bitmap(path);
            return Base64EncodeDecodeHelper.ImageToBase64(image, ImageFormat.Jpeg);
        }

        private static void SendImageToDevice(string userAccessToken,
                                              string deviceUniqueId,
                                              MessageToSendRoot messageToSend)
        {
            SendMessageToDevice(userAccessToken, deviceUniqueId, messageToSend);
        }

        private static void SendMessageToDevice(string userAccessToken,
                                                string deviceUniqueId,
                                                MessageToSendRoot messageToSend)
        {
            var deviceService = new DeviceService();
            
            // Send a message to device
            var status = deviceService.SendIm(userAccessToken,
                deviceUniqueId, JsonConvert.SerializeObject(messageToSend));

            Console.WriteLine("Status: {0}", status);
        }
    }
}
