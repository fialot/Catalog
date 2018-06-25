using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Google.Contacts;
using Google.GData.Contacts;
using Google.GData.Client;
using Google.GData.Extensions;


namespace Katalog
{
    class GoogleContacts
    {
        RequestSettings settings = new RequestSettings("FialotCatalog");
        // Add authorization token.
        // ...
        ContactsRequest cr = new ContactsRequest(settings);

    }
}
