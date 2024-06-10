using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0427_MVVM
{
    internal class MessageIO : ObservableCollection<Message>
    {
        public MessageIO()
        {
            this.Add(new Message("Adventure Works", "Hi, what can we do for you?"));
            this.Add(new Message("Client", "Did you receive the GR268 KZ bike?"));
            this.Add(new Message("Adventure Works", "Not yet, but we have a similar model available."));
            this.Add(new Message("Client", "What is it like?"));
            this.Add(new Message("Adventure Works", "It boasts a carbon frame, hydraulic brakes and suspension, and a gear hub."));
            this.Add(new Message("Client", "How much does it cost?"));
            this.Add(new Message("Adventure Works", "Same as the GR268 KZ model you requested. You can get it from our online shop."));
            this.Add(new Message("Client", "Thanks."));
            this.Add(new Message("Adventure Works", "Thank you, have a nice ride."));
        }
    }
}
