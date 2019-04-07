using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Entity
{
    public class Notification
    {
        /// <summary>
        /// Message Code to uniquely identify type of event raised based on which payload will be filled.
        /// </summary>
        public string MessageCode
        {
            get;
            set;
        }
        /// <summary>
        /// Temporary code. Payload needs to be filled from db based on MessageCode. Setter needs to be added
        /// </summary>
        [JsonIgnore]
        public string Payload
        {
            get
            {
                switch (MessageCode)
                {
                    case "Booked":
                        return "Table has been booked for you successfully";
                    default:
                        return MessageCode;
                }
            }
        }

        /// <summary>
        /// List of recipients ids.
        /// </summary>
        public IList<long> Recipients
        {
            get;
            set;
        }
        public Notification(string messageCode, IList<long> recipients)
        {
            this.MessageCode = messageCode;
            this.Recipients = recipients;
        }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        public static Notification Parse(string value)
        {
            return JsonConvert.DeserializeObject<Notification>(value);
        }
    }
}
