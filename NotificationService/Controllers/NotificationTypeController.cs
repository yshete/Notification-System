using Microsoft.AspNetCore.Mvc;
using NotificationService.Application.Handlers;
using NotificationService.Entities;
using NotificationService.Filters;
using System.Net;

namespace NotificationService.Controllers;

//[ServiceFilter(typeof(CustomExceptionFilter))]
[Route("api/[controller]")]
[ApiController]
public class NotificationTypeController : BaseApiController
{
    private readonly INotificationTypeHandler notificationTypeHandler;

    public NotificationTypeController(ILogger<NotificationTypeController> logger, INotificationTypeHandler notificationTypeHandler) : base(logger)
    {
        this.notificationTypeHandler = notificationTypeHandler;
    }

    // GET: api/notificationtype
    [HttpGet]
    public async Task<ActionResult<IEnumerable<NotificationType>>> GetAllNotificationTypes(CancellationToken cancellationToken)
    {
        var notificationTypes = await notificationTypeHandler.GetAllAsync(cancellationToken);
        return Ok(notificationTypes);
    }

    // GET: api/notificationtype/5
    [HttpGet("{id}")]
    public async Task<ActionResult<NotificationType>> GetNotificationType(long id, CancellationToken cancellationToken)
    {
        var notificationType = await notificationTypeHandler.GetByIdAsync(id, cancellationToken);
        if (notificationType.HttpStatusCode == HttpStatusCode.NotFound)
        {
            return NotFound();
        }
        if (notificationType.HttpStatusCode == HttpStatusCode.OK)
        {
            return Ok(notificationType.Value);
        }
        return BadRequest(notificationType.Errors);
    }

    // POST: api/notificationtype
    [HttpPost]
    public async Task<ActionResult> CreateNotificationType([FromBody] NotificationType notificationType, CancellationToken cancellationToken)
    {
        notificationType.Id = 0;
        var o = await notificationTypeHandler.AddAsync(notificationType, cancellationToken);
        if (o.Errors.Any())
        {
            return BadRequest(o.Errors);
        }
        return CreatedAtAction(nameof(GetNotificationType), new { id = notificationType.Id }, notificationType);
    }

    // PUT: api/notificationtype/5
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateNotificationType(long id, [FromBody] NotificationType notificationType, CancellationToken cancellationToken)
    {
        if (id != notificationType.Id)
        {
            return BadRequest();
        }

        var o = await notificationTypeHandler.Update(id, notificationType, cancellationToken);
        if (o.HttpStatusCode == HttpStatusCode.NotFound)
        {
            return NotFound();
        }
        if (o.Errors.Any())
        {
            return BadRequest(o.Errors);
        }
        return NoContent();
    }

    // DELETE: api/notificationtype/5
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteNotificationType(long id, CancellationToken cancellationToken)
    {
        var o = await notificationTypeHandler.Delete(id, cancellationToken);
        if (o.HttpStatusCode == HttpStatusCode.NotFound)
        {
            return NotFound();
        }
        if (o.Errors.Any())
        {
            return BadRequest(o.Errors);
        }
        return NoContent();
    }
}
