using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class Message
    {
        public string MessageType { get; set; }
        public string Payload { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        public static Message Parse(string value)
        {
            return JsonConvert.DeserializeObject<Message>(value);
        }
    }
}
