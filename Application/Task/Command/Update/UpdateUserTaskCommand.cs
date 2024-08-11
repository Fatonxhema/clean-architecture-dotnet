
using Domain.Core.Primitives.Result;
using MediatR;

namespace Application.Task.Command.Update
{
    public class UpdateUserTaskCommand : IRequest<Result<TaskDto>>
    {
        public Guid TaskId { get; set; }
        public Guid UserId { get; set; }
    }
}