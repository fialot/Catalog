using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Google.Contacts;
using Google.GData.Contacts;
using Google.GData.Client;
using Google.GData.Extensions;
using System.IO;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Util.Store;

namespace GContacts
{
    public struct contacts
    {
        public cName Name;
        public List<cCompany> Company;
        public List<cValue> Phone;
        public List<cValue> Email;
        public List<cAddress> Address;
        public DateTime BirthDate;
        public List<cDates> Date;
        public List<cValue> Web;
        public List<cValue> IM;
        public List<cValue> Relation;
        public string Note;

        public string Group;

        public string gID;              // google ID
    }

    public struct cName
    {
        public string FullName;
        public string Firstname;
        public string AdditionalName;
        public string Surname;
        public string FirstnamePhonetic;
        public string AdditionalNamePhonetic;
        public string SurnamePhonetic;
        public string Nick;

        public string Prefix;
        public string Suffix;
    }

    public struct cCompany
    {
        public string Name;
        public string Position;
        public cAddress Address;
    }

    public struct cValue
    {
        public string Value;
        public string Desc;
        public bool Primary;
    }

    public struct cAddress
    {
        public string Street;
        public string City;
        public string Region;
        public string ZipCode;
        public string Country;

        public string Tag;
        public bool Primary;
    }

    public struct cDates
    {
        public DateTime Date;
        public string Tag;
        public bool AlLDay;
    }

    public struct cGroups
    {
        public string ID;
        public string Name;
        public bool System;
    }

    class GoogleContacts
    {
        public List<contacts> Contact;
        public List<cGroups> Groups;

        ContactsRequest cr;

        public static string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private ContactsRequest GetContactRequest(string user)
        {
            string AppName = "Fialot catalog";
            string clientId = "109433857970-aqsm7jd6fqqkd0l7b413lu12gb1h8nca.apps.googleusercontent.com";
            string clientSecret = "4aU49fgGxIULQahEd1o1nOnI";

            string[] scopes = new string[] { "https://www.google.com/m8/feeds/" };     // view your basic profile info.

            //string[] scopes = new string[] { "https://www.googleapis.com/auth/contacts.readonly" };     // view your basic profile info.

            //string[] scopes = new string[] { "https://www.google.com/m8/feeds/groups/fialot@gmail.com/full" };     // view your basic profile info.

            // Use the current Google .net client library to get the Oauth2 stuff.
            UserCredential credential = GoogleWebAuthorizationBroker.AuthorizeAsync(new ClientSecrets { ClientId = clientId, ClientSecret = clientSecret }
                                                                                         , scopes
                                                                                         , user
                                                                                         , CancellationToken.None
                                                                                         , new FileDataStore("test")).Result;

            // Translate the Oauth permissions to something the old client libray can read
            OAuth2Parameters parameters = new OAuth2Parameters();
            parameters.AccessToken = credential.Token.AccessToken;
            parameters.RefreshToken = credential.Token.RefreshToken;
            parameters.TokenType = credential.Token.TokenType;
            parameters.TokenExpiry = DateTime.Now.AddYears(1);




            RequestSettings settings = new RequestSettings(AppName, parameters);
            //settings.Credentials = new GDataCredentials("mfialot@gmail.com", "timoty");
            return new ContactsRequest(settings);
        }

        public bool Login(ref string user)
        {
            if (user == "") user = "user";
            cr = GetContactRequest(user);

            try
            {
                Feed<Google.Contacts.Group> fg = cr.GetGroups();
                int x = fg.PageSize;
            }
            catch (Exception Err)
            {
                user = RandomString(10);
                cr = GetContactRequest(user);
            }

            return true;
        }

        public void ImportGmail()
        {
            // ----- Get Groups -----
            Groups = new List<cGroups>();
            string MyContactID = "";

            Feed<Google.Contacts.Group> fg = cr.GetGroups();

            foreach (Google.Contacts.Group group in fg.Entries)
            {
                cGroups gItem = new cGroups();
                gItem.ID = group.Id;
                gItem.Name = group.Title;
                gItem.System = false;
                if (!string.IsNullOrEmpty(group.SystemGroup))
                {
                    gItem.Name = group.SystemGroup;
                    gItem.System = true;
                    if (gItem.Name == "Contacts") MyContactID = gItem.ID;
                }
                Groups.Add(gItem);
            }



            Contact = new List<contacts>();

            ContactsQuery query = new ContactsQuery(ContactsQuery.CreateContactsUri("default"));
            query.StartIndex = 0;
            query.ShowDeleted = false;
            if (MyContactID != "")
                query.Group = MyContactID;

            Feed<Google.Contacts.Contact> f = cr.Get<Google.Contacts.Contact>(query);
            int contactsCount = f.Entries.Count();
            while (contactsCount > 0)
            {
                foreach (Google.Contacts.Contact entry in f.Entries)
                {

                    contacts item = new contacts();
                    if (entry.Name != null)
                    {
                        // ----- NAME -----
                        Name name = entry.Name;
                        if (!string.IsNullOrEmpty(name.FullName))
                            item.Name.FullName = name.FullName;
                        if (!string.IsNullOrEmpty(name.NamePrefix))
                            item.Name.Prefix = name.NamePrefix;
                        if (!string.IsNullOrEmpty(name.GivenName))
                            item.Name.Firstname = name.GivenName;
                        if (!string.IsNullOrEmpty(name.GivenNamePhonetics))
                            item.Name.FirstnamePhonetic = name.GivenNamePhonetics;
                        if (!string.IsNullOrEmpty(name.AdditionalName))
                            item.Name.AdditionalName = name.AdditionalName;
                        if (!string.IsNullOrEmpty(name.AdditionalNamePhonetics))
                            item.Name.AdditionalNamePhonetic = name.AdditionalNamePhonetics;
                        if (!string.IsNullOrEmpty(name.FamilyName))
                            item.Name.Surname = name.FamilyName;
                        if (!string.IsNullOrEmpty(name.FamilyNamePhonetics))
                            item.Name.SurnamePhonetic = name.FamilyNamePhonetics;
                        if (!string.IsNullOrEmpty(name.NameSuffix))
                            item.Name.Suffix = name.NameSuffix;
                        if (!string.IsNullOrEmpty(entry.ContactEntry.Nickname))
                            item.Name.Nick = entry.ContactEntry.Nickname;

                        // ----- EMAIL -----
                        foreach (EMail email in entry.Emails)
                        {
                            cValue cItem;
                            item.Email = new List<cValue>();
                            cItem.Value = email.Address;
                            if (email.Rel != null)
                            {
                                cItem.Desc = email.Rel.Replace("http://schemas.google.com/g/2005", "");
                                if (cItem.Desc == "#other")
                                    cItem.Desc = email.Label;
                            }
                            else cItem.Desc = email.Label;
                            cItem.Primary = email.Primary;
                            item.Email.Add(cItem);
                        }

                        // ----- PHONE -----
                        foreach (PhoneNumber phone in entry.Phonenumbers)
                        {
                            cValue cItem;
                            item.Phone = new List<cValue>();
                            cItem.Value = phone.Value;
                            if (phone.Rel != null)
                            {
                                cItem.Desc = phone.Rel.Replace("http://schemas.google.com/g/2005", "");
                                if (cItem.Desc == "#other")
                                    cItem.Desc = phone.Label;
                            }
                            else cItem.Desc = phone.Label;
                            cItem.Primary = phone.Primary;
                            item.Phone.Add(cItem);
                        }

                        // ----- Organization -----
                        foreach (Organization organization in entry.Organizations)
                        {
                            cCompany cItem;
                            item.Company = new List<cCompany>();
                            cItem.Name = organization.Name;
                            cItem.Position = organization.Title;
                            cItem.Address = new cAddress();
                            item.Company.Add(cItem);
                        }

                        // ----- ADDRESS -----
                        foreach (StructuredPostalAddress address in entry.PostalAddresses)
                        {
                            cAddress aItem;
                            item.Address = new List<cAddress>();
                            aItem.Street = address.Street;
                            aItem.City = address.City;
                            aItem.Region = address.Region;
                            aItem.Country = address.Country;
                            aItem.ZipCode = address.Postcode;
                            aItem.Primary = address.Primary;
                            aItem.Tag = "";
                            if (address.Rel != null)
                            {
                                aItem.Tag = address.Rel.Replace("http://schemas.google.com/g/2005", "");
                                if (aItem.Tag == "#other")
                                    aItem.Tag = address.Label;
                            }
                            item.Address.Add(aItem);
                        }

                        // ----- BIRTHDATE -----
                        DateTime date = DateTime.MinValue;
                        if (DateTime.TryParse(entry.ContactEntry.Birthday, out date)) item.BirthDate = date;

                        // ----- DATES -----
                        foreach (Event ev in entry.ContactEntry.Events)
                        {
                            cDates eItem;
                            item.Date = new List<cDates>();
                            eItem.Date = ev.When.StartTime;
                            if (ev.Relation != null) eItem.Tag = ev.Relation;
                            else eItem.Tag = ev.Label;
                            eItem.AlLDay = ev.When.AllDay;
                            item.Date.Add(eItem);
                        }

                        // ----- WEBSITES -----
                        foreach (Website web in entry.ContactEntry.Websites)
                        {
                            cValue wItem;
                            item.Web = new List<cValue>();
                            wItem.Value = web.Href;
                            wItem.Primary = web.Primary;
                            if (web.Rel != null) wItem.Desc = web.Rel;
                            else wItem.Desc = web.Label;
                            item.Web.Add(wItem);
                        }

                        // ----- IM -----
                        foreach (IMAddress IM in entry.IMs)
                        {
                            cValue imItem;
                            item.IM = new List<cValue>();
                            imItem.Value = IM.Address;
                            imItem.Primary = false;
                            imItem.Desc = IM.Protocol;
                            if (IM.Protocol != null)
                            {
                                imItem.Desc = IM.Protocol.Replace("http://schemas.google.com/g/2005", "");
                            }
                            item.IM.Add(imItem);
                        }

                        // ----- RELATIONS -----
                        foreach (Relation rel in entry.ContactEntry.Relations)
                        {
                            cValue rItem;
                            item.Relation = new List<cValue>();
                            rItem.Value = rel.Value;
                            rItem.Primary = false;
                            if (rel.Rel != null) rItem.Desc = rel.Rel;
                            else rItem.Desc = rel.Label;
                            item.Relation.Add(rItem);
                        }


                        // ----- NOTE -----
                        item.Note = entry.Content;


                        // ----- google ID -----
                        item.gID = entry.AtomEntry.Id.AbsoluteUri;


                        // ----- GROUP -----
                        item.Group = "";
                        bool haveGroup = false;
                        foreach (GroupMembership groupMembership in entry.GroupMembership)
                        {
                            if (!IsSystemGroup(groupMembership.HRef))
                            {
                                if (item.Group.Length > 0) item.Group += ",";
                                item.Group += GetGroupName(groupMembership.HRef);
                            }
                            haveGroup = true;
                        }
                        if (haveGroup)
                            Contact.Add(item);
                    }
                    else
                    {
                        int x = 1;
                        // ----- no name -----
                    }


                }

                query.StartIndex += contactsCount;
                f = cr.Get<Google.Contacts.Contact>(query);
                contactsCount = f.Entries.Count();
            }
        }

        public Google.Contacts.Contact CreateContact(contacts item)
        {
            Google.Contacts.Contact newEntry = new Google.Contacts.Contact();

            // ----- NAME -----
            newEntry.Name = new Name()
            {
                FullName = item.Name.FullName,
                NamePrefix = item.Name.Prefix,
                GivenName = item.Name.Firstname,
                AdditionalName = item.Name.AdditionalName,
                FamilyName = item.Name.Surname,
                NameSuffix = item.Name.Suffix,
                //FamilyNamePhonetics = ,
                //AdditionalNamePhonetics = ,
                //FamilyNamePhonetics = ,
            };

            // ----- EMAIL -----
            foreach (var email in item.Email)
            {
                newEntry.Emails.Add(new EMail()
                {
                    Primary = email.Primary,
                    Rel = email.Desc,            // ContactsRelationships.IsHome,
                    Address = email.Value
                });
            }

            // ----- PHONE -----
            foreach (var phone in item.Phone)
            {
                newEntry.Phonenumbers.Add(new PhoneNumber()
                {
                    Primary = phone.Primary,
                    Rel = phone.Desc,            // ContactsRelationships.IsWork,
                    Value = phone.Value
                });
            }

            // ----- Organization -----

            // ----- ADDRESS -----
            foreach (var address in item.Address)
            {
                newEntry.PostalAddresses.Add(new StructuredPostalAddress()
                {
                    Rel = address.Tag,            // ContactsRelationships.IsWork,
                    Primary = address.Primary,
                    Street = address.Street,
                    City = address.City,
                    Region = address.Region,
                    Postcode = address.ZipCode,
                    Country = address.Country,
                    FormattedAddress = address.Street + ", " + address.City + ", " + address.Region + ", " + address.Country + ", " + address.ZipCode
                });
            }

            // ----- BIRTHDATE -----


            // ----- DATES -----

            // ----- WEBSITES -----

            // ----- IM -----
            foreach (var im in item.IM)
            {
                newEntry.IMs.Add(new IMAddress()
                {
                    Address = im.Value,
                    Primary = im.Primary,
                    //Rel = //ContactsRelationships.IsHome,
                    Protocol = im.Desc, // ContactsProtocols.IsGoogleTalk,

                });
            }


            // ----- RELATIONS -----

            // ----- NOTE -----
            newEntry.Content = item.Note;

            // ----- google ID -----

            // ----- GROUP -----



            // Insert the contact.
            Uri feedUri = new Uri(ContactsQuery.CreateContactsUri("default"));
            Google.Contacts.Contact createdEntry = cr.Insert(feedUri, newEntry);
            Console.WriteLine("Contact's ID: " + createdEntry.Id);
            return createdEntry;
        }


        public Google.Contacts.Contact UpdateContact(contacts item)
        {
            Uri contactURL = new Uri(item.gID);
            Google.Contacts.Contact newEntry = cr.Retrieve<Google.Contacts.Contact>(contactURL);

            // ----- NAME -----
            newEntry.Name = new Name()
            {
                FullName = item.Name.FullName,
                NamePrefix = item.Name.Prefix,
                GivenName = item.Name.Firstname,
                AdditionalName = item.Name.AdditionalName,
                FamilyName = item.Name.Surname,
                NameSuffix = item.Name.Suffix,
                //FamilyNamePhonetics = ,
                //AdditionalNamePhonetics = ,
                //FamilyNamePhonetics = ,
            };

            // ----- EMAIL -----
            foreach (var email in item.Email)
            {
                newEntry.Emails.Add(new EMail()
                {
                    Primary = email.Primary,
                    Rel = email.Desc,            // ContactsRelationships.IsHome,
                    Address = email.Value
                });
            }

            // ----- PHONE -----
            foreach (var phone in item.Phone)
            {
                newEntry.Phonenumbers.Add(new PhoneNumber()
                {
                    Primary = phone.Primary,
                    Rel = phone.Desc,            // ContactsRelationships.IsWork,
                    Value = phone.Value
                });
            }

            // ----- Organization -----

            // ----- ADDRESS -----
            foreach (var address in item.Address)
            {
                newEntry.PostalAddresses.Add(new StructuredPostalAddress()
                {
                    Rel = address.Tag,            // ContactsRelationships.IsWork,
                    Primary = address.Primary,
                    Street = address.Street,
                    City = address.City,
                    Region = address.Region,
                    Postcode = address.ZipCode,
                    Country = address.Country,
                    FormattedAddress = address.Street + ", " + address.City + ", " + address.Region + ", " + address.Country + ", " + address.ZipCode
                });
            }

            // ----- BIRTHDATE -----


            // ----- DATES -----

            // ----- WEBSITES -----

            // ----- IM -----
            foreach (var im in item.IM)
            {
                newEntry.IMs.Add(new IMAddress()
                {
                    Address = im.Value,
                    Primary = im.Primary,
                    //Rel = //ContactsRelationships.IsHome,
                    Protocol = im.Desc, // ContactsProtocols.IsGoogleTalk,

                });
            }


            // ----- RELATIONS -----

            // ----- NOTE -----
            newEntry.Content = item.Note;

            // ----- google ID -----

            // ----- GROUP -----



            // Insert the contact.
            try
            {
                Google.Contacts.Contact updatedContact = cr.Update(newEntry);
                Console.WriteLine("Updated: " + updatedContact.Updated.ToString());
                return updatedContact;
            }
            catch (GDataVersionConflictException e)
            {
                // Etags mismatch: handle the exception.
            }
            return null;
        }

        public void DeleteContact(Uri contactURL)
        {
            // Retrieving the contact is required in order to get the Etag.
            Google.Contacts.Contact contact = cr.Retrieve<Google.Contacts.Contact>(contactURL);

            try
            {
                cr.Delete(contact);
            }
            catch (GDataVersionConflictException e)
            {
                // Etags mismatch: handle the exception.
            }
        }

        public void DownloadPhoto(Uri contactURL)
        {
            Google.Contacts.Contact contact = cr.Retrieve<Google.Contacts.Contact>(contactURL);

            Stream photoStream = cr.GetPhoto(contact);
            FileStream outStream = File.OpenWrite("test.jpg");
            byte[] buffer = new byte[photoStream.Length];

            photoStream.Read(buffer, 0, (int)photoStream.Length);
            outStream.Write(buffer, 0, (int)photoStream.Length);
            photoStream.Close();
            outStream.Close();
        }

        public void UpdateContactPhoto(Uri contactURL, Stream photoStream)
        {
            Google.Contacts.Contact contact = cr.Retrieve<Google.Contacts.Contact>(contactURL);

            try
            {
                cr.SetPhoto(contact, photoStream);
            }
            catch (GDataVersionConflictException e)
            {
                // Etags mismatch: handle the exception.
            }
        }

        public void DeleteContactPhoto(Uri contactURL)
        {
            Google.Contacts.Contact contact = cr.Retrieve<Google.Contacts.Contact>(contactURL);
            try
            {
                cr.Delete(contact.PhotoUri, contact.PhotoEtag);
            }
            catch (GDataVersionConflictException e)
            {
                // Etags mismatch: handle the exception.
            }
        }

        public string PrintContactList()
        {
            string result = "";
            for (int i = 0; i < Contact.Count; i++)
            {



                if (!string.IsNullOrEmpty(Contact[i].Name.FullName)) result += Contact[i].Name.FullName + ", ";
                if (!string.IsNullOrEmpty(Contact[i].Name.Prefix)) result += Contact[i].Name.Prefix + " ";
                if (!string.IsNullOrEmpty(Contact[i].Name.Firstname)) result += Contact[i].Name.Firstname + " ";
                if (!string.IsNullOrEmpty(Contact[i].Name.AdditionalName)) result += Contact[i].Name.AdditionalName + " ";
                if (!string.IsNullOrEmpty(Contact[i].Name.Surname)) result += Contact[i].Name.Surname + " ";
                if (!string.IsNullOrEmpty(Contact[i].Name.Suffix)) result += Contact[i].Name.Suffix;
                result += ", ";
                if (!string.IsNullOrEmpty(Contact[i].Name.Nick)) result += Contact[i].Name.Nick;

                //result += Contact[i].Group;
                result += Environment.NewLine;
            }
            return result;
        }

        public string PrintCSVContactList()
        {
            string result = "FullName;Prefix;FirstName;AdditionalName;Surname;Suffix;Nick;Group;Email;Phone" + Environment.NewLine;
            for (int i = 0; i < Contact.Count; i++)
            {



                if (!string.IsNullOrEmpty(Contact[i].Name.FullName)) result += Contact[i].Name.FullName; result += ";";
                if (!string.IsNullOrEmpty(Contact[i].Name.Prefix)) result += Contact[i].Name.Prefix; result += ";";
                if (!string.IsNullOrEmpty(Contact[i].Name.Firstname)) result += Contact[i].Name.Firstname; result += ";";
                if (!string.IsNullOrEmpty(Contact[i].Name.AdditionalName)) result += Contact[i].Name.AdditionalName; result += ";";
                if (!string.IsNullOrEmpty(Contact[i].Name.Surname)) result += Contact[i].Name.Surname; result += ";";
                if (!string.IsNullOrEmpty(Contact[i].Name.Suffix)) result += Contact[i].Name.Suffix; result += ";";
                if (!string.IsNullOrEmpty(Contact[i].Name.Nick)) result += Contact[i].Name.Nick; result += ";";
                if (!string.IsNullOrEmpty(Contact[i].Group)) result += Contact[i].Group; result += ";";

                if (Contact[i].Email != null)
                {
                    for (int j = 0; j < Contact[i].Email.Count; j++)
                    {
                        if (j > 0) result += ",";
                        result += Contact[i].Email[j].Value + ":" + Contact[i].Email[j].Desc;
                    }
                }
                result += ";";
                if (Contact[i].Phone != null)
                {
                    for (int j = 0; j < Contact[i].Phone.Count; j++)
                    {
                        if (j > 0) result += ",";
                        result += Contact[i].Phone[j].Value + ":" + Contact[i].Phone[j].Desc;
                    }
                }
                result += ";";

                result += Environment.NewLine;
            }
            return result;
        }

        public string GetGroupName(string ID)
        {
            if (Groups != null)
            {
                for (int i = 0; i < Groups.Count; i++)
                {
                    if (Groups[i].ID == ID)
                    {
                        return Groups[i].Name;
                    }
                }
            }
            return "";
        }

        public bool IsSystemGroup(string ID)
        {
            if (Groups != null)
            {
                for (int i = 0; i < Groups.Count; i++)
                {
                    if (Groups[i].ID == ID)
                    {
                        return Groups[i].System;
                    }
                }
            }
            return false;
        }

    }
}
