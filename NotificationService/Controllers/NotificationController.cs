using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NotificationService.Application.Handlers;
using NotificationService.Entities;
using NotificationService.Filters;

namespace NotificationService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificationController : BaseApiController
{
    private readonly INotificationHandler notificationHandler;
    public NotificationController(ILogger<NotificationController> logger, INotificationHandler notificationHandler) : base(logger)
    {
        this.notificationHandler = notificationHandler;
    }
    // GET: api/notificationtype/5
    [HttpGet("{id}")]
    public async Task<ActionResult<NotificationType>> GetNotification(long id, CancellationToken cancellationToken)
    {
        var notification = await notificationHandler.GetByIdAsync(id, cancellationToken);
        if (notification.HttpStatusCode == HttpStatusCode.NotFound)
        {
            return NotFound();
        }
        if (notification.HttpStatusCode == HttpStatusCode.OK)
        {
            return Ok(notification.Value);
        }
        return BadRequest(notification.Errors);
    }

    [HttpPost("notify")]
    public async Task<IActionResult> NotifyAsync(Notification notification, CancellationToken cancellationToken)
    {
        var o = await notificationHandler.SendNotificationAsync(notification, cancellationToken);
        if (o.Errors.Any())
        {
            return BadRequest(o.Errors);
        }
        return CreatedAtAction(nameof(GetNotification), new { id = o.Value?.Id }, o.Value);
        //return Ok(o.Value);
    }

    [HttpPost("search")]
    public IActionResult SearchNotifications(NotificationFilter notificationFilter, CancellationToken cancellationToken)
    {
        var o = notificationHandler.SearchNotifications(notificationFilter);
        if (o.HttpStatusCode == System.Net.HttpStatusCode.BadRequest)
        {
            return BadRequest(o.Errors);
        }
        return Ok(o.Value);
    }
}
