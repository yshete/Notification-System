using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationService.Entities
{
    public class NotificationFilter
    {
        public long? UserId { get; set; }
        public long? NotificationTypeId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public string[] Validate()
        {
            List<string> validationErrors = new();
            if(UserId.HasValue && UserId<=0)
            {
                validationErrors.Add($"{nameof(UserId)} should be greater than 0");   
            }
            if(NotificationTypeId.HasValue && NotificationTypeId<=0)
            {
                validationErrors.Add($"{nameof(NotificationTypeId)} should be greater than 0");   
            }
            if (FromDate > ToDate)
            { 
                validationErrors.Add($"{nameof(FromDate)} should not be greater than {nameof(ToDate)}");
            }
            return validationErrors.ToArray();
        }
    }
}