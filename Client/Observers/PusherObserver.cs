using Entity;
using System;
using Client;

namespace Client.Observers
{
    /// <summary>
    /// The 'PusherObserver' class
    /// </summary>
    class PusherObserver : Observer
    {
        private string _observerState;

        // Constructor
        public PusherObserver(
          ConcreteSubject subject)
        {
            this.Subject = subject;
        }

        public override void Update()
        {
            try
            {
                _observerState = Subject.SubjectState;
                Message message = Message.Parse(_observerState);
                if (message.MessageType == "Notify")
                {
                    Notification notification = Notification.Parse(message.Payload);

                    // Here code need to push notification to client.

                    Console.WriteLine("Push Message {0} to client.", _observerState);
                }
                else
                {
                    Console.WriteLine("Message Type {0} not Supported yet by Pusher.", message.MessageType);
                }
            }
            catch (Exception ex)
            {
                // log exception here.
                Console.WriteLine("Unexpected error occurred while processing message \"{0}\". Please check format of the message.", _observerState);
            }
        }

        // Gets or sets subject
        public ConcreteSubject Subject { get; set; }
    }
}
