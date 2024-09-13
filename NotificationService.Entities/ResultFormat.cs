using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace NotificationService.Entities;
public class ResultFormat<T>
{
    public T Value { get; set; }
    public IEnumerable<string> Errors { get; set; }

    public HttpStatusCode HttpStatusCode { get; set; } = HttpStatusCode.OK;
}